using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000988 RID: 2440
	[Name("PlayerCamera")]
	[Doc("Use these methods to access the player cameras.")]
	public static class WPlayerCamera
	{
		// Token: 0x06003DEF RID: 15855 RVA: 0x00029E00 File Offset: 0x00028000
		[Doc("Instantly resets any first person weapon recoil.[..]")]
		public static void ResetRecoil()
		{
			PlayerFpParent.instance.ResetRecoil();
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x00029E0C File Offset: 0x0002800C
		[Doc("Makes the first person weapon wobble up and down.[..]")]
		public static void ApplyWeaponSnap(float magnitude, float frequency, float duration)
		{
			PlayerFpParent.instance.ApplyWeaponSnap(magnitude, duration, frequency);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x00029E1B File Offset: 0x0002801B
		[Doc("Apply a recoil on the first person weapon. Optionally also kicks the camera.[..]")]
		public static void ApplyRecoil(Vector3 recoil, bool applyCameraKick)
		{
			PlayerFpParent.instance.ApplyRecoil(recoil, applyCameraKick);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x00029E29 File Offset: 0x00028029
		[Doc("Apply a screen shake effect.[..]")]
		public static void ApplyScreenshake(float magnitude, int iterations)
		{
			PlayerFpParent.instance.ApplyScreenshake(magnitude, iterations);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00029E37 File Offset: 0x00028037
		[Doc("Kick the first person camera by an euler rotation.[..]")]
		public static void KickCamera(Vector3 kickEuler)
		{
			PlayerFpParent.instance.KickCamera(kickEuler);
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x00029E44 File Offset: 0x00028044
		// (set) Token: 0x06003DF5 RID: 15861 RVA: 0x00029E5A File Offset: 0x0002805A
		[Doc("The first person camera's local offset position.[..] This is an offset from the default value, and is always Vector3.zero by default.")]
		public static Vector3 fpCameraLocalPosition
		{
			get
			{
				return PlayerFpParent.instance.fpCamera.transform.localPosition;
			}
			set
			{
				PlayerFpParent.instance.fpCamera.transform.localPosition = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x00029E71 File Offset: 0x00028071
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x00029E87 File Offset: 0x00028087
		[Doc("The first person camera's local offset rotation.[..] This is an offset from the default value, and is always Quaternion.identity by default.")]
		public static Quaternion fpCameraLocalRotation
		{
			get
			{
				return PlayerFpParent.instance.fpCamera.transform.localRotation;
			}
			set
			{
				PlayerFpParent.instance.fpCamera.transform.localRotation = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x00029E9E File Offset: 0x0002809E
		[Doc("Get the player's currently active camera.[..]")]
		public static Camera activeCamera
		{
			get
			{
				return FpsActorController.instance.GetActiveCamera();
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x00029EAA File Offset: 0x000280AA
		[Doc("True when an override camera is active.[..] An override camera is typically used when the player enters a vehicle with a custom camera setup.")]
		public static bool isUsingOverrideCamera
		{
			get
			{
				return FpsActorController.instance.IsUsingOverrideCamera();
			}
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x00029EB6 File Offset: 0x000280B6
		[Doc("Override the active camera.[..]")]
		public static void OverrideActiveCamera(Camera camera)
		{
			FpsActorController.instance.SetOverrideCamera(camera);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00029EC3 File Offset: 0x000280C3
		[Doc("Cancel the camera override.[..] Returns to the player's default first person or third person camera.")]
		public static void CancelOverrideCamera()
		{
			FpsActorController.instance.CancelOverrideCamera();
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00029ECF File Offset: 0x000280CF
		[Doc("Switch to first person camera.")]
		public static void FirstPersonCamera()
		{
			FpsActorController.instance.FirstPersonCamera();
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00029EDB File Offset: 0x000280DB
		[Doc("Switch to third person camera.")]
		public static void ThirdPersonCamera()
		{
			FpsActorController.instance.ThirdPersonCamera();
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x00029EE7 File Offset: 0x000280E7
		[Doc("Get the player's first person camera.[..] This is the camera that is active while the player is running around as infantry or in a vehicle without a special camera.")]
		public static Camera fpCamera
		{
			get
			{
				return FpsActorController.instance.fpCamera;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x00029EF3 File Offset: 0x000280F3
		[Doc("Get the player's third person camera.[..] This is the camera that is active while the player is ragdolled. This is not the same as the third person camera while inside vehicles.")]
		public static Camera tpCamera
		{
			get
			{
				return FpsActorController.instance.tpCamera;
			}
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00029EFF File Offset: 0x000280FF
		[Doc("Rotates the player camera, simulating player mouse movement.")]
		public static void RotateFirstPersonCamera(Vector2 rotation)
		{
			FpsActorController.instance.controller.m_MouseLook.ApplyScriptedRotation(rotation);
		}
	}
}
