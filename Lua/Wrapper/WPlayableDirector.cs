using System;
using UnityEngine.Playables;

namespace Lua.Wrapper
{
	// Token: 0x02000956 RID: 2390
	[Wrapper(typeof(PlayableDirector), includeTarget = true, includeBase = false)]
	public static class WPlayableDirector
	{
		// Token: 0x06003C9A RID: 15514 RVA: 0x000290B5 File Offset: 0x000272B5
		[Getter]
		public static ScriptEvent GetPaused(PlayableDirector self)
		{
			return PlayableDirectorEventProvider.Get(self).paused;
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000290C2 File Offset: 0x000272C2
		[Getter]
		public static ScriptEvent GetStopped(PlayableDirector self)
		{
			return PlayableDirectorEventProvider.Get(self).stopped;
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000290CF File Offset: 0x000272CF
		[Getter]
		public static ScriptEvent GetPlayed(PlayableDirector self)
		{
			return PlayableDirectorEventProvider.Get(self).played;
		}
	}
}
