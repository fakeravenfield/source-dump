using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008C8 RID: 2248
	public class SourceCode : IScriptPrivateResource
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x00026533 File Offset: 0x00024733
		// (set) Token: 0x060038A0 RID: 14496 RVA: 0x0002653B File Offset: 0x0002473B
		public string Name { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x00026544 File Offset: 0x00024744
		// (set) Token: 0x060038A2 RID: 14498 RVA: 0x0002654C File Offset: 0x0002474C
		public string Code { get; private set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x00026555 File Offset: 0x00024755
		// (set) Token: 0x060038A4 RID: 14500 RVA: 0x0002655D File Offset: 0x0002475D
		public string[] Lines { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x00026566 File Offset: 0x00024766
		// (set) Token: 0x060038A6 RID: 14502 RVA: 0x0002656E File Offset: 0x0002476E
		public Script OwnerScript { get; private set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x00026577 File Offset: 0x00024777
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x0002657F File Offset: 0x0002477F
		public int SourceID { get; private set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x00026588 File Offset: 0x00024788
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x00026590 File Offset: 0x00024790
		internal List<SourceRef> Refs { get; private set; }

		// Token: 0x060038AB RID: 14507 RVA: 0x00125AFC File Offset: 0x00123CFC
		internal SourceCode(string name, string code, int sourceID, Script ownerScript)
		{
			this.Refs = new List<SourceRef>();
			List<string> list = new List<string>();
			this.Name = name;
			this.Code = code;
			list.Add(string.Format("-- Begin of chunk : {0} ", name));
			list.AddRange(this.Code.Split(new char[]
			{
				'\n'
			}));
			this.Lines = list.ToArray();
			this.OwnerScript = ownerScript;
			this.SourceID = sourceID;
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x00125B78 File Offset: 0x00123D78
		public string GetCodeSnippet(SourceRef sourceCodeRef)
		{
			if (sourceCodeRef.FromLine == sourceCodeRef.ToLine)
			{
				int num = this.AdjustStrIndex(this.Lines[sourceCodeRef.FromLine], sourceCodeRef.FromChar);
				int num2 = this.AdjustStrIndex(this.Lines[sourceCodeRef.FromLine], sourceCodeRef.ToChar);
				return this.Lines[sourceCodeRef.FromLine].Substring(num, num2 - num);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = sourceCodeRef.FromLine; i <= sourceCodeRef.ToLine; i++)
			{
				if (i == sourceCodeRef.FromLine)
				{
					int startIndex = this.AdjustStrIndex(this.Lines[i], sourceCodeRef.FromChar);
					stringBuilder.Append(this.Lines[i].Substring(startIndex));
				}
				else if (i == sourceCodeRef.ToLine)
				{
					int num3 = this.AdjustStrIndex(this.Lines[i], sourceCodeRef.ToChar);
					stringBuilder.Append(this.Lines[i].Substring(0, num3 + 1));
				}
				else
				{
					stringBuilder.Append(this.Lines[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x00026599 File Offset: 0x00024799
		private int AdjustStrIndex(string str, int loc)
		{
			return Math.Max(Math.Min(str.Length, loc), 0);
		}
	}
}
