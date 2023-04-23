using System;
using System.Collections.Generic;
using System.IO;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007B8 RID: 1976
	public class SymbolRef
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x00021B87 File Offset: 0x0001FD87
		public SymbolRefType Type
		{
			get
			{
				return this.i_Type;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x00021B8F File Offset: 0x0001FD8F
		public int Index
		{
			get
			{
				return this.i_Index;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x00021B97 File Offset: 0x0001FD97
		public string Name
		{
			get
			{
				return this.i_Name;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x00021B9F File Offset: 0x0001FD9F
		public SymbolRef Environment
		{
			get
			{
				return this.i_Env;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x00021BA7 File Offset: 0x0001FDA7
		public static SymbolRef DefaultEnv
		{
			get
			{
				return SymbolRef.s_DefaultEnv;
			}
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x00021BAE File Offset: 0x0001FDAE
		public static SymbolRef Global(string name, SymbolRef envSymbol)
		{
			return new SymbolRef
			{
				i_Index = -1,
				i_Type = SymbolRefType.Global,
				i_Env = envSymbol,
				i_Name = name
			};
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x00021BD1 File Offset: 0x0001FDD1
		internal static SymbolRef Local(string name, int index)
		{
			return new SymbolRef
			{
				i_Index = index,
				i_Type = SymbolRefType.Local,
				i_Name = name
			};
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x00021BED File Offset: 0x0001FDED
		internal static SymbolRef Upvalue(string name, int index)
		{
			return new SymbolRef
			{
				i_Index = index,
				i_Type = SymbolRefType.Upvalue,
				i_Name = name
			};
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x0010EC9C File Offset: 0x0010CE9C
		public override string ToString()
		{
			if (this.i_Type == SymbolRefType.DefaultEnv)
			{
				return "(default _ENV)";
			}
			if (this.i_Type == SymbolRefType.Global)
			{
				return string.Format("{2} : {0} / {1}", this.i_Type, this.i_Env, this.i_Name);
			}
			return string.Format("{2} : {0}[{1}]", this.i_Type, this.i_Index, this.i_Name);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x00021C09 File Offset: 0x0001FE09
		internal void WriteBinary(BinaryWriter bw)
		{
			bw.Write((byte)this.i_Type);
			bw.Write(this.i_Index);
			bw.Write(this.i_Name);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x00021C30 File Offset: 0x0001FE30
		internal static SymbolRef ReadBinary(BinaryReader br)
		{
			return new SymbolRef
			{
				i_Type = (SymbolRefType)br.ReadByte(),
				i_Index = br.ReadInt32(),
				i_Name = br.ReadString()
			};
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x00021C5B File Offset: 0x0001FE5B
		internal void WriteBinaryEnv(BinaryWriter bw, Dictionary<SymbolRef, int> symbolMap)
		{
			if (this.i_Env != null)
			{
				bw.Write(symbolMap[this.i_Env]);
				return;
			}
			bw.Write(-1);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x0010ED0C File Offset: 0x0010CF0C
		internal void ReadBinaryEnv(BinaryReader br, SymbolRef[] symbolRefs)
		{
			int num = br.ReadInt32();
			if (num >= 0)
			{
				this.i_Env = symbolRefs[num];
			}
		}

		// Token: 0x04002C00 RID: 11264
		private static SymbolRef s_DefaultEnv = new SymbolRef
		{
			i_Type = SymbolRefType.DefaultEnv
		};

		// Token: 0x04002C01 RID: 11265
		internal SymbolRefType i_Type;

		// Token: 0x04002C02 RID: 11266
		internal SymbolRef i_Env;

		// Token: 0x04002C03 RID: 11267
		internal int i_Index;

		// Token: 0x04002C04 RID: 11268
		internal string i_Name;
	}
}
