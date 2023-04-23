using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000861 RID: 2145
	public class MethodMemberDescriptor : FunctionMemberDescriptorBase, IOptimizableDescriptor, IWireableDescriptor
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x0002416C File Offset: 0x0002236C
		// (set) Token: 0x06003531 RID: 13617 RVA: 0x00024174 File Offset: 0x00022374
		public MethodBase MethodInfo { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x0002417D File Offset: 0x0002237D
		// (set) Token: 0x06003533 RID: 13619 RVA: 0x00024185 File Offset: 0x00022385
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x0002418E File Offset: 0x0002238E
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x00024196 File Offset: 0x00022396
		public bool IsConstructor { get; private set; }

		// Token: 0x06003536 RID: 13622 RVA: 0x00119BC4 File Offset: 0x00117DC4
		public MethodMemberDescriptor(MethodBase methodBase, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			MethodMemberDescriptor.CheckMethodIsCompatible(methodBase, true);
			this.IsConstructor = (methodBase is ConstructorInfo);
			this.MethodInfo = methodBase;
			bool isStatic = methodBase.IsStatic || this.IsConstructor;
			MethodInfo methodInfo = methodBase as MethodInfo;
			this.m_ReturnType = ((methodInfo != null) ? methodInfo.ReturnType : null);
			if (this.IsConstructor)
			{
				this.m_IsAction = false;
			}
			else
			{
				this.m_IsAction = (this.m_ReturnType == typeof(void));
			}
			ParameterInfo[] parameters = methodBase.GetParameters();
			ParameterDescriptor[] array;
			if (this.MethodInfo.DeclaringType.IsArray)
			{
				this.m_IsArrayCtor = true;
				int arrayRank = this.MethodInfo.DeclaringType.GetArrayRank();
				array = new ParameterDescriptor[arrayRank];
				for (int i = 0; i < arrayRank; i++)
				{
					array[i] = new ParameterDescriptor("idx" + i.ToString(), typeof(int), false, null, false, false, false);
				}
			}
			else
			{
				array = (from pi in parameters
				select new ParameterDescriptor(pi)).ToArray<ParameterDescriptor>();
			}
			bool isExtensionMethod = methodBase.IsStatic && array.Length != 0 && methodBase.GetCustomAttributes(typeof(ExtensionAttribute), false).Any<object>();
			base.Initialize(methodBase.Name, isStatic, array, isExtensionMethod);
			if (Script.GlobalOptions.Platform.IsRunningOnAOT())
			{
				accessMode = InteropAccessMode.Reflection;
			}
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = UserData.DefaultAccessMode;
			}
			if (accessMode == InteropAccessMode.HideMembers)
			{
				throw new ArgumentException("Invalid accessMode");
			}
			if (array.Any((ParameterDescriptor p) => p.Type.IsByRef))
			{
				accessMode = InteropAccessMode.Reflection;
			}
			this.AccessMode = accessMode;
			if (this.AccessMode == InteropAccessMode.Preoptimized)
			{
				((IOptimizableDescriptor)this).Optimize();
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x00119D98 File Offset: 0x00117F98
		public static MethodMemberDescriptor TryCreateIfVisible(MethodBase methodBase, InteropAccessMode accessMode, bool forceVisibility = false)
		{
			if (!MethodMemberDescriptor.CheckMethodIsCompatible(methodBase, false))
			{
				return null;
			}
			if (forceVisibility || (methodBase.GetVisibilityFromAttributes() ?? methodBase.IsPublic))
			{
				return new MethodMemberDescriptor(methodBase, accessMode);
			}
			return null;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x00119DE0 File Offset: 0x00117FE0
		public static bool CheckMethodIsCompatible(MethodBase methodBase, bool throwException)
		{
			if (methodBase.ContainsGenericParameters)
			{
				if (throwException)
				{
					throw new ArgumentException("Method cannot contain unresolved generic parameters");
				}
				return false;
			}
			else
			{
				if (!methodBase.GetParameters().Any((ParameterInfo p) => p.ParameterType.IsPointer))
				{
					MethodInfo methodInfo = methodBase as MethodInfo;
					if (methodInfo != null)
					{
						if (methodInfo.ReturnType.IsPointer)
						{
							if (throwException)
							{
								throw new ArgumentException("Method cannot have a pointer return type");
							}
							return false;
						}
						else if (Framework.Do.IsGenericTypeDefinition(methodInfo.ReturnType))
						{
							if (throwException)
							{
								throw new ArgumentException("Method cannot have an unresolved generic return type");
							}
							return false;
						}
					}
					return true;
				}
				if (throwException)
				{
					throw new ArgumentException("Method cannot contain pointer parameters");
				}
				return false;
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x00119E94 File Offset: 0x00118094
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanExecute, obj);
			if (this.AccessMode == InteropAccessMode.LazyOptimized && this.m_OptimizedFunc == null && this.m_OptimizedAction == null)
			{
				((IOptimizableDescriptor)this).Optimize();
			}
			List<int> outParams = null;
			object[] array = base.BuildArgumentList(script, obj, context, args, out outParams);
			object retv;
			if (this.m_OptimizedFunc != null)
			{
				retv = this.m_OptimizedFunc(obj, array);
			}
			else if (this.m_OptimizedAction != null)
			{
				this.m_OptimizedAction(obj, array);
				retv = DynValue.Void;
			}
			else if (this.m_IsAction)
			{
				this.MethodInfo.Invoke(obj, array);
				retv = DynValue.Void;
			}
			else if (this.IsConstructor)
			{
				retv = ((ConstructorInfo)this.MethodInfo).Invoke(array);
			}
			else
			{
				retv = this.MethodInfo.Invoke(obj, array);
			}
			return FunctionMemberDescriptorBase.BuildReturnValue(script, outParams, array, retv, this.m_ReturnType);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x00119F68 File Offset: 0x00118168
		void IOptimizableDescriptor.Optimize()
		{
			ParameterDescriptor[] parameters = base.Parameters;
			if (this.AccessMode == InteropAccessMode.Reflection)
			{
				return;
			}
			MethodInfo methodInfo = this.MethodInfo as MethodInfo;
			if (methodInfo == null)
			{
				return;
			}
			using (PerformanceStatistics.StartGlobalStopwatch(PerformanceCounter.AdaptersCompilation))
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "pars");
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "instance");
				UnaryExpression instance = Expression.Convert(parameterExpression2, this.MethodInfo.DeclaringType);
				Expression[] array = new Expression[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i].OriginalType.IsByRef)
					{
						throw new InternalErrorException("Out/Ref params cannot be precompiled.");
					}
					BinaryExpression expression = Expression.ArrayIndex(parameterExpression, Expression.Constant(i));
					array[i] = Expression.Convert(expression, parameters[i].OriginalType);
				}
				Expression expression2;
				if (base.IsStatic)
				{
					expression2 = Expression.Call(methodInfo, array);
				}
				else
				{
					expression2 = Expression.Call(instance, methodInfo, array);
				}
				if (this.m_IsAction)
				{
					Expression<Action<object, object[]>> expression3 = Expression.Lambda<Action<object, object[]>>(expression2, new ParameterExpression[]
					{
						parameterExpression2,
						parameterExpression
					});
					Interlocked.Exchange<Action<object, object[]>>(ref this.m_OptimizedAction, expression3.Compile());
				}
				else
				{
					Expression<Func<object, object[], object>> expression4 = Expression.Lambda<Func<object, object[], object>>(Expression.Convert(expression2, typeof(object)), new ParameterExpression[]
					{
						parameterExpression2,
						parameterExpression
					});
					Interlocked.Exchange<Func<object, object[], object>>(ref this.m_OptimizedFunc, expression4.Compile());
				}
			}
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x0011A0F8 File Offset: 0x001182F8
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(base.Name));
			t.Set("ctor", DynValue.NewBoolean(this.IsConstructor));
			t.Set("special", DynValue.NewBoolean(this.MethodInfo.IsSpecialName));
			t.Set("visibility", DynValue.NewString(this.MethodInfo.GetClrVisibility()));
			if (this.IsConstructor)
			{
				t.Set("ret", DynValue.NewString(((ConstructorInfo)this.MethodInfo).DeclaringType.FullName));
			}
			else
			{
				t.Set("ret", DynValue.NewString(((MethodInfo)this.MethodInfo).ReturnType.FullName));
			}
			if (this.m_IsArrayCtor)
			{
				t.Set("arraytype", DynValue.NewString(this.MethodInfo.DeclaringType.GetElementType().FullName));
			}
			t.Set("decltype", DynValue.NewString(this.MethodInfo.DeclaringType.FullName));
			t.Set("static", DynValue.NewBoolean(base.IsStatic));
			t.Set("extension", DynValue.NewBoolean(base.ExtensionMethodType != null));
			DynValue dynValue = DynValue.NewPrimeTable();
			t.Set("params", dynValue);
			int num = 0;
			foreach (ParameterDescriptor parameterDescriptor in base.Parameters)
			{
				DynValue dynValue2 = DynValue.NewPrimeTable();
				dynValue.Table.Set(++num, dynValue2);
				parameterDescriptor.PrepareForWiring(dynValue2.Table);
			}
		}

		// Token: 0x04002E04 RID: 11780
		private Func<object, object[], object> m_OptimizedFunc;

		// Token: 0x04002E05 RID: 11781
		private Action<object, object[]> m_OptimizedAction;

		// Token: 0x04002E06 RID: 11782
		private bool m_IsAction;

		// Token: 0x04002E07 RID: 11783
		private bool m_IsArrayCtor;

		// Token: 0x04002E08 RID: 11784
		private Type m_ReturnType;
	}
}
