using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009DD RID: 2525
	[Proxy(typeof(MountedStabilizedTurret))]
	public class MountedStabilizedTurretProxy : IProxy
	{
		// Token: 0x060048CD RID: 18637 RVA: 0x000333CC File Offset: 0x000315CC
		[MoonSharpHidden]
		public MountedStabilizedTurretProxy(MountedStabilizedTurret value)
		{
			this._value = value;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000333DB File Offset: 0x000315DB
		public MountedStabilizedTurretProxy()
		{
			this._value = new MountedStabilizedTurret();
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x000333EE File Offset: 0x000315EE
		// (set) Token: 0x060048D0 RID: 18640 RVA: 0x00131718 File Offset: 0x0012F918
		public TransformProxy bearingTransform
		{
			get
			{
				return TransformProxy.New(this._value.bearingTransform);
			}
			set
			{
				Transform bearingTransform = null;
				if (value != null)
				{
					bearingTransform = value._value;
				}
				this._value.bearingTransform = bearingTransform;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x00033400 File Offset: 0x00031600
		// (set) Token: 0x060048D2 RID: 18642 RVA: 0x0003340D File Offset: 0x0003160D
		public float maxTurnSpeed
		{
			get
			{
				return this._value.maxTurnSpeed;
			}
			set
			{
				this._value.maxTurnSpeed = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x0003341B File Offset: 0x0003161B
		// (set) Token: 0x060048D4 RID: 18644 RVA: 0x00131740 File Offset: 0x0012F940
		public TransformProxy pitchTransform
		{
			get
			{
				return TransformProxy.New(this._value.pitchTransform);
			}
			set
			{
				Transform pitchTransform = null;
				if (value != null)
				{
					pitchTransform = value._value;
				}
				this._value.pitchTransform = pitchTransform;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0003342D File Offset: 0x0003162D
		// (set) Token: 0x060048D6 RID: 18646 RVA: 0x0003343A File Offset: 0x0003163A
		public float sensitivityX
		{
			get
			{
				return this._value.sensitivityX;
			}
			set
			{
				this._value.sensitivityX = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x00033448 File Offset: 0x00031648
		// (set) Token: 0x060048D8 RID: 18648 RVA: 0x00033455 File Offset: 0x00031655
		public float sensitivityY
		{
			get
			{
				return this._value.sensitivityY;
			}
			set
			{
				this._value.sensitivityY = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x00033463 File Offset: 0x00031663
		// (set) Token: 0x060048DA RID: 18650 RVA: 0x00033470 File Offset: 0x00031670
		public float springAmount
		{
			get
			{
				return this._value.springAmount;
			}
			set
			{
				this._value.springAmount = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x0003347E File Offset: 0x0003167E
		// (set) Token: 0x060048DC RID: 18652 RVA: 0x0003348B File Offset: 0x0003168B
		public float springDrag
		{
			get
			{
				return this._value.springDrag;
			}
			set
			{
				this._value.springDrag = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060048DD RID: 18653 RVA: 0x00033499 File Offset: 0x00031699
		// (set) Token: 0x060048DE RID: 18654 RVA: 0x000334A6 File Offset: 0x000316A6
		public float springForce
		{
			get
			{
				return this._value.springForce;
			}
			set
			{
				this._value.springForce = value;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x000334B4 File Offset: 0x000316B4
		// (set) Token: 0x060048E0 RID: 18656 RVA: 0x000334C6 File Offset: 0x000316C6
		public Vector2Proxy springMaxOffset
		{
			get
			{
				return Vector2Proxy.New(this._value.springMaxOffset);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.springMaxOffset = value._value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x000334E7 File Offset: 0x000316E7
		// (set) Token: 0x060048E2 RID: 18658 RVA: 0x000334F4 File Offset: 0x000316F4
		public bool stabilizeX
		{
			get
			{
				return this._value.stabilizeX;
			}
			set
			{
				this._value.stabilizeX = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x00033502 File Offset: 0x00031702
		// (set) Token: 0x060048E4 RID: 18660 RVA: 0x0003350F File Offset: 0x0003170F
		public bool stabilizeY
		{
			get
			{
				return this._value.stabilizeY;
			}
			set
			{
				this._value.stabilizeY = value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x0003351D File Offset: 0x0003171D
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x0003352A File Offset: 0x0003172A
		public bool useMaxTurnSpeed
		{
			get
			{
				return this._value.useMaxTurnSpeed;
			}
			set
			{
				this._value.useMaxTurnSpeed = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x00033538 File Offset: 0x00031738
		// (set) Token: 0x060048E8 RID: 18664 RVA: 0x00033545 File Offset: 0x00031745
		public bool useSpring
		{
			get
			{
				return this._value.useSpring;
			}
			set
			{
				this._value.useSpring = value;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060048E9 RID: 18665 RVA: 0x00033553 File Offset: 0x00031753
		// (set) Token: 0x060048EA RID: 18666 RVA: 0x00131768 File Offset: 0x0012F968
		public CameraProxy aimCamera
		{
			get
			{
				return CameraProxy.New(this._value.aimCamera);
			}
			set
			{
				Camera aimCamera = null;
				if (value != null)
				{
					aimCamera = value._value;
				}
				this._value.aimCamera = aimCamera;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x00033565 File Offset: 0x00031765
		// (set) Token: 0x060048EC RID: 18668 RVA: 0x00033572 File Offset: 0x00031772
		public float aimChangeSpeed
		{
			get
			{
				return this._value.aimChangeSpeed;
			}
			set
			{
				this._value.aimChangeSpeed = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x00033580 File Offset: 0x00031780
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x00131790 File Offset: 0x0012F990
		public CameraProxy overrideCamera
		{
			get
			{
				return CameraProxy.New(this._value.overrideCamera);
			}
			set
			{
				Camera overrideCamera = null;
				if (value != null)
				{
					overrideCamera = value._value;
				}
				this._value.overrideCamera = overrideCamera;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00033592 File Offset: 0x00031792
		// (set) Token: 0x060048F0 RID: 18672 RVA: 0x001317B8 File Offset: 0x0012F9B8
		public SeatProxy seat
		{
			get
			{
				return SeatProxy.New(this._value.seat);
			}
			set
			{
				Seat seat = null;
				if (value != null)
				{
					seat = value._value;
				}
				this._value.seat = seat;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x000335A4 File Offset: 0x000317A4
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x000335B1 File Offset: 0x000317B1
		public float vehicleRigidbodyRecoilForce
		{
			get
			{
				return this._value.vehicleRigidbodyRecoilForce;
			}
			set
			{
				this._value.vehicleRigidbodyRecoilForce = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x000335BF File Offset: 0x000317BF
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x000335D1 File Offset: 0x000317D1
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x000335E3 File Offset: 0x000317E3
		public bool canFire
		{
			get
			{
				return WWeapon.CanFire(this._value);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x000335F0 File Offset: 0x000317F0
		public int activeSightModeIndex
		{
			get
			{
				return WWeapon.GetActiveSightModeIndex(this._value);
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x000335FD File Offset: 0x000317FD
		public WeaponProxy activeSubWeapon
		{
			get
			{
				return WeaponProxy.New(WWeapon.GetActiveSubWeapon(this._value));
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0003360F File Offset: 0x0003180F
		public int activeSubWeaponIndex
		{
			get
			{
				return WWeapon.GetActiveSubWeaponIndex(this._value);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x0003361C File Offset: 0x0003181C
		// (set) Token: 0x060048FA RID: 18682 RVA: 0x00033629 File Offset: 0x00031829
		public float aimFov
		{
			get
			{
				return WWeapon.GetAimFov(this._value);
			}
			set
			{
				WWeapon.SetAimFov(this._value, value);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060048FB RID: 18683 RVA: 0x00033637 File Offset: 0x00031837
		public Weapon[] alternativeWeapons
		{
			get
			{
				return WWeapon.GetAlternativeWeapons(this._value);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x00033644 File Offset: 0x00031844
		// (set) Token: 0x060048FD RID: 18685 RVA: 0x00033651 File Offset: 0x00031851
		public int ammo
		{
			get
			{
				return WWeapon.GetAmmo(this._value);
			}
			set
			{
				WWeapon.SetAmmo(this._value, value);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060048FE RID: 18686 RVA: 0x0003365F File Offset: 0x0003185F
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(WWeapon.GetAnimator(this._value));
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x00033671 File Offset: 0x00031871
		// (set) Token: 0x06004900 RID: 18688 RVA: 0x0003367E File Offset: 0x0003187E
		public bool applyHeat
		{
			get
			{
				return WWeapon.GetApplyHeat(this._value);
			}
			set
			{
				WWeapon.SetApplyHeat(this._value, value);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06004901 RID: 18689 RVA: 0x0003368C File Offset: 0x0003188C
		// (set) Token: 0x06004902 RID: 18690 RVA: 0x00033699 File Offset: 0x00031899
		public float baseSpread
		{
			get
			{
				return WWeapon.GetBaseSpread(this._value);
			}
			set
			{
				WWeapon.SetBaseSpread(this._value, value);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x000336A7 File Offset: 0x000318A7
		// (set) Token: 0x06004904 RID: 18692 RVA: 0x000336B4 File Offset: 0x000318B4
		public float chargeTime
		{
			get
			{
				return WWeapon.GetChargeTime(this._value);
			}
			set
			{
				WWeapon.SetChargeTime(this._value, value);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x000336C2 File Offset: 0x000318C2
		// (set) Token: 0x06004906 RID: 18694 RVA: 0x000336CF File Offset: 0x000318CF
		public float cooldown
		{
			get
			{
				return WWeapon.GetCooldown(this._value);
			}
			set
			{
				WWeapon.SetCooldown(this._value, value);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x000336DD File Offset: 0x000318DD
		public TransformProxy currentMuzzleTransform
		{
			get
			{
				return TransformProxy.New(WWeapon.GetCurrentMuzzleTransform(this._value));
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x000336EF File Offset: 0x000318EF
		public float currentSpreadMagnitude
		{
			get
			{
				return WWeapon.GetCurrentSpreadMagnitude(this._value);
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06004909 RID: 18697 RVA: 0x000336FC File Offset: 0x000318FC
		public float currentSpreadMaxAngleRadians
		{
			get
			{
				return WWeapon.GetCurrentSpreadMaxAngleRadians(this._value);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x00033709 File Offset: 0x00031909
		// (set) Token: 0x0600490B RID: 18699 RVA: 0x00033716 File Offset: 0x00031916
		public Weapon.Difficulty difficultyAir
		{
			get
			{
				return WWeapon.GetDifficultyAir(this._value);
			}
			set
			{
				WWeapon.SetDifficultyAir(this._value, value);
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x00033724 File Offset: 0x00031924
		// (set) Token: 0x0600490D RID: 18701 RVA: 0x00033731 File Offset: 0x00031931
		public Weapon.Difficulty difficultyAirFastMover
		{
			get
			{
				return WWeapon.GetDifficultyAirFastMover(this._value);
			}
			set
			{
				WWeapon.SetDifficultyAirFastMover(this._value, value);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x0003373F File Offset: 0x0003193F
		// (set) Token: 0x0600490F RID: 18703 RVA: 0x0003374C File Offset: 0x0003194C
		public Weapon.Difficulty difficultyGroundVehicles
		{
			get
			{
				return WWeapon.GetDifficultyGroundVehicles(this._value);
			}
			set
			{
				WWeapon.SetDifficultyGroundVehicles(this._value, value);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x0003375A File Offset: 0x0003195A
		// (set) Token: 0x06004911 RID: 18705 RVA: 0x00033767 File Offset: 0x00031967
		public Weapon.Difficulty difficultyInfantry
		{
			get
			{
				return WWeapon.GetDifficultyInfantry(this._value);
			}
			set
			{
				WWeapon.SetDifficultyInfantry(this._value, value);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x00033775 File Offset: 0x00031975
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x00033782 File Offset: 0x00031982
		public Weapon.Difficulty difficultyInfantryGroup
		{
			get
			{
				return WWeapon.GetDifficultyInfantryGroup(this._value);
			}
			set
			{
				WWeapon.SetDifficultyInfantryGroup(this._value, value);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x00033790 File Offset: 0x00031990
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x0003379D File Offset: 0x0003199D
		public Weapon.Effectiveness effectivenessAir
		{
			get
			{
				return WWeapon.GetEffectivenessAir(this._value);
			}
			set
			{
				WWeapon.SetEffectivenessAir(this._value, value);
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x000337AB File Offset: 0x000319AB
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x000337B8 File Offset: 0x000319B8
		public Weapon.Effectiveness effectivenessAirFastMover
		{
			get
			{
				return WWeapon.GetEffectivenessAirFastMover(this._value);
			}
			set
			{
				WWeapon.SetEffectivenessAirFastMover(this._value, value);
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x000337C6 File Offset: 0x000319C6
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x000337D3 File Offset: 0x000319D3
		public Weapon.Effectiveness effectivenessArmored
		{
			get
			{
				return WWeapon.GetEffectivenessArmored(this._value);
			}
			set
			{
				WWeapon.SetEffectivenessArmored(this._value, value);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x000337E1 File Offset: 0x000319E1
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x000337EE File Offset: 0x000319EE
		public Weapon.Effectiveness effectivenessInfantry
		{
			get
			{
				return WWeapon.GetEffectivenessInfantry(this._value);
			}
			set
			{
				WWeapon.SetEffectivenessInfantry(this._value, value);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x000337FC File Offset: 0x000319FC
		// (set) Token: 0x0600491D RID: 18717 RVA: 0x00033809 File Offset: 0x00031A09
		public Weapon.Effectiveness effectivenessInfantryGroup
		{
			get
			{
				return WWeapon.GetEffectivenessInfantryGroup(this._value);
			}
			set
			{
				WWeapon.SetEffectivenessInfantryGroup(this._value, value);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x00033817 File Offset: 0x00031A17
		// (set) Token: 0x0600491F RID: 18719 RVA: 0x00033824 File Offset: 0x00031A24
		public Weapon.Effectiveness effectivenessUnarmored
		{
			get
			{
				return WWeapon.GetEffectivenessUnarmored(this._value);
			}
			set
			{
				WWeapon.SetEffectivenessUnarmored(this._value, value);
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x00033832 File Offset: 0x00031A32
		// (set) Token: 0x06004921 RID: 18721 RVA: 0x0003383F File Offset: 0x00031A3F
		public float effectiveRange
		{
			get
			{
				return WWeapon.GetEffectiveRange(this._value);
			}
			set
			{
				WWeapon.SetEffectiveRange(this._value, value);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x0003384D File Offset: 0x00031A4D
		// (set) Token: 0x06004923 RID: 18723 RVA: 0x0003385F File Offset: 0x00031A5F
		public WFollowupSpreadProxy followupSpread
		{
			get
			{
				return WFollowupSpreadProxy.New(WWeapon.GetFollowupSpread(this._value));
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				WWeapon.SetFollowupSpread(this._value, value._value);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06004924 RID: 18724 RVA: 0x00033880 File Offset: 0x00031A80
		public bool hasAdvancedReload
		{
			get
			{
				return WWeapon.GetHasAdvancedReload(this._value);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x0003388D File Offset: 0x00031A8D
		// (set) Token: 0x06004926 RID: 18726 RVA: 0x0003389A File Offset: 0x00031A9A
		public float heat
		{
			get
			{
				return WWeapon.GetHeat(this._value);
			}
			set
			{
				WWeapon.SetHeat(this._value, value);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x000338A8 File Offset: 0x00031AA8
		// (set) Token: 0x06004928 RID: 18728 RVA: 0x000338B5 File Offset: 0x00031AB5
		public float heatDrainRate
		{
			get
			{
				return WWeapon.GetHeatDrainRate(this._value);
			}
			set
			{
				WWeapon.SetHeatDrainRate(this._value, value);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x000338C3 File Offset: 0x00031AC3
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x000338D0 File Offset: 0x00031AD0
		public float heatGainPerShot
		{
			get
			{
				return WWeapon.GetHeatGainPerShot(this._value);
			}
			set
			{
				WWeapon.SetHeatGainPerShot(this._value, value);
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x000338DE File Offset: 0x00031ADE
		// (set) Token: 0x0600492C RID: 18732 RVA: 0x000338EB File Offset: 0x00031AEB
		public bool isAuto
		{
			get
			{
				return WWeapon.GetIsAuto(this._value);
			}
			set
			{
				WWeapon.SetIsAuto(this._value, value);
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x000338F9 File Offset: 0x00031AF9
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x00033906 File Offset: 0x00031B06
		public bool isLocked
		{
			get
			{
				return WWeapon.GetIsLocked(this._value);
			}
			set
			{
				WWeapon.SetIsLocked(this._value, value);
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x00033914 File Offset: 0x00031B14
		// (set) Token: 0x06004930 RID: 18736 RVA: 0x00033921 File Offset: 0x00031B21
		public bool isLoud
		{
			get
			{
				return WWeapon.GetIsLoud(this._value);
			}
			set
			{
				WWeapon.SetIsLoud(this._value, value);
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x0003392F File Offset: 0x00031B2F
		// (set) Token: 0x06004932 RID: 18738 RVA: 0x001317E0 File Offset: 0x0012F9E0
		public ActorProxy killCredit
		{
			get
			{
				return ActorProxy.New(WWeapon.GetKillCredit(this._value));
			}
			set
			{
				Actor killCredit = null;
				if (value != null)
				{
					killCredit = value._value;
				}
				WWeapon.SetKillCredit(this._value, killCredit);
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x00033941 File Offset: 0x00031B41
		// (set) Token: 0x06004934 RID: 18740 RVA: 0x0003394E File Offset: 0x00031B4E
		public int maxAmmo
		{
			get
			{
				return WWeapon.GetMaxAmmo(this._value);
			}
			set
			{
				WWeapon.SetMaxAmmo(this._value, value);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x0003395C File Offset: 0x00031B5C
		// (set) Token: 0x06004936 RID: 18742 RVA: 0x00033969 File Offset: 0x00031B69
		public int maxSpareAmmo
		{
			get
			{
				return WWeapon.GetMaxSpareAmmo(this._value);
			}
			set
			{
				WWeapon.SetMaxSpareAmmo(this._value, value);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x00033977 File Offset: 0x00031B77
		public Transform[] muzzleTransforms
		{
			get
			{
				return WWeapon.GetMuzzleTransforms(this._value);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x00033984 File Offset: 0x00031B84
		// (set) Token: 0x06004939 RID: 18745 RVA: 0x00033991 File Offset: 0x00031B91
		public int projectilesPerShot
		{
			get
			{
				return WWeapon.GetProjectilesPerShot(this._value);
			}
			set
			{
				WWeapon.SetProjectilesPerShot(this._value, value);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x0003399F File Offset: 0x00031B9F
		// (set) Token: 0x0600493B RID: 18747 RVA: 0x000339AC File Offset: 0x00031BAC
		public float recoilBaseKickback
		{
			get
			{
				return WWeapon.GetRecoilBaseKickback(this._value);
			}
			set
			{
				WWeapon.SetRecoilBaseKickback(this._value, value);
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x000339BA File Offset: 0x00031BBA
		// (set) Token: 0x0600493D RID: 18749 RVA: 0x000339C7 File Offset: 0x00031BC7
		public float recoilKickbackProneMultiplier
		{
			get
			{
				return WWeapon.GetRecoilKickbackProneMultiplier(this._value);
			}
			set
			{
				WWeapon.SetRecoilKickbackProneMultiplier(this._value, value);
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x000339D5 File Offset: 0x00031BD5
		// (set) Token: 0x0600493F RID: 18751 RVA: 0x000339E2 File Offset: 0x00031BE2
		public float recoilRandomKickback
		{
			get
			{
				return WWeapon.GetRecoilRandomKickback(this._value);
			}
			set
			{
				WWeapon.SetRecoilRandomKickback(this._value, value);
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06004940 RID: 18752 RVA: 0x000339F0 File Offset: 0x00031BF0
		// (set) Token: 0x06004941 RID: 18753 RVA: 0x000339FD File Offset: 0x00031BFD
		public float recoilSnapDuration
		{
			get
			{
				return WWeapon.GetRecoilSnapDuration(this._value);
			}
			set
			{
				WWeapon.SetRecoilSnapDuration(this._value, value);
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x00033A0B File Offset: 0x00031C0B
		// (set) Token: 0x06004943 RID: 18755 RVA: 0x00033A18 File Offset: 0x00031C18
		public float recoilSnapFrequency
		{
			get
			{
				return WWeapon.GetRecoilSnapFrequency(this._value);
			}
			set
			{
				WWeapon.SetRecoilSnapFrequency(this._value, value);
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x00033A26 File Offset: 0x00031C26
		// (set) Token: 0x06004945 RID: 18757 RVA: 0x00033A33 File Offset: 0x00031C33
		public float recoilSnapMagnitude
		{
			get
			{
				return WWeapon.GetRecoilSnapMagnitude(this._value);
			}
			set
			{
				WWeapon.SetRecoilSnapMagnitude(this._value, value);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x00033A41 File Offset: 0x00031C41
		// (set) Token: 0x06004947 RID: 18759 RVA: 0x00033A4E File Offset: 0x00031C4E
		public float recoilSnapProneMultiplier
		{
			get
			{
				return WWeapon.GetRecoilSnapProneMultiplier(this._value);
			}
			set
			{
				WWeapon.SetRecoilSnapProneMultiplier(this._value, value);
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x00033A5C File Offset: 0x00031C5C
		// (set) Token: 0x06004949 RID: 18761 RVA: 0x00033A69 File Offset: 0x00031C69
		public float reloadTime
		{
			get
			{
				return WWeapon.GetReloadTime(this._value);
			}
			set
			{
				WWeapon.SetReloadTime(this._value, value);
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x00033A77 File Offset: 0x00031C77
		public GameObjectProxy scopeAimObject
		{
			get
			{
				return GameObjectProxy.New(WWeapon.GetScopeAimObject(this._value));
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x00033A89 File Offset: 0x00031C89
		public int slot
		{
			get
			{
				return WWeapon.GetSlot(this._value);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x00033A96 File Offset: 0x00031C96
		// (set) Token: 0x0600494D RID: 18765 RVA: 0x00033AA3 File Offset: 0x00031CA3
		public int spareAmmo
		{
			get
			{
				return WWeapon.GetSpareAmmo(this._value);
			}
			set
			{
				WWeapon.SetSpareAmmo(this._value, value);
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600494E RID: 18766 RVA: 0x00033AB1 File Offset: 0x00031CB1
		public Vector3Proxy thirdPersonOffset
		{
			get
			{
				return Vector3Proxy.New(WWeapon.GetThirdPersonOffset(this._value));
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x00033AC3 File Offset: 0x00031CC3
		public QuaternionProxy thirdPersonRotation
		{
			get
			{
				return QuaternionProxy.New(WWeapon.GetThirdPersonRotation(this._value));
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x00033AD5 File Offset: 0x00031CD5
		public float thirdPersonScale
		{
			get
			{
				return WWeapon.GetThirdPersonScale(this._value);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06004951 RID: 18769 RVA: 0x00033AE2 File Offset: 0x00031CE2
		public SpriteProxy uiSprite
		{
			get
			{
				return SpriteProxy.New(WWeapon.GetUiSprite(this._value));
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x00033AF4 File Offset: 0x00031CF4
		// (set) Token: 0x06004953 RID: 18771 RVA: 0x00033B01 File Offset: 0x00031D01
		public float unholsterTime
		{
			get
			{
				return WWeapon.GetUnholsterTime(this._value);
			}
			set
			{
				WWeapon.SetUnholsterTime(this._value, value);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x00033B0F File Offset: 0x00031D0F
		// (set) Token: 0x06004955 RID: 18773 RVA: 0x00033B1C File Offset: 0x00031D1C
		public bool useChargeTime
		{
			get
			{
				return WWeapon.GetUseChargeTime(this._value);
			}
			set
			{
				WWeapon.SetUseChargeTime(this._value, value);
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x00033B2A File Offset: 0x00031D2A
		public ActorProxy user
		{
			get
			{
				return ActorProxy.New(WWeapon.GetUser(this._value));
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x00033B3C File Offset: 0x00031D3C
		public WeaponEntryProxy weaponEntry
		{
			get
			{
				return WeaponEntryProxy.New(WWeapon.GetWeaponEntry(this._value));
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x00033B4E File Offset: 0x00031D4E
		public bool hasLoadedAmmo
		{
			get
			{
				return WWeapon.HasLoadedAmmo(this._value);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x00033B5B File Offset: 0x00031D5B
		public bool hasSpareAmmo
		{
			get
			{
				return WWeapon.HasSpareAmmo(this._value);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x00033B68 File Offset: 0x00031D68
		public bool isAiming
		{
			get
			{
				return WWeapon.IsAiming(this._value);
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600495B RID: 18779 RVA: 0x00033B75 File Offset: 0x00031D75
		public bool isCharged
		{
			get
			{
				return WWeapon.IsCharged(this._value);
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x00033B82 File Offset: 0x00031D82
		public bool isCoolingDown
		{
			get
			{
				return WWeapon.IsCoolingDown(this._value);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x00033B8F File Offset: 0x00031D8F
		public bool isEmpty
		{
			get
			{
				return WWeapon.IsEmpty(this._value);
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x00033B9C File Offset: 0x00031D9C
		public bool isFull
		{
			get
			{
				return WWeapon.IsFull(this._value);
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x00033BA9 File Offset: 0x00031DA9
		public bool isHoldingFire
		{
			get
			{
				return WWeapon.IsHoldingFire(this._value);
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x00033BB6 File Offset: 0x00031DB6
		public bool isOverheating
		{
			get
			{
				return WWeapon.IsOverheating(this._value);
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06004961 RID: 18785 RVA: 0x00033BC3 File Offset: 0x00031DC3
		public bool isReloading
		{
			get
			{
				return WWeapon.IsReloading(this._value);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x00033BD0 File Offset: 0x00031DD0
		public bool isUnholstered
		{
			get
			{
				return WWeapon.IsUnholstered(this._value);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x00033BDD File Offset: 0x00031DDD
		public ScriptEventProxy onFire
		{
			get
			{
				return ScriptEventProxy.New(WWeapon.GetOnFire(this._value));
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x00033BEF File Offset: 0x00031DEF
		public ScriptEventProxy onSpawnProjectiles
		{
			get
			{
				return ScriptEventProxy.New(WWeapon.GetOnSpawnProjectiles(this._value));
			}
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x00033C01 File Offset: 0x00031E01
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00131808 File Offset: 0x0012FA08
		[MoonSharpHidden]
		public static MountedStabilizedTurretProxy New(MountedStabilizedTurret value)
		{
			if (value == null)
			{
				return null;
			}
			MountedStabilizedTurretProxy mountedStabilizedTurretProxy = (MountedStabilizedTurretProxy)ObjectCache.Get(typeof(MountedStabilizedTurretProxy), value);
			if (mountedStabilizedTurretProxy == null)
			{
				mountedStabilizedTurretProxy = new MountedStabilizedTurretProxy(value);
				ObjectCache.Add(typeof(MountedStabilizedTurretProxy), value, mountedStabilizedTurretProxy);
			}
			return mountedStabilizedTurretProxy;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x00033C09 File Offset: 0x00031E09
		[MoonSharpUserDataMetamethod("__call")]
		public static MountedStabilizedTurretProxy Call(DynValue _)
		{
			return new MountedStabilizedTurretProxy();
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x00033C10 File Offset: 0x00031E10
		public bool CanAimAt(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return this._value.CanAimAt(position._value);
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00033C31 File Offset: 0x00031E31
		public Vector3Proxy GetClampedTurretRandomLookDirection()
		{
			return Vector3Proxy.New(this._value.GetClampedTurretRandomLookDirection());
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x00033C43 File Offset: 0x00031E43
		public void GetNotchedClampYValues(float bearing, out float min, out float max)
		{
			min = 0f;
			max = 0f;
			this._value.GetNotchedClampYValues(bearing, out min, out max);
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x00033C61 File Offset: 0x00031E61
		public bool IsClampedTurret()
		{
			return this._value.IsClampedTurret();
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x00033C6E File Offset: 0x00031E6E
		public void Unholster()
		{
			this._value.Unholster();
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x00033C7B File Offset: 0x00031E7B
		public void AssignFpVehicleAudioMix()
		{
			this._value.AssignFpVehicleAudioMix();
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x00033C88 File Offset: 0x00031E88
		public void AssignPlayerVehicleAudioMix()
		{
			this._value.AssignPlayerVehicleAudioMix();
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x00033C95 File Offset: 0x00031E95
		public bool CanFire()
		{
			return this._value.CanFire();
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x00033CA2 File Offset: 0x00031EA2
		public void Fire(Vector3Proxy direction, bool useMuzzleDirection)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			this._value.Fire(direction._value, useMuzzleDirection);
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x00033CC4 File Offset: 0x00031EC4
		public void Hide()
		{
			this._value.Hide();
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x00033CD1 File Offset: 0x00031ED1
		public void Holster()
		{
			this._value.Holster();
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x00033CDE File Offset: 0x00031EDE
		public bool IsMountedWeapon()
		{
			return this._value.IsMountedWeapon();
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x00033CEB File Offset: 0x00031EEB
		public void OnWeaponFire(Vector3Proxy muzzlePosition, Vector3Proxy fireDirection)
		{
			if (muzzlePosition == null)
			{
				throw new ScriptRuntimeException("argument 'muzzlePosition' is nil");
			}
			if (fireDirection == null)
			{
				throw new ScriptRuntimeException("argument 'fireDirection' is nil");
			}
			this._value.OnWeaponFire(muzzlePosition._value, fireDirection._value);
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x00033D20 File Offset: 0x00031F20
		public void ResetAudioMix()
		{
			this._value.ResetAudioMix();
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x00033D2D File Offset: 0x00031F2D
		public void Show()
		{
			this._value.Show();
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x00131854 File Offset: 0x0012FA54
		public int AddSubWeapon(WeaponProxy subWeapon)
		{
			Weapon subWeapon2 = null;
			if (subWeapon != null)
			{
				subWeapon2 = subWeapon._value;
			}
			return WWeapon.AddSubWeapon(this._value, subWeapon2);
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x00033D3A File Offset: 0x00031F3A
		public void EquipSubWeapon(int subWeaponIndex)
		{
			WWeapon.EquipSubWeapon(this._value, subWeaponIndex);
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x00033D48 File Offset: 0x00031F48
		public Weapon.WeaponRole GenerateWeaponRoleFromStats()
		{
			return WWeapon.GenerateWeaponRoleFromStats(this._value);
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x00033D55 File Offset: 0x00031F55
		public void InstantlyReload()
		{
			WWeapon.InstantlyReload(this._value);
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00033D62 File Offset: 0x00031F62
		public void LockWeapon()
		{
			WWeapon.LockWeapon(this._value);
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00033D6F File Offset: 0x00031F6F
		public void NextSightMode()
		{
			WWeapon.NextSightMode(this._value);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00033D7C File Offset: 0x00031F7C
		public void NextSubWeapon()
		{
			WWeapon.NextSubWeapon(this._value);
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00033D89 File Offset: 0x00031F89
		public void PreviousSightMode()
		{
			WWeapon.PreviousSightMode(this._value);
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00033D96 File Offset: 0x00031F96
		public void Reload()
		{
			WWeapon.Reload(this._value);
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x00033DA3 File Offset: 0x00031FA3
		public void RemoveSubWeapon(int subWeaponIndex)
		{
			WWeapon.RemoveSubWeapon(this._value, subWeaponIndex);
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x0013187C File Offset: 0x0012FA7C
		public void RemoveSubWeapon(WeaponProxy subWeapon)
		{
			Weapon subWeapon2 = null;
			if (subWeapon != null)
			{
				subWeapon2 = subWeapon._value;
			}
			WWeapon.RemoveSubWeapon(this._value, subWeapon2);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x001318A4 File Offset: 0x0012FAA4
		public void SetProjectilePrefab(GameObjectProxy prefab)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			WWeapon.SetProjectilePrefab(this._value, prefab2);
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00033DB1 File Offset: 0x00031FB1
		public void Shoot()
		{
			WWeapon.Shoot(this._value);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x00033DBE File Offset: 0x00031FBE
		public void Shoot(bool force)
		{
			WWeapon.Shoot(this._value, force);
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x00033DCC File Offset: 0x00031FCC
		public void UnlockWeapon()
		{
			WWeapon.UnlockWeapon(this._value);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x00033DD9 File Offset: 0x00031FD9
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003175 RID: 12661
		[MoonSharpHidden]
		public MountedStabilizedTurret _value;
	}
}
