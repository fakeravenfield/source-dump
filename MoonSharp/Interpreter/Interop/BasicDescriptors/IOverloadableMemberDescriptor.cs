using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000898 RID: 2200
	public interface IOverloadableMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x0600371C RID: 14108
		DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args);

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600371D RID: 14109
		Type ExtensionMethodType { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600371E RID: 14110
		ParameterDescriptor[] Parameters { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600371F RID: 14111
		Type VarArgsArrayType { get; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06003720 RID: 14112
		Type VarArgsElementType { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06003721 RID: 14113
		string SortDiscriminant { get; }
	}
}
