using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class BotPathVisualizer : MonoBehaviour
{
	// Token: 0x06001185 RID: 4485 RVA: 0x0000DC8B File Offset: 0x0000BE8B
	public static BotPathVisualizer InstantiateForBot(AiActorController ai)
	{
		BotPathVisualizer botPathVisualizer = ai.gameObject.AddComponent<BotPathVisualizer>();
		botPathVisualizer.Initialize(ai);
		return botPathVisualizer;
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x0000DC9F File Offset: 0x0000BE9F
	private void Initialize(AiActorController ai)
	{
		this.ai = ai;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
	private void Update()
	{
		if (this.ai.HasPath())
		{
			if (this.PathRequiresUpdate())
			{
				this.UpdatePathGizmo();
				return;
			}
		}
		else if (this.currentGizmo != null)
		{
			this.currentGizmo.Destroy();
			this.currentGizmo = null;
		}
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0008C354 File Offset: 0x0008A554
	private void UpdatePathGizmo()
	{
		IngameDebugGizmos.Gizmo gizmo = this.currentGizmo;
		if (gizmo != null)
		{
			gizmo.Destroy();
		}
		this.visualizedPath = this.ai.vectorPath;
		this.currentGizmo = IngameDebugGizmos.DrawPath(this.visualizedPath.ToArray(), Color.magenta, 1000000f);
		this.pathStart = this.visualizedPath[0];
		this.pathEnd = this.visualizedPath[this.visualizedPath.Count - 1];
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x0008C3D4 File Offset: 0x0008A5D4
	private bool PathRequiresUpdate()
	{
		return this.ai.vectorPath != this.visualizedPath || this.pathStart != this.ai.vectorPath[0] || this.pathEnd != this.ai.vectorPath[this.ai.vectorPath.Count - 1];
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x0008C444 File Offset: 0x0008A644
	private static bool PathsMatch(List<Vector3> path, List<Vector3> other)
	{
		return path == other || (path == null ^ other == null) || (path.Count == other.Count && path[0] == other[0] && path[path.Count - 1] == other[other.Count - 1]);
	}

	// Token: 0x0400129A RID: 4762
	private AiActorController ai;

	// Token: 0x0400129B RID: 4763
	private List<Vector3> visualizedPath;

	// Token: 0x0400129C RID: 4764
	private IngameDebugGizmos.Gizmo currentGizmo;

	// Token: 0x0400129D RID: 4765
	private Vector3 pathStart;

	// Token: 0x0400129E RID: 4766
	private Vector3 pathEnd;
}
