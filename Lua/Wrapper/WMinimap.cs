using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000973 RID: 2419
	[Name("Minimap")]
	[Doc("Use this class to access the ingame minimap")]
	public static class WMinimap
	{
		// Token: 0x06003DA0 RID: 15776 RVA: 0x00029B24 File Offset: 0x00027D24
		[Getter]
		[Doc("The minimap camera of the current level.")]
		public static Camera GetCamera()
		{
			return MinimapCamera.instance.camera;
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x00029B30 File Offset: 0x00027D30
		[Getter]
		[Doc("Texture of the rendered level")]
		public static Texture GetTexture()
		{
			return MinimapCamera.instance.GetTexture();
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x00029B3C File Offset: 0x00027D3C
		[Getter]
		[Doc("Texture of rendered actor blips excluding player squad.[..] For performance reasons, this texture is not updated every frame.")]
		public static Texture GetActorBlipTexture()
		{
			return MinimapUi.instance.actorBlipsRTFront;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00029B48 File Offset: 0x00027D48
		[Getter]
		[Doc("Texture of rendered player squad actor blips.[..] Updated every frame.")]
		public static Texture GetPlayerSquadBlipTexture()
		{
			return MinimapUi.instance.playerBlipsRT;
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00029B54 File Offset: 0x00027D54
		public static void SetBlipScale(float actorScale, float playerSquadScale, float viewConeScale)
		{
			MinimapUi.instance.actorBlipScale = actorScale;
			MinimapUi.instance.playerSquadBlipScale = playerSquadScale;
			MinimapUi.instance.playerViewConeBlipScale = viewConeScale;
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00029B77 File Offset: 0x00027D77
		[Doc("Render a new minimap texture.")]
		public static void Render()
		{
			MinimapCamera.instance.Render();
		}
	}
}
