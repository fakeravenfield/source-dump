using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200067C RID: 1660
	public class TerrainAlphaTool : TerrainTool
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x0001D024 File Offset: 0x0001B224
		protected override void OnInitialize()
		{
			this.gizmo = MeGizmos.CreateTerrainBrushGizmo(base.transform);
			this.activeLayer = MapEditorTerrain.instance.GetAlphamap().GetLayer(0);
			base.OnInitialize();
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0001D053 File Offset: 0x0001B253
		protected override void OnActivate()
		{
			this.gizmo.Activate(null);
			base.OnActivate();
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0001D067 File Offset: 0x0001B267
		protected override void OnDeactivate()
		{
			if (this.gizmo)
			{
				this.gizmo.Deactivate();
			}
			this.AddUndoActionIfAny();
			base.OnDeactivate();
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000FF1A4 File Offset: 0x000FD3A4
		protected override void Update()
		{
			base.Update();
			if (MeInput.instance.DeactivateTool())
			{
				base.Deactivate();
				return;
			}
			Ray ray = MeInput.instance.CursorToSceneRay();
			bool flag;
			Vector3? vector = MapEditorTerrain.instance.RayCast(ray, out flag);
			if (vector != null)
			{
				base.transform.position = vector.Value;
				this.gizmo.SetSize(this.brushSize);
				if (flag)
				{
					if (MeInput.instance.DragObjectInScene())
					{
						AlphamapRegion region = this.GetRegion(vector.Value);
						this.CreateUndoActionIfNone(region);
						this.Paint(region, this.GetIntensity());
						return;
					}
					this.AddUndoActionIfAny();
				}
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0001D08D File Offset: 0x0001B28D
		private void CreateUndoActionIfNone(AlphamapRegion region)
		{
			if (this.undoAction == null)
			{
				this.undoAction = new RestoreTerrainAlphaAction(MapEditorTerrain.instance.GetAlphamap());
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		private void DiscardUndoAction()
		{
			this.undoAction = null;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0001D0B5 File Offset: 0x0001B2B5
		private void AddUndoActionIfAny()
		{
			if (this.undoAction != null)
			{
				MapEditor.instance.AddUndoableAction(this.undoAction);
				this.undoAction = null;
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0001D0D6 File Offset: 0x0001B2D6
		private AlphamapRegion GetEntireRegion()
		{
			return new AlphamapRegion(MapEditorTerrain.instance, this.activeLayer);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		private AlphamapRegion GetRegion(Vector3 brushPosition)
		{
			return new AlphamapRegion(MapEditorTerrain.instance, this.activeLayer, this.brushSize, brushPosition);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x0001D101 File Offset: 0x0001B301
		private double GetIntensity()
		{
			return (double)(this.brushIntensity * this.intensityMultiplier);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000FF248 File Offset: 0x000FD448
		private void Paint(AlphamapRegion region, double intensity)
		{
			region.ForEach(delegate(double a, int x, int y)
			{
				double num = region.DistanceFalloff(x, y);
				if (num > 0.0)
				{
					a = TerrainAlphaTool.Clamp01(a + intensity * num * (double)Time.deltaTime);
				}
				return a;
			});
			region.Apply();
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0001D111 File Offset: 0x0001B311
		private static double Clamp01(double x)
		{
			if (x < 0.0)
			{
				return 0.0;
			}
			if (x <= 1.0)
			{
				return x;
			}
			return 1.0;
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0001D140 File Offset: 0x0001B340
		public void SetSize(float size)
		{
			this.brushSize = size;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x0001D149 File Offset: 0x0001B349
		public void SetIntensity(float intensity)
		{
			this.brushIntensity = Mathf.Clamp01(intensity);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0001D157 File Offset: 0x0001B357
		public void SetActiveLayer(TerrainAlphamap.Layer layer)
		{
			this.activeLayer = layer;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x0001D160 File Offset: 0x0001B360
		public TerrainAlphamap.Layer GetActiveLayer()
		{
			return this.activeLayer;
		}

		// Token: 0x04002774 RID: 10100
		public float intensityMultiplier = 20f;

		// Token: 0x04002775 RID: 10101
		private TerrainBrushGizmo gizmo;

		// Token: 0x04002776 RID: 10102
		private RestoreTerrainAlphaAction undoAction;

		// Token: 0x04002777 RID: 10103
		private TerrainAlphamap.Layer activeLayer;

		// Token: 0x04002778 RID: 10104
		private float brushSize = 10f;

		// Token: 0x04002779 RID: 10105
		private float brushIntensity = 0.5f;
	}
}
