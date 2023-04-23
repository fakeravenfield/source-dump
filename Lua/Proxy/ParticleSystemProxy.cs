using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009E1 RID: 2529
	[Proxy(typeof(ParticleSystem))]
	public class ParticleSystemProxy : IProxy
	{
		// Token: 0x06004A43 RID: 19011 RVA: 0x000347E5 File Offset: 0x000329E5
		[MoonSharpHidden]
		public ParticleSystemProxy(ParticleSystem value)
		{
			this._value = value;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x000347F4 File Offset: 0x000329F4
		public ParticleSystemProxy()
		{
			this._value = new ParticleSystem();
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06004A45 RID: 19013 RVA: 0x00034807 File Offset: 0x00032A07
		public bool isEmitting
		{
			get
			{
				return this._value.isEmitting;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06004A46 RID: 19014 RVA: 0x00034814 File Offset: 0x00032A14
		public bool isPaused
		{
			get
			{
				return this._value.isPaused;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x00034821 File Offset: 0x00032A21
		public bool isPlaying
		{
			get
			{
				return this._value.isPlaying;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0003482E File Offset: 0x00032A2E
		public bool isStopped
		{
			get
			{
				return this._value.isStopped;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x0003483B File Offset: 0x00032A3B
		public int particleCount
		{
			get
			{
				return this._value.particleCount;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x00034848 File Offset: 0x00032A48
		public bool proceduralSimulationSupported
		{
			get
			{
				return this._value.proceduralSimulationSupported;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x00034855 File Offset: 0x00032A55
		// (set) Token: 0x06004A4C RID: 19020 RVA: 0x00034862 File Offset: 0x00032A62
		public uint randomSeed
		{
			get
			{
				return this._value.randomSeed;
			}
			set
			{
				this._value.randomSeed = value;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06004A4D RID: 19021 RVA: 0x00034870 File Offset: 0x00032A70
		// (set) Token: 0x06004A4E RID: 19022 RVA: 0x0003487D File Offset: 0x00032A7D
		public float time
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

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0003488B File Offset: 0x00032A8B
		// (set) Token: 0x06004A50 RID: 19024 RVA: 0x00034898 File Offset: 0x00032A98
		public bool useAutoRandomSeed
		{
			get
			{
				return this._value.useAutoRandomSeed;
			}
			set
			{
				this._value.useAutoRandomSeed = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x000348A6 File Offset: 0x00032AA6
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x000348B8 File Offset: 0x00032AB8
		// (set) Token: 0x06004A53 RID: 19027 RVA: 0x000348C5 File Offset: 0x00032AC5
		public string tag
		{
			get
			{
				return this._value.tag;
			}
			set
			{
				this._value.tag = value;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x000348D3 File Offset: 0x00032AD3
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x000348E5 File Offset: 0x00032AE5
		// (set) Token: 0x06004A56 RID: 19030 RVA: 0x000348F2 File Offset: 0x00032AF2
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00034900 File Offset: 0x00032B00
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00131B34 File Offset: 0x0012FD34
		[MoonSharpHidden]
		public static ParticleSystemProxy New(ParticleSystem value)
		{
			if (value == null)
			{
				return null;
			}
			ParticleSystemProxy particleSystemProxy = (ParticleSystemProxy)ObjectCache.Get(typeof(ParticleSystemProxy), value);
			if (particleSystemProxy == null)
			{
				particleSystemProxy = new ParticleSystemProxy(value);
				ObjectCache.Add(typeof(ParticleSystemProxy), value, particleSystemProxy);
			}
			return particleSystemProxy;
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00034908 File Offset: 0x00032B08
		[MoonSharpUserDataMetamethod("__call")]
		public static ParticleSystemProxy Call(DynValue _)
		{
			return new ParticleSystemProxy();
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0003490F File Offset: 0x00032B0F
		public void AllocateAxisOfRotationAttribute()
		{
			this._value.AllocateAxisOfRotationAttribute();
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0003491C File Offset: 0x00032B1C
		public void AllocateMeshIndexAttribute()
		{
			this._value.AllocateMeshIndexAttribute();
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x00034929 File Offset: 0x00032B29
		public void Clear(bool withChildren)
		{
			this._value.Clear(withChildren);
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00034937 File Offset: 0x00032B37
		public void Clear()
		{
			this._value.Clear();
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00034944 File Offset: 0x00032B44
		public void Emit(int count)
		{
			this._value.Emit(count);
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00034952 File Offset: 0x00032B52
		public bool IsAlive(bool withChildren)
		{
			return this._value.IsAlive(withChildren);
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00034960 File Offset: 0x00032B60
		public bool IsAlive()
		{
			return this._value.IsAlive();
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x0003496D File Offset: 0x00032B6D
		public void Pause(bool withChildren)
		{
			this._value.Pause(withChildren);
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x0003497B File Offset: 0x00032B7B
		public void Pause()
		{
			this._value.Pause();
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x00034988 File Offset: 0x00032B88
		public void Play(bool withChildren)
		{
			this._value.Play(withChildren);
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x00034996 File Offset: 0x00032B96
		public void Play()
		{
			this._value.Play();
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x000349A3 File Offset: 0x00032BA3
		public static void ResetPreMappedBufferMemory()
		{
			ParticleSystem.ResetPreMappedBufferMemory();
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x000349AA File Offset: 0x00032BAA
		public static void SetMaximumPreMappedBufferCounts(int vertexBuffersCount, int indexBuffersCount)
		{
			ParticleSystem.SetMaximumPreMappedBufferCounts(vertexBuffersCount, indexBuffersCount);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x000349B3 File Offset: 0x00032BB3
		public void Simulate(float t, bool withChildren, bool restart, bool fixedTimeStep)
		{
			this._value.Simulate(t, withChildren, restart, fixedTimeStep);
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x000349C5 File Offset: 0x00032BC5
		public void Simulate(float t, bool withChildren, bool restart)
		{
			this._value.Simulate(t, withChildren, restart);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x000349D5 File Offset: 0x00032BD5
		public void Simulate(float t, bool withChildren)
		{
			this._value.Simulate(t, withChildren);
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x000349E4 File Offset: 0x00032BE4
		public void Simulate(float t)
		{
			this._value.Simulate(t);
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x000349F2 File Offset: 0x00032BF2
		public void Stop(bool withChildren)
		{
			this._value.Stop(withChildren);
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x00034A00 File Offset: 0x00032C00
		public void Stop()
		{
			this._value.Stop();
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00034A0D File Offset: 0x00032C0D
		public void TriggerSubEmitter(int subEmitterIndex)
		{
			this._value.TriggerSubEmitter(subEmitterIndex);
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x00034A1B File Offset: 0x00032C1B
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00034A29 File Offset: 0x00032C29
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x00034A36 File Offset: 0x00032C36
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003179 RID: 12665
		[MoonSharpHidden]
		public ParticleSystem _value;
	}
}
