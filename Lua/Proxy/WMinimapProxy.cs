using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A1E RID: 2590
	[Proxy(typeof(WMinimap))]
	public class WMinimapProxy : IProxy
	{
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x0003CD2C File Offset: 0x0003AF2C
		public static TextureProxy actorBlipTexture
		{
			get
			{
				return TextureProxy.New(WMinimap.GetActorBlipTexture());
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06005277 RID: 21111 RVA: 0x0003CD38 File Offset: 0x0003AF38
		public static CameraProxy camera
		{
			get
			{
				return CameraProxy.New(WMinimap.GetCamera());
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x0003CD44 File Offset: 0x0003AF44
		public static TextureProxy playerSquadBlipTexture
		{
			get
			{
				return TextureProxy.New(WMinimap.GetPlayerSquadBlipTexture());
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x0003CD50 File Offset: 0x0003AF50
		public static TextureProxy texture
		{
			get
			{
				return TextureProxy.New(WMinimap.GetTexture());
			}
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0003CD5C File Offset: 0x0003AF5C
		public static void Render()
		{
			WMinimap.Render();
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0003CD63 File Offset: 0x0003AF63
		public static void SetBlipScale(float actorScale, float playerSquadScale, float viewConeScale)
		{
			WMinimap.SetBlipScale(actorScale, playerSquadScale, viewConeScale);
		}
	}
}
