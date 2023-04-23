using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x02000378 RID: 888
	public class ScriptedPathGroup : MonoBehaviour
	{
		// Token: 0x06001661 RID: 5729 RVA: 0x00011ADC File Offset: 0x0000FCDC
		public void FindPaths()
		{
			this.paths = new List<ScriptedPath>(base.GetComponentsInChildren<ScriptedPath>());
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00011AEF File Offset: 0x0000FCEF
		public void Play(ScriptedPathSeeker[] seekers)
		{
			this.isPlaying = true;
			this.activeSeekers = seekers;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00011AFF File Offset: 0x0000FCFF
		public void Stop()
		{
			this.isPlaying = false;
			this.activeSeekers = null;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000A021C File Offset: 0x0009E41C
		public void Update()
		{
			if (!this.isPlaying)
			{
				return;
			}
			try
			{
				this.activeSyncs.Clear();
				this.notReadySyncs.Clear();
				bool flag = false;
				for (int i = 0; i < this.activeSeekers.Length; i++)
				{
					if (this.activeSeekers[i].isTraversingPath)
					{
						flag = true;
						byte nextSyncNumber = this.activeSeekers[i].nextSyncNumber;
						if (nextSyncNumber != 255)
						{
							if (this.activeSeekers[i].awaitingSync)
							{
								this.activeSyncs.Add(nextSyncNumber);
							}
							else
							{
								this.notReadySyncs.Add(nextSyncNumber);
							}
						}
					}
				}
				foreach (byte b in this.activeSyncs)
				{
					if (!this.notReadySyncs.Contains(b))
					{
						this.Synchronize(b);
					}
				}
				if (!flag)
				{
					this.Stop();
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.Stop();
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000A032C File Offset: 0x0009E52C
		public void Synchronize(byte syncNumber)
		{
			for (int i = 0; i < this.activeSeekers.Length; i++)
			{
				this.activeSeekers[i].Synchronize(syncNumber);
			}
		}

		// Token: 0x040018C5 RID: 6341
		private HashSet<byte> activeSyncs = new HashSet<byte>();

		// Token: 0x040018C6 RID: 6342
		private HashSet<byte> notReadySyncs = new HashSet<byte>();

		// Token: 0x040018C7 RID: 6343
		private bool isPlaying;

		// Token: 0x040018C8 RID: 6344
		private ScriptedPathSeeker[] activeSeekers;

		// Token: 0x040018C9 RID: 6345
		[NonSerialized]
		public List<ScriptedPath> paths;
	}
}
