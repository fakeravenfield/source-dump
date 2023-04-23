using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000955 RID: 2389
	[Name("Physics")]
	[Doc("Use these methods to interact with Unity's physics engine.")]
	public static class WPhysics
	{
		// Token: 0x06003C8D RID: 15501 RVA: 0x0012E784 File Offset: 0x0012C984
		[Doc("Casts a ray through the scene until it collides with a target.[..]")]
		[return: Doc("A RaycastHit if a collision occurs along the ray; otherwise nil.")]
		public static object Raycast([Doc("Test for collisions along this ray.")] Ray ray, [Doc("Look no further than this [meters].")] float range, [Doc("Test for collisions with these things.")] WRaycastTarget target)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, range, (int)target))
			{
				return raycastHit;
			}
			return null;
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00029051 File Offset: 0x00027251
		[Doc("Casts a ray through the scene registering all collisions with target objects.[..]")]
		[return: Doc("An array of RaycastHits that occured along the ray.")]
		public static RaycastHit[] RaycastAll([Doc("Test for collisions along this ray.")] Ray ray, [Doc("Look no further than this [meters].")] float range, [Doc("Test for collisions with these things.")] WRaycastTarget target)
		{
			return Physics.RaycastAll(ray, range, (int)target);
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x0012E7A8 File Offset: 0x0012C9A8
		[Doc("Casts a ray from start to end until it collides with a target.[..]")]
		[return: Doc("A RaycastHit if a collision occurs along the ray; otherwise nil.")]
		public static object Linecast(Vector3 start, Vector3 end, WRaycastTarget target)
		{
			RaycastHit raycastHit;
			if (Physics.Linecast(start, end, out raycastHit, (int)target))
			{
				return raycastHit;
			}
			return null;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x0012E7CC File Offset: 0x0012C9CC
		[Doc("Casts a sphere ray from start to end until it collides with a target.[..]")]
		[return: Doc("A RaycastHit if a collision occurs along the ray; otherwise nil.")]
		public static object Spherecast(Ray ray, float radius, float range, WRaycastTarget target)
		{
			RaycastHit raycastHit;
			if (Physics.SphereCast(ray, radius, out raycastHit, range, (int)target))
			{
				return raycastHit;
			}
			return null;
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x0002905B File Offset: 0x0002725B
		[Doc("Casts a sphere ray from start to end until it collides with a target.[..]")]
		[return: Doc("A RaycastHit if a collision occurs along the ray; otherwise nil.")]
		public static object SpherecastAll(Ray ray, float radius, float range, WRaycastTarget target)
		{
			return Physics.SphereCastAll(ray, radius, range, (int)target);
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x00029066 File Offset: 0x00027266
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x0002906D File Offset: 0x0002726D
		[Doc("The gravity applied to physics objects.[..]")]
		public static Vector3 gravity
		{
			get
			{
				return Physics.gravity;
			}
			set
			{
				Physics.gravity = value;
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x00029075 File Offset: 0x00027275
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, WRaycastTarget target)
		{
			return Physics.CheckBox(center, halfExtents, orientation, (int)target);
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x00029080 File Offset: 0x00027280
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, WRaycastTarget target)
		{
			return Physics.OverlapBox(center, halfExtents, orientation, (int)target);
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x0002908B File Offset: 0x0002728B
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, WRaycastTarget target)
		{
			return Physics.CheckCapsule(start, end, radius, (int)target);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x00029096 File Offset: 0x00027296
		public static Collider[] OverlapCapsule(Vector3 start, Vector3 end, float radius, WRaycastTarget target)
		{
			return Physics.OverlapCapsule(start, end, radius, (int)target);
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000290A1 File Offset: 0x000272A1
		public static bool CheckSphere(Vector3 position, float radius, WRaycastTarget target)
		{
			return Physics.CheckSphere(position, radius, (int)target);
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000290AB File Offset: 0x000272AB
		public static Collider[] OverlapSphere(Vector3 position, float radius, WRaycastTarget target)
		{
			return Physics.OverlapSphere(position, radius, (int)target);
		}
	}
}
