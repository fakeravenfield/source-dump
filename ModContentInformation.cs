using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class ModContentInformation
{
	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0000B633 File Offset: 0x00009833
	public static ModContentInformation VanillaContent
	{
		get
		{
			return new ModContentInformation
			{
				contentObject = null,
				versionInfo = ModManager.EngineVersionInfo.CurrentPlayer,
				bundlePath = "",
				requiresShaderReload = false,
				sourceMod = ModInformation.OfficialContent,
				bundleId = -1
			};
		}
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0000256A File Offset: 0x0000076A
	public ModContentInformation()
	{
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x0000B670 File Offset: 0x00009870
	public ModContentInformation(string assetBundlePath, ModInformation mod)
	{
		this.bundlePath = assetBundlePath;
		this.sourceMod = mod;
		this.versionInfo = ModManager.ExtractBundleEditorVersion(assetBundlePath);
		this.requiresShaderReload = FixBundleShaders.LoadedContentNeedsShaderReload(this.versionInfo);
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0000B6A3 File Offset: 0x000098A3
	public void AssignBundleID(AssetBundle bundle)
	{
		this.bundleId = bundle.GetInstanceID();
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x0000B6B1 File Offset: 0x000098B1
	public bool HasPatchData()
	{
		return this.patchData != null;
	}

	// Token: 0x04000F1F RID: 3871
	public GameObject contentObject;

	// Token: 0x04000F20 RID: 3872
	public PatchData patchData;

	// Token: 0x04000F21 RID: 3873
	public ModManager.EngineVersionInfo versionInfo;

	// Token: 0x04000F22 RID: 3874
	public string bundlePath;

	// Token: 0x04000F23 RID: 3875
	public ModInformation sourceMod;

	// Token: 0x04000F24 RID: 3876
	public bool requiresShaderReload;

	// Token: 0x04000F25 RID: 3877
	public int bundleId;
}
