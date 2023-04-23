using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200062F RID: 1583
	public class MeoCapturePoint : MapEditorObject, IPropertyChangeNotify
	{
		// Token: 0x06002892 RID: 10386 RVA: 0x0001BF63 File Offset: 0x0001A163
		private void Start()
		{
			this.assistant = base.GetComponentInChildren<MeoCapturePointAssistant>();
			this.SwitchFlagColor();
			this.ChangeCircleRadius();
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000FAD78 File Offset: 0x000F8F78
		public override void Destroy()
		{
			MeoCapturePoint[] array = MapEditor.instance.FindObjectsToSave<MeoCapturePoint>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RemoveNeighbour(this);
			}
			base.Destroy();
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0001BF7D File Offset: 0x0001A17D
		public void OnPropertyChanged()
		{
			this.SwitchFlagColor();
			this.ChangeCircleRadius();
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000FADB0 File Offset: 0x000F8FB0
		private void SwitchFlagColor()
		{
			switch (this.owner)
			{
			case SpawnPoint.Team.Neutral:
				this.SetFlagColor(new Color(0.75f, 0.75f, 0.75f));
				return;
			case SpawnPoint.Team.Blue:
				this.SetFlagColor(Color.blue);
				return;
			case SpawnPoint.Team.Red:
				this.SetFlagColor(Color.red);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0001BF8B File Offset: 0x0001A18B
		private void SetFlagColor(Color color)
		{
			this.assistant.GetFlagRenderer().material.color = color;
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000FAE0C File Offset: 0x000F900C
		private void ChangeCircleRadius()
		{
			this.assistant.protectionCircle.radius = this.protectRange;
			this.assistant.captureCircle.radius = this.captureRange;
			this.assistant.ceilingCircle.radius = this.captureRange;
			this.assistant.floorCircle.radius = this.captureRange;
			Vector3 position = this.assistant.captureCircle.transform.position;
			this.assistant.ceilingCircle.transform.position = position + Vector3.up * this.captureCeiling;
			this.assistant.floorCircle.transform.position = position + Vector3.down * this.captureFloor;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0001BFA3 File Offset: 0x0001A1A3
		public override string GetCategoryName()
		{
			return "Capture Point";
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000FAEE0 File Offset: 0x000F90E0
		public override MapEditorObject Clone()
		{
			MeoCapturePoint meoCapturePoint = MeoCapturePoint.Create(base.transform.parent);
			Utils.CopyLocalTransform(base.transform, meoCapturePoint.transform);
			PropertyProvider.CopyProperties(this, meoCapturePoint, false);
			return meoCapturePoint;
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0001BFAA File Offset: 0x0001A1AA
		public static MeoCapturePoint Create(Transform parent = null)
		{
			MeoCapturePoint meoCapturePoint = MapEditorObject.Create<MeoCapturePoint>(MapEditorAssistant.instance.capturePointRenderingPrefab, null, parent, true);
			meoCapturePoint.selectableObject.DisableAction(MapEditor.Action.Rotate);
			meoCapturePoint.selectableObject.DisableAction(MapEditor.Action.Scale);
			return meoCapturePoint;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0001BFD6 File Offset: 0x0001A1D6
		public MeoSpawnPoint[] GetSpawnPoints()
		{
			return this.spawnPoints.ToArray();
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000FAF18 File Offset: 0x000F9118
		public static void DistributeSpawnPoints()
		{
			MapEditor instance = MapEditor.instance;
			MeoSpawnPoint[] array = instance.FindObjectsToSave<MeoSpawnPoint>();
			MeoCapturePoint[] array2 = instance.FindObjectsToSave<MeoCapturePoint>();
			MeoCapturePoint[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].spawnPoints.Clear();
			}
			MeoSpawnPoint[] array4 = array;
			for (int i = 0; i < array4.Length; i++)
			{
				MeoSpawnPoint sp = array4[i];
				try
				{
					(from cp in array2
					select new
					{
						cp = cp,
						dist = MeoCapturePoint.Distance(cp, sp)
					} into x
					orderby x.dist
					select x).First().cp.spawnPoints.Add(sp);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000FAFE4 File Offset: 0x000F91E4
		private static float Distance(MeoCapturePoint cp, MeoSpawnPoint sp)
		{
			return (cp.transform.position - sp.transform.position).sqrMagnitude;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0001BFE3 File Offset: 0x0001A1E3
		public MeoCapturePoint.Neighbour[] GetNeighbours()
		{
			return this.neighbours.ToArray();
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000FB014 File Offset: 0x000F9214
		public void AddNeighbour(MeoCapturePoint cp, bool byLand, bool byWater, bool oneWay)
		{
			if (this.NeighbourExists(cp))
			{
				Debug.LogError("This neighbour already exists!");
				return;
			}
			MeoCapturePoint.Neighbour item = new MeoCapturePoint.Neighbour(this, cp, byLand, byWater, oneWay);
			this.neighbours.Add(item);
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		public void ClearNeighbours()
		{
			this.neighbours.Clear();
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000FB050 File Offset: 0x000F9250
		public void RemoveNeighbour(MeoCapturePoint capturePoint)
		{
			this.neighbours = (from n in this.neighbours
			where n.capturePointA != capturePoint && n.capturePointB != capturePoint
			select n).ToList<MeoCapturePoint.Neighbour>();
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000FB08C File Offset: 0x000F928C
		private bool NeighbourExists(MeoCapturePoint cp)
		{
			return (from n in this.neighbours
			where n.capturePointB == cp
			select n).Any<MeoCapturePoint.Neighbour>();
		}

		// Token: 0x0400268B RID: 9867
		[ShowInMapEditor(2)]
		[NonSerialized]
		public string shortName = "";

		// Token: 0x0400268C RID: 9868
		[ShowInMapEditor(3)]
		[NonSerialized]
		public SpawnPoint.Team owner = SpawnPoint.Team.Neutral;

		// Token: 0x0400268D RID: 9869
		[ShowInMapEditor(4)]
		[Range(0f, 150f)]
		[NonSerialized]
		public float protectRange = 15f;

		// Token: 0x0400268E RID: 9870
		[ShowInMapEditor(5)]
		[Range(0f, 64f)]
		[NonSerialized]
		public float captureRange = 10f;

		// Token: 0x0400268F RID: 9871
		[ShowInMapEditor(6)]
		[Range(0f, 64f)]
		[NonSerialized]
		public float captureFloor = 5f;

		// Token: 0x04002690 RID: 9872
		[ShowInMapEditor(7)]
		[Range(0f, 64f)]
		[NonSerialized]
		public float captureCeiling = 20f;

		// Token: 0x04002691 RID: 9873
		[ShowInMapEditor(8)]
		[Range(0f, 64f)]
		[NonSerialized]
		public float captureRate = 1f;

		// Token: 0x04002692 RID: 9874
		private MapEditor editor;

		// Token: 0x04002693 RID: 9875
		private MeoCapturePointAssistant assistant;

		// Token: 0x04002694 RID: 9876
		private List<MeoSpawnPoint> spawnPoints = new List<MeoSpawnPoint>();

		// Token: 0x04002695 RID: 9877
		private List<MeoCapturePoint.Neighbour> neighbours = new List<MeoCapturePoint.Neighbour>();

		// Token: 0x02000630 RID: 1584
		public struct Neighbour
		{
			// Token: 0x060028A4 RID: 10404 RVA: 0x0001BFFD File Offset: 0x0001A1FD
			public Neighbour(MeoCapturePoint a, MeoCapturePoint b, bool byLand, bool byWater, bool oneWay)
			{
				this.capturePointA = a;
				this.capturePointB = b;
				this.landConnection = byLand;
				this.waterConnection = byWater;
				this.oneWay = oneWay;
			}

			// Token: 0x04002696 RID: 9878
			public readonly MeoCapturePoint capturePointA;

			// Token: 0x04002697 RID: 9879
			public readonly MeoCapturePoint capturePointB;

			// Token: 0x04002698 RID: 9880
			public readonly bool landConnection;

			// Token: 0x04002699 RID: 9881
			public readonly bool waterConnection;

			// Token: 0x0400269A RID: 9882
			public readonly bool oneWay;
		}
	}
}
