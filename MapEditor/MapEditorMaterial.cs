using System;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x02000615 RID: 1557
	public class MapEditorMaterial
	{
		// Token: 0x060027EB RID: 10219 RVA: 0x0001B946 File Offset: 0x00019B46
		public MapEditorMaterial(MaterialList materialList) : this(materialList, "New Material")
		{
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000F97F0 File Offset: 0x000F79F0
		public MapEditorMaterial(MaterialList materialList, string name)
		{
			this.guid = Guid.NewGuid().ToString();
			this.onTextureChanged = new UnityEvent();
			base..ctor();
			this.materialList = materialList;
			this.name = name;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0001B954 File Offset: 0x00019B54
		public MapEditorMaterial(MaterialList materialList, string name, string guid, TextureAsset albedoAsset, TextureAsset normalAsset) : this(materialList, name)
		{
			this.guid = guid;
			this.albedoAsset = albedoAsset;
			this.normalAsset = normalAsset;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000F9838 File Offset: 0x000F7A38
		public MapEditorMaterial(TerrainLayer terrainLayer)
		{
			this.guid = Guid.NewGuid().ToString();
			this.onTextureChanged = new UnityEvent();
			base..ctor();
			this.name = terrainLayer.diffuseTexture.name;
			this.normalAsset = AssetTable.FindTexture(terrainLayer.normalMapTexture);
			this.albedoAsset = AssetTable.FindTexture(terrainLayer.diffuseTexture);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0001B975 File Offset: 0x00019B75
		public MapEditorMaterial ShallowCopy()
		{
			return base.MemberwiseClone() as MapEditorMaterial;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000F98A4 File Offset: 0x000F7AA4
		public void CopyFrom(MapEditorMaterial other)
		{
			this.guid = other.guid;
			this.name = other.name;
			this.onTextureChanged = other.onTextureChanged;
			this.materialList = other.materialList;
			this.albedoAsset = other.albedoAsset;
			this.normalAsset = other.normalAsset;
			this.onTextureChanged.Invoke();
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x0001B982 File Offset: 0x00019B82
		public TextureAsset GetAlbedoAsset()
		{
			return this.albedoAsset;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0001B98A File Offset: 0x00019B8A
		public void SetAlbedoAsset(TextureAsset asset)
		{
			this.albedoAsset = asset;
			this.onTextureChanged.Invoke();
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0001B99E File Offset: 0x00019B9E
		public TextureAsset GetNormalAsset()
		{
			return this.normalAsset;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0001B9A6 File Offset: 0x00019BA6
		public void SetNormalAsset(TextureAsset asset)
		{
			this.normalAsset = asset;
			this.onTextureChanged.Invoke();
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0001B9BA File Offset: 0x00019BBA
		public TerrainLayer ToTerrainLayer()
		{
			return new TerrainLayer
			{
				diffuseTexture = this.GetAlbedoTexture(),
				normalMapTexture = this.GetNormalTexture(),
				tileSize = new Vector2(10f, 10f)
			};
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0001B9EE File Offset: 0x00019BEE
		public Texture2D GetAlbedoTexture()
		{
			if (this.albedoAsset == TextureAsset.empty)
			{
				return this.materialList.missingAlbedoTexture;
			}
			return this.albedoAsset.texture;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0001BA19 File Offset: 0x00019C19
		public Texture2D GetNormalTexture()
		{
			return this.normalAsset.texture;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000F9904 File Offset: 0x000F7B04
		public Material GetMaterial(Material baseMaterial)
		{
			Material material = new Material(baseMaterial);
			if (this.albedoAsset != TextureAsset.empty)
			{
				material.mainTexture = this.albedoAsset.texture;
			}
			if (this.normalAsset != TextureAsset.empty)
			{
				material.SetTexture("_BumpMap", this.normalAsset.texture);
			}
			return material;
		}

		// Token: 0x040025F0 RID: 9712
		public const float TERRAIN_MATERIAL_TILE_SIZE = 10f;

		// Token: 0x040025F1 RID: 9713
		public string guid;

		// Token: 0x040025F2 RID: 9714
		public string name;

		// Token: 0x040025F3 RID: 9715
		[NonSerialized]
		public UnityEvent onTextureChanged;

		// Token: 0x040025F4 RID: 9716
		private MaterialList materialList;

		// Token: 0x040025F5 RID: 9717
		private TextureAsset albedoAsset;

		// Token: 0x040025F6 RID: 9718
		private TextureAsset normalAsset;
	}
}
