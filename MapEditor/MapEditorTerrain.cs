using System;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200066A RID: 1642
	public class MapEditorTerrain : MonoBehaviour
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		public static MapEditorTerrain instance
		{
			get
			{
				return MapEditor.instance.editorTerrain;
			}
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x0001C9DE File Offset: 0x0001ABDE
		private void Awake()
		{
			Utils.MoveToLayer(base.gameObject, Layers.GetTerrainLayer());
			Utils.MoveToLayer(this.biomeContainer.gameObject, Layers.GetTerrainLayer());
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public Terrain GetTerrain()
		{
			return this.biomeContainer.GetTerrain();
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x0001CA12 File Offset: 0x0001AC12
		public TerrainHeightmap GetHeightmap()
		{
			if (this.heightmap == null)
			{
				this.heightmap = new TerrainHeightmap(this.GetTerrain());
				Debug.LogWarning("Heightmap created");
			}
			return this.heightmap;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x0001CA3D File Offset: 0x0001AC3D
		public TerrainAlphamap GetAlphamap()
		{
			if (this.alphamap == null)
			{
				this.alphamap = new TerrainAlphamap(this.GetTerrain());
				Debug.LogWarning("Alphamap created");
			}
			return this.alphamap;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0001CA68 File Offset: 0x0001AC68
		public float GetWaterLevel()
		{
			return this.biomeContainer.GetWaterLevel().transform.position.y - base.transform.position.y;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0001CA95 File Offset: 0x0001AC95
		public void SetWaterLevel(float waterLevel)
		{
			this.biomeContainer.GetWaterLevel().transform.position = new Vector3(0f, base.transform.position.y + waterLevel);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		public int GetTerrainSize()
		{
			return (int)this.GetTerrain().terrainData.size.x;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000FD968 File Offset: 0x000FBB68
		public float HighestMountain()
		{
			float y = this.GetTerrain().transform.position.y;
			float y2 = this.GetTerrain().terrainData.size.y;
			float num = (float)this.GetHeightmap().FindMaximumValue();
			float waterLevel = this.GetWaterLevel();
			return Mathf.Max(0f, y + y2 * num - waterLevel);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000FD9C8 File Offset: 0x000FBBC8
		public float? TerrainElevation(Vector3 position)
		{
			float? result = null;
			position.y = this.GetWaterLevel() + 50000f;
			bool flag;
			Vector3? vector = this.RayCast(new Ray(position, Vector3.down), out flag);
			if (vector != null && flag)
			{
				result = new float?(vector.Value.y);
			}
			return result;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000FDA24 File Offset: 0x000FBC24
		public float? HeightAboveGround(Vector3 position)
		{
			float? result = this.TerrainElevation(position);
			if (result != null)
			{
				result = new float?(position.y - result.Value);
			}
			return result;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000FDA58 File Offset: 0x000FBC58
		public float? HeightAboveGroundOrWater(Vector3 position)
		{
			float? result = null;
			float? num = this.TerrainElevation(position);
			float waterLevel = this.GetWaterLevel();
			if (num != null)
			{
				float num2 = Mathf.Max(num.Value, waterLevel);
				result = new float?(position.y - num2);
			}
			return result;
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000FDAA4 File Offset: 0x000FBCA4
		public bool CanRemoveLayer(TerrainAlphamap.Layer layer)
		{
			PileTexturer pileTexturer = this.biomeContainer.GetPileTexturer();
			return !(pileTexturer != null) || pileTexturer.layers == null || layer.FindIndex() >= pileTexturer.layers.Length;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000FDAE4 File Offset: 0x000FBCE4
		public static bool IsMaterialInUse(MapEditorMaterial material)
		{
			return MapEditor.instance.GetEditorTerrain().alphamap.GetLayers().Any((TerrainAlphamap.Layer t) => t.GetMaterial() == material);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		public Vector3 WorldPositionToHeightmapCoords(Vector3 worldPosition)
		{
			return (worldPosition - this.GetTerrain().GetPosition()).ToVector2XZ() * (float)this.GetHeightmap().resolution;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0001CB0E File Offset: 0x0001AD0E
		public Vector3 WorldPositionToAlphamapCoords(Vector3 worldPosition)
		{
			return (worldPosition - this.GetTerrain().GetPosition()).ToVector2XZ() * (float)this.GetAlphamap().resolution;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000FDB24 File Offset: 0x000FBD24
		public Vector3? RayCast(Ray ray, out bool hitTerrain, out bool hitWater)
		{
			hitWater = false;
			Vector3? result = this.RayCast(ray, out hitTerrain);
			bool flag = !hitTerrain || result == null || result.Value.y < this.GetWaterLevel();
			if (flag)
			{
				Vector3 inPoint = Vector3.up * this.GetWaterLevel();
				UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, inPoint);
				float distance;
				if (plane.Raycast(ray, out distance))
				{
					Vector3 point = ray.GetPoint(distance);
					result = new Vector3?(point);
					hitWater = true;
				}
			}
			return result;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000FDBB0 File Offset: 0x000FBDB0
		public Vector3? RayCast(Ray ray, out bool hitTerrain)
		{
			Vector3? result = null;
			hitTerrain = false;
			Vector3? vector = MapEditorTerrain.RayCast(ray, 100000f);
			if (vector != null)
			{
				result = new Vector3?(vector.Value);
				hitTerrain = true;
			}
			else
			{
				Vector3 position = base.transform.position;
				UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, position);
				float distance;
				if (plane.Raycast(ray, out distance))
				{
					Vector3 point = ray.GetPoint(distance);
					result = new Vector3?(point);
					hitTerrain = false;
				}
				else
				{
					result = null;
					hitTerrain = false;
				}
			}
			return result;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000FDC3C File Offset: 0x000FBE3C
		public static Vector3? RayCast(Ray ray, float maxDistance = 100000f)
		{
			int terrainLayerMask = Layers.GetTerrainLayerMask();
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, maxDistance, terrainLayerMask))
			{
				return new Vector3?(raycastHit.point);
			}
			return null;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000FDC74 File Offset: 0x000FBE74
		public static bool RayCast(Ray ray, out RaycastHit hit)
		{
			int terrainLayerMask = Layers.GetTerrainLayerMask();
			return Physics.Raycast(ray, out hit, 100000f, terrainLayerMask);
		}

		// Token: 0x04002725 RID: 10021
		public const float RAYCAST_MAX_DISTANCE = 100000f;

		// Token: 0x04002726 RID: 10022
		public BiomeContainer biomeContainer;

		// Token: 0x04002727 RID: 10023
		public BiomeAsset biomeAsset;

		// Token: 0x04002728 RID: 10024
		private TerrainHeightmap heightmap;

		// Token: 0x04002729 RID: 10025
		private TerrainAlphamap alphamap;
	}
}
