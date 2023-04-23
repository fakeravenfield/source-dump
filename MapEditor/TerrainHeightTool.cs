using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200067E RID: 1662
	public class TerrainHeightTool : TerrainTool
	{
		// Token: 0x06002A54 RID: 10836 RVA: 0x0001D191 File Offset: 0x0001B391
		protected override void OnInitialize()
		{
			this.gizmo = MeGizmos.CreateTerrainBrushGizmo(base.transform);
			base.OnInitialize();
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x0001D1AA File Offset: 0x0001B3AA
		protected override void OnActivate()
		{
			this.gizmo.Activate(null);
			base.OnActivate();
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x0001D1BE File Offset: 0x0001B3BE
		protected override void OnDeactivate()
		{
			if (this.gizmo)
			{
				this.gizmo.Deactivate();
			}
			this.AddUndoActionIfAny();
			base.OnDeactivate();
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000FF2CC File Offset: 0x000FD4CC
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
						HeighmapRegion region = this.GetRegion(vector.Value);
						this.CreateUndoActionIfNone(region);
						this.PerformOperation(region);
						return;
					}
					this.AddUndoActionIfAny();
				}
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
		private void CreateUndoActionIfNone(HeighmapRegion region)
		{
			if (this.undoAction == null)
			{
				this.undoAction = new TerrainHeightAction(this.GetEntireHeightmap());
			}
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0001D1FF File Offset: 0x0001B3FF
		private void DiscardUndoAction()
		{
			this.undoAction = null;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0001D208 File Offset: 0x0001B408
		private void AddUndoActionIfAny()
		{
			if (this.undoAction != null)
			{
				this.undoAction.SetAfter(this.GetEntireHeightmap());
				MapEditor.instance.AddUndoableAction(this.undoAction);
				this.undoAction = null;
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0001D23A File Offset: 0x0001B43A
		private HeighmapRegion GetEntireHeightmap()
		{
			return new HeighmapRegion(MapEditorTerrain.instance);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0001D246 File Offset: 0x0001B446
		private HeighmapRegion GetRegion(Vector3 brushPosition)
		{
			return new HeighmapRegion(MapEditorTerrain.instance, this.brushSize, brushPosition);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000FF36C File Offset: 0x000FD56C
		private void PerformOperation(HeighmapRegion region)
		{
			switch (this.operation)
			{
			case TerrainHeightTool.Operation.Raise:
			{
				double num = (double)(MeInput.instance.LowerTerrainModifier() ? -1 : 1);
				this.Raise(region, this.GetRaiseItensity() * num);
				return;
			}
			case TerrainHeightTool.Operation.Smooth:
				this.Smooth(region, this.GetSmoothIntensity());
				return;
			case TerrainHeightTool.Operation.Flatten:
				if (MeInput.instance.SampleTerrainModifier())
				{
					this.SampleFlattenHeight(region);
					this.DiscardUndoAction();
					return;
				}
				this.Flatten(region);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000FF3E4 File Offset: 0x000FD5E4
		private double GetRaiseItensity()
		{
			float time = this.brushIntensity;
			return (double)(this.raiseIntensityCurve.Evaluate(time) * this.raiseIntensityMultiplier);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0001D259 File Offset: 0x0001B459
		private double GetSmoothIntensity()
		{
			return (double)(this.smoothIntensityMultiplier * this.brushIntensity);
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000FF40C File Offset: 0x000FD60C
		private void Raise(HeighmapRegion region, double intensity)
		{
			region.ForEach(delegate(double h, int x, int y)
			{
				double num = region.DistanceFalloff(x, y);
				if (num > 0.0)
				{
					return h + intensity * num * (double)Time.deltaTime;
				}
				return h;
			});
			region.Apply();
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000FF450 File Offset: 0x000FD650
		private void Smooth(HeighmapRegion region, double intensity)
		{
			region.ForEach(delegate(double h, int x, int y)
			{
				double num = region.DistanceFalloff(x, y);
				if (num > 0.0)
				{
					double num2 = region.Multisample(x, y, region.size / 4) - h;
					return h + num2 * intensity * num * (double)Time.deltaTime;
				}
				return h;
			});
			region.Apply();
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000FF494 File Offset: 0x000FD694
		private void Flatten(HeighmapRegion region)
		{
			region.ForEach(delegate(double h, int x, int y)
			{
				if (region.DistanceFalloff(x, y) > 0.0)
				{
					return this.flattenHeight;
				}
				return h;
			});
			region.Apply();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0001D269 File Offset: 0x0001B469
		private void SampleFlattenHeight(HeighmapRegion region)
		{
			this.flattenHeight = region.GetHeight(region.xCenter, region.yCenter);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0001D284 File Offset: 0x0001B484
		public void SetOperation(TerrainHeightTool.Operation op)
		{
			this.operation = op;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x0001D28D File Offset: 0x0001B48D
		public void SetSize(float size)
		{
			this.brushSize = size;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0001D296 File Offset: 0x0001B496
		public void SetIntensity(float intensity)
		{
			this.brushIntensity = Mathf.Clamp01(intensity);
		}

		// Token: 0x0400277C RID: 10108
		public AnimationCurve raiseIntensityCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 0f),
			new Keyframe(1f, 1f, 2f, 2f)
		});

		// Token: 0x0400277D RID: 10109
		public float raiseIntensityMultiplier = 0.2f;

		// Token: 0x0400277E RID: 10110
		public float smoothIntensityMultiplier = 20f;

		// Token: 0x0400277F RID: 10111
		private TerrainBrushGizmo gizmo;

		// Token: 0x04002780 RID: 10112
		private TerrainHeightAction undoAction;

		// Token: 0x04002781 RID: 10113
		private float brushSize = 10f;

		// Token: 0x04002782 RID: 10114
		private float brushIntensity = 0.5f;

		// Token: 0x04002783 RID: 10115
		private double flattenHeight;

		// Token: 0x04002784 RID: 10116
		private TerrainHeightTool.Operation operation = TerrainHeightTool.Operation.Smooth;

		// Token: 0x0200067F RID: 1663
		public enum Operation
		{
			// Token: 0x04002786 RID: 10118
			Raise,
			// Token: 0x04002787 RID: 10119
			Smooth,
			// Token: 0x04002788 RID: 10120
			Flatten
		}
	}
}
