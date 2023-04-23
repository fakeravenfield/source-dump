using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000251 RID: 593
public class SpiritTrack : MonoBehaviour
{
	// Token: 0x06001054 RID: 4180 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
	private void Start()
	{
		SpiritTrack.instance = this;
		this.seeker = base.GetComponent<Seeker>();
		base.Invoke("Setup", 0.1f);
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0008888C File Offset: 0x00086A8C
	public static bool[] GetScrolls()
	{
		return new bool[]
		{
			PlayerPrefs.HasKey("scroll_1"),
			PlayerPrefs.HasKey("scroll_2"),
			PlayerPrefs.HasKey("scroll_3"),
			PlayerPrefs.HasKey("scroll_4"),
			PlayerPrefs.HasKey("scroll_5"),
			PlayerPrefs.HasKey("scroll_6")
		};
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x000888F0 File Offset: 0x00086AF0
	public static bool HasAllScrolls()
	{
		bool[] scrolls = SpiritTrack.GetScrolls();
		for (int i = 0; i < scrolls.Length; i++)
		{
			if (!scrolls[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
	private void Update()
	{
		if (this.tracks != null && this.encounterIndex < this.generatedTracks)
		{
			this.tracks[this.encounterIndex].Update();
		}
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x0008891C File Offset: 0x00086B1C
	private void Setup()
	{
		try
		{
			this.infantryGraphs = new List<RecastGraph>();
			foreach (NavGraph navGraph in AstarPath.active.graphs)
			{
				if (navGraph != null && navGraph.active)
				{
					RecastGraph recastGraph = navGraph as RecastGraph;
					if (recastGraph != null && PathfindingManager.GetTypeFromGraphName(recastGraph) == PathfindingBox.Type.Infantry)
					{
						this.infantryGraphs.Add(recastGraph);
					}
				}
			}
			List<SpawnPoint> list = new List<SpawnPoint>(ActorManager.instance.spawnPoints);
			list.RemoveAll((SpawnPoint s) => !s.isActiveAndEnabled || s.isGhostSpawn || s.nLandConnections < 2);
			if (list.Count == 0)
			{
				base.enabled = false;
			}
			Vector3 spawnPosition = list[UnityEngine.Random.Range(0, list.Count)].GetSpawnPosition();
			this.rootNode = PathfindingManager.instance.FindClosestNode(spawnPosition, PathfindingBox.Type.Infantry, -1);
			this.possibleNodes = new List<GraphNode>();
			this.infantryGraphs.ForEach(delegate(RecastGraph graph)
			{
				graph.GetNodes(delegate(GraphNode node)
				{
					if (node.Area == this.rootNode.Area && node.Tag != 3U)
					{
						this.possibleNodes.Add(node);
					}
				});
			});
			this.RemoveNodesAround(this.possibleNodes, spawnPosition);
			this.possibleNodes.Sort((GraphNode a, GraphNode b) => a.SurfaceArea().CompareTo(b.SurfaceArea()));
			this.possibleNodes.RemoveRange(0, (int)((float)this.possibleNodes.Count * 0.7f));
			this.GenerateNewEncounters();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			base.enabled = false;
		}
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x00088AA4 File Offset: 0x00086CA4
	private void GenerateNewEncounters()
	{
		this.encounterIndex = -1;
		this.availableNodes = new List<GraphNode>(this.possibleNodes);
		if (SceneManager.GetActiveScene().buildIndex == 7 && SpiritTrack.HasAllScrolls())
		{
			this.GenerateSequence();
			return;
		}
		this.GenerateSingle();
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0000D11E File Offset: 0x0000B31E
	private void GenerateSequence()
	{
		this.generatedTracks = this.sequencePrefabs.Length;
		this.sequence = true;
		this.RemoveNodesAround(this.availableNodes, SpiritTrack.ARTIFACT_LOCATION);
		this.GenerateNextEncounterNode();
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x00088AF0 File Offset: 0x00086CF0
	private void GenerateSingle()
	{
		this.generatedTracks = 1;
		this.sequence = false;
		bool[] scrolls = SpiritTrack.GetScrolls();
		int num = Mathf.Min(this.prefabs.Length, scrolls.Length);
		int num2 = UnityEngine.Random.Range(0, num);
		for (int i = 0; i < num; i++)
		{
			this.targetIndex = (num2 + i) % num;
			if (!scrolls[this.targetIndex])
			{
				break;
			}
		}
		this.GenerateNextEncounterNode();
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x00088B54 File Offset: 0x00086D54
	private void GenerateNextEncounterNode()
	{
		this.encounterIndex++;
		if (this.encounterIndex >= this.generatedTracks)
		{
			this.GenerationDone();
			return;
		}
		if (this.encounterIndex < 2)
		{
			Vector3 vector = (Vector3)this.availableNodes[UnityEngine.Random.Range(0, this.availableNodes.Count)].position;
			this.encounterNodes[this.encounterIndex] = vector;
			this.RemoveNodesAround(this.availableNodes, vector);
		}
		else
		{
			this.encounterNodes[this.encounterIndex] = SpiritTrack.ARTIFACT_LOCATION;
		}
		if (this.encounterIndex > 0)
		{
			this.FindPath(this.encounterNodes[this.encounterIndex - 1], this.encounterNodes[this.encounterIndex]);
			return;
		}
		this.FindPath((Vector3)this.rootNode.position, this.encounterNodes[this.encounterIndex]);
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x00088C48 File Offset: 0x00086E48
	private void RemoveNodesAround(List<GraphNode> list, Vector3 position)
	{
		list.RemoveAll((GraphNode n) => Vector3.Distance((Vector3)n.position, position) < 60f);
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x00088C78 File Offset: 0x00086E78
	private void GenerationDone()
	{
		this.encounterIndex = 0;
		this.tracks = new SpiritTrack.Track[this.generatedTracks];
		for (int i = 0; i < this.generatedTracks; i++)
		{
			List<Vector3> path = this.encounterPaths[i];
			float pathLength;
			Mesh mesh = this.GeneratePathGeometry(path, out pathLength);
			GameObject gameObject = new GameObject("Path Renderer");
			gameObject.AddComponent<MeshFilter>().mesh = mesh;
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			meshRenderer.material = new Material(this.crumbMaterial);
			this.tracks[i] = new SpiritTrack.Track(path, pathLength, meshRenderer);
			if (this.sequence)
			{
				this.tracks[i].prefab = this.sequencePrefabs[i];
			}
			else
			{
				this.tracks[i].prefab = this.prefabs[this.targetIndex];
			}
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localRotation = Quaternion.identity;
			if (i == 0)
			{
				this.tracks[i].Show();
			}
			else
			{
				this.tracks[i].Hide();
			}
		}
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x00088DA0 File Offset: 0x00086FA0
	public void NextTrack()
	{
		this.encounterIndex++;
		if (this.encounterIndex >= this.generatedTracks)
		{
			return;
		}
		this.tracks[this.encounterIndex - 1].Hide();
		this.tracks[this.encounterIndex].Show();
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x00088DF0 File Offset: 0x00086FF0
	private Mesh GeneratePathGeometry(List<Vector3> path, out float pathLength)
	{
		pathLength = 0f;
		for (int i = 0; i < path.Count - 1; i++)
		{
			Debug.DrawLine(path[i], path[i + 1], Color.blue, 100f);
			pathLength += Vector3.Distance(path[i + 1], path[i]);
		}
		float num = Mathf.Min(pathLength * 1f, 1820f) / pathLength;
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		List<Vector2> list3 = new List<Vector2>();
		int num2 = 0;
		float num3 = 0f;
		for (int j = 0; j < path.Count - 1; j++)
		{
			num3 += Vector3.Distance(path[j + 1], path[j]);
			int num4 = (int)(num3 * num);
			for (int k = num2; k < num4; k++)
			{
				Vector3 origin = path[j + 1] + UnityEngine.Random.insideUnitSphere * 1f;
				origin.y += 5f;
				this.GenerateCrumbGroup(origin, num3, list, list2, list3);
			}
			num2 = num4;
		}
		Mesh mesh = new Mesh();
		mesh.vertices = list.ToArray();
		mesh.triangles = list2.ToArray();
		mesh.uv = list3.ToArray();
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		return mesh;
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x00088F50 File Offset: 0x00087150
	private void GenerateCrumbGroup(Vector3 origin, float distance, List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(origin, Vector3.down), out raycastHit, 25f, 2232321))
		{
			origin = raycastHit.point;
			for (int i = 0; i < 3; i++)
			{
				Vector3 b = (UnityEngine.Random.insideUnitSphere * 0.3f).ToGround();
				b.y += -0.1f;
				Vector3 pos = origin + b;
				float num = UnityEngine.Random.Range(0.03f, 0.1f);
				Matrix4x4 matrix4x = Matrix4x4.TRS(pos, UnityEngine.Random.rotation, new Vector3(num, num, num));
				float y = UnityEngine.Random.Range(0f, 1f);
				for (int j = 0; j < 12; j++)
				{
					triangles.Add(vertices.Count + j);
					uvs.Add(new Vector2(distance, y));
				}
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(0f, -1f, 1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(-1f, -1f, -1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(1f, -1f, -1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(-1f, -1f, -1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(0f, -1f, 1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(0f, 1f, 0f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(0f, -1f, 1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(1f, -1f, -1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(0f, 1f, 0f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(1f, -1f, -1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(-1f, -1f, -1f)));
				vertices.Add(matrix4x.MultiplyPoint(new Vector3(0f, 1f, 0f)));
			}
			return;
		}
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0000D14C File Offset: 0x0000B34C
	public void FindPath(Vector3 from, Vector3 to)
	{
		this.seeker.StartPath(from, to, new OnPathDelegate(this.OnPathCompleted), PathfindingManager.GetMaskFromGraphType(PathfindingBox.Type.Infantry));
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x0000D173 File Offset: 0x0000B373
	private void OnPathCompleted(Path path)
	{
		if (!path.error)
		{
			this.encounterPaths[this.encounterIndex] = path.vectorPath;
			this.GenerateNextEncounterNode();
			return;
		}
		this.GenerateNewEncounters();
	}

	// Token: 0x0400115E RID: 4446
	private const int MAX_NODE_COUNT = 3;

	// Token: 0x0400115F RID: 4447
	private static readonly Vector3 ARTIFACT_LOCATION = new Vector3(992.7f, 318.66f, 974.73f);

	// Token: 0x04001160 RID: 4448
	public static SpiritTrack instance;

	// Token: 0x04001161 RID: 4449
	private List<RecastGraph> infantryGraphs;

	// Token: 0x04001162 RID: 4450
	private Vector3[] encounterNodes = new Vector3[3];

	// Token: 0x04001163 RID: 4451
	private List<Vector3>[] encounterPaths = new List<Vector3>[3];

	// Token: 0x04001164 RID: 4452
	private SpiritTrack.Track[] tracks;

	// Token: 0x04001165 RID: 4453
	private List<GraphNode> possibleNodes;

	// Token: 0x04001166 RID: 4454
	private List<GraphNode> availableNodes;

	// Token: 0x04001167 RID: 4455
	private GraphNode rootNode;

	// Token: 0x04001168 RID: 4456
	private Seeker seeker;

	// Token: 0x04001169 RID: 4457
	public Material crumbMaterial;

	// Token: 0x0400116A RID: 4458
	public GameObject[] prefabs;

	// Token: 0x0400116B RID: 4459
	public GameObject[] sequencePrefabs;

	// Token: 0x0400116C RID: 4460
	private bool sequence;

	// Token: 0x0400116D RID: 4461
	private int targetIndex;

	// Token: 0x0400116E RID: 4462
	private const float MIN_NODE_DISTANCE = 60f;

	// Token: 0x0400116F RID: 4463
	private const float REVEAL_TRACK_DISTANCE = 6f;

	// Token: 0x04001170 RID: 4464
	private const int MAX_VERTICES = 65535;

	// Token: 0x04001171 RID: 4465
	private const int VERTICES_PER_CRUMB = 12;

	// Token: 0x04001172 RID: 4466
	private const int MAX_PATH_CRUMBS = 5461;

	// Token: 0x04001173 RID: 4467
	private const float CRUMB_GROUPS_PER_METER = 1f;

	// Token: 0x04001174 RID: 4468
	private const int CRUMBS_PER_GROUP = 3;

	// Token: 0x04001175 RID: 4469
	private const int MAX_PATH_CRUMB_GROUPS = 1820;

	// Token: 0x04001176 RID: 4470
	private const float CRUMB_GROUP_RANDOM_OFFSET = 1f;

	// Token: 0x04001177 RID: 4471
	private const float CRUMB_RANDOM_OFFSET = 0.3f;

	// Token: 0x04001178 RID: 4472
	private const float CRUMB_VERTICAL_OFFSET = -0.1f;

	// Token: 0x04001179 RID: 4473
	private const float CRUMB_SIZE_MIN = 0.03f;

	// Token: 0x0400117A RID: 4474
	private const float CRUMB_SIZE_MAX = 0.1f;

	// Token: 0x0400117B RID: 4475
	private const float CRUMB_GROUP_VERTICAL_OFFSET = 5f;

	// Token: 0x0400117C RID: 4476
	private const float CRUMB_GROUP_VERTICAL_RAY_LENGTH = 25f;

	// Token: 0x0400117D RID: 4477
	private int encounterIndex;

	// Token: 0x0400117E RID: 4478
	private int generatedTracks;

	// Token: 0x02000252 RID: 594
	private class Track
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x000891B8 File Offset: 0x000873B8
		public Track(List<Vector3> path, float pathLength, MeshRenderer renderer)
		{
			this.path = path;
			this.pathLength = pathLength;
			this.renderer = renderer;
			this.material = this.renderer.material;
			this.progress = 0f;
			this.progressIndex = 0;
			this.targetProgress = 4f;
			this.trackDistance = new List<float>();
			this.trackDistance.Add(0f);
			float num = 0f;
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				num += Vector3.Distance(this.path[i], this.path[i + 1]);
				this.trackDistance.Add(num);
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00089278 File Offset: 0x00087478
		public void Update()
		{
			Vector3 vector = this.path[this.progressIndex];
			Debug.DrawRay(vector, Vector3.up * 5f, Color.red);
			if ((FpsActorController.instance.actor.Position() - vector).ToGround().magnitude < 6f)
			{
				this.Reveal();
			}
			if (this.IsCompleted() && !this.prefabSpawned)
			{
				Vector3 vector2 = this.path[this.path.Count - 1];
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(vector2 + new Vector3(0f, 5f, 0f), Vector3.down), out raycastHit, 25f, 2232321))
				{
					vector2 = raycastHit.point;
				}
				UnityEngine.Object.Instantiate<GameObject>(this.prefab, vector2, Quaternion.identity);
				this.prefabSpawned = true;
			}
			float num = this.IsCompleted() ? 10f : 4f;
			this.progress = Mathf.MoveTowards(this.progress, this.targetProgress, num * Time.deltaTime);
			if (this.trackDistance[this.progressIndex] < this.progress)
			{
				this.progressIndex = Mathf.Min(this.progressIndex + 1, this.path.Count - 1);
			}
			this.material.SetFloat("_VisDistance", this.progress + 0.5f);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000893EC File Offset: 0x000875EC
		public void Reveal()
		{
			this.targetProgress = Mathf.Clamp(this.progress + 0.5f, 0f, this.pathLength);
			if (this.targetProgress > this.pathLength - 30f)
			{
				this.targetProgress = this.pathLength + 0.5f;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0000D216 File Offset: 0x0000B416
		public bool IsCompleted()
		{
			return this.targetProgress >= this.pathLength;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0000D229 File Offset: 0x0000B429
		public void Hide()
		{
			this.renderer.enabled = false;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0000D237 File Offset: 0x0000B437
		public void Show()
		{
			this.renderer.enabled = true;
		}

		// Token: 0x0400117F RID: 4479
		private const float VISIBILITY_AHEAD_METERS = 0.5f;

		// Token: 0x04001180 RID: 4480
		private const float VISIBILITY_START = 4f;

		// Token: 0x04001181 RID: 4481
		private const float REVEAL_SPEED = 4f;

		// Token: 0x04001182 RID: 4482
		private const float REVEAL_SPEED_AUTOCOMPLETE = 10f;

		// Token: 0x04001183 RID: 4483
		private const float REVEAL_COMPLETE_DISTANCE = 30f;

		// Token: 0x04001184 RID: 4484
		public List<Vector3> path;

		// Token: 0x04001185 RID: 4485
		public List<float> trackDistance;

		// Token: 0x04001186 RID: 4486
		public Material material;

		// Token: 0x04001187 RID: 4487
		public MeshRenderer renderer;

		// Token: 0x04001188 RID: 4488
		public float progress;

		// Token: 0x04001189 RID: 4489
		public int progressIndex;

		// Token: 0x0400118A RID: 4490
		public float targetProgress;

		// Token: 0x0400118B RID: 4491
		public float pathLength;

		// Token: 0x0400118C RID: 4492
		public GameObject prefab;

		// Token: 0x0400118D RID: 4493
		private float metersToNormalized;

		// Token: 0x0400118E RID: 4494
		private bool prefabSpawned;
	}
}
