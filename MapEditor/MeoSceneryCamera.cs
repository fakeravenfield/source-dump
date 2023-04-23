using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000638 RID: 1592
	public class MeoSceneryCamera : MapEditorObject
	{
		// Token: 0x060028B7 RID: 10423 RVA: 0x0001C0B5 File Offset: 0x0001A2B5
		public override string GetCategoryName()
		{
			return "Scenery Camera";
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		public override MapEditorObject Clone()
		{
			Debug.LogError("Clone() not supported on MeoSceneryCamera");
			return null;
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0001C0C9 File Offset: 0x0001A2C9
		public override void Delete()
		{
			Debug.LogError("Delete() not supported on MeoSceneryCamera");
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0001C0D5 File Offset: 0x0001A2D5
		public override void Undelete()
		{
			Debug.LogError("Undelete() not supported on MeoSceneryCamera");
			throw new NotSupportedException();
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0001C0E6 File Offset: 0x0001A2E6
		public override void Destroy()
		{
			Debug.LogError("Destroy() not supported on MeoSceneryCamera");
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000FB240 File Offset: 0x000F9440
		public static MeoSceneryCamera Create(Transform parent = null)
		{
			MeoSceneryCamera meoSceneryCamera = MapEditorObject.Create<MeoSceneryCamera>(MapEditorAssistant.instance.sceneryCameraRenderingPrefab, null, parent, true);
			meoSceneryCamera.transform.position = new Vector3(-250f, 150f, 150f);
			meoSceneryCamera.transform.rotation = Quaternion.LookRotation(-meoSceneryCamera.transform.position);
			meoSceneryCamera.selectableObject.DisableAction(MapEditor.Action.Scale);
			meoSceneryCamera.selectableObject.DisableAction(MapEditor.Action.Delete);
			meoSceneryCamera.selectableObject.DisableAction(MapEditor.Action.Clone);
			meoSceneryCamera.selectableObject.DisableAction(MapEditor.Action.Place);
			meoSceneryCamera.renderTexture = new RenderTexture(512, 512, 16);
			meoSceneryCamera.camera = meoSceneryCamera.GetOrCreateComponent<Camera>();
			meoSceneryCamera.camera.farClipPlane = 2000f;
			meoSceneryCamera.camera.clearFlags = CameraClearFlags.Depth;
			meoSceneryCamera.camera.cullingMask = (int.MaxValue & ~Layers.GetGizmoPartLayerMask() & ~LayerMask.GetMask(new string[]
			{
				"Background"
			}));
			meoSceneryCamera.camera.depth = 50f;
			meoSceneryCamera.camera.allowHDR = false;
			meoSceneryCamera.camera.enabled = false;
			meoSceneryCamera.camera.targetTexture = meoSceneryCamera.renderTexture;
			return meoSceneryCamera;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0001C0F2 File Offset: 0x0001A2F2
		public void Render()
		{
			this.camera.Render();
			this.camera.enabled = false;
		}

		// Token: 0x040026AA RID: 9898
		private const int RESOLUTION = 512;

		// Token: 0x040026AB RID: 9899
		[NonSerialized]
		public Camera camera;

		// Token: 0x040026AC RID: 9900
		[NonSerialized]
		public RenderTexture renderTexture;
	}
}
