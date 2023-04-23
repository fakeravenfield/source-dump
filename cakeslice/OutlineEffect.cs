using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace cakeslice
{
	// Token: 0x02000773 RID: 1907
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public class OutlineEffect : MonoBehaviour
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x00020544 File Offset: 0x0001E744
		public static OutlineEffect Instance
		{
			get
			{
				if (object.Equals(OutlineEffect.m_instance, null))
				{
					return OutlineEffect.m_instance = (UnityEngine.Object.FindObjectOfType(typeof(OutlineEffect)) as OutlineEffect);
				}
				return OutlineEffect.m_instance;
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0010A5E0 File Offset: 0x001087E0
		private OutlineEffect()
		{
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x00020573 File Offset: 0x0001E773
		private Material GetMaterialFromID(int ID)
		{
			if (ID == 0)
			{
				return this.outline1Material;
			}
			if (ID == 1)
			{
				return this.outline2Material;
			}
			return this.outline3Material;
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0010A65C File Offset: 0x0010885C
		private Material CreateMaterial(Color emissionColor)
		{
			Material material = new Material(this.outlineBufferShader);
			material.SetColor("_Color", emissionColor);
			material.SetInt("_SrcBlend", 5);
			material.SetInt("_DstBlend", 10);
			material.SetInt("_ZWrite", 0);
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 3000;
			return material;
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x00020590 File Offset: 0x0001E790
		private void Awake()
		{
			OutlineEffect.m_instance = this;
			base.enabled = false;
			this.commandBuffer = new CommandBuffer();
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0010A6D4 File Offset: 0x001088D4
		public void SetSourceCamera(Camera camera)
		{
			if (camera == null)
			{
				throw new ArgumentNullException("camera");
			}
			this.sourceCamera = camera;
			if (this.outlineCamera == null)
			{
				GameObject gameObject = new GameObject("Outline Camera");
				this.outlineCamera = gameObject.AddComponent<Camera>();
				this.outlineCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.commandBuffer);
				this.outlineCamera.enabled = false;
				this.CreateMaterialsIfNeeded();
				this.UpdateOutlineCameraFromSource();
				this.UpdateMaterialsPublicProperties();
				base.enabled = true;
			}
			this.outlineCamera.transform.parent = this.sourceCamera.transform;
			this.outlineCamera.transform.localPosition = Vector3.zero;
			this.outlineCamera.transform.localRotation = Quaternion.identity;
			this.UpdateOutlineCameraFromSource();
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0010A7A4 File Offset: 0x001089A4
		public void OnPreRender()
		{
			if (this.commandBuffer == null || this.outlineCamera == null || this.sourceCamera == null)
			{
				return;
			}
			this.CreateMaterialsIfNeeded();
			if (this.renderTexture == null || this.renderTexture.width != this.sourceCamera.pixelWidth || this.renderTexture.height != this.sourceCamera.pixelHeight)
			{
				this.renderTexture = new RenderTexture(this.sourceCamera.pixelWidth, this.sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
				this.extraRenderTexture = new RenderTexture(this.sourceCamera.pixelWidth, this.sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
				this.outlineCamera.targetTexture = this.renderTexture;
			}
			this.UpdateMaterialsPublicProperties();
			this.UpdateOutlineCameraFromSource();
			this.outlineCamera.targetTexture = this.renderTexture;
			this.commandBuffer.SetRenderTarget(this.renderTexture);
			this.commandBuffer.Clear();
			foreach (Outline outline in OutlineEffect.outlines)
			{
				LayerMask mask = this.sourceCamera.cullingMask;
				if (outline != null && mask == (mask | 1 << outline.originalLayer))
				{
					foreach (Renderer renderer in outline.GetRenderers())
					{
						Material materialFromID;
						if (outline.eraseRenderer)
						{
							materialFromID = this.outlineEraseMaterial;
						}
						else
						{
							materialFromID = this.GetMaterialFromID(outline.color);
						}
						if (this.backfaceCulling)
						{
							materialFromID.SetInt("_Culling", 2);
						}
						else
						{
							materialFromID.SetInt("_Culling", 0);
						}
						this.commandBuffer.DrawRenderer(renderer, materialFromID, 0, 0);
						MeshFilter component = renderer.GetComponent<MeshFilter>();
						if (component && component.sharedMesh)
						{
							for (int j = 1; j < component.sharedMesh.subMeshCount; j++)
							{
								this.commandBuffer.DrawRenderer(renderer, materialFromID, j, 0);
							}
						}
						SkinnedMeshRenderer component2 = renderer.GetComponent<SkinnedMeshRenderer>();
						if (component2 && component2.sharedMesh)
						{
							for (int k = 1; k < component2.sharedMesh.subMeshCount; k++)
							{
								this.commandBuffer.DrawRenderer(renderer, materialFromID, k, 0);
							}
						}
					}
				}
			}
			this.outlineCamera.Render();
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000205AA File Offset: 0x0001E7AA
		private void OnDestroy()
		{
			if (this.renderTexture != null)
			{
				this.renderTexture.Release();
			}
			if (this.extraRenderTexture != null)
			{
				this.extraRenderTexture.Release();
			}
			this.DestroyMaterials();
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0010AA60 File Offset: 0x00108C60
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			this.outlineShaderMaterial.SetTexture("_OutlineSource", this.renderTexture);
			if (this.addLinesBetweenColors)
			{
				Graphics.Blit(source, this.extraRenderTexture, this.outlineShaderMaterial, 0);
				this.outlineShaderMaterial.SetTexture("_OutlineSource", this.extraRenderTexture);
			}
			Graphics.Blit(source, destination, this.outlineShaderMaterial, 1);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0010AAC4 File Offset: 0x00108CC4
		private void CreateMaterialsIfNeeded()
		{
			if (this.outlineShader == null)
			{
				this.outlineShader = Resources.Load<Shader>("OutlineShader");
			}
			if (this.outlineBufferShader == null)
			{
				this.outlineBufferShader = Resources.Load<Shader>("OutlineBufferShader");
			}
			if (this.outlineShaderMaterial == null)
			{
				this.outlineShaderMaterial = new Material(this.outlineShader);
				this.outlineShaderMaterial.hideFlags = HideFlags.HideAndDontSave;
				this.UpdateMaterialsPublicProperties();
			}
			if (this.outlineEraseMaterial == null)
			{
				this.outlineEraseMaterial = this.CreateMaterial(new Color(0f, 0f, 0f, 0f));
			}
			if (this.outline1Material == null)
			{
				this.outline1Material = this.CreateMaterial(new Color(1f, 0f, 0f, 0f));
			}
			if (this.outline2Material == null)
			{
				this.outline2Material = this.CreateMaterial(new Color(0f, 1f, 0f, 0f));
			}
			if (this.outline3Material == null)
			{
				this.outline3Material = this.CreateMaterial(new Color(0f, 0f, 1f, 0f));
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0010AC0C File Offset: 0x00108E0C
		private void DestroyMaterials()
		{
			foreach (Material obj in this.materialBuffer)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			this.materialBuffer.Clear();
			UnityEngine.Object.DestroyImmediate(this.outlineShaderMaterial);
			UnityEngine.Object.DestroyImmediate(this.outlineEraseMaterial);
			UnityEngine.Object.DestroyImmediate(this.outline1Material);
			UnityEngine.Object.DestroyImmediate(this.outline2Material);
			UnityEngine.Object.DestroyImmediate(this.outline3Material);
			this.outlineShader = null;
			this.outlineBufferShader = null;
			this.outlineShaderMaterial = null;
			this.outlineEraseMaterial = null;
			this.outline1Material = null;
			this.outline2Material = null;
			this.outline3Material = null;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0010ACD0 File Offset: 0x00108ED0
		public void UpdateMaterialsPublicProperties()
		{
			if (this.outlineShaderMaterial)
			{
				float num = 1f;
				if (this.scaleWithScreenSize)
				{
					num = (float)Screen.height / 360f;
				}
				if (this.scaleWithScreenSize && num < 1f)
				{
					this.outlineShaderMaterial.SetFloat("_LineThicknessX", 0.001f * (1f / (float)Screen.width) * 1000f);
					this.outlineShaderMaterial.SetFloat("_LineThicknessY", 0.001f * (1f / (float)Screen.height) * 1000f);
				}
				else
				{
					this.outlineShaderMaterial.SetFloat("_LineThicknessX", num * (this.lineThickness / 1000f) * (1f / (float)Screen.width) * 1000f);
					this.outlineShaderMaterial.SetFloat("_LineThicknessY", num * (this.lineThickness / 1000f) * (1f / (float)Screen.height) * 1000f);
				}
				this.outlineShaderMaterial.SetFloat("_LineIntensity", this.lineIntensity);
				this.outlineShaderMaterial.SetFloat("_FillAmount", this.fillAmount);
				this.outlineShaderMaterial.SetColor("_LineColor1", this.lineColor0 * this.lineColor0);
				this.outlineShaderMaterial.SetColor("_LineColor2", this.lineColor1 * this.lineColor1);
				this.outlineShaderMaterial.SetColor("_LineColor3", this.lineColor2 * this.lineColor2);
				if (this.flipY)
				{
					this.outlineShaderMaterial.SetInt("_FlipY", 1);
				}
				else
				{
					this.outlineShaderMaterial.SetInt("_FlipY", 0);
				}
				if (!this.additiveRendering)
				{
					this.outlineShaderMaterial.SetInt("_Dark", 1);
				}
				else
				{
					this.outlineShaderMaterial.SetInt("_Dark", 0);
				}
				if (this.cornerOutlines)
				{
					this.outlineShaderMaterial.SetInt("_CornerOutlines", 1);
				}
				else
				{
					this.outlineShaderMaterial.SetInt("_CornerOutlines", 0);
				}
				Shader.SetGlobalFloat("_OutlineAlphaCutoff", this.alphaCutoff);
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0010AEF0 File Offset: 0x001090F0
		private void UpdateOutlineCameraFromSource()
		{
			this.outlineCamera.CopyFrom(this.sourceCamera);
			this.outlineCamera.renderingPath = RenderingPath.Forward;
			this.outlineCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			this.outlineCamera.clearFlags = CameraClearFlags.Color;
			this.outlineCamera.rect = new Rect(0f, 0f, 1f, 1f);
			this.outlineCamera.cullingMask = 0;
			this.outlineCamera.targetTexture = this.renderTexture;
			this.outlineCamera.enabled = false;
			this.outlineCamera.allowHDR = false;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000205E4 File Offset: 0x0001E7E4
		public static void AddOutline(Outline outline)
		{
			if (!OutlineEffect.outlines.Contains(outline))
			{
				OutlineEffect.outlines.Add(outline);
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000205FE File Offset: 0x0001E7FE
		public static void RemoveOutline(Outline outline)
		{
			if (OutlineEffect.outlines.Contains(outline))
			{
				OutlineEffect.outlines.Remove(outline);
			}
		}

		// Token: 0x04002B1C RID: 11036
		private static OutlineEffect m_instance;

		// Token: 0x04002B1D RID: 11037
		private static readonly List<Outline> outlines = new List<Outline>();

		// Token: 0x04002B1E RID: 11038
		[Range(1f, 6f)]
		public float lineThickness = 1.25f;

		// Token: 0x04002B1F RID: 11039
		[Range(0f, 10f)]
		public float lineIntensity = 0.5f;

		// Token: 0x04002B20 RID: 11040
		[Range(0f, 1f)]
		public float fillAmount = 0.2f;

		// Token: 0x04002B21 RID: 11041
		public Color lineColor0 = Color.red;

		// Token: 0x04002B22 RID: 11042
		public Color lineColor1 = Color.green;

		// Token: 0x04002B23 RID: 11043
		public Color lineColor2 = Color.blue;

		// Token: 0x04002B24 RID: 11044
		public bool additiveRendering;

		// Token: 0x04002B25 RID: 11045
		public bool backfaceCulling = true;

		// Token: 0x04002B26 RID: 11046
		[Header("These settings can affect performance!")]
		public bool cornerOutlines;

		// Token: 0x04002B27 RID: 11047
		public bool addLinesBetweenColors;

		// Token: 0x04002B28 RID: 11048
		[Header("Advanced settings")]
		public bool scaleWithScreenSize = true;

		// Token: 0x04002B29 RID: 11049
		[Range(0.1f, 0.9f)]
		public float alphaCutoff = 0.5f;

		// Token: 0x04002B2A RID: 11050
		public bool flipY;

		// Token: 0x04002B2B RID: 11051
		public Camera sourceCamera;

		// Token: 0x04002B2C RID: 11052
		[HideInInspector]
		public Camera outlineCamera;

		// Token: 0x04002B2D RID: 11053
		private Material outline1Material;

		// Token: 0x04002B2E RID: 11054
		private Material outline2Material;

		// Token: 0x04002B2F RID: 11055
		private Material outline3Material;

		// Token: 0x04002B30 RID: 11056
		private Material outlineEraseMaterial;

		// Token: 0x04002B31 RID: 11057
		private Shader outlineShader;

		// Token: 0x04002B32 RID: 11058
		private Shader outlineBufferShader;

		// Token: 0x04002B33 RID: 11059
		[HideInInspector]
		[NonSerialized]
		public Material outlineShaderMaterial;

		// Token: 0x04002B34 RID: 11060
		[HideInInspector]
		[NonSerialized]
		public RenderTexture renderTexture;

		// Token: 0x04002B35 RID: 11061
		[HideInInspector]
		[NonSerialized]
		public RenderTexture extraRenderTexture;

		// Token: 0x04002B36 RID: 11062
		private CommandBuffer commandBuffer;

		// Token: 0x04002B37 RID: 11063
		private List<Material> materialBuffer = new List<Material>();
	}
}
