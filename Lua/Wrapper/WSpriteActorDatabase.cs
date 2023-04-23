using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000994 RID: 2452
	[Name("SpriteActorDatabase")]
	[Doc("Used to manage and access the sprite actor database")]
	public static class WSpriteActorDatabase
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x0002A238 File Offset: 0x00028438
		[Doc("Get the base sprite for the specified sprite actor pose.")]
		public static Sprite GetPoseBaseSprite(string poseName)
		{
			return SpriteActorDatabase.GetSpritePose(poseName).baseSprite;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x0002A245 File Offset: 0x00028445
		[Doc("Get the default actor name for the specified sprite actor pose.")]
		public static string GetPoseDefaultActorName(string poseName)
		{
			return SpriteActorDatabase.GetSpritePose(poseName).defaultDisplayName;
		}
	}
}
