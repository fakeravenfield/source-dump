using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x0200062A RID: 1578
	public class MePathfindingLinkRenderer : MonoBehaviour, ISelectedNotify
	{
		// Token: 0x0600287A RID: 10362 RVA: 0x0001BE99 File Offset: 0x0001A099
		private void Initialize()
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.OnInitialize();
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000FA9A0 File Offset: 0x000F8BA0
		private void OnInitialize()
		{
			this.link = base.GetComponentInParent<PathfindingLink>();
			this.meObject = base.GetComponentInParent<MapEditorObject>();
			this.gizmo = this.meObject.GetComponentInChildren<TranslateGizmo>();
			if (!this.gizmo)
			{
				this.gizmo = MeGizmos.CreateTranslateGizmo(this.meObject.transform);
				this.gizmo.Deactivate();
			}
			MeTools.instance.onToolChanged.AddListener(new UnityAction(this.OnToolChange));
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		private void Start()
		{
			this.Initialize();
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		private void OnToolChange()
		{
			this.ToggleGizmo();
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000FAA20 File Offset: 0x000F8C20
		public void OnSelected()
		{
			this.isSelected = (MapEditor.instance.GetSelection().GetLength() == 1);
			this.ToggleGizmo();
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		public void OnDeselected()
		{
			this.isSelected = false;
			this.ToggleGizmo();
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000FAA50 File Offset: 0x000F8C50
		private void ToggleGizmo()
		{
			if (this.isSelected && MeTools.instance.IsCurrent<NoopTool>())
			{
				this.Initialize();
				this.gizmo.transform.position = this.link.MeEndPosition;
				this.gizmo.Activate(null);
				return;
			}
			if (this.gizmo)
			{
				this.gizmo.Deactivate();
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x0001BECF File Offset: 0x0001A0CF
		private void OnDestroy()
		{
			if (this.isInitialized && MeTools.instance)
			{
				MeTools.instance.onToolChanged.RemoveListener(new UnityAction(this.OnToolChange));
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000FAAB8 File Offset: 0x000F8CB8
		private void LateUpdate()
		{
			if (this.isSelected && MeTools.instance.IsCurrent<NoopTool>())
			{
				this.link.MeEndPosition = this.gizmo.transform.position;
			}
			this.endCircle.transform.position = this.link.MeEndPosition;
			int num = this.CircleHashCode();
			if (this.previousHash != num)
			{
				this.previousHash = num;
				Vector3[] array = MePathfindingLinkRenderer.PointsBetween(this.startCircle.transform.position, this.endCircle.transform.position);
				this.line.positionCount = array.Length;
				this.line.SetPositions(array);
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000FAB68 File Offset: 0x000F8D68
		public int CircleHashCode()
		{
			return (1584462887 * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.startCircle.transform.position)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.endCircle.transform.position);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000FABBC File Offset: 0x000F8DBC
		public static Vector3[] PointsBetween(Vector3 start, Vector3 end)
		{
			Vector3[] array = new Vector3[30];
			Vector3 a = (end - start) / (float)(array.Length - 1);
			float magnitude = (end - start).magnitude;
			float num = Mathf.Clamp(1f + magnitude / 6f, 1f, 6f);
			for (int i = 0; i < array.Length / 2; i++)
			{
				float y = Mathf.Sin(3.1415927f * (float)i / (float)array.Length) * num;
				Vector3 b = new Vector3(0f, y, 0f);
				array[i] = start + a * (float)i + b;
				array[array.Length - 1 - i] = end - a * (float)i + b;
			}
			return array;
		}

		// Token: 0x0400267B RID: 9851
		public LineRenderer line;

		// Token: 0x0400267C RID: 9852
		public CircleRenderer startCircle;

		// Token: 0x0400267D RID: 9853
		public CircleRenderer endCircle;

		// Token: 0x0400267E RID: 9854
		private int previousHash;

		// Token: 0x0400267F RID: 9855
		private bool isSelected;

		// Token: 0x04002680 RID: 9856
		private MapEditorObject meObject;

		// Token: 0x04002681 RID: 9857
		private PathfindingLink link;

		// Token: 0x04002682 RID: 9858
		private TranslateGizmo gizmo;

		// Token: 0x04002683 RID: 9859
		private bool isInitialized;
	}
}
