using System;
using System.Collections.Generic;
using MapEditor;
using Pathfinding;
using UnityEngine;

// Token: 0x02000242 RID: 578
public class CoverPlacer : MonoBehaviour
{
	// Token: 0x06001009 RID: 4105 RVA: 0x00086A60 File Offset: 0x00084C60
	public void Generate()
	{
		try
		{
			CoverPlacer.activeCoverPointMask = (MapEditor.IsAvailable() ? 270667777 : 2232321);
		}
		catch
		{
			CoverPlacer.activeCoverPointMask = 2232321;
		}
		WaterLevel waterLevel = UnityEngine.Object.FindObjectOfType<WaterLevel>();
		if (waterLevel != null)
		{
			this.waterHeight = waterLevel.transform.position.y;
			WaterLevel.instance = waterLevel;
		}
		Debug.Log("Water height: " + this.waterHeight.ToString());
		this.nFlat = 0;
		this.nNotFlat = 0;
		this.newCoverPoints = new List<CoverPoint>();
		CoverPoint[] componentsInChildren = base.GetComponentsInChildren<CoverPoint>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(componentsInChildren[i].gameObject);
		}
		this.pathfinding = base.GetComponent<AstarPath>();
		foreach (NavGraph navGraph in this.pathfinding.graphs)
		{
			if (navGraph != null && !(navGraph.GetType() != typeof(RecastGraph)))
			{
				this.graph = (RecastGraph)navGraph;
				this.nFlat = 0;
				this.nNotFlat = 0;
				this.nUnderWater = 0;
				if (PathfindingManager.instance != null && PathfindingManager.instance.graphToBox != null && PathfindingManager.instance.graphToBox.ContainsKey(this.graph))
				{
					this.coverPointSampleSpacing = Mathf.Max(0.05f, PathfindingManager.instance.graphToBox[this.graph].coverPointSpacing);
				}
				else
				{
					this.coverPointSampleSpacing = 0.05f;
				}
				if (navGraph.name[0] == 'I')
				{
					this.applyInclinationPenalty = false;
					this.graph.GetNodes(new Func<GraphNode, bool>(this.HandleInfantryNode));
				}
				else if (navGraph.name[0] == 'C')
				{
					this.applyInclinationPenalty = true;
					this.graph.GetNodes(new Func<GraphNode, bool>(this.HandleNodePenalty));
				}
				else if (navGraph.name[0] == 'B')
				{
					this.applyInclinationPenalty = false;
					this.graph.GetNodes(new Func<GraphNode, bool>(this.HandleBoatNode));
				}
				Debug.Log("--- " + this.graph.name + " ---" + "\nFlat: " + this.nFlat.ToString() + "\nNot Flat: " + this.nNotFlat.ToString() + "\nUnderwater: " + this.nUnderWater.ToString());
			}
		}
		this.PruneCloseCoverPoints();
		this.skipSamples = Mathf.Max(this.skipSamples, 5);
		this.SampleCoverPointCoverage();
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x0000CDDE File Offset: 0x0000AFDE
	public void ScanAndGenerate()
	{
		this.pathfinding = base.GetComponent<AstarPath>();
		Debug.Log("Scanning pathfinding.");
		this.pathfinding.Scan(null);
		Debug.Log("Scan complete, generating cover & penalties.");
		this.Generate();
		Debug.Log("Generate complete.");
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00086D10 File Offset: 0x00084F10
	private bool HandleBoatNode(GraphNode gNode)
	{
		if (!gNode.Walkable)
		{
			return true;
		}
		MeshNode meshNode = (MeshNode)gNode;
		meshNode.GetVertexCount();
		bool flag = false;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray((Vector3)meshNode.position, Vector3.down), out raycastHit))
		{
			flag = (raycastHit.distance <= 2f);
		}
		gNode.Tag = (flag ? 2U : 0U);
		return true;
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0000CE1C File Offset: 0x0000B01C
	private bool HandleInfantryNode(GraphNode gNode)
	{
		if (!gNode.Walkable)
		{
			return true;
		}
		this.HandleNodeCover(gNode);
		this.HandleNodePenalty(gNode);
		return true;
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00086D78 File Offset: 0x00084F78
	private bool HandleNodeCover(GraphNode gNode)
	{
		MeshNode meshNode = (MeshNode)gNode;
		int vertexCount = meshNode.GetVertexCount();
		Vector3 normalized = Vector3.Cross((Vector3)(meshNode.GetVertex(1) - meshNode.GetVertex(0)), (Vector3)(meshNode.GetVertex(2) - meshNode.GetVertex(0))).normalized;
		for (int i = 0; i < vertexCount; i++)
		{
			this.FindCoverPoints((Vector3)meshNode.GetVertex(i), (Vector3)meshNode.GetVertex((i + 1) % vertexCount), (Vector3)meshNode.position);
		}
		return true;
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x00086E0C File Offset: 0x0008500C
	private bool HandleNodePenalty(GraphNode gNode)
	{
		if (!gNode.Walkable)
		{
			return true;
		}
		MeshNode meshNode = (MeshNode)gNode;
		int vertexCount = meshNode.GetVertexCount();
		Vector3 normalized = Vector3.Cross((Vector3)(meshNode.GetVertex(1) - meshNode.GetVertex(0)), (Vector3)(meshNode.GetVertex(2) - meshNode.GetVertex(0))).normalized;
		int num = 0;
		for (int i = 0; i < vertexCount; i++)
		{
			float num2;
			if (WaterLevel.IsInWater((Vector3)meshNode.GetVertex(i), out num2) && num2 > 1f)
			{
				num++;
			}
		}
		bool flag = num >= 2;
		float num3 = Mathf.Abs(normalized.y);
		bool flag2 = num3 > 0.88f;
		if (flag)
		{
			meshNode.Tag = 3U;
		}
		uint num4 = 0U;
		if (this.applyInclinationPenalty)
		{
			num4 = (uint)Mathf.RoundToInt((1f - num3) * this.vehicleInclinationPenalty);
		}
		meshNode.Penalty = (uint)(num4 + (flag ? 10000f : 0f));
		if (flag2)
		{
			this.nFlat++;
		}
		else
		{
			this.nNotFlat++;
		}
		if (flag)
		{
			this.nUnderWater++;
		}
		return true;
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x00086F44 File Offset: 0x00085144
	private void FindCoverPoints(Vector3 v1, Vector3 v2, Vector3 center)
	{
		int num = Mathf.FloorToInt(Vector3.Distance(v1, v2) / this.coverPointSampleSpacing);
		Vector3 vector = (v2 - v1) / (float)num;
		Vector3 vector2 = Vector3.Cross(vector, Vector3.up);
		float maxDistance = this.graph.characterRadius * 2f;
		if (Vector3.Dot(vector2, v1 - center) < -1f)
		{
			vector2 = -vector2;
		}
		vector2 = vector2.normalized;
		for (int i = 0; i < num; i++)
		{
			Vector3 vector3 = v1 + vector * (float)i;
			RaycastHit raycastHit;
			if (!WaterLevel.IsInWater(vector3) && Physics.Raycast(new Ray(vector3 + Vector3.up * 2f, Vector3.down), out raycastHit, 10f, CoverPlacer.activeCoverPointMask))
			{
				vector3 = raycastHit.point;
				CoverPoint.Type type;
				Vector3 vector4;
				if (this.SuitableCoverPoint(vector3, vector2, maxDistance, out type, out vector4))
				{
					this.GenerateCoverPoint(vector3, vector2, type);
					i += Mathf.FloorToInt(1f / this.coverPointSampleSpacing);
				}
			}
		}
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00087054 File Offset: 0x00085254
	private bool SuitableCoverPoint(Vector3 point, Vector3 outwards, float maxDistance, out CoverPoint.Type type, out Vector3 normal)
	{
		type = CoverPoint.Type.Crouch;
		normal = Vector3.zero;
		if (point.y < this.waterHeight)
		{
			return false;
		}
		RaycastHit raycastHit;
		if (!Physics.Raycast(new Ray(point + Vector3.up * 0.7f, outwards), out raycastHit, maxDistance, CoverPlacer.activeCoverPointMask))
		{
			return false;
		}
		Vector3 a = Vector3.Cross(Vector3.up, outwards);
		if (!Physics.Raycast(new Ray(point + Vector3.up * 0.7f + a * 0.4f, outwards), maxDistance, CoverPlacer.activeCoverPointMask) && !Physics.Raycast(new Ray(point + Vector3.up * 0.7f - a * 0.4f, outwards), maxDistance, CoverPlacer.activeCoverPointMask))
		{
			return false;
		}
		normal = raycastHit.normal;
		if (!Physics.SphereCast(new Ray(CoverPlacer.CoverPointRayOrigin(point, outwards, CoverPoint.Type.Crouch), outwards), 0.1f, 3f * maxDistance, CoverPlacer.activeCoverPointMask))
		{
			return true;
		}
		if (!Physics.SphereCast(new Ray(CoverPlacer.CoverPointRayOrigin(point, outwards, CoverPoint.Type.LeanRight), outwards), 0.1f, 3f * maxDistance, CoverPlacer.activeCoverPointMask))
		{
			type = CoverPoint.Type.LeanRight;
			return true;
		}
		if (!Physics.SphereCast(new Ray(CoverPlacer.CoverPointRayOrigin(point, outwards, CoverPoint.Type.LeanLeft), outwards), 0.1f, 3f * maxDistance, CoverPlacer.activeCoverPointMask))
		{
			type = CoverPoint.Type.LeanLeft;
			return true;
		}
		return false;
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0000CE39 File Offset: 0x0000B039
	public static Vector3 CoverPointRayOrigin(CoverPoint coverPoint)
	{
		return CoverPlacer.CoverPointRayOrigin(coverPoint.transform.position, coverPoint.transform.forward, coverPoint.type);
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x000871BC File Offset: 0x000853BC
	private static Vector3 CoverPointRayOrigin(Vector3 point, Vector3 outwards, CoverPoint.Type type)
	{
		if (type == CoverPoint.Type.Crouch)
		{
			return point + Vector3.up * 1.5f;
		}
		Vector3 a = Vector3.Cross(Vector3.up, outwards);
		if (type == CoverPoint.Type.LeanRight)
		{
			return point + Vector3.up * 1.5f + a * 0.3f;
		}
		return point + Vector3.up * 1.5f - a * 0.3f;
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x00087240 File Offset: 0x00085440
	private void GenerateCoverPoint(Vector3 point, Vector3 direction, CoverPoint.Type type)
	{
		Vector3 position = point;
		RaycastHit raycastHit;
		if (Physics.SphereCast(new Ray(point + Vector3.up * 0.5f * 1f, direction), 0.5f, out raycastHit, (float)CoverPlacer.activeCoverPointMask))
		{
			position = raycastHit.point - direction * 0.5f;
		}
		position.y = point.y;
		GameObject gameObject = new GameObject("Cover Point");
		gameObject.transform.position = position;
		gameObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		CoverPoint coverPoint = gameObject.AddComponent<CoverPoint>();
		coverPoint.type = type;
		this.newCoverPoints.Add(coverPoint);
		gameObject.transform.parent = base.transform;
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00087304 File Offset: 0x00085504
	private void PruneCloseCoverPoints()
	{
		foreach (CoverPoint coverPoint in this.newCoverPoints.ToArray())
		{
			foreach (CoverPoint coverPoint2 in this.newCoverPoints.ToArray())
			{
				if (coverPoint != coverPoint2 && coverPoint.CoversDirection(coverPoint2.transform.forward) && Vector3.Distance(coverPoint.transform.position, coverPoint2.transform.position) < 0.8f)
				{
					this.newCoverPoints.Remove(coverPoint);
					UnityEngine.Object.DestroyImmediate(coverPoint.gameObject);
					break;
				}
			}
		}
		foreach (TurretSpawner turretSpawner in UnityEngine.Object.FindObjectsOfType<TurretSpawner>())
		{
			float num = 2f;
			foreach (CoverPoint coverPoint3 in this.newCoverPoints.ToArray())
			{
				if (Vector3.Distance(coverPoint3.transform.position, turretSpawner.transform.position) < num)
				{
					this.newCoverPoints.Remove(coverPoint3);
					UnityEngine.Object.DestroyImmediate(coverPoint3.gameObject);
				}
			}
		}
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00087438 File Offset: 0x00085638
	private void SampleCoverPointCoverage()
	{
		UnityEngine.Object.FindObjectsOfType<PathfindingBox>();
		this.samplePoints = new List<Vector3>();
		foreach (NavGraph navGraph in this.pathfinding.graphs)
		{
			if (navGraph != null)
			{
				this.skip = this.skipSamples;
				navGraph.GetNodes(new Func<GraphNode, bool>(this.AddSamplePoint));
			}
		}
		int num = 0;
		byte b = 0;
		float num2 = 0f;
		foreach (CoverPoint coverPoint in this.newCoverPoints)
		{
			Vector3 vector = CoverPlacer.CoverPointRayOrigin(coverPoint);
			foreach (Vector3 vector2 in this.samplePoints)
			{
				if (Vector3.Dot((vector2 - vector).normalized, coverPoint.transform.forward) > 0.707f && !Physics.Linecast(vector, vector2, 1))
				{
					CoverPoint coverPoint2 = coverPoint;
					coverPoint2.coverage += 1;
					if (coverPoint.coverage == 255)
					{
						num++;
						break;
					}
				}
			}
			if (!Physics.Raycast(vector, Vector3.up, 5f, 1))
			{
				coverPoint.coverage /= 2;
			}
			num2 += (float)coverPoint.coverage;
			if (coverPoint.coverage > b)
			{
				b = coverPoint.coverage;
			}
		}
		num2 /= (float)this.newCoverPoints.Count;
		Debug.Log("Max Coverage: " + b.ToString());
		Debug.Log("Mean Coverage: " + num2.ToString());
		Debug.Log(string.Concat(new string[]
		{
			"Overflowed Coverage: ",
			num.ToString(),
			"(",
			(100f * (float)num / (float)this.newCoverPoints.Count).ToString(),
			"%)"
		}));
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00087660 File Offset: 0x00085860
	private bool AddSamplePoint(GraphNode arg)
	{
		if (!arg.Walkable)
		{
			return true;
		}
		if (this.skip > 0)
		{
			this.skip--;
			return true;
		}
		this.skip = this.skipSamples;
		Vector3 origin = (Vector3)arg.position + Vector3.up * 20f;
		this.samplePoints.Add((Vector3)arg.position);
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(origin, Vector3.down), out raycastHit, 50f, 1))
		{
			Vector3 point = raycastHit.point;
			point.y += 1.3f;
			this.samplePoints.Add(point);
		}
		return true;
	}

	// Token: 0x040010D5 RID: 4309
	private const float UNDERWATER_PENALTY = 10000f;

	// Token: 0x040010D6 RID: 4310
	private const float WATER_MAX_WALKING_DEPTH = 1f;

	// Token: 0x040010D7 RID: 4311
	private const float WATER_SHALLOW_DEPTH = 2f;

	// Token: 0x040010D8 RID: 4312
	private const float COVER_POINT_SAMPLE_SPACING = 0.05f;

	// Token: 0x040010D9 RID: 4313
	public const float COVER_POINT_SIDE_CHECK = 0.4f;

	// Token: 0x040010DA RID: 4314
	public const float COVER_POINT_LOW_HEIGHT = 0.7f;

	// Token: 0x040010DB RID: 4315
	public const float COVER_POINT_HIGH_HEIGHT = 1.5f;

	// Token: 0x040010DC RID: 4316
	private const float LEAN_DISTANCE = 0.3f;

	// Token: 0x040010DD RID: 4317
	private const float COVER_POINT_MIN_SPACING = 1f;

	// Token: 0x040010DE RID: 4318
	private const float SPHERECAST_RADIUS = 0.1f;

	// Token: 0x040010DF RID: 4319
	private const float SPHERECAST_PLAYER_RADIUS = 0.5f;

	// Token: 0x040010E0 RID: 4320
	private const float MIN_CAR_NORMAL_Y = 0.88f;

	// Token: 0x040010E1 RID: 4321
	private const int COVER_POINT_MASK = 2232321;

	// Token: 0x040010E2 RID: 4322
	private const int COVER_POINT_MASK_INGAME_EDITOR = 270667777;

	// Token: 0x040010E3 RID: 4323
	private static int activeCoverPointMask = 2232321;

	// Token: 0x040010E4 RID: 4324
	private AstarPath pathfinding;

	// Token: 0x040010E5 RID: 4325
	private RecastGraph graph;

	// Token: 0x040010E6 RID: 4326
	private List<CoverPoint> newCoverPoints;

	// Token: 0x040010E7 RID: 4327
	private int nFlat;

	// Token: 0x040010E8 RID: 4328
	private int nNotFlat;

	// Token: 0x040010E9 RID: 4329
	private int nUnderWater;

	// Token: 0x040010EA RID: 4330
	public float vehicleInclinationPenalty = 1000f;

	// Token: 0x040010EB RID: 4331
	public int skipSamples;

	// Token: 0x040010EC RID: 4332
	public bool drawWidgets;

	// Token: 0x040010ED RID: 4333
	private float waterHeight;

	// Token: 0x040010EE RID: 4334
	private float coverPointSampleSpacing = 0.05f;

	// Token: 0x040010EF RID: 4335
	private bool applyInclinationPenalty;

	// Token: 0x040010F0 RID: 4336
	private const float COVERAGE_SAMPLE_HEIGHT_OVER_TERRAIN = 1.3f;

	// Token: 0x040010F1 RID: 4337
	private const int COVERAGE_SAMPLE_RAY_MASK = 1;

	// Token: 0x040010F2 RID: 4338
	private List<Vector3> samplePoints;

	// Token: 0x040010F3 RID: 4339
	private int skip;
}
