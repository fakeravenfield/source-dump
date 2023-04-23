using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009FB RID: 2555
	[Proxy(typeof(SgmActorDiedArgs))]
	public class SgmActorDiedArgsProxy : IProxy
	{
		// Token: 0x06004E86 RID: 20102 RVA: 0x00038EAF File Offset: 0x000370AF
		[MoonSharpHidden]
		public SgmActorDiedArgsProxy(SgmActorDiedArgs value)
		{
			this._value = value;
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x00038EBE File Offset: 0x000370BE
		// (set) Token: 0x06004E88 RID: 20104 RVA: 0x00137ED4 File Offset: 0x001360D4
		public ActorProxy actor
		{
			get
			{
				return ActorProxy.New(this._value.actor);
			}
			set
			{
				Actor actor = null;
				if (value != null)
				{
					actor = value._value;
				}
				this._value.actor = actor;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x00038ED0 File Offset: 0x000370D0
		// (set) Token: 0x06004E8A RID: 20106 RVA: 0x00038EE2 File Offset: 0x000370E2
		public Vector3Proxy position
		{
			get
			{
				return Vector3Proxy.New(this._value.position);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.position = value._value;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x00038F03 File Offset: 0x00037103
		// (set) Token: 0x06004E8C RID: 20108 RVA: 0x00038F10 File Offset: 0x00037110
		public bool silentKill
		{
			get
			{
				return this._value.silentKill;
			}
			set
			{
				this._value.silentKill = value;
			}
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00038F1E File Offset: 0x0003711E
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00137EFC File Offset: 0x001360FC
		[MoonSharpHidden]
		public static SgmActorDiedArgsProxy New(SgmActorDiedArgs value)
		{
			if (value == null)
			{
				return null;
			}
			SgmActorDiedArgsProxy sgmActorDiedArgsProxy = (SgmActorDiedArgsProxy)ObjectCache.Get(typeof(SgmActorDiedArgsProxy), value);
			if (sgmActorDiedArgsProxy == null)
			{
				sgmActorDiedArgsProxy = new SgmActorDiedArgsProxy(value);
				ObjectCache.Add(typeof(SgmActorDiedArgsProxy), value, sgmActorDiedArgsProxy);
			}
			return sgmActorDiedArgsProxy;
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00038F26 File Offset: 0x00037126
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400328A RID: 12938
		[MoonSharpHidden]
		public SgmActorDiedArgs _value;
	}
}
