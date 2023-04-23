using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000798 RID: 1944
	public class Event : ProtocolMessage
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x00020D9C File Offset: 0x0001EF9C
		// (set) Token: 0x06002FBB RID: 12219 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public string @event { get; private set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x00020DAD File Offset: 0x0001EFAD
		// (set) Token: 0x06002FBD RID: 12221 RVA: 0x00020DB5 File Offset: 0x0001EFB5
		public object body { get; private set; }

		// Token: 0x06002FBE RID: 12222 RVA: 0x00020DBE File Offset: 0x0001EFBE
		public Event(string type, object bdy = null) : base("event")
		{
			this.@event = type;
			this.body = bdy;
		}
	}
}
