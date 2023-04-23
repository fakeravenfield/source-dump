using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000617 RID: 1559
	public class MaterialList : MonoBehaviour, IEnumerable<MapEditorMaterial>, IEnumerable
	{
		// Token: 0x06002801 RID: 10241 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		public void Clear()
		{
			this.materials.Clear();
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000F9964 File Offset: 0x000F7B64
		public void Add(MapEditorMaterial material)
		{
			if (this.materials.Contains(material))
			{
				Debug.LogError("Material already in list: " + ((material != null) ? material.ToString() : null));
				return;
			}
			if (material.GetAlbedoAsset() == TextureAsset.empty)
			{
				Debug.LogErrorFormat("Material texture not set! Loading this map will result in pink object and/or terrain. The material is most likely missing from the asset table. Material name: {0}", new object[]
				{
					material.name
				});
			}
			this.materials.Add(material);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000F99D4 File Offset: 0x000F7BD4
		public void Remove(MapEditorMaterial material)
		{
			if (!this.materials.Contains(material))
			{
				Debug.LogError("Material not in list: " + ((material != null) ? material.ToString() : null));
				return;
			}
			material.name = "Removed";
			material.SetAlbedoAsset(AssetTable.FindTexture(this.missingAlbedoTexture));
			material.SetNormalAsset(TextureAsset.empty);
			this.materials.Remove(material);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000F9A40 File Offset: 0x000F7C40
		public MapEditorMaterial FindOrCreate(string guid)
		{
			MapEditorMaterial mapEditorMaterial = this.FindOrNull(guid);
			if (mapEditorMaterial == null)
			{
				return new MapEditorMaterial(this, "Missing material: " + guid);
			}
			return mapEditorMaterial;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000F9A6C File Offset: 0x000F7C6C
		public MapEditorMaterial FindOrNull(string guid)
		{
			return (from m in this.materials
			where m.guid == guid
			select m).FirstOrDefault<MapEditorMaterial>();
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000F9AA4 File Offset: 0x000F7CA4
		public MapEditorMaterial CreateDefaultMaterial()
		{
			MapEditorMaterial mapEditorMaterial = new MapEditorMaterial(this);
			mapEditorMaterial.SetAlbedoAsset(AssetTable.FindTexture(this.defaultAlbedoTexture));
			mapEditorMaterial.SetNormalAsset(AssetTable.FindTexture(this.defaultNormalTexture));
			this.Add(mapEditorMaterial);
			return mapEditorMaterial;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000F9AE4 File Offset: 0x000F7CE4
		public void AddRange(IEnumerable<MapEditorMaterial> materials)
		{
			foreach (MapEditorMaterial material in materials)
			{
				this.Add(material);
			}
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x0001BAB5 File Offset: 0x00019CB5
		public IEnumerator<MapEditorMaterial> GetEnumerator()
		{
			return ((IEnumerable<MapEditorMaterial>)this.materials).GetEnumerator();
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0001BAB5 File Offset: 0x00019CB5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<MapEditorMaterial>)this.materials).GetEnumerator();
		}

		// Token: 0x040025F7 RID: 9719
		public Texture2D defaultAlbedoTexture;

		// Token: 0x040025F8 RID: 9720
		public Texture2D defaultNormalTexture;

		// Token: 0x040025F9 RID: 9721
		public Texture2D missingAlbedoTexture;

		// Token: 0x040025FA RID: 9722
		private List<MapEditorMaterial> materials = new List<MapEditorMaterial>();
	}
}
