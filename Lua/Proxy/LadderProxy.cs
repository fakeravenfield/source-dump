using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009CF RID: 2511
	[Proxy(typeof(Ladder))]
	public class LadderProxy : IProxy
	{
		// Token: 0x06004627 RID: 17959 RVA: 0x00030E1C File Offset: 0x0002F01C
		[MoonSharpHidden]
		public LadderProxy(Ladder value)
		{
			this._value = value;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x00030E2B File Offset: 0x0002F02B
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x00030E3D File Offset: 0x0002F03D
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x00030E4F File Offset: 0x0002F04F
		public Vector3Proxy bottomExitPosition
		{
			get
			{
				return Vector3Proxy.New(WLadder.GetBottomExitPosition(this._value));
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x00030E61 File Offset: 0x0002F061
		public Vector3Proxy bottomPosition
		{
			get
			{
				return Vector3Proxy.New(WLadder.GetBottomPosition(this._value));
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x00030E73 File Offset: 0x0002F073
		public float height
		{
			get
			{
				return WLadder.GetHeight(this._value);
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x00030E80 File Offset: 0x0002F080
		public Vector3Proxy topExitPosition
		{
			get
			{
				return Vector3Proxy.New(WLadder.GetTopExitPosition(this._value));
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x00030E92 File Offset: 0x0002F092
		public Vector3Proxy topPosition
		{
			get
			{
				return Vector3Proxy.New(WLadder.GetTopPosition(this._value));
			}
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x00030EA4 File Offset: 0x0002F0A4
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x00130D98 File Offset: 0x0012EF98
		[MoonSharpHidden]
		public static LadderProxy New(Ladder value)
		{
			if (value == null)
			{
				return null;
			}
			LadderProxy ladderProxy = (LadderProxy)ObjectCache.Get(typeof(LadderProxy), value);
			if (ladderProxy == null)
			{
				ladderProxy = new LadderProxy(value);
				ObjectCache.Add(typeof(LadderProxy), value, ladderProxy);
			}
			return ladderProxy;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x00030EAC File Offset: 0x0002F0AC
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003167 RID: 12647
		[MoonSharpHidden]
		public Ladder _value;
	}
}
