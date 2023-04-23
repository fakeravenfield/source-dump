using System;
using System.IO;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class WeaponPhotoShoot : MonoBehaviour
{
	// Token: 0x06001024 RID: 4132 RVA: 0x0000CF29 File Offset: 0x0000B129
	private void Start()
	{
		this.camera = base.GetComponent<Camera>();
		this.Render();
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x00087A68 File Offset: 0x00085C68
	private void Render()
	{
		this.renderTexture = new RenderTexture(2048, 2048, 16);
		Transform transform = this.fallbackTarget;
		Weapon weapon = UnityEngine.Object.FindObjectOfType<Weapon>();
		if (weapon != null)
		{
			transform = weapon.transform;
		}
		transform.position = Vector3.zero;
		if (weapon != null && weapon.arms != null)
		{
			weapon.arms.enabled = false;
		}
		else
		{
			try
			{
				transform.Find("Arms").GetComponent<Renderer>().enabled = false;
			}
			catch (Exception)
			{
			}
		}
		if (transform == null)
		{
			Debug.LogError("No weapon target");
			return;
		}
		Animator component = transform.GetComponent<Animator>();
		if (component != null)
		{
			component.enabled = false;
		}
		Bounds bounds = default(Bounds);
		foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
		{
			if (renderer.enabled)
			{
				Material[] array = new Material[renderer.materials.Length];
				for (int j = 0; j < renderer.materials.Length; j++)
				{
					array[j] = this.material;
				}
				renderer.materials = array;
				bounds.SetMinMax(Vector3.Min(bounds.min, renderer.bounds.min), Vector3.Max(bounds.max, renderer.bounds.max));
			}
		}
		Vector3 center = bounds.center;
		center.x = -10f;
		this.camera.transform.position = center;
		Debug.Log(bounds.extents.z);
		this.camera.orthographicSize = bounds.extents.z + (0.1f + bounds.extents.z * (5f - 5f * this.sizeMultiplier));
		this.camera.aspect = 1f;
		this.camera.targetTexture = this.renderTexture;
		this.pass2Quad.material.mainTexture = this.renderTexture;
		this.camera.Render();
		this.camera.enabled = false;
		RenderTexture renderTexture;
		if (this.large)
		{
			renderTexture = new RenderTexture(1760, 608, 16, RenderTextureFormat.ARGB32);
			this.pass2Camera.orthographicSize *= 0.5f;
		}
		else
		{
			renderTexture = new RenderTexture(840, 608, 16, RenderTextureFormat.ARGB32);
			this.pass2Camera.orthographicSize *= 0.85f;
		}
		this.pass2Camera.targetTexture = renderTexture;
		this.pass2Camera.Render();
		this.pass2Camera.enabled = false;
		Graphics.SetRenderTarget(this.renderTexture);
		Graphics.SetRenderTarget(null);
		this.pass2Quad.material.mainTexture = renderTexture;
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)renderTexture.width, (float)renderTexture.height), 0, 0);
		texture2D.Apply();
		Debug.Log("Writing stream");
		byte[] array2 = new byte[]
		{
			1,
			2,
			3,
			4
		};
		File.WriteAllBytes("weapon_render.png", texture2D.EncodeToPNG());
		Debug.Log("Done!");
	}

	// Token: 0x0400110C RID: 4364
	public bool large;

	// Token: 0x0400110D RID: 4365
	[Range(0f, 1f)]
	public float sizeMultiplier = 1f;

	// Token: 0x0400110E RID: 4366
	private Camera camera;

	// Token: 0x0400110F RID: 4367
	public Camera pass2Camera;

	// Token: 0x04001110 RID: 4368
	private RenderTexture renderTexture;

	// Token: 0x04001111 RID: 4369
	public Material material;

	// Token: 0x04001112 RID: 4370
	public Renderer pass2Quad;

	// Token: 0x04001113 RID: 4371
	public Transform fallbackTarget;
}
