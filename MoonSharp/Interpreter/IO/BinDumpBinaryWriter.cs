using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x0200089D RID: 2205
	public class BinDumpBinaryWriter : BinaryWriter
	{
		// Token: 0x0600373E RID: 14142 RVA: 0x00025485 File Offset: 0x00023685
		public BinDumpBinaryWriter(Stream s) : base(s)
		{
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x00025499 File Offset: 0x00023699
		public BinDumpBinaryWriter(Stream s, Encoding e) : base(s, e)
		{
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x00120A34 File Offset: 0x0011EC34
		public override void Write(uint value)
		{
			byte b = (byte)value;
			if ((uint)b == value && b != 127 && b != 126)
			{
				base.Write(b);
				return;
			}
			ushort num = (ushort)value;
			if ((uint)num == value)
			{
				base.Write(127);
				base.Write(num);
				return;
			}
			base.Write(126);
			base.Write(value);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x00120A80 File Offset: 0x0011EC80
		public override void Write(int value)
		{
			sbyte b = (sbyte)value;
			if ((int)b == value && b != 127 && b != 126)
			{
				base.Write(b);
				return;
			}
			short num = (short)value;
			if ((int)num == value)
			{
				base.Write(sbyte.MaxValue);
				base.Write(num);
				return;
			}
			base.Write(126);
			base.Write(value);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x00120ACC File Offset: 0x0011ECCC
		public override void Write(string value)
		{
			int count;
			if (this.m_StringMap.TryGetValue(value, out count))
			{
				this.Write(this.m_StringMap[value]);
				return;
			}
			count = this.m_StringMap.Count;
			this.m_StringMap[value] = count;
			this.Write(count);
			base.Write(value);
		}

		// Token: 0x04002EDC RID: 11996
		private Dictionary<string, int> m_StringMap = new Dictionary<string, int>();
	}
}
