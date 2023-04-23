using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000846 RID: 2118
	public abstract class FunctionMemberDescriptorBase : IOverloadableMemberDescriptor, IMemberDescriptor
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x00023E3A File Offset: 0x0002203A
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x00023E42 File Offset: 0x00022042
		public bool IsStatic { get; private set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x00023E4B File Offset: 0x0002204B
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x00023E53 File Offset: 0x00022053
		public string Name { get; private set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x00023E5C File Offset: 0x0002205C
		// (set) Token: 0x06003487 RID: 13447 RVA: 0x00023E64 File Offset: 0x00022064
		public string SortDiscriminant { get; private set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x00023E6D File Offset: 0x0002206D
		// (set) Token: 0x06003489 RID: 13449 RVA: 0x00023E75 File Offset: 0x00022075
		public ParameterDescriptor[] Parameters { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x00023E7E File Offset: 0x0002207E
		// (set) Token: 0x0600348B RID: 13451 RVA: 0x00023E86 File Offset: 0x00022086
		public Type ExtensionMethodType { get; private set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x00023E8F File Offset: 0x0002208F
		// (set) Token: 0x0600348D RID: 13453 RVA: 0x00023E97 File Offset: 0x00022097
		public Type VarArgsArrayType { get; private set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x00023EA0 File Offset: 0x000220A0
		// (set) Token: 0x0600348F RID: 13455 RVA: 0x00023EA8 File Offset: 0x000220A8
		public Type VarArgsElementType { get; private set; }

		// Token: 0x06003490 RID: 13456 RVA: 0x0011896C File Offset: 0x00116B6C
		protected void Initialize(string funcName, bool isStatic, ParameterDescriptor[] parameters, bool isExtensionMethod)
		{
			this.Name = funcName;
			this.IsStatic = isStatic;
			this.Parameters = parameters;
			if (isExtensionMethod)
			{
				this.ExtensionMethodType = this.Parameters[0].Type;
			}
			if (this.Parameters.Length != 0 && this.Parameters[this.Parameters.Length - 1].IsVarArgs)
			{
				this.VarArgsArrayType = this.Parameters[this.Parameters.Length - 1].Type;
				this.VarArgsElementType = this.Parameters[this.Parameters.Length - 1].Type.GetElementType();
			}
			this.SortDiscriminant = string.Join(":", (from pi in this.Parameters
			select pi.Type.FullName).ToArray<string>());
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x00023EB1 File Offset: 0x000220B1
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj = null)
		{
			return (ScriptExecutionContext c, CallbackArguments a) => this.Execute(script, obj, c, a);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x00023ED8 File Offset: 0x000220D8
		public CallbackFunction GetCallbackFunction(Script script, object obj = null)
		{
			return new CallbackFunction(this.GetCallback(script, obj), this.Name);
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x00023EED File Offset: 0x000220ED
		public DynValue GetCallbackAsDynValue(Script script, object obj = null)
		{
			return DynValue.NewCallback(this.GetCallbackFunction(script, obj));
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x00023EFC File Offset: 0x000220FC
		public static DynValue CreateCallbackDynValue(Script script, MethodInfo mi, object obj = null)
		{
			return new MethodMemberDescriptor(mi, InteropAccessMode.Default).GetCallbackAsDynValue(script, obj);
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x00118A44 File Offset: 0x00116C44
		protected virtual object[] BuildArgumentList(Script script, object obj, ScriptExecutionContext context, CallbackArguments args, out List<int> outParams)
		{
			ParameterDescriptor[] parameters = this.Parameters;
			object[] array = new object[parameters.Length];
			int num = args.IsMethodCall ? 1 : 0;
			outParams = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (parameters[i].Type.IsByRef)
				{
					if (outParams == null)
					{
						outParams = new List<int>();
					}
					outParams.Add(i);
				}
				if (this.ExtensionMethodType != null && obj != null && i == 0)
				{
					array[i] = obj;
				}
				else if (parameters[i].Type == typeof(Script))
				{
					array[i] = script;
				}
				else if (parameters[i].Type == typeof(ScriptExecutionContext))
				{
					array[i] = context;
				}
				else if (parameters[i].Type == typeof(CallbackArguments))
				{
					array[i] = args.SkipMethodCall();
				}
				else if (parameters[i].IsOut)
				{
					array[i] = null;
				}
				else if (i == parameters.Length - 1 && this.VarArgsArrayType != null)
				{
					List<DynValue> list = new List<DynValue>();
					for (;;)
					{
						DynValue dynValue = args.RawGet(num, false);
						num++;
						if (dynValue == null)
						{
							break;
						}
						list.Add(dynValue);
					}
					if (list.Count == 1)
					{
						DynValue dynValue2 = list[0];
						if (dynValue2.Type == DataType.UserData && dynValue2.UserData.Object != null && Framework.Do.IsAssignableFrom(this.VarArgsArrayType, dynValue2.UserData.Object.GetType()))
						{
							array[i] = dynValue2.UserData.Object;
							goto IL_218;
						}
					}
					Array array2 = Array.CreateInstance(this.VarArgsElementType, list.Count);
					for (int j = 0; j < list.Count; j++)
					{
						array2.SetValue(ScriptToClrConversions.DynValueToObjectOfType(list[j], this.VarArgsElementType, null, false), j);
					}
					array[i] = array2;
				}
				else
				{
					DynValue value = args.RawGet(num, false) ?? DynValue.Void;
					array[i] = ScriptToClrConversions.DynValueToObjectOfType(value, parameters[i].Type, parameters[i].DefaultValue, parameters[i].HasDefaultValue);
					num++;
				}
				IL_218:;
			}
			return array;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00118C78 File Offset: 0x00116E78
		protected static DynValue BuildReturnValue(Script script, List<int> outParams, object[] pars, object retv, Type returnType = null)
		{
			if (outParams == null)
			{
				return ClrToScriptConversions.ObjectToDynValue(script, retv, returnType);
			}
			DynValue[] array = new DynValue[outParams.Count + 1];
			if (retv is DynValue && ((DynValue)retv).IsVoid())
			{
				array[0] = DynValue.Nil;
			}
			else
			{
				array[0] = ClrToScriptConversions.ObjectToDynValue(script, retv, null);
			}
			for (int i = 0; i < outParams.Count; i++)
			{
				array[i + 1] = ClrToScriptConversions.ObjectToDynValue(script, pars[outParams[i]], null);
			}
			return DynValue.NewTuple(array);
		}

		// Token: 0x06003497 RID: 13463
		public abstract DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00023F0C File Offset: 0x0002210C
		public virtual DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			return this.GetCallbackAsDynValue(script, obj);
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x00023F1E File Offset: 0x0002211E
		public virtual void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}
	}
}
