using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C1 RID: 705
public class ObjectiveEntry : MonoBehaviour
{
	// Token: 0x060012D1 RID: 4817 RVA: 0x0000EE1B File Offset: 0x0000D01B
	private void Awake()
	{
		this.worldIndicator.gameObject.SetActive(false);
		this.SetUncleared();
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x0000684B File Offset: 0x00004A4B
	private void Remove()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x0000EE34 File Offset: 0x0000D034
	public void SetText(string text)
	{
		this.text.text = text;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x00091458 File Offset: 0x0008F658
	public void SetUncleared()
	{
		this.isCompleted = false;
		this.isFailed = false;
		this.text.CrossFadeColor(Color.white, 0.5f, false, false);
		this.strikethrough.gameObject.SetActive(false);
		this.indicatorLine.gameObject.SetActive(true);
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000914AC File Offset: 0x0008F6AC
	public void SetCompleted()
	{
		this.isCompleted = true;
		this.text.CrossFadeColor(Color.gray, 0.5f, false, false);
		this.strikethrough.gameObject.SetActive(true);
		this.indicatorLine.gameObject.SetActive(false);
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000914FC File Offset: 0x0008F6FC
	public void SetFailed()
	{
		this.isFailed = true;
		this.text.CrossFadeColor(Color.red, 0.5f, false, false);
		this.strikethrough.gameObject.SetActive(true);
		this.indicatorLine.gameObject.SetActive(false);
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0000EE42 File Offset: 0x0000D042
	public void SetWorldTarget(Transform target)
	{
		this.targetTransform = target;
		this.hasWorldTarget = true;
		this.worldIndicator.gameObject.SetActive(true);
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x0000EE63 File Offset: 0x0000D063
	public void SetWorldTarget(Vector3 position)
	{
		this.targetTransform = null;
		this.targetPosition = position;
		this.hasWorldTarget = true;
		this.worldIndicator.gameObject.SetActive(true);
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x0000EE8B File Offset: 0x0000D08B
	public void DropWorldTarget()
	{
		this.hasWorldTarget = false;
		this.worldIndicator.gameObject.SetActive(false);
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
	public void SetSortIndex(int index)
	{
		base.transform.SetSiblingIndex(Mathf.Max(1, index + 1));
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x0000EEBB File Offset: 0x0000D0BB
	public Vector3 GetTargetWorldPosition()
	{
		if (!(this.targetTransform != null))
		{
			return this.targetPosition;
		}
		return this.targetTransform.position;
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x0009154C File Offset: 0x0008F74C
	public void LateUpdate()
	{
		if (this.hasWorldTarget)
		{
			bool flag = MinimapUi.DrawObjectiveMarkersOnMinimap();
			Vector3 targetWorldPosition = this.GetTargetWorldPosition();
			Vector3 vector = Vector3.zero;
			float num = Vector3.Distance(FpsActorController.instance.actor.Position(), targetWorldPosition);
			if (flag)
			{
				vector = MinimapCamera.WorldToMinimapScreenPosition(targetWorldPosition);
			}
			else
			{
				vector = FpsActorController.instance.GetActiveCamera().WorldToScreenPoint(targetWorldPosition);
				if (vector.z < 0f || vector.x < 0f || vector.y < 0f || vector.x > (float)Screen.width || vector.y > (float)Screen.height)
				{
					this.worldIndicator.gameObject.SetActive(false);
					return;
				}
			}
			this.distanceText.text = string.Format("{0} m", (int)num);
			bool enabled = !FpsActorController.instance.actor.dead;
			this.distanceText.enabled = enabled;
			this.worldIndicator.gameObject.SetActive(true);
			Vector3 position = this.worldIndicator.position;
			Vector3 vector2 = vector - position;
			vector2.z = 0f;
			float num2 = 0f;
			if (Mathf.Abs(vector2.x) > Mathf.Epsilon || Mathf.Abs(vector2.y) > Mathf.Epsilon)
			{
				num2 = Mathf.Atan2(vector2.y, vector2.x);
			}
			this.worldIndicator.localEulerAngles = new Vector3(0f, 0f, num2 * 57.29578f);
			this.worldIndicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector2.magnitude);
			this.distanceText.rectTransform.rotation = Quaternion.identity;
		}
	}

	// Token: 0x04001417 RID: 5143
	public Text text;

	// Token: 0x04001418 RID: 5144
	public Text distanceText;

	// Token: 0x04001419 RID: 5145
	public RectTransform indicatorLine;

	// Token: 0x0400141A RID: 5146
	public RectTransform worldIndicator;

	// Token: 0x0400141B RID: 5147
	public RawImage strikethrough;

	// Token: 0x0400141C RID: 5148
	private const string DISTANCE_TEXT_FORMAT = "{0} m";

	// Token: 0x0400141D RID: 5149
	[NonSerialized]
	public bool hasWorldTarget;

	// Token: 0x0400141E RID: 5150
	[NonSerialized]
	public Transform targetTransform;

	// Token: 0x0400141F RID: 5151
	[NonSerialized]
	public Vector3 targetPosition;

	// Token: 0x04001420 RID: 5152
	[NonSerialized]
	public bool isCompleted;

	// Token: 0x04001421 RID: 5153
	[NonSerialized]
	public bool isFailed;
}
