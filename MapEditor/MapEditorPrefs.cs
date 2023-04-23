using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000616 RID: 1558
	public static class MapEditorPrefs
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x0001BA26 File Offset: 0x00019C26
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x0001BA36 File Offset: 0x00019C36
		public static bool showAutosaveFiles
		{
			get
			{
				return PlayerPrefs.GetInt("MapEditor.showAutosaveFiles", 0) != 0;
			}
			set
			{
				PlayerPrefs.SetInt("MapEditor.showAutosaveFiles", value ? 1 : 0);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x0001BA49 File Offset: 0x00019C49
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x0001BA5A File Offset: 0x00019C5A
		public static float autosaveInterval
		{
			get
			{
				return PlayerPrefs.GetFloat("MapEditor.autosaveInterval", 600f);
			}
			set
			{
				PlayerPrefs.SetFloat("MapEditor.autosaveInterval", value);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x0001BA67 File Offset: 0x00019C67
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x0001BA78 File Offset: 0x00019C78
		public static float gridSize
		{
			get
			{
				return PlayerPrefs.GetFloat("MapEditor.gridSize", 0.5f);
			}
			set
			{
				PlayerPrefs.SetFloat("MapEditor.gridSize", value);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x0001BA85 File Offset: 0x00019C85
		// (set) Token: 0x06002800 RID: 10240 RVA: 0x0001BA95 File Offset: 0x00019C95
		public static bool muteAudio
		{
			get
			{
				return PlayerPrefs.GetInt("MapEditor.muteAudio", 0) != 0;
			}
			set
			{
				PlayerPrefs.SetInt("MapEditor.muteAudio", value ? 1 : 0);
			}
		}
	}
}
