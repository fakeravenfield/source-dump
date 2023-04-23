using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008AF RID: 2223
	internal class Instruction
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x00025D9E File Offset: 0x00023F9E
		internal Instruction(SourceRef sourceref)
		{
			this.SourceCodeRef = sourceref;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x0012117C File Offset: 0x0011F37C
		public override string ToString()
		{
			string text = this.OpCode.ToString().ToUpperInvariant();
			int fieldUsage = (int)this.OpCode.GetFieldUsage();
			if (fieldUsage != 0)
			{
				text += this.GenSpaces();
			}
			if (this.OpCode == OpCode.Meta || (fieldUsage & 32784) == 32784)
			{
				text = text + " " + this.NumVal.ToString("X8");
			}
			else if ((fieldUsage & 16) != 0)
			{
				text = text + " " + this.NumVal.ToString();
			}
			if ((fieldUsage & 32) != 0)
			{
				text = text + " " + this.NumVal2.ToString();
			}
			if ((fieldUsage & 4) != 0)
			{
				text = text + " " + this.Name;
			}
			if ((fieldUsage & 8) != 0)
			{
				text = text + " " + this.PurifyFromNewLines(this.Value);
			}
			if ((fieldUsage & 1) != 0)
			{
				string str = text;
				string str2 = " ";
				SymbolRef symbol = this.Symbol;
				text = str + str2 + ((symbol != null) ? symbol.ToString() : null);
			}
			if ((fieldUsage & 2) != 0 && this.SymbolList != null)
			{
				text = text + " " + string.Join(",", (from s in this.SymbolList
				select s.ToString()).ToArray<string>());
			}
			return text;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x00025DAD File Offset: 0x00023FAD
		private string PurifyFromNewLines(DynValue Value)
		{
			if (Value == null)
			{
				return "";
			}
			return Value.ToString().Replace('\n', ' ').Replace('\r', ' ');
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x00025DD0 File Offset: 0x00023FD0
		private string GenSpaces()
		{
			return new string(' ', 10 - this.OpCode.ToString().Length);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x001212D8 File Offset: 0x0011F4D8
		internal void WriteBinary(BinaryWriter wr, int baseAddress, Dictionary<SymbolRef, int> symbolMap)
		{
			wr.Write((byte)this.OpCode);
			int fieldUsage = (int)this.OpCode.GetFieldUsage();
			if ((fieldUsage & 32784) == 32784)
			{
				wr.Write(this.NumVal - baseAddress);
			}
			else if ((fieldUsage & 16) != 0)
			{
				wr.Write(this.NumVal);
			}
			if ((fieldUsage & 32) != 0)
			{
				wr.Write(this.NumVal2);
			}
			if ((fieldUsage & 4) != 0)
			{
				wr.Write(this.Name ?? "");
			}
			if ((fieldUsage & 8) != 0)
			{
				this.DumpValue(wr, this.Value);
			}
			if ((fieldUsage & 1) != 0)
			{
				Instruction.WriteSymbol(wr, this.Symbol, symbolMap);
			}
			if ((fieldUsage & 2) != 0)
			{
				wr.Write(this.SymbolList.Length);
				for (int i = 0; i < this.SymbolList.Length; i++)
				{
					Instruction.WriteSymbol(wr, this.SymbolList[i], symbolMap);
				}
			}
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x001213B4 File Offset: 0x0011F5B4
		private static void WriteSymbol(BinaryWriter wr, SymbolRef symbolRef, Dictionary<SymbolRef, int> symbolMap)
		{
			int value = (symbolRef == null) ? -1 : symbolMap[symbolRef];
			wr.Write(value);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x001213D8 File Offset: 0x0011F5D8
		private static SymbolRef ReadSymbol(BinaryReader rd, SymbolRef[] deserializedSymbols)
		{
			int num = rd.ReadInt32();
			if (num < 0)
			{
				return null;
			}
			return deserializedSymbols[num];
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x001213F8 File Offset: 0x0011F5F8
		internal static Instruction ReadBinary(SourceRef chunkRef, BinaryReader rd, int baseAddress, Table envTable, SymbolRef[] deserializedSymbols)
		{
			Instruction instruction = new Instruction(chunkRef);
			instruction.OpCode = (OpCode)rd.ReadByte();
			int fieldUsage = (int)instruction.OpCode.GetFieldUsage();
			if ((fieldUsage & 32784) == 32784)
			{
				instruction.NumVal = rd.ReadInt32() + baseAddress;
			}
			else if ((fieldUsage & 16) != 0)
			{
				instruction.NumVal = rd.ReadInt32();
			}
			if ((fieldUsage & 32) != 0)
			{
				instruction.NumVal2 = rd.ReadInt32();
			}
			if ((fieldUsage & 4) != 0)
			{
				instruction.Name = rd.ReadString();
			}
			if ((fieldUsage & 8) != 0)
			{
				instruction.Value = Instruction.ReadValue(rd, envTable);
			}
			if ((fieldUsage & 1) != 0)
			{
				instruction.Symbol = Instruction.ReadSymbol(rd, deserializedSymbols);
			}
			if ((fieldUsage & 2) != 0)
			{
				int num = rd.ReadInt32();
				instruction.SymbolList = new SymbolRef[num];
				for (int i = 0; i < instruction.SymbolList.Length; i++)
				{
					instruction.SymbolList[i] = Instruction.ReadSymbol(rd, deserializedSymbols);
				}
			}
			return instruction;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x001214D8 File Offset: 0x0011F6D8
		private static DynValue ReadValue(BinaryReader rd, Table envTable)
		{
			if (!rd.ReadBoolean())
			{
				return null;
			}
			DataType dataType = (DataType)rd.ReadByte();
			switch (dataType)
			{
			case DataType.Nil:
				return DynValue.NewNil();
			case DataType.Void:
				return DynValue.Void;
			case DataType.Boolean:
				return DynValue.NewBoolean(rd.ReadBoolean());
			case DataType.Number:
				return DynValue.NewNumber(rd.ReadDouble());
			case DataType.String:
				return DynValue.NewString(rd.ReadString());
			case DataType.Table:
				return DynValue.NewTable(envTable);
			}
			throw new NotSupportedException(string.Format("Unsupported type in chunk dump : {0}", dataType));
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0012156C File Offset: 0x0011F76C
		private void DumpValue(BinaryWriter wr, DynValue value)
		{
			if (value == null)
			{
				wr.Write(false);
				return;
			}
			wr.Write(true);
			wr.Write((byte)value.Type);
			switch (value.Type)
			{
			case DataType.Nil:
			case DataType.Void:
			case DataType.Table:
				return;
			case DataType.Boolean:
				wr.Write(value.Boolean);
				return;
			case DataType.Number:
				wr.Write(value.Number);
				return;
			case DataType.String:
				wr.Write(value.String);
				return;
			}
			throw new NotSupportedException(string.Format("Unsupported type in chunk dump : {0}", value.Type));
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x00025DF2 File Offset: 0x00023FF2
		internal void GetSymbolReferences(out SymbolRef[] symbolList, out SymbolRef symbol)
		{
			InstructionFieldUsage fieldUsage = this.OpCode.GetFieldUsage();
			symbol = null;
			symbolList = null;
			if ((fieldUsage & InstructionFieldUsage.Symbol) != InstructionFieldUsage.None)
			{
				symbol = this.Symbol;
			}
			if ((fieldUsage & InstructionFieldUsage.SymbolList) != InstructionFieldUsage.None)
			{
				symbolList = this.SymbolList;
			}
		}

		// Token: 0x04002F15 RID: 12053
		internal OpCode OpCode;

		// Token: 0x04002F16 RID: 12054
		internal SymbolRef Symbol;

		// Token: 0x04002F17 RID: 12055
		internal SymbolRef[] SymbolList;

		// Token: 0x04002F18 RID: 12056
		internal string Name;

		// Token: 0x04002F19 RID: 12057
		internal DynValue Value;

		// Token: 0x04002F1A RID: 12058
		internal int NumVal;

		// Token: 0x04002F1B RID: 12059
		internal int NumVal2;

		// Token: 0x04002F1C RID: 12060
		internal SourceRef SourceCodeRef;
	}
}
