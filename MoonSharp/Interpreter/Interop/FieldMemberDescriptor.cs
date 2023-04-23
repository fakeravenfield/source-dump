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
	// Token: 0x02000860 RID: 2144
	public class FieldMemberDescriptor : IMemberDescriptor, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000240E1 File Offset: 0x000222E1
		// (set) Token: 0x0600351D RID: 13597 RVA: 0x000240E9 File Offset: 0x000222E9
		public FieldInfo FieldInfo { get; private set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000240F2 File Offset: 0x000222F2
		// (set) Token: 0x0600351F RID: 13599 RVA: 0x000240FA File Offset: 0x000222FA
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x00024103 File Offset: 0x00022303
		// (set) Token: 0x06003521 RID: 13601 RVA: 0x0002410B File Offset: 0x0002230B
		public bool IsStatic { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x00024114 File Offset: 0x00022314
		// (set) Token: 0x06003523 RID: 13603 RVA: 0x0002411C File Offset: 0x0002231C
		public string Name { get; private set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x00024125 File Offset: 0x00022325
		// (set) Token: 0x06003525 RID: 13605 RVA: 0x0002412D File Offset: 0x0002232D
		public bool IsConst { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x00024136 File Offset: 0x00022336
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x0002413E File Offset: 0x0002233E
		public bool IsReadonly { get; private set; }

		// Token: 0x06003528 RID: 13608 RVA: 0x00119738 File Offset: 0x00117938
		public static FieldMemberDescriptor TryCreateIfVisible(FieldInfo fi, InteropAccessMode accessMode)
		{
			if (fi.GetVisibilityFromAttributes() ?? fi.IsPublic)
			{
				return new FieldMemberDescriptor(fi, accessMode);
			}
			return null;
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x00119770 File Offset: 0x00117970
		public FieldMemberDescriptor(FieldInfo fi, InteropAccessMode accessMode)
		{
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			this.FieldInfo = fi;
			this.AccessMode = accessMode;
			this.Name = fi.Name;
			this.IsStatic = this.FieldInfo.IsStatic;
			if (this.FieldInfo.IsLiteral)
			{
				this.IsConst = true;
				this.m_ConstValue = this.FieldInfo.GetValue(null);
			}
			else
			{
				this.IsReadonly = this.FieldInfo.IsInitOnly;
			}
			if (this.AccessMode == InteropAccessMode.Preoptimized)
			{
				this.OptimizeGetter();
			}
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x0011980C File Offset: 0x00117A0C
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.IsConst)
			{
				return ClrToScriptConversions.ObjectToDynValue(script, this.m_ConstValue, this.FieldInfo.FieldType);
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
				obj2 = this.FieldInfo.GetValue(obj);
			}
			return ClrToScriptConversions.ObjectToDynValue(script, obj2, this.FieldInfo.FieldType);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x00119890 File Offset: 0x00117A90
		internal void OptimizeGetter()
		{
			if (this.IsConst)
			{
				return;
			}
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				if (this.IsStatic)
				{
					ParameterExpression parameterExpression;
					Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Field(null, this.FieldInfo), typeof(object)), new ParameterExpression[]
					{
						parameterExpression
					});
					Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression.Compile());
				}
				else
				{
					ParameterExpression parameterExpression2;
					Expression<Func<object, object>> expression2 = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Field(Expression.Convert(parameterExpression2, this.FieldInfo.DeclaringType), this.FieldInfo), typeof(object)), new ParameterExpression[]
					{
						parameterExpression2
					});
					Interlocked.Exchange<Func<object, object>>(ref this.m_OptimizedGetter, expression2.Compile());
				}
			}
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x0011998C File Offset: 0x00117B8C
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			if (this.IsReadonly || this.IsConst)
			{
				throw new ScriptRuntimeException("userdata field '{0}.{1}' cannot be written to.", new object[]
				{
					this.FieldInfo.DeclaringType.Name,
					this.Name
				});
			}
			object obj2 = ScriptToClrConversions.DynValueToObjectOfType(v, this.FieldInfo.FieldType, null, false);
			try
			{
				if (obj2 is double)
				{
					obj2 = NumericConversions.DoubleToType(this.FieldInfo.FieldType, (double)obj2);
				}
				this.FieldInfo.SetValue(this.IsStatic ? null : obj, obj2);
			}
			catch (ArgumentException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.FieldInfo.FieldType);
			}
			catch (InvalidCastException)
			{
				throw ScriptRuntimeException.UserDataArgumentTypeMismatch(v.Type, this.FieldInfo.FieldType);
			}
			catch (FieldAccessException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x00024147 File Offset: 0x00022347
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				if (this.IsReadonly || this.IsConst)
				{
					return MemberDescriptorAccess.CanRead;
				}
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite;
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x0002415C File Offset: 0x0002235C
		void IOptimizableDescriptor.Optimize()
		{
			if (this.m_OptimizedGetter == null)
			{
				this.OptimizeGetter();
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00119A8C File Offset: 0x00117C8C
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("visibility", DynValue.NewString(this.FieldInfo.GetClrVisibility()));
			t.Set("name", DynValue.NewString(this.Name));
			t.Set("static", DynValue.NewBoolean(this.IsStatic));
			t.Set("const", DynValue.NewBoolean(this.IsConst));
			t.Set("readonly", DynValue.NewBoolean(this.IsReadonly));
			t.Set("decltype", DynValue.NewString(this.FieldInfo.DeclaringType.FullName));
			t.Set("declvtype", DynValue.NewBoolean(Framework.Do.IsValueType(this.FieldInfo.DeclaringType)));
			t.Set("type", DynValue.NewString(this.FieldInfo.FieldType.FullName));
			t.Set("read", DynValue.NewBoolean(true));
			t.Set("write", DynValue.NewBoolean(!this.IsConst && !this.IsReadonly));
		}

		// Token: 0x04002DFF RID: 11775
		private object m_ConstValue;

		// Token: 0x04002E00 RID: 11776
		private Func<object, object> m_OptimizedGetter;
	}
}
