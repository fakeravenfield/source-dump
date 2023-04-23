using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class DecalManager : MonoBehaviour
{
	// Token: 0x060008C5 RID: 2245 RVA: 0x00007C12 File Offset: 0x00005E12
	private void Awake()
	{
		DecalManager.instance = this;
		this.splatParticleSystem = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00007C26 File Offset: 0x00005E26
	public void ResetDecals()
	{
		this.InitMeshes();
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00068F90 File Offset: 0x00067190
	public void InitMeshes()
	{
		int dropdown = Options.GetDropdown(OptionDropdown.Id.Quality);
		if (dropdown == 0)
		{
			this.maxVerts = 800;
		}
		else if (dropdown == 1)
		{
			this.maxVerts = 8000;
		}
		else
		{
			this.maxVerts = 65532;
		}
		this.decalMeshData = new Dictionary<DecalManager.DecalType, DecalManager.MeshData>();
		this.vertexIndex = new Dictionary<DecalManager.DecalType, int>();
		Vector3[] array = new Vector3[this.maxVerts];
		Vector2[] array2 = new Vector2[this.maxVerts];
		Vector3[] array3 = new Vector3[this.maxVerts];
		int[] array4 = new int[3 * (this.maxVerts / 2)];
		for (int i = 0; i < this.maxVerts; i++)
		{
			array[i] = new Vector3((float)(i / 2 % 2), (float)((i + 1) / 2 % 2), 0f) * 0.0001f;
			array2[i] = new Vector2((float)(i / 2 % 2), (float)((i + 1) / 2 % 2));
			array3[i] = Vector3.up;
		}
		for (int j = 0; j < array4.Length / 6; j++)
		{
			array4[j * 6] = j * 4;
			array4[j * 6 + 1] = j * 4 + 1;
			array4[j * 6 + 2] = j * 4 + 2;
			array4[j * 6 + 3] = j * 4;
			array4[j * 6 + 4] = j * 4 + 2;
			array4[j * 6 + 5] = j * 4 + 3;
		}
		foreach (object obj in Enum.GetValues(typeof(DecalManager.DecalType)))
		{
			DecalManager.DecalType decalType = (DecalManager.DecalType)obj;
			Mesh mesh = new Mesh();
			DecalManager.MeshData meshData = new DecalManager.MeshData();
			mesh.name = decalType.ToString() + " mesh";
			meshData.mesh = mesh;
			meshData.verts = (Vector3[])array.Clone();
			meshData.uvs = (Vector2[])array2.Clone();
			meshData.normals = (Vector3[])array3.Clone();
			meshData.meshNeedsUpdating = true;
			meshData.UpdateMesh();
			mesh.triangles = array4;
			mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 9999f);
			mesh.MarkDynamic();
			this.decalDrawers[(int)decalType].GetComponent<MeshFilter>().mesh = mesh;
			this.decalMeshData.Add(decalType, meshData);
			this.vertexIndex.Add(decalType, 0);
		}
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00069228 File Offset: 0x00067428
	public static void AddDecal(Vector3 point, Vector3 normal, float size, DecalManager.DecalType type)
	{
		Vector3 vector = Vector3.Cross(normal, UnityEngine.Random.insideUnitSphere).normalized * (size / 2f);
		Vector3 vector2 = Vector3.Cross(normal, vector);
		point += normal * 0.03f;
		bool flag = DecalManager.CanSpawnDecal(point, vector, vector2, normal);
		if (!flag)
		{
			vector *= 0.3f;
			vector2 *= 0.3f;
		}
		if (flag || DecalManager.CanSpawnDecal(point, vector, vector2, normal))
		{
			DecalManager.instance.PushQuad(point - vector - vector2, point + vector - vector2, point + vector + vector2, point - vector + vector2, normal, type);
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x000692E4 File Offset: 0x000674E4
	private static bool CanSpawnDecal(Vector3 point, Vector3 up, Vector3 right, Vector3 normal)
	{
		return Physics.Raycast(point + (-up - right) * 0.3f, -normal, 0.23f, 1) && Physics.Raycast(point + (up - right) * 0.3f, -normal, 0.23f, 1) && Physics.Raycast(point + (up + right) * 0.3f, -normal, 0.23f, 1) && Physics.Raycast(point + (-up + right) * 0.3f, -normal, 0.23f, 1);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x000693A8 File Offset: 0x000675A8
	private void Update()
	{
		if (!GameManager.instance.ingame)
		{
			return;
		}
		foreach (object obj in Enum.GetValues(typeof(DecalManager.DecalType)))
		{
			DecalManager.DecalType key = (DecalManager.DecalType)obj;
			try
			{
				this.decalMeshData[key].UpdateMesh();
			}
			catch (Exception e)
			{
				ModManager.HandleModException(e);
			}
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00069438 File Offset: 0x00067638
	private void PushQuad(Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4, Vector3 normal, DecalManager.DecalType type)
	{
		DecalManager.MeshData meshData = this.decalMeshData[type];
		int num = this.vertexIndex[type];
		meshData.verts[num] = c1;
		meshData.verts[num + 1] = c2;
		meshData.verts[num + 2] = c3;
		meshData.verts[num + 3] = c4;
		meshData.normals[num] = normal;
		meshData.normals[num + 1] = normal;
		meshData.normals[num + 2] = normal;
		meshData.normals[num + 3] = normal;
		meshData.meshNeedsUpdating = true;
		this.vertexIndex[type] = (num + 4) % this.maxVerts;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x000694F8 File Offset: 0x000676F8
	public static void CreateBloodDrop(Vector3 point, Vector3 baseVelocity, int team)
	{
		Color color = ColorScheme.TeamColor(team);
		BloodParticle component = UnityEngine.Object.Instantiate<GameObject>(DecalManager.instance.bloodDropPrefab, point, Quaternion.identity).GetComponent<BloodParticle>();
		component.transform.localScale.Scale(Vector3.one * UnityEngine.Random.Range(2f, 3f));
		if (BloodParticle.BLOOD_PARTICLE_SETTING == BloodParticle.BloodParticleType.BloodExplosions)
		{
			component.velocity = baseVelocity + (UnityEngine.Random.insideUnitSphere + new Vector3(0f, 1.4f, 0f)) * 8f;
		}
		else if (BloodParticle.BLOOD_PARTICLE_SETTING == BloodParticle.BloodParticleType.DecalOnly)
		{
			component.velocity = baseVelocity * 2f + (UnityEngine.Random.insideUnitSphere + Vector3.down) * 2f;
		}
		else
		{
			component.velocity = baseVelocity + (UnityEngine.Random.insideUnitSphere + Vector3.up) * 2f;
		}
		component.team = team;
		Renderer component2 = component.GetComponent<Renderer>();
		if (BloodParticle.BLOOD_PARTICLE_SETTING == BloodParticle.BloodParticleType.DecalOnly)
		{
			component2.enabled = false;
			return;
		}
		component2.material.color = color;
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0006961C File Offset: 0x0006781C
	public static void EmitBloodEffect(Vector3 point, Vector3 baseVelocity, int team)
	{
		DecalManager.bloodParticleParams.position = point - baseVelocity.normalized * 0.4f;
		DecalManager.bloodParticleParams.velocity = -baseVelocity * 0.3f + Vector3.up + UnityEngine.Random.insideUnitSphere;
		DecalManager.bloodParticleParams.startColor = ColorScheme.TeamColor(team);
		DecalManager.bloodParticleParams.startSize = UnityEngine.Random.Range(0.7f, 3f);
		DecalManager.instance.splatParticleSystem.Emit(DecalManager.bloodParticleParams, 1);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00007C2E File Offset: 0x00005E2E
	public static void SetBloodDecalColor(int team, Color color)
	{
		DecalManager.instance.decalDrawers[team + 1].GetComponent<Renderer>().material.SetColor("_Color", color);
	}

	// Token: 0x04000970 RID: 2416
	private const int SCENERY_MASK = 1;

	// Token: 0x04000971 RID: 2417
	private const int MAX_DECALS = 16383;

	// Token: 0x04000972 RID: 2418
	private const int MAX_VERTS_PER_MESH = 65532;

	// Token: 0x04000973 RID: 2419
	private const float MESH_BOUNDS = 9999f;

	// Token: 0x04000974 RID: 2420
	private const float NORMAL_OFFSET = 0.03f;

	// Token: 0x04000975 RID: 2421
	private const float MAX_SCENERY_DISTANCE = 0.2f;

	// Token: 0x04000976 RID: 2422
	public static DecalManager instance;

	// Token: 0x04000977 RID: 2423
	public GameObject impactPrefab;

	// Token: 0x04000978 RID: 2424
	public GameObject bloodPrefab;

	// Token: 0x04000979 RID: 2425
	public GameObject bloodDropPrefab;

	// Token: 0x0400097A RID: 2426
	public GameObject[] decalDrawers;

	// Token: 0x0400097B RID: 2427
	private Dictionary<DecalManager.DecalType, int> vertexIndex;

	// Token: 0x0400097C RID: 2428
	private Dictionary<DecalManager.DecalType, DecalManager.MeshData> decalMeshData;

	// Token: 0x0400097D RID: 2429
	private ParticleSystem splatParticleSystem;

	// Token: 0x0400097E RID: 2430
	private int maxVerts;

	// Token: 0x0400097F RID: 2431
	private static ParticleSystem.EmitParams bloodParticleParams = new ParticleSystem.EmitParams
	{
		startLifetime = 0.25f
	};

	// Token: 0x02000137 RID: 311
	public enum DecalType
	{
		// Token: 0x04000981 RID: 2433
		Impact,
		// Token: 0x04000982 RID: 2434
		BloodBlue,
		// Token: 0x04000983 RID: 2435
		BloodRed
	}

	// Token: 0x02000138 RID: 312
	private class MeshData
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x000696E4 File Offset: 0x000678E4
		public void UpdateMesh()
		{
			if (!this.meshNeedsUpdating)
			{
				return;
			}
			this.mesh.vertices = this.verts;
			this.mesh.normals = this.normals;
			this.mesh.uv = this.uvs;
			this.meshNeedsUpdating = false;
		}

		// Token: 0x04000984 RID: 2436
		public Mesh mesh;

		// Token: 0x04000985 RID: 2437
		public Vector3[] verts;

		// Token: 0x04000986 RID: 2438
		public Vector3[] normals;

		// Token: 0x04000987 RID: 2439
		public Vector2[] uvs;

		// Token: 0x04000988 RID: 2440
		public bool meshNeedsUpdating = true;
	}
}
