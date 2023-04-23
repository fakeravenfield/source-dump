using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000782 RID: 1922
	public class Thread
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x0002090D File Offset: 0x0001EB0D
		// (set) Token: 0x06002F5C RID: 12124 RVA: 0x00020915 File Offset: 0x0001EB15
		public int id { get; private set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x0002091E File Offset: 0x0001EB1E
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x00020926 File Offset: 0x0001EB26
		public string name { get; private set; }

		// Token: 0x06002F5F RID: 12127 RVA: 0x0002092F File Offset: 0x0001EB2F
		public Thread(int id, string name)
		{
			this.id = id;
			if (name == null || name.Length == 0)
			{
				this.name = string.Format("Thread #{0}", id);
				return;
			}
			this.name = name;
		}
	}
}
