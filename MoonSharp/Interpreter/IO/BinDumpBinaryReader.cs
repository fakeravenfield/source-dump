using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x0200089C RID: 2204
	public class BinDumpBinaryReader : BinaryReader
	{
		// Token: 0x06003739 RID: 14137 RVA: 0x0002545C File Offset: 0x0002365C
		public BinDumpBinaryReader(Stream s) : base(s)
		{
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x00025470 File Offset: 0x00023670
		public BinDumpBinaryReader(Stream s, Encoding e) : base(s, e)
		{
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x00120978 File Offset: 0x0011EB78
		public override int ReadInt32()
		{
			sbyte b = base.ReadSByte();
			if (b == 127)
			{
				return (int)base.ReadInt16();
			}
			if (b == 126)
			{
				return base.ReadInt32();
			}
			return (int)b;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x001209A8 File Offset: 0x0011EBA8
		public override uint ReadUInt32()
		{
			byte b = base.ReadByte();
			if (b == 127)
			{
				return (uint)base.ReadUInt16();
			}
			if (b == 126)
			{
				return base.ReadUInt32();
			}
			return (uint)b;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x001209D8 File Offset: 0x0011EBD8
		public override string ReadString()
		{
			int num = this.ReadInt32();
			if (num < this.m_Strings.Count)
			{
				return this.m_Strings[num];
			}
			if (num == this.m_Strings.Count)
			{
				string text = base.ReadString();
				this.m_Strings.Add(text);
				return text;
			}
			throw new IOException("string map failure");
		}

		// Token: 0x04002EDB RID: 11995
		private List<string> m_Strings = new List<string>();
	}
}
