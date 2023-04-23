using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200047F RID: 1151
	public class MB_TexSet
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00015B5E File Offset: 0x00013D5E
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x00015B66 File Offset: 0x00013D66
		public bool allTexturesUseSameMatTiling { get; private set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x00015B6F File Offset: 0x00013D6F
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x00015B77 File Offset: 0x00013D77
		public bool thisIsOnlyTexSetInAtlas { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00015B80 File Offset: 0x00013D80
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x00015B88 File Offset: 0x00013D88
		public MB_TextureTilingTreatment tilingTreatment { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00015B91 File Offset: 0x00013D91
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x00015B99 File Offset: 0x00013D99
		public Vector2 obUVoffset { get; private set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00015BA2 File Offset: 0x00013DA2
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x00015BAA File Offset: 0x00013DAA
		public Vector2 obUVscale { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00015BB3 File Offset: 0x00013DB3
		internal DRect obUVrect
		{
			get
			{
				return new DRect(this.obUVoffset, this.obUVscale);
			}
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000BE2B8 File Offset: 0x000BC4B8
		public MB_TexSet(MeshBakerMaterialTexture[] tss, Vector2 uvOffset, Vector2 uvScale, MB_TextureTilingTreatment treatment)
		{
			this.ts = tss;
			this.tilingTreatment = treatment;
			this.obUVoffset = uvOffset;
			this.obUVscale = uvScale;
			this.allTexturesUseSameMatTiling = false;
			this.thisIsOnlyTexSetInAtlas = false;
			this.matsAndGOs = new MatsAndGOs();
			this.matsAndGOs.mats = new List<MatAndTransformToMerged>();
			this.matsAndGOs.gos = new List<GameObject>();
			this.pipelineVariation = new MB_TexSet.PipelineVariationSomeTexturesUseDifferentMatTiling(this);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000BE330 File Offset: 0x000BC530
		internal bool IsEqual(object obj, bool fixOutOfBoundsUVs, MB3_TextureCombinerNonTextureProperties resultMaterialTextureBlender)
		{
			if (!(obj is MB_TexSet))
			{
				return false;
			}
			MB_TexSet mb_TexSet = (MB_TexSet)obj;
			if (mb_TexSet.ts.Length != this.ts.Length)
			{
				return false;
			}
			for (int i = 0; i < this.ts.Length; i++)
			{
				if (this.ts[i].matTilingRect != mb_TexSet.ts[i].matTilingRect)
				{
					return false;
				}
				if (!this.ts[i].AreTexturesEqual(mb_TexSet.ts[i]))
				{
					return false;
				}
				if (!resultMaterialTextureBlender.NonTexturePropertiesAreEqual(this.matsAndGOs.mats[0].mat, mb_TexSet.matsAndGOs.mats[0].mat))
				{
					return false;
				}
			}
			return (!fixOutOfBoundsUVs || (this.obUVoffset.x == mb_TexSet.obUVoffset.x && this.obUVoffset.y == mb_TexSet.obUVoffset.y)) && (!fixOutOfBoundsUVs || (this.obUVscale.x == mb_TexSet.obUVscale.x && this.obUVscale.y == mb_TexSet.obUVscale.y));
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000BE458 File Offset: 0x000BC658
		public Vector2 GetMaxRawTextureHeightWidth()
		{
			Vector2 vector = new Vector2(0f, 0f);
			for (int i = 0; i < this.ts.Length; i++)
			{
				MeshBakerMaterialTexture meshBakerMaterialTexture = this.ts[i];
				if (!meshBakerMaterialTexture.isNull)
				{
					vector.x = Mathf.Max(vector.x, (float)meshBakerMaterialTexture.width);
					vector.y = Mathf.Max(vector.y, (float)meshBakerMaterialTexture.height);
				}
			}
			return vector;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000BE4D0 File Offset: 0x000BC6D0
		private Rect GetEncapsulatingSamplingRectIfTilingSame()
		{
			if (this.ts.Length != 0)
			{
				return this.ts[0].GetEncapsulatingSamplingRect().GetRect();
			}
			return new Rect(0f, 0f, 1f, 1f);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000BE518 File Offset: 0x000BC718
		public void SetEncapsulatingSamplingRectWhenMergingTexSets(DRect newEncapsulatingSamplingRect)
		{
			for (int i = 0; i < this.ts.Length; i++)
			{
				this.ts[i].SetEncapsulatingSamplingRect(this, newEncapsulatingSamplingRect);
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00015BC6 File Offset: 0x00013DC6
		public void SetEncapsulatingSamplingRectForTesting(int propIdx, DRect newEncapsulatingSamplingRect)
		{
			this.ts[propIdx].SetEncapsulatingSamplingRect(this, newEncapsulatingSamplingRect);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000BE548 File Offset: 0x000BC748
		public void SetEncapsulatingRect(int propIdx, bool considerMeshUVs)
		{
			if (considerMeshUVs)
			{
				this.ts[propIdx].SetEncapsulatingSamplingRect(this, this.obUVrect);
				return;
			}
			this.ts[propIdx].SetEncapsulatingSamplingRect(this, new DRect(0f, 0f, 1f, 1f));
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00015BD7 File Offset: 0x00013DD7
		public void CreateColoredTexToReplaceNull(string propName, int propIdx, bool considerMeshUVs, MB3_TextureCombiner combiner, Color col)
		{
			MeshBakerMaterialTexture meshBakerMaterialTexture = this.ts[propIdx];
			meshBakerMaterialTexture.t = combiner._createTemporaryTexture(propName, 16, 16, TextureFormat.ARGB32, true);
			MB_Utility.setSolidColor(meshBakerMaterialTexture.GetTexture2D(), col);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00015C01 File Offset: 0x00013E01
		public void SetThisIsOnlyTexSetInAtlasTrue()
		{
			this.thisIsOnlyTexSetInAtlas = true;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00015C0A File Offset: 0x00013E0A
		public void SetAllTexturesUseSameMatTilingTrue()
		{
			this.allTexturesUseSameMatTiling = true;
			this.pipelineVariation = new MB_TexSet.PipelineVariationAllTexturesUseSameMatTiling(this);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00015C1F File Offset: 0x00013E1F
		public void AdjustResultMaterialNonTextureProperties(Material resultMaterial, List<ShaderTextureProperty> props)
		{
			this.pipelineVariation.AdjustResultMaterialNonTextureProperties(resultMaterial, props);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00015C2E File Offset: 0x00013E2E
		public void SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(MB_TextureTilingTreatment newTilingTreatment)
		{
			this.tilingTreatment = newTilingTreatment;
			this.pipelineVariation.SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(newTilingTreatment);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00015C43 File Offset: 0x00013E43
		internal void GetRectsForTextureBakeResults(out Rect allPropsUseSameTiling_encapsulatingSamplingRect, out Rect propsUseDifferntTiling_obUVRect)
		{
			this.pipelineVariation.GetRectsForTextureBakeResults(out allPropsUseSameTiling_encapsulatingSamplingRect, out propsUseDifferntTiling_obUVRect);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00015C52 File Offset: 0x00013E52
		internal Rect GetMaterialTilingRectForTextureBakerResults(int materialIndex)
		{
			return this.pipelineVariation.GetMaterialTilingRectForTextureBakerResults(materialIndex);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000BE594 File Offset: 0x000BC794
		internal void CalcInitialFullSamplingRects(bool fixOutOfBoundsUVs)
		{
			DRect r = new DRect(0f, 0f, 1f, 1f);
			if (fixOutOfBoundsUVs)
			{
				r = this.obUVrect;
			}
			for (int i = 0; i < this.ts.Length; i++)
			{
				if (!this.ts[i].isNull)
				{
					DRect matTilingRect = this.ts[i].matTilingRect;
					DRect obUVrect;
					if (fixOutOfBoundsUVs)
					{
						obUVrect = this.obUVrect;
					}
					else
					{
						obUVrect = new DRect(0.0, 0.0, 1.0, 1.0);
					}
					this.ts[i].SetEncapsulatingSamplingRect(this, MB3_UVTransformUtility.CombineTransforms(ref obUVrect, ref matTilingRect));
					r = this.ts[i].GetEncapsulatingSamplingRect();
				}
			}
			for (int j = 0; j < this.ts.Length; j++)
			{
				if (this.ts[j].isNull)
				{
					this.ts[j].SetEncapsulatingSamplingRect(this, r);
				}
			}
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000BE68C File Offset: 0x000BC88C
		internal void CalcMatAndUVSamplingRects()
		{
			DRect matTilingRect = new DRect(0f, 0f, 1f, 1f);
			if (this.allTexturesUseSameMatTiling)
			{
				for (int i = 0; i < this.ts.Length; i++)
				{
					if (!this.ts[i].isNull)
					{
						matTilingRect = this.ts[i].matTilingRect;
						break;
					}
				}
			}
			for (int j = 0; j < this.matsAndGOs.mats.Count; j++)
			{
				this.matsAndGOs.mats[j].AssignInitialValuesForMaterialTilingAndSamplingRectMatAndUVTiling(this.allTexturesUseSameMatTiling, matTilingRect);
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000BE728 File Offset: 0x000BC928
		public bool AllTexturesAreSameForMerge(MB_TexSet other, bool considerNonTextureProperties, MB3_TextureCombinerNonTextureProperties resultMaterialTextureBlender)
		{
			if (other.ts.Length != this.ts.Length)
			{
				return false;
			}
			if (!other.allTexturesUseSameMatTiling || !this.allTexturesUseSameMatTiling)
			{
				return false;
			}
			int num = -1;
			for (int i = 0; i < this.ts.Length; i++)
			{
				if (!this.ts[i].AreTexturesEqual(other.ts[i]))
				{
					return false;
				}
				if (num == -1 && !this.ts[i].isNull)
				{
					num = i;
				}
				if (considerNonTextureProperties && !resultMaterialTextureBlender.NonTexturePropertiesAreEqual(this.matsAndGOs.mats[0].mat, other.matsAndGOs.mats[0].mat))
				{
					return false;
				}
			}
			if (num != -1)
			{
				for (int j = 0; j < this.ts.Length; j++)
				{
					if (!this.ts[j].AreTexturesEqual(other.ts[j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000BE808 File Offset: 0x000BCA08
		public void DrawRectsToMergeGizmos(Color encC, Color innerC)
		{
			DRect encapsulatingSamplingRect = this.ts[0].GetEncapsulatingSamplingRect();
			encapsulatingSamplingRect.Expand(0.05f);
			Gizmos.color = encC;
			Gizmos.DrawWireCube(encapsulatingSamplingRect.center.GetVector2(), encapsulatingSamplingRect.size);
			for (int i = 0; i < this.matsAndGOs.mats.Count; i++)
			{
				DRect samplingRectMatAndUVTiling = this.matsAndGOs.mats[i].samplingRectMatAndUVTiling;
				DRect shiftTransformToFitBinA = MB3_UVTransformUtility.GetShiftTransformToFitBinA(ref encapsulatingSamplingRect, ref samplingRectMatAndUVTiling);
				Vector2 vector = MB3_UVTransformUtility.TransformPoint(ref shiftTransformToFitBinA, samplingRectMatAndUVTiling.min);
				samplingRectMatAndUVTiling.x = (double)vector.x;
				samplingRectMatAndUVTiling.y = (double)vector.y;
				Gizmos.color = innerC;
				Gizmos.DrawWireCube(samplingRectMatAndUVTiling.center.GetVector2(), samplingRectMatAndUVTiling.size);
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000BE8F4 File Offset: 0x000BCAF4
		internal string GetDescription()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[GAME_OBJS=", Array.Empty<object>());
			for (int i = 0; i < this.matsAndGOs.gos.Count; i++)
			{
				stringBuilder.AppendFormat("{0},", this.matsAndGOs.gos[i].name);
			}
			stringBuilder.AppendFormat("MATS=", Array.Empty<object>());
			for (int j = 0; j < this.matsAndGOs.mats.Count; j++)
			{
				stringBuilder.AppendFormat("{0},", this.matsAndGOs.mats[j].GetMaterialName());
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000BE9B8 File Offset: 0x000BCBB8
		internal string GetMatSubrectDescriptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.matsAndGOs.mats.Count; i++)
			{
				stringBuilder.AppendFormat("\n    {0}={1},", this.matsAndGOs.mats[i].GetMaterialName(), this.matsAndGOs.mats[i].samplingRectMatAndUVTiling);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001DC0 RID: 7616
		public MeshBakerMaterialTexture[] ts;

		// Token: 0x04001DC1 RID: 7617
		public MatsAndGOs matsAndGOs;

		// Token: 0x04001DC7 RID: 7623
		public int idealWidth;

		// Token: 0x04001DC8 RID: 7624
		public int idealHeight;

		// Token: 0x04001DC9 RID: 7625
		private MB_TexSet.PipelineVariation pipelineVariation;

		// Token: 0x02000480 RID: 1152
		private interface PipelineVariation
		{
			// Token: 0x06001D23 RID: 7459
			void GetRectsForTextureBakeResults(out Rect allPropsUseSameTiling_encapsulatingSamplingRect, out Rect propsUseDifferntTiling_obUVRect);

			// Token: 0x06001D24 RID: 7460
			void SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(MB_TextureTilingTreatment newTilingTreatment);

			// Token: 0x06001D25 RID: 7461
			Rect GetMaterialTilingRectForTextureBakerResults(int materialIndex);

			// Token: 0x06001D26 RID: 7462
			void AdjustResultMaterialNonTextureProperties(Material resultMaterial, List<ShaderTextureProperty> props);
		}

		// Token: 0x02000481 RID: 1153
		private class PipelineVariationAllTexturesUseSameMatTiling : MB_TexSet.PipelineVariation
		{
			// Token: 0x06001D27 RID: 7463 RVA: 0x00015C60 File Offset: 0x00013E60
			public PipelineVariationAllTexturesUseSameMatTiling(MB_TexSet ts)
			{
				this.texSet = ts;
			}

			// Token: 0x06001D28 RID: 7464 RVA: 0x000BEA2C File Offset: 0x000BCC2C
			public void GetRectsForTextureBakeResults(out Rect allPropsUseSameTiling_encapsulatingSamplingRect, out Rect propsUseDifferntTiling_obUVRect)
			{
				propsUseDifferntTiling_obUVRect = new Rect(0f, 0f, 0f, 0f);
				allPropsUseSameTiling_encapsulatingSamplingRect = this.texSet.GetEncapsulatingSamplingRectIfTilingSame();
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeX)
				{
					allPropsUseSameTiling_encapsulatingSamplingRect.x = 0f;
					allPropsUseSameTiling_encapsulatingSamplingRect.width = 1f;
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeY)
				{
					allPropsUseSameTiling_encapsulatingSamplingRect.y = 0f;
					allPropsUseSameTiling_encapsulatingSamplingRect.height = 1f;
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeXY)
				{
					allPropsUseSameTiling_encapsulatingSamplingRect = new Rect(0f, 0f, 1f, 1f);
				}
			}

			// Token: 0x06001D29 RID: 7465 RVA: 0x000BEAE0 File Offset: 0x000BCCE0
			public void SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(MB_TextureTilingTreatment newTilingTreatment)
			{
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeX)
				{
					foreach (MeshBakerMaterialTexture meshBakerMaterialTexture in this.texSet.ts)
					{
						DRect encapsulatingSamplingRect = meshBakerMaterialTexture.GetEncapsulatingSamplingRect();
						encapsulatingSamplingRect.width = 1.0;
						meshBakerMaterialTexture.SetEncapsulatingSamplingRect(this.texSet, encapsulatingSamplingRect);
					}
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeY)
				{
					foreach (MeshBakerMaterialTexture meshBakerMaterialTexture2 in this.texSet.ts)
					{
						DRect encapsulatingSamplingRect2 = meshBakerMaterialTexture2.GetEncapsulatingSamplingRect();
						encapsulatingSamplingRect2.height = 1.0;
						meshBakerMaterialTexture2.SetEncapsulatingSamplingRect(this.texSet, encapsulatingSamplingRect2);
					}
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeXY)
				{
					foreach (MeshBakerMaterialTexture meshBakerMaterialTexture3 in this.texSet.ts)
					{
						DRect encapsulatingSamplingRect3 = meshBakerMaterialTexture3.GetEncapsulatingSamplingRect();
						encapsulatingSamplingRect3.height = 1.0;
						encapsulatingSamplingRect3.width = 1.0;
						meshBakerMaterialTexture3.SetEncapsulatingSamplingRect(this.texSet, encapsulatingSamplingRect3);
					}
				}
			}

			// Token: 0x06001D2A RID: 7466 RVA: 0x000BEBEC File Offset: 0x000BCDEC
			public Rect GetMaterialTilingRectForTextureBakerResults(int materialIndex)
			{
				return this.texSet.matsAndGOs.mats[materialIndex].materialTiling.GetRect();
			}

			// Token: 0x06001D2B RID: 7467 RVA: 0x0000296E File Offset: 0x00000B6E
			public void AdjustResultMaterialNonTextureProperties(Material resultMaterial, List<ShaderTextureProperty> props)
			{
			}

			// Token: 0x04001DCA RID: 7626
			private MB_TexSet texSet;
		}

		// Token: 0x02000482 RID: 1154
		private class PipelineVariationSomeTexturesUseDifferentMatTiling : MB_TexSet.PipelineVariation
		{
			// Token: 0x06001D2C RID: 7468 RVA: 0x00015C6F File Offset: 0x00013E6F
			public PipelineVariationSomeTexturesUseDifferentMatTiling(MB_TexSet ts)
			{
				this.texSet = ts;
			}

			// Token: 0x06001D2D RID: 7469 RVA: 0x000BEC1C File Offset: 0x000BCE1C
			public void GetRectsForTextureBakeResults(out Rect allPropsUseSameTiling_encapsulatingSamplingRect, out Rect propsUseDifferntTiling_obUVRect)
			{
				allPropsUseSameTiling_encapsulatingSamplingRect = new Rect(0f, 0f, 0f, 0f);
				propsUseDifferntTiling_obUVRect = this.texSet.obUVrect.GetRect();
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeX)
				{
					propsUseDifferntTiling_obUVRect.x = 0f;
					propsUseDifferntTiling_obUVRect.width = 1f;
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeY)
				{
					propsUseDifferntTiling_obUVRect.y = 0f;
					propsUseDifferntTiling_obUVRect.height = 1f;
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeXY)
				{
					propsUseDifferntTiling_obUVRect = new Rect(0f, 0f, 1f, 1f);
				}
			}

			// Token: 0x06001D2E RID: 7470 RVA: 0x000BECD8 File Offset: 0x000BCED8
			public void SetTilingTreatmentAndAdjustEncapsulatingSamplingRect(MB_TextureTilingTreatment newTilingTreatment)
			{
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeX)
				{
					foreach (MeshBakerMaterialTexture meshBakerMaterialTexture in this.texSet.ts)
					{
						DRect encapsulatingSamplingRect = meshBakerMaterialTexture.GetEncapsulatingSamplingRect();
						encapsulatingSamplingRect.width = 1.0;
						meshBakerMaterialTexture.SetEncapsulatingSamplingRect(this.texSet, encapsulatingSamplingRect);
					}
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeY)
				{
					foreach (MeshBakerMaterialTexture meshBakerMaterialTexture2 in this.texSet.ts)
					{
						DRect encapsulatingSamplingRect2 = meshBakerMaterialTexture2.GetEncapsulatingSamplingRect();
						encapsulatingSamplingRect2.height = 1.0;
						meshBakerMaterialTexture2.SetEncapsulatingSamplingRect(this.texSet, encapsulatingSamplingRect2);
					}
					return;
				}
				if (this.texSet.tilingTreatment == MB_TextureTilingTreatment.edgeToEdgeXY)
				{
					foreach (MeshBakerMaterialTexture meshBakerMaterialTexture3 in this.texSet.ts)
					{
						DRect encapsulatingSamplingRect3 = meshBakerMaterialTexture3.GetEncapsulatingSamplingRect();
						encapsulatingSamplingRect3.height = 1.0;
						encapsulatingSamplingRect3.width = 1.0;
						meshBakerMaterialTexture3.SetEncapsulatingSamplingRect(this.texSet, encapsulatingSamplingRect3);
					}
				}
			}

			// Token: 0x06001D2F RID: 7471 RVA: 0x00015C7E File Offset: 0x00013E7E
			public Rect GetMaterialTilingRectForTextureBakerResults(int materialIndex)
			{
				return new Rect(0f, 0f, 0f, 0f);
			}

			// Token: 0x06001D30 RID: 7472 RVA: 0x000BEDE4 File Offset: 0x000BCFE4
			public void AdjustResultMaterialNonTextureProperties(Material resultMaterial, List<ShaderTextureProperty> props)
			{
				if (this.texSet.thisIsOnlyTexSetInAtlas)
				{
					for (int i = 0; i < props.Count; i++)
					{
						if (resultMaterial.HasProperty(props[i].name))
						{
							resultMaterial.SetTextureOffset(props[i].name, this.texSet.ts[i].matTilingRect.min);
							resultMaterial.SetTextureScale(props[i].name, this.texSet.ts[i].matTilingRect.size);
						}
					}
				}
			}

			// Token: 0x04001DCB RID: 7627
			private MB_TexSet texSet;
		}
	}
}
