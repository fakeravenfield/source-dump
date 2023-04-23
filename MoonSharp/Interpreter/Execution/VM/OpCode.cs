using System;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008B1 RID: 2225
	internal enum OpCode
	{
		// Token: 0x04002F20 RID: 12064
		Nop,
		// Token: 0x04002F21 RID: 12065
		Debug,
		// Token: 0x04002F22 RID: 12066
		Pop,
		// Token: 0x04002F23 RID: 12067
		Copy,
		// Token: 0x04002F24 RID: 12068
		Swap,
		// Token: 0x04002F25 RID: 12069
		Literal,
		// Token: 0x04002F26 RID: 12070
		Closure,
		// Token: 0x04002F27 RID: 12071
		NewTable,
		// Token: 0x04002F28 RID: 12072
		TblInitN,
		// Token: 0x04002F29 RID: 12073
		TblInitI,
		// Token: 0x04002F2A RID: 12074
		StoreLcl,
		// Token: 0x04002F2B RID: 12075
		Local,
		// Token: 0x04002F2C RID: 12076
		StoreUpv,
		// Token: 0x04002F2D RID: 12077
		Upvalue,
		// Token: 0x04002F2E RID: 12078
		IndexSet,
		// Token: 0x04002F2F RID: 12079
		Index,
		// Token: 0x04002F30 RID: 12080
		IndexSetN,
		// Token: 0x04002F31 RID: 12081
		IndexN,
		// Token: 0x04002F32 RID: 12082
		IndexSetL,
		// Token: 0x04002F33 RID: 12083
		IndexL,
		// Token: 0x04002F34 RID: 12084
		Clean,
		// Token: 0x04002F35 RID: 12085
		Meta,
		// Token: 0x04002F36 RID: 12086
		BeginFn,
		// Token: 0x04002F37 RID: 12087
		Args,
		// Token: 0x04002F38 RID: 12088
		Call,
		// Token: 0x04002F39 RID: 12089
		ThisCall,
		// Token: 0x04002F3A RID: 12090
		Ret,
		// Token: 0x04002F3B RID: 12091
		Jump,
		// Token: 0x04002F3C RID: 12092
		Jf,
		// Token: 0x04002F3D RID: 12093
		JNil,
		// Token: 0x04002F3E RID: 12094
		JFor,
		// Token: 0x04002F3F RID: 12095
		JtOrPop,
		// Token: 0x04002F40 RID: 12096
		JfOrPop,
		// Token: 0x04002F41 RID: 12097
		Concat,
		// Token: 0x04002F42 RID: 12098
		LessEq,
		// Token: 0x04002F43 RID: 12099
		Less,
		// Token: 0x04002F44 RID: 12100
		Eq,
		// Token: 0x04002F45 RID: 12101
		Add,
		// Token: 0x04002F46 RID: 12102
		Sub,
		// Token: 0x04002F47 RID: 12103
		Mul,
		// Token: 0x04002F48 RID: 12104
		Div,
		// Token: 0x04002F49 RID: 12105
		Mod,
		// Token: 0x04002F4A RID: 12106
		Not,
		// Token: 0x04002F4B RID: 12107
		Len,
		// Token: 0x04002F4C RID: 12108
		Neg,
		// Token: 0x04002F4D RID: 12109
		Power,
		// Token: 0x04002F4E RID: 12110
		CNot,
		// Token: 0x04002F4F RID: 12111
		MkTuple,
		// Token: 0x04002F50 RID: 12112
		Scalar,
		// Token: 0x04002F51 RID: 12113
		Incr,
		// Token: 0x04002F52 RID: 12114
		ToNum,
		// Token: 0x04002F53 RID: 12115
		ToBool,
		// Token: 0x04002F54 RID: 12116
		ExpTuple,
		// Token: 0x04002F55 RID: 12117
		IterPrep,
		// Token: 0x04002F56 RID: 12118
		IterUpd,
		// Token: 0x04002F57 RID: 12119
		Invalid
	}
}
