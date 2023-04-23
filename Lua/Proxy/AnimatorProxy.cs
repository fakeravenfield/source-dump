using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B0 RID: 2480
	[Proxy(typeof(Animator))]
	public class AnimatorProxy : IProxy
	{
		// Token: 0x06004085 RID: 16517 RVA: 0x0002BC2D File Offset: 0x00029E2D
		[MoonSharpHidden]
		public AnimatorProxy(Animator value)
		{
			this._value = value;
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x0002BC3C File Offset: 0x00029E3C
		public AnimatorProxy()
		{
			this._value = new Animator();
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x0002BC4F File Offset: 0x00029E4F
		public Vector3Proxy angularVelocity
		{
			get
			{
				return Vector3Proxy.New(this._value.angularVelocity);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x0002BC61 File Offset: 0x00029E61
		// (set) Token: 0x06004089 RID: 16521 RVA: 0x0002BC6E File Offset: 0x00029E6E
		public bool applyRootMotion
		{
			get
			{
				return this._value.applyRootMotion;
			}
			set
			{
				this._value.applyRootMotion = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x0002BC7C File Offset: 0x00029E7C
		// (set) Token: 0x0600408B RID: 16523 RVA: 0x0002BC8E File Offset: 0x00029E8E
		public Vector3Proxy bodyPosition
		{
			get
			{
				return Vector3Proxy.New(this._value.bodyPosition);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.bodyPosition = value._value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x0002BCAF File Offset: 0x00029EAF
		// (set) Token: 0x0600408D RID: 16525 RVA: 0x0002BCC1 File Offset: 0x00029EC1
		public QuaternionProxy bodyRotation
		{
			get
			{
				return QuaternionProxy.New(this._value.bodyRotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.bodyRotation = value._value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x0002BCE2 File Offset: 0x00029EE2
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x0002BCEF File Offset: 0x00029EEF
		public AnimatorCullingMode cullingMode
		{
			get
			{
				return this._value.cullingMode;
			}
			set
			{
				this._value.cullingMode = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x0002BCFD File Offset: 0x00029EFD
		public Vector3Proxy deltaPosition
		{
			get
			{
				return Vector3Proxy.New(this._value.deltaPosition);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x0002BD0F File Offset: 0x00029F0F
		public QuaternionProxy deltaRotation
		{
			get
			{
				return QuaternionProxy.New(this._value.deltaRotation);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x0002BD21 File Offset: 0x00029F21
		// (set) Token: 0x06004093 RID: 16531 RVA: 0x0002BD2E File Offset: 0x00029F2E
		public float feetPivotActive
		{
			get
			{
				return this._value.feetPivotActive;
			}
			set
			{
				this._value.feetPivotActive = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x0002BD3C File Offset: 0x00029F3C
		// (set) Token: 0x06004095 RID: 16533 RVA: 0x0002BD49 File Offset: 0x00029F49
		public bool fireEvents
		{
			get
			{
				return this._value.fireEvents;
			}
			set
			{
				this._value.fireEvents = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x0002BD57 File Offset: 0x00029F57
		public float gravityWeight
		{
			get
			{
				return this._value.gravityWeight;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x0002BD64 File Offset: 0x00029F64
		public bool hasBoundPlayables
		{
			get
			{
				return this._value.hasBoundPlayables;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x0002BD71 File Offset: 0x00029F71
		public bool hasRootMotion
		{
			get
			{
				return this._value.hasRootMotion;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x0002BD7E File Offset: 0x00029F7E
		public bool hasTransformHierarchy
		{
			get
			{
				return this._value.hasTransformHierarchy;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x0002BD8B File Offset: 0x00029F8B
		public float humanScale
		{
			get
			{
				return this._value.humanScale;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x0002BD98 File Offset: 0x00029F98
		public bool isHuman
		{
			get
			{
				return this._value.isHuman;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x0002BDA5 File Offset: 0x00029FA5
		public bool isInitialized
		{
			get
			{
				return this._value.isInitialized;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x0002BDB2 File Offset: 0x00029FB2
		public bool isMatchingTarget
		{
			get
			{
				return this._value.isMatchingTarget;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x0002BDBF File Offset: 0x00029FBF
		public bool isOptimizable
		{
			get
			{
				return this._value.isOptimizable;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x0002BDCC File Offset: 0x00029FCC
		// (set) Token: 0x060040A0 RID: 16544 RVA: 0x0002BDD9 File Offset: 0x00029FD9
		public bool keepAnimatorControllerStateOnDisable
		{
			get
			{
				return this._value.keepAnimatorControllerStateOnDisable;
			}
			set
			{
				this._value.keepAnimatorControllerStateOnDisable = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x0002BDE7 File Offset: 0x00029FE7
		public int layerCount
		{
			get
			{
				return this._value.layerCount;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x0002BDF4 File Offset: 0x00029FF4
		// (set) Token: 0x060040A3 RID: 16547 RVA: 0x0002BE01 File Offset: 0x0002A001
		public bool layersAffectMassCenter
		{
			get
			{
				return this._value.layersAffectMassCenter;
			}
			set
			{
				this._value.layersAffectMassCenter = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x0002BE0F File Offset: 0x0002A00F
		public float leftFeetBottomHeight
		{
			get
			{
				return this._value.leftFeetBottomHeight;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x0002BE1C File Offset: 0x0002A01C
		// (set) Token: 0x060040A6 RID: 16550 RVA: 0x0002BE29 File Offset: 0x0002A029
		public bool logWarnings
		{
			get
			{
				return this._value.logWarnings;
			}
			set
			{
				this._value.logWarnings = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x0002BE37 File Offset: 0x0002A037
		public int parameterCount
		{
			get
			{
				return this._value.parameterCount;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x0002BE44 File Offset: 0x0002A044
		public Vector3Proxy pivotPosition
		{
			get
			{
				return Vector3Proxy.New(this._value.pivotPosition);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x0002BE56 File Offset: 0x0002A056
		public float pivotWeight
		{
			get
			{
				return this._value.pivotWeight;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x0002BE63 File Offset: 0x0002A063
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x0002BE70 File Offset: 0x0002A070
		public float playbackTime
		{
			get
			{
				return this._value.playbackTime;
			}
			set
			{
				this._value.playbackTime = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x0002BE7E File Offset: 0x0002A07E
		// (set) Token: 0x060040AD RID: 16557 RVA: 0x0002BE8B File Offset: 0x0002A08B
		public float recorderStartTime
		{
			get
			{
				return this._value.recorderStartTime;
			}
			set
			{
				this._value.recorderStartTime = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x0002BE99 File Offset: 0x0002A099
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x0002BEA6 File Offset: 0x0002A0A6
		public float recorderStopTime
		{
			get
			{
				return this._value.recorderStopTime;
			}
			set
			{
				this._value.recorderStopTime = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x0002BEB4 File Offset: 0x0002A0B4
		public float rightFeetBottomHeight
		{
			get
			{
				return this._value.rightFeetBottomHeight;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x0002BEC1 File Offset: 0x0002A0C1
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x0002BED3 File Offset: 0x0002A0D3
		public Vector3Proxy rootPosition
		{
			get
			{
				return Vector3Proxy.New(this._value.rootPosition);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.rootPosition = value._value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		// (set) Token: 0x060040B4 RID: 16564 RVA: 0x0002BF06 File Offset: 0x0002A106
		public QuaternionProxy rootRotation
		{
			get
			{
				return QuaternionProxy.New(this._value.rootRotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.rootRotation = value._value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x0002BF27 File Offset: 0x0002A127
		// (set) Token: 0x060040B6 RID: 16566 RVA: 0x0012FD08 File Offset: 0x0012DF08
		public RuntimeAnimatorControllerProxy runtimeAnimatorController
		{
			get
			{
				return RuntimeAnimatorControllerProxy.New(this._value.runtimeAnimatorController);
			}
			set
			{
				RuntimeAnimatorController runtimeAnimatorController = null;
				if (value != null)
				{
					runtimeAnimatorController = value._value;
				}
				this._value.runtimeAnimatorController = runtimeAnimatorController;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x0002BF39 File Offset: 0x0002A139
		// (set) Token: 0x060040B8 RID: 16568 RVA: 0x0002BF46 File Offset: 0x0002A146
		public float speed
		{
			get
			{
				return this._value.speed;
			}
			set
			{
				this._value.speed = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x0002BF54 File Offset: 0x0002A154
		// (set) Token: 0x060040BA RID: 16570 RVA: 0x0002BF61 File Offset: 0x0002A161
		public bool stabilizeFeet
		{
			get
			{
				return this._value.stabilizeFeet;
			}
			set
			{
				this._value.stabilizeFeet = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x0002BF6F File Offset: 0x0002A16F
		public Vector3Proxy targetPosition
		{
			get
			{
				return Vector3Proxy.New(this._value.targetPosition);
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x0002BF81 File Offset: 0x0002A181
		public QuaternionProxy targetRotation
		{
			get
			{
				return QuaternionProxy.New(this._value.targetRotation);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x0002BF93 File Offset: 0x0002A193
		// (set) Token: 0x060040BE RID: 16574 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public AnimatorUpdateMode updateMode
		{
			get
			{
				return this._value.updateMode;
			}
			set
			{
				this._value.updateMode = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x0002BFAE File Offset: 0x0002A1AE
		public Vector3Proxy velocity
		{
			get
			{
				return Vector3Proxy.New(this._value.velocity);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x0002BFC0 File Offset: 0x0002A1C0
		// (set) Token: 0x060040C1 RID: 16577 RVA: 0x0002BFCD File Offset: 0x0002A1CD
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

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x0002BFDB File Offset: 0x0002A1DB
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x0002BFFA File Offset: 0x0002A1FA
		// (set) Token: 0x060040C5 RID: 16581 RVA: 0x0002C007 File Offset: 0x0002A207
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

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x0002C015 File Offset: 0x0002A215
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x0002C027 File Offset: 0x0002A227
		// (set) Token: 0x060040C8 RID: 16584 RVA: 0x0002C034 File Offset: 0x0002A234
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

		// Token: 0x060040C9 RID: 16585 RVA: 0x0002C042 File Offset: 0x0002A242
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0012FD30 File Offset: 0x0012DF30
		[MoonSharpHidden]
		public static AnimatorProxy New(Animator value)
		{
			if (value == null)
			{
				return null;
			}
			AnimatorProxy animatorProxy = (AnimatorProxy)ObjectCache.Get(typeof(AnimatorProxy), value);
			if (animatorProxy == null)
			{
				animatorProxy = new AnimatorProxy(value);
				ObjectCache.Add(typeof(AnimatorProxy), value, animatorProxy);
			}
			return animatorProxy;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x0002C04A File Offset: 0x0002A24A
		[MoonSharpUserDataMetamethod("__call")]
		public static AnimatorProxy Call(DynValue _)
		{
			return new AnimatorProxy();
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0002C051 File Offset: 0x0002A251
		public void ApplyBuiltinRootMotion()
		{
			this._value.ApplyBuiltinRootMotion();
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x0002C05E File Offset: 0x0002A25E
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			this._value.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0002C070 File Offset: 0x0002A270
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer)
		{
			this._value.CrossFade(stateName, normalizedTransitionDuration, layer);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0002C080 File Offset: 0x0002A280
		public void CrossFade(string stateName, float normalizedTransitionDuration)
		{
			this._value.CrossFade(stateName, normalizedTransitionDuration);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x0002C08F File Offset: 0x0002A28F
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset, float normalizedTransitionTime)
		{
			this._value.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0002C0A3 File Offset: 0x0002A2A3
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset, float normalizedTransitionTime)
		{
			this._value.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x0002C0B7 File Offset: 0x0002A2B7
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			this._value.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0002C0C9 File Offset: 0x0002A2C9
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer)
		{
			this._value.CrossFade(stateHashName, normalizedTransitionDuration, layer);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0002C0D9 File Offset: 0x0002A2D9
		public void CrossFade(int stateHashName, float normalizedTransitionDuration)
		{
			this._value.CrossFade(stateHashName, normalizedTransitionDuration);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration)
		{
			this._value.CrossFadeInFixedTime(stateName, fixedTransitionDuration);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x0002C0F7 File Offset: 0x0002A2F7
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer)
		{
			this._value.CrossFadeInFixedTime(stateName, fixedTransitionDuration, layer);
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x0002C107 File Offset: 0x0002A307
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			this._value.CrossFadeInFixedTime(stateName, fixedTransitionDuration, layer, fixedTimeOffset);
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x0002C119 File Offset: 0x0002A319
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset, float normalizedTransitionTime)
		{
			this._value.CrossFadeInFixedTime(stateName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x0002C12D File Offset: 0x0002A32D
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			this._value.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x0002C13F File Offset: 0x0002A33F
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer)
		{
			this._value.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x0002C14F File Offset: 0x0002A34F
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration)
		{
			this._value.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x0002C15E File Offset: 0x0002A35E
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset, float normalizedTransitionTime)
		{
			this._value.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x0002C172 File Offset: 0x0002A372
		public TransformProxy GetBoneTransform(HumanBodyBones humanBoneId)
		{
			return TransformProxy.New(this._value.GetBoneTransform(humanBoneId));
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x0002C185 File Offset: 0x0002A385
		public bool GetBool(string name)
		{
			return this._value.GetBool(name);
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x0002C193 File Offset: 0x0002A393
		public bool GetBool(int id)
		{
			return this._value.GetBool(id);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x0002C1A1 File Offset: 0x0002A3A1
		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return this._value.GetCurrentAnimatorClipInfoCount(layerIndex);
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x0002C1AF File Offset: 0x0002A3AF
		public float GetFloat(string name)
		{
			return this._value.GetFloat(name);
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x0002C1BD File Offset: 0x0002A3BD
		public float GetFloat(int id)
		{
			return this._value.GetFloat(id);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x0002C1CB File Offset: 0x0002A3CB
		public Vector3Proxy GetIKHintPosition(AvatarIKHint hint)
		{
			return Vector3Proxy.New(this._value.GetIKHintPosition(hint));
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0002C1DE File Offset: 0x0002A3DE
		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			return this._value.GetIKHintPositionWeight(hint);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		public Vector3Proxy GetIKPosition(AvatarIKGoal goal)
		{
			return Vector3Proxy.New(this._value.GetIKPosition(goal));
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x0002C1FF File Offset: 0x0002A3FF
		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			return this._value.GetIKPositionWeight(goal);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0002C20D File Offset: 0x0002A40D
		public QuaternionProxy GetIKRotation(AvatarIKGoal goal)
		{
			return QuaternionProxy.New(this._value.GetIKRotation(goal));
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x0002C220 File Offset: 0x0002A420
		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			return this._value.GetIKRotationWeight(goal);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x0002C22E File Offset: 0x0002A42E
		public int GetInteger(string name)
		{
			return this._value.GetInteger(name);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x0002C23C File Offset: 0x0002A43C
		public int GetInteger(int id)
		{
			return this._value.GetInteger(id);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x0002C24A File Offset: 0x0002A44A
		public int GetLayerIndex(string layerName)
		{
			return this._value.GetLayerIndex(layerName);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0002C258 File Offset: 0x0002A458
		public string GetLayerName(int layerIndex)
		{
			return this._value.GetLayerName(layerIndex);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0002C266 File Offset: 0x0002A466
		public float GetLayerWeight(int layerIndex)
		{
			return this._value.GetLayerWeight(layerIndex);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0002C274 File Offset: 0x0002A474
		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return this._value.GetNextAnimatorClipInfoCount(layerIndex);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x0002C282 File Offset: 0x0002A482
		public bool HasState(int layerIndex, int stateID)
		{
			return this._value.HasState(layerIndex, stateID);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0002C291 File Offset: 0x0002A491
		public void InterruptMatchTarget()
		{
			this._value.InterruptMatchTarget();
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x0002C29E File Offset: 0x0002A49E
		public void InterruptMatchTarget(bool completeMatch)
		{
			this._value.InterruptMatchTarget(completeMatch);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		public bool IsInTransition(int layerIndex)
		{
			return this._value.IsInTransition(layerIndex);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x0002C2BA File Offset: 0x0002A4BA
		public bool IsParameterControlledByCurve(string name)
		{
			return this._value.IsParameterControlledByCurve(name);
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x0002C2C8 File Offset: 0x0002A4C8
		public bool IsParameterControlledByCurve(int id)
		{
			return this._value.IsParameterControlledByCurve(id);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x0002C2D6 File Offset: 0x0002A4D6
		public void Play(string stateName, int layer)
		{
			this._value.Play(stateName, layer);
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x0002C2E5 File Offset: 0x0002A4E5
		public void Play(string stateName)
		{
			this._value.Play(stateName);
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x0002C2F3 File Offset: 0x0002A4F3
		public void Play(string stateName, int layer, float normalizedTime)
		{
			this._value.Play(stateName, layer, normalizedTime);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x0002C303 File Offset: 0x0002A503
		public void Play(int stateNameHash, int layer, float normalizedTime)
		{
			this._value.Play(stateNameHash, layer, normalizedTime);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x0002C313 File Offset: 0x0002A513
		public void Play(int stateNameHash, int layer)
		{
			this._value.Play(stateNameHash, layer);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0002C322 File Offset: 0x0002A522
		public void Play(int stateNameHash)
		{
			this._value.Play(stateNameHash);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0002C330 File Offset: 0x0002A530
		public void PlayInFixedTime(string stateName, int layer)
		{
			this._value.PlayInFixedTime(stateName, layer);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0002C33F File Offset: 0x0002A53F
		public void PlayInFixedTime(string stateName)
		{
			this._value.PlayInFixedTime(stateName);
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x0002C34D File Offset: 0x0002A54D
		public void PlayInFixedTime(string stateName, int layer, float fixedTime)
		{
			this._value.PlayInFixedTime(stateName, layer, fixedTime);
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x0002C35D File Offset: 0x0002A55D
		public void PlayInFixedTime(int stateNameHash, int layer, float fixedTime)
		{
			this._value.PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x0002C36D File Offset: 0x0002A56D
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			this._value.PlayInFixedTime(stateNameHash, layer);
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0002C37C File Offset: 0x0002A57C
		public void PlayInFixedTime(int stateNameHash)
		{
			this._value.PlayInFixedTime(stateNameHash);
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0002C38A File Offset: 0x0002A58A
		public void Rebind()
		{
			this._value.Rebind();
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x0002C397 File Offset: 0x0002A597
		public void ResetTrigger(string name)
		{
			this._value.ResetTrigger(name);
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x0002C3A5 File Offset: 0x0002A5A5
		public void ResetTrigger(int id)
		{
			this._value.ResetTrigger(id);
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x0002C3B3 File Offset: 0x0002A5B3
		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, QuaternionProxy rotation)
		{
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			this._value.SetBoneLocalRotation(humanBoneId, rotation._value);
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x0002C3D5 File Offset: 0x0002A5D5
		public void SetBool(string name, bool value)
		{
			this._value.SetBool(name, value);
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x0002C3E4 File Offset: 0x0002A5E4
		public void SetBool(int id, bool value)
		{
			this._value.SetBool(id, value);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x0002C3F3 File Offset: 0x0002A5F3
		public void SetFloat(string name, float value)
		{
			this._value.SetFloat(name, value);
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x0002C402 File Offset: 0x0002A602
		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this._value.SetFloat(name, value, dampTime, deltaTime);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x0002C414 File Offset: 0x0002A614
		public void SetFloat(int id, float value)
		{
			this._value.SetFloat(id, value);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x0002C423 File Offset: 0x0002A623
		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this._value.SetFloat(id, value, dampTime, deltaTime);
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0002C435 File Offset: 0x0002A635
		public void SetIKHintPosition(AvatarIKHint hint, Vector3Proxy hintPosition)
		{
			if (hintPosition == null)
			{
				throw new ScriptRuntimeException("argument 'hintPosition' is nil");
			}
			this._value.SetIKHintPosition(hint, hintPosition._value);
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x0002C457 File Offset: 0x0002A657
		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			this._value.SetIKHintPositionWeight(hint, value);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x0002C466 File Offset: 0x0002A666
		public void SetIKPosition(AvatarIKGoal goal, Vector3Proxy goalPosition)
		{
			if (goalPosition == null)
			{
				throw new ScriptRuntimeException("argument 'goalPosition' is nil");
			}
			this._value.SetIKPosition(goal, goalPosition._value);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x0002C488 File Offset: 0x0002A688
		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			this._value.SetIKPositionWeight(goal, value);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x0002C497 File Offset: 0x0002A697
		public void SetIKRotation(AvatarIKGoal goal, QuaternionProxy goalRotation)
		{
			if (goalRotation == null)
			{
				throw new ScriptRuntimeException("argument 'goalRotation' is nil");
			}
			this._value.SetIKRotation(goal, goalRotation._value);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x0002C4B9 File Offset: 0x0002A6B9
		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			this._value.SetIKRotationWeight(goal, value);
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x0002C4C8 File Offset: 0x0002A6C8
		public void SetInteger(string name, int value)
		{
			this._value.SetInteger(name, value);
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x0002C4D7 File Offset: 0x0002A6D7
		public void SetInteger(int id, int value)
		{
			this._value.SetInteger(id, value);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0002C4E6 File Offset: 0x0002A6E6
		public void SetLayerWeight(int layerIndex, float weight)
		{
			this._value.SetLayerWeight(layerIndex, weight);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0002C4F5 File Offset: 0x0002A6F5
		public void SetLookAtPosition(Vector3Proxy lookAtPosition)
		{
			if (lookAtPosition == null)
			{
				throw new ScriptRuntimeException("argument 'lookAtPosition' is nil");
			}
			this._value.SetLookAtPosition(lookAtPosition._value);
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0002C516 File Offset: 0x0002A716
		public void SetLookAtWeight(float weight)
		{
			this._value.SetLookAtWeight(weight);
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x0002C524 File Offset: 0x0002A724
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			this._value.SetLookAtWeight(weight, bodyWeight);
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x0002C533 File Offset: 0x0002A733
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			this._value.SetLookAtWeight(weight, bodyWeight, headWeight);
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x0002C543 File Offset: 0x0002A743
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			this._value.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x0002C555 File Offset: 0x0002A755
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight)
		{
			this._value.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x0002C569 File Offset: 0x0002A769
		public void SetTrigger(string name)
		{
			this._value.SetTrigger(name);
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0002C577 File Offset: 0x0002A777
		public void SetTrigger(int id)
		{
			this._value.SetTrigger(id);
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0002C585 File Offset: 0x0002A785
		public void StartPlayback()
		{
			this._value.StartPlayback();
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0002C592 File Offset: 0x0002A792
		public void StartRecording(int frameCount)
		{
			this._value.StartRecording(frameCount);
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		public void StopPlayback()
		{
			this._value.StopPlayback();
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x0002C5AD File Offset: 0x0002A7AD
		public void StopRecording()
		{
			this._value.StopRecording();
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0002C5BA File Offset: 0x0002A7BA
		public static int StringToHash(string name)
		{
			return Animator.StringToHash(name);
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x0002C5C2 File Offset: 0x0002A7C2
		public void Update(float deltaTime)
		{
			this._value.Update(deltaTime);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
		public void WriteDefaultValues()
		{
			this._value.WriteDefaultValues();
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0002C5DD File Offset: 0x0002A7DD
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x0002C5EB File Offset: 0x0002A7EB
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x0002C5F8 File Offset: 0x0002A7F8
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003149 RID: 12617
		[MoonSharpHidden]
		public Animator _value;
	}
}
