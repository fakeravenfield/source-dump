using System;
using System.IO;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007E3 RID: 2019
	public class ScriptOptions
	{
		// Token: 0x06003257 RID: 12887 RVA: 0x0000256A File Offset: 0x0000076A
		internal ScriptOptions()
		{
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x00110B2C File Offset: 0x0010ED2C
		internal ScriptOptions(ScriptOptions defaults)
		{
			this.DebugInput = defaults.DebugInput;
			this.DebugPrint = defaults.DebugPrint;
			this.UseLuaErrorLocations = defaults.UseLuaErrorLocations;
			this.Stdin = defaults.Stdin;
			this.Stdout = defaults.Stdout;
			this.Stderr = defaults.Stderr;
			this.TailCallOptimizationThreshold = defaults.TailCallOptimizationThreshold;
			this.ScriptLoader = defaults.ScriptLoader;
			this.CheckThreadAccess = defaults.CheckThreadAccess;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x00022CA7 File Offset: 0x00020EA7
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x00022CAF File Offset: 0x00020EAF
		public IScriptLoader ScriptLoader { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x00022CB8 File Offset: 0x00020EB8
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x00022CC0 File Offset: 0x00020EC0
		public Action<string> DebugPrint { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x00022CC9 File Offset: 0x00020EC9
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x00022CD1 File Offset: 0x00020ED1
		public Func<string, string> DebugInput { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x00022CDA File Offset: 0x00020EDA
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x00022CE2 File Offset: 0x00020EE2
		public bool UseLuaErrorLocations { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x00022CEB File Offset: 0x00020EEB
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x00022CF3 File Offset: 0x00020EF3
		public ColonOperatorBehaviour ColonOperatorClrCallbackBehaviour { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x00022CFC File Offset: 0x00020EFC
		// (set) Token: 0x06003264 RID: 12900 RVA: 0x00022D04 File Offset: 0x00020F04
		public Stream Stdin { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x00022D0D File Offset: 0x00020F0D
		// (set) Token: 0x06003266 RID: 12902 RVA: 0x00022D15 File Offset: 0x00020F15
		public Stream Stdout { get; set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x00022D1E File Offset: 0x00020F1E
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x00022D26 File Offset: 0x00020F26
		public Stream Stderr { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00022D2F File Offset: 0x00020F2F
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x00022D37 File Offset: 0x00020F37
		public int TailCallOptimizationThreshold { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x00022D40 File Offset: 0x00020F40
		// (set) Token: 0x0600326C RID: 12908 RVA: 0x00022D48 File Offset: 0x00020F48
		public bool CheckThreadAccess { get; set; }
	}
}
