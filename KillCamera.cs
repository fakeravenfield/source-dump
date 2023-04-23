using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B2 RID: 690
public class KillCamera : MonoBehaviour
{
	// Token: 0x0600124B RID: 4683 RVA: 0x0000E6F5 File Offset: 0x0000C8F5
	private static Renderer[] GetRenderers(Transform parent)
	{
		List<Renderer> list = new List<Renderer>();
		list.AddRange(parent.GetComponentsInChildren<MeshRenderer>());
		list.AddRange(parent.GetComponentsInChildren<SkinnedMeshRenderer>());
		return list.ToArray();
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x0008EB70 File Offset: 0x0008CD70
	public static void Show(Actor killer, string info)
	{
		KillCamera.instance.killerText.text = "KILLED BY " + killer.name;
		KillCamera.instance.infoText.text = info;
		KillCamera.instance.canvas.enabled = true;
		KillCamera.instance.killcamImage.enabled = true;
		KillCamera.instance.splatImage.enabled = true;
		KillCamera.instance.splatImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 360f));
		Color color = ColorScheme.TeamColor(killer.team);
		color = Color.Lerp(color, Color.white, 0.2f);
		color.a = 0.95f;
		KillCamera.instance.splatImage.color = color;
		KillCamera.instance.splatAnimationAction.Start();
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x0008EC58 File Offset: 0x0008CE58
	public static void ShowSuicide()
	{
		KillCamera.instance.killerText.text = "OOPSIE...";
		KillCamera.instance.canvas.enabled = true;
		KillCamera.instance.killcamImage.enabled = false;
		KillCamera.instance.splatImage.enabled = false;
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x0000E719 File Offset: 0x0000C919
	public static void Hide()
	{
		KillCamera.instance.canvas.enabled = false;
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0008ECAC File Offset: 0x0008CEAC
	private void LateUpdate()
	{
		if (!this.splatAnimationAction.TrueDone())
		{
			float f = Mathf.Clamp01(2f * this.splatAnimationAction.Ratio() - 0.3f);
			float num = 1.5f - 0.5f * Mathf.Pow(f, 2f);
			this.killcamImage.rectTransform.localScale = new Vector3(num, num, num);
			float num2 = Mathf.Clamp01(2f * this.splatAnimationAction.Ratio() - 1f);
			float num3 = Mathf.Pow(num2, 2f);
			if (num2 < 1f)
			{
				this.splatImage.rectTransform.Rotate(new Vector3(0f, 0f, 200f * Time.deltaTime));
			}
			this.splatImage.rectTransform.localScale = new Vector3(num3, num3, num3);
		}
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x0008ED88 File Offset: 0x0008CF88
	public static void RenderKillCamera(Actor target)
	{
		Transform transform = target.transform;
		if (target.distanceCull && target.activeWeapon != null)
		{
			target.activeWeapon.Show();
		}
		if (target.IsSeated())
		{
			transform = target.seat.vehicle.transform;
		}
		Renderer[] renderers = KillCamera.GetRenderers(transform);
		int[] array = new int[renderers.Length];
		for (int i = 0; i < renderers.Length; i++)
		{
			array[i] = renderers[i].gameObject.layer;
			renderers[i].gameObject.layer = 30;
		}
		if (target.ragdoll.IsRagdoll())
		{
			transform = target.ragdoll.ragdollObject.transform;
		}
		Bounds bounds = new Bounds(transform.position, Vector3.zero);
		Collider[] componentsInChildren = transform.GetComponentsInChildren<Collider>();
		if (componentsInChildren.Length != 0)
		{
			bounds = componentsInChildren[0].bounds;
		}
		foreach (Collider collider in componentsInChildren)
		{
			bounds.Encapsulate(collider.bounds);
		}
		Vector3 normalized = (bounds.center - FpsActorController.instance.actor.CenterPosition()).normalized;
		Vector3 normalized2 = normalized.ToGround().normalized;
		Vector3 vector = (normalized2 * 2.5f + normalized).normalized;
		float num = Mathf.Max(new float[]
		{
			bounds.size.x,
			bounds.size.y,
			bounds.size.z
		}) * 2f;
		Vector3 b = new Vector3(0f, num * 0.1f, 0f);
		if (target.IsSeated() || target.ragdoll.IsRagdoll())
		{
			num *= 0.35f;
			b.y += num * 0.25f;
		}
		else if (target.parachuteDeployed)
		{
			vector = normalized2;
			vector.y = 2f;
			vector.Normalize();
			num *= 0.7f;
			b.y += 4f;
		}
		else
		{
			num *= 0.35f;
			b.y += num * 0.5f;
		}
		float d = num / Mathf.Sin(KillCamera.instance.killCamera.fieldOfView * 0.017453292f / 2f);
		Vector3 position = bounds.center + b - vector * d;
		KillCamera.instance.killCamera.transform.SetParent(null);
		KillCamera.instance.killCamera.transform.position = position;
		KillCamera.instance.killCamera.transform.rotation = Quaternion.LookRotation(vector);
		KillCamera.instance.killCamera.Render();
		if (target.distanceCull && target.activeWeapon != null)
		{
			target.activeWeapon.Hide();
		}
		for (int k = 0; k < renderers.Length; k++)
		{
			renderers[k].gameObject.layer = array[k];
		}
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x0008F0A8 File Offset: 0x0008D2A8
	private void Awake()
	{
		KillCamera.instance = this;
		this.killCamera.aspect = 1f;
		this.killCamera.useOcclusionCulling = false;
		this.canvas = base.GetComponent<Canvas>();
		int num = Mathf.NextPowerOfTwo(Screen.height / 2);
		this.renderTexture = new RenderTexture(num, num, 24, RenderTextureFormat.ARGB32);
		this.renderTexture.Create();
		this.killCamera.targetTexture = this.renderTexture;
		this.killcamImage.texture = this.renderTexture;
		KillCamera.Hide();
	}

	// Token: 0x04001380 RID: 4992
	private const int RENDER_LAYER = 30;

	// Token: 0x04001381 RID: 4993
	public static KillCamera instance;

	// Token: 0x04001382 RID: 4994
	public bool isEnabled = true;

	// Token: 0x04001383 RID: 4995
	public Camera killCamera;

	// Token: 0x04001384 RID: 4996
	public Text killerText;

	// Token: 0x04001385 RID: 4997
	public Text infoText;

	// Token: 0x04001386 RID: 4998
	public RawImage killcamImage;

	// Token: 0x04001387 RID: 4999
	public RawImage splatImage;

	// Token: 0x04001388 RID: 5000
	private TimedAction splatAnimationAction = new TimedAction(0.6f, false);

	// Token: 0x04001389 RID: 5001
	private RenderTexture renderTexture;

	// Token: 0x0400138A RID: 5002
	private Canvas canvas;
}
