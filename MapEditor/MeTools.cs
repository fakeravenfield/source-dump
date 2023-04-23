using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x02000673 RID: 1651
	public class MeTools : MonoBehaviour
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x0001CD7D File Offset: 0x0001AF7D
		public static MeTools instance
		{
			get
			{
				if (!MeTools._instance)
				{
					MeTools._instance = UnityEngine.Object.FindObjectOfType<MeTools>();
				}
				return MeTools._instance;
			}
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0001CD9A File Offset: 0x0001AF9A
		private void Awake()
		{
			MeTools._instance = this;
			this.current = null;
			this.orientation = MeTools.Orientation.Global;
			this.onToolChanged = new UnityEvent();
			this.onOrientationChanged = new UnityEvent();
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000FE7F4 File Offset: 0x000FC9F4
		private void Start()
		{
			this.noopTool = this.Create<NoopTool>();
			this.placeTool = this.Create<PlaceTool>();
			this.rotateTool = this.Create<RotateTool>();
			this.scaleTool = this.Create<ScaleTool>();
			this.translateTool = this.Create<TranslateTool>();
			this.terrainAlphaTool = this.Create<TerrainAlphaTool>();
			this.terrainHeightTool = this.Create<TerrainHeightTool>();
			this.SwitchToNoopTool();
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0001CDC6 File Offset: 0x0001AFC6
		private void OnDestroy()
		{
			MeTools._instance = null;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0001CDCE File Offset: 0x0001AFCE
		private T Create<T>() where T : Component
		{
			return new GameObject(typeof(T).Name)
			{
				transform = 
				{
					parent = base.transform
				}
			}.GetOrCreateComponent<T>();
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000FE85C File Offset: 0x000FCA5C
		public bool IsCurrent<T>()
		{
			Type typeFromHandle = typeof(T);
			return this.current && typeFromHandle.IsAssignableFrom(this.current.GetType());
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0001CDFA File Offset: 0x0001AFFA
		public AbstractTool GetCurrent()
		{
			return this.current;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000FE894 File Offset: 0x000FCA94
		private void SetCurrent(AbstractTool tool)
		{
			if (this.current != tool)
			{
				if (this.current)
				{
					this.current.Deactivate();
				}
				this.current = tool;
				if (tool)
				{
					tool.Activate();
				}
				this.onToolChanged.Invoke();
			}
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x0001CE02 File Offset: 0x0001B002
		public void SetOrientation(MeTools.Orientation orientation)
		{
			if (this.orientation != orientation)
			{
				this.orientation = orientation;
				this.onOrientationChanged.Invoke();
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x0001CE1F File Offset: 0x0001B01F
		public MeTools.Orientation GetOrientation()
		{
			return this.orientation;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x0001CE27 File Offset: 0x0001B027
		public void SwitchToNoopTool()
		{
			this.SetCurrent(this.noopTool);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x0001CE35 File Offset: 0x0001B035
		public void SwitchToTranslateTool()
		{
			this.SetCurrent(this.translateTool);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x0001CE43 File Offset: 0x0001B043
		public void SwitchToRotateTool()
		{
			this.SetCurrent(this.rotateTool);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x0001CE51 File Offset: 0x0001B051
		public void SwitchToScaleTool()
		{
			this.SetCurrent(this.scaleTool);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x0001CE5F File Offset: 0x0001B05F
		public void SwitchToPlaceTool(params MapEditorObject[] editorObjects)
		{
			if (editorObjects != null && editorObjects.Any<MapEditorObject>())
			{
				MapEditor.instance.SetSelection(Selection.empty);
				this.placeTool.SetObjectsToPlace(editorObjects);
				this.SetCurrent(this.placeTool);
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0001CE93 File Offset: 0x0001B093
		public void SwitchToTerrainHeightTool()
		{
			MapEditor.instance.SetSelection(Selection.empty);
			this.SetCurrent(this.terrainHeightTool);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		public void SwitchToTerrainAlphaTool()
		{
			MapEditor.instance.SetSelection(Selection.empty);
			this.SetCurrent(this.terrainAlphaTool);
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0001CECD File Offset: 0x0001B0CD
		public void SwitchToPhotoTool()
		{
			MapEditor.instance.SetSelection(Selection.empty);
			this.SetCurrent(this.photoTool);
		}

		// Token: 0x0400274B RID: 10059
		[NonSerialized]
		public NoopTool noopTool;

		// Token: 0x0400274C RID: 10060
		[NonSerialized]
		public PlaceTool placeTool;

		// Token: 0x0400274D RID: 10061
		[NonSerialized]
		public RotateTool rotateTool;

		// Token: 0x0400274E RID: 10062
		[NonSerialized]
		public ScaleTool scaleTool;

		// Token: 0x0400274F RID: 10063
		[NonSerialized]
		public TranslateTool translateTool;

		// Token: 0x04002750 RID: 10064
		[NonSerialized]
		public TerrainAlphaTool terrainAlphaTool;

		// Token: 0x04002751 RID: 10065
		[NonSerialized]
		public TerrainHeightTool terrainHeightTool;

		// Token: 0x04002752 RID: 10066
		public PhotoTool photoTool;

		// Token: 0x04002753 RID: 10067
		[NonSerialized]
		public UnityEvent onToolChanged;

		// Token: 0x04002754 RID: 10068
		[NonSerialized]
		public UnityEvent onOrientationChanged;

		// Token: 0x04002755 RID: 10069
		private AbstractTool current;

		// Token: 0x04002756 RID: 10070
		private MeTools.Orientation orientation;

		// Token: 0x04002757 RID: 10071
		private static MeTools _instance;

		// Token: 0x02000674 RID: 1652
		public enum Orientation
		{
			// Token: 0x04002759 RID: 10073
			Global,
			// Token: 0x0400275A RID: 10074
			WithSelection
		}
	}
}
