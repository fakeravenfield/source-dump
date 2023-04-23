using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class WaterLevel : MonoBehaviour
{
	// Token: 0x06000CAD RID: 3245 RVA: 0x0000A608 File Offset: 0x00008808
	public static float GetHeight()
	{
		return WaterLevel.instance.height;
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00079A28 File Offset: 0x00077C28
	public static bool IsInWater(Vector3 position)
	{
		float num;
		return WaterLevel.IsInWater(position, out num);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00079A40 File Offset: 0x00077C40
	public static void ForceFindWaterVolumesForPathfinding()
	{
		WaterLevel.instance = UnityEngine.Object.FindObjectOfType<WaterLevel>();
		WaterLevel.instance.InitializeWaterVolumeContainers();
		WaterVolume[] array = UnityEngine.Object.FindObjectsOfType<WaterVolume>();
		for (int i = 0; i < array.Length; i++)
		{
			WaterLevel.RegisterWaterVolume(array[i]);
		}
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00079A80 File Offset: 0x00077C80
	public static bool IsInWater(Vector3 position, out float depth)
	{
		position.y += (Mathf.PerlinNoise(Time.time * 0.5f % 10000f + position.x * 0.3f, position.z * 0.3f) - 0.5f) * 0.3f;
		depth = WaterLevel.instance.height - position.y;
		if (position.y <= WaterLevel.instance.height)
		{
			return true;
		}
		foreach (WaterVolume waterVolume in WaterLevel.instance.waterVolumes)
		{
			Vector3 vector = WaterLevel.instance.waterVolumeWorldtoLocalMatrix[waterVolume].MultiplyPoint(position);
			if (Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.y) < 0.5f && Mathf.Abs(vector.z) < 0.5f)
			{
				depth = waterVolume.transform.localToWorldMatrix.MultiplyPoint(new Vector3(0f, 0.5f, 0f)).y - position.y;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00079BD4 File Offset: 0x00077DD4
	public static void RegisterWaterVolume(WaterVolume waterVolume)
	{
		WaterLevel.instance.waterVolumes.Add(waterVolume);
		WaterLevel.instance.waterVolumeWorldtoLocalMatrix.Add(waterVolume, waterVolume.transform.worldToLocalMatrix);
		WaterLevel.instance.waterVolumeLocalToWorldMatrix.Add(waterVolume, waterVolume.transform.localToWorldMatrix);
		Matrix4x4 localToWorldMatrix = waterVolume.transform.localToWorldMatrix;
		WaterLevel.instance.waterVolumePlanes.Add(waterVolume, new UnityEngine.Plane(localToWorldMatrix.MultiplyVector(Vector3.up), localToWorldMatrix.MultiplyPoint(new Vector3(0f, 0.5f, 0f))));
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0000A614 File Offset: 0x00008814
	public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
	{
		return WaterLevel.Raycast(ray, out hitInfo) && hitInfo.distance <= maxDistance;
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00079C70 File Offset: 0x00077E70
	public static bool Raycast(Ray ray, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (ray.origin.y < WaterLevel.instance.height || ray.direction.y > 0f)
		{
			return false;
		}
		float num;
		bool flag = WaterLevel.instance.plane.Raycast(ray, out num);
		if (flag)
		{
			hitInfo.distance = num;
			hitInfo.point = ray.origin + ray.direction * num;
			hitInfo.normal = Vector3.up;
		}
		RaycastHit raycastHit;
		if (WaterLevel.VolumeRaycast(ray, out raycastHit) && raycastHit.distance < hitInfo.distance)
		{
			hitInfo = raycastHit;
			return true;
		}
		return flag;
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00079D1C File Offset: 0x00077F1C
	private static bool VolumeRaycast(Ray ray, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		hitInfo.distance = float.MaxValue;
		float maxValue = float.MaxValue;
		foreach (WaterVolume key in WaterLevel.instance.waterVolumes)
		{
			bool flag = WaterLevel.instance.waterVolumePlanes[key].Raycast(ray, out maxValue);
			Vector3 point = ray.origin + ray.direction * maxValue;
			Vector3 vector = WaterLevel.instance.waterVolumeWorldtoLocalMatrix[key].MultiplyPoint(point);
			if (flag && maxValue < hitInfo.distance && Mathf.Abs(vector.x) < 0.5f && Mathf.Abs(vector.z) < 0.5f)
			{
				hitInfo.distance = maxValue;
				hitInfo.point = ray.origin + ray.direction * maxValue;
				hitInfo.normal = Vector3.up;
			}
		}
		return hitInfo.distance < float.MaxValue;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0000A62D File Offset: 0x0000882D
	private void InitializeWaterVolumeContainers()
	{
		this.waterVolumeWorldtoLocalMatrix = new Dictionary<WaterVolume, Matrix4x4>();
		this.waterVolumeLocalToWorldMatrix = new Dictionary<WaterVolume, Matrix4x4>();
		this.waterVolumePlanes = new Dictionary<WaterVolume, UnityEngine.Plane>();
		this.waterVolumes = new List<WaterVolume>();
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x0000A65B File Offset: 0x0000885B
	private void Awake()
	{
		WaterLevel.instance = this;
		this.InitializeWaterVolumeContainers();
		this.height = base.transform.position.y;
		this.plane = new UnityEngine.Plane(Vector3.up, base.transform.position);
	}

	// Token: 0x04000DAE RID: 3502
	private const float WAVE_NOISE_AMPLITUDE = 0.3f;

	// Token: 0x04000DAF RID: 3503
	private const float WAVE_NOISE_FREQUENCY = 0.3f;

	// Token: 0x04000DB0 RID: 3504
	private const float WAVE_NOISE_SCROLL_SPEED = 0.5f;

	// Token: 0x04000DB1 RID: 3505
	public static WaterLevel instance;

	// Token: 0x04000DB2 RID: 3506
	[NonSerialized]
	public float height;

	// Token: 0x04000DB3 RID: 3507
	private UnityEngine.Plane plane;

	// Token: 0x04000DB4 RID: 3508
	[NonSerialized]
	public List<WaterVolume> waterVolumes;

	// Token: 0x04000DB5 RID: 3509
	private Dictionary<WaterVolume, Matrix4x4> waterVolumeWorldtoLocalMatrix;

	// Token: 0x04000DB6 RID: 3510
	private Dictionary<WaterVolume, Matrix4x4> waterVolumeLocalToWorldMatrix;

	// Token: 0x04000DB7 RID: 3511
	private Dictionary<WaterVolume, UnityEngine.Plane> waterVolumePlanes;
}
