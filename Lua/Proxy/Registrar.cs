using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using Ravenfield.Mods.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Lua.Proxy
{
	// Token: 0x020009ED RID: 2541
	public class Registrar
	{
		// Token: 0x06004C13 RID: 19475 RVA: 0x00132020 File Offset: 0x00130220
		public static Type[] GetProxyTypes()
		{
			return new Type[]
			{
				typeof(MathfProxy),
				typeof(InputProxy),
				typeof(MeshProxy),
				typeof(MaterialProxy),
				typeof(Vector2Proxy),
				typeof(Vector3Proxy),
				typeof(Vector4Proxy),
				typeof(Matrix4x4Proxy),
				typeof(QuaternionProxy),
				typeof(ColorProxy),
				typeof(GradientProxy),
				typeof(TransformProxy),
				typeof(RectTransformProxy),
				typeof(GameObjectProxy),
				typeof(MeshRendererProxy),
				typeof(SkinnedMeshRendererProxy),
				typeof(RendererProxy),
				typeof(CameraProxy),
				typeof(RayProxy),
				typeof(RaycastHitProxy),
				typeof(LineRendererProxy),
				typeof(MonoBehaviourProxy),
				typeof(RandomProxy),
				typeof(AnimatorProxy),
				typeof(RuntimeAnimatorControllerProxy),
				typeof(RigidbodyProxy),
				typeof(ParticleSystemProxy),
				typeof(ColliderProxy),
				typeof(BoxColliderProxy),
				typeof(CapsuleColliderProxy),
				typeof(SphereColliderProxy),
				typeof(AudioSourceProxy),
				typeof(AudioClipProxy),
				typeof(SpriteProxy),
				typeof(LightProxy),
				typeof(ScreenProxy),
				typeof(BoundsProxy),
				typeof(PlaneProxy),
				typeof(TextureProxy),
				typeof(RenderSettingsProxy),
				typeof(CanvasGroupProxy),
				typeof(CanvasProxy),
				typeof(AnimationCurveProxy),
				typeof(RectProxy),
				typeof(VideoPlayerProxy),
				typeof(VideoClipProxy),
				typeof(PlayableDirectorProxy),
				typeof(RawImageProxy),
				typeof(ImageProxy),
				typeof(TextProxy),
				typeof(ButtonProxy),
				typeof(ToggleProxy),
				typeof(DropdownProxy),
				typeof(SliderProxy),
				typeof(InputFieldProxy),
				typeof(ScrollRectProxy),
				typeof(ScriptedBehaviourProxy),
				typeof(ActorProxy),
				typeof(AiActorControllerProxy),
				typeof(LadderProxy),
				typeof(VehicleProxy),
				typeof(EngineProxy),
				typeof(SeatProxy),
				typeof(SpawnPointProxy),
				typeof(CapturePointProxy),
				typeof(VehicleSpawnerProxy),
				typeof(TurretSpawnerProxy),
				typeof(ResupplyCrateProxy),
				typeof(OrderProxy),
				typeof(SquadProxy),
				typeof(WeaponEntryProxy),
				typeof(LoadoutSetProxy),
				typeof(LoadoutPickStrategyProxy),
				typeof(MutatorEntryProxy),
				typeof(MeanFilterProxy),
				typeof(MeanFilterVector3Proxy),
				typeof(ActorSkinProxy),
				typeof(MeshSkinProxy),
				typeof(DataContainerProxy),
				typeof(EffectUiProxy),
				typeof(SoundBankProxy),
				typeof(DamageInfoProxy),
				typeof(ExplosionInfoProxy),
				typeof(HitInfoProxy),
				typeof(ExplosionConfigurationProxy),
				typeof(ArcadeCarProxy),
				typeof(AirplaneProxy),
				typeof(HelicopterProxy),
				typeof(BoatProxy),
				typeof(AnimationDrivenVehicleProxy),
				typeof(WeaponProxy),
				typeof(MountedWeaponProxy),
				typeof(MountedStabilizedTurretProxy),
				typeof(ProjectileProxy),
				typeof(ExplodingProjectileProxy),
				typeof(RocketProxy),
				typeof(TargetSeekingMissileProxy),
				typeof(WireGuidedMissileProxy),
				typeof(GrenadeProjectileProxy),
				typeof(RigidbodyProjectileProxy),
				typeof(WPlayerProxy),
				typeof(WPlayerCameraProxy),
				typeof(WPlayerHudProxy),
				typeof(WActorManagerProxy),
				typeof(WWeaponManagerProxy),
				typeof(WPhysicsProxy),
				typeof(WPathfindingProxy),
				typeof(WPathfindingNodeProxy),
				typeof(WOverlayProxy),
				typeof(WWaterProxy),
				typeof(WWaitForSecondsProxy),
				typeof(WTimeProxy),
				typeof(WColorSchemeProxy),
				typeof(WGameManagerProxy),
				typeof(WMinimapProxy),
				typeof(WMathUtilsProxy),
				typeof(WCurrentEventProxy),
				typeof(WFollowupSpreadProxy),
				typeof(WDebugProxy),
				typeof(WIngameDialogProxy),
				typeof(WSpawnUiProxy),
				typeof(WPortraitGeneratorProxy),
				typeof(ScriptEventProxy),
				typeof(RavenscriptEventsProxy),
				typeof(SgmModifyRulesArgsProxy),
				typeof(SgmSpawnActorsArgsProxy),
				typeof(SgmPointCapturedArgsProxy),
				typeof(SgmActorDiedArgsProxy),
				typeof(SgmLoadoutAcceptedArgsProxy),
				typeof(TextMeshProProxy),
				typeof(TextMeshProUGUIProxy)
			};
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x00132764 File Offset: 0x00130964
		public static Type[] GetAllowedTypes()
		{
			return new Type[]
			{
				typeof(PrimitiveType),
				typeof(Space),
				typeof(SendMessageOptions),
				typeof(KeyCode),
				typeof(HumanBodyBones),
				typeof(AvatarIKGoal),
				typeof(AvatarIKHint),
				typeof(ForceMode),
				typeof(AnimatorUpdateMode),
				typeof(AnimatorCullingMode),
				typeof(RigidbodyConstraints),
				typeof(VideoRenderMode),
				typeof(Vehicle.ArmorRating),
				typeof(WTeam),
				typeof(WRaycastTarget),
				typeof(WWeaponSlot),
				typeof(WPathfindingNodeType),
				typeof(SteelInput.KeyBinds),
				typeof(Squad.FormationType),
				typeof(Order.OrderType),
				typeof(VehicleSpawner.VehicleSpawnType),
				typeof(TurretSpawner.TurretSpawnType),
				typeof(WeaponManager.WeaponSlot),
				typeof(WeaponManager.WeaponEntry.Distance),
				typeof(WeaponManager.WeaponEntry.LoadoutType),
				typeof(AudioMixer),
				typeof(EffectUi.FadeType),
				typeof(Seat.Type),
				typeof(Weapon.Effectiveness),
				typeof(Weapon.Difficulty),
				typeof(AiActorController.SkillLevel),
				typeof(DamageInfo.DamageSourceType),
				typeof(Weapon.WeaponRole)
			};
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x00132940 File Offset: 0x00130B40
		public static void RegisterTypes()
		{
			UserData.RegisterType(typeof(MathfProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Mathf>((Script s, Mathf v) => DynValue.FromObject(s, MathfProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Mathf), (DynValue v) => v.ToObject<MathfProxy>()._value);
			UserData.RegisterType(typeof(InputProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Input>((Script s, Input v) => DynValue.FromObject(s, InputProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Input), (DynValue v) => v.ToObject<InputProxy>()._value);
			UserData.RegisterType(typeof(MeshProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Mesh>((Script s, Mesh v) => DynValue.FromObject(s, MeshProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Mesh), (DynValue v) => v.ToObject<MeshProxy>()._value);
			UserData.RegisterType(typeof(MaterialProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Material>((Script s, Material v) => DynValue.FromObject(s, MaterialProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Material), (DynValue v) => v.ToObject<MaterialProxy>()._value);
			UserData.RegisterType(typeof(Vector2Proxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vector2>((Script s, Vector2 v) => DynValue.FromObject(s, Vector2Proxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Vector2), (DynValue v) => v.ToObject<Vector2Proxy>()._value);
			UserData.RegisterType(typeof(Vector3Proxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vector3>((Script s, Vector3 v) => DynValue.FromObject(s, Vector3Proxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Vector3), (DynValue v) => v.ToObject<Vector3Proxy>()._value);
			UserData.RegisterType(typeof(Vector4Proxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vector4>((Script s, Vector4 v) => DynValue.FromObject(s, Vector4Proxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Vector4), (DynValue v) => v.ToObject<Vector4Proxy>()._value);
			UserData.RegisterType(typeof(Matrix4x4Proxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Matrix4x4>((Script s, Matrix4x4 v) => DynValue.FromObject(s, Matrix4x4Proxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Matrix4x4), (DynValue v) => v.ToObject<Matrix4x4Proxy>()._value);
			UserData.RegisterType(typeof(QuaternionProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Quaternion>((Script s, Quaternion v) => DynValue.FromObject(s, QuaternionProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Quaternion), (DynValue v) => v.ToObject<QuaternionProxy>()._value);
			UserData.RegisterType(typeof(ColorProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Color>((Script s, Color v) => DynValue.FromObject(s, ColorProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Color), (DynValue v) => v.ToObject<ColorProxy>()._value);
			UserData.RegisterType(typeof(GradientProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Gradient>((Script s, Gradient v) => DynValue.FromObject(s, GradientProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Gradient), (DynValue v) => v.ToObject<GradientProxy>()._value);
			UserData.RegisterType(typeof(TransformProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Transform>((Script s, Transform v) => DynValue.FromObject(s, TransformProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Transform), (DynValue v) => v.ToObject<TransformProxy>()._value);
			UserData.RegisterType(typeof(RectTransformProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(RectTransformProxy), typeof(TransformProxy), (object v) => TransformProxy.New(((RectTransformProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RectTransform>((Script s, RectTransform v) => DynValue.FromObject(s, RectTransformProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RectTransform), (DynValue v) => v.ToObject<RectTransformProxy>()._value);
			UserData.RegisterType(typeof(GameObjectProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<GameObject>((Script s, GameObject v) => DynValue.FromObject(s, GameObjectProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(GameObject), (DynValue v) => v.ToObject<GameObjectProxy>()._value);
			UserData.RegisterType(typeof(MeshRendererProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(MeshRendererProxy), typeof(RendererProxy), (object v) => RendererProxy.New(((MeshRendererProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MeshRenderer>((Script s, MeshRenderer v) => DynValue.FromObject(s, MeshRendererProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MeshRenderer), (DynValue v) => v.ToObject<MeshRendererProxy>()._value);
			UserData.RegisterType(typeof(SkinnedMeshRendererProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(SkinnedMeshRendererProxy), typeof(RendererProxy), (object v) => RendererProxy.New(((SkinnedMeshRendererProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SkinnedMeshRenderer>((Script s, SkinnedMeshRenderer v) => DynValue.FromObject(s, SkinnedMeshRendererProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SkinnedMeshRenderer), (DynValue v) => v.ToObject<SkinnedMeshRendererProxy>()._value);
			UserData.RegisterType(typeof(RendererProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Renderer>((Script s, Renderer v) => DynValue.FromObject(s, RendererProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Renderer), (DynValue v) => v.ToObject<RendererProxy>()._value);
			UserData.RegisterType(typeof(CameraProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Camera>((Script s, Camera v) => DynValue.FromObject(s, CameraProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Camera), (DynValue v) => v.ToObject<CameraProxy>()._value);
			UserData.RegisterType(typeof(RayProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Ray>((Script s, Ray v) => DynValue.FromObject(s, RayProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Ray), (DynValue v) => v.ToObject<RayProxy>()._value);
			UserData.RegisterType(typeof(RaycastHitProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RaycastHit>((Script s, RaycastHit v) => DynValue.FromObject(s, RaycastHitProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RaycastHit), (DynValue v) => v.ToObject<RaycastHitProxy>()._value);
			UserData.RegisterType(typeof(LineRendererProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(LineRendererProxy), typeof(RendererProxy), (object v) => RendererProxy.New(((LineRendererProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<LineRenderer>((Script s, LineRenderer v) => DynValue.FromObject(s, LineRendererProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(LineRenderer), (DynValue v) => v.ToObject<LineRendererProxy>()._value);
			UserData.RegisterType(typeof(MonoBehaviourProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MonoBehaviour>((Script s, MonoBehaviour v) => DynValue.FromObject(s, MonoBehaviourProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MonoBehaviour), (DynValue v) => v.ToObject<MonoBehaviourProxy>()._value);
			UserData.RegisterType(typeof(RandomProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AnimatorProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Animator>((Script s, Animator v) => DynValue.FromObject(s, AnimatorProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Animator), (DynValue v) => v.ToObject<AnimatorProxy>()._value);
			UserData.RegisterType(typeof(RuntimeAnimatorControllerProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RuntimeAnimatorController>((Script s, RuntimeAnimatorController v) => DynValue.FromObject(s, RuntimeAnimatorControllerProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RuntimeAnimatorController), (DynValue v) => v.ToObject<RuntimeAnimatorControllerProxy>()._value);
			UserData.RegisterType(typeof(RigidbodyProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Rigidbody>((Script s, Rigidbody v) => DynValue.FromObject(s, RigidbodyProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Rigidbody), (DynValue v) => v.ToObject<RigidbodyProxy>()._value);
			UserData.RegisterType(typeof(ParticleSystemProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ParticleSystem>((Script s, ParticleSystem v) => DynValue.FromObject(s, ParticleSystemProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ParticleSystem), (DynValue v) => v.ToObject<ParticleSystemProxy>()._value);
			UserData.RegisterType(typeof(ColliderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Collider>((Script s, Collider v) => DynValue.FromObject(s, ColliderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Collider), (DynValue v) => v.ToObject<ColliderProxy>()._value);
			UserData.RegisterType(typeof(BoxColliderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(BoxColliderProxy), typeof(ColliderProxy), (object v) => ColliderProxy.New(((BoxColliderProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<BoxCollider>((Script s, BoxCollider v) => DynValue.FromObject(s, BoxColliderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(BoxCollider), (DynValue v) => v.ToObject<BoxColliderProxy>()._value);
			UserData.RegisterType(typeof(CapsuleColliderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(CapsuleColliderProxy), typeof(ColliderProxy), (object v) => ColliderProxy.New(((CapsuleColliderProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<CapsuleCollider>((Script s, CapsuleCollider v) => DynValue.FromObject(s, CapsuleColliderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(CapsuleCollider), (DynValue v) => v.ToObject<CapsuleColliderProxy>()._value);
			UserData.RegisterType(typeof(SphereColliderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(SphereColliderProxy), typeof(ColliderProxy), (object v) => ColliderProxy.New(((SphereColliderProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SphereCollider>((Script s, SphereCollider v) => DynValue.FromObject(s, SphereColliderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SphereCollider), (DynValue v) => v.ToObject<SphereColliderProxy>()._value);
			UserData.RegisterType(typeof(AudioSourceProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AudioSource>((Script s, AudioSource v) => DynValue.FromObject(s, AudioSourceProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(AudioSource), (DynValue v) => v.ToObject<AudioSourceProxy>()._value);
			UserData.RegisterType(typeof(AudioClipProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AudioClip>((Script s, AudioClip v) => DynValue.FromObject(s, AudioClipProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(AudioClip), (DynValue v) => v.ToObject<AudioClipProxy>()._value);
			UserData.RegisterType(typeof(SpriteProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Sprite>((Script s, Sprite v) => DynValue.FromObject(s, SpriteProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Sprite), (DynValue v) => v.ToObject<SpriteProxy>()._value);
			UserData.RegisterType(typeof(LightProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Light>((Script s, Light v) => DynValue.FromObject(s, LightProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Light), (DynValue v) => v.ToObject<LightProxy>()._value);
			UserData.RegisterType(typeof(ScreenProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(BoundsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Bounds>((Script s, Bounds v) => DynValue.FromObject(s, BoundsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Bounds), (DynValue v) => v.ToObject<BoundsProxy>()._value);
			UserData.RegisterType(typeof(PlaneProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<UnityEngine.Plane>((Script s, UnityEngine.Plane v) => DynValue.FromObject(s, PlaneProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(UnityEngine.Plane), (DynValue v) => v.ToObject<PlaneProxy>()._value);
			UserData.RegisterType(typeof(TextureProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Texture>((Script s, Texture v) => DynValue.FromObject(s, TextureProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Texture), (DynValue v) => v.ToObject<TextureProxy>()._value);
			UserData.RegisterType(typeof(RenderSettingsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RenderSettings>((Script s, RenderSettings v) => DynValue.FromObject(s, RenderSettingsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RenderSettings), (DynValue v) => v.ToObject<RenderSettingsProxy>()._value);
			UserData.RegisterType(typeof(CanvasGroupProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<CanvasGroup>((Script s, CanvasGroup v) => DynValue.FromObject(s, CanvasGroupProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(CanvasGroup), (DynValue v) => v.ToObject<CanvasGroupProxy>()._value);
			UserData.RegisterType(typeof(CanvasProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Canvas>((Script s, Canvas v) => DynValue.FromObject(s, CanvasProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Canvas), (DynValue v) => v.ToObject<CanvasProxy>()._value);
			UserData.RegisterType(typeof(AnimationCurveProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AnimationCurve>((Script s, AnimationCurve v) => DynValue.FromObject(s, AnimationCurveProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(AnimationCurve), (DynValue v) => v.ToObject<AnimationCurveProxy>()._value);
			UserData.RegisterType(typeof(RectProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Rect>((Script s, Rect v) => DynValue.FromObject(s, RectProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Rect), (DynValue v) => v.ToObject<RectProxy>()._value);
			UserData.RegisterType(typeof(VideoPlayerProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<VideoPlayer>((Script s, VideoPlayer v) => DynValue.FromObject(s, VideoPlayerProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(VideoPlayer), (DynValue v) => v.ToObject<VideoPlayerProxy>()._value);
			UserData.RegisterType(typeof(VideoClipProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<VideoClip>((Script s, VideoClip v) => DynValue.FromObject(s, VideoClipProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(VideoClip), (DynValue v) => v.ToObject<VideoClipProxy>()._value);
			UserData.RegisterType(typeof(PlayableDirectorProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<PlayableDirector>((Script s, PlayableDirector v) => DynValue.FromObject(s, PlayableDirectorProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(PlayableDirector), (DynValue v) => v.ToObject<PlayableDirectorProxy>()._value);
			UserData.RegisterType(typeof(RawImageProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RawImage>((Script s, RawImage v) => DynValue.FromObject(s, RawImageProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RawImage), (DynValue v) => v.ToObject<RawImageProxy>()._value);
			UserData.RegisterType(typeof(ImageProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Image>((Script s, Image v) => DynValue.FromObject(s, ImageProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Image), (DynValue v) => v.ToObject<ImageProxy>()._value);
			UserData.RegisterType(typeof(TextProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Text>((Script s, Text v) => DynValue.FromObject(s, TextProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Text), (DynValue v) => v.ToObject<TextProxy>()._value);
			UserData.RegisterType(typeof(ButtonProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Button>((Script s, Button v) => DynValue.FromObject(s, ButtonProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Button), (DynValue v) => v.ToObject<ButtonProxy>()._value);
			UserData.RegisterType(typeof(ToggleProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Toggle>((Script s, Toggle v) => DynValue.FromObject(s, ToggleProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Toggle), (DynValue v) => v.ToObject<ToggleProxy>()._value);
			UserData.RegisterType(typeof(DropdownProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Dropdown>((Script s, Dropdown v) => DynValue.FromObject(s, DropdownProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Dropdown), (DynValue v) => v.ToObject<DropdownProxy>()._value);
			UserData.RegisterType(typeof(SliderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Slider>((Script s, Slider v) => DynValue.FromObject(s, SliderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Slider), (DynValue v) => v.ToObject<SliderProxy>()._value);
			UserData.RegisterType(typeof(InputFieldProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<InputField>((Script s, InputField v) => DynValue.FromObject(s, InputFieldProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(InputField), (DynValue v) => v.ToObject<InputFieldProxy>()._value);
			UserData.RegisterType(typeof(ScrollRectProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ScrollRect>((Script s, ScrollRect v) => DynValue.FromObject(s, ScrollRectProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ScrollRect), (DynValue v) => v.ToObject<ScrollRectProxy>()._value);
			UserData.RegisterType(typeof(ScriptedBehaviourProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(ScriptedBehaviourProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((ScriptedBehaviourProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ScriptedBehaviour>((Script s, ScriptedBehaviour v) => DynValue.FromObject(s, ScriptedBehaviourProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ScriptedBehaviour), (DynValue v) => v.ToObject<ScriptedBehaviourProxy>()._value);
			UserData.RegisterType(typeof(ActorProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Actor>((Script s, Actor v) => DynValue.FromObject(s, ActorProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Actor), (DynValue v) => v.ToObject<ActorProxy>()._value);
			UserData.RegisterType(typeof(AiActorControllerProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AiActorController>((Script s, AiActorController v) => DynValue.FromObject(s, AiActorControllerProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(AiActorController), (DynValue v) => v.ToObject<AiActorControllerProxy>()._value);
			UserData.RegisterType(typeof(LadderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Ladder>((Script s, Ladder v) => DynValue.FromObject(s, LadderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Ladder), (DynValue v) => v.ToObject<LadderProxy>()._value);
			UserData.RegisterType(typeof(VehicleProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(VehicleProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((VehicleProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vehicle>((Script s, Vehicle v) => DynValue.FromObject(s, VehicleProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Vehicle), (DynValue v) => v.ToObject<VehicleProxy>()._value);
			UserData.RegisterType(typeof(EngineProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vehicle.Engine>((Script s, Vehicle.Engine v) => DynValue.FromObject(s, EngineProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Vehicle.Engine), (DynValue v) => v.ToObject<EngineProxy>()._value);
			UserData.RegisterType(typeof(SeatProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(SeatProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((SeatProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Seat>((Script s, Seat v) => DynValue.FromObject(s, SeatProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Seat), (DynValue v) => v.ToObject<SeatProxy>()._value);
			UserData.RegisterType(typeof(SpawnPointProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(SpawnPointProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((SpawnPointProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SpawnPoint>((Script s, SpawnPoint v) => DynValue.FromObject(s, SpawnPointProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SpawnPoint), (DynValue v) => v.ToObject<SpawnPointProxy>()._value);
			UserData.RegisterType(typeof(CapturePointProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(CapturePointProxy), typeof(SpawnPointProxy), (object v) => SpawnPointProxy.New(((CapturePointProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<CapturePoint>((Script s, CapturePoint v) => DynValue.FromObject(s, CapturePointProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(CapturePoint), (DynValue v) => v.ToObject<CapturePointProxy>()._value);
			UserData.RegisterType(typeof(VehicleSpawnerProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(VehicleSpawnerProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((VehicleSpawnerProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<VehicleSpawner>((Script s, VehicleSpawner v) => DynValue.FromObject(s, VehicleSpawnerProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(VehicleSpawner), (DynValue v) => v.ToObject<VehicleSpawnerProxy>()._value);
			UserData.RegisterType(typeof(TurretSpawnerProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(TurretSpawnerProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((TurretSpawnerProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<TurretSpawner>((Script s, TurretSpawner v) => DynValue.FromObject(s, TurretSpawnerProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(TurretSpawner), (DynValue v) => v.ToObject<TurretSpawnerProxy>()._value);
			UserData.RegisterType(typeof(ResupplyCrateProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(ResupplyCrateProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((ResupplyCrateProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ResupplyCrate>((Script s, ResupplyCrate v) => DynValue.FromObject(s, ResupplyCrateProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ResupplyCrate), (DynValue v) => v.ToObject<ResupplyCrateProxy>()._value);
			UserData.RegisterType(typeof(OrderProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Order>((Script s, Order v) => DynValue.FromObject(s, OrderProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Order), (DynValue v) => v.ToObject<OrderProxy>()._value);
			UserData.RegisterType(typeof(SquadProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Squad>((Script s, Squad v) => DynValue.FromObject(s, SquadProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Squad), (DynValue v) => v.ToObject<SquadProxy>()._value);
			UserData.RegisterType(typeof(WeaponEntryProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<WeaponManager.WeaponEntry>((Script s, WeaponManager.WeaponEntry v) => DynValue.FromObject(s, WeaponEntryProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(WeaponManager.WeaponEntry), (DynValue v) => v.ToObject<WeaponEntryProxy>()._value);
			UserData.RegisterType(typeof(LoadoutSetProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<WeaponManager.LoadoutSet>((Script s, WeaponManager.LoadoutSet v) => DynValue.FromObject(s, LoadoutSetProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(WeaponManager.LoadoutSet), (DynValue v) => v.ToObject<LoadoutSetProxy>()._value);
			UserData.RegisterType(typeof(LoadoutPickStrategyProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AiActorController.LoadoutPickStrategy>((Script s, AiActorController.LoadoutPickStrategy v) => DynValue.FromObject(s, LoadoutPickStrategyProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(AiActorController.LoadoutPickStrategy), (DynValue v) => v.ToObject<LoadoutPickStrategyProxy>()._value);
			UserData.RegisterType(typeof(MutatorEntryProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MutatorEntry>((Script s, MutatorEntry v) => DynValue.FromObject(s, MutatorEntryProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MutatorEntry), (DynValue v) => v.ToObject<MutatorEntryProxy>()._value);
			UserData.RegisterType(typeof(MeanFilterProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MeanFilter>((Script s, MeanFilter v) => DynValue.FromObject(s, MeanFilterProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MeanFilter), (DynValue v) => v.ToObject<MeanFilterProxy>()._value);
			UserData.RegisterType(typeof(MeanFilterVector3Proxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MeanFilterVector3>((Script s, MeanFilterVector3 v) => DynValue.FromObject(s, MeanFilterVector3Proxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MeanFilterVector3), (DynValue v) => v.ToObject<MeanFilterVector3Proxy>()._value);
			UserData.RegisterType(typeof(ActorSkinProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ActorSkin>((Script s, ActorSkin v) => DynValue.FromObject(s, ActorSkinProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ActorSkin), (DynValue v) => v.ToObject<ActorSkinProxy>()._value);
			UserData.RegisterType(typeof(MeshSkinProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ActorSkin.MeshSkin>((Script s, ActorSkin.MeshSkin v) => DynValue.FromObject(s, MeshSkinProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ActorSkin.MeshSkin), (DynValue v) => v.ToObject<MeshSkinProxy>()._value);
			UserData.RegisterType(typeof(DataContainerProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(DataContainerProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((DataContainerProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<DataContainer>((Script s, DataContainer v) => DynValue.FromObject(s, DataContainerProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(DataContainer), (DynValue v) => v.ToObject<DataContainerProxy>()._value);
			UserData.RegisterType(typeof(EffectUiProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(SoundBankProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(SoundBankProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((SoundBankProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SoundBank>((Script s, SoundBank v) => DynValue.FromObject(s, SoundBankProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SoundBank), (DynValue v) => v.ToObject<SoundBankProxy>()._value);
			UserData.RegisterType(typeof(DamageInfoProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<DamageInfo>((Script s, DamageInfo v) => DynValue.FromObject(s, DamageInfoProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(DamageInfo), (DynValue v) => v.ToObject<DamageInfoProxy>()._value);
			UserData.RegisterType(typeof(ExplosionInfoProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ExplosionInfo>((Script s, ExplosionInfo v) => DynValue.FromObject(s, ExplosionInfoProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ExplosionInfo), (DynValue v) => v.ToObject<ExplosionInfoProxy>()._value);
			UserData.RegisterType(typeof(HitInfoProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<HitInfo>((Script s, HitInfo v) => DynValue.FromObject(s, HitInfoProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(HitInfo), (DynValue v) => v.ToObject<HitInfoProxy>()._value);
			UserData.RegisterType(typeof(ExplosionConfigurationProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ExplodingProjectile.ExplosionConfiguration>((Script s, ExplodingProjectile.ExplosionConfiguration v) => DynValue.FromObject(s, ExplosionConfigurationProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ExplodingProjectile.ExplosionConfiguration), (DynValue v) => v.ToObject<ExplosionConfigurationProxy>()._value);
			UserData.RegisterType(typeof(ArcadeCarProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(ArcadeCarProxy), typeof(VehicleProxy), (object v) => VehicleProxy.New(((ArcadeCarProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ArcadeCar>((Script s, ArcadeCar v) => DynValue.FromObject(s, ArcadeCarProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ArcadeCar), (DynValue v) => v.ToObject<ArcadeCarProxy>()._value);
			UserData.RegisterType(typeof(AirplaneProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(AirplaneProxy), typeof(VehicleProxy), (object v) => VehicleProxy.New(((AirplaneProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Airplane>((Script s, Airplane v) => DynValue.FromObject(s, AirplaneProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Airplane), (DynValue v) => v.ToObject<AirplaneProxy>()._value);
			UserData.RegisterType(typeof(HelicopterProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(HelicopterProxy), typeof(VehicleProxy), (object v) => VehicleProxy.New(((HelicopterProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Helicopter>((Script s, Helicopter v) => DynValue.FromObject(s, HelicopterProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Helicopter), (DynValue v) => v.ToObject<HelicopterProxy>()._value);
			UserData.RegisterType(typeof(BoatProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(BoatProxy), typeof(VehicleProxy), (object v) => VehicleProxy.New(((BoatProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Boat>((Script s, Boat v) => DynValue.FromObject(s, BoatProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Boat), (DynValue v) => v.ToObject<BoatProxy>()._value);
			UserData.RegisterType(typeof(AnimationDrivenVehicleProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(AnimationDrivenVehicleProxy), typeof(VehicleProxy), (object v) => VehicleProxy.New(((AnimationDrivenVehicleProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AnimationDrivenVehicle>((Script s, AnimationDrivenVehicle v) => DynValue.FromObject(s, AnimationDrivenVehicleProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(AnimationDrivenVehicle), (DynValue v) => v.ToObject<AnimationDrivenVehicleProxy>()._value);
			UserData.RegisterType(typeof(WeaponProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(WeaponProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((WeaponProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Weapon>((Script s, Weapon v) => DynValue.FromObject(s, WeaponProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Weapon), (DynValue v) => v.ToObject<WeaponProxy>()._value);
			UserData.RegisterType(typeof(MountedWeaponProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(MountedWeaponProxy), typeof(WeaponProxy), (object v) => WeaponProxy.New(((MountedWeaponProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MountedWeapon>((Script s, MountedWeapon v) => DynValue.FromObject(s, MountedWeaponProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MountedWeapon), (DynValue v) => v.ToObject<MountedWeaponProxy>()._value);
			UserData.RegisterType(typeof(MountedStabilizedTurretProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(MountedStabilizedTurretProxy), typeof(MountedWeaponProxy), (object v) => MountedWeaponProxy.New(((MountedStabilizedTurretProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<MountedStabilizedTurret>((Script s, MountedStabilizedTurret v) => DynValue.FromObject(s, MountedStabilizedTurretProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(MountedStabilizedTurret), (DynValue v) => v.ToObject<MountedStabilizedTurretProxy>()._value);
			UserData.RegisterType(typeof(ProjectileProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(ProjectileProxy), typeof(MonoBehaviourProxy), (object v) => MonoBehaviourProxy.New(((ProjectileProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Projectile>((Script s, Projectile v) => DynValue.FromObject(s, ProjectileProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Projectile), (DynValue v) => v.ToObject<ProjectileProxy>()._value);
			UserData.RegisterType(typeof(ExplodingProjectileProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(ExplodingProjectileProxy), typeof(ProjectileProxy), (object v) => ProjectileProxy.New(((ExplodingProjectileProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ExplodingProjectile>((Script s, ExplodingProjectile v) => DynValue.FromObject(s, ExplodingProjectileProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ExplodingProjectile), (DynValue v) => v.ToObject<ExplodingProjectileProxy>()._value);
			UserData.RegisterType(typeof(RocketProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(RocketProxy), typeof(ExplodingProjectileProxy), (object v) => ExplodingProjectileProxy.New(((RocketProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Rocket>((Script s, Rocket v) => DynValue.FromObject(s, RocketProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(Rocket), (DynValue v) => v.ToObject<RocketProxy>()._value);
			UserData.RegisterType(typeof(TargetSeekingMissileProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(TargetSeekingMissileProxy), typeof(RocketProxy), (object v) => RocketProxy.New(((TargetSeekingMissileProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<TargetSeekingMissile>((Script s, TargetSeekingMissile v) => DynValue.FromObject(s, TargetSeekingMissileProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(TargetSeekingMissile), (DynValue v) => v.ToObject<TargetSeekingMissileProxy>()._value);
			UserData.RegisterType(typeof(WireGuidedMissileProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(WireGuidedMissileProxy), typeof(RocketProxy), (object v) => RocketProxy.New(((WireGuidedMissileProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<WireGuidedMissile>((Script s, WireGuidedMissile v) => DynValue.FromObject(s, WireGuidedMissileProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(WireGuidedMissile), (DynValue v) => v.ToObject<WireGuidedMissileProxy>()._value);
			UserData.RegisterType(typeof(GrenadeProjectileProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(GrenadeProjectileProxy), typeof(ProjectileProxy), (object v) => ProjectileProxy.New(((GrenadeProjectileProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<GrenadeProjectile>((Script s, GrenadeProjectile v) => DynValue.FromObject(s, GrenadeProjectileProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(GrenadeProjectile), (DynValue v) => v.ToObject<GrenadeProjectileProxy>()._value);
			UserData.RegisterType(typeof(RigidbodyProjectileProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetOneToOneConversion(typeof(RigidbodyProjectileProxy), typeof(ProjectileProxy), (object v) => ProjectileProxy.New(((RigidbodyProjectileProxy)v)._value));
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RigidbodyProjectile>((Script s, RigidbodyProjectile v) => DynValue.FromObject(s, RigidbodyProjectileProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RigidbodyProjectile), (DynValue v) => v.ToObject<RigidbodyProjectileProxy>()._value);
			UserData.RegisterType(typeof(WPlayerProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPlayerCameraProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPlayerHudProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WActorManagerProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WWeaponManagerProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPhysicsProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPathfindingProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPathfindingNodeProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<WPathfindingNode>((Script s, WPathfindingNode v) => DynValue.FromObject(s, WPathfindingNodeProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(WPathfindingNode), (DynValue v) => v.ToObject<WPathfindingNodeProxy>()._value);
			UserData.RegisterType(typeof(WOverlayProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WWaterProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WWaitForSecondsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<WWaitForSeconds>((Script s, WWaitForSeconds v) => DynValue.FromObject(s, WWaitForSecondsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(WWaitForSeconds), (DynValue v) => v.ToObject<WWaitForSecondsProxy>()._value);
			UserData.RegisterType(typeof(WTimeProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WColorSchemeProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WGameManagerProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WMinimapProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WMathUtilsProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WCurrentEventProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WFollowupSpreadProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<WFollowupSpread>((Script s, WFollowupSpread v) => DynValue.FromObject(s, WFollowupSpreadProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(WFollowupSpread), (DynValue v) => v.ToObject<WFollowupSpreadProxy>()._value);
			UserData.RegisterType(typeof(WDebugProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WIngameDialogProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WSpawnUiProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPortraitGeneratorProxy), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(ScriptEventProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<ScriptEvent>((Script s, ScriptEvent v) => DynValue.FromObject(s, ScriptEventProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(ScriptEvent), (DynValue v) => v.ToObject<ScriptEventProxy>()._value);
			UserData.RegisterType(typeof(RavenscriptEventsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<RavenscriptEvents>((Script s, RavenscriptEvents v) => DynValue.FromObject(s, RavenscriptEventsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(RavenscriptEvents), (DynValue v) => v.ToObject<RavenscriptEventsProxy>()._value);
			UserData.RegisterType(typeof(SgmModifyRulesArgsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SgmModifyRulesArgs>((Script s, SgmModifyRulesArgs v) => DynValue.FromObject(s, SgmModifyRulesArgsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SgmModifyRulesArgs), (DynValue v) => v.ToObject<SgmModifyRulesArgsProxy>()._value);
			UserData.RegisterType(typeof(SgmSpawnActorsArgsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SgmSpawnActorsArgs>((Script s, SgmSpawnActorsArgs v) => DynValue.FromObject(s, SgmSpawnActorsArgsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SgmSpawnActorsArgs), (DynValue v) => v.ToObject<SgmSpawnActorsArgsProxy>()._value);
			UserData.RegisterType(typeof(SgmPointCapturedArgsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SgmPointCapturedArgs>((Script s, SgmPointCapturedArgs v) => DynValue.FromObject(s, SgmPointCapturedArgsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SgmPointCapturedArgs), (DynValue v) => v.ToObject<SgmPointCapturedArgsProxy>()._value);
			UserData.RegisterType(typeof(SgmActorDiedArgsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SgmActorDiedArgs>((Script s, SgmActorDiedArgs v) => DynValue.FromObject(s, SgmActorDiedArgsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SgmActorDiedArgs), (DynValue v) => v.ToObject<SgmActorDiedArgsProxy>()._value);
			UserData.RegisterType(typeof(SgmLoadoutAcceptedArgsProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<SgmLoadoutAcceptedArgs>((Script s, SgmLoadoutAcceptedArgs v) => DynValue.FromObject(s, SgmLoadoutAcceptedArgsProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(SgmLoadoutAcceptedArgs), (DynValue v) => v.ToObject<SgmLoadoutAcceptedArgsProxy>()._value);
			UserData.RegisterType(typeof(TextMeshProProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<TextMeshPro>((Script s, TextMeshPro v) => DynValue.FromObject(s, TextMeshProProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(TextMeshPro), (DynValue v) => v.ToObject<TextMeshProProxy>()._value);
			UserData.RegisterType(typeof(TextMeshProUGUIProxy), InteropAccessMode.Default, null);
			Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<TextMeshProUGUI>((Script s, TextMeshProUGUI v) => DynValue.FromObject(s, TextMeshProUGUIProxy.New(v)));
			Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.UserData, typeof(TextMeshProUGUI), (DynValue v) => v.ToObject<TextMeshProUGUIProxy>()._value);
			UserData.RegisterType(typeof(PrimitiveType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Space), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(SendMessageOptions), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(KeyCode), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(HumanBodyBones), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AvatarIKGoal), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AvatarIKHint), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(ForceMode), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AnimatorUpdateMode), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AnimatorCullingMode), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(RigidbodyConstraints), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(VideoRenderMode), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Vehicle.ArmorRating), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WTeam), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WRaycastTarget), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WWeaponSlot), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WPathfindingNodeType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(SteelInput.KeyBinds), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Squad.FormationType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Order.OrderType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(VehicleSpawner.VehicleSpawnType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(TurretSpawner.TurretSpawnType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WeaponManager.WeaponSlot), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WeaponManager.WeaponEntry.Distance), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(WeaponManager.WeaponEntry.LoadoutType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AudioMixer), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(EffectUi.FadeType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Seat.Type), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Weapon.Effectiveness), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Weapon.Difficulty), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(AiActorController.SkillLevel), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(DamageInfo.DamageSourceType), InteropAccessMode.Default, null);
			UserData.RegisterType(typeof(Weapon.WeaponRole), InteropAccessMode.Default, null);
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x001368F0 File Offset: 0x00134AF0
		public static void ExposeTypes(Script script)
		{
			script.Globals["Mathf"] = typeof(MathfProxy);
			script.Globals["Input"] = typeof(InputProxy);
			script.Globals["Mesh"] = typeof(MeshProxy);
			script.Globals["Material"] = typeof(MaterialProxy);
			script.Globals["Vector2"] = typeof(Vector2Proxy);
			script.Globals["Vector3"] = typeof(Vector3Proxy);
			script.Globals["Vector4"] = typeof(Vector4Proxy);
			script.Globals["Matrix4x4"] = typeof(Matrix4x4Proxy);
			script.Globals["Quaternion"] = typeof(QuaternionProxy);
			script.Globals["Color"] = typeof(ColorProxy);
			script.Globals["Gradient"] = typeof(GradientProxy);
			script.Globals["Transform"] = typeof(TransformProxy);
			script.Globals["RectTransform"] = typeof(RectTransformProxy);
			script.Globals["GameObject"] = typeof(GameObjectProxy);
			script.Globals["MeshRenderer"] = typeof(MeshRendererProxy);
			script.Globals["SkinnedMeshRenderer"] = typeof(SkinnedMeshRendererProxy);
			script.Globals["Renderer"] = typeof(RendererProxy);
			script.Globals["Camera"] = typeof(CameraProxy);
			script.Globals["Ray"] = typeof(RayProxy);
			script.Globals["RaycastHit"] = typeof(RaycastHitProxy);
			script.Globals["LineRenderer"] = typeof(LineRendererProxy);
			script.Globals["MonoBehaviour"] = typeof(MonoBehaviourProxy);
			script.Globals["Random"] = typeof(RandomProxy);
			script.Globals["Animator"] = typeof(AnimatorProxy);
			script.Globals["RuntimeAnimatorController"] = typeof(RuntimeAnimatorControllerProxy);
			script.Globals["Rigidbody"] = typeof(RigidbodyProxy);
			script.Globals["ParticleSystem"] = typeof(ParticleSystemProxy);
			script.Globals["Collider"] = typeof(ColliderProxy);
			script.Globals["BoxCollider"] = typeof(BoxColliderProxy);
			script.Globals["CapsuleCollider"] = typeof(CapsuleColliderProxy);
			script.Globals["SphereCollider"] = typeof(SphereColliderProxy);
			script.Globals["AudioSource"] = typeof(AudioSourceProxy);
			script.Globals["AudioClip"] = typeof(AudioClipProxy);
			script.Globals["Sprite"] = typeof(SpriteProxy);
			script.Globals["Light"] = typeof(LightProxy);
			script.Globals["Screen"] = typeof(ScreenProxy);
			script.Globals["Bounds"] = typeof(BoundsProxy);
			script.Globals["Plane"] = typeof(PlaneProxy);
			script.Globals["Texture"] = typeof(TextureProxy);
			script.Globals["RenderSettings"] = typeof(RenderSettingsProxy);
			script.Globals["CanvasGroup"] = typeof(CanvasGroupProxy);
			script.Globals["Canvas"] = typeof(CanvasProxy);
			script.Globals["AnimationCurve"] = typeof(AnimationCurveProxy);
			script.Globals["Rect"] = typeof(RectProxy);
			script.Globals["VideoPlayer"] = typeof(VideoPlayerProxy);
			script.Globals["VideoClip"] = typeof(VideoClipProxy);
			script.Globals["PlayableDirector"] = typeof(PlayableDirectorProxy);
			script.Globals["RawImage"] = typeof(RawImageProxy);
			script.Globals["Image"] = typeof(ImageProxy);
			script.Globals["Text"] = typeof(TextProxy);
			script.Globals["Button"] = typeof(ButtonProxy);
			script.Globals["Toggle"] = typeof(ToggleProxy);
			script.Globals["Dropdown"] = typeof(DropdownProxy);
			script.Globals["Slider"] = typeof(SliderProxy);
			script.Globals["InputField"] = typeof(InputFieldProxy);
			script.Globals["ScrollRect"] = typeof(ScrollRectProxy);
			script.Globals["ScriptedBehaviour"] = typeof(ScriptedBehaviourProxy);
			script.Globals["Actor"] = typeof(ActorProxy);
			script.Globals["AiActorController"] = typeof(AiActorControllerProxy);
			script.Globals["Ladder"] = typeof(LadderProxy);
			script.Globals["Vehicle"] = typeof(VehicleProxy);
			script.Globals["Engine"] = typeof(EngineProxy);
			script.Globals["Seat"] = typeof(SeatProxy);
			script.Globals["SpawnPoint"] = typeof(SpawnPointProxy);
			script.Globals["CapturePoint"] = typeof(CapturePointProxy);
			script.Globals["VehicleSpawner"] = typeof(VehicleSpawnerProxy);
			script.Globals["TurretSpawner"] = typeof(TurretSpawnerProxy);
			script.Globals["ResupplyCrate"] = typeof(ResupplyCrateProxy);
			script.Globals["Order"] = typeof(OrderProxy);
			script.Globals["Squad"] = typeof(SquadProxy);
			script.Globals["WeaponEntry"] = typeof(WeaponEntryProxy);
			script.Globals["LoadoutSet"] = typeof(LoadoutSetProxy);
			script.Globals["LoadoutPickStrategy"] = typeof(LoadoutPickStrategyProxy);
			script.Globals["Mutator"] = typeof(MutatorEntryProxy);
			script.Globals["MeanFilter"] = typeof(MeanFilterProxy);
			script.Globals["MeanFilterVector3"] = typeof(MeanFilterVector3Proxy);
			script.Globals["ActorSkin"] = typeof(ActorSkinProxy);
			script.Globals["MeshSkin"] = typeof(MeshSkinProxy);
			script.Globals["DataContainer"] = typeof(DataContainerProxy);
			script.Globals["EffectUi"] = typeof(EffectUiProxy);
			script.Globals["SoundBank"] = typeof(SoundBankProxy);
			script.Globals["DamageInfo"] = typeof(DamageInfoProxy);
			script.Globals["ExplosionInfo"] = typeof(ExplosionInfoProxy);
			script.Globals["HitInfo"] = typeof(HitInfoProxy);
			script.Globals["ExplosionConfiguration"] = typeof(ExplosionConfigurationProxy);
			script.Globals["Car"] = typeof(ArcadeCarProxy);
			script.Globals["Airplane"] = typeof(AirplaneProxy);
			script.Globals["Helicopter"] = typeof(HelicopterProxy);
			script.Globals["Boat"] = typeof(BoatProxy);
			script.Globals["AnimationDrivenVehicle"] = typeof(AnimationDrivenVehicleProxy);
			script.Globals["Weapon"] = typeof(WeaponProxy);
			script.Globals["MountedWeapon"] = typeof(MountedWeaponProxy);
			script.Globals["MountedStabilizedTurret"] = typeof(MountedStabilizedTurretProxy);
			script.Globals["Projectile"] = typeof(ProjectileProxy);
			script.Globals["ExplodingProjectile"] = typeof(ExplodingProjectileProxy);
			script.Globals["RocketProjectile"] = typeof(RocketProxy);
			script.Globals["TargetSeekingMissileProjectile"] = typeof(TargetSeekingMissileProxy);
			script.Globals["WireGuidedMissileProjectile"] = typeof(WireGuidedMissileProxy);
			script.Globals["GrenadeProjectile"] = typeof(GrenadeProjectileProxy);
			script.Globals["RigidbodyProjectile"] = typeof(RigidbodyProjectileProxy);
			script.Globals["Player"] = typeof(WPlayerProxy);
			script.Globals["PlayerCamera"] = typeof(WPlayerCameraProxy);
			script.Globals["PlayerHud"] = typeof(WPlayerHudProxy);
			script.Globals["ActorManager"] = typeof(WActorManagerProxy);
			script.Globals["WeaponManager"] = typeof(WWeaponManagerProxy);
			script.Globals["Physics"] = typeof(WPhysicsProxy);
			script.Globals["Pathfinding"] = typeof(WPathfindingProxy);
			script.Globals["PathfindingNode"] = typeof(WPathfindingNodeProxy);
			script.Globals["Overlay"] = typeof(WOverlayProxy);
			script.Globals["Water"] = typeof(WWaterProxy);
			script.Globals["WaitForSeconds"] = typeof(WWaitForSecondsProxy);
			script.Globals["Time"] = typeof(WTimeProxy);
			script.Globals["ColorScheme"] = typeof(WColorSchemeProxy);
			script.Globals["GameManager"] = typeof(WGameManagerProxy);
			script.Globals["Minimap"] = typeof(WMinimapProxy);
			script.Globals["MathUtils"] = typeof(WMathUtilsProxy);
			script.Globals["CurrentEvent"] = typeof(WCurrentEventProxy);
			script.Globals["FollowupSpread"] = typeof(WFollowupSpreadProxy);
			script.Globals["Debug"] = typeof(WDebugProxy);
			script.Globals["IngameDialog"] = typeof(WIngameDialogProxy);
			script.Globals["SpawnUi"] = typeof(WSpawnUiProxy);
			script.Globals["PortraitGenerator"] = typeof(WPortraitGeneratorProxy);
			script.Globals["ScriptEvent"] = typeof(ScriptEventProxy);
			script.Globals["GameEvents"] = typeof(RavenscriptEventsProxy);
			script.Globals["SgmModifyRulesArgs"] = typeof(SgmModifyRulesArgsProxy);
			script.Globals["SgmSpawnActorsArgs"] = typeof(SgmSpawnActorsArgsProxy);
			script.Globals["SgmPointCapturedArgs"] = typeof(SgmPointCapturedArgsProxy);
			script.Globals["SgmActorDiedArgs"] = typeof(SgmActorDiedArgsProxy);
			script.Globals["SgmLoadoutAcceptedArgs"] = typeof(SgmLoadoutAcceptedArgsProxy);
			script.Globals["TextMeshPro"] = typeof(TextMeshProProxy);
			script.Globals["TextMeshProUGUI"] = typeof(TextMeshProUGUIProxy);
			script.Globals["PrimitiveType"] = typeof(PrimitiveType);
			script.Globals["Space"] = typeof(Space);
			script.Globals["SendMessageOptions"] = typeof(SendMessageOptions);
			script.Globals["KeyCode"] = typeof(KeyCode);
			script.Globals["HumanBodyBones"] = typeof(HumanBodyBones);
			script.Globals["AvatarIKGoal"] = typeof(AvatarIKGoal);
			script.Globals["AvatarIKHint"] = typeof(AvatarIKHint);
			script.Globals["ForceMode"] = typeof(ForceMode);
			script.Globals["AnimatorUpdateMode"] = typeof(AnimatorUpdateMode);
			script.Globals["AnimatorCullingMode"] = typeof(AnimatorCullingMode);
			script.Globals["RigidbodyConstraints"] = typeof(RigidbodyConstraints);
			script.Globals["VideoRenderMode"] = typeof(VideoRenderMode);
			script.Globals["ArmorRating"] = typeof(Vehicle.ArmorRating);
			script.Globals["Team"] = typeof(WTeam);
			script.Globals["RaycastTarget"] = typeof(WRaycastTarget);
			script.Globals["WeaponSlot"] = typeof(WWeaponSlot);
			script.Globals["PathfindingNodeType"] = typeof(WPathfindingNodeType);
			script.Globals["KeyBinds"] = typeof(SteelInput.KeyBinds);
			script.Globals["FormationType"] = typeof(Squad.FormationType);
			script.Globals["OrderType"] = typeof(Order.OrderType);
			script.Globals["VehicleSpawnType"] = typeof(VehicleSpawner.VehicleSpawnType);
			script.Globals["TurretSpawnType"] = typeof(TurretSpawner.TurretSpawnType);
			script.Globals["WeaponSlot"] = typeof(WeaponManager.WeaponSlot);
			script.Globals["Distance"] = typeof(WeaponManager.WeaponEntry.Distance);
			script.Globals["LoadoutType"] = typeof(WeaponManager.WeaponEntry.LoadoutType);
			script.Globals["AudioMixer"] = typeof(AudioMixer);
			script.Globals["FadeType"] = typeof(EffectUi.FadeType);
			script.Globals["SeatCameraType"] = typeof(Seat.Type);
			script.Globals["Effectiveness"] = typeof(Weapon.Effectiveness);
			script.Globals["Difficulty"] = typeof(Weapon.Difficulty);
			script.Globals["SkillLevel"] = typeof(AiActorController.SkillLevel);
			script.Globals["DamageSourceType"] = typeof(DamageInfo.DamageSourceType);
			script.Globals["WeaponRole"] = typeof(Weapon.WeaponRole);
		}
	}
}
