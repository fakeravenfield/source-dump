using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000619 RID: 1561
	public class MeInput : MonoBehaviour
	{
		// Token: 0x0600280D RID: 10253 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		private void Awake()
		{
			MeInput.instance = this;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000F9B2C File Offset: 0x000F7D2C
		private void Update()
		{
			this.mouseOverUI = EventSystem.current.IsPointerOverGameObject();
			this.uiFocused = this.IsSelectableUIElementSelected();
			this.rotateCamera = (!this.mouseOverUI && Input.GetMouseButton(1));
			if (this.rotateCamera)
			{
				float axisRaw = Input.GetAxisRaw("Mouse X");
				float axisRaw2 = Input.GetAxisRaw("Mouse Y");
				this.rotateCameraAxis = new Vector2(axisRaw, axisRaw2);
			}
			this.moveCamera = !this.uiFocused;
			this.cameraSpeed = MeInput.CameraSpeed.Normal;
			if (!this.uiFocused && Input.GetKey(KeyCode.LeftShift))
			{
				this.cameraSpeed = MeInput.CameraSpeed.Fast;
			}
			else if (!this.uiFocused && Input.GetKey(KeyCode.LeftControl))
			{
				this.cameraSpeed = MeInput.CameraSpeed.Slow;
			}
			this.moveCameraDirection = this.DetermineMoveCameraDirection();
			this.clickInScene = (!this.mouseOverUI && Input.GetMouseButtonDown(0));
			if ((this.mouseOverUI && this.dragObjectInScene) || !this.mouseOverUI)
			{
				this.dragObjectInScene = Input.GetMouseButton(0);
			}
			if (this.dragObjectInScene)
			{
				float axisRaw3 = Input.GetAxisRaw("Mouse X");
				float axisRaw4 = Input.GetAxisRaw("Mouse Y");
				this.dragObjectInSceneAxis = new Vector2(axisRaw3, axisRaw4);
			}
			this.snapToGridModifier = Input.GetKey(KeyCode.LeftShift);
			this.snapToGroundModifier = Input.GetKey(KeyCode.LeftControl);
			this.addToSelectionModifier = Input.GetKey(KeyCode.LeftControl);
			this.multiPlaceModifier = Input.GetKey(KeyCode.LeftShift);
			this.deleteSelectedObjects = (!this.uiFocused && Input.GetKeyDown(KeyCode.Delete));
			this.lowerTerrainModifier = Input.GetKey(KeyCode.LeftControl);
			this.sampleTerrainModifier = Input.GetKey(KeyCode.LeftControl);
			this.deactivateTool = (!this.uiFocused && Input.GetKey(KeyCode.Escape));
			this.undoAction = (!this.uiFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z));
			this.redoAction = (!this.uiFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y));
			this.copySelection = (!this.uiFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C));
			this.cutSelection = (!this.uiFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.X));
			this.paste = (!this.uiFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V));
			this.duplicateSelection = (!this.uiFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B));
			this.closeWindow = Input.GetKey(KeyCode.Escape);
			this.dragCursorOnUI = (this.mouseOverUI && Input.GetMouseButton(0));
			this.clickOnUI = (this.mouseOverUI && Input.GetMouseButtonDown(0));
			this.quickSave = (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S));
			if (!this.uiFocused && SteelInput.GetButtonDown(SteelInput.KeyBinds.Goggles))
			{
				GameManager.ToggleNightVision();
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000F9E2C File Offset: 0x000F802C
		private bool IsSelectableUIElementSelected()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			return currentSelectedGameObject && currentSelectedGameObject.GetComponent<Selectable>();
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000F9E60 File Offset: 0x000F8060
		private Vector3 DetermineMoveCameraDirection()
		{
			float x = 0f;
			float z = 0f;
			float y = 0f;
			if (Input.GetKey(KeyCode.A))
			{
				x = -1f;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				x = 1f;
			}
			if (Input.GetKey(KeyCode.W))
			{
				z = 1f;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				z = -1f;
			}
			if (Input.GetKey(KeyCode.E))
			{
				y = 1f;
			}
			else if (Input.GetKey(KeyCode.Q))
			{
				y = -1f;
			}
			return new Vector3(x, y, z);
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0001BAF0 File Offset: 0x00019CF0
		public bool IsMouseOverUI()
		{
			return this.mouseOverUI;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0001BAF8 File Offset: 0x00019CF8
		public bool IsFocusOnUI()
		{
			return this.uiFocused;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0001BB00 File Offset: 0x00019D00
		public bool IsFocusOnUI(Selectable obj)
		{
			return EventSystem.current.currentSelectedGameObject == obj || EventSystem.current.IsPointerOverGameObject();
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0001BB20 File Offset: 0x00019D20
		public bool RotateCamera()
		{
			return this.rotateCamera;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0001BB28 File Offset: 0x00019D28
		public Vector2 RotateCameraAxis()
		{
			return this.rotateCameraAxis;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0001BB30 File Offset: 0x00019D30
		public float RotateCameraSensitivity()
		{
			return 3f;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0001BB37 File Offset: 0x00019D37
		public float ZoomCameraSensitivity()
		{
			return 0.01f;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0001BB3E File Offset: 0x00019D3E
		public bool MoveCamera()
		{
			return this.moveCamera;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0001BB46 File Offset: 0x00019D46
		public Vector3 MoveCameraDirection()
		{
			return this.moveCameraDirection.normalized;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000F9EE8 File Offset: 0x000F80E8
		public float MoveCameraSpeed()
		{
			MeInput.CameraSpeed cameraSpeed = this.cameraSpeed;
			if (cameraSpeed == MeInput.CameraSpeed.Slow)
			{
				return 10f;
			}
			if (cameraSpeed != MeInput.CameraSpeed.Fast)
			{
				return 40f;
			}
			return 200f;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0001BB53 File Offset: 0x00019D53
		public bool DragObjectInScene()
		{
			return this.dragObjectInScene;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0001BB5B File Offset: 0x00019D5B
		public Vector2 DragObjectInSceneAxis()
		{
			return this.dragObjectInSceneAxis;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0001BB63 File Offset: 0x00019D63
		public bool ClickInScene()
		{
			return this.clickInScene;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0001BB6B File Offset: 0x00019D6B
		public Ray CursorToSceneRay()
		{
			return MapEditor.instance.GetCamera().ScreenPointToRay(Input.mousePosition);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0001BB81 File Offset: 0x00019D81
		public UnityEngine.Plane CameraFacingPlane(Vector3 inPoint)
		{
			return new UnityEngine.Plane(MapEditor.instance.GetCamera().transform.position - inPoint, inPoint);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000F9F18 File Offset: 0x000F8118
		public Vector3 ProjectCursorToPlane(Vector3 normal, Vector3 inPlane)
		{
			UnityEngine.Plane plane = new UnityEngine.Plane(normal, inPlane);
			Ray ray = this.CursorToSceneRay();
			float num;
			if (!plane.Raycast(ray, out num))
			{
				num = -num;
			}
			return ray.GetPoint(num);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0001BBA3 File Offset: 0x00019DA3
		public bool SnapToGridModifier()
		{
			return this.snapToGridModifier;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0001BBAB File Offset: 0x00019DAB
		public bool SnapToGroundModifier()
		{
			return this.snapToGroundModifier;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0001BBB3 File Offset: 0x00019DB3
		public bool AddToSelectionModifier()
		{
			return this.addToSelectionModifier;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0001BBBB File Offset: 0x00019DBB
		public bool MultiPlaceModifier()
		{
			return this.multiPlaceModifier;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0001BBC3 File Offset: 0x00019DC3
		public bool DeleteSelectedObjects()
		{
			return this.deleteSelectedObjects;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0001BBCB File Offset: 0x00019DCB
		public bool DeactivateTool()
		{
			return this.deactivateTool;
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0001BBD3 File Offset: 0x00019DD3
		public bool UndoAction()
		{
			return this.undoAction;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0001BBDB File Offset: 0x00019DDB
		public bool RedoAction()
		{
			return this.redoAction;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0001BBE3 File Offset: 0x00019DE3
		public bool CopySelection()
		{
			return this.copySelection;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0001BBEB File Offset: 0x00019DEB
		public bool CutSelection()
		{
			return this.cutSelection;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0001BBF3 File Offset: 0x00019DF3
		public bool Paste()
		{
			return this.paste;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0001BBFB File Offset: 0x00019DFB
		public bool DuplicateSelection()
		{
			return this.duplicateSelection;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0001BC03 File Offset: 0x00019E03
		public bool CloseWindow()
		{
			return this.closeWindow;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0001BC0B File Offset: 0x00019E0B
		public bool DragCursorOnUI()
		{
			return this.dragCursorOnUI;
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0001BC13 File Offset: 0x00019E13
		public bool ClickOnUI()
		{
			return this.clickOnUI;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0001BC1B File Offset: 0x00019E1B
		public float TranslateObjectSensitivity()
		{
			return 40f;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0001BC22 File Offset: 0x00019E22
		public float ScaleObjectSensitivity()
		{
			return 10f;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0001BC29 File Offset: 0x00019E29
		public float RotateObjectSensitivity()
		{
			return 5f;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0001BC30 File Offset: 0x00019E30
		public bool LowerTerrainModifier()
		{
			return this.lowerTerrainModifier;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0001BC38 File Offset: 0x00019E38
		public bool SampleTerrainModifier()
		{
			return this.sampleTerrainModifier;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0001BC40 File Offset: 0x00019E40
		public bool QuickSave()
		{
			return this.quickSave;
		}

		// Token: 0x040025FC RID: 9724
		private const int ROTATE_CAMERA_MOUSE_BUTTON = 1;

		// Token: 0x040025FD RID: 9725
		private const string ROTATE_CAMERA_AXIS_X = "Mouse X";

		// Token: 0x040025FE RID: 9726
		private const string ROTATE_CAMERA_AXIS_Y = "Mouse Y";

		// Token: 0x040025FF RID: 9727
		private const KeyCode MOVE_CAMERA_FORWARD_KEY = KeyCode.W;

		// Token: 0x04002600 RID: 9728
		private const KeyCode MOVE_CAMERA_BACKWARD_KEY = KeyCode.S;

		// Token: 0x04002601 RID: 9729
		private const KeyCode MOVE_CAMERA_LEFT_KEY = KeyCode.A;

		// Token: 0x04002602 RID: 9730
		private const KeyCode MOVE_CAMERA_RIGHT_KEY = KeyCode.D;

		// Token: 0x04002603 RID: 9731
		private const KeyCode MOVE_CAMERA_UP_KEY = KeyCode.E;

		// Token: 0x04002604 RID: 9732
		private const KeyCode MOVE_CAMERA_DOWN_KEY = KeyCode.Q;

		// Token: 0x04002605 RID: 9733
		private const KeyCode MOVE_CAMERA_FAST_KEY = KeyCode.LeftShift;

		// Token: 0x04002606 RID: 9734
		private const KeyCode MOVE_CAMERA_SLOW_KEY = KeyCode.LeftControl;

		// Token: 0x04002607 RID: 9735
		private const int CLICK_IN_SCENE_MOUSE_BUTTON = 0;

		// Token: 0x04002608 RID: 9736
		private const string DRAG_OBJECT_IN_SCENE_AXIS_X = "Mouse X";

		// Token: 0x04002609 RID: 9737
		private const string DRAG_OBJECT_IN_SCENE_AXIS_Y = "Mouse Y";

		// Token: 0x0400260A RID: 9738
		private const KeyCode SNAP_TO_GRID_MODIFIER = KeyCode.LeftShift;

		// Token: 0x0400260B RID: 9739
		private const KeyCode SNAP_TO_GROUND_MODIFIER = KeyCode.LeftControl;

		// Token: 0x0400260C RID: 9740
		private const KeyCode MULTI_PLACE_MODIFIER = KeyCode.LeftShift;

		// Token: 0x0400260D RID: 9741
		private const KeyCode ADD_TO_SELECTION_MODIFIER = KeyCode.LeftControl;

		// Token: 0x0400260E RID: 9742
		private const KeyCode DELETE_SELECTED_OBJECTS_KEY = KeyCode.Delete;

		// Token: 0x0400260F RID: 9743
		private const KeyCode LOWER_TERRAIN_MODIFIER = KeyCode.LeftControl;

		// Token: 0x04002610 RID: 9744
		private const KeyCode SAMPLE_TERRAIN_MODIFIER = KeyCode.LeftControl;

		// Token: 0x04002611 RID: 9745
		private const KeyCode DEACTIVATE_TOOL_KEY = KeyCode.Escape;

		// Token: 0x04002612 RID: 9746
		private const KeyCode UNDO_KEY = KeyCode.Z;

		// Token: 0x04002613 RID: 9747
		private const KeyCode UNDO_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x04002614 RID: 9748
		private const KeyCode REDO_KEY = KeyCode.Y;

		// Token: 0x04002615 RID: 9749
		private const KeyCode REDO_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x04002616 RID: 9750
		private const KeyCode DUPLICATE_KEY = KeyCode.B;

		// Token: 0x04002617 RID: 9751
		private const KeyCode DUPLICATE_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x04002618 RID: 9752
		private const KeyCode COPY_KEY = KeyCode.C;

		// Token: 0x04002619 RID: 9753
		private const KeyCode COPY_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x0400261A RID: 9754
		private const KeyCode CUT_KEY = KeyCode.X;

		// Token: 0x0400261B RID: 9755
		private const KeyCode CUT_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x0400261C RID: 9756
		private const KeyCode PASTE_KEY = KeyCode.V;

		// Token: 0x0400261D RID: 9757
		private const KeyCode PASTE_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x0400261E RID: 9758
		private const KeyCode QUICK_SAVE_KEY = KeyCode.S;

		// Token: 0x0400261F RID: 9759
		private const KeyCode QUICK_SAVE_MODIFIER_KEY = KeyCode.LeftControl;

		// Token: 0x04002620 RID: 9760
		private const KeyCode CLOSE_WINDOW_KEY = KeyCode.Escape;

		// Token: 0x04002621 RID: 9761
		private const int CLICK_CURSOR_ON_UI_BUTTON = 0;

		// Token: 0x04002622 RID: 9762
		private const float ROTATE_CAMERA_SENSITIVITY = 3f;

		// Token: 0x04002623 RID: 9763
		private const float ZOOM_CAMERA_SENSITIVITY = 0.01f;

		// Token: 0x04002624 RID: 9764
		private const float MOVE_CAMERA_SPEED_SLOW = 10f;

		// Token: 0x04002625 RID: 9765
		private const float MOVE_CAMERA_SPEED_NORMAL = 40f;

		// Token: 0x04002626 RID: 9766
		private const float MOVE_CAMERA_SPEED_FAST = 200f;

		// Token: 0x04002627 RID: 9767
		private const float TRANSLATE_OBJECT_SENSITIVITY = 40f;

		// Token: 0x04002628 RID: 9768
		private const float SCALE_OBJECT_SENSITIVITY = 10f;

		// Token: 0x04002629 RID: 9769
		private const float ROTATE_OBJECT_SENSITIVITY = 5f;

		// Token: 0x0400262A RID: 9770
		private bool uiFocused;

		// Token: 0x0400262B RID: 9771
		private bool mouseOverUI;

		// Token: 0x0400262C RID: 9772
		private bool rotateCamera;

		// Token: 0x0400262D RID: 9773
		private Vector2 rotateCameraAxis;

		// Token: 0x0400262E RID: 9774
		private bool moveCamera;

		// Token: 0x0400262F RID: 9775
		private MeInput.CameraSpeed cameraSpeed;

		// Token: 0x04002630 RID: 9776
		private Vector3 moveCameraDirection;

		// Token: 0x04002631 RID: 9777
		private bool clickInScene;

		// Token: 0x04002632 RID: 9778
		private bool dragObjectInScene;

		// Token: 0x04002633 RID: 9779
		private Vector2 dragObjectInSceneAxis;

		// Token: 0x04002634 RID: 9780
		private bool snapToGridModifier;

		// Token: 0x04002635 RID: 9781
		private bool snapToGroundModifier;

		// Token: 0x04002636 RID: 9782
		private bool addToSelectionModifier;

		// Token: 0x04002637 RID: 9783
		private bool multiPlaceModifier;

		// Token: 0x04002638 RID: 9784
		private bool deleteSelectedObjects;

		// Token: 0x04002639 RID: 9785
		private bool lowerTerrainModifier;

		// Token: 0x0400263A RID: 9786
		private bool sampleTerrainModifier;

		// Token: 0x0400263B RID: 9787
		private bool deactivateTool;

		// Token: 0x0400263C RID: 9788
		private bool undoAction;

		// Token: 0x0400263D RID: 9789
		private bool redoAction;

		// Token: 0x0400263E RID: 9790
		private bool copySelection;

		// Token: 0x0400263F RID: 9791
		private bool cutSelection;

		// Token: 0x04002640 RID: 9792
		private bool paste;

		// Token: 0x04002641 RID: 9793
		private bool duplicateSelection;

		// Token: 0x04002642 RID: 9794
		private bool closeWindow;

		// Token: 0x04002643 RID: 9795
		private bool dragCursorOnUI;

		// Token: 0x04002644 RID: 9796
		private bool clickOnUI;

		// Token: 0x04002645 RID: 9797
		private bool quickSave;

		// Token: 0x04002646 RID: 9798
		public static MeInput instance;

		// Token: 0x0200061A RID: 1562
		private enum CameraSpeed
		{
			// Token: 0x04002648 RID: 9800
			Normal,
			// Token: 0x04002649 RID: 9801
			Slow,
			// Token: 0x0400264A RID: 9802
			Fast
		}
	}
}
