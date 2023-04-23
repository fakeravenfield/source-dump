using System;
using System.Collections.Generic;
using System.Reflection;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007A5 RID: 1957
	public sealed class CallbackFunction : RefIdObject
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x00021294 File Offset: 0x0001F494
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x0002129C File Offset: 0x0001F49C
		public string Name { get; private set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000212A5 File Offset: 0x0001F4A5
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000212AD File Offset: 0x0001F4AD
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> ClrCallback { get; private set; }

		// Token: 0x06003046 RID: 12358 RVA: 0x000212B6 File Offset: 0x0001F4B6
		public CallbackFunction(Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack, string name = null)
		{
			this.ClrCallback = callBack;
			this.Name = name;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0010DB30 File Offset: 0x0010BD30
		public DynValue Invoke(ScriptExecutionContext executionContext, IList<DynValue> args, bool isMethodCall = false)
		{
			if (isMethodCall)
			{
				ColonOperatorBehaviour colonOperatorClrCallbackBehaviour = executionContext.GetScript().Options.ColonOperatorClrCallbackBehaviour;
				if (colonOperatorClrCallbackBehaviour == ColonOperatorBehaviour.TreatAsColon)
				{
					isMethodCall = false;
				}
				else if (colonOperatorClrCallbackBehaviour == ColonOperatorBehaviour.TreatAsDotOnUserData)
				{
					isMethodCall = (args.Count > 0 && args[0].Type == DataType.UserData);
				}
			}
			return this.ClrCallback(executionContext, new CallbackArguments(args, isMethodCall));
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x000212CC File Offset: 0x0001F4CC
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x000212D3 File Offset: 0x0001F4D3
		public static InteropAccessMode DefaultAccessMode
		{
			get
			{
				return CallbackFunction.m_DefaultAccessMode;
			}
			set
			{
				if (value == InteropAccessMode.Default || value == InteropAccessMode.HideMembers || value == InteropAccessMode.BackgroundOptimized)
				{
					throw new ArgumentException("DefaultAccessMode");
				}
				CallbackFunction.m_DefaultAccessMode = value;
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000212F2 File Offset: 0x0001F4F2
		public static CallbackFunction FromDelegate(Script script, Delegate del, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = CallbackFunction.m_DefaultAccessMode;
			}
			return new MethodMemberDescriptor(del.Method, accessMode).GetCallbackFunction(script, del.Target);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x00021317 File Offset: 0x0001F517
		public static CallbackFunction FromMethodInfo(Script script, MethodInfo mi, object obj = null, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = CallbackFunction.m_DefaultAccessMode;
			}
			return new MethodMemberDescriptor(mi, accessMode).GetCallbackFunction(script, obj);
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x00021332 File Offset: 0x0001F532
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x0002133A File Offset: 0x0001F53A
		public object AdditionalData { get; set; }

		// Token: 0x0600304E RID: 12366 RVA: 0x0010DB90 File Offset: 0x0010BD90
		public static bool CheckCallbackSignature(MethodInfo mi, bool requirePublicVisibility)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			return parameters.Length == 2 && parameters[0].ParameterType == typeof(ScriptExecutionContext) && parameters[1].ParameterType == typeof(CallbackArguments) && mi.ReturnType == typeof(DynValue) && (requirePublicVisibility || mi.IsPublic);
		}

		// Token: 0x04002BB0 RID: 11184
		private static InteropAccessMode m_DefaultAccessMode = InteropAccessMode.LazyOptimized;
	}
}
