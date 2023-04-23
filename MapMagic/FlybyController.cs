using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004B0 RID: 1200
	public class FlybyController : MonoBehaviour
	{
		// Token: 0x06001E10 RID: 7696 RVA: 0x000C5BA8 File Offset: 0x000C3DA8
		public Vector3 RayCast(Vector3 start, Vector3 target)
		{
			Vector3 direction = target - start;
			float magnitude = direction.magnitude;
			RaycastHit raycastHit = default(RaycastHit);
			if (!Physics.Raycast(start, direction, out raycastHit, magnitude))
			{
				return target;
			}
			return raycastHit.point;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000C5BE4 File Offset: 0x000C3DE4
		public Vector3 SphereCast(Vector3 start, Vector3 target, float radius)
		{
			Vector3 vector = target - start;
			float magnitude = vector.magnitude;
			RaycastHit raycastHit = default(RaycastHit);
			if (!Physics.SphereCast(start, radius, vector, out raycastHit, magnitude))
			{
				return target;
			}
			return start + raycastHit.distance * (vector / magnitude);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000C5C34 File Offset: 0x000C3E34
		public Vector3 FindLowestPoint(Vector3 pos, float direction, float angle, float dist, int angleSteps, int distSteps, float sphereRadius)
		{
			Vector3 vector = pos + new Vector3(0f, 2000000f, 0f);
			for (int i = 0; i < angleSteps; i++)
			{
				float num = direction - angle / 2f + angle / (float)angleSteps * (float)i + angle / (float)angleSteps / 2f;
				for (int j = 0; j < distSteps; j++)
				{
					float d = dist / 2f + dist / 2f / (float)distSteps * (float)j;
					Vector3 vector2 = new Vector3(Mathf.Sin(num * 0.017453292f), 0f, Mathf.Cos(num * 0.017453292f)) * d + pos;
					vector2.y = pos.y + 1000f;
					Vector3 vector3 = this.SphereCast(vector2, vector2 - new Vector3(0f, 1000f, 0f), sphereRadius);
					if (vector3.y < vector.y)
					{
						vector = vector3;
					}
				}
			}
			return vector;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000163F0 File Offset: 0x000145F0
		public IEnumerable<Vector3> SpokeDirections2D(Vector3 direction, float fov, float steps)
		{
			float angle = direction.Angle();
			float stepAngle = fov / (steps - 1f);
			if (steps % 2f == 0f)
			{
				float startAngle = stepAngle / 2f;
				int halfSteps = (int)(steps / 2f);
				int num;
				for (int a = 0; a < halfSteps; a = num + 1)
				{
					yield return (angle + startAngle + stepAngle * (float)a).Direction();
					yield return (angle - startAngle - stepAngle * (float)a).Direction();
					num = a;
				}
			}
			else
			{
				yield return angle.Direction();
				int halfSteps = (int)((steps + 1f) / 2f);
				int num;
				for (int a = 0; a < halfSteps; a = num + 1)
				{
					yield return (angle + stepAngle * (float)a).Direction();
					yield return (angle - stepAngle * (float)a).Direction();
					num = a;
				}
			}
			yield break;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x0001640E File Offset: 0x0001460E
		public IEnumerable<Vector3> LinePositions2D(Vector3 direction, float distance, float steps)
		{
			Vector3 start = -direction * (distance / 2f);
			float stepDist = distance / (steps - 1f);
			int i = 0;
			while ((float)i < steps)
			{
				yield return start + direction * stepDist * (float)i;
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0001642C File Offset: 0x0001462C
		public IEnumerable<Vector3> RectPositions2D(Vector3 direction, float distanceAlong, float stepsAlong, float distanceAcross, float stepsAcross)
		{
			float stepDistAlong = distanceAlong / (stepsAlong - 1f);
			float stepDistAcross = distanceAcross / (stepsAcross - 1f);
			Vector3 perpendicular = Vector3.Cross(Vector3.up, direction);
			Vector3 startAlong = -direction * (distanceAlong / 2f);
			Vector3 startAcross = -perpendicular * (distanceAcross / 2f);
			int i = 0;
			while ((float)i < stepsAlong)
			{
				Vector3 pos = startAlong + direction * stepDistAlong * (float)i;
				int j = 0;
				int num;
				while ((float)j < stepsAcross)
				{
					yield return pos + startAcross + perpendicular * stepDistAcross * (float)j;
					num = j;
					j = num + 1;
				}
				pos = default(Vector3);
				num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000C5D3C File Offset: 0x000C3F3C
		public Vector3 LowestPoint(Vector3 pos, Vector3 dir, float dist, float fov, float radius)
		{
			Vector3 vector = pos;
			vector.y = pos.y + 20000f;
			bool flag = false;
			foreach (Vector3 vector2 in this.SpokeDirections2D(dir, fov, 7f))
			{
				foreach (Vector3 b in this.LinePositions2D(vector2, dist / 4f, 5f))
				{
					Vector3 a = pos + vector2 * (dist / 2f + 50f) + b;
					if (this.RayCast(a + Vector3.up * 2000f, a + Vector3.down * 1000f).y <= vector.y - radius)
					{
						Vector3 vector3 = this.SphereCast(a + Vector3.up * 2000f, a + Vector3.down * 1000f, radius);
						if (vector3.y <= vector.y && vector3.y >= 0f && !Physics.Linecast(pos, vector3))
						{
							vector = vector3;
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				return vector;
			}
			return pos + dir * dist / 2f;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000C5EF0 File Offset: 0x000C40F0
		private void OnEnable()
		{
			this.target = this.LowestPoint(base.transform.localPosition, this.flyDir, this.evalDist, this.evalFov, this.evalRadius);
			this.targetDist = (this.target - base.transform.localPosition).magnitude;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000C5F50 File Offset: 0x000C4150
		private void Update()
		{
			if (!Physics.Raycast(base.transform.localPosition + new Vector3(0f, 2000f, 0f), Vector3.down, 4000f))
			{
				return;
			}
			float magnitude = (base.transform.localPosition - this.target).magnitude;
			if (magnitude > this.targetDist)
			{
				this.debugPos = this.target;
				this.target = this.LowestPoint(this.target, this.flyDir, this.evalDist, this.evalFov, this.evalRadius);
				magnitude = (this.target - base.transform.localPosition).magnitude;
			}
			this.targetDist = magnitude;
			this.moveVector = Vector3.SmoothDamp(this.moveVector, this.target - base.transform.localPosition, ref this.rotateVelocity, this.smooth);
			this.moveVector = this.moveVector.normalized;
			Vector3 vector = base.transform.localPosition + this.moveVector * Time.deltaTime * this.speed;
			Vector3 vector2 = this.RayCast(vector + new Vector3(0f, 2000f, 0f), vector - new Vector3(0f, 2000f, 0f)) + Vector3.up * 2f;
			if (vector.y < vector2.y)
			{
				vector = vector2;
			}
			base.transform.localPosition = vector;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0001645A File Offset: 0x0001465A
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.target, this.evalRadius);
		}

		// Token: 0x04001E9D RID: 7837
		public float evalDist = 300f;

		// Token: 0x04001E9E RID: 7838
		public float evalFov = 60f;

		// Token: 0x04001E9F RID: 7839
		public float evalRadius = 50f;

		// Token: 0x04001EA0 RID: 7840
		private Vector3 target;

		// Token: 0x04001EA1 RID: 7841
		private float targetDist;

		// Token: 0x04001EA2 RID: 7842
		private Vector3 flyDir = new Vector3(1f, 0f, 0f);

		// Token: 0x04001EA3 RID: 7843
		private Vector3 moveVector;

		// Token: 0x04001EA4 RID: 7844
		private Vector3 rotateVelocity;

		// Token: 0x04001EA5 RID: 7845
		public float speed = 100f;

		// Token: 0x04001EA6 RID: 7846
		public float smooth = 10f;

		// Token: 0x04001EA7 RID: 7847
		public Vector3 debugPos;
	}
}
