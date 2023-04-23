using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A2D RID: 2605
	[Proxy(typeof(Weapon))]
	public class WeaponProxy : IProxy
	{
		// Token: 0x0600531B RID: 21275 RVA: 0x0003D3ED File Offset: 0x0003B5ED
		[MoonSharpHidden]
		public WeaponProxy(Weapon value)
		{
			this._value = value;
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x0003D3FC File Offset: 0x0003B5FC
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x0003D40E File Offset: 0x0003B60E
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x0003D420 File Offset: 0x0003B620
		public bool canFire
		{
			get
			{
				return WWeapon.CanFire(this._value);
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x0003D42D File Offset: 0x0003B62D
		public int activeSightModeIndex
		{
			get
			{
				return WWeapon.GetActiveSightModeIndex(this._value);
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x0003D43A File Offset: 0x0003B63A
		public WeaponProxy activeSubWeapon
		{
			get
			{
				return WeaponProxy.New(WWeapon.GetActiveSubWeapon(this._value));
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x0003D44C File Offset: 0x0003B64C
		public int activeSubWeaponIndex
		{
			get
			{
				return WWeapon.GetActiveSubWeaponIndex(this._value);
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x0003D459 File Offset: 0x0003B659
		// (set) Token: 0x06005323 RID: 21283 RVA: 0x0003D466 File Offset: 0x0003B666
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

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x0003D474 File Offset: 0x0003B674
		public Weapon[] alternativeWeapons
		{
			get
			{
				return WWeapon.GetAlternativeWeapons(this._value);
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x0003D481 File Offset: 0x0003B681
		// (set) Token: 0x06005326 RID: 21286 RVA: 0x0003D48E File Offset: 0x0003B68E
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

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x0003D49C File Offset: 0x0003B69C
		public AnimatorProxy animator
		{
			get
			{
				return AnimatorProxy.New(WWeapon.GetAnimator(this._value));
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x0003D4AE File Offset: 0x0003B6AE
		// (set) Token: 0x06005329 RID: 21289 RVA: 0x0003D4BB File Offset: 0x0003B6BB
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

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x0003D4C9 File Offset: 0x0003B6C9
		// (set) Token: 0x0600532B RID: 21291 RVA: 0x0003D4D6 File Offset: 0x0003B6D6
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

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		// (set) Token: 0x0600532D RID: 21293 RVA: 0x0003D4F1 File Offset: 0x0003B6F1
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

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x0003D4FF File Offset: 0x0003B6FF
		// (set) Token: 0x0600532F RID: 21295 RVA: 0x0003D50C File Offset: 0x0003B70C
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

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x0003D51A File Offset: 0x0003B71A
		public TransformProxy currentMuzzleTransform
		{
			get
			{
				return TransformProxy.New(WWeapon.GetCurrentMuzzleTransform(this._value));
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x0003D52C File Offset: 0x0003B72C
		public float currentSpreadMagnitude
		{
			get
			{
				return WWeapon.GetCurrentSpreadMagnitude(this._value);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x0003D539 File Offset: 0x0003B739
		public float currentSpreadMaxAngleRadians
		{
			get
			{
				return WWeapon.GetCurrentSpreadMaxAngleRadians(this._value);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06005333 RID: 21299 RVA: 0x0003D546 File Offset: 0x0003B746
		// (set) Token: 0x06005334 RID: 21300 RVA: 0x0003D553 File Offset: 0x0003B753
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

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06005335 RID: 21301 RVA: 0x0003D561 File Offset: 0x0003B761
		// (set) Token: 0x06005336 RID: 21302 RVA: 0x0003D56E File Offset: 0x0003B76E
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

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06005337 RID: 21303 RVA: 0x0003D57C File Offset: 0x0003B77C
		// (set) Token: 0x06005338 RID: 21304 RVA: 0x0003D589 File Offset: 0x0003B789
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

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x0003D597 File Offset: 0x0003B797
		// (set) Token: 0x0600533A RID: 21306 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
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

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x0003D5B2 File Offset: 0x0003B7B2
		// (set) Token: 0x0600533C RID: 21308 RVA: 0x0003D5BF File Offset: 0x0003B7BF
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

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x0003D5CD File Offset: 0x0003B7CD
		// (set) Token: 0x0600533E RID: 21310 RVA: 0x0003D5DA File Offset: 0x0003B7DA
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

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
		// (set) Token: 0x06005340 RID: 21312 RVA: 0x0003D5F5 File Offset: 0x0003B7F5
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

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06005341 RID: 21313 RVA: 0x0003D603 File Offset: 0x0003B803
		// (set) Token: 0x06005342 RID: 21314 RVA: 0x0003D610 File Offset: 0x0003B810
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

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x0003D61E File Offset: 0x0003B81E
		// (set) Token: 0x06005344 RID: 21316 RVA: 0x0003D62B File Offset: 0x0003B82B
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

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x0003D639 File Offset: 0x0003B839
		// (set) Token: 0x06005346 RID: 21318 RVA: 0x0003D646 File Offset: 0x0003B846
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

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06005347 RID: 21319 RVA: 0x0003D654 File Offset: 0x0003B854
		// (set) Token: 0x06005348 RID: 21320 RVA: 0x0003D661 File Offset: 0x0003B861
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

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x0003D66F File Offset: 0x0003B86F
		// (set) Token: 0x0600534A RID: 21322 RVA: 0x0003D67C File Offset: 0x0003B87C
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

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600534B RID: 21323 RVA: 0x0003D68A File Offset: 0x0003B88A
		// (set) Token: 0x0600534C RID: 21324 RVA: 0x0003D69C File Offset: 0x0003B89C
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

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x0003D6BD File Offset: 0x0003B8BD
		public bool hasAdvancedReload
		{
			get
			{
				return WWeapon.GetHasAdvancedReload(this._value);
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x0003D6CA File Offset: 0x0003B8CA
		// (set) Token: 0x0600534F RID: 21327 RVA: 0x0003D6D7 File Offset: 0x0003B8D7
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

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06005350 RID: 21328 RVA: 0x0003D6E5 File Offset: 0x0003B8E5
		// (set) Token: 0x06005351 RID: 21329 RVA: 0x0003D6F2 File Offset: 0x0003B8F2
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

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06005352 RID: 21330 RVA: 0x0003D700 File Offset: 0x0003B900
		// (set) Token: 0x06005353 RID: 21331 RVA: 0x0003D70D File Offset: 0x0003B90D
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

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06005354 RID: 21332 RVA: 0x0003D71B File Offset: 0x0003B91B
		// (set) Token: 0x06005355 RID: 21333 RVA: 0x0003D728 File Offset: 0x0003B928
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

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06005356 RID: 21334 RVA: 0x0003D736 File Offset: 0x0003B936
		// (set) Token: 0x06005357 RID: 21335 RVA: 0x0003D743 File Offset: 0x0003B943
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

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06005358 RID: 21336 RVA: 0x0003D751 File Offset: 0x0003B951
		// (set) Token: 0x06005359 RID: 21337 RVA: 0x0003D75E File Offset: 0x0003B95E
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

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600535A RID: 21338 RVA: 0x0003D76C File Offset: 0x0003B96C
		// (set) Token: 0x0600535B RID: 21339 RVA: 0x001397D4 File Offset: 0x001379D4
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

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600535C RID: 21340 RVA: 0x0003D77E File Offset: 0x0003B97E
		// (set) Token: 0x0600535D RID: 21341 RVA: 0x0003D78B File Offset: 0x0003B98B
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

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600535E RID: 21342 RVA: 0x0003D799 File Offset: 0x0003B999
		// (set) Token: 0x0600535F RID: 21343 RVA: 0x0003D7A6 File Offset: 0x0003B9A6
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

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06005360 RID: 21344 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
		public Transform[] muzzleTransforms
		{
			get
			{
				return WWeapon.GetMuzzleTransforms(this._value);
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06005361 RID: 21345 RVA: 0x0003D7C1 File Offset: 0x0003B9C1
		// (set) Token: 0x06005362 RID: 21346 RVA: 0x0003D7CE File Offset: 0x0003B9CE
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

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06005363 RID: 21347 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		// (set) Token: 0x06005364 RID: 21348 RVA: 0x0003D7E9 File Offset: 0x0003B9E9
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

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06005365 RID: 21349 RVA: 0x0003D7F7 File Offset: 0x0003B9F7
		// (set) Token: 0x06005366 RID: 21350 RVA: 0x0003D804 File Offset: 0x0003BA04
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

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06005367 RID: 21351 RVA: 0x0003D812 File Offset: 0x0003BA12
		// (set) Token: 0x06005368 RID: 21352 RVA: 0x0003D81F File Offset: 0x0003BA1F
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

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06005369 RID: 21353 RVA: 0x0003D82D File Offset: 0x0003BA2D
		// (set) Token: 0x0600536A RID: 21354 RVA: 0x0003D83A File Offset: 0x0003BA3A
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

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600536B RID: 21355 RVA: 0x0003D848 File Offset: 0x0003BA48
		// (set) Token: 0x0600536C RID: 21356 RVA: 0x0003D855 File Offset: 0x0003BA55
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

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600536D RID: 21357 RVA: 0x0003D863 File Offset: 0x0003BA63
		// (set) Token: 0x0600536E RID: 21358 RVA: 0x0003D870 File Offset: 0x0003BA70
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

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600536F RID: 21359 RVA: 0x0003D87E File Offset: 0x0003BA7E
		// (set) Token: 0x06005370 RID: 21360 RVA: 0x0003D88B File Offset: 0x0003BA8B
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

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06005371 RID: 21361 RVA: 0x0003D899 File Offset: 0x0003BA99
		// (set) Token: 0x06005372 RID: 21362 RVA: 0x0003D8A6 File Offset: 0x0003BAA6
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

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
		public GameObjectProxy scopeAimObject
		{
			get
			{
				return GameObjectProxy.New(WWeapon.GetScopeAimObject(this._value));
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06005374 RID: 21364 RVA: 0x0003D8C6 File Offset: 0x0003BAC6
		public int slot
		{
			get
			{
				return WWeapon.GetSlot(this._value);
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06005375 RID: 21365 RVA: 0x0003D8D3 File Offset: 0x0003BAD3
		// (set) Token: 0x06005376 RID: 21366 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
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

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06005377 RID: 21367 RVA: 0x0003D8EE File Offset: 0x0003BAEE
		public Vector3Proxy thirdPersonOffset
		{
			get
			{
				return Vector3Proxy.New(WWeapon.GetThirdPersonOffset(this._value));
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06005378 RID: 21368 RVA: 0x0003D900 File Offset: 0x0003BB00
		public QuaternionProxy thirdPersonRotation
		{
			get
			{
				return QuaternionProxy.New(WWeapon.GetThirdPersonRotation(this._value));
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x0003D912 File Offset: 0x0003BB12
		public float thirdPersonScale
		{
			get
			{
				return WWeapon.GetThirdPersonScale(this._value);
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x0003D91F File Offset: 0x0003BB1F
		public SpriteProxy uiSprite
		{
			get
			{
				return SpriteProxy.New(WWeapon.GetUiSprite(this._value));
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x0003D931 File Offset: 0x0003BB31
		// (set) Token: 0x0600537C RID: 21372 RVA: 0x0003D93E File Offset: 0x0003BB3E
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

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x0003D94C File Offset: 0x0003BB4C
		// (set) Token: 0x0600537E RID: 21374 RVA: 0x0003D959 File Offset: 0x0003BB59
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

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x0003D967 File Offset: 0x0003BB67
		public ActorProxy user
		{
			get
			{
				return ActorProxy.New(WWeapon.GetUser(this._value));
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06005380 RID: 21376 RVA: 0x0003D979 File Offset: 0x0003BB79
		public WeaponEntryProxy weaponEntry
		{
			get
			{
				return WeaponEntryProxy.New(WWeapon.GetWeaponEntry(this._value));
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x0003D98B File Offset: 0x0003BB8B
		public bool hasLoadedAmmo
		{
			get
			{
				return WWeapon.HasLoadedAmmo(this._value);
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06005382 RID: 21378 RVA: 0x0003D998 File Offset: 0x0003BB98
		public bool hasSpareAmmo
		{
			get
			{
				return WWeapon.HasSpareAmmo(this._value);
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x0003D9A5 File Offset: 0x0003BBA5
		public bool isAiming
		{
			get
			{
				return WWeapon.IsAiming(this._value);
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06005384 RID: 21380 RVA: 0x0003D9B2 File Offset: 0x0003BBB2
		public bool isCharged
		{
			get
			{
				return WWeapon.IsCharged(this._value);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x0003D9BF File Offset: 0x0003BBBF
		public bool isCoolingDown
		{
			get
			{
				return WWeapon.IsCoolingDown(this._value);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06005386 RID: 21382 RVA: 0x0003D9CC File Offset: 0x0003BBCC
		public bool isEmpty
		{
			get
			{
				return WWeapon.IsEmpty(this._value);
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06005387 RID: 21383 RVA: 0x0003D9D9 File Offset: 0x0003BBD9
		public bool isFull
		{
			get
			{
				return WWeapon.IsFull(this._value);
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x0003D9E6 File Offset: 0x0003BBE6
		public bool isHoldingFire
		{
			get
			{
				return WWeapon.IsHoldingFire(this._value);
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06005389 RID: 21385 RVA: 0x0003D9F3 File Offset: 0x0003BBF3
		public bool isOverheating
		{
			get
			{
				return WWeapon.IsOverheating(this._value);
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600538A RID: 21386 RVA: 0x0003DA00 File Offset: 0x0003BC00
		public bool isReloading
		{
			get
			{
				return WWeapon.IsReloading(this._value);
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x0003DA0D File Offset: 0x0003BC0D
		public bool isUnholstered
		{
			get
			{
				return WWeapon.IsUnholstered(this._value);
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600538C RID: 21388 RVA: 0x0003DA1A File Offset: 0x0003BC1A
		public ScriptEventProxy onFire
		{
			get
			{
				return ScriptEventProxy.New(WWeapon.GetOnFire(this._value));
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		public ScriptEventProxy onSpawnProjectiles
		{
			get
			{
				return ScriptEventProxy.New(WWeapon.GetOnSpawnProjectiles(this._value));
			}
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0003DA3E File Offset: 0x0003BC3E
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x001397FC File Offset: 0x001379FC
		[MoonSharpHidden]
		public static WeaponProxy New(Weapon value)
		{
			if (value == null)
			{
				return null;
			}
			WeaponProxy weaponProxy = (WeaponProxy)ObjectCache.Get(typeof(WeaponProxy), value);
			if (weaponProxy == null)
			{
				weaponProxy = new WeaponProxy(value);
				ObjectCache.Add(typeof(WeaponProxy), value, weaponProxy);
			}
			return weaponProxy;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00139848 File Offset: 0x00137A48
		public int AddSubWeapon(WeaponProxy subWeapon)
		{
			Weapon subWeapon2 = null;
			if (subWeapon != null)
			{
				subWeapon2 = subWeapon._value;
			}
			return WWeapon.AddSubWeapon(this._value, subWeapon2);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0003DA46 File Offset: 0x0003BC46
		public void EquipSubWeapon(int subWeaponIndex)
		{
			WWeapon.EquipSubWeapon(this._value, subWeaponIndex);
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0003DA54 File Offset: 0x0003BC54
		public Weapon.WeaponRole GenerateWeaponRoleFromStats()
		{
			return WWeapon.GenerateWeaponRoleFromStats(this._value);
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0003DA61 File Offset: 0x0003BC61
		public void InstantlyReload()
		{
			WWeapon.InstantlyReload(this._value);
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0003DA6E File Offset: 0x0003BC6E
		public void LockWeapon()
		{
			WWeapon.LockWeapon(this._value);
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0003DA7B File Offset: 0x0003BC7B
		public void NextSightMode()
		{
			WWeapon.NextSightMode(this._value);
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0003DA88 File Offset: 0x0003BC88
		public void NextSubWeapon()
		{
			WWeapon.NextSubWeapon(this._value);
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0003DA95 File Offset: 0x0003BC95
		public void PreviousSightMode()
		{
			WWeapon.PreviousSightMode(this._value);
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x0003DAA2 File Offset: 0x0003BCA2
		public void Reload()
		{
			WWeapon.Reload(this._value);
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0003DAAF File Offset: 0x0003BCAF
		public void RemoveSubWeapon(int subWeaponIndex)
		{
			WWeapon.RemoveSubWeapon(this._value, subWeaponIndex);
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00139870 File Offset: 0x00137A70
		public void RemoveSubWeapon(WeaponProxy subWeapon)
		{
			Weapon subWeapon2 = null;
			if (subWeapon != null)
			{
				subWeapon2 = subWeapon._value;
			}
			WWeapon.RemoveSubWeapon(this._value, subWeapon2);
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x00139898 File Offset: 0x00137A98
		public void SetProjectilePrefab(GameObjectProxy prefab)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			WWeapon.SetProjectilePrefab(this._value, prefab2);
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0003DABD File Offset: 0x0003BCBD
		public void Shoot()
		{
			WWeapon.Shoot(this._value);
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0003DACA File Offset: 0x0003BCCA
		public void Shoot(bool force)
		{
			WWeapon.Shoot(this._value, force);
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0003DAD8 File Offset: 0x0003BCD8
		public void UnlockWeapon()
		{
			WWeapon.UnlockWeapon(this._value);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0003DAE5 File Offset: 0x0003BCE5
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A9 RID: 12969
		[MoonSharpHidden]
		public Weapon _value;
	}
}
