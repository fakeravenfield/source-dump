using System;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x02000374 RID: 884
	[Serializable]
	public class ScriptedPathEdgeModifier : ScriptableObject
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x000A01BC File Offset: 0x0009E3BC
		public static int Compare(ScriptedPathEdgeModifier x, ScriptedPathEdgeModifier y)
		{
			return x.GetSequenceValue().CompareTo(y.GetSequenceValue());
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00011971 File Offset: 0x0000FB71
		public float GetSequenceValue()
		{
			return (float)this.edgeIndex + this.edgeT;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual ScriptedPathEdgeModifier.GuiEventResult DrawEditorSceneUI(Vector3 handlePosition, ScriptedPath path)
		{
			return ScriptedPathEdgeModifier.GuiEventResult.None;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual bool DrawEditorGUI()
		{
			return false;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnPassed(ScriptedPathSeeker seeker)
		{
		}

		// Token: 0x040018B8 RID: 6328
		public int edgeIndex;

		// Token: 0x040018B9 RID: 6329
		public float edgeT = 0.5f;

		// Token: 0x02000375 RID: 885
		public enum GuiEventResult
		{
			// Token: 0x040018BB RID: 6331
			None,
			// Token: 0x040018BC RID: 6332
			SetDirty,
			// Token: 0x040018BD RID: 6333
			Remove
		}
	}
}
