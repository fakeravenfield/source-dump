using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class IngameUiWorker : MonoBehaviour
{
	// Token: 0x06001217 RID: 4631 RVA: 0x0000E383 File Offset: 0x0000C583
	public IngameUiWorker()
	{
		IngameUiWorker.instance = this;
		this.items = new List<IngameUiWorker.WorkItem>();
		this.idCount = 0;
		IngameUiWorker.OnResolutionChanged();
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x0000E3B3 File Offset: 0x0000C5B3
	public void Clear()
	{
		this.items.Clear();
		this.idCount = 0;
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x0008E004 File Offset: 0x0008C204
	public static int Register(IngameUiWorker.WorkItem item)
	{
		item.UpdateVirtualScreenSize();
		IngameUiWorker ingameUiWorker = IngameUiWorker.instance;
		int num = ingameUiWorker.idCount;
		ingameUiWorker.idCount = num + 1;
		item.id = num;
		IngameUiWorker.instance.items.Add(item);
		return item.id;
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x0008E04C File Offset: 0x0008C24C
	public static void ApplyClamp(int id, float clampSize, GameObject activateWhenClamped, GameObject deactivateWhenClamped)
	{
		IngameUiWorker.WorkItem value = IngameUiWorker.instance.items[id];
		value.ApplyClamp(clampSize, activateWhenClamped, deactivateWhenClamped);
		IngameUiWorker.instance.items[id] = value;
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x0008E088 File Offset: 0x0008C288
	public static bool Remove(int id)
	{
		for (int i = 0; i < IngameUiWorker.instance.items.Count; i++)
		{
			if (IngameUiWorker.instance.items[i].id == id)
			{
				IngameUiWorker.instance.items.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x0008E0DC File Offset: 0x0008C2DC
	public static void OnResolutionChanged()
	{
		if (IngameUiWorker.instance.items == null)
		{
			return;
		}
		for (int i = 0; i < IngameUiWorker.instance.items.Count; i++)
		{
			IngameUiWorker.instance.items[i].UpdateVirtualScreenSize();
		}
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x0008E128 File Offset: 0x0008C328
	public void LateUpdate()
	{
		if (!GameManager.instance.ingame)
		{
			return;
		}
		try
		{
			Camera activeCamera = FpsActorController.instance.GetActiveCamera();
			Matrix4x4 worldToCameraMatrix = activeCamera.worldToCameraMatrix;
			Matrix4x4 matrix4x = IngameUiWorker.normalizedScreenMatrix * activeCamera.projectionMatrix * worldToCameraMatrix;
			int i = 0;
			try
			{
				for (i = 0; i < this.items.Count; i++)
				{
					Vector3 position = this.items[i].GetPosition();
					Vector3 vector = worldToCameraMatrix.MultiplyPoint(position);
					Vector3 normalizedPosition = matrix4x.MultiplyPoint(position);
					this.items[i].UpdateElementPosition(normalizedPosition, vector.z < 0f);
				}
			}
			catch (Exception innerException)
			{
				Exception e = new Exception(string.Format("Could not update screen element with id {0}, removing it.", this.items[i].id), innerException);
				this.items.RemoveAt(i);
				ModManager.HandleModException(e);
			}
		}
		catch (Exception e2)
		{
			ModManager.HandleModException(e2);
		}
	}

	// Token: 0x04001343 RID: 4931
	private static IngameUiWorker instance;

	// Token: 0x04001344 RID: 4932
	public List<IngameUiWorker.WorkItem> items = new List<IngameUiWorker.WorkItem>();

	// Token: 0x04001345 RID: 4933
	private int idCount;

	// Token: 0x04001346 RID: 4934
	private static Vector2 screenSize;

	// Token: 0x04001347 RID: 4935
	private static Matrix4x4 normalizedScreenMatrix = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));

	// Token: 0x020002AB RID: 683
	public struct WorkItem
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x0008E23C File Offset: 0x0008C43C
		public Vector3 GetPosition()
		{
			if (this.transform != null)
			{
				return this.transform.localToWorldMatrix.MultiplyPoint(this.position);
			}
			if (this.actor != null)
			{
				return this.actor.CenterPosition() + this.position;
			}
			return this.position;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0000E400 File Offset: 0x0000C600
		public void ApplyClamp(float clampSize, GameObject activateWhenClamped, GameObject deactivateWhenClamped)
		{
			this.clamp = true;
			this.clampSize = clampSize;
			this.activateWhenClamped = activateWhenClamped;
			this.deactivateWhenClamped = deactivateWhenClamped;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0008E29C File Offset: 0x0008C49C
		public void UpdateVirtualScreenSize()
		{
			Canvas componentInParent = this.target.GetComponentInParent<Canvas>();
			this.virtualScreenSize = componentInParent.pixelRect.size / componentInParent.scaleFactor;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0008E2D4 File Offset: 0x0008C4D4
		public void UpdateElementPosition(Vector3 normalizedPosition, bool inFront)
		{
			Vector2 vector = normalizedPosition * this.virtualScreenSize;
			if (this.clamp)
			{
				bool flag;
				vector.x = this.Clamp(vector.x, this.clampSize, this.virtualScreenSize.x - this.clampSize, out flag);
				bool flag2;
				vector.y = this.Clamp(vector.y, this.clampSize, this.virtualScreenSize.y - this.clampSize, out flag2);
				bool flag3 = flag || flag2;
				GameObject gameObject = this.activateWhenClamped;
				if (gameObject != null)
				{
					gameObject.SetActive(flag3);
				}
				GameObject gameObject2 = this.deactivateWhenClamped;
				if (gameObject2 != null)
				{
					gameObject2.SetActive(!flag3);
				}
			}
			this.target.anchoredPosition = vector;
			GameObject gameObject3 = this.activateOnScreen;
			if (gameObject3 == null)
			{
				return;
			}
			gameObject3.SetActive(inFront);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0000E41E File Offset: 0x0000C61E
		private float Clamp(float value, float min, float max, out bool wasClamped)
		{
			wasClamped = false;
			if (value < min)
			{
				value = min;
				wasClamped = true;
			}
			else if (value > max)
			{
				value = max;
				wasClamped = true;
			}
			return value;
		}

		// Token: 0x04001348 RID: 4936
		public Actor actor;

		// Token: 0x04001349 RID: 4937
		public Transform transform;

		// Token: 0x0400134A RID: 4938
		public Vector3 position;

		// Token: 0x0400134B RID: 4939
		public RectTransform target;

		// Token: 0x0400134C RID: 4940
		public GameObject activateOnScreen;

		// Token: 0x0400134D RID: 4941
		public int id;

		// Token: 0x0400134E RID: 4942
		public bool clamp;

		// Token: 0x0400134F RID: 4943
		public float clampSize;

		// Token: 0x04001350 RID: 4944
		public GameObject activateWhenClamped;

		// Token: 0x04001351 RID: 4945
		public GameObject deactivateWhenClamped;

		// Token: 0x04001352 RID: 4946
		public Vector2 virtualScreenSize;
	}
}
