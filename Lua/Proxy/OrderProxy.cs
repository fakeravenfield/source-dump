using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009E0 RID: 2528
	[Proxy(typeof(Order))]
	public class OrderProxy : IProxy
	{
		// Token: 0x06004A31 RID: 18993 RVA: 0x000346EE File Offset: 0x000328EE
		[MoonSharpHidden]
		public OrderProxy(Order value)
		{
			this._value = value;
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x000346FD File Offset: 0x000328FD
		// (set) Token: 0x06004A33 RID: 18995 RVA: 0x0003470A File Offset: 0x0003290A
		public int basePriority
		{
			get
			{
				return WOrder.GetBasePriority(this._value);
			}
			set
			{
				WOrder.SetBasePriority(this._value, value);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x00034718 File Offset: 0x00032918
		public bool hasOverrideTargetPosition
		{
			get
			{
				return WOrder.GetHasOverrideTargetPosition(this._value);
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06004A35 RID: 18997 RVA: 0x00034725 File Offset: 0x00032925
		public bool isIssuedByPlayer
		{
			get
			{
				return WOrder.GetIsIssuedByPlayer(this._value);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x00034732 File Offset: 0x00032932
		public int priority
		{
			get
			{
				return WOrder.GetPriority(this._value);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x0003473F File Offset: 0x0003293F
		public SpawnPointProxy sourcePoint
		{
			get
			{
				return SpawnPointProxy.New(WOrder.GetSourcePoint(this._value));
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06004A38 RID: 19000 RVA: 0x00034751 File Offset: 0x00032951
		public SpawnPointProxy targetPoint
		{
			get
			{
				return SpawnPointProxy.New(WOrder.GetTargetPoint(this._value));
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00034763 File Offset: 0x00032963
		public Order.OrderType type
		{
			get
			{
				return WOrder.GetType(this._value);
			}
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x00034770 File Offset: 0x00032970
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x00131A74 File Offset: 0x0012FC74
		[MoonSharpHidden]
		public static OrderProxy New(Order value)
		{
			if (value == null)
			{
				return null;
			}
			OrderProxy orderProxy = (OrderProxy)ObjectCache.Get(typeof(OrderProxy), value);
			if (orderProxy == null)
			{
				orderProxy = new OrderProxy(value);
				ObjectCache.Add(typeof(OrderProxy), value, orderProxy);
			}
			return orderProxy;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x00131AB8 File Offset: 0x0012FCB8
		public static OrderProxy Create(Order.OrderType type, SpawnPointProxy source, SpawnPointProxy target)
		{
			SpawnPoint source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			SpawnPoint target2 = null;
			if (target != null)
			{
				target2 = target._value;
			}
			return OrderProxy.New(WOrder.Create(type, source2, target2));
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00131AEC File Offset: 0x0012FCEC
		public static OrderProxy Create(Order.OrderType type, SpawnPointProxy source, SpawnPointProxy target, Vector3Proxy overridePosition)
		{
			SpawnPoint source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			SpawnPoint target2 = null;
			if (target != null)
			{
				target2 = target._value;
			}
			if (overridePosition == null)
			{
				throw new ScriptRuntimeException("argument 'overridePosition' is nil");
			}
			return OrderProxy.New(WOrder.Create(type, source2, target2, overridePosition._value));
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x00034778 File Offset: 0x00032978
		public static OrderProxy CreateMoveOrder(Vector3Proxy targetPosition)
		{
			if (targetPosition == null)
			{
				throw new ScriptRuntimeException("argument 'targetPosition' is nil");
			}
			return OrderProxy.New(WOrder.CreateMoveOrder(targetPosition._value));
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00034798 File Offset: 0x00032998
		public void DropOverrideTargetPosition()
		{
			WOrder.DropOverrideTargetPosition(this._value);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x000347A5 File Offset: 0x000329A5
		public Vector3Proxy GetOverrideTargetPosition()
		{
			return Vector3Proxy.New(WOrder.GetOverrideTargetPosition(this._value));
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x000347B7 File Offset: 0x000329B7
		public void SetOverrideTargetPosition(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			WOrder.SetOverrideTargetPosition(this._value, position._value);
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x000347D8 File Offset: 0x000329D8
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003178 RID: 12664
		[MoonSharpHidden]
		public Order _value;
	}
}
