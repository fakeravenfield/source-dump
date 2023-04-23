using System;
using UnityEngine;

// Token: 0x02000305 RID: 773
public static class RetargetSkin
{
	// Token: 0x06001426 RID: 5158 RVA: 0x000956D0 File Offset: 0x000938D0
	public static void Retarget(ActorSkin skin)
	{
		RetargetSkin.RetargetSettings settings = default(RetargetSkin.RetargetSettings);
		if (skin.rigSettings.version == ActorSkin.RigSettings.RigVersion.Unity_5)
		{
			settings.addRootBone = true;
		}
		skin.characterSkin.mesh = RetargetSkin.Retarget(skin.characterSkin.mesh, settings);
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000100B9 File Offset: 0x0000E2B9
	private static Mesh Retarget(Mesh mesh, RetargetSkin.RetargetSettings settings)
	{
		Mesh mesh2 = UnityEngine.Object.Instantiate<Mesh>(mesh);
		RetargetSkin.UpdateBoneWeights(mesh2, settings);
		RetargetSkin.UpdateBindposes(mesh2, settings);
		return mesh2;
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x00095718 File Offset: 0x00093918
	private static void UpdateBoneWeights(Mesh mesh, RetargetSkin.RetargetSettings settings)
	{
		if (settings.addRootBone)
		{
			BoneWeight[] boneWeights = mesh.boneWeights;
			for (int i = 0; i < boneWeights.Length; i++)
			{
				BoneWeight boneWeight = boneWeights[i];
				int num = boneWeight.boneIndex0;
				boneWeight.boneIndex0 = num + 1;
				num = boneWeight.boneIndex1;
				boneWeight.boneIndex1 = num + 1;
				num = boneWeight.boneIndex2;
				boneWeight.boneIndex2 = num + 1;
				num = boneWeight.boneIndex3;
				boneWeight.boneIndex3 = num + 1;
				boneWeights[i] = boneWeight;
			}
			mesh.boneWeights = boneWeights;
		}
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x000957A0 File Offset: 0x000939A0
	private static void UpdateBindposes(Mesh mesh, RetargetSkin.RetargetSettings settings)
	{
		if (settings.addRootBone)
		{
			Matrix4x4[] bindposes = mesh.bindposes;
			Matrix4x4[] array = new Matrix4x4[bindposes.Length + 1];
			array[0] = Matrix4x4.identity;
			for (int i = 1; i < array.Length; i++)
			{
				array[i] = bindposes[i - 1];
			}
			mesh.bindposes = array;
		}
	}

	// Token: 0x02000306 RID: 774
	private struct RetargetSettings
	{
		// Token: 0x04001593 RID: 5523
		public bool addRootBone;
	}
}
