using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x0200037D RID: 893
	public class TriggerVolume : MonoBehaviour
	{
		// Token: 0x0600167A RID: 5754 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		private void Awake()
		{
			this.UpdateAllData();
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		private void OnDisable()
		{
			if (this.IsRegisteredActorTrigger())
			{
				this.DisableActorTrigger();
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00011C00 File Offset: 0x0000FE00
		private void OnEnable()
		{
			if (this.IsRegisteredActorTrigger())
			{
				this.UpdateActorTriggerData();
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00011C10 File Offset: 0x0000FE10
		public void UpdateTransformDataIfNeeded()
		{
			if (base.transform.hasChanged)
			{
				this.UpdateVolumeTransformData();
				base.transform.hasChanged = false;
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00011C31 File Offset: 0x0000FE31
		private void Update()
		{
			this.UpdateTransformDataIfNeeded();
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000A0AA8 File Offset: 0x0009ECA8
		private void OnValidate()
		{
			this.data.ceiling = Mathf.Max(0f, this.data.ceiling);
			this.data.floor = Mathf.Max(0f, this.data.floor);
			this.data.GenerateBounds();
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00011C39 File Offset: 0x0000FE39
		public void UpdateAllData()
		{
			this.data.GenerateBounds();
			this.UpdateVolumeTransformData();
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00011C4C File Offset: 0x0000FE4C
		public void UpdateVolumeTransformData()
		{
			this.data.UpdateTransform(base.transform.worldToLocalMatrix);
			if (this.IsRegisteredActorTrigger())
			{
				this.UpdateActorTriggerData();
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00011C72 File Offset: 0x0000FE72
		private void UpdateActorTriggerData()
		{
			this.data.actorQueryActive = true;
			ActorManager.instance.activeTriggers[this.actorTriggerIndex] = this.data;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00011C9B File Offset: 0x0000FE9B
		private void DisableActorTrigger()
		{
			this.data.actorQueryActive = false;
			ActorManager.instance.activeTriggers[this.actorTriggerIndex] = this.data;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00011CC4 File Offset: 0x0000FEC4
		public void SetVertexPosition(int index, Vector3 worldPosition)
		{
			this.data.SetVertexPosition(index, worldPosition);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00011CD3 File Offset: 0x0000FED3
		public Vector3 GetLocalVertexPosition(int index)
		{
			return this.data.vertices[index].ToVector3XZ();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00011CEB File Offset: 0x0000FEEB
		public Matrix4x4 LocalToWorldMatrix()
		{
			return base.transform.localToWorldMatrix;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		public bool PointIsInVolume(Vector3 worldPoint)
		{
			return this.data.PointIsInVolume(worldPoint);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00011D06 File Offset: 0x0000FF06
		public bool IsRegisteredActorTrigger()
		{
			return this.actorTriggerIndex > -1;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00011D11 File Offset: 0x0000FF11
		public void RegisterActorTrigger()
		{
			if (this.IsRegisteredActorTrigger())
			{
				return;
			}
			this.actorTriggerIndex = ActorManager.RegisterTrigger(this);
			this.UpdateActorTriggerData();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00011D2E File Offset: 0x0000FF2E
		public TriggerVolumeActorState GetActorState()
		{
			if (this.actorTriggerIndex > -1)
			{
				return ActorManager.instance.triggerStates[this.actorTriggerIndex];
			}
			throw new Exception("Trigger Volume is not a registered actor trigger");
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000A0B00 File Offset: 0x0009ED00
		public int GetTeamCount(int team)
		{
			TriggerVolumeActorState actorState = this.GetActorState();
			if (team == 0)
			{
				return actorState.team0Actors;
			}
			return actorState.team1Actors;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000A0B24 File Offset: 0x0009ED24
		public bool ActorIsInside(Actor actor)
		{
			return this.GetActorState().GetActorIsInside(actor.actorIndex);
		}

		// Token: 0x040018F9 RID: 6393
		public TriggerVolume.RuntimeData data;

		// Token: 0x040018FA RID: 6394
		private int actorTriggerIndex = -1;

		// Token: 0x0200037E RID: 894
		[Serializable]
		public struct RuntimeData
		{
			// Token: 0x0600168E RID: 5774 RVA: 0x00011D68 File Offset: 0x0000FF68
			private Vector3 WorldToLocal(Vector3 worldPoint)
			{
				return this.worldToLocalMatrix.MultiplyPoint(worldPoint);
			}

			// Token: 0x0600168F RID: 5775 RVA: 0x000A0B48 File Offset: 0x0009ED48
			public void GenerateBounds()
			{
				Vector3 vector = this.vertices[0].ToVector3XZ();
				Vector3 max = vector;
				vector.y -= this.floor;
				max.y += this.ceiling;
				this.localBounds = new Bounds
				{
					min = vector,
					max = max
				};
				for (int i = 1; i < this.vertices.Count; i++)
				{
					this.localBounds.Encapsulate(this.vertices[i].ToVector3XZ());
				}
				this.UpdateWorldBounds();
			}

			// Token: 0x06001690 RID: 5776 RVA: 0x00011D76 File Offset: 0x0000FF76
			public void SetVertexPosition(int index, Vector3 worldPosition)
			{
				this.vertices[index] = this.WorldToLocal(worldPosition).ToVector2XZ();
				this.GenerateBounds();
			}

			// Token: 0x06001691 RID: 5777 RVA: 0x000A0BE4 File Offset: 0x0009EDE4
			public bool PointIsInVolume(Vector3 worldPoint)
			{
				if (!this.worldBounds.Contains(worldPoint))
				{
					return false;
				}
				Vector3 vector = this.WorldToLocal(worldPoint);
				if (!this.localBounds.Contains(vector))
				{
					return false;
				}
				Vector2 vector2 = vector.ToVector2XZ();
				int num = 0;
				for (int i = 0; i < this.vertices.Count; i++)
				{
					Vector2 vector3 = this.vertices[i];
					Vector2 vector4 = this.vertices[(i + 1) % this.vertices.Count];
					if (!Mathf.Approximately(vector3.y, vector4.y))
					{
						if (vector3.y > vector4.y)
						{
							Vector2 vector5 = vector3;
							vector3 = vector4;
							vector4 = vector5;
						}
						if (vector2.y >= vector3.y && vector2.y < vector4.y)
						{
							float num2 = vector2.y - vector3.y;
							float num3 = (vector4.x - vector3.x) / (vector4.y - vector3.y);
							if (vector3.x + num3 * num2 < vector2.x)
							{
								num++;
							}
						}
					}
				}
				return num % 2 == 1;
			}

			// Token: 0x06001692 RID: 5778 RVA: 0x00011D96 File Offset: 0x0000FF96
			public void UpdateTransform(Matrix4x4 worldToLocalMatrix)
			{
				this.worldToLocalMatrix = worldToLocalMatrix;
				this.UpdateWorldBounds();
			}

			// Token: 0x06001693 RID: 5779 RVA: 0x00011DA5 File Offset: 0x0000FFA5
			private void UpdateWorldBounds()
			{
				this.worldBounds = SMath.TransformBounds(this.localBounds, this.worldToLocalMatrix.inverse);
			}

			// Token: 0x040018FB RID: 6395
			public List<Vector2> vertices;

			// Token: 0x040018FC RID: 6396
			public float ceiling;

			// Token: 0x040018FD RID: 6397
			public float floor;

			// Token: 0x040018FE RID: 6398
			[NonSerialized]
			public Bounds worldBounds;

			// Token: 0x040018FF RID: 6399
			[NonSerialized]
			public Bounds localBounds;

			// Token: 0x04001900 RID: 6400
			[NonSerialized]
			public Matrix4x4 worldToLocalMatrix;

			// Token: 0x04001901 RID: 6401
			[NonSerialized]
			public bool actorQueryActive;
		}
	}
}
