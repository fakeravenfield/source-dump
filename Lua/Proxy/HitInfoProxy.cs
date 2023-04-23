using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009CB RID: 2507
	[Proxy(typeof(HitInfo))]
	public class HitInfoProxy : IProxy
	{
		// Token: 0x0600454C RID: 17740 RVA: 0x00030346 File Offset: 0x0002E546
		[MoonSharpHidden]
		public HitInfoProxy(HitInfo value)
		{
			this._value = value;
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x00130AA8 File Offset: 0x0012ECA8
		public HitInfoProxy(ActorProxy actor)
		{
			Actor actor2 = null;
			if (actor != null)
			{
				actor2 = actor._value;
			}
			this._value = new HitInfo(actor2);
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00130AD4 File Offset: 0x0012ECD4
		public HitInfoProxy(VehicleProxy vehicle)
		{
			Vehicle vehicle2 = null;
			if (vehicle != null)
			{
				vehicle2 = vehicle._value;
			}
			this._value = new HitInfo(vehicle2);
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x00030355 File Offset: 0x0002E555
		public HitInfoProxy()
		{
			this._value = default(HitInfo);
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x00030369 File Offset: 0x0002E569
		// (set) Token: 0x06004551 RID: 17745 RVA: 0x00130B00 File Offset: 0x0012ED00
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

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06004552 RID: 17746 RVA: 0x0003037B File Offset: 0x0002E57B
		public MonoBehaviourProxy destructible
		{
			get
			{
				return MonoBehaviourProxy.New(this._value.destructible);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x0003038D File Offset: 0x0002E58D
		// (set) Token: 0x06004554 RID: 17748 RVA: 0x00130B28 File Offset: 0x0012ED28
		public VehicleProxy vehicle
		{
			get
			{
				return VehicleProxy.New(this._value.vehicle);
			}
			set
			{
				Vehicle vehicle = null;
				if (value != null)
				{
					vehicle = value._value;
				}
				this._value.vehicle = vehicle;
			}
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x0003039F File Offset: 0x0002E59F
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x000303AC File Offset: 0x0002E5AC
		[MoonSharpHidden]
		public static HitInfoProxy New(HitInfo value)
		{
			return new HitInfoProxy(value);
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x000303B4 File Offset: 0x0002E5B4
		[MoonSharpUserDataMetamethod("__call")]
		public static HitInfoProxy Call(DynValue _, ActorProxy actor)
		{
			return new HitInfoProxy(actor);
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x000303BC File Offset: 0x0002E5BC
		[MoonSharpUserDataMetamethod("__call")]
		public static HitInfoProxy Call(DynValue _, VehicleProxy vehicle)
		{
			return new HitInfoProxy(vehicle);
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000303C4 File Offset: 0x0002E5C4
		[MoonSharpUserDataMetamethod("__call")]
		public static HitInfoProxy Call(DynValue _)
		{
			return new HitInfoProxy();
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x000303CB File Offset: 0x0002E5CB
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003163 RID: 12643
		[MoonSharpHidden]
		public HitInfo _value;
	}
}
