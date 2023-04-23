using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200014C RID: 332
public class IRFlaresRenderer : MonoBehaviour
{
	// Token: 0x0600090F RID: 2319 RVA: 0x00007FAD File Offset: 0x000061AD
	public static void SetFlareRenderingEnabled(bool enabled)
	{
		if (IRFlaresRenderer.instance != null)
		{
			IRFlaresRenderer.instance.drawFlares = enabled;
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00007FC7 File Offset: 0x000061C7
	private void Awake()
	{
		IRFlaresRenderer.instance = this;
		IRFlaresRenderer.BRIGHTNESS_MATERIAL_ID = Shader.PropertyToID("_Brightness");
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00069E28 File Offset: 0x00068028
	private void Update()
	{
		if (!this.drawFlares || GameManager.IsSpectating())
		{
			return;
		}
		Component activeCamera = FpsActorController.instance.GetActiveCamera();
		List<Actor> list = ActorManager.AliveActorsOnTeam(GameManager.PlayerTeam());
		List<Matrix4x4> list2 = new List<Matrix4x4>();
		HashSet<Vehicle> hashSet = new HashSet<Vehicle>();
		Vector3 position = activeCamera.transform.position;
		foreach (Actor actor in list)
		{
			if (actor.aiControlled && ActorManager.ActorCanSeePlayer(actor))
			{
				float num = 1.5f;
				Vector3 vector = actor.CenterPosition();
				if (actor.IsSeated() && actor.seat.enclosed)
				{
					Vehicle vehicle = actor.seat.vehicle;
					if (vehicle.playerIsInside || hashSet.Contains(vehicle))
					{
						continue;
					}
					hashSet.Add(vehicle);
					num = Mathf.Clamp(vehicle.GetAvoidanceCoarseRadius() * 1.2f, 3f, 15f);
					vector = vehicle.targetLockPoint.position;
				}
				vector.y += 0.25f;
				Vector3 forward = vector - position;
				list2.Add(Matrix4x4.TRS(vector, Quaternion.LookRotation(forward), new Vector3(num, num, num)));
			}
		}
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		float value = 0.06f + Mathf.Sin(Time.time * 8f) * 0.04f;
		materialPropertyBlock.SetFloat(IRFlaresRenderer.BRIGHTNESS_MATERIAL_ID, value);
		Graphics.DrawMeshInstanced(this.mesh, 0, this.material, list2, materialPropertyBlock, ShadowCastingMode.Off, false, 0);
	}

	// Token: 0x040009DA RID: 2522
	private static IRFlaresRenderer instance;

	// Token: 0x040009DB RID: 2523
	private static int BRIGHTNESS_MATERIAL_ID;

	// Token: 0x040009DC RID: 2524
	private const float BRIGHTNESS_BASE = 0.06f;

	// Token: 0x040009DD RID: 2525
	private const float BRIGHTNESS_WAVE_AMPLITUDE = 0.04f;

	// Token: 0x040009DE RID: 2526
	private const float BRIGHTNESS_WAVE_FREQUENCY = 8f;

	// Token: 0x040009DF RID: 2527
	[NonSerialized]
	public bool drawFlares;

	// Token: 0x040009E0 RID: 2528
	public Mesh mesh;

	// Token: 0x040009E1 RID: 2529
	public Material material;
}
