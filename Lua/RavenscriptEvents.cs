using System;
using Lua.Wrapper;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000926 RID: 2342
	[GlobalInstance]
	[Include]
	[Name("GameEvents")]
	public class RavenscriptEvents : ScriptEventManager
	{
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000280E9 File Offset: 0x000262E9
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x000280F1 File Offset: 0x000262F1
		[CallbackSignature(new string[]
		{
			"damage",
			"hit"
		})]
		[Doc("Invoked when the player deals damage.[..] This is a good event to drive feedback effects such as hitmarkers from!")]
		public ScriptEvent<DamageInfo, HitInfo> onPlayerDealtDamage { get; protected set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000280FA File Offset: 0x000262FA
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x00028102 File Offset: 0x00026302
		[Deprecated("Use onActorDiedInfo instead!")]
		[CallbackSignature(new string[]
		{
			"actor",
			"killer",
			"isSilentKill"
		})]
		[Doc("Invoked when an actor dies.[..] Silent kills should typically not count towards team score. Example of silent kill: When the player takes over a bot, the bot actor is silently killed.")]
		public ScriptEvent<Actor, Actor, bool> onActorDied { get; protected set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x0002810B File Offset: 0x0002630B
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x00028113 File Offset: 0x00026313
		[CallbackSignature(new string[]
		{
			"actor",
			"info",
			"isSilentKill"
		})]
		[Doc("Invoked when an actor dies.[..] Silent kills should typically not count towards team score. Example of silent kill: When the player takes over a bot, the bot actor is silently killed.")]
		public ScriptEvent<Actor, DamageInfo, bool> onActorDiedInfo { get; protected set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x0002811C File Offset: 0x0002631C
		// (set) Token: 0x06003B62 RID: 15202 RVA: 0x00028124 File Offset: 0x00026324
		[CallbackSignature(new string[]
		{
			"vehicle",
			"info"
		})]
		[Doc("Invoked when a vehicle is destroyed.")]
		public ScriptEvent<Vehicle, DamageInfo> onVehicleDestroyed { get; protected set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x0002812D File Offset: 0x0002632D
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x00028135 File Offset: 0x00026335
		[CallbackSignature(new string[]
		{
			"vehicle",
			"info"
		})]
		[Doc("Invoked when a vehicle starts burning.")]
		public ScriptEvent<Vehicle, DamageInfo> onVehicleDisabled { get; protected set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x0002813E File Offset: 0x0002633E
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x00028146 File Offset: 0x00026346
		[CallbackSignature(new string[]
		{
			"vehicle"
		})]
		[Doc("Invoked when a vehicle stops burning.")]
		public ScriptEvent<Vehicle> onVehicleExtinguished { get; protected set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x0002814F File Offset: 0x0002634F
		// (set) Token: 0x06003B68 RID: 15208 RVA: 0x00028157 File Offset: 0x00026357
		[Doc("Invoked when an actor spawns.")]
		public ScriptEvent<Actor> onActorSpawn { get; protected set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x00028160 File Offset: 0x00026360
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x00028168 File Offset: 0x00026368
		[Doc("Invoked when an actor is created during a game.[..] Please note this event is typically not invoked for actors created at the very start of the game, such as the default actors in the  game. To access those actors, you can get ``ActorManager.actors`` in your ``Start()`` function.")]
		public ScriptEvent<Actor> onActorCreated { get; protected set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x00028171 File Offset: 0x00026371
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x00028179 File Offset: 0x00026379
		[Doc("Invoked when an actor has selected a loadout.[..] This function is called just before the actor spawns, allowing you to modify their spawn loadout by changing the loadout object. For the player actor, the loadout strategy is nil.")]
		public ScriptEvent<Actor, WeaponManager.LoadoutSet, AiActorController.LoadoutPickStrategy> onActorSelectedLoadout { get; protected set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x00028182 File Offset: 0x00026382
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x0002818A File Offset: 0x0002638A
		[CallbackSignature(new string[]
		{
			"capturePoint",
			"previousOwner"
		})]
		[Doc("Invoked when a capture point is neutralized[..] IE when it's owner is set to Team.Neutral.")]
		public ScriptEvent<CapturePoint, WTeam> onCapturePointNeutralized { get; protected set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x00028193 File Offset: 0x00026393
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x0002819B File Offset: 0x0002639B
		[CallbackSignature(new string[]
		{
			"capturePoint",
			"newOwner"
		})]
		[Doc("Invoked when a capture point is captured.[..] IE when it's owner is set to Team.Blue or Team.Red.")]
		public ScriptEvent<CapturePoint, WTeam> onCapturePointCaptured { get; protected set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000281A4 File Offset: 0x000263A4
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x000281AC File Offset: 0x000263AC
		[Doc("Invoked when a squad is created.")]
		public ScriptEvent<Squad> onSquadCreated { get; protected set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000281B5 File Offset: 0x000263B5
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000281BD File Offset: 0x000263BD
		[Consumable("Consuming this event stops the squad from automatically being assigned an order.")]
		[Doc("Invoked when a squad requests a new order.")]
		public ScriptEvent<Squad> onSquadRequestNewOrder { get; protected set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000281C6 File Offset: 0x000263C6
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000281CE File Offset: 0x000263CE
		[Doc("Invoked when a squad is assigned a new order.")]
		public ScriptEvent<Squad, Order> onSquadAssignedNewOrder { get; protected set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000281D7 File Offset: 0x000263D7
		// (set) Token: 0x06003B78 RID: 15224 RVA: 0x000281DF File Offset: 0x000263DF
		[Doc("Invoked when a squad requested a new order, but none were available.")]
		public ScriptEvent<Squad> onSquadFailedToAssignNewOrder { get; protected set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000281E8 File Offset: 0x000263E8
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000281F0 File Offset: 0x000263F0
		[Doc("Invoked when a projectile is spawned.")]
		public ScriptEvent<Projectile> onProjectileSpawned { get; protected set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000281F9 File Offset: 0x000263F9
		// (set) Token: 0x06003B7C RID: 15228 RVA: 0x00028201 File Offset: 0x00026401
		[Consumable("Consuming this event stops the default victory screen from playing and the match ending.")]
		[Doc("Invoked when a match ends.")]
		public ScriptEvent<WTeam> onMatchEnd { get; protected set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x0002820A File Offset: 0x0002640A
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x00028212 File Offset: 0x00026412
		[Deprecated("Use onExplosionInfo instead!")]
		[CallbackSignature(new string[]
		{
			"position",
			"range",
			"source"
		})]
		[Doc("Invoked when an explosion goes off.")]
		public ScriptEvent<Vector3, float, Actor> onExplosion { get; protected set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x0002821B File Offset: 0x0002641B
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x00028223 File Offset: 0x00026423
		[CallbackSignature(new string[]
		{
			"info"
		})]
		[Doc("Invoked when an explosion goes off.")]
		public ScriptEvent<ExplosionInfo> onExplosionInfo { get; protected set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x0002822C File Offset: 0x0002642C
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x00028234 File Offset: 0x00026434
		[CallbackSignature(new string[]
		{
			"vehicle",
			"spawner"
		})]
		[Doc("Invoked when a vehicle is spawned.[..] Note that if the vehicle isn't spawned by a VehicleSpawner, the spawner value is nil.")]
		public ScriptEvent<Vehicle, VehicleSpawner> onVehicleSpawn { get; protected set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x0002823D File Offset: 0x0002643D
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x00028245 File Offset: 0x00026445
		[CallbackSignature(new string[]
		{
			"turret",
			"spawner"
		})]
		[Doc("Invoked when a turret is activated.")]
		public ScriptEvent<Vehicle, TurretSpawner> onTurretActivated { get; protected set; }
	}
}
