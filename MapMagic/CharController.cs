using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020004AE RID: 1198
	public class CharController : MonoBehaviour
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x000C514C File Offset: 0x000C334C
		public void Update()
		{
			if (this.hero == null)
			{
				this.hero = base.transform;
			}
			CapsuleCollider component = this.hero.GetComponent<CapsuleCollider>();
			if (component != null)
			{
				this.capsuleP1 = component.center + Vector3.up * (component.height / 2f - component.radius);
				this.capsuleP2 = component.center - Vector3.up * (component.height / 2f - component.radius);
				this.capsuleR = component.radius;
			}
			this.lagTimeLeft -= Time.deltaTime;
			if (this.useLag && this.lagTimeLeft > 0f)
			{
				return;
			}
			this.lagTimeLeft = this.lagTime;
			Vector3 vector;
			if (this.cameraSpace)
			{
				vector = new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z);
				if (this.gravity)
				{
					vector.y = 0f;
				}
			}
			else
			{
				vector = this.hero.transform.forward;
			}
			vector = vector.normalized;
			Vector3 b = Vector3.Cross(Vector3.up, vector);
			Vector3 vector2 = new Vector3(0f, 0f, 0f);
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				vector2 += vector;
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				vector2 -= vector;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				vector2 += b;
			}
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				vector2 -= b;
			}
			if (Input.GetKey(KeyCode.Space) && !this.inAir)
			{
				this.forceVelocity.y = this.jumpSpeed;
			}
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				this.speed *= this.shiftAcceleration;
			}
			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				this.speed /= this.shiftAcceleration;
			}
			vector2 *= this.speed * 10f;
			float time = Time.deltaTime;
			if (this.useLag)
			{
				time = Time.realtimeSinceStartup - this.oldTime;
			}
			this.hero.position = this.MoveChar(this.hero.position, vector2, time);
			this.oldTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000C53F0 File Offset: 0x000C35F0
		public Vector3 MoveChar(Vector3 pos, Vector3 direction, float time)
		{
			int num = (int)(time * this.stepsPerSecond) + 1;
			float num2 = time / (float)num;
			if ((float)num > this.stepsPerSecond / 10f)
			{
				num = (int)(this.stepsPerSecond / 10f);
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = pos;
				if (direction.sqrMagnitude > 0.01f)
				{
					this.velocity += direction.normalized * this.acceleration * num2;
					this.velocity = Vector3.ClampMagnitude(this.velocity, this.speed);
				}
				else
				{
					Vector3 b = this.velocity.normalized * this.acceleration * num2;
					if (this.velocity.sqrMagnitude > b.sqrMagnitude)
					{
						this.velocity -= b;
					}
					else
					{
						this.velocity = Vector3.zero;
					}
				}
				pos = this.TryMove(pos, this.velocity * num2);
				if (this.gravity)
				{
					this.forceVelocity += Vector3.down * num2 * 9.8f;
					if (this.forceVelocity.y * num2 > this.capsuleR)
					{
						this.forceVelocity.y = this.capsuleR / num2;
					}
					Vector3 vector2 = pos + this.forceVelocity * num2;
					if (!Physics.CheckSphere(vector2 + this.capsuleP2, this.capsuleR) && !Physics.CheckSphere(vector2 + this.capsuleP1, this.capsuleR))
					{
						pos = vector2;
						this.inAir = true;
					}
					else
					{
						this.forceVelocity.y = 0f;
						this.inAir = false;
					}
				}
				if (Physics.CheckSphere(pos + this.capsuleP1, this.capsuleR) || Physics.CheckSphere(pos + this.capsuleP2, this.capsuleR) || Physics.Linecast(vector, pos))
				{
					Debug.Log("CharController stuck");
					pos = vector;
					if (Physics.CheckSphere(pos + this.capsuleP1, this.capsuleR) || Physics.CheckSphere(pos + this.capsuleP2, this.capsuleR))
					{
						Debug.Log("CharController locked");
						pos = this.GetOutofStuck(pos);
					}
				}
			}
			return pos;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000C564C File Offset: 0x000C384C
		public Vector3 TryMove(Vector3 pos, Vector3 moveVector)
		{
			if (!Physics.CheckSphere(pos + moveVector + this.capsuleP1, this.capsuleR) && !Physics.CheckSphere(pos + moveVector + this.capsuleP2, this.capsuleR))
			{
				return pos + moveVector;
			}
			Vector3 normalized = Vector3.Cross(moveVector, Vector3.up).normalized;
			Vector3 normalized2 = Vector3.Cross(moveVector, normalized).normalized;
			float magnitude = moveVector.magnitude;
			for (float num = 0.5f; num < 100f; num = num * 1.5f + 0.5f)
			{
				for (int i = 0; i < 8; i++)
				{
					Vector3 vector = (moveVector + normalized * (float)CharController.searchDirHor[i] * num * magnitude * 0.1f + normalized2 * (float)CharController.searchDirVert[i] * num * magnitude * 0.1f).normalized;
					vector *= Vector3.Dot(moveVector, vector);
					if (!Physics.CheckSphere(pos + vector + this.capsuleP1, this.capsuleR) && !Physics.CheckSphere(pos + vector + this.capsuleP2, this.capsuleR))
					{
						return pos + vector;
					}
				}
			}
			return pos;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000C57C8 File Offset: 0x000C39C8
		public Vector3 GetOutofStuck(Vector3 pos)
		{
			for (float num = 0.07f; num < 3f; num *= 1.5f)
			{
				for (int i = 0; i < 20; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						int num2;
						if (i % 2 == 0)
						{
							num2 = i * i;
						}
						else
						{
							num2 = -(i + 1) * (i + 1);
						}
						int num3;
						if (j % 2 == 0)
						{
							num3 = j * j;
						}
						else
						{
							num3 = -(j + 1) * (j + 1);
						}
						Vector3 b = new Vector3((float)num2, 150f, (float)num3).normalized;
						b.y -= 0.5f;
						b = b.normalized * num;
						if (!Physics.CheckSphere(pos + b + this.capsuleP1, this.capsuleR) && !Physics.CheckSphere(pos + b + this.capsuleP2, this.capsuleR))
						{
							return pos + b;
						}
					}
				}
			}
			return pos;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0000296E File Offset: 0x00000B6E
		public void OnDrawGizmos()
		{
		}

		// Token: 0x04001E84 RID: 7812
		public Transform hero;

		// Token: 0x04001E85 RID: 7813
		public bool cameraSpace = true;

		// Token: 0x04001E86 RID: 7814
		public float speed = 3.5f;

		// Token: 0x04001E87 RID: 7815
		public float acceleration = 50f;

		// Token: 0x04001E88 RID: 7816
		public float shiftAcceleration = 1.75f;

		// Token: 0x04001E89 RID: 7817
		public float jumpSpeed = 5f;

		// Token: 0x04001E8A RID: 7818
		public bool gravity = true;

		// Token: 0x04001E8B RID: 7819
		public Vector3 velocity = Vector3.zero;

		// Token: 0x04001E8C RID: 7820
		public Vector3 forceVelocity = Vector3.zero;

		// Token: 0x04001E8D RID: 7821
		public bool inAir = true;

		// Token: 0x04001E8E RID: 7822
		public Vector3 capsuleP1;

		// Token: 0x04001E8F RID: 7823
		public Vector3 capsuleP2;

		// Token: 0x04001E90 RID: 7824
		public float capsuleR;

		// Token: 0x04001E91 RID: 7825
		public float stepsPerSecond = 50f;

		// Token: 0x04001E92 RID: 7826
		public bool useLag;

		// Token: 0x04001E93 RID: 7827
		public float lagTime = 0.1f;

		// Token: 0x04001E94 RID: 7828
		public float lagTimeLeft;

		// Token: 0x04001E95 RID: 7829
		public float oldTime;

		// Token: 0x04001E96 RID: 7830
		public static readonly int[] searchDirVert = new int[]
		{
			1,
			-1,
			0,
			0,
			1,
			1,
			-1,
			-1
		};

		// Token: 0x04001E97 RID: 7831
		public static readonly int[] searchDirHor = new int[]
		{
			0,
			0,
			1,
			-1,
			-1,
			1,
			1,
			-1
		};
	}
}
