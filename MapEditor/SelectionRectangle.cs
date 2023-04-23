using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200070D RID: 1805
	public class SelectionRectangle : MonoBehaviour
	{
		// Token: 0x06002D2F RID: 11567 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		public void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0001F14D File Offset: 0x0001D34D
		public void Hide()
		{
			this.isStartSet = false;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		public bool IsVisible()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0001F162 File Offset: 0x0001D362
		public bool IsStartSet()
		{
			return this.isStartSet;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x00105650 File Offset: 0x00103850
		public bool IsSizeThresholdReached()
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			return this.isStartSet && rectTransform.rect.size.magnitude > 5f;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0001F16A File Offset: 0x0001D36A
		public void SetStartPosition(Vector2 mousePosition)
		{
			this.isStartSet = true;
			RectTransform rectTransform = (RectTransform)base.transform;
			rectTransform.position = mousePosition;
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x00105690 File Offset: 0x00103890
		public void SetEndPosition(Vector2 mousePosition)
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			Vector2 vector = rectTransform.InverseTransformPoint(mousePosition);
			rectTransform.pivot = new Vector2((float)((vector.x < 0f) ? 1 : 0), (float)((vector.y < 0f) ? 1 : 0));
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(vector.x));
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(vector.y));
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0010570C File Offset: 0x0010390C
		public SelectableObject[] GetSelection()
		{
			List<SelectableObject> list = new List<SelectableObject>();
			MapEditor instance = MapEditor.instance;
			Camera camera = instance.GetCamera();
			MapEditorObject[] array = instance.FindObjectsToSave<MapEditorObject>();
			RectTransform rectTransform = (RectTransform)base.transform;
			Rect rect = rectTransform.rect;
			rect.position += rectTransform.position;
			Vector3 position = camera.transform.position;
			UnityEngine.Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
			foreach (MapEditorObject mapEditorObject in array)
			{
				Vector3 position2 = mapEditorObject.transform.position;
				if (GeometryUtility.TestPlanesAABB(planes, new Bounds(position2, Vector3.zero)))
				{
					Vector3 point = camera.WorldToScreenPoint(position2);
					if (rect.Contains(point))
					{
						float magnitude = (position2 - position).magnitude;
						Vector3? vector = MapEditorTerrain.RayCast(new Ray(position, (position2 - position).normalized), magnitude);
						if (vector == null)
						{
							list.Add(mapEditorObject.GetSelectableObject());
						}
						else if ((vector.Value - position2).magnitude < 1f)
						{
							list.Add(mapEditorObject.GetSelectableObject());
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400299F RID: 10655
		private const float ACTIVATION_THRESHOLD = 5f;

		// Token: 0x040029A0 RID: 10656
		private bool isStartSet;
	}
}
