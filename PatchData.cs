using System;
using System.IO;
using System.Text;
using Lzf;
using UnityEngine;

// Token: 0x02000224 RID: 548
[Serializable]
public class PatchData
{
	// Token: 0x06000EBC RID: 3772 RVA: 0x0000BC60 File Offset: 0x00009E60
	private void OnDeserialized()
	{
		this.animationDatabase.OnDeserialized();
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x0000BC6D File Offset: 0x00009E6D
	public bool ContainsPatchedAnimations()
	{
		return this.animationDatabase != null;
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x000818CC File Offset: 0x0007FACC
	public void Serialize(string path)
	{
		Debug.Log("Serializing PatchData to " + path);
		string s = JsonUtility.ToJson(this);
		byte[] bytes = LZF.Compress(Encoding.UTF8.GetBytes(s));
		File.WriteAllBytes(path, bytes);
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00081908 File Offset: 0x0007FB08
	public static PatchData Deserialize(string path)
	{
		byte[] bytes = LZF.Decompress(File.ReadAllBytes(path));
		PatchData patchData = JsonUtility.FromJson<PatchData>(Encoding.UTF8.GetString(bytes));
		patchData.OnDeserialized();
		return patchData;
	}

	// Token: 0x04000F92 RID: 3986
	private const int GAME_BUILD_VERSION = 26;

	// Token: 0x04000F93 RID: 3987
	public int version = 26;

	// Token: 0x04000F94 RID: 3988
	public PatchedAnimationDatabase animationDatabase;
}
