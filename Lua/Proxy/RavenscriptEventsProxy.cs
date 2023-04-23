using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009E7 RID: 2535
	[Proxy(typeof(RavenscriptEvents))]
	public class RavenscriptEventsProxy : IProxy
	{
		// Token: 0x06004B04 RID: 19204 RVA: 0x000355E0 File Offset: 0x000337E0
		[MoonSharpHidden]
		public RavenscriptEventsProxy(RavenscriptEvents value)
		{
			this._value = value;
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x000355EF File Offset: 0x000337EF
		public RavenscriptEventsProxy()
		{
			this._value = new RavenscriptEvents();
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x00035602 File Offset: 0x00033802
		public ScriptEventProxy onActorCreated
		{
			get
			{
				return ScriptEventProxy.New(this._value.onActorCreated);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06004B07 RID: 19207 RVA: 0x00035614 File Offset: 0x00033814
		public ScriptEventProxy onActorDied
		{
			get
			{
				return ScriptEventProxy.New(this._value.onActorDied);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06004B08 RID: 19208 RVA: 0x00035626 File Offset: 0x00033826
		public ScriptEventProxy onActorDiedInfo
		{
			get
			{
				return ScriptEventProxy.New(this._value.onActorDiedInfo);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x00035638 File Offset: 0x00033838
		public ScriptEventProxy onActorSelectedLoadout
		{
			get
			{
				return ScriptEventProxy.New(this._value.onActorSelectedLoadout);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06004B0A RID: 19210 RVA: 0x0003564A File Offset: 0x0003384A
		public ScriptEventProxy onActorSpawn
		{
			get
			{
				return ScriptEventProxy.New(this._value.onActorSpawn);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004B0B RID: 19211 RVA: 0x0003565C File Offset: 0x0003385C
		public ScriptEventProxy onCapturePointCaptured
		{
			get
			{
				return ScriptEventProxy.New(this._value.onCapturePointCaptured);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x0003566E File Offset: 0x0003386E
		public ScriptEventProxy onCapturePointNeutralized
		{
			get
			{
				return ScriptEventProxy.New(this._value.onCapturePointNeutralized);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06004B0D RID: 19213 RVA: 0x00035680 File Offset: 0x00033880
		public ScriptEventProxy onExplosion
		{
			get
			{
				return ScriptEventProxy.New(this._value.onExplosion);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06004B0E RID: 19214 RVA: 0x00035692 File Offset: 0x00033892
		public ScriptEventProxy onExplosionInfo
		{
			get
			{
				return ScriptEventProxy.New(this._value.onExplosionInfo);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x000356A4 File Offset: 0x000338A4
		public ScriptEventProxy onMatchEnd
		{
			get
			{
				return ScriptEventProxy.New(this._value.onMatchEnd);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06004B10 RID: 19216 RVA: 0x000356B6 File Offset: 0x000338B6
		public ScriptEventProxy onPlayerDealtDamage
		{
			get
			{
				return ScriptEventProxy.New(this._value.onPlayerDealtDamage);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x000356C8 File Offset: 0x000338C8
		public ScriptEventProxy onProjectileSpawned
		{
			get
			{
				return ScriptEventProxy.New(this._value.onProjectileSpawned);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06004B12 RID: 19218 RVA: 0x000356DA File Offset: 0x000338DA
		public ScriptEventProxy onSquadAssignedNewOrder
		{
			get
			{
				return ScriptEventProxy.New(this._value.onSquadAssignedNewOrder);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06004B13 RID: 19219 RVA: 0x000356EC File Offset: 0x000338EC
		public ScriptEventProxy onSquadCreated
		{
			get
			{
				return ScriptEventProxy.New(this._value.onSquadCreated);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x000356FE File Offset: 0x000338FE
		public ScriptEventProxy onSquadFailedToAssignNewOrder
		{
			get
			{
				return ScriptEventProxy.New(this._value.onSquadFailedToAssignNewOrder);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x00035710 File Offset: 0x00033910
		public ScriptEventProxy onSquadRequestNewOrder
		{
			get
			{
				return ScriptEventProxy.New(this._value.onSquadRequestNewOrder);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x00035722 File Offset: 0x00033922
		public ScriptEventProxy onTurretActivated
		{
			get
			{
				return ScriptEventProxy.New(this._value.onTurretActivated);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x00035734 File Offset: 0x00033934
		public ScriptEventProxy onVehicleDestroyed
		{
			get
			{
				return ScriptEventProxy.New(this._value.onVehicleDestroyed);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x00035746 File Offset: 0x00033946
		public ScriptEventProxy onVehicleDisabled
		{
			get
			{
				return ScriptEventProxy.New(this._value.onVehicleDisabled);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x00035758 File Offset: 0x00033958
		public ScriptEventProxy onVehicleExtinguished
		{
			get
			{
				return ScriptEventProxy.New(this._value.onVehicleExtinguished);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x0003576A File Offset: 0x0003396A
		public ScriptEventProxy onVehicleSpawn
		{
			get
			{
				return ScriptEventProxy.New(this._value.onVehicleSpawn);
			}
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0003577C File Offset: 0x0003397C
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x00131D18 File Offset: 0x0012FF18
		[MoonSharpHidden]
		public static RavenscriptEventsProxy New(RavenscriptEvents value)
		{
			if (value == null)
			{
				return null;
			}
			RavenscriptEventsProxy ravenscriptEventsProxy = (RavenscriptEventsProxy)ObjectCache.Get(typeof(RavenscriptEventsProxy), value);
			if (ravenscriptEventsProxy == null)
			{
				ravenscriptEventsProxy = new RavenscriptEventsProxy(value);
				ObjectCache.Add(typeof(RavenscriptEventsProxy), value, ravenscriptEventsProxy);
			}
			return ravenscriptEventsProxy;
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x00035784 File Offset: 0x00033984
		[MoonSharpUserDataMetamethod("__call")]
		public static RavenscriptEventsProxy Call(DynValue _)
		{
			return new RavenscriptEventsProxy();
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x0003578B File Offset: 0x0003398B
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400317E RID: 12670
		[MoonSharpHidden]
		public RavenscriptEvents _value;
	}
}
