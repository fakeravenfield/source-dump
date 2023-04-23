using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A26 RID: 2598
	[Proxy(typeof(WPortraitGenerator))]
	public class WPortraitGeneratorProxy : IProxy
	{
		// Token: 0x060052D4 RID: 21204 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0003D176 File Offset: 0x0003B376
		public static SpriteProxy GetTeamActorPortraitSprite(WTeam team)
		{
			return SpriteProxy.New(WPortraitGenerator.GetTeamActorPortraitSprite(team));
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x001395B8 File Offset: 0x001377B8
		public static TextureProxy RenderSkinPortrait(MeshProxy mesh, Material[] materials, int teamMaterialIndex, WTeam team)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			return TextureProxy.New(WPortraitGenerator.RenderSkinPortrait(mesh2, materials, teamMaterialIndex, team));
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0003D183 File Offset: 0x0003B383
		public static TextureProxy RenderTeamPortrait(WTeam team)
		{
			return TextureProxy.New(WPortraitGenerator.RenderTeamPortrait(team));
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0003D190 File Offset: 0x0003B390
		public static void ResetPortraitRenderOffset()
		{
			WPortraitGenerator.ResetPortraitRenderOffset();
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0003D197 File Offset: 0x0003B397
		public static void SetPortraitRenderOffset(Vector3Proxy positionOffset, QuaternionProxy rotationOffset)
		{
			if (positionOffset == null)
			{
				throw new ScriptRuntimeException("argument 'positionOffset' is nil");
			}
			if (rotationOffset == null)
			{
				throw new ScriptRuntimeException("argument 'rotationOffset' is nil");
			}
			WPortraitGenerator.SetPortraitRenderOffset(positionOffset._value, rotationOffset._value);
		}
	}
}
