using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A20 RID: 2592
	[Proxy(typeof(WPathfindingNode))]
	public class WPathfindingNodeProxy : IProxy
	{
		// Token: 0x06005282 RID: 21122 RVA: 0x0003CD7E File Offset: 0x0003AF7E
		[MoonSharpHidden]
		public WPathfindingNodeProxy(WPathfindingNode value)
		{
			this._value = value;
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x0003CD8D File Offset: 0x0003AF8D
		public Vector3Proxy position
		{
			get
			{
				return Vector3Proxy.New(this._value.position);
			}
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0003CD9F File Offset: 0x0003AF9F
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0013930C File Offset: 0x0013750C
		[MoonSharpHidden]
		public static WPathfindingNodeProxy New(WPathfindingNode value)
		{
			if (value == null)
			{
				return null;
			}
			WPathfindingNodeProxy wpathfindingNodeProxy = (WPathfindingNodeProxy)ObjectCache.Get(typeof(WPathfindingNodeProxy), value);
			if (wpathfindingNodeProxy == null)
			{
				wpathfindingNodeProxy = new WPathfindingNodeProxy(value);
				ObjectCache.Add(typeof(WPathfindingNodeProxy), value, wpathfindingNodeProxy);
			}
			return wpathfindingNodeProxy;
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x0003CDA7 File Offset: 0x0003AFA7
		public Vector3Proxy RandomPointOnSurface()
		{
			return Vector3Proxy.New(this._value.RandomPointOnSurface());
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0003CDB9 File Offset: 0x0003AFB9
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A6 RID: 12966
		[MoonSharpHidden]
		public WPathfindingNode _value;
	}
}
