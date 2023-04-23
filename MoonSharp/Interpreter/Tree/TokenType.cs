using System;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007E9 RID: 2025
	internal enum TokenType
	{
		// Token: 0x04002CA5 RID: 11429
		Eof,
		// Token: 0x04002CA6 RID: 11430
		HashBang,
		// Token: 0x04002CA7 RID: 11431
		Name,
		// Token: 0x04002CA8 RID: 11432
		And,
		// Token: 0x04002CA9 RID: 11433
		Break,
		// Token: 0x04002CAA RID: 11434
		Do,
		// Token: 0x04002CAB RID: 11435
		Else,
		// Token: 0x04002CAC RID: 11436
		ElseIf,
		// Token: 0x04002CAD RID: 11437
		End,
		// Token: 0x04002CAE RID: 11438
		False,
		// Token: 0x04002CAF RID: 11439
		For,
		// Token: 0x04002CB0 RID: 11440
		Function,
		// Token: 0x04002CB1 RID: 11441
		Lambda,
		// Token: 0x04002CB2 RID: 11442
		Goto,
		// Token: 0x04002CB3 RID: 11443
		If,
		// Token: 0x04002CB4 RID: 11444
		In,
		// Token: 0x04002CB5 RID: 11445
		Local,
		// Token: 0x04002CB6 RID: 11446
		Nil,
		// Token: 0x04002CB7 RID: 11447
		Not,
		// Token: 0x04002CB8 RID: 11448
		Or,
		// Token: 0x04002CB9 RID: 11449
		Repeat,
		// Token: 0x04002CBA RID: 11450
		Return,
		// Token: 0x04002CBB RID: 11451
		Then,
		// Token: 0x04002CBC RID: 11452
		True,
		// Token: 0x04002CBD RID: 11453
		Until,
		// Token: 0x04002CBE RID: 11454
		While,
		// Token: 0x04002CBF RID: 11455
		Op_Equal,
		// Token: 0x04002CC0 RID: 11456
		Op_Assignment,
		// Token: 0x04002CC1 RID: 11457
		Op_LessThan,
		// Token: 0x04002CC2 RID: 11458
		Op_LessThanEqual,
		// Token: 0x04002CC3 RID: 11459
		Op_GreaterThanEqual,
		// Token: 0x04002CC4 RID: 11460
		Op_GreaterThan,
		// Token: 0x04002CC5 RID: 11461
		Op_NotEqual,
		// Token: 0x04002CC6 RID: 11462
		Op_Concat,
		// Token: 0x04002CC7 RID: 11463
		VarArgs,
		// Token: 0x04002CC8 RID: 11464
		Dot,
		// Token: 0x04002CC9 RID: 11465
		Colon,
		// Token: 0x04002CCA RID: 11466
		DoubleColon,
		// Token: 0x04002CCB RID: 11467
		Comma,
		// Token: 0x04002CCC RID: 11468
		Brk_Close_Curly,
		// Token: 0x04002CCD RID: 11469
		Brk_Open_Curly,
		// Token: 0x04002CCE RID: 11470
		Brk_Close_Round,
		// Token: 0x04002CCF RID: 11471
		Brk_Open_Round,
		// Token: 0x04002CD0 RID: 11472
		Brk_Close_Square,
		// Token: 0x04002CD1 RID: 11473
		Brk_Open_Square,
		// Token: 0x04002CD2 RID: 11474
		Op_Len,
		// Token: 0x04002CD3 RID: 11475
		Op_Pwr,
		// Token: 0x04002CD4 RID: 11476
		Op_Mod,
		// Token: 0x04002CD5 RID: 11477
		Op_Div,
		// Token: 0x04002CD6 RID: 11478
		Op_Mul,
		// Token: 0x04002CD7 RID: 11479
		Op_MinusOrSub,
		// Token: 0x04002CD8 RID: 11480
		Op_Add,
		// Token: 0x04002CD9 RID: 11481
		Comment,
		// Token: 0x04002CDA RID: 11482
		String,
		// Token: 0x04002CDB RID: 11483
		String_Long,
		// Token: 0x04002CDC RID: 11484
		Number,
		// Token: 0x04002CDD RID: 11485
		Number_HexFloat,
		// Token: 0x04002CDE RID: 11486
		Number_Hex,
		// Token: 0x04002CDF RID: 11487
		SemiColon,
		// Token: 0x04002CE0 RID: 11488
		Invalid,
		// Token: 0x04002CE1 RID: 11489
		Brk_Open_Curly_Shared,
		// Token: 0x04002CE2 RID: 11490
		Op_Dollar
	}
}
