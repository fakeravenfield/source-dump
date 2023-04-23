using System;

namespace MapMagic
{
	// Token: 0x02000569 RID: 1385
	public class GeneratorMenuAttribute : Attribute
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x00018C46 File Offset: 0x00016E46
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x00018C4E File Offset: 0x00016E4E
		public string menu { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x00018C57 File Offset: 0x00016E57
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x00018C5F File Offset: 0x00016E5F
		public string name { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00018C68 File Offset: 0x00016E68
		// (set) Token: 0x06002363 RID: 9059 RVA: 0x00018C70 File Offset: 0x00016E70
		public bool disengageable { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x00018C79 File Offset: 0x00016E79
		// (set) Token: 0x06002365 RID: 9061 RVA: 0x00018C81 File Offset: 0x00016E81
		public bool disabled { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x00018C8A File Offset: 0x00016E8A
		// (set) Token: 0x06002367 RID: 9063 RVA: 0x00018C92 File Offset: 0x00016E92
		public int priority { get; set; }
	}
}
