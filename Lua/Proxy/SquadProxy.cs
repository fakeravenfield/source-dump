using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A06 RID: 2566
	[Proxy(typeof(Squad))]
	public class SquadProxy : IProxy
	{
		// Token: 0x06004F84 RID: 20356 RVA: 0x00039BFB File Offset: 0x00037DFB
		[MoonSharpHidden]
		public SquadProxy(Squad value)
		{
			this._value = value;
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x00039C0A File Offset: 0x00037E0A
		// (set) Token: 0x06004F86 RID: 20358 RVA: 0x00138420 File Offset: 0x00136620
		public ActorProxy attackTarget
		{
			get
			{
				return ActorProxy.New(WSquad.GetAttackTarget(this._value));
			}
			set
			{
				Actor attackTarget = null;
				if (value != null)
				{
					attackTarget = value._value;
				}
				WSquad.SetAttackTarget(this._value, attackTarget);
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x00039C1C File Offset: 0x00037E1C
		public bool hasPlayerLeader
		{
			get
			{
				return WSquad.GetHasPlayerLeader(this._value);
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x00039C29 File Offset: 0x00037E29
		public ActorProxy leader
		{
			get
			{
				return ActorProxy.New(WSquad.GetLeader(this._value));
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x00039C3B File Offset: 0x00037E3B
		public Actor[] members
		{
			get
			{
				return WSquad.GetMembers(this._value);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x00039C48 File Offset: 0x00037E48
		public OrderProxy order
		{
			get
			{
				return OrderProxy.New(WSquad.GetOrder(this._value));
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06004F8B RID: 20363 RVA: 0x00039C5A File Offset: 0x00037E5A
		public VehicleProxy squadVehicle
		{
			get
			{
				return VehicleProxy.New(WSquad.GetSquadVehicle(this._value));
			}
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00039C6C File Offset: 0x00037E6C
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x00138448 File Offset: 0x00136648
		[MoonSharpHidden]
		public static SquadProxy New(Squad value)
		{
			if (value == null)
			{
				return null;
			}
			SquadProxy squadProxy = (SquadProxy)ObjectCache.Get(typeof(SquadProxy), value);
			if (squadProxy == null)
			{
				squadProxy = new SquadProxy(value);
				ObjectCache.Add(typeof(SquadProxy), value, squadProxy);
			}
			return squadProxy;
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x0013848C File Offset: 0x0013668C
		public void AddMember(ActorProxy newMember)
		{
			Actor newMember2 = null;
			if (newMember != null)
			{
				newMember2 = newMember._value;
			}
			WSquad.AddMember(this._value, newMember2);
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x001384B4 File Offset: 0x001366B4
		public void AssignOrder(OrderProxy order)
		{
			Order order2 = null;
			if (order != null)
			{
				order2 = order._value;
			}
			WSquad.AssignOrder(this._value, order2);
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x00039C74 File Offset: 0x00037E74
		public static SquadProxy Create(List<Actor> actors)
		{
			return SquadProxy.New(WSquad.Create(actors));
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x001384DC File Offset: 0x001366DC
		public void RemoveMember(ActorProxy member)
		{
			Actor member2 = null;
			if (member != null)
			{
				member2 = member._value;
			}
			WSquad.RemoveMember(this._value, member2);
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x00039C81 File Offset: 0x00037E81
		public void SetCustomFormation(Vector2[] formation)
		{
			WSquad.SetCustomFormation(this._value, formation);
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x00039C8F File Offset: 0x00037E8F
		public void SetFormation(Squad.FormationType formation)
		{
			WSquad.SetFormation(this._value, formation);
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x00039C9D File Offset: 0x00037E9D
		public void SetFormationSize(float formationWidth, float formationDepth)
		{
			WSquad.SetFormationSize(this._value, formationWidth, formationDepth);
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x00039CAC File Offset: 0x00037EAC
		public void SetRandomFormation()
		{
			WSquad.SetRandomFormation(this._value);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00039CB9 File Offset: 0x00037EB9
		public SquadProxy SplitSquad(int count)
		{
			return SquadProxy.New(WSquad.SplitSquad(this._value, count));
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00039CCC File Offset: 0x00037ECC
		public SquadProxy SplitSquad(Actor[] membersToDrop)
		{
			return SquadProxy.New(WSquad.SplitSquad(this._value, membersToDrop));
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00039CDF File Offset: 0x00037EDF
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003295 RID: 12949
		[MoonSharpHidden]
		public Squad _value;
	}
}
