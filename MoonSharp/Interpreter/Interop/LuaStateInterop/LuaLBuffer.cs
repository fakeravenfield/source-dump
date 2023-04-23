using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000885 RID: 2181
	public class LuaLBuffer
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x00024EF3 File Offset: 0x000230F3
		// (set) Token: 0x0600369F RID: 13983 RVA: 0x00024EFB File Offset: 0x000230FB
		public StringBuilder StringBuilder { get; private set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x00024F04 File Offset: 0x00023104
		// (set) Token: 0x060036A1 RID: 13985 RVA: 0x00024F0C File Offset: 0x0002310C
		public LuaState LuaState { get; private set; }

		// Token: 0x060036A2 RID: 13986 RVA: 0x00024F15 File Offset: 0x00023115
		public LuaLBuffer(LuaState l)
		{
			this.StringBuilder = new StringBuilder();
			this.LuaState = l;
		}
	}
}
