using System;

namespace MoonSharp.Interpreter.CoreLib.StringLib
{
	// Token: 0x020008F2 RID: 2290
	internal class StringRange
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x00027557 File Offset: 0x00025757
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x0002755F File Offset: 0x0002575F
		public int Start { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x00027568 File Offset: 0x00025768
		// (set) Token: 0x06003A4C RID: 14924 RVA: 0x00027570 File Offset: 0x00025770
		public int End { get; set; }

		// Token: 0x06003A4D RID: 14925 RVA: 0x00027579 File Offset: 0x00025779
		public StringRange()
		{
			this.Start = 0;
			this.End = 0;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x0002758F File Offset: 0x0002578F
		public StringRange(int start, int end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x0012B658 File Offset: 0x00129858
		public static StringRange FromLuaRange(DynValue start, DynValue end, int? defaultEnd = null)
		{
			int num = start.IsNil() ? 1 : ((int)start.Number);
			int end2 = end.IsNil() ? (defaultEnd ?? num) : ((int)end.Number);
			return new StringRange(num, end2);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x0012B6A8 File Offset: 0x001298A8
		public string ApplyToString(string value)
		{
			int num = (this.Start < 0) ? (this.Start + value.Length + 1) : this.Start;
			int num2 = (this.End < 0) ? (this.End + value.Length + 1) : this.End;
			if (num < 1)
			{
				num = 1;
			}
			if (num2 > value.Length)
			{
				num2 = value.Length;
			}
			if (num > num2)
			{
				return string.Empty;
			}
			return value.Substring(num - 1, num2 - num + 1);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000275A5 File Offset: 0x000257A5
		public int Length()
		{
			return this.End - this.Start + 1;
		}
	}
}
