using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200043F RID: 1087
	public interface MB2_EditorMethodsInterface
	{
		// Token: 0x06001ABA RID: 6842
		void Clear();

		// Token: 0x06001ABB RID: 6843
		void RestoreReadFlagsAndFormats(ProgressUpdateDelegate progressInfo);

		// Token: 0x06001ABC RID: 6844
		void SetReadWriteFlag(Texture2D tx, bool isReadable, bool addToList);

		// Token: 0x06001ABD RID: 6845
		void AddTextureFormat(Texture2D tx, bool isNormalMap);

		// Token: 0x06001ABE RID: 6846
		void SaveAtlasToAssetDatabase(Texture2D atlas, ShaderTextureProperty texPropertyName, int atlasNum, Material resMat);

		// Token: 0x06001ABF RID: 6847
		bool IsNormalMap(Texture2D tx);

		// Token: 0x06001AC0 RID: 6848
		string GetPlatformString();

		// Token: 0x06001AC1 RID: 6849
		void SetTextureSize(Texture2D tx, int size);

		// Token: 0x06001AC2 RID: 6850
		bool IsCompressed(Texture2D tx);

		// Token: 0x06001AC3 RID: 6851
		void CheckBuildSettings(long estimatedAtlasSize);

		// Token: 0x06001AC4 RID: 6852
		bool CheckPrefabTypes(MB_ObjsToCombineTypes prefabType, List<GameObject> gos);

		// Token: 0x06001AC5 RID: 6853
		bool ValidateSkinnedMeshes(List<GameObject> mom);

		// Token: 0x06001AC6 RID: 6854
		void CommitChangesToAssets();

		// Token: 0x06001AC7 RID: 6855
		void OnPreTextureBake();

		// Token: 0x06001AC8 RID: 6856
		void OnPostTextureBake();

		// Token: 0x06001AC9 RID: 6857
		void Destroy(UnityEngine.Object o);
	}
}
