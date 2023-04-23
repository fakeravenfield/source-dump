using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009DE RID: 2526
	[Proxy(typeof(MountedWeapon))]
	public class MountedWeaponProxy : IProxy
	{
		// Token: 0x06004987 RID: 18823 RVA: 0x00033DE6 File Offset: 0x00031FE6
		[MoonSharpHidden]
		public MountedWeaponProxy(MountedWeapon value)
		{
			this._value = value;
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00033DF5 File Offset: 0x00031FF5
		public MountedWeaponProxy()
		{
			this._value = new MountedWeapon();
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x00033E08 File Offset: 0x00032008
		// (set) Token: 0x0600498A RID: 18826 RVA: 0x001318CC File Offset: 0x0012FACC
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

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x00033E1A File Offset: 0x0003201A
		// (set) Token: 0x0600498C RID: 18828 RVA: 0x00033E27 File Offset: 0x00032027
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

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x00033E35 File Offset: 0x00032035
		// (set) Token: 0x0600498E RID: 18830 RVA: 0x001318F4 File Offset: 0x0012FAF4
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

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x00033E47 File Offset: 0x00032047
		// (set) Token: 0x06004990 RID: 18832 RVA: 0x0013191C File Offset: 0x0012FB1C
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

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x00033E59 File Offset: 0x00032059
		// (set) Token: 0x06004992 RID: 18834 RVA: 0x00033E66 File Offset: 0x00032066
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

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x00033E74 File Offset: 0x00032074
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x00033E86 File Offset: 0x00032086
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x00033E98 File Offset: 0x00032098
		public bool canFire
		{
			get
			{
				return WWeapon.CanFire(this._value);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x00033EA5 File Offset: 0x000320A5
		public int activeSightModeIndex
		{
			get
			{
				return WWeapon.GetActiveSightModeIndex(this._value);
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x00033EB2 File Offset: 0x000320B2
		public WeaponProxy activeSubWeapon
		{
			get
			{
				return WeaponProxy.New(WWeapon.GetActiveSubWeapon(this._value));
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x00033EC4 File Offset: 0x000320C4
		public int activeSubWeaponIndex
		{
			get
			{
				return WWeapon.GetActiveSubWeaponIndex(this._value);
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x00033ED1 File Offset: 0x000320D1
		// (set) Token: 0x0600499A RID: 18842 RVA: 0x00033EDE File Offset: 0x000320DE
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

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x00033EEC File Offset: 0x000320EC
		public Weapon[] alternativeWeapons
		{
			get
			{
				return WWeapon.GetAlternativeWeapons(this._value);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x00033EF9 File Offset: 0x000320F9
		// (set) Token: 0x0600499D RID: 18845 RVA: 0x00033F06 File Offset: 0x00032106
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

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x00033F14 File Offset: 0x00032114
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(WWeapon.GetAnimator(this._value));
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x00033F26 File Offset: 0x00032126
		// (set) Token: 0x060049A0 RID: 18848 RVA: 0x00033F33 File Offset: 0x00032133
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

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x00033F41 File Offset: 0x00032141
		// (set) Token: 0x060049A2 RID: 18850 RVA: 0x00033F4E File Offset: 0x0003214E
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

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x00033F5C File Offset: 0x0003215C
		// (set) Token: 0x060049A4 RID: 18852 RVA: 0x00033F69 File Offset: 0x00032169
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

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x00033F77 File Offset: 0x00032177
		// (set) Token: 0x060049A6 RID: 18854 RVA: 0x00033F84 File Offset: 0x00032184
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

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x00033F92 File Offset: 0x00032192
		public TransformProxy currentMuzzleTransform
		{
			get
			{
				return TransformProxy.New(WWeapon.GetCurrentMuzzleTransform(this._value));
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x00033FA4 File Offset: 0x000321A4
		public float currentSpreadMagnitude
		{
			get
			{
				return WWeapon.GetCurrentSpreadMagnitude(this._value);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x00033FB1 File Offset: 0x000321B1
		public float currentSpreadMaxAngleRadians
		{
			get
			{
				return WWeapon.GetCurrentSpreadMaxAngleRadians(this._value);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x00033FBE File Offset: 0x000321BE
		// (set) Token: 0x060049AB RID: 18859 RVA: 0x00033FCB File Offset: 0x000321CB
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

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x00033FD9 File Offset: 0x000321D9
		// (set) Token: 0x060049AD RID: 18861 RVA: 0x00033FE6 File Offset: 0x000321E6
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

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x00033FF4 File Offset: 0x000321F4
		// (set) Token: 0x060049AF RID: 18863 RVA: 0x00034001 File Offset: 0x00032201
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

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x0003400F File Offset: 0x0003220F
		// (set) Token: 0x060049B1 RID: 18865 RVA: 0x0003401C File Offset: 0x0003221C
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

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x0003402A File Offset: 0x0003222A
		// (set) Token: 0x060049B3 RID: 18867 RVA: 0x00034037 File Offset: 0x00032237
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

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x00034045 File Offset: 0x00032245
		// (set) Token: 0x060049B5 RID: 18869 RVA: 0x00034052 File Offset: 0x00032252
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

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x00034060 File Offset: 0x00032260
		// (set) Token: 0x060049B7 RID: 18871 RVA: 0x0003406D File Offset: 0x0003226D
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

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060049B8 RID: 18872 RVA: 0x0003407B File Offset: 0x0003227B
		// (set) Token: 0x060049B9 RID: 18873 RVA: 0x00034088 File Offset: 0x00032288
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

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x00034096 File Offset: 0x00032296
		// (set) Token: 0x060049BB RID: 18875 RVA: 0x000340A3 File Offset: 0x000322A3
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

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x000340B1 File Offset: 0x000322B1
		// (set) Token: 0x060049BD RID: 18877 RVA: 0x000340BE File Offset: 0x000322BE
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

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x000340CC File Offset: 0x000322CC
		// (set) Token: 0x060049BF RID: 18879 RVA: 0x000340D9 File Offset: 0x000322D9
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

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060049C0 RID: 18880 RVA: 0x000340E7 File Offset: 0x000322E7
		// (set) Token: 0x060049C1 RID: 18881 RVA: 0x000340F4 File Offset: 0x000322F4
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

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060049C2 RID: 18882 RVA: 0x00034102 File Offset: 0x00032302
		// (set) Token: 0x060049C3 RID: 18883 RVA: 0x00034114 File Offset: 0x00032314
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

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x00034135 File Offset: 0x00032335
		public bool hasAdvancedReload
		{
			get
			{
				return WWeapon.GetHasAdvancedReload(this._value);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x00034142 File Offset: 0x00032342
		// (set) Token: 0x060049C6 RID: 18886 RVA: 0x0003414F File Offset: 0x0003234F
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

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x060049C8 RID: 18888 RVA: 0x0003416A File Offset: 0x0003236A
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

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x00034178 File Offset: 0x00032378
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x00034185 File Offset: 0x00032385
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

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x00034193 File Offset: 0x00032393
		// (set) Token: 0x060049CC RID: 18892 RVA: 0x000341A0 File Offset: 0x000323A0
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

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x000341AE File Offset: 0x000323AE
		// (set) Token: 0x060049CE RID: 18894 RVA: 0x000341BB File Offset: 0x000323BB
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

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x000341C9 File Offset: 0x000323C9
		// (set) Token: 0x060049D0 RID: 18896 RVA: 0x000341D6 File Offset: 0x000323D6
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

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x000341E4 File Offset: 0x000323E4
		// (set) Token: 0x060049D2 RID: 18898 RVA: 0x00131944 File Offset: 0x0012FB44
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

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x000341F6 File Offset: 0x000323F6
		// (set) Token: 0x060049D4 RID: 18900 RVA: 0x00034203 File Offset: 0x00032403
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

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x00034211 File Offset: 0x00032411
		// (set) Token: 0x060049D6 RID: 18902 RVA: 0x0003421E File Offset: 0x0003241E
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

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x0003422C File Offset: 0x0003242C
		public Transform[] muzzleTransforms
		{
			get
			{
				return WWeapon.GetMuzzleTransforms(this._value);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x00034239 File Offset: 0x00032439
		// (set) Token: 0x060049D9 RID: 18905 RVA: 0x00034246 File Offset: 0x00032446
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

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x00034254 File Offset: 0x00032454
		// (set) Token: 0x060049DB RID: 18907 RVA: 0x00034261 File Offset: 0x00032461
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

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x0003426F File Offset: 0x0003246F
		// (set) Token: 0x060049DD RID: 18909 RVA: 0x0003427C File Offset: 0x0003247C
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

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x0003428A File Offset: 0x0003248A
		// (set) Token: 0x060049DF RID: 18911 RVA: 0x00034297 File Offset: 0x00032497
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

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x000342A5 File Offset: 0x000324A5
		// (set) Token: 0x060049E1 RID: 18913 RVA: 0x000342B2 File Offset: 0x000324B2
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

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x000342C0 File Offset: 0x000324C0
		// (set) Token: 0x060049E3 RID: 18915 RVA: 0x000342CD File Offset: 0x000324CD
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

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x000342DB File Offset: 0x000324DB
		// (set) Token: 0x060049E5 RID: 18917 RVA: 0x000342E8 File Offset: 0x000324E8
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

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x000342F6 File Offset: 0x000324F6
		// (set) Token: 0x060049E7 RID: 18919 RVA: 0x00034303 File Offset: 0x00032503
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

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060049E8 RID: 18920 RVA: 0x00034311 File Offset: 0x00032511
		// (set) Token: 0x060049E9 RID: 18921 RVA: 0x0003431E File Offset: 0x0003251E
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

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x0003432C File Offset: 0x0003252C
		public GameObjectProxy scopeAimObject
		{
			get
			{
				return GameObjectProxy.New(WWeapon.GetScopeAimObject(this._value));
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x0003433E File Offset: 0x0003253E
		public int slot
		{
			get
			{
				return WWeapon.GetSlot(this._value);
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x0003434B File Offset: 0x0003254B
		// (set) Token: 0x060049ED RID: 18925 RVA: 0x00034358 File Offset: 0x00032558
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

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x00034366 File Offset: 0x00032566
		public Vector3Proxy thirdPersonOffset
		{
			get
			{
				return Vector3Proxy.New(WWeapon.GetThirdPersonOffset(this._value));
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x00034378 File Offset: 0x00032578
		public QuaternionProxy thirdPersonRotation
		{
			get
			{
				return QuaternionProxy.New(WWeapon.GetThirdPersonRotation(this._value));
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x0003438A File Offset: 0x0003258A
		public float thirdPersonScale
		{
			get
			{
				return WWeapon.GetThirdPersonScale(this._value);
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x00034397 File Offset: 0x00032597
		public SpriteProxy uiSprite
		{
			get
			{
				return SpriteProxy.New(WWeapon.GetUiSprite(this._value));
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x000343A9 File Offset: 0x000325A9
		// (set) Token: 0x060049F3 RID: 18931 RVA: 0x000343B6 File Offset: 0x000325B6
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

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060049F4 RID: 18932 RVA: 0x000343C4 File Offset: 0x000325C4
		// (set) Token: 0x060049F5 RID: 18933 RVA: 0x000343D1 File Offset: 0x000325D1
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

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x000343DF File Offset: 0x000325DF
		public ActorProxy user
		{
			get
			{
				return ActorProxy.New(WWeapon.GetUser(this._value));
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x000343F1 File Offset: 0x000325F1
		public WeaponEntryProxy weaponEntry
		{
			get
			{
				return WeaponEntryProxy.New(WWeapon.GetWeaponEntry(this._value));
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x00034403 File Offset: 0x00032603
		public bool hasLoadedAmmo
		{
			get
			{
				return WWeapon.HasLoadedAmmo(this._value);
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060049F9 RID: 18937 RVA: 0x00034410 File Offset: 0x00032610
		public bool hasSpareAmmo
		{
			get
			{
				return WWeapon.HasSpareAmmo(this._value);
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060049FA RID: 18938 RVA: 0x0003441D File Offset: 0x0003261D
		public bool isAiming
		{
			get
			{
				return WWeapon.IsAiming(this._value);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0003442A File Offset: 0x0003262A
		public bool isCharged
		{
			get
			{
				return WWeapon.IsCharged(this._value);
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060049FC RID: 18940 RVA: 0x00034437 File Offset: 0x00032637
		public bool isCoolingDown
		{
			get
			{
				return WWeapon.IsCoolingDown(this._value);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x00034444 File Offset: 0x00032644
		public bool isEmpty
		{
			get
			{
				return WWeapon.IsEmpty(this._value);
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x00034451 File Offset: 0x00032651
		public bool isFull
		{
			get
			{
				return WWeapon.IsFull(this._value);
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x0003445E File Offset: 0x0003265E
		public bool isHoldingFire
		{
			get
			{
				return WWeapon.IsHoldingFire(this._value);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06004A00 RID: 18944 RVA: 0x0003446B File Offset: 0x0003266B
		public bool isOverheating
		{
			get
			{
				return WWeapon.IsOverheating(this._value);
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x00034478 File Offset: 0x00032678
		public bool isReloading
		{
			get
			{
				return WWeapon.IsReloading(this._value);
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x00034485 File Offset: 0x00032685
		public bool isUnholstered
		{
			get
			{
				return WWeapon.IsUnholstered(this._value);
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x00034492 File Offset: 0x00032692
		public ScriptEventProxy onFire
		{
			get
			{
				return ScriptEventProxy.New(WWeapon.GetOnFire(this._value));
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x000344A4 File Offset: 0x000326A4
		public ScriptEventProxy onSpawnProjectiles
		{
			get
			{
				return ScriptEventProxy.New(WWeapon.GetOnSpawnProjectiles(this._value));
			}
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x000344B6 File Offset: 0x000326B6
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x0013196C File Offset: 0x0012FB6C
		[MoonSharpHidden]
		public static MountedWeaponProxy New(MountedWeapon value)
		{
			if (value == null)
			{
				return null;
			}
			MountedWeaponProxy mountedWeaponProxy = (MountedWeaponProxy)ObjectCache.Get(typeof(MountedWeaponProxy), value);
			if (mountedWeaponProxy == null)
			{
				mountedWeaponProxy = new MountedWeaponProxy(value);
				ObjectCache.Add(typeof(MountedWeaponProxy), value, mountedWeaponProxy);
			}
			return mountedWeaponProxy;
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x000344BE File Offset: 0x000326BE
		[MoonSharpUserDataMetamethod("__call")]
		public static MountedWeaponProxy Call(DynValue _)
		{
			return new MountedWeaponProxy();
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x000344C5 File Offset: 0x000326C5
		public void AssignFpVehicleAudioMix()
		{
			this._value.AssignFpVehicleAudioMix();
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x000344D2 File Offset: 0x000326D2
		public void AssignPlayerVehicleAudioMix()
		{
			this._value.AssignPlayerVehicleAudioMix();
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x000344DF File Offset: 0x000326DF
		public bool CanFire()
		{
			return this._value.CanFire();
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x000344EC File Offset: 0x000326EC
		public void Fire(Vector3Proxy direction, bool useMuzzleDirection)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			this._value.Fire(direction._value, useMuzzleDirection);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x0003450E File Offset: 0x0003270E
		public Vector3Proxy GetClampedTurretRandomLookDirection()
		{
			return Vector3Proxy.New(this._value.GetClampedTurretRandomLookDirection());
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x00034520 File Offset: 0x00032720
		public void Hide()
		{
			this._value.Hide();
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0003452D File Offset: 0x0003272D
		public void Holster()
		{
			this._value.Holster();
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0003453A File Offset: 0x0003273A
		public bool IsClampedTurret()
		{
			return this._value.IsClampedTurret();
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x00034547 File Offset: 0x00032747
		public bool IsMountedWeapon()
		{
			return this._value.IsMountedWeapon();
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x00034554 File Offset: 0x00032754
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

		// Token: 0x06004A12 RID: 18962 RVA: 0x00034589 File Offset: 0x00032789
		public void ResetAudioMix()
		{
			this._value.ResetAudioMix();
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x00034596 File Offset: 0x00032796
		public void Show()
		{
			this._value.Show();
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x000345A3 File Offset: 0x000327A3
		public void Unholster()
		{
			this._value.Unholster();
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x001319B8 File Offset: 0x0012FBB8
		public int AddSubWeapon(WeaponProxy subWeapon)
		{
			Weapon subWeapon2 = null;
			if (subWeapon != null)
			{
				subWeapon2 = subWeapon._value;
			}
			return WWeapon.AddSubWeapon(this._value, subWeapon2);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x000345B0 File Offset: 0x000327B0
		public void EquipSubWeapon(int subWeaponIndex)
		{
			WWeapon.EquipSubWeapon(this._value, subWeaponIndex);
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x000345BE File Offset: 0x000327BE
		public Weapon.WeaponRole GenerateWeaponRoleFromStats()
		{
			return WWeapon.GenerateWeaponRoleFromStats(this._value);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x000345CB File Offset: 0x000327CB
		public void InstantlyReload()
		{
			WWeapon.InstantlyReload(this._value);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x000345D8 File Offset: 0x000327D8
		public void LockWeapon()
		{
			WWeapon.LockWeapon(this._value);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x000345E5 File Offset: 0x000327E5
		public void NextSightMode()
		{
			WWeapon.NextSightMode(this._value);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x000345F2 File Offset: 0x000327F2
		public void NextSubWeapon()
		{
			WWeapon.NextSubWeapon(this._value);
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x000345FF File Offset: 0x000327FF
		public void PreviousSightMode()
		{
			WWeapon.PreviousSightMode(this._value);
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x0003460C File Offset: 0x0003280C
		public void Reload()
		{
			WWeapon.Reload(this._value);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x00034619 File Offset: 0x00032819
		public void RemoveSubWeapon(int subWeaponIndex)
		{
			WWeapon.RemoveSubWeapon(this._value, subWeaponIndex);
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x001319E0 File Offset: 0x0012FBE0
		public void RemoveSubWeapon(WeaponProxy subWeapon)
		{
			Weapon subWeapon2 = null;
			if (subWeapon != null)
			{
				subWeapon2 = subWeapon._value;
			}
			WWeapon.RemoveSubWeapon(this._value, subWeapon2);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00131A08 File Offset: 0x0012FC08
		public void SetProjectilePrefab(GameObjectProxy prefab)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			WWeapon.SetProjectilePrefab(this._value, prefab2);
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00034627 File Offset: 0x00032827
		public void Shoot()
		{
			WWeapon.Shoot(this._value);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00034634 File Offset: 0x00032834
		public void Shoot(bool force)
		{
			WWeapon.Shoot(this._value, force);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00034642 File Offset: 0x00032842
		public void UnlockWeapon()
		{
			WWeapon.UnlockWeapon(this._value);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0003464F File Offset: 0x0003284F
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003176 RID: 12662
		[MoonSharpHidden]
		public MountedWeapon _value;
	}
}
