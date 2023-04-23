using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine.Playables;

namespace Lua.Proxy
{
	// Token: 0x020009E3 RID: 2531
	[Proxy(typeof(PlayableDirector))]
	public class PlayableDirectorProxy : IProxy
	{
		// Token: 0x06004A8D RID: 19085 RVA: 0x00034CDF File Offset: 0x00032EDF
		[MoonSharpHidden]
		public PlayableDirectorProxy(PlayableDirector value)
		{
			this._value = value;
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x00034CEE File Offset: 0x00032EEE
		public PlayableDirectorProxy()
		{
			this._value = new PlayableDirector();
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x00034D01 File Offset: 0x00032F01
		public double duration
		{
			get
			{
				return this._value.duration;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x00034D0E File Offset: 0x00032F0E
		// (set) Token: 0x06004A91 RID: 19089 RVA: 0x00034D1B File Offset: 0x00032F1B
		public double initialTime
		{
			get
			{
				return this._value.initialTime;
			}
			set
			{
				this._value.initialTime = value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x00034D29 File Offset: 0x00032F29
		// (set) Token: 0x06004A93 RID: 19091 RVA: 0x00034D36 File Offset: 0x00032F36
		public bool playOnAwake
		{
			get
			{
				return this._value.playOnAwake;
			}
			set
			{
				this._value.playOnAwake = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x00034D44 File Offset: 0x00032F44
		// (set) Token: 0x06004A95 RID: 19093 RVA: 0x00034D51 File Offset: 0x00032F51
		public double time
		{
			get
			{
				return this._value.time;
			}
			set
			{
				this._value.time = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x00034D5F File Offset: 0x00032F5F
		public ScriptEventProxy paused
		{
			get
			{
				return ScriptEventProxy.New(WPlayableDirector.GetPaused(this._value));
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x00034D71 File Offset: 0x00032F71
		public ScriptEventProxy played
		{
			get
			{
				return ScriptEventProxy.New(WPlayableDirector.GetPlayed(this._value));
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x00034D83 File Offset: 0x00032F83
		public ScriptEventProxy stopped
		{
			get
			{
				return ScriptEventProxy.New(WPlayableDirector.GetStopped(this._value));
			}
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00034D95 File Offset: 0x00032F95
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00131C30 File Offset: 0x0012FE30
		[MoonSharpHidden]
		public static PlayableDirectorProxy New(PlayableDirector value)
		{
			if (value == null)
			{
				return null;
			}
			PlayableDirectorProxy playableDirectorProxy = (PlayableDirectorProxy)ObjectCache.Get(typeof(PlayableDirectorProxy), value);
			if (playableDirectorProxy == null)
			{
				playableDirectorProxy = new PlayableDirectorProxy(value);
				ObjectCache.Add(typeof(PlayableDirectorProxy), value, playableDirectorProxy);
			}
			return playableDirectorProxy;
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00034D9D File Offset: 0x00032F9D
		[MoonSharpUserDataMetamethod("__call")]
		public static PlayableDirectorProxy Call(DynValue _)
		{
			return new PlayableDirectorProxy();
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00034DA4 File Offset: 0x00032FA4
		public void DeferredEvaluate()
		{
			this._value.DeferredEvaluate();
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00034DB1 File Offset: 0x00032FB1
		public void Evaluate()
		{
			this._value.Evaluate();
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00034DBE File Offset: 0x00032FBE
		public void Pause()
		{
			this._value.Pause();
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00034DCB File Offset: 0x00032FCB
		public void Play()
		{
			this._value.Play();
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00034DD8 File Offset: 0x00032FD8
		public void RebindPlayableGraphOutputs()
		{
			this._value.RebindPlayableGraphOutputs();
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00034DE5 File Offset: 0x00032FE5
		public void RebuildGraph()
		{
			this._value.RebuildGraph();
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00034DF2 File Offset: 0x00032FF2
		public void Resume()
		{
			this._value.Resume();
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00034DFF File Offset: 0x00032FFF
		public void Stop()
		{
			this._value.Stop();
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00034E0C File Offset: 0x0003300C
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400317B RID: 12667
		[MoonSharpHidden]
		public PlayableDirector _value;
	}
}
