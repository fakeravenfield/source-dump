using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000868 RID: 2152
	public class PropertyMemberDescriptor : IMemberDescriptor, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x00024356 File Offset: 0x00022556
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x0002435E File Offset: 0x0002255E
		public PropertyInfo PropertyInfo { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x00024367 File Offset: 0x00022567
		// (set) Token: 0x06003565 RID: 13669 RVA: 0x0002436F File Offset: 0x0002256F
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x00024378 File Offset: 0x00022578
		// (set) Token: 0x06003567 RID: 13671 RVA: 0x00024380 File Offset: 0x00022580
		public bool IsStatic { get; private set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x00024389 File Offset: 0x00022589
		// (set) Token: 0x06003569 RID: 13673 RVA: 0x00024391 File Offset: 0x00022591
		public string Name { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x0002439A File Offset: 0x0002259A
		public bool CanRead
		{
			get
			{
				return this.m_Getter != null;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000243A8 File Offset: 0x000225A8
		public bool CanWrite
		{
			get
			{
				return this.m_Setter != null;
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x0011AA6C File Offset: 0x00118C6C
		public static PropertyMemberDescriptor TryCreateIfVisible(PropertyInfo pi, InteropAccessMode accessMode)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(pi);
			MethodInfo setMethod = Framework.Do.GetSetMethod(pi);
			bool? visibilityFromAttributes = pi.GetVisibilityFromAttributes();
			bool? visibilityFromAttributes2 = getMethod.GetVisibilityFromAttributes();
			bool? visibilityFromAttributes3 = setMethod.GetVisibilityFromAttributes();
			if (visibilityFromAttributes != null)
			{
				return PropertyMemberDescriptor.TryCreate(pi, accessMode, (visibilityFromAttributes2 ?? visibilityFromAttributes.Value) ? getMethod : null, (visibilityFromAttributes3 ?? visibilityFromAttributes.Value) ? setMethod : null);
			}
			return PropertyMemberDescriptor.TryCreate(pi, accessMode, (visibilityFromAttributes2 ?? getMethod.IsPublic) ? getMethod : null, (visibilityFromAttributes3 ?? setMethod.IsPublic) ? setMethod : null);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000243B6 File Offset: 0x000225B6
		private static PropertyMemberDescriptor TryCreate(PropertyInfo pi, InteropAccessMode accessMode, MethodInfo getter, MethodInfo setter)
		{
			if (getter == null && setter == null)
			{
				return null;
			}
			return new PropertyMemberDescriptor(pi, accessMode, getter, setter);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000243D5 File Offset: 0x000225D5
		public PropertyMemberDescriptor(PropertyInfo pi, InteropAccessMode accessMode) : this(pi, accessMode, Framework.Do.GetGetMethod(pi), Framework.Do.GetSetMethod(pi))
		{
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x0011AB48 File Offset: 0x00118D48
		public PropertyMemberDescriptor(PropertyInfo pi, InteropAccessMode accessMode, MethodInfo getter, MethodInfo setter)
		{
			if (getter == null && setter == null)
			{
				throw new ArgumentNullException("getter and setter cannot both be null");
			}
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			this.PropertyInfo = pi;
			this.AccessMode = accessMode;
			this.Name = pi.Name;
			this.m_Getter = getter;
			this.m_Setter = setter;
			this.IsStatic = (this.m_Getter ?? this.m_Setter).IsStatic;
			if (this.AccessMode == InteropAccessMode.Preoptimized)
			{
				this.OptimizeGetter();
				this.OptimizeSetter();
			}
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x0011ABE8 File Offset: 0x00118DE8
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.m_Getter == null)
			{
				throw new ScriptRuntimeException("userdata property '{0}.{1}' cannot be read from.", new object[]
				{
					this.PropertyInfo.DeclaringType.Name,
					this.Name
				});
			}
			if (this.AccessMode == InteropAccessMode.LazyOptimized && this.m_OptimizedGetter == null)
			{
				this.OptimizeGetter();
			}
			object obj2;
			if (this.m_OptimizedGetter != null)
			{
				obj2 = this.m_OptimizedGetter(obj);
			}
			else
			{
				obj2 = this.m_Getter.Invoke(this.IsStatic ? null : obj, null);
			}
			return ClrToScriptConversions.ObjectToDynValue(script, obj2, this.PropertyInfo.PropertyType);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x0011AC94 File Offset: 0x00118E94
		internal void OptimizeGetter()
		{
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				if (this.m_Getter != null)
				{
					if (this.IsStatic)
					{
						ParameterExpression parameterExpression;
						Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Property(null, this.PropertyInfo), typeof(object)), new ParameterExpression[]
						{
							parameterExpression
						});
						Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression.Compile());
					}
					else
					{
						ParameterExpression parameterExpression2;
						Expression<Func<object, object>> expression2 = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Property(Expression.Convert(parameterExpression2, this.PropertyInfo.DeclaringType), this.PropertyInfo), typeof(object)), new ParameterExpression[]
						{
							parameterExpression2
						});
						Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression2.Compile());
					}
				}
			}
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x0011AD98 File Offset: 0x00118F98
		internal void OptimizeSetter()
		{
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				if (this.m_Setter != null && !Framework.Do.IsValueType(this.PropertyInfo.DeclaringType))
				{
					MethodInfo setMethod = Framework.Do.GetSetMethod(this.PropertyInfo);
					if (this.IsStatic)
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "dummy");
						ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "val");
						UnaryExpression arg = Expression.Convert(parameterExpression2, this.PropertyInfo.PropertyType);
						Expression<Action<object, object>> expression = Expression.Lambda<Action<object, object>>(Expression.Call(setMethod, arg), new ParameterExpression[]
						{
							parameterExpression,
							parameterExpression2
						});
						Interlocked.Exchange<Action<object, object>>(ref this.m_OptimizedSetter, expression.Compile());
					}
					else
					{
						ParameterExpression parameterExpression3 = Expression.Parameter(typeof(object), "obj");
						ParameterExpression parameterExpression4 = Expression.Parameter(typeof(object), "val");
						Expression instance = Expression.Convert(parameterExpression3, this.PropertyInfo.DeclaringType);
						UnaryExpression unaryExpression = Expression.Convert(parameterExpression4, this.PropertyInfo.PropertyType);
						Expression<Action<object, object>> expression2 = Expression.Lambda<Action<object, object>>(Expression.Call(instance, setMethod, new Expression[]
						{
							unaryExpression
						}), new ParameterExpression[]
						{
							parameterExpression3,
							parameterExpression4
						});
						Interlocked.Exchange<Action<object, object>>(ref this.m_OptimizedSetter, expression2.Compile());
					}
				}
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0011AF18 File Offset: 0x00119118
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			if (this.m_Setter == null)
			{
				throw new ScriptRuntimeException("userdata property '{0}.{1}' cannot be written to.", new object[]
				{
					this.PropertyInfo.DeclaringType.Name,
					this.Name
				});
			}
			object obj2 = ScriptToClrConversions.DynValueToObjectOfType(v, this.PropertyInfo.PropertyType, null, false);
			try
			{
				if (obj2 is double)
				{
					obj2 = NumericConversions.DoubleToType(this.PropertyInfo.PropertyType, (double)obj2);
				}
				if (this.AccessMode == InteropAccessMode.LazyOptimized && this.m_OptimizedSetter == null)
				{
					this.OptimizeSetter();
				}
				if (this.m_OptimizedSetter != null)
				{
					this.m_OptimizedSetter(obj, obj2);
				}
				else
				{
					this.m_Setter.Invoke(this.IsStatic ? null : obj, new object[]
					{
						obj2
					});
				}
			}
			catch (ArgumentException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.PropertyInfo.PropertyType);
			}
			catch (InvalidCastException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.PropertyInfo.PropertyType);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x0011B03C File Offset: 0x0011923C
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				MemberDescriptorAccess memberDescriptorAccess = (MemberDescriptorAccess)0;
				if (this.m_Setter != null)
				{
					memberDescriptorAccess |= MemberDescriptorAccess.CanWrite;
				}
				if (this.m_Getter != null)
				{
					memberDescriptorAccess |= MemberDescriptorAccess.CanRead;
				}
				return memberDescriptorAccess;
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000243F5 File Offset: 0x000225F5
		void IOptimizableDescriptor.Optimize()
		{
			this.OptimizeGetter();
			this.OptimizeSetter();
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x0011B070 File Offset: 0x00119270
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("visibility", DynValue.NewString(this.PropertyInfo.GetClrVisibility()));
			t.Set("name", DynValue.NewString(this.Name));
			t.Set("static", DynValue.NewBoolean(this.IsStatic));
			t.Set("read", DynValue.NewBoolean(this.CanRead));
			t.Set("write", DynValue.NewBoolean(this.CanWrite));
			t.Set("decltype", DynValue.NewString(this.PropertyInfo.DeclaringType.FullName));
			t.Set("declvtype", DynValue.NewBoolean(Framework.Do.IsValueType(this.PropertyInfo.DeclaringType)));
			t.Set("type", DynValue.NewString(this.PropertyInfo.PropertyType.FullName));
		}

		// Token: 0x04002E25 RID: 11813
		private MethodInfo m_Getter;

		// Token: 0x04002E26 RID: 11814
		private MethodInfo m_Setter;

		// Token: 0x04002E27 RID: 11815
		private Func<object, object> m_OptimizedGetter;

		// Token: 0x04002E28 RID: 11816
		private Action<object, object> m_OptimizedSetter;
	}
}
