using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A22 RID: 2594
	[Proxy(typeof(WPhysics))]
	public class WPhysicsProxy : IProxy
	{
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x0003CE42 File Offset: 0x0003B042
		// (set) Token: 0x0600528F RID: 21135 RVA: 0x0003CE4E File Offset: 0x0003B04E
		public static Vector3Proxy gravity
		{
			get
			{
				return Vector3Proxy.New(WPhysics.gravity);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				WPhysics.gravity = value._value;
			}
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00139350 File Offset: 0x00137550
		public static bool CheckBox(Vector3Proxy center, Vector3Proxy halfExtents, QuaternionProxy orientation, WRaycastTarget target)
		{
			if (center == null)
			{
				throw new ScriptRuntimeException("argument 'center' is nil");
			}
			if (halfExtents == null)
			{
				throw new ScriptRuntimeException("argument 'halfExtents' is nil");
			}
			if (orientation == null)
			{
				throw new ScriptRuntimeException("argument 'orientation' is nil");
			}
			return WPhysics.CheckBox(center._value, halfExtents._value, orientation._value, target);
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0003CE69 File Offset: 0x0003B069
		public static bool CheckCapsule(Vector3Proxy start, Vector3Proxy end, float radius, WRaycastTarget target)
		{
			if (start == null)
			{
				throw new ScriptRuntimeException("argument 'start' is nil");
			}
			if (end == null)
			{
				throw new ScriptRuntimeException("argument 'end' is nil");
			}
			return WPhysics.CheckCapsule(start._value, end._value, radius, target);
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x0003CE9A File Offset: 0x0003B09A
		public static bool CheckSphere(Vector3Proxy position, float radius, WRaycastTarget target)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WPhysics.CheckSphere(position._value, radius, target);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0003CEB7 File Offset: 0x0003B0B7
		public static object Linecast(Vector3Proxy start, Vector3Proxy end, WRaycastTarget target)
		{
			if (start == null)
			{
				throw new ScriptRuntimeException("argument 'start' is nil");
			}
			if (end == null)
			{
				throw new ScriptRuntimeException("argument 'end' is nil");
			}
			return WPhysics.Linecast(start._value, end._value, target);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x001393A0 File Offset: 0x001375A0
		public static Collider[] OverlapBox(Vector3Proxy center, Vector3Proxy halfExtents, QuaternionProxy orientation, WRaycastTarget target)
		{
			if (center == null)
			{
				throw new ScriptRuntimeException("argument 'center' is nil");
			}
			if (halfExtents == null)
			{
				throw new ScriptRuntimeException("argument 'halfExtents' is nil");
			}
			if (orientation == null)
			{
				throw new ScriptRuntimeException("argument 'orientation' is nil");
			}
			return WPhysics.OverlapBox(center._value, halfExtents._value, orientation._value, target);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0003CEE7 File Offset: 0x0003B0E7
		public static Collider[] OverlapCapsule(Vector3Proxy start, Vector3Proxy end, float radius, WRaycastTarget target)
		{
			if (start == null)
			{
				throw new ScriptRuntimeException("argument 'start' is nil");
			}
			if (end == null)
			{
				throw new ScriptRuntimeException("argument 'end' is nil");
			}
			return WPhysics.OverlapCapsule(start._value, end._value, radius, target);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0003CF18 File Offset: 0x0003B118
		public static Collider[] OverlapSphere(Vector3Proxy position, float radius, WRaycastTarget target)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WPhysics.OverlapSphere(position._value, radius, target);
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0003CF35 File Offset: 0x0003B135
		public static object Raycast(RayProxy ray, float range, WRaycastTarget target)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			return WPhysics.Raycast(ray._value, range, target);
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0003CF52 File Offset: 0x0003B152
		public static RaycastHit[] RaycastAll(RayProxy ray, float range, WRaycastTarget target)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			return WPhysics.RaycastAll(ray._value, range, target);
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x0003CF6F File Offset: 0x0003B16F
		public static object Spherecast(RayProxy ray, float radius, float range, WRaycastTarget target)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			return WPhysics.Spherecast(ray._value, radius, range, target);
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0003CF8D File Offset: 0x0003B18D
		public static object SpherecastAll(RayProxy ray, float radius, float range, WRaycastTarget target)
		{
			if (ray == null)
			{
				throw new ScriptRuntimeException("argument 'ray' is nil");
			}
			return WPhysics.SpherecastAll(ray._value, radius, range, target);
		}
	}
}
