using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200047D RID: 1149
	public class MatAndTransformToMerged
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00015A9B File Offset: 0x00013C9B
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x00015AA3 File Offset: 0x00013CA3
		public DRect obUVRectIfTilingSame { get; private set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00015AAC File Offset: 0x00013CAC
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00015AB4 File Offset: 0x00013CB4
		public DRect samplingRectMatAndUVTiling { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00015ABD File Offset: 0x00013CBD
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x00015AC5 File Offset: 0x00013CC5
		public DRect materialTiling { get; private set; }

		// Token: 0x06001CFC RID: 7420 RVA: 0x00015ACE File Offset: 0x00013CCE
		public MatAndTransformToMerged(DRect obUVrect, bool fixOutOfBoundsUVs)
		{
			this._init(obUVrect, fixOutOfBoundsUVs, null);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00015ADF File Offset: 0x00013CDF
		public MatAndTransformToMerged(DRect obUVrect, bool fixOutOfBoundsUVs, Material m)
		{
			this._init(obUVrect, fixOutOfBoundsUVs, m);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00015AF0 File Offset: 0x00013CF0
		private void _init(DRect obUVrect, bool fixOutOfBoundsUVs, Material m)
		{
			if (fixOutOfBoundsUVs)
			{
				this.obUVRectIfTilingSame = obUVrect;
			}
			else
			{
				this.obUVRectIfTilingSame = new DRect(0f, 0f, 1f, 1f);
			}
			this.mat = m;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000BE1D4 File Offset: 0x000BC3D4
		public override bool Equals(object obj)
		{
			if (obj is MatAndTransformToMerged)
			{
				MatAndTransformToMerged matAndTransformToMerged = (MatAndTransformToMerged)obj;
				if (matAndTransformToMerged.mat == this.mat && matAndTransformToMerged.obUVRectIfTilingSame == this.obUVRectIfTilingSame)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000BE21C File Offset: 0x000BC41C
		public override int GetHashCode()
		{
			return this.mat.GetHashCode() ^ this.obUVRectIfTilingSame.GetHashCode() ^ this.samplingRectMatAndUVTiling.GetHashCode();
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00015B24 File Offset: 0x00013D24
		public string GetMaterialName()
		{
			if (this.mat != null)
			{
				return this.mat.name;
			}
			if (this.objName != null)
			{
				return string.Format("[matFor: {0}]", this.objName);
			}
			return "Unknown";
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000BE260 File Offset: 0x000BC460
		public void AssignInitialValuesForMaterialTilingAndSamplingRectMatAndUVTiling(bool allTexturesUseSameMatTiling, DRect matTiling)
		{
			if (allTexturesUseSameMatTiling)
			{
				this.materialTiling = matTiling;
			}
			else
			{
				this.materialTiling = new DRect(0f, 0f, 1f, 1f);
			}
			DRect materialTiling = this.materialTiling;
			DRect obUVRectIfTilingSame = this.obUVRectIfTilingSame;
			this.samplingRectMatAndUVTiling = MB3_UVTransformUtility.CombineTransforms(ref obUVRectIfTilingSame, ref materialTiling);
		}

		// Token: 0x04001DB9 RID: 7609
		public Material mat;

		// Token: 0x04001DBD RID: 7613
		public string objName;
	}
}
