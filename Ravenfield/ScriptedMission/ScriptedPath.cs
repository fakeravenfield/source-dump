using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x02000376 RID: 886
	public class ScriptedPath : MonoBehaviour
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x00011994 File Offset: 0x0000FB94
		private void Start()
		{
			this.UpdateTransformMatrix();
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0001199C File Offset: 0x0000FB9C
		public void SortModifiers()
		{
			this.modifiers.Sort(new Comparison<ScriptedPathEdgeModifier>(ScriptedPathEdgeModifier.Compare));
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000119B5 File Offset: 0x0000FBB5
		public int GetNodeCount()
		{
			return this.pathNodes.Count;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000119C2 File Offset: 0x0000FBC2
		public void UpdateTransformMatrix()
		{
			this.localToWorld = base.transform.localToWorldMatrix;
			this.worldToLocal = base.transform.worldToLocalMatrix;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000119E6 File Offset: 0x0000FBE6
		public Vector3 GetNodeWorldPosition(int index)
		{
			return this.localToWorld.MultiplyPoint(this.pathNodes[index].localPosition);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000A01E0 File Offset: 0x0009E3E0
		public void SetNodeWorldPosition(int index, Vector3 worldPosition)
		{
			ScriptedPathNode value = this.pathNodes[index];
			value.localPosition = this.worldToLocal.MultiplyPoint(worldPosition);
			this.pathNodes[index] = value;
		}

		// Token: 0x040018BE RID: 6334
		public bool limitSpeed = true;

		// Token: 0x040018BF RID: 6335
		public List<ScriptedPathNode> pathNodes = new List<ScriptedPathNode>();

		// Token: 0x040018C0 RID: 6336
		public List<ScriptedPathEdgeModifier> modifiers = new List<ScriptedPathEdgeModifier>();

		// Token: 0x040018C1 RID: 6337
		[NonSerialized]
		public Matrix4x4 localToWorld = Matrix4x4.identity;

		// Token: 0x040018C2 RID: 6338
		[NonSerialized]
		public Matrix4x4 worldToLocal = Matrix4x4.identity;

		// Token: 0x02000377 RID: 887
		public static class Debug
		{
			// Token: 0x17000122 RID: 290
			// (get) Token: 0x0600165E RID: 5726 RVA: 0x00011A3F File Offset: 0x0000FC3F
			public static GUIStyle TITLE_GUI_STYLE
			{
				get
				{
					if (ScriptedPath.Debug._titleGuiStyle == null)
					{
						ScriptedPath.Debug._titleGuiStyle = new GUIStyle
						{
							fontSize = 14,
							normal = new GUIStyleState
							{
								textColor = Color.white
							}
						};
					}
					return ScriptedPath.Debug._titleGuiStyle;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x0600165F RID: 5727 RVA: 0x00011A75 File Offset: 0x0000FC75
			public static GUIStyle BODY_GUI_STYLE
			{
				get
				{
					if (ScriptedPath.Debug._bodyGuiStyle == null)
					{
						ScriptedPath.Debug._bodyGuiStyle = new GUIStyle
						{
							normal = new GUIStyleState
							{
								textColor = Color.white
							}
						};
					}
					return ScriptedPath.Debug._bodyGuiStyle;
				}
			}

			// Token: 0x06001660 RID: 5728 RVA: 0x00011AA3 File Offset: 0x0000FCA3
			public static bool Toggle(bool value, string label)
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Label(label, ScriptedPath.Debug.BODY_GUI_STYLE, Array.Empty<GUILayoutOption>());
				GUILayout.FlexibleSpace();
				value = GUILayout.Toggle(value, "", Array.Empty<GUILayoutOption>());
				GUILayout.EndHorizontal();
				return value;
			}

			// Token: 0x040018C3 RID: 6339
			private static GUIStyle _titleGuiStyle;

			// Token: 0x040018C4 RID: 6340
			private static GUIStyle _bodyGuiStyle;
		}
	}
}
