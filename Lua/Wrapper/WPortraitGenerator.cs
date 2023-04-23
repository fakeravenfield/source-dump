using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x0200098A RID: 2442
	[Name("PortraitGenerator")]
	[Doc("Used to render portrait sprites from actor skins.")]
	public static class WPortraitGenerator
	{
		// Token: 0x06003E0E RID: 15886 RVA: 0x0012F2B8 File Offset: 0x0012D4B8
		[Doc("Get the portrait sprite for the runtime generated actor portrait.")]
		public static Sprite GetTeamActorPortraitSprite(WTeam team)
		{
			int num = Mathf.Clamp((int)team, 0, 1);
			return SpriteActorDatabase.GetSpritePoseFromDefaultDatabase(RuntimePortraitGenerator.POSE_NAMES[num]).baseSprite;
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00029F60 File Offset: 0x00028160
		[Doc("Renders a portrait using the specified team skin.")]
		public static Texture RenderTeamPortrait(WTeam team)
		{
			return RuntimePortraitGenerator.RenderTeamPortrait(Mathf.Clamp((int)team, 0, 1));
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00029F6F File Offset: 0x0002816F
		[Doc("Renders a portrait using the specified skin.")]
		public static Texture RenderSkinPortrait(Mesh mesh, Material[] materials, int teamMaterialIndex, WTeam team)
		{
			return RuntimePortraitGenerator.RenderSkinPortrait(new ActorSkin.MeshSkin(mesh, materials, teamMaterialIndex), (int)team);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x00029F7F File Offset: 0x0002817F
		[Doc("Set a camera offset for the next portrait render.")]
		public static void SetPortraitRenderOffset(Vector3 positionOffset, Quaternion rotationOffset)
		{
			RuntimePortraitGenerator.ApplyOffset(positionOffset, rotationOffset);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00029F88 File Offset: 0x00028188
		[Doc("Resets the camera offset for the next portrait render.[..] This is automatically done after rendering any portrait.")]
		public static void ResetPortraitRenderOffset()
		{
			RuntimePortraitGenerator.ResetOffset();
		}
	}
}
