using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000364 RID: 868
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Rendering/Cubemap Global Fog")]
	internal class CubemapGlobalFog : PostEffectsBase
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x0001175B File Offset: 0x0000F95B
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.fogMaterial = base.CheckShaderAndCreateMaterial(this.fogShader, this.fogMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0009F784 File Offset: 0x0009D984
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources() || (!this.distanceFog && !this.heightFog))
			{
				Graphics.Blit(source, destination);
				return;
			}
			Camera component = base.GetComponent<Camera>();
			Transform transform = component.transform;
			float nearClipPlane = component.nearClipPlane;
			float farClipPlane = component.farClipPlane;
			float fieldOfView = component.fieldOfView;
			float aspect = component.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			float num = fieldOfView * 0.5f;
			Vector3 b = transform.right * nearClipPlane * Mathf.Tan(num * 0.017453292f) * aspect;
			Vector3 b2 = transform.up * nearClipPlane * Mathf.Tan(num * 0.017453292f);
			Vector3 vector = transform.forward * nearClipPlane - b + b2;
			float d = vector.magnitude * farClipPlane / nearClipPlane;
			vector.Normalize();
			vector *= d;
			Vector3 vector2 = transform.forward * nearClipPlane + b + b2;
			vector2.Normalize();
			vector2 *= d;
			Vector3 vector3 = transform.forward * nearClipPlane + b - b2;
			vector3.Normalize();
			vector3 *= d;
			Vector3 vector4 = transform.forward * nearClipPlane - b - b2;
			vector4.Normalize();
			vector4 *= d;
			identity.SetRow(0, vector);
			identity.SetRow(1, vector2);
			identity.SetRow(2, vector3);
			identity.SetRow(3, vector4);
			Vector3 position = transform.position;
			float num2 = position.y - this.height;
			float z = (num2 <= 0f) ? 1f : 0f;
			this.fogMaterial.SetMatrix("_FrustumCornersWS", identity);
			this.fogMaterial.SetVector("_CameraWS", position);
			this.fogMaterial.SetVector("_HeightParams", new Vector4(this.height, num2, z, this.heightDensity * 0.5f));
			this.fogMaterial.SetVector("_DistanceParams", new Vector4(-Mathf.Max(this.startDistance, 0f), 0f, 0f, 0f));
			FogMode fogMode = RenderSettings.fogMode;
			float fogDensity = RenderSettings.fogDensity;
			float fogStartDistance = RenderSettings.fogStartDistance;
			float fogEndDistance = RenderSettings.fogEndDistance;
			bool flag = fogMode == FogMode.Linear;
			float num3 = flag ? (fogEndDistance - fogStartDistance) : 0f;
			float num4 = (Mathf.Abs(num3) > 0.0001f) ? (1f / num3) : 0f;
			Vector4 value;
			value.x = fogDensity * 1.2011224f;
			value.y = fogDensity * 1.442695f;
			value.z = (flag ? (-num4) : 0f);
			value.w = (flag ? (fogEndDistance * num4) : 0f);
			this.fogMaterial.SetVector("_SceneFogParams", value);
			this.fogMaterial.SetVector("_SceneFogMode", new Vector4((float)fogMode, (float)(this.useRadialDistance ? 1 : 0), 0f, 0f));
			this.fogMaterial.SetTexture("_Skybox", this.skyboxCubemap);
			int passNr;
			if (this.distanceFog && this.heightFog)
			{
				passNr = 0;
			}
			else if (this.distanceFog)
			{
				passNr = 1;
			}
			else
			{
				passNr = 2;
			}
			CubemapGlobalFog.CustomGraphicsBlit(source, destination, this.fogMaterial, passNr);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0009FB18 File Offset: 0x0009DD18
		private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
		{
			RenderTexture.active = dest;
			fxMaterial.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			fxMaterial.SetPass(passNr);
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 3f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 2f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x04001889 RID: 6281
		[Tooltip("Apply distance-based fog?")]
		public bool distanceFog = true;

		// Token: 0x0400188A RID: 6282
		[Tooltip("Distance fog is based on radial distance from camera when checked")]
		public bool useRadialDistance;

		// Token: 0x0400188B RID: 6283
		[Tooltip("Apply height-based fog?")]
		public bool heightFog = true;

		// Token: 0x0400188C RID: 6284
		[Tooltip("Fog top Y coordinate")]
		public float height = 1f;

		// Token: 0x0400188D RID: 6285
		[Range(0.001f, 10f)]
		public float heightDensity = 2f;

		// Token: 0x0400188E RID: 6286
		[Tooltip("Push fog away from the camera by this amount")]
		public float startDistance;

		// Token: 0x0400188F RID: 6287
		public Shader fogShader;

		// Token: 0x04001890 RID: 6288
		private Material fogMaterial;

		// Token: 0x04001891 RID: 6289
		public Cubemap skyboxCubemap;
	}
}
