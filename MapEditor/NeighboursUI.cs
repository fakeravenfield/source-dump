using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006EC RID: 1772
	public class NeighboursUI : WindowBase
	{
		// Token: 0x06002C89 RID: 11401 RVA: 0x00103A30 File Offset: 0x00101C30
		private new void Initialize()
		{
			if (this.isInitialized)
			{
				return;
			}
			this.isInitialized = true;
			this.editor = MapEditor.instance;
			this.editorUI = this.editor.GetEditorUI();
			this.input = this.editor.GetInput();
			this.minimap = UnityEngine.Object.FindObjectOfType<NeighbourCamera>();
			this.minimapImage.texture = this.minimap.GetTexture();
			this.minimapImage.color = Color.white;
			this.minimapCamera = this.minimap.GetComponent<Camera>();
			this.neighbours = new List<NeighboursUI.CapturePointNeighbour>();
			this.byLandToggle.onValueChanged.AddListener(new UnityAction<bool>(this.NeighbourSettingsChanged));
			this.byWaterToggle.onValueChanged.AddListener(new UnityAction<bool>(this.NeighbourSettingsChanged));
			this.oneWayToggle.onValueChanged.AddListener(new UnityAction<bool>(this.NeighbourSettingsChanged));
			this.reverseLinkButton.onClick.AddListener(new UnityAction(this.ReverseButtonClicked));
			this.deleteLinkButton.onClick.AddListener(new UnityAction(this.DeleteButtonClicked));
			this.saveButton.onClick.AddListener(new UnityAction(this.SaveButtonClicked));
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		private void Reset()
		{
			this.dragTarget = null;
			this.selectedNeighbour = null;
			this.neighbours.Clear();
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0001EA27 File Offset: 0x0001CC27
		private void Update()
		{
			if (this.dragTarget != null)
			{
				this.dragTarget.SetEndPoint(this.GetMinimapCursorPosition());
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0001EA42 File Offset: 0x0001CC42
		protected override void OnShow()
		{
			base.OnShow();
			this.Initialize();
			this.Reset();
			this.GenerateMinimap();
			this.DestroyDragTarget();
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0001EA62 File Offset: 0x0001CC62
		protected override void OnHide()
		{
			base.OnHide();
			this.StoreNeighboursInScene();
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00103B70 File Offset: 0x00101D70
		private void GenerateMinimap()
		{
			MeoCapturePoint[] array = this.editor.FindObjectsToSave<MeoCapturePoint>();
			this.minimap.AdjustZoomAndPosition(this.editor.GetEditorTerrain(), array);
			this.minimap.Render();
			Utils.DestroyChildren(this.buttonContainer.gameObject);
			Utils.DestroyChildren(this.toggleContainer.gameObject);
			MeoCapturePoint[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				MeoCapturePoint cp = array2[i];
				Button button = UnityEngine.Object.Instantiate<Button>(this.capturePointButtonPrefab);
				button.transform.SetParent(this.buttonContainer, false);
				button.transform.localPosition = this.TransformWorldToMinimap(cp.transform.position);
				button.onClick.AddListener(delegate()
				{
					this.BeginDragCapturePoint(cp, button);
				});
				foreach (MeoCapturePoint.Neighbour neighbour in cp.GetNeighbours())
				{
					if (neighbour.capturePointB && !neighbour.capturePointB.IsDeleted())
					{
						NeighboursUI.CapturePointLink capturePointLink = this.CreateLink(cp, button);
						capturePointLink.SetEndPoint(this.TransformWorldToMinimap(neighbour.capturePointB.transform.position));
						capturePointLink.RenderAsPreview(false);
						NeighboursUI.CapturePointNeighbour capturePointNeighbour = this.SaveLink(capturePointLink, neighbour.capturePointB);
						capturePointNeighbour.byLand = neighbour.landConnection;
						capturePointNeighbour.byWater = neighbour.waterConnection;
						capturePointNeighbour.oneWay = neighbour.oneWay;
						this.UpdateArrowSprite(capturePointNeighbour);
					}
				}
			}
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x00103D34 File Offset: 0x00101F34
		private void StoreNeighboursInScene()
		{
			MeoCapturePoint[] array = this.editor.FindObjectsToSave<MeoCapturePoint>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ClearNeighbours();
			}
			foreach (NeighboursUI.CapturePointNeighbour capturePointNeighbour in this.neighbours)
			{
				capturePointNeighbour.from.AddNeighbour(capturePointNeighbour.to, capturePointNeighbour.byLand, capturePointNeighbour.byWater, capturePointNeighbour.oneWay);
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0001EA70 File Offset: 0x0001CC70
		private Vector2 GetMinimapCursorPosition()
		{
			return this.minimapRect.InverseTransformPoint(Input.mousePosition);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x00103DC8 File Offset: 0x00101FC8
		private Vector2 TransformWorldToMinimap(Vector3 worldPos)
		{
			Vector2 size = UtilsUI.GetSize(this.minimapRect);
			return Vector2.Scale(this.minimapCamera.WorldToViewportPoint(worldPos) - Vector2.one / 2f, size);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0001EA87 File Offset: 0x0001CC87
		private Toggle CreateArrow()
		{
			Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.arrowPrefab);
			toggle.transform.SetParent(this.toggleContainer, false);
			toggle.group = this.neighbourGroup;
			return toggle;
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x00103E0C File Offset: 0x0010200C
		private NeighboursUI.CapturePointLink CreateLink(MeoCapturePoint capturePoint, Button button)
		{
			Toggle toggle = this.CreateArrow();
			NeighboursUI.CapturePointLink link = new NeighboursUI.CapturePointLink(capturePoint, button, toggle);
			toggle.onValueChanged.AddListener(delegate(bool isOn)
			{
				this.LinkSelected(isOn, link);
			});
			return link;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x00103E58 File Offset: 0x00102058
		private void BeginDragCapturePoint(MeoCapturePoint capturePoint, Button button)
		{
			NeighboursUI.CapturePointLink capturePointLink = this.CreateLink(capturePoint, button);
			capturePointLink.RenderAsPreview(true);
			if (this.dragTarget != null)
			{
				if (this.dragTarget.capturePoint == capturePoint)
				{
					this.DestroyDragTarget();
				}
				else
				{
					this.dragTarget.SetEndPoint(capturePointLink.GetStartPoint());
					if (this.SaveLink(this.dragTarget, capturePoint) != null)
					{
						this.dragTarget.RenderAsPreview(false);
					}
					else
					{
						this.DestroyDragTarget();
					}
				}
			}
			this.dragTarget = capturePointLink;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0001EAB2 File Offset: 0x0001CCB2
		private void DestroyDragTarget()
		{
			if (this.dragTarget != null)
			{
				this.dragTarget.Destroy();
				this.dragTarget = null;
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x0001EACE File Offset: 0x0001CCCE
		public void MinimapClicked()
		{
			this.DestroyDragTarget();
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x00103ED4 File Offset: 0x001020D4
		private void LinkSelected(bool selected, NeighboursUI.CapturePointLink link)
		{
			if (selected)
			{
				NeighboursUI.CapturePointNeighbour capturePointNeighbour = this.FindNeighbour(link);
				this.SetSelectedNeighbour(capturePointNeighbour);
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00103EF4 File Offset: 0x001020F4
		private void SetSelectedNeighbour(NeighboursUI.CapturePointNeighbour neighbour)
		{
			if (this.selectedNeighbour == neighbour)
			{
				return;
			}
			this.selectedNeighbour = null;
			this.byLandToggle.interactable = (neighbour != null);
			this.byWaterToggle.interactable = (neighbour != null);
			this.oneWayToggle.interactable = (neighbour != null);
			this.reverseLinkButton.interactable = (neighbour != null);
			this.deleteLinkButton.interactable = (neighbour != null);
			this.selectedNeighbour = neighbour;
			if (neighbour != null)
			{
				this.byLandToggle.isOn = neighbour.byLand;
				this.byWaterToggle.isOn = neighbour.byWater;
				this.oneWayToggle.isOn = neighbour.oneWay;
				neighbour.link.arrow.isOn = true;
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x00103FAC File Offset: 0x001021AC
		private void NeighbourSettingsChanged(bool isOn)
		{
			if (this.selectedNeighbour != null)
			{
				this.selectedNeighbour.byLand = this.byLandToggle.isOn;
				this.selectedNeighbour.byWater = this.byWaterToggle.isOn;
				this.selectedNeighbour.oneWay = this.oneWayToggle.isOn;
				this.UpdateArrowSprite(this.selectedNeighbour);
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x00104010 File Offset: 0x00102210
		private void UpdateArrowSprite(NeighboursUI.CapturePointNeighbour neighbour)
		{
			foreach (Image image in neighbour.link.arrow.GetComponentsInChildren<Image>())
			{
				if (neighbour.oneWay)
				{
					image.sprite = this.unidirectionalArrowSprite;
				}
				else
				{
					image.sprite = this.arrowSprite;
				}
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x00104064 File Offset: 0x00102264
		private NeighboursUI.CapturePointNeighbour SaveLink(NeighboursUI.CapturePointLink link, MeoCapturePoint to)
		{
			NeighboursUI.CapturePointNeighbour capturePointNeighbour = null;
			if (!this.LinkExists(link.capturePoint, to))
			{
				capturePointNeighbour = new NeighboursUI.CapturePointNeighbour(link, to);
				this.neighbours.Add(capturePointNeighbour);
				if (this.neighbours.Count == 1)
				{
					this.SetSelectedNeighbour(this.neighbours.First<NeighboursUI.CapturePointNeighbour>());
				}
			}
			return capturePointNeighbour;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x001040B8 File Offset: 0x001022B8
		private NeighboursUI.CapturePointNeighbour FindNeighbour(NeighboursUI.CapturePointLink link)
		{
			return (from x in this.neighbours
			where x.link == link
			select x).FirstOrDefault<NeighboursUI.CapturePointNeighbour>();
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x001040F0 File Offset: 0x001022F0
		private bool LinkExists(MeoCapturePoint from, MeoCapturePoint to)
		{
			return (from x in this.neighbours
			where (x.@from == @from && x.to == to) || (x.to == @from && x.@from == to)
			select x).Any<NeighboursUI.CapturePointNeighbour>();
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x0001EAD6 File Offset: 0x0001CCD6
		public void ReverseButtonClicked()
		{
			if (this.selectedNeighbour != null)
			{
				this.selectedNeighbour.Reverse();
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x00104130 File Offset: 0x00102330
		public void DeleteButtonClicked()
		{
			NeighboursUI.CapturePointNeighbour capturePointNeighbour = this.selectedNeighbour;
			if (capturePointNeighbour != null)
			{
				this.neighbours.Remove(capturePointNeighbour);
				capturePointNeighbour.link.Destroy();
			}
			if (capturePointNeighbour == this.selectedNeighbour)
			{
				this.SetSelectedNeighbour(this.neighbours.FirstOrDefault<NeighboursUI.CapturePointNeighbour>());
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0001EAEB File Offset: 0x0001CCEB
		public void SaveButtonClicked()
		{
			this.StoreNeighboursInScene();
			base.Hide();
		}

		// Token: 0x0400290B RID: 10507
		public RawImage minimapImage;

		// Token: 0x0400290C RID: 10508
		public RectTransform minimapRect;

		// Token: 0x0400290D RID: 10509
		public RectTransform buttonContainer;

		// Token: 0x0400290E RID: 10510
		public RectTransform toggleContainer;

		// Token: 0x0400290F RID: 10511
		public ToggleGroup neighbourGroup;

		// Token: 0x04002910 RID: 10512
		public Toggle byLandToggle;

		// Token: 0x04002911 RID: 10513
		public Toggle byWaterToggle;

		// Token: 0x04002912 RID: 10514
		public Toggle oneWayToggle;

		// Token: 0x04002913 RID: 10515
		public Button reverseLinkButton;

		// Token: 0x04002914 RID: 10516
		public Button deleteLinkButton;

		// Token: 0x04002915 RID: 10517
		public Button capturePointButtonPrefab;

		// Token: 0x04002916 RID: 10518
		public Button saveButton;

		// Token: 0x04002917 RID: 10519
		public Toggle arrowPrefab;

		// Token: 0x04002918 RID: 10520
		public Sprite arrowSprite;

		// Token: 0x04002919 RID: 10521
		public Sprite unidirectionalArrowSprite;

		// Token: 0x0400291A RID: 10522
		private MapEditor editor;

		// Token: 0x0400291B RID: 10523
		private MapEditorUI editorUI;

		// Token: 0x0400291C RID: 10524
		private MeInput input;

		// Token: 0x0400291D RID: 10525
		private NeighbourCamera minimap;

		// Token: 0x0400291E RID: 10526
		private Camera minimapCamera;

		// Token: 0x0400291F RID: 10527
		private NeighboursUI.CapturePointLink dragTarget;

		// Token: 0x04002920 RID: 10528
		private NeighboursUI.CapturePointNeighbour selectedNeighbour;

		// Token: 0x04002921 RID: 10529
		private List<NeighboursUI.CapturePointNeighbour> neighbours;

		// Token: 0x04002922 RID: 10530
		private bool isInitialized;

		// Token: 0x020006ED RID: 1773
		private class CapturePointNeighbour
		{
			// Token: 0x06002CA2 RID: 11426 RVA: 0x0001EAF9 File Offset: 0x0001CCF9
			public CapturePointNeighbour(NeighboursUI.CapturePointLink link, MeoCapturePoint to)
			{
				this.link = link;
				this.from = link.capturePoint;
				this.to = to;
				this.byLand = true;
				this.byWater = true;
				this.oneWay = false;
			}

			// Token: 0x06002CA3 RID: 11427 RVA: 0x0010417C File Offset: 0x0010237C
			public void Reverse()
			{
				MeoCapturePoint meoCapturePoint = this.from;
				MeoCapturePoint meoCapturePoint2 = this.to;
				Vector2 startPoint = this.link.GetStartPoint();
				Vector2 endPoint = this.link.GetEndPoint();
				this.from = meoCapturePoint2;
				this.to = meoCapturePoint;
				this.link.SetStartPoint(endPoint);
				this.link.SetEndPoint(startPoint);
				this.link.capturePoint = this.from;
			}

			// Token: 0x06002CA4 RID: 11428 RVA: 0x0001EB30 File Offset: 0x0001CD30
			public bool IsSelected()
			{
				return this.link.arrow.isOn;
			}

			// Token: 0x06002CA5 RID: 11429 RVA: 0x0001EB42 File Offset: 0x0001CD42
			public void Select()
			{
				this.link.arrow.isOn = true;
			}

			// Token: 0x04002923 RID: 10531
			public NeighboursUI.CapturePointLink link;

			// Token: 0x04002924 RID: 10532
			public MeoCapturePoint from;

			// Token: 0x04002925 RID: 10533
			public MeoCapturePoint to;

			// Token: 0x04002926 RID: 10534
			public bool byLand;

			// Token: 0x04002927 RID: 10535
			public bool byWater;

			// Token: 0x04002928 RID: 10536
			public bool oneWay;
		}

		// Token: 0x020006EE RID: 1774
		private class CapturePointLink
		{
			// Token: 0x06002CA6 RID: 11430 RVA: 0x0001EB55 File Offset: 0x0001CD55
			public CapturePointLink(MeoCapturePoint capturePoint, Button cpButton, Toggle arrow)
			{
				this.capturePoint = capturePoint;
				this.arrow = arrow;
				this.arrowRect = this.arrow.GetComponent<RectTransform>();
				this.SetStartPoint(cpButton.transform.localPosition);
			}

			// Token: 0x06002CA7 RID: 11431 RVA: 0x0001EB92 File Offset: 0x0001CD92
			public void RenderAsPreview(bool preview)
			{
				this.arrow.interactable = !preview;
				UtilsUI.EnableRayCastTargets(this.arrow.gameObject, !preview);
			}

			// Token: 0x06002CA8 RID: 11432 RVA: 0x0001EBB7 File Offset: 0x0001CDB7
			public Vector2 GetStartPoint()
			{
				return this.startPoint;
			}

			// Token: 0x06002CA9 RID: 11433 RVA: 0x0001EBBF File Offset: 0x0001CDBF
			public void SetStartPoint(Vector2 start)
			{
				this.startPoint = start;
				this.SetEndPoint(this.GetEndPoint());
			}

			// Token: 0x06002CAA RID: 11434 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
			public Vector2 GetEndPoint()
			{
				return this.arrowRect.transform.localPosition;
			}

			// Token: 0x06002CAB RID: 11435 RVA: 0x001041E8 File Offset: 0x001023E8
			public void SetEndPoint(Vector2 end)
			{
				this.arrowRect.transform.localPosition = end;
				Vector2 vector = end - this.startPoint;
				float magnitude = vector.magnitude;
				this.arrowRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, magnitude);
				float num = Vector3.Angle(vector, Vector3.right);
				if (vector.y < 0f)
				{
					num = -num;
				}
				this.arrowRect.rotation = Quaternion.Euler(0f, 0f, num);
			}

			// Token: 0x06002CAC RID: 11436 RVA: 0x0001EBEB File Offset: 0x0001CDEB
			public void Destroy()
			{
				UnityEngine.Object.Destroy(this.arrow.gameObject);
			}

			// Token: 0x04002929 RID: 10537
			public MeoCapturePoint capturePoint;

			// Token: 0x0400292A RID: 10538
			public Toggle arrow;

			// Token: 0x0400292B RID: 10539
			private RectTransform arrowRect;

			// Token: 0x0400292C RID: 10540
			private Vector2 startPoint;
		}
	}
}
