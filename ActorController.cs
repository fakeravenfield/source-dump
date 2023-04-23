using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public abstract class ActorController : MonoBehaviour
{
	// Token: 0x06000257 RID: 599
	public abstract Vector3 FacingDirection();

	// Token: 0x06000258 RID: 600
	public abstract bool UseMuzzleDirection();

	// Token: 0x06000259 RID: 601
	public abstract Vector3 Velocity();

	// Token: 0x0600025A RID: 602
	public abstract bool IsMoving();

	// Token: 0x0600025B RID: 603
	public abstract bool OnGround();

	// Token: 0x0600025C RID: 604
	public abstract bool Fire();

	// Token: 0x0600025D RID: 605
	public abstract bool ProjectToGround();

	// Token: 0x0600025E RID: 606
	public abstract Vector2 AimInput();

	// Token: 0x0600025F RID: 607
	public abstract float RangeInput();

	// Token: 0x06000260 RID: 608
	public abstract Vector3 SwimInput();

	// Token: 0x06000261 RID: 609
	public abstract bool ForceStopVehicle();

	// Token: 0x06000262 RID: 610
	public abstract Vector2 BoatInput();

	// Token: 0x06000263 RID: 611
	public abstract Vector2 CarInput();

	// Token: 0x06000264 RID: 612
	public abstract Vector4 HelicopterInput();

	// Token: 0x06000265 RID: 613
	public abstract Vector4 AirplaneInput();

	// Token: 0x06000266 RID: 614
	public abstract float LadderInput();

	// Token: 0x06000267 RID: 615
	public abstract float Lean();

	// Token: 0x06000268 RID: 616
	public abstract bool Jump();

	// Token: 0x06000269 RID: 617
	public abstract bool Crouch();

	// Token: 0x0600026A RID: 618
	public abstract bool Prone();

	// Token: 0x0600026B RID: 619
	public abstract bool Aiming();

	// Token: 0x0600026C RID: 620
	public abstract bool Reload();

	// Token: 0x0600026D RID: 621
	public abstract bool SwitchFireMode();

	// Token: 0x0600026E RID: 622
	public abstract bool NextSightMode();

	// Token: 0x0600026F RID: 623
	public abstract bool PreviousSightMode();

	// Token: 0x06000270 RID: 624
	public abstract bool DeployParachute();

	// Token: 0x06000271 RID: 625
	public abstract bool Countermeasures();

	// Token: 0x06000272 RID: 626
	public abstract bool IsSprinting();

	// Token: 0x06000273 RID: 627
	public abstract bool UseSprintingAnimation();

	// Token: 0x06000274 RID: 628
	public abstract bool HoldingSprint();

	// Token: 0x06000275 RID: 629
	public abstract bool IdlePose();

	// Token: 0x06000276 RID: 630
	public abstract SpawnPoint SelectedSpawnPoint();

	// Token: 0x06000277 RID: 631
	public abstract Transform WeaponParent();

	// Token: 0x06000278 RID: 632
	public abstract Transform TpWeaponParent();

	// Token: 0x06000279 RID: 633
	public abstract WeaponManager.LoadoutSet GetLoadout();

	// Token: 0x0600027A RID: 634
	public abstract bool IsGroupedUp();

	// Token: 0x0600027B RID: 635
	public abstract Vector2 ParachuteInput();

	// Token: 0x0600027C RID: 636
	public abstract void SwitchedToWeapon(Weapon weapon);

	// Token: 0x0600027D RID: 637
	public abstract void HolsteredActiveWeapon();

	// Token: 0x0600027E RID: 638
	public abstract void ChangeAimFieldOfView(float fov);

	// Token: 0x0600027F RID: 639
	public abstract void ReceivedDamage(bool friendlyFire, float damage, float balanceDamage, Vector3 point, Vector3 direction, Vector3 force);

	// Token: 0x06000280 RID: 640
	public abstract void OnVehicleWasDamaged(Actor source, float damage);

	// Token: 0x06000281 RID: 641
	public abstract void DisableInput();

	// Token: 0x06000282 RID: 642
	public abstract void EnableInput();

	// Token: 0x06000283 RID: 643
	public abstract void StartLadder(Ladder ladder);

	// Token: 0x06000284 RID: 644
	public abstract void EndLadder(Vector3 exitPosition, Quaternion flatFacing);

	// Token: 0x06000285 RID: 645
	public abstract void StartSeated(Seat seat);

	// Token: 0x06000286 RID: 646
	public abstract void EndSeatedSwap(Seat leftSeat);

	// Token: 0x06000287 RID: 647
	public abstract void EndSeated(Seat leftSeat, Vector3 exitPosition, Quaternion flatFacing, bool forcedByFallingOver);

	// Token: 0x06000288 RID: 648
	public abstract void StartRagdoll();

	// Token: 0x06000289 RID: 649
	public abstract void GettingUp();

	// Token: 0x0600028A RID: 650
	public abstract void EndRagdoll();

	// Token: 0x0600028B RID: 651
	public abstract void Die(Actor killer);

	// Token: 0x0600028C RID: 652
	public abstract void SpawnAt(Vector3 position, Quaternion rotation);

	// Token: 0x0600028D RID: 653
	public abstract void ApplyRecoil(Vector3 impulse);

	// Token: 0x0600028E RID: 654
	public abstract bool ChangeStance(Actor.Stance stance);

	// Token: 0x0600028F RID: 655
	public abstract void ForceChangeStance(Actor.Stance stance);

	// Token: 0x06000290 RID: 656
	public abstract void StartClimbingSlope();

	// Token: 0x06000291 RID: 657
	public abstract void DisableMovement();

	// Token: 0x06000292 RID: 658
	public abstract void EnableMovement();

	// Token: 0x06000293 RID: 659
	public abstract void Move(Vector3 movement);

	// Token: 0x06000294 RID: 660
	public abstract bool IsMovementEnabled();

	// Token: 0x06000295 RID: 661
	public abstract bool IsOnPlayerSquad();

	// Token: 0x06000296 RID: 662
	public abstract void LookAt(Vector3 position);

	// Token: 0x06000297 RID: 663
	public abstract void FillDriverSeat();

	// Token: 0x06000298 RID: 664
	public abstract void OnCancelParachute();

	// Token: 0x06000299 RID: 665
	public abstract bool FindCover();

	// Token: 0x0600029A RID: 666
	public abstract bool EnterCover(CoverPoint coverPoint);

	// Token: 0x0600029B RID: 667
	public abstract bool FindCoverAtPoint(Vector3 point);

	// Token: 0x0600029C RID: 668
	public abstract bool FindCoverAwayFrom(Vector3 point);

	// Token: 0x0600029D RID: 669
	public abstract bool FindCoverTowards(Vector3 direction);

	// Token: 0x0600029E RID: 670
	public abstract void LeaveCover();

	// Token: 0x0600029F RID: 671
	public abstract void OnAssignedToSquad(Squad squad);

	// Token: 0x060002A0 RID: 672
	public abstract void OnDroppedFromSquad();

	// Token: 0x060002A1 RID: 673
	public abstract void ChangeToSquad(Squad squad);

	// Token: 0x060002A2 RID: 674
	public abstract bool IsTakingFire();

	// Token: 0x060002A3 RID: 675
	public abstract bool HasSpottedTarget();

	// Token: 0x060002A4 RID: 676
	public abstract bool HasPath();

	// Token: 0x060002A5 RID: 677
	public abstract bool IsReadyToPickUpPassengers();

	// Token: 0x060002A6 RID: 678
	public abstract bool UseEyeMuzzle();

	// Token: 0x060002A7 RID: 679
	public abstract Actor GetTarget();

	// Token: 0x060002A8 RID: 680
	public abstract Vector3 PathEndPoint();

	// Token: 0x060002A9 RID: 681
	public abstract bool IsAirborne();

	// Token: 0x060002AA RID: 682
	public abstract Squad GetSquad();

	// Token: 0x060002AB RID: 683
	public abstract bool IsAlert();

	// Token: 0x060002AC RID: 684
	public abstract bool CurrentWaypoint(out Vector3 waypoint);

	// Token: 0x060002AD RID: 685
	public abstract void MarkReachedWaypoint();

	// Token: 0x0400022F RID: 559
	public Actor actor;
}
