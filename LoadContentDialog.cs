using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EA RID: 490
public class LoadContentDialog : MonoBehaviour
{
	// Token: 0x06000D21 RID: 3361 RVA: 0x0000AA6B File Offset: 0x00008C6B
	public static void SetProgress(float progress)
	{
		LoadContentDialog.instance.globalBar.anchorMax = new Vector2(progress, 1f);
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0007BEC0 File Offset: 0x0007A0C0
	public static void Show()
	{
		LoadContentDialog.instance.canvas.enabled = true;
		LoadContentDialog.instance.barPanel.SetActive(false);
		LoadContentDialog.instance.errorPanel.SetActive(false);
		LoadContentDialog.instance.versionPanel.SetActive(false);
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0007BF10 File Offset: 0x0007A110
	public static void Hide()
	{
		try
		{
			LoadContentDialog.instance.canvas.enabled = false;
			LoadContentDialog.ClearLoadingBars();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0007BF48 File Offset: 0x0007A148
	public static void ClearLoadingBars()
	{
		for (int i = 0; i < LoadContentDialog.instance.activeBars.Count; i++)
		{
			UnityEngine.Object.Destroy(LoadContentDialog.instance.activeBars[i].gameObject);
		}
		LoadContentDialog.instance.activeBars.Clear();
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0007BF98 File Offset: 0x0007A198
	public static void CreateLoadingBars(int nWorkers)
	{
		for (int i = 0; i < nWorkers; i++)
		{
			LoadWorkerProgressBar component = UnityEngine.Object.Instantiate<GameObject>(LoadContentDialog.instance.loadInfoPrefab, LoadContentDialog.instance.barPanel.transform).GetComponent<LoadWorkerProgressBar>();
			LoadContentDialog.instance.activeBars.Add(component);
		}
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0000AA87 File Offset: 0x00008C87
	public static void ClearErrorPanelText()
	{
		LoadContentDialog.instance.errorText.text = "";
		LoadContentDialog.hasError = false;
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0000AAA3 File Offset: 0x00008CA3
	public static void AppendError(string message)
	{
		Text text = LoadContentDialog.instance.errorText;
		text.text = text.text + message + "\n";
		LoadContentDialog.hasError = true;
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0000AACB File Offset: 0x00008CCB
	public static void ShowBarPanel(int nWorkers)
	{
		LoadContentDialog.ClearLoadingBars();
		LoadContentDialog.CreateLoadingBars(nWorkers);
		LoadContentDialog.Show();
		LoadContentDialog.instance.barPanel.SetActive(true);
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0000AAED File Offset: 0x00008CED
	public static void ShowErrorPanel()
	{
		LoadContentDialog.Show();
		LoadContentDialog.instance.errorPanel.SetActive(true);
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0000AB04 File Offset: 0x00008D04
	public void CloseErrorPanel()
	{
		this.errorPanel.SetActive(false);
		if (this.showVersionWarning)
		{
			LoadContentDialog.ShowVersionPanel();
			return;
		}
		LoadContentDialog.Hide();
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0000AB25 File Offset: 0x00008D25
	public static void ShowVersionPanel()
	{
		LoadContentDialog.Show();
		LoadContentDialog.instance.versionPanel.SetActive(true);
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0007BFE8 File Offset: 0x0007A1E8
	public static void RegisterWorker(int index, LoadModWorker worker)
	{
		try
		{
			LoadContentDialog.instance.activeBars[index].RegisterWorker(worker);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0000AB3C File Offset: 0x00008D3C
	public void CloseVersionPanel()
	{
		this.versionPanel.SetActive(false);
		LoadContentDialog.Hide();
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0000AB4F File Offset: 0x00008D4F
	public void OpenVersionInfo()
	{
		GameManager.instance.steamworks.OpenCommunityFilePage(new PublishedFileId_t((ulong)-2070029642));
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0000AB6B File Offset: 0x00008D6B
	private void Awake()
	{
		LoadContentDialog.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.activeBars = new List<LoadWorkerProgressBar>();
		LoadContentDialog.Hide();
	}

	// Token: 0x04000E2E RID: 3630
	private const ulong BETA_BRANCH_INFO_GUIDE_ID = 2224937654UL;

	// Token: 0x04000E2F RID: 3631
	public static LoadContentDialog instance;

	// Token: 0x04000E30 RID: 3632
	public static bool hasError;

	// Token: 0x04000E31 RID: 3633
	public GameObject loadInfoPrefab;

	// Token: 0x04000E32 RID: 3634
	public GameObject barPanel;

	// Token: 0x04000E33 RID: 3635
	public GameObject errorPanel;

	// Token: 0x04000E34 RID: 3636
	public GameObject versionPanel;

	// Token: 0x04000E35 RID: 3637
	public RectTransform globalBar;

	// Token: 0x04000E36 RID: 3638
	public Text errorText;

	// Token: 0x04000E37 RID: 3639
	[NonSerialized]
	public bool showVersionWarning;

	// Token: 0x04000E38 RID: 3640
	private Canvas canvas;

	// Token: 0x04000E39 RID: 3641
	private List<LoadWorkerProgressBar> activeBars;
}
