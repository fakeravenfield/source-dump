using System;
using DigitalOpus.MB.Core;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000038 RID: 56
[Serializable]
public class MB_MaterialAndUVRect
{
	// Token: 0x060000F1 RID: 241 RVA: 0x00040A84 File Offset: 0x0003EC84
	public MB_MaterialAndUVRect(Material mat, Rect destRect, bool allPropsUseSameTiling, Rect sourceMaterialTiling, Rect samplingEncapsulatingRect, Rect srcUVsamplingRect, MB_TextureTilingTreatment treatment, string objName)
	{
		this.material = mat;
		this.atlasRect = destRect;
		this.tilingTreatment = treatment;
		this.allPropsUseSameTiling = allPropsUseSameTiling;
		this.allPropsUseSameTiling_sourceMaterialTiling = sourceMaterialTiling;
		this.allPropsUseSameTiling_samplingEncapsulatinRect = samplingEncapsulatingRect;
		this.propsUseDifferntTiling_srcUVsamplingRect = srcUVsamplingRect;
		this.srcObjName = objName;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00002CE8 File Offset: 0x00000EE8
	public override int GetHashCode()
	{
		return this.material.GetInstanceID() ^ this.allPropsUseSameTiling_samplingEncapsulatinRect.GetHashCode() ^ this.propsUseDifferntTiling_srcUVsamplingRect.GetHashCode();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00040AE8 File Offset: 0x0003ECE8
	public override bool Equals(object obj)
	{
		if (!(obj is MB_MaterialAndUVRect))
		{
			return false;
		}
		MB_MaterialAndUVRect mb_MaterialAndUVRect = (MB_MaterialAndUVRect)obj;
		return this.material == mb_MaterialAndUVRect.material && this.allPropsUseSameTiling_samplingEncapsulatinRect == mb_MaterialAndUVRect.allPropsUseSameTiling_samplingEncapsulatinRect && this.allPropsUseSameTiling_sourceMaterialTiling == mb_MaterialAndUVRect.allPropsUseSameTiling_sourceMaterialTiling && this.allPropsUseSameTiling == mb_MaterialAndUVRect.allPropsUseSameTiling && this.propsUseDifferntTiling_srcUVsamplingRect == mb_MaterialAndUVRect.propsUseDifferntTiling_srcUVsamplingRect;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00002D19 File Offset: 0x00000F19
	public Rect GetEncapsulatingRect()
	{
		if (this.allPropsUseSameTiling)
		{
			return this.allPropsUseSameTiling_samplingEncapsulatinRect;
		}
		return this.propsUseDifferntTiling_srcUVsamplingRect;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00002D30 File Offset: 0x00000F30
	public Rect GetMaterialTilingRect()
	{
		if (this.allPropsUseSameTiling)
		{
			return this.allPropsUseSameTiling_sourceMaterialTiling;
		}
		return new Rect(0f, 0f, 1f, 1f);
	}

	// Token: 0x0400009F RID: 159
	public Material material;

	// Token: 0x040000A0 RID: 160
	public Rect atlasRect;

	// Token: 0x040000A1 RID: 161
	public string srcObjName;

	// Token: 0x040000A2 RID: 162
	public bool allPropsUseSameTiling = true;

	// Token: 0x040000A3 RID: 163
	[FormerlySerializedAs("sourceMaterialTiling")]
	public Rect allPropsUseSameTiling_sourceMaterialTiling;

	// Token: 0x040000A4 RID: 164
	[FormerlySerializedAs("samplingEncapsulatinRect")]
	public Rect allPropsUseSameTiling_samplingEncapsulatinRect;

	// Token: 0x040000A5 RID: 165
	public Rect propsUseDifferntTiling_srcUVsamplingRect;

	// Token: 0x040000A6 RID: 166
	public MB_TextureTilingTreatment tilingTreatment = MB_TextureTilingTreatment.unknown;
}
