using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009C3 RID: 2499
	[Proxy(typeof(Vehicle.Engine))]
	public class EngineProxy : IProxy
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x0002F6AA File Offset: 0x0002D8AA
		[MoonSharpHidden]
		public EngineProxy(Vehicle.Engine value)
		{
			this._value = value;
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0002F6B9 File Offset: 0x0002D8B9
		public EngineProxy()
		{
			this._value = new Vehicle.Engine();
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x0002F6CC File Offset: 0x0002D8CC
		// (set) Token: 0x06004452 RID: 17490 RVA: 0x0002F6D9 File Offset: 0x0002D8D9
		public bool controlAudio
		{
			get
			{
				return this._value.controlAudio;
			}
			set
			{
				this._value.controlAudio = value;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x0002F6E7 File Offset: 0x0002D8E7
		// (set) Token: 0x06004454 RID: 17492 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
		public bool enabled
		{
			get
			{
				return this._value.enabled;
			}
			set
			{
				this._value.enabled = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0002F702 File Offset: 0x0002D902
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x0013054C File Offset: 0x0012E74C
		public AudioClipProxy ignitionClip
		{
			get
			{
				return AudioClipProxy.New(this._value.ignitionClip);
			}
			set
			{
				AudioClip ignitionClip = null;
				if (value != null)
				{
					ignitionClip = value._value;
				}
				this._value.ignitionClip = ignitionClip;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x0002F714 File Offset: 0x0002D914
		// (set) Token: 0x06004458 RID: 17496 RVA: 0x0002F721 File Offset: 0x0002D921
		public float pitchGainSpeed
		{
			get
			{
				return this._value.pitchGainSpeed;
			}
			set
			{
				this._value.pitchGainSpeed = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x0002F72F File Offset: 0x0002D92F
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x0002F73C File Offset: 0x0002D93C
		public float power
		{
			get
			{
				return this._value.power;
			}
			set
			{
				this._value.power = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x0002F74A File Offset: 0x0002D94A
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x0002F757 File Offset: 0x0002D957
		public float powerGainSpeed
		{
			get
			{
				return this._value.powerGainSpeed;
			}
			set
			{
				this._value.powerGainSpeed = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x0002F765 File Offset: 0x0002D965
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x00130574 File Offset: 0x0012E774
		public AudioClipProxy shiftForwardClip
		{
			get
			{
				return AudioClipProxy.New(this._value.shiftForwardClip);
			}
			set
			{
				AudioClip shiftForwardClip = null;
				if (value != null)
				{
					shiftForwardClip = value._value;
				}
				this._value.shiftForwardClip = shiftForwardClip;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x0002F777 File Offset: 0x0002D977
		// (set) Token: 0x06004460 RID: 17504 RVA: 0x0013059C File Offset: 0x0012E79C
		public AudioClipProxy shiftReverseClip
		{
			get
			{
				return AudioClipProxy.New(this._value.shiftReverseClip);
			}
			set
			{
				AudioClip shiftReverseClip = null;
				if (value != null)
				{
					shiftReverseClip = value._value;
				}
				this._value.shiftReverseClip = shiftReverseClip;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x0002F789 File Offset: 0x0002D989
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x0002F796 File Offset: 0x0002D996
		public float targetPitch
		{
			get
			{
				return this._value.targetPitch;
			}
			set
			{
				this._value.targetPitch = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x0002F7B1 File Offset: 0x0002D9B1
		public float targetThrottle
		{
			get
			{
				return this._value.targetThrottle;
			}
			set
			{
				this._value.targetThrottle = value;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x0002F7BF File Offset: 0x0002D9BF
		// (set) Token: 0x06004466 RID: 17510 RVA: 0x0002F7CC File Offset: 0x0002D9CC
		public float throttleGainSpeed
		{
			get
			{
				return this._value.throttleGainSpeed;
			}
			set
			{
				this._value.throttleGainSpeed = value;
			}
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x0002F7DA File Offset: 0x0002D9DA
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x001305C4 File Offset: 0x0012E7C4
		[MoonSharpHidden]
		public static EngineProxy New(Vehicle.Engine value)
		{
			if (value == null)
			{
				return null;
			}
			EngineProxy engineProxy = (EngineProxy)ObjectCache.Get(typeof(EngineProxy), value);
			if (engineProxy == null)
			{
				engineProxy = new EngineProxy(value);
				ObjectCache.Add(typeof(EngineProxy), value, engineProxy);
			}
			return engineProxy;
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x0002F7E2 File Offset: 0x0002D9E2
		[MoonSharpUserDataMetamethod("__call")]
		public static EngineProxy Call(DynValue _)
		{
			return new EngineProxy();
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x0002F7E9 File Offset: 0x0002D9E9
		public void PlayIgnitionSound()
		{
			this._value.PlayIgnitionSound();
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x0002F7F6 File Offset: 0x0002D9F6
		public void PlayShiftForwardSound()
		{
			this._value.PlayShiftForwardSound();
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x0002F803 File Offset: 0x0002DA03
		public void PlayShiftReverseSound()
		{
			this._value.PlayShiftReverseSound();
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x0002F810 File Offset: 0x0002DA10
		public void Reset()
		{
			this._value.Reset();
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x0002F81D File Offset: 0x0002DA1D
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400315B RID: 12635
		[MoonSharpHidden]
		public Vehicle.Engine _value;
	}
}
