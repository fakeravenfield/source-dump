using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Lua.Wrapper
{
	// Token: 0x0200094B RID: 2379
	public class PlayableDirectorEventProvider : MonoBehaviour
	{
		// Token: 0x06003C48 RID: 15432 RVA: 0x00028D63 File Offset: 0x00026F63
		private static PlayableDirectorEventProvider CreateProviderFor(PlayableDirector pd)
		{
			PlayableDirectorEventProvider playableDirectorEventProvider = pd.gameObject.AddComponent<PlayableDirectorEventProvider>();
			playableDirectorEventProvider.RegisterEvents(pd);
			return playableDirectorEventProvider;
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x0012E300 File Offset: 0x0012C500
		public static PlayableDirectorEventProvider Get(PlayableDirector pd)
		{
			PlayableDirectorEventProvider playableDirectorEventProvider = pd.GetComponent<PlayableDirectorEventProvider>();
			if (playableDirectorEventProvider == null)
			{
				playableDirectorEventProvider = PlayableDirectorEventProvider.CreateProviderFor(pd);
			}
			return playableDirectorEventProvider;
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00028D77 File Offset: 0x00026F77
		private void RegisterEvents(PlayableDirector director)
		{
			director.paused += delegate(PlayableDirector p)
			{
				this.paused.Invoke(p);
			};
			director.stopped += delegate(PlayableDirector p)
			{
				this.stopped.Invoke(p);
			};
			director.played += delegate(PlayableDirector p)
			{
				this.played.Invoke(p);
			};
		}

		// Token: 0x040030FF RID: 12543
		public ScriptEvent<PlayableDirector> paused = new ScriptEvent<PlayableDirector>();

		// Token: 0x04003100 RID: 12544
		public ScriptEvent<PlayableDirector> stopped = new ScriptEvent<PlayableDirector>();

		// Token: 0x04003101 RID: 12545
		public ScriptEvent<PlayableDirector> played = new ScriptEvent<PlayableDirector>();
	}
}
