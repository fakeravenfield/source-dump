using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004AF RID: 1199
	public class CharRover : MonoBehaviour
	{
		// Token: 0x06001E0D RID: 7693 RVA: 0x000C594C File Offset: 0x000C3B4C
		public void Update()
		{
			Vector3 vector = base.transform.forward;
			vector.y = 0f;
			vector = vector.normalized;
			vector.y = -this.descent;
			vector = vector.normalized;
			vector *= this.collisionDist;
			Debug.DrawLine(base.transform.position, base.transform.position + vector, Color.red);
			Vector3 possibleDirection = CharRover.GetPossibleDirection(base.transform.position, this.collisionRadius, vector, 4f, 4f, 20);
			Vector3 normalized = possibleDirection.normalized;
			base.transform.position += normalized * this.speed * Time.deltaTime;
			Quaternion b = Quaternion.LookRotation(possibleDirection);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, this.rotationSmoothness);
			Debug.DrawLine(base.transform.position, base.transform.position + possibleDirection, Color.green);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000C5A6C File Offset: 0x000C3C6C
		public static Vector3 GetPossibleDirection(Vector3 pos, float radius, Vector3 direction, float horizontalStep = 5f, float verticalStep = 5f, int numSteps = 20)
		{
			float num = Mathf.Atan2(direction.x, direction.z);
			float num2 = Mathf.Asin(direction.y / direction.magnitude);
			Coord coord = new Coord(0, 0);
			foreach (Coord coord2 in coord.DistanceArea(numSteps / 2))
			{
				float f = num + (float)coord2.x * horizontalStep * 0.017453292f;
				float f2 = num2 + (float)coord2.z * verticalStep * 0.017453292f;
				Vector3 vector = new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f));
				vector *= Mathf.Cos(f2);
				vector.y = Mathf.Sin(f2);
				vector *= direction.magnitude;
				Ray ray = new Ray(pos, vector);
				if (!Physics.Raycast(ray, direction.magnitude) && !Physics.CheckSphere(pos + vector, radius) && !Physics.SphereCast(ray, radius, direction.magnitude))
				{
					return vector;
				}
			}
			return Vector3.zero;
		}

		// Token: 0x04001E98 RID: 7832
		public float speed = 20f;

		// Token: 0x04001E99 RID: 7833
		public float descent = 0.25f;

		// Token: 0x04001E9A RID: 7834
		public float collisionDist = 200f;

		// Token: 0x04001E9B RID: 7835
		public float collisionRadius = 10f;

		// Token: 0x04001E9C RID: 7836
		public float rotationSmoothness = 0.5f;
	}
}
