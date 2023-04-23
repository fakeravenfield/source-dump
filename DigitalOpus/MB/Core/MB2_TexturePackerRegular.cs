using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200046C RID: 1132
	public class MB2_TexturePackerRegular : MB2_TexturePacker
	{
		// Token: 0x06001C6C RID: 7276 RVA: 0x000BA364 File Offset: 0x000B8564
		private static void printTree(MB2_TexturePackerRegular.Node r, string spc)
		{
			string[] array = new string[5];
			array[0] = spc;
			array[1] = "Nd img=";
			array[2] = (r.img != null).ToString();
			array[3] = " r=";
			int num = 4;
			MB2_TexturePacker.PixRect r2 = r.r;
			array[num] = ((r2 != null) ? r2.ToString() : null);
			Debug.Log(string.Concat(array));
			if (r.child[0] != null)
			{
				MB2_TexturePackerRegular.printTree(r.child[0], spc + "      ");
			}
			if (r.child[1] != null)
			{
				MB2_TexturePackerRegular.printTree(r.child[1], spc + "      ");
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000BA404 File Offset: 0x000B8604
		private static void flattenTree(MB2_TexturePackerRegular.Node r, List<MB2_TexturePacker.Image> putHere)
		{
			if (r.img != null)
			{
				r.img.x = r.r.x;
				r.img.y = r.r.y;
				putHere.Add(r.img);
			}
			if (r.child[0] != null)
			{
				MB2_TexturePackerRegular.flattenTree(r.child[0], putHere);
			}
			if (r.child[1] != null)
			{
				MB2_TexturePackerRegular.flattenTree(r.child[1], putHere);
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000BA484 File Offset: 0x000B8684
		private static void drawGizmosNode(MB2_TexturePackerRegular.Node r)
		{
			Vector3 vector = new Vector3((float)r.r.w, (float)r.r.h, 0f);
			Vector3 center = new Vector3((float)r.r.x + vector.x / 2f, (float)(-(float)r.r.y) - vector.y / 2f, 0f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(center, vector);
			if (r.img != null)
			{
				Gizmos.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
				vector = new Vector3((float)r.img.w, (float)r.img.h, 0f);
				Gizmos.DrawCube(new Vector3((float)r.r.x + vector.x / 2f, (float)(-(float)r.r.y) - vector.y / 2f, 0f), vector);
			}
			if (r.child[0] != null)
			{
				Gizmos.color = Color.red;
				MB2_TexturePackerRegular.drawGizmosNode(r.child[0]);
			}
			if (r.child[1] != null)
			{
				Gizmos.color = Color.green;
				MB2_TexturePackerRegular.drawGizmosNode(r.child[1]);
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000BA5D0 File Offset: 0x000B87D0
		private static Texture2D createFilledTex(Color c, int w, int h)
		{
			Texture2D texture2D = new Texture2D(w, h);
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					texture2D.SetPixel(i, j, c);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000BA610 File Offset: 0x000B8810
		public void DrawGizmos()
		{
			if (this.bestRoot != null)
			{
				MB2_TexturePackerRegular.drawGizmosNode(this.bestRoot.root);
				Gizmos.color = Color.yellow;
				Vector3 vector = new Vector3((float)this.bestRoot.outW, (float)(-(float)this.bestRoot.outH), 0f);
				Gizmos.DrawWireCube(new Vector3(vector.x / 2f, vector.y / 2f, 0f), vector);
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000BA68C File Offset: 0x000B888C
		private bool ProbeSingleAtlas(MB2_TexturePacker.Image[] imgsToAdd, int idealAtlasW, int idealAtlasH, float imgArea, int maxAtlasDimX, int maxAtlasDimY, MB2_TexturePackerRegular.ProbeResult pr)
		{
			MB2_TexturePackerRegular.Node node = new MB2_TexturePackerRegular.Node(MB2_TexturePacker.NodeType.maxDim);
			node.r = new MB2_TexturePacker.PixRect(0, 0, idealAtlasW, idealAtlasH);
			for (int i = 0; i < imgsToAdd.Length; i++)
			{
				if (node.Insert(imgsToAdd[i], false) == null)
				{
					return false;
				}
				if (i == imgsToAdd.Length - 1)
				{
					int num = 0;
					int num2 = 0;
					this.GetExtent(node, ref num, ref num2);
					int num3 = num;
					int num4 = num2;
					bool fits;
					float e;
					float sq;
					if (this.atlasMustBePowerOfTwo)
					{
						num3 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(num), maxAtlasDimX);
						num4 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(num2), maxAtlasDimY);
						if (num4 < num3 / 2)
						{
							num4 = num3 / 2;
						}
						if (num3 < num4 / 2)
						{
							num3 = num4 / 2;
						}
						fits = (num <= maxAtlasDimX && num2 <= maxAtlasDimY);
						float num5 = Mathf.Max(1f, (float)num / (float)maxAtlasDimX);
						float num6 = Mathf.Max(1f, (float)num2 / (float)maxAtlasDimY);
						float num7 = (float)num3 * num5 * (float)num4 * num6;
						e = 1f - (num7 - imgArea) / num7;
						sq = 1f;
					}
					else
					{
						e = 1f - ((float)(num * num2) - imgArea) / (float)(num * num2);
						if (num < num2)
						{
							sq = (float)num / (float)num2;
						}
						else
						{
							sq = (float)num2 / (float)num;
						}
						fits = (num <= maxAtlasDimX && num2 <= maxAtlasDimY);
					}
					pr.Set(num, num2, num3, num4, node, fits, e, sq);
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug(string.Concat(new string[]
						{
							"Probe success efficiency w=",
							num.ToString(),
							" h=",
							num2.ToString(),
							" e=",
							e.ToString(),
							" sq=",
							sq.ToString(),
							" fits=",
							fits.ToString()
						}), Array.Empty<object>());
					}
					return true;
				}
			}
			Debug.LogError("Should never get here.");
			return false;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000BA86C File Offset: 0x000B8A6C
		private bool ProbeMultiAtlas(MB2_TexturePacker.Image[] imgsToAdd, int idealAtlasW, int idealAtlasH, float imgArea, int maxAtlasDimX, int maxAtlasDimY, MB2_TexturePackerRegular.ProbeResult pr)
		{
			int num = 0;
			MB2_TexturePackerRegular.Node node = new MB2_TexturePackerRegular.Node(MB2_TexturePacker.NodeType.maxDim);
			node.r = new MB2_TexturePacker.PixRect(0, 0, idealAtlasW, idealAtlasH);
			for (int i = 0; i < imgsToAdd.Length; i++)
			{
				if (node.Insert(imgsToAdd[i], false) == null)
				{
					if (imgsToAdd[i].x > idealAtlasW && imgsToAdd[i].y > idealAtlasH)
					{
						return false;
					}
					MB2_TexturePackerRegular.Node node2 = new MB2_TexturePackerRegular.Node(MB2_TexturePacker.NodeType.Container);
					node2.r = new MB2_TexturePacker.PixRect(0, 0, node.r.w + idealAtlasW, idealAtlasH);
					MB2_TexturePackerRegular.Node node3 = new MB2_TexturePackerRegular.Node(MB2_TexturePacker.NodeType.maxDim);
					node3.r = new MB2_TexturePacker.PixRect(node.r.w, 0, idealAtlasW, idealAtlasH);
					node2.child[1] = node3;
					node2.child[0] = node;
					node = node2;
					node.Insert(imgsToAdd[i], false);
					num++;
				}
			}
			pr.numAtlases = num;
			pr.root = node;
			pr.totalAtlasArea = (float)(num * maxAtlasDimX * maxAtlasDimY);
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug("Probe success efficiency numAtlases=" + num.ToString() + " totalArea=" + pr.totalAtlasArea.ToString(), Array.Empty<object>());
			}
			return true;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000BA984 File Offset: 0x000B8B84
		internal void GetExtent(MB2_TexturePackerRegular.Node r, ref int x, ref int y)
		{
			if (r.img != null)
			{
				if (r.r.x + r.img.w > x)
				{
					x = r.r.x + r.img.w;
				}
				if (r.r.y + r.img.h > y)
				{
					y = r.r.y + r.img.h;
				}
			}
			if (r.child[0] != null)
			{
				this.GetExtent(r.child[0], ref x, ref y);
			}
			if (r.child[1] != null)
			{
				this.GetExtent(r.child[1], ref x, ref y);
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000BAA38 File Offset: 0x000B8C38
		private int StepWidthHeight(int oldVal, int step, int maxDim)
		{
			if (this.atlasMustBePowerOfTwo && oldVal < maxDim)
			{
				return oldVal * 2;
			}
			int num = oldVal + step;
			if (num > maxDim && oldVal < maxDim)
			{
				num = maxDim;
			}
			return num;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000BAA64 File Offset: 0x000B8C64
		public override AtlasPackingResult[] GetRects(List<Vector2> imgWidthHeights, int maxDimensionX, int maxDimensionY, int atPadding)
		{
			List<AtlasPadding> list = new List<AtlasPadding>();
			for (int i = 0; i < imgWidthHeights.Count; i++)
			{
				list.Add(new AtlasPadding
				{
					topBottom = atPadding,
					leftRight = atPadding
				});
			}
			return this.GetRects(imgWidthHeights, list, maxDimensionX, maxDimensionY, false);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000BAAB8 File Offset: 0x000B8CB8
		public override AtlasPackingResult[] GetRects(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionX, int maxDimensionY, bool doMultiAtlas)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < paddings.Count; i++)
			{
				num = Mathf.Max(num, paddings[i].leftRight);
				num2 = Mathf.Max(num2, paddings[i].topBottom);
			}
			if (doMultiAtlas)
			{
				return this._GetRectsMultiAtlas(imgWidthHeights, paddings, maxDimensionX, maxDimensionY, 2 + num * 2, 2 + num2 * 2, 2 + num * 2, 2 + num2 * 2);
			}
			AtlasPackingResult atlasPackingResult = this._GetRectsSingleAtlas(imgWidthHeights, paddings, maxDimensionX, maxDimensionY, 2 + num * 2, 2 + num2 * 2, 2 + num * 2, 2 + num2 * 2, 0);
			if (atlasPackingResult == null)
			{
				return null;
			}
			return new AtlasPackingResult[]
			{
				atlasPackingResult
			};
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000BAB54 File Offset: 0x000B8D54
		private AtlasPackingResult _GetRectsSingleAtlas(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionX, int maxDimensionY, int minImageSizeX, int minImageSizeY, int masterImageSizeX, int masterImageSizeY, int recursionDepth)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Format("_GetRects numImages={0}, maxDimension={1}, minImageSizeX={2}, minImageSizeY={3}, masterImageSizeX={4}, masterImageSizeY={5}, recursionDepth={6}", new object[]
				{
					imgWidthHeights.Count,
					maxDimensionX,
					minImageSizeX,
					minImageSizeY,
					masterImageSizeX,
					masterImageSizeY,
					recursionDepth
				}));
			}
			if (recursionDepth > 10)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Maximum recursion depth reached. Couldn't find packing for these textures.");
				}
				return null;
			}
			float num = 0f;
			int num2 = 0;
			int num3 = 0;
			MB2_TexturePacker.Image[] array = new MB2_TexturePacker.Image[imgWidthHeights.Count];
			for (int i = 0; i < array.Length; i++)
			{
				int tw = (int)imgWidthHeights[i].x;
				int th = (int)imgWidthHeights[i].y;
				MB2_TexturePacker.Image image = array[i] = new MB2_TexturePacker.Image(i, tw, th, paddings[i], minImageSizeX, minImageSizeY);
				num += (float)(image.w * image.h);
				num2 = Mathf.Max(num2, image.w);
				num3 = Mathf.Max(num3, image.h);
			}
			if ((float)num3 / (float)num2 > 2f)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Using height Comparer", Array.Empty<object>());
				}
				Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageHeightComparer());
			}
			else if ((double)((float)num3 / (float)num2) < 0.5)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Using width Comparer", Array.Empty<object>());
				}
				Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageWidthComparer());
			}
			else
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Using area Comparer", Array.Empty<object>());
				}
				Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageAreaComparer());
			}
			int num4 = (int)Mathf.Sqrt(num);
			int num6;
			int num5;
			if (this.atlasMustBePowerOfTwo)
			{
				num5 = (num6 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num4));
				if (num2 > num6)
				{
					num6 = MB2_TexturePacker.CeilToNearestPowerOfTwo(num6);
				}
				if (num3 > num5)
				{
					num5 = MB2_TexturePacker.CeilToNearestPowerOfTwo(num5);
				}
			}
			else
			{
				num6 = num4;
				num5 = num4;
				if (num2 > num4)
				{
					num6 = num2;
					num5 = Mathf.Max(Mathf.CeilToInt(num / (float)num2), num3);
				}
				if (num3 > num4)
				{
					num6 = Mathf.Max(Mathf.CeilToInt(num / (float)num3), num2);
					num5 = num3;
				}
			}
			if (num6 == 0)
			{
				num6 = 4;
			}
			if (num5 == 0)
			{
				num5 = 4;
			}
			int num7 = (int)((float)num6 * 0.15f);
			int num8 = (int)((float)num5 * 0.15f);
			if (num7 == 0)
			{
				num7 = 1;
			}
			if (num8 == 0)
			{
				num8 = 1;
			}
			int num9 = 2;
			int num10 = num5;
			while (num9 >= 1 && num10 < num4 * 1000)
			{
				bool flag = false;
				num9 = 0;
				int num11 = num6;
				while (!flag && num11 < num4 * 1000)
				{
					MB2_TexturePackerRegular.ProbeResult probeResult = new MB2_TexturePackerRegular.ProbeResult();
					if (this.LOG_LEVEL >= MB2_LogLevel.trace)
					{
						Debug.Log("Probing h=" + num10.ToString() + " w=" + num11.ToString());
					}
					if (this.ProbeSingleAtlas(array, num11, num10, num, maxDimensionX, maxDimensionY, probeResult))
					{
						flag = true;
						if (this.bestRoot == null)
						{
							this.bestRoot = probeResult;
						}
						else if (probeResult.GetScore(this.atlasMustBePowerOfTwo) > this.bestRoot.GetScore(this.atlasMustBePowerOfTwo))
						{
							this.bestRoot = probeResult;
						}
					}
					else
					{
						num9++;
						num11 = this.StepWidthHeight(num11, num7, maxDimensionX);
						if (this.LOG_LEVEL >= MB2_LogLevel.trace)
						{
							MB2_Log.LogDebug("increasing Width h=" + num10.ToString() + " w=" + num11.ToString(), Array.Empty<object>());
						}
					}
				}
				num10 = this.StepWidthHeight(num10, num8, maxDimensionY);
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("increasing Height h=" + num10.ToString() + " w=" + num11.ToString(), Array.Empty<object>());
				}
			}
			if (this.bestRoot == null)
			{
				return null;
			}
			int num12 = 0;
			int num13 = 0;
			if (this.atlasMustBePowerOfTwo)
			{
				num12 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(this.bestRoot.w), maxDimensionX);
				num13 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(this.bestRoot.h), maxDimensionY);
				if (num13 < num12 / 2)
				{
					num13 = num12 / 2;
				}
				if (num12 < num13 / 2)
				{
					num12 = num13 / 2;
				}
			}
			else
			{
				num12 = Mathf.Min(this.bestRoot.w, maxDimensionX);
				num13 = Mathf.Min(this.bestRoot.h, maxDimensionY);
			}
			this.bestRoot.outW = num12;
			this.bestRoot.outH = num13;
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new string[]
				{
					"Best fit found: atlasW=",
					num12.ToString(),
					" atlasH",
					num13.ToString(),
					" w=",
					this.bestRoot.w.ToString(),
					" h=",
					this.bestRoot.h.ToString(),
					" efficiency=",
					this.bestRoot.efficiency.ToString(),
					" squareness=",
					this.bestRoot.squareness.ToString(),
					" fits in max dimension=",
					this.bestRoot.largerOrEqualToMaxDim.ToString()
				}));
			}
			List<MB2_TexturePacker.Image> list = new List<MB2_TexturePacker.Image>();
			MB2_TexturePackerRegular.flattenTree(this.bestRoot.root, list);
			list.Sort(new MB2_TexturePacker.ImgIDComparer());
			Vector2 rootWH = new Vector2((float)this.bestRoot.w, (float)this.bestRoot.h);
			float num14;
			float num15;
			int minImageSizeX2;
			int minImageSizeY2;
			if (!base.ScaleAtlasToFitMaxDim(rootWH, list, maxDimensionX, maxDimensionY, paddings[0], minImageSizeX, minImageSizeY, masterImageSizeX, masterImageSizeY, ref num12, ref num13, out num14, out num15, out minImageSizeX2, out minImageSizeY2))
			{
				AtlasPackingResult atlasPackingResult = new AtlasPackingResult(paddings.ToArray());
				atlasPackingResult.rects = new Rect[list.Count];
				atlasPackingResult.srcImgIdxs = new int[list.Count];
				atlasPackingResult.atlasX = num12;
				atlasPackingResult.atlasY = num13;
				atlasPackingResult.usedW = -1;
				atlasPackingResult.usedH = -1;
				for (int j = 0; j < list.Count; j++)
				{
					MB2_TexturePacker.Image image2 = list[j];
					Rect rect = atlasPackingResult.rects[j] = new Rect((float)image2.x / (float)num12 + num14, (float)image2.y / (float)num13 + num15, (float)image2.w / (float)num12 - num14 * 2f, (float)image2.h / (float)num13 - num15 * 2f);
					atlasPackingResult.srcImgIdxs[j] = image2.imgId;
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug(string.Concat(new string[]
						{
							"Image: ",
							j.ToString(),
							" imgID=",
							image2.imgId.ToString(),
							" x=",
							(rect.x * (float)num12).ToString(),
							" y=",
							(rect.y * (float)num13).ToString(),
							" w=",
							(rect.width * (float)num12).ToString(),
							" h=",
							(rect.height * (float)num13).ToString(),
							" padding=",
							paddings[j].ToString()
						}), Array.Empty<object>());
					}
				}
				atlasPackingResult.CalcUsedWidthAndHeight();
				return atlasPackingResult;
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("==================== REDOING PACKING ================");
			}
			return this._GetRectsSingleAtlas(imgWidthHeights, paddings, maxDimensionX, maxDimensionY, minImageSizeX2, minImageSizeY2, masterImageSizeX, masterImageSizeY, recursionDepth + 1);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000BB2F8 File Offset: 0x000B94F8
		private AtlasPackingResult[] _GetRectsMultiAtlas(List<Vector2> imgWidthHeights, List<AtlasPadding> paddings, int maxDimensionPassedX, int maxDimensionPassedY, int minImageSizeX, int minImageSizeY, int masterImageSizeX, int masterImageSizeY)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Format("_GetRects numImages={0}, maxDimensionX={1}, maxDimensionY={2} minImageSizeX={3}, minImageSizeY={4}, masterImageSizeX={5}, masterImageSizeY={6}", new object[]
				{
					imgWidthHeights.Count,
					maxDimensionPassedX,
					maxDimensionPassedY,
					minImageSizeX,
					minImageSizeY,
					masterImageSizeX,
					masterImageSizeY
				}));
			}
			float num = 0f;
			int a = 0;
			int a2 = 0;
			MB2_TexturePacker.Image[] array = new MB2_TexturePacker.Image[imgWidthHeights.Count];
			int num2 = maxDimensionPassedX;
			int num3 = maxDimensionPassedY;
			if (this.atlasMustBePowerOfTwo)
			{
				num2 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num2);
				num3 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num3);
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num4 = (int)imgWidthHeights[i].x;
				int num5 = (int)imgWidthHeights[i].y;
				num4 = Mathf.Min(num4, num2 - paddings[i].leftRight * 2);
				num5 = Mathf.Min(num5, num3 - paddings[i].topBottom * 2);
				MB2_TexturePacker.Image image = array[i] = new MB2_TexturePacker.Image(i, num4, num5, paddings[i], minImageSizeX, minImageSizeY);
				num += (float)(image.w * image.h);
				a = Mathf.Max(a, image.w);
				a2 = Mathf.Max(a2, image.h);
			}
			int num6;
			int num7;
			if (this.atlasMustBePowerOfTwo)
			{
				num6 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num3);
				num7 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num2);
			}
			else
			{
				num6 = num3;
				num7 = num2;
			}
			if (num7 == 0)
			{
				num7 = 4;
			}
			if (num6 == 0)
			{
				num6 = 4;
			}
			MB2_TexturePackerRegular.ProbeResult probeResult = new MB2_TexturePackerRegular.ProbeResult();
			Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageHeightComparer());
			if (this.ProbeMultiAtlas(array, num7, num6, num, num2, num3, probeResult))
			{
				this.bestRoot = probeResult;
			}
			Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageWidthComparer());
			if (this.ProbeMultiAtlas(array, num7, num6, num, num2, num3, probeResult) && probeResult.totalAtlasArea < this.bestRoot.totalAtlasArea)
			{
				this.bestRoot = probeResult;
			}
			Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageAreaComparer());
			if (this.ProbeMultiAtlas(array, num7, num6, num, num2, num3, probeResult) && probeResult.totalAtlasArea < this.bestRoot.totalAtlasArea)
			{
				this.bestRoot = probeResult;
			}
			if (this.bestRoot == null)
			{
				return null;
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new string[]
				{
					"Best fit found: w=",
					this.bestRoot.w.ToString(),
					" h=",
					this.bestRoot.h.ToString(),
					" efficiency=",
					this.bestRoot.efficiency.ToString(),
					" squareness=",
					this.bestRoot.squareness.ToString(),
					" fits in max dimension=",
					this.bestRoot.largerOrEqualToMaxDim.ToString()
				}));
			}
			List<AtlasPackingResult> list = new List<AtlasPackingResult>();
			List<MB2_TexturePackerRegular.Node> list2 = new List<MB2_TexturePackerRegular.Node>();
			Stack<MB2_TexturePackerRegular.Node> stack = new Stack<MB2_TexturePackerRegular.Node>();
			for (MB2_TexturePackerRegular.Node node = this.bestRoot.root; node != null; node = node.child[0])
			{
				stack.Push(node);
			}
			while (stack.Count > 0)
			{
				MB2_TexturePackerRegular.Node node = stack.Pop();
				if (node.isFullAtlas == MB2_TexturePacker.NodeType.maxDim)
				{
					list2.Add(node);
				}
				if (node.child[1] != null)
				{
					for (node = node.child[1]; node != null; node = node.child[0])
					{
						stack.Push(node);
					}
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				List<MB2_TexturePacker.Image> list3 = new List<MB2_TexturePacker.Image>();
				MB2_TexturePackerRegular.flattenTree(list2[j], list3);
				Rect[] array2 = new Rect[list3.Count];
				int[] array3 = new int[list3.Count];
				for (int k = 0; k < list3.Count; k++)
				{
					array2[k] = new Rect((float)(list3[k].x - list2[j].r.x), (float)list3[k].y, (float)list3[k].w, (float)list3[k].h);
					array3[k] = list3[k].imgId;
				}
				AtlasPackingResult atlasPackingResult = new AtlasPackingResult(paddings.ToArray());
				this.GetExtent(list2[j], ref atlasPackingResult.usedW, ref atlasPackingResult.usedH);
				atlasPackingResult.usedW -= list2[j].r.x;
				int num8 = list2[j].r.w;
				int num9 = list2[j].r.h;
				if (this.atlasMustBePowerOfTwo)
				{
					num8 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(atlasPackingResult.usedW), list2[j].r.w);
					num9 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(atlasPackingResult.usedH), list2[j].r.h);
					if (num9 < num8 / 2)
					{
						num9 = num8 / 2;
					}
					if (num8 < num9 / 2)
					{
						num8 = num9 / 2;
					}
				}
				else
				{
					num8 = atlasPackingResult.usedW;
					num9 = atlasPackingResult.usedH;
				}
				atlasPackingResult.atlasY = num9;
				atlasPackingResult.atlasX = num8;
				atlasPackingResult.rects = array2;
				atlasPackingResult.srcImgIdxs = array3;
				atlasPackingResult.CalcUsedWidthAndHeight();
				list.Add(atlasPackingResult);
				base.normalizeRects(atlasPackingResult, paddings[j]);
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Format("Done GetRects ", Array.Empty<object>()), Array.Empty<object>());
				}
			}
			return list.ToArray();
		}

		// Token: 0x04001D51 RID: 7505
		private MB2_TexturePackerRegular.ProbeResult bestRoot;

		// Token: 0x04001D52 RID: 7506
		public int atlasY;

		// Token: 0x0200046D RID: 1133
		private class ProbeResult
		{
			// Token: 0x06001C7A RID: 7290 RVA: 0x000156BF File Offset: 0x000138BF
			public void Set(int ww, int hh, int outw, int outh, MB2_TexturePackerRegular.Node r, bool fits, float e, float sq)
			{
				this.w = ww;
				this.h = hh;
				this.outW = outw;
				this.outH = outh;
				this.root = r;
				this.largerOrEqualToMaxDim = fits;
				this.efficiency = e;
				this.squareness = sq;
			}

			// Token: 0x06001C7B RID: 7291 RVA: 0x000BB8BC File Offset: 0x000B9ABC
			public float GetScore(bool doPowerOfTwoScore)
			{
				float num = this.largerOrEqualToMaxDim ? 1f : 0f;
				if (doPowerOfTwoScore)
				{
					return num * 2f + this.efficiency;
				}
				return this.squareness + 2f * this.efficiency + num;
			}

			// Token: 0x06001C7C RID: 7292 RVA: 0x000156FE File Offset: 0x000138FE
			public void PrintTree()
			{
				MB2_TexturePackerRegular.printTree(this.root, "  ");
			}

			// Token: 0x04001D53 RID: 7507
			public int w;

			// Token: 0x04001D54 RID: 7508
			public int h;

			// Token: 0x04001D55 RID: 7509
			public int outW;

			// Token: 0x04001D56 RID: 7510
			public int outH;

			// Token: 0x04001D57 RID: 7511
			public MB2_TexturePackerRegular.Node root;

			// Token: 0x04001D58 RID: 7512
			public bool largerOrEqualToMaxDim;

			// Token: 0x04001D59 RID: 7513
			public float efficiency;

			// Token: 0x04001D5A RID: 7514
			public float squareness;

			// Token: 0x04001D5B RID: 7515
			public float totalAtlasArea;

			// Token: 0x04001D5C RID: 7516
			public int numAtlases;
		}

		// Token: 0x0200046E RID: 1134
		internal class Node
		{
			// Token: 0x06001C7E RID: 7294 RVA: 0x00015710 File Offset: 0x00013910
			internal Node(MB2_TexturePacker.NodeType rootType)
			{
				this.isFullAtlas = rootType;
			}

			// Token: 0x06001C7F RID: 7295 RVA: 0x0001572B File Offset: 0x0001392B
			private bool isLeaf()
			{
				return this.child[0] == null || this.child[1] == null;
			}

			// Token: 0x06001C80 RID: 7296 RVA: 0x000BB908 File Offset: 0x000B9B08
			internal MB2_TexturePackerRegular.Node Insert(MB2_TexturePacker.Image im, bool handed)
			{
				int num;
				int num2;
				if (handed)
				{
					num = 0;
					num2 = 1;
				}
				else
				{
					num = 1;
					num2 = 0;
				}
				if (!this.isLeaf())
				{
					MB2_TexturePackerRegular.Node node = this.child[num].Insert(im, handed);
					if (node != null)
					{
						return node;
					}
					return this.child[num2].Insert(im, handed);
				}
				else
				{
					if (this.img != null)
					{
						return null;
					}
					if (this.r.w < im.w || this.r.h < im.h)
					{
						return null;
					}
					if (this.r.w == im.w && this.r.h == im.h)
					{
						this.img = im;
						return this;
					}
					this.child[num] = new MB2_TexturePackerRegular.Node(MB2_TexturePacker.NodeType.regular);
					this.child[num2] = new MB2_TexturePackerRegular.Node(MB2_TexturePacker.NodeType.regular);
					int num3 = this.r.w - im.w;
					int num4 = this.r.h - im.h;
					if (num3 > num4)
					{
						this.child[num].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y, im.w, this.r.h);
						this.child[num2].r = new MB2_TexturePacker.PixRect(this.r.x + im.w, this.r.y, this.r.w - im.w, this.r.h);
					}
					else
					{
						this.child[num].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y, this.r.w, im.h);
						this.child[num2].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y + im.h, this.r.w, this.r.h - im.h);
					}
					return this.child[num].Insert(im, handed);
				}
			}

			// Token: 0x04001D5D RID: 7517
			internal MB2_TexturePacker.NodeType isFullAtlas;

			// Token: 0x04001D5E RID: 7518
			internal MB2_TexturePackerRegular.Node[] child = new MB2_TexturePackerRegular.Node[2];

			// Token: 0x04001D5F RID: 7519
			internal MB2_TexturePacker.PixRect r;

			// Token: 0x04001D60 RID: 7520
			internal MB2_TexturePacker.Image img;

			// Token: 0x04001D61 RID: 7521
			private MB2_TexturePackerRegular.ProbeResult bestRoot;
		}
	}
}
