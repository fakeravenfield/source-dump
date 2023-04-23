using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A23 RID: 2595
	[Proxy(typeof(WPlayerCamera))]
	public class WPlayerCameraProxy : IProxy
	{
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x0003CFAB File Offset: 0x0003B1AB
		public static CameraProxy activeCamera
		{
			get
			{
				return CameraProxy.New(WPlayerCamera.activeCamera);
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x0003CFB7 File Offset: 0x0003B1B7
		public static CameraProxy fpCamera
		{
			get
			{
				return CameraProxy.New(WPlayerCamera.fpCamera);
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x0003CFC3 File Offset: 0x0003B1C3
		// (set) Token: 0x060052A0 RID: 21152 RVA: 0x0003CFCF File Offset: 0x0003B1CF
		public static Vector3Proxy fpCameraLocalPosition
		{
			get
			{
				return Vector3Proxy.New(WPlayerCamera.fpCameraLocalPosition);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				WPlayerCamera.fpCameraLocalPosition = value._value;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x0003CFEA File Offset: 0x0003B1EA
		// (set) Token: 0x060052A2 RID: 21154 RVA: 0x0003CFF6 File Offset: 0x0003B1F6
		public static QuaternionProxy fpCameraLocalRotation
		{
			get
			{
				return QuaternionProxy.New(WPlayerCamera.fpCameraLocalRotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				WPlayerCamera.fpCameraLocalRotation = value._value;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x0003D011 File Offset: 0x0003B211
		public static bool isUsingOverrideCamera
		{
			get
			{
				return WPlayerCamera.isUsingOverrideCamera;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x0003D018 File Offset: 0x0003B218
		public static CameraProxy tpCamera
		{
			get
			{
				return CameraProxy.New(WPlayerCamera.tpCamera);
			}
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0003D024 File Offset: 0x0003B224
		public static void ApplyRecoil(Vector3Proxy recoil, bool applyCameraKick)
		{
			if (recoil == null)
			{
				throw new ScriptRuntimeException("argument 'recoil' is nil");
			}
			WPlayerCamera.ApplyRecoil(recoil._value, applyCameraKick);
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0003D040 File Offset: 0x0003B240
		public static void ApplyScreenshake(float magnitude, int iterations)
		{
			WPlayerCamera.ApplyScreenshake(magnitude, iterations);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0003D049 File Offset: 0x0003B249
		public static void ApplyWeaponSnap(float magnitude, float frequency, float duration)
		{
			WPlayerCamera.ApplyWeaponSnap(magnitude, frequency, duration);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0003D053 File Offset: 0x0003B253
		public static void CancelOverrideCamera()
		{
			WPlayerCamera.CancelOverrideCamera();
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0003D05A File Offset: 0x0003B25A
		public static void FirstPersonCamera()
		{
			WPlayerCamera.FirstPersonCamera();
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0003D061 File Offset: 0x0003B261
		public static void KickCamera(Vector3Proxy kickEuler)
		{
			if (kickEuler == null)
			{
				throw new ScriptRuntimeException("argument 'kickEuler' is nil");
			}
			WPlayerCamera.KickCamera(kickEuler._value);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x001393F0 File Offset: 0x001375F0
		public static void OverrideActiveCamera(CameraProxy camera)
		{
			Camera camera2 = null;
			if (camera != null)
			{
				camera2 = camera._value;
			}
			WPlayerCamera.OverrideActiveCamera(camera2);
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x0003D07C File Offset: 0x0003B27C
		public static void ResetRecoil()
		{
			WPlayerCamera.ResetRecoil();
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0003D083 File Offset: 0x0003B283
		public static void RotateFirstPersonCamera(Vector2Proxy rotation)
		{
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			WPlayerCamera.RotateFirstPersonCamera(rotation._value);
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0003D09E File Offset: 0x0003B29E
		public static void ThirdPersonCamera()
		{
			WPlayerCamera.ThirdPersonCamera();
		}
	}
}
