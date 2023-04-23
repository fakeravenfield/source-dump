using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x0200037B RID: 891
	public class ScriptedPathSeeker
	{
		// Token: 0x06001668 RID: 5736 RVA: 0x000A03B0 File Offset: 0x0009E5B0
		public ScriptedPathSeeker(IScriptedPathSeekerCallbackTarget callbackTarget)
		{
			this.callbackTarget = callbackTarget;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000A0400 File Offset: 0x0009E600
		public void SetPath(ScriptedPath path, Vector3 position, Quaternion rotation, int startIndex = 0)
		{
			this.path = path;
			this.CreateModifierQueue();
			this.position = position;
			this.rotation = rotation;
			if (this.path.GetNodeCount() < 2)
			{
				this.isTraversingPath = false;
				ModManager.HandleModException(new Exception(string.Format("ScriptedPath must have {0} or more nodes", 2)));
				return;
			}
			this.isTraversingPath = true;
			this.destinationNodeIndex = startIndex - 1;
			this.modifierData = default(ScriptedPathSeeker.ModifierData);
			this.NextNode();
			this.UpdateNextSyncNumber();
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000A0484 File Offset: 0x0009E684
		private void CreateModifierQueue()
		{
			this.path.SortModifiers();
			this.pendingModifiers = new Queue<ScriptedPathEdgeModifier>(this.path.modifiers.Count);
			for (int i = 0; i < this.path.modifiers.Count; i++)
			{
				this.pendingModifiers.Enqueue(this.path.modifiers[i]);
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000A04F0 File Offset: 0x0009E6F0
		private void UpdateNextSyncNumber()
		{
			this.nextSyncNumber = byte.MaxValue;
			int nodeCount = this.path.GetNodeCount();
			for (int i = this.destinationNodeIndex; i < nodeCount; i++)
			{
				ScriptedPathNode scriptedPathNode = this.path.pathNodes[i];
				if (scriptedPathNode.synchronize)
				{
					this.nextSyncNumber = scriptedPathNode.syncNumber;
					return;
				}
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00011B2D File Offset: 0x0000FD2D
		private void TryNextNode()
		{
			if (this.path.pathNodes[this.destinationNodeIndex].synchronize)
			{
				this.awaitingSync = true;
				return;
			}
			this.NextNode();
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000A054C File Offset: 0x0009E74C
		private void NextNode()
		{
			ScriptedPathNode scriptedPathNode = this.path.pathNodes[this.destinationNodeIndex];
			this.nodeStartPosition = this.position;
			this.awaitingSync = false;
			this.stance = scriptedPathNode.stance;
			this.destinationNodeIndex++;
			if (this.destinationNodeIndex == this.path.GetNodeCount())
			{
				this.OnPathCompleted();
				return;
			}
			float waitTime = this.path.pathNodes[this.destinationNodeIndex - 1].waitTime;
			if (waitTime > 0f)
			{
				this.waitAction.StartLifetime(waitTime);
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00011B5A File Offset: 0x0000FD5A
		public void Synchronize(byte syncNumber)
		{
			if (this.awaitingSync && this.nextSyncNumber == syncNumber)
			{
				this.NextNode();
				this.UpdateNextSyncNumber();
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00011B79 File Offset: 0x0000FD79
		private void OnPathCompleted()
		{
			this.Stop();
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00011B81 File Offset: 0x0000FD81
		public void Stop()
		{
			this.isTraversingPath = false;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00011B8A File Offset: 0x0000FD8A
		public void Update(float dt, float defaultSpeed)
		{
			if (this.path == null)
			{
				return;
			}
			if (!this.awaitingSync && this.waitAction.TrueDone())
			{
				this.UpdateMovement(dt, defaultSpeed);
				this.UpdateModifiers(dt);
			}
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000A05EC File Offset: 0x0009E7EC
		private void UpdateMovement(float dt, float defaultSpeed)
		{
			Vector3 nodeWorldPosition = this.path.GetNodeWorldPosition(this.destinationNodeIndex);
			Vector3 a = nodeWorldPosition - this.position;
			float magnitude = a.magnitude;
			float magnitude2 = (this.position - this.nodeStartPosition).magnitude;
			float magnitude3 = (nodeWorldPosition - this.nodeStartPosition).magnitude;
			float a2 = magnitude3 / 2f;
			float num = Mathf.Max(0.5f, this.GetNodeSpeed(this.destinationNodeIndex - 2, defaultSpeed, true));
			float num2 = Mathf.Max(0.5f, this.GetNodeSpeed(this.destinationNodeIndex - 1, defaultSpeed, false));
			float b = Mathf.Min(a2, num * 0.5f);
			float t = Mathf.InverseLerp(0f, b, magnitude2);
			float num3 = Mathf.Lerp(num, num2, t);
			if (this.destinationNodeIndex > 0 && this.path.pathNodes[this.destinationNodeIndex - 1].slowDownForNextTurn)
			{
				float num4 = (num2 + num) / 2f;
				float t2 = Mathf.InverseLerp(Mathf.Min(a2, num4 * 1f), 0f, magnitude);
				float num5 = Mathf.Lerp(1f, 0.4f, t2);
				num3 *= num5;
			}
			num3 = Mathf.Max(0.5f, num3);
			Vector3 vector = this.position + a / magnitude * num3 * dt;
			Vector3 a3 = vector - this.position;
			float num6 = SMath.LineSegmentVsPointClosestT(this.position, vector, nodeWorldPosition);
			Vector3 vector2 = this.position + a3 * num6;
			float num7 = Mathf.Max(2f, num2 * 2f);
			if (this.modifierData.hasOverrideLookAtPoint)
			{
				this.lookAtPoint = this.modifierData.overrideLookAtPoint;
			}
			else if (magnitude > num7)
			{
				this.lookAtPoint = nodeWorldPosition;
			}
			else if (this.destinationNodeIndex < this.path.GetNodeCount() - 1)
			{
				float num8 = num7 - magnitude;
				num8 = Mathf.Min(num8, magnitude2 * 2f);
				Vector3 a4 = this.path.GetNodeWorldPosition(this.destinationNodeIndex + 1) - nodeWorldPosition;
				float magnitude4 = a4.magnitude;
				num8 = Mathf.Min(num8, magnitude4);
				this.lookAtPoint = nodeWorldPosition + a4 / magnitude4 * num8;
			}
			else
			{
				this.lookAtPoint = vector2;
			}
			Quaternion target = Quaternion.LookRotation(this.lookAtPoint - this.position);
			this.rotation = SMath.DampSlerp(this.rotation, target, 0.005f, dt);
			this.position = vector2;
			this.currentSeekerSequenceValue = (float)(this.destinationNodeIndex - 1) + magnitude2 / magnitude3;
			if (num6 < 1f)
			{
				this.TryNextNode();
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x000A08B4 File Offset: 0x0009EAB4
		private void UpdateModifiers(float dt)
		{
			if (this.pendingModifiers.Count > 0)
			{
				float sequenceValue = this.pendingModifiers.Peek().GetSequenceValue();
				if (this.currentSeekerSequenceValue > sequenceValue)
				{
					this.OnPassModifier(this.pendingModifiers.Dequeue());
				}
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000A08FC File Offset: 0x0009EAFC
		private void OnPassModifier(ScriptedPathEdgeModifier scriptedPathEdgeModifier)
		{
			try
			{
				scriptedPathEdgeModifier.OnPassed(this);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000A092C File Offset: 0x0009EB2C
		public void OverrideLookAtPoint(Vector3 lookAtPoint, LookAtModifier.Target target)
		{
			if (target == LookAtModifier.Target.Body)
			{
				this.modifierData.overrideLookAtPoint = lookAtPoint;
				this.modifierData.hasOverrideLookAtPoint = true;
				return;
			}
			if (this.modifierData.hasHeadLookAtPoint)
			{
				this.modifierData.prevHeadLookAtPoint = this.modifierData.headLookAtPoint;
				this.interpolateHeadPositionAction.Start();
			}
			else
			{
				this.interpolateHeadWeightAction.Start();
			}
			this.modifierData.headLookAtPoint = lookAtPoint;
			this.modifierData.hasHeadLookAtPoint = true;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00011BBF File Offset: 0x0000FDBF
		public void CancelOverrideLookAtPoint(LookAtModifier.Target target)
		{
			if (target == LookAtModifier.Target.Body)
			{
				this.modifierData.hasOverrideLookAtPoint = false;
				return;
			}
			this.modifierData.hasHeadLookAtPoint = false;
			this.interpolateHeadWeightAction.Start();
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000A09A8 File Offset: 0x0009EBA8
		public Vector3 GetInterpolatedHeadLookAtPoint()
		{
			if (this.interpolateHeadPositionAction.TrueDone())
			{
				return this.modifierData.headLookAtPoint;
			}
			return Vector3.Lerp(this.modifierData.prevHeadLookAtPoint, this.modifierData.headLookAtPoint, this.interpolateHeadPositionAction.Ratio());
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000A09F4 File Offset: 0x0009EBF4
		public float GetInterpolatedHeadIKWeight()
		{
			if (this.interpolateHeadWeightAction.TrueDone())
			{
				if (!this.modifierData.hasHeadLookAtPoint)
				{
					return 0f;
				}
				return 1f;
			}
			else
			{
				if (!this.modifierData.hasHeadLookAtPoint)
				{
					return 1f - this.interpolateHeadWeightAction.Ratio();
				}
				return this.interpolateHeadWeightAction.Ratio();
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000A0A54 File Offset: 0x0009EC54
		public float GetNodeSpeed(int index, float defaultSpeed, bool isPrevious)
		{
			if (index < 0 || index >= this.path.GetNodeCount() - 1)
			{
				return defaultSpeed;
			}
			ScriptedPathNode scriptedPathNode = this.path.pathNodes[index];
			if (isPrevious && scriptedPathNode.slowDownForNextTurn)
			{
				return scriptedPathNode.speed * 0.4f;
			}
			return scriptedPathNode.speed;
		}

		// Token: 0x040018D6 RID: 6358
		private const int MIN_VALID_NODE_COUNT = 2;

		// Token: 0x040018D7 RID: 6359
		public const byte SYNC_NUMBER_NONE = 255;

		// Token: 0x040018D8 RID: 6360
		public const float MIN_SPEED = 0.5f;

		// Token: 0x040018D9 RID: 6361
		private const float CHANGE_SPEED_EXTRAPOLATION_TIME = 0.5f;

		// Token: 0x040018DA RID: 6362
		private const float SLOW_DOWN_EXTRAPOLATION_TIME = 1f;

		// Token: 0x040018DB RID: 6363
		private const float SLOW_DOWN_SPEED_MULTIPLIER = 0.4f;

		// Token: 0x040018DC RID: 6364
		private const float AIM_LEAD_EXTRAPOLATION_TIME = 2f;

		// Token: 0x040018DD RID: 6365
		private const float MIN_AIM_LEAD_DISTANCE = 2f;

		// Token: 0x040018DE RID: 6366
		private const float LOOK_TOWARDS_TARGET_DAMP = 0.005f;

		// Token: 0x040018DF RID: 6367
		public ScriptedPath path;

		// Token: 0x040018E0 RID: 6368
		private Vector3 nodeStartPosition;

		// Token: 0x040018E1 RID: 6369
		public Vector3 position;

		// Token: 0x040018E2 RID: 6370
		public Quaternion rotation;

		// Token: 0x040018E3 RID: 6371
		public Vector3 lookAtPoint;

		// Token: 0x040018E4 RID: 6372
		public int destinationNodeIndex;

		// Token: 0x040018E5 RID: 6373
		[NonSerialized]
		public bool isTraversingPath;

		// Token: 0x040018E6 RID: 6374
		public ScriptedPathNode.Stance stance;

		// Token: 0x040018E7 RID: 6375
		public byte nextSyncNumber;

		// Token: 0x040018E8 RID: 6376
		public bool awaitingSync;

		// Token: 0x040018E9 RID: 6377
		public bool awaitingTimer;

		// Token: 0x040018EA RID: 6378
		private TimedAction waitAction = new TimedAction(1f, true);

		// Token: 0x040018EB RID: 6379
		private float currentSeekerSequenceValue;

		// Token: 0x040018EC RID: 6380
		public IScriptedPathSeekerCallbackTarget callbackTarget;

		// Token: 0x040018ED RID: 6381
		public ScriptedPathSeeker.ModifierData modifierData;

		// Token: 0x040018EE RID: 6382
		public TimedAction interpolateHeadWeightAction = new TimedAction(0.3f, true);

		// Token: 0x040018EF RID: 6383
		public TimedAction interpolateHeadPositionAction = new TimedAction(0.3f, true);

		// Token: 0x040018F0 RID: 6384
		private Queue<ScriptedPathEdgeModifier> pendingModifiers;

		// Token: 0x0200037C RID: 892
		public struct ModifierData
		{
			// Token: 0x040018F1 RID: 6385
			public Vector3 overrideLookAtPoint;

			// Token: 0x040018F2 RID: 6386
			public bool hasOverrideLookAtPoint;

			// Token: 0x040018F3 RID: 6387
			public Vector3 prevHeadLookAtPoint;

			// Token: 0x040018F4 RID: 6388
			public Vector3 headLookAtPoint;

			// Token: 0x040018F5 RID: 6389
			public bool hasHeadLookAtPoint;

			// Token: 0x040018F6 RID: 6390
			public bool isNotAlert;

			// Token: 0x040018F7 RID: 6391
			public bool isLeaning;

			// Token: 0x040018F8 RID: 6392
			public float lean;
		}
	}
}
