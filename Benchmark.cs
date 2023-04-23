using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

// Token: 0x020002F0 RID: 752
public class Benchmark : MonoBehaviour
{
	// Token: 0x060013C8 RID: 5064 RVA: 0x0000FCAD File Offset: 0x0000DEAD
	private void Awake()
	{
		this.samples = new List<Benchmark.FrameSample>(60000);
		if (GameModeBase.instance.spawnLayout == GameModeBase.SpawnLayout.Default)
		{
			this.GenerateSpawnLayout();
		}
		this.GenerateCameraWaypoints();
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x000942A4 File Offset: 0x000924A4
	private void GenerateCameraWaypoints()
	{
		IEnumerable<SpawnPoint> enumerable = from s in ActorManager.instance.spawnPoints
		where s.owner == 0 && s.IsFrontLine()
		select s;
		IEnumerable<SpawnPoint> enumerable2 = from s in ActorManager.instance.spawnPoints
		where s.owner == 1 && s.IsFrontLine()
		select s;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		int num = 0;
		foreach (SpawnPoint spawnPoint in enumerable)
		{
			foreach (SpawnPoint spawnPoint2 in enumerable2)
			{
				vector2 += (spawnPoint.transform.position + spawnPoint2.transform.position) * 0.5f;
				vector += spawnPoint.transform.position - spawnPoint2.transform.position;
				num++;
			}
		}
		vector2 /= (float)num;
		Vector3 normalized = new Vector3(-vector.z, 0f, vector.x).normalized;
		float num2 = 1f;
		float num3 = 0f;
		foreach (SpawnPoint spawnPoint3 in enumerable)
		{
			foreach (SpawnPoint spawnPoint4 in enumerable2)
			{
				float a = Vector3.Dot(((spawnPoint3.transform.position + spawnPoint4.transform.position) * 0.5f - vector2).ToGround(), normalized);
				num2 = Mathf.Max(a, num2);
				num3 = Mathf.Min(a, num3);
			}
		}
		Quaternion lhs = Quaternion.LookRotation(normalized);
		this.startRot = lhs * Quaternion.Euler(50f, 0f, 0f);
		this.endRot = lhs * Quaternion.Euler(10f, 0f, 0f);
		this.start = vector2 + (num3 - 100f) * normalized;
		this.end = vector2 + num2 * normalized;
		this.start -= 100f * (this.startRot * Vector3.forward);
		this.end -= 100f * (this.endRot * Vector3.forward);
		this.end.y = this.start.y;
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x000945C8 File Offset: 0x000927C8
	private void GenerateSpawnLayout()
	{
		SpawnPoint[] spawnPoints = ActorManager.instance.spawnPoints;
		bool flag = true;
		while (flag)
		{
			flag = false;
			foreach (SpawnPoint spawnPoint in spawnPoints)
			{
				if (spawnPoint.owner != -1)
				{
					foreach (SpawnPoint spawnPoint2 in spawnPoint.outgoingNeighbors)
					{
						if (spawnPoint2.owner == -1)
						{
							spawnPoint2.SetOwner(spawnPoint.owner, false);
							flag = true;
						}
					}
				}
			}
		}
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x00094668 File Offset: 0x00092868
	private void Start()
	{
		GameObject[] array = this.activateWhenModsAreLoaded;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!ModManager.instance.onlyOfficialContent);
		}
		this.resultsPanel.SetActive(false);
		this.plotPointPrefab.SetActive(false);
		this.plotIntervalPrefab.SetActive(false);
		FpsActorController.instance.unlockCursorRavenscriptOverride = true;
		base.StartCoroutine(this.StartBenchmark());
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x0000FCD7 File Offset: 0x0000DED7
	private IEnumerator StartBenchmark()
	{
		EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 0f, Color.black);
		this.minFps = float.PositiveInfinity;
		yield return new WaitForSeconds(1f);
		GameManager.instance.hudEnabled = false;
		EffectUi.FadeOut(EffectUi.FadeType.FullScreen, 0f, Color.black);
		GameModeBase.instance.ForceSpawnDeadActors();
		this.spectatorCamera = UnityEngine.Object.FindObjectOfType<SpectatorCamera>();
		this.spectatorCamera.DisableImageEffects();
		this.cameraAction.Start();
		yield return new WaitForSeconds(2f);
		this.startTimestamp = Time.time;
		this.benchmarkAction.Start();
		EffectUi.FadeIn(EffectUi.FadeType.FullScreen, 1f, Color.black);
		yield return new WaitUntil(new Func<bool>(this.benchmarkAction.TrueDone));
		this.EndBenchmark();
		yield break;
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
	private void Update()
	{
		if (!this.benchmarkAction.TrueDone())
		{
			this.SampleFrame();
		}
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x000946DC File Offset: 0x000928DC
	private void SampleFrame()
	{
		float deltaTime = Time.deltaTime;
		float time = Time.time - this.startTimestamp;
		Benchmark.FrameSample frameSample = new Benchmark.FrameSample(deltaTime, time);
		this.samples.Add(frameSample);
		this.UpdateSampleUI(frameSample);
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x00094718 File Offset: 0x00092918
	private void UpdateSampleUI(Benchmark.FrameSample sample)
	{
		float num = sample.ToFrameRate();
		this.minFps = Mathf.Min(this.minFps, num);
		this.maxFps = Mathf.Max(this.maxFps, num);
		this.fpsValue.text = this.FormatString("{0:0.0}", new object[]
		{
			num
		});
		this.meanFpsValue.text = this.FormatString("{0:0.0}", new object[]
		{
			(float)this.samples.Count / sample.time
		});
		this.minFpsValue.text = this.FormatString("{0:0.0}", new object[]
		{
			this.minFps
		});
		this.maxFpsValue.text = this.FormatString("{0:0.0}", new object[]
		{
			this.maxFps
		});
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x0000FCFB File Offset: 0x0000DEFB
	private void LateUpdate()
	{
		if (!this.cameraAction.TrueDone())
		{
			this.UpdateCamera();
		}
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x00094804 File Offset: 0x00092A04
	private void UpdateCamera()
	{
		float t = this.cameraAction.Ratio();
		this.spectatorCamera.transform.position = Vector3.Lerp(this.start, this.end, t);
		this.spectatorCamera.transform.rotation = Quaternion.Slerp(this.startRot, this.endRot, t);
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x00094864 File Offset: 0x00092A64
	private void EndBenchmark()
	{
		this.infoLabel.text = GameManager.GenerateVersionString();
		this.resultsPanel.SetActive(true);
		this.resultsTitle.text = string.Format("Benchmark Results - {0} bots on {1}", ActorManager.instance.actors.Count - 1, GameManager.instance.sceneName);
		int count = this.samples.Count;
		int num = 0;
		float num2 = (float)this.samples.Count / 60f;
		this.plotMaxFPS = Mathf.Ceil(this.maxFps * 1.05f);
		this.plotMaxValueLabel.text = this.FormatString("{0:0}", new object[]
		{
			this.plotMaxFPS
		});
		for (int i = 0; i < Benchmark.THRESHOLD_VALUES.Length; i++)
		{
			float num3 = Benchmark.THRESHOLD_VALUES[i] / this.plotMaxFPS;
			this.fpsThresholds[i].anchorMin = new Vector2(0f, num3);
			this.fpsThresholds[i].anchorMax = new Vector2(1f, num3);
			this.fpsThresholds[i].gameObject.SetActive(num3 < 0.98f);
		}
		float y = num2 / this.plotMaxFPS;
		this.meanThreshold.anchorMin = new Vector2(0f, y);
		this.meanThreshold.anchorMax = new Vector2(1f, y);
		this.meanThreshold.GetComponentInChildren<Text>().text = this.FormatString("Mean ({0:0.0})", new object[]
		{
			num2
		});
		for (int j = 0; j < 120; j++)
		{
			float num4 = (float)(j + 1) / 120f * 60f;
			float num5 = 0f;
			int num6 = 0;
			float num7 = float.MaxValue;
			float num8 = float.MinValue;
			for (int k = num; k < count; k++)
			{
				float b = this.samples[k].ToFrameRate();
				if (this.samples[k].time >= num4)
				{
					num = Mathf.Max(k - 1, num);
					break;
				}
				num6++;
				num5 += this.samples[k].dt;
				num7 = Mathf.Min(num7, b);
				num8 = Mathf.Max(num8, b);
			}
			if (num6 > 0)
			{
				float num9 = num5 / (float)num6;
				float num10 = 1f / num9;
				float normalizedTime = num4 / 60f;
				this.InstantiatePlotInterval(normalizedTime, num10, num8, this.maxColor);
				this.InstantiatePlotInterval(normalizedTime, num7, num10, this.minColor);
				this.InstantiatePlotPoint(normalizedTime, num10, this.meanColor);
			}
		}
		this.PopulateResultLabels();
		base.GetComponentInChildren<GraphicRaycaster>(true).enabled = true;
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x00094B24 File Offset: 0x00092D24
	private void InstantiatePlotPoint(float normalizedTime, float fps, Color color)
	{
		RawImage component = UnityEngine.Object.Instantiate<GameObject>(this.plotPointPrefab, this.plotContainer).GetComponent<RawImage>();
		Vector2 vector = new Vector2(normalizedTime, fps / this.plotMaxFPS);
		component.rectTransform.anchorMin = vector;
		component.rectTransform.anchorMax = vector;
		component.color = color;
		component.gameObject.SetActive(true);
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x00094B84 File Offset: 0x00092D84
	private void InstantiatePlotInterval(float normalizedTime, float from, float to, Color color)
	{
		RawImage component = UnityEngine.Object.Instantiate<GameObject>(this.plotIntervalPrefab, this.plotContainer).GetComponent<RawImage>();
		component.rectTransform.anchorMin = new Vector2(normalizedTime, from / this.plotMaxFPS);
		component.rectTransform.anchorMax = new Vector2(normalizedTime, to / this.plotMaxFPS);
		component.color = color;
		component.gameObject.SetActive(true);
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x00094BEC File Offset: 0x00092DEC
	private void PopulateResultLabels()
	{
		try
		{
			this.PopulateCPULabels();
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		try
		{
			this.PopulateGPULabels();
		}
		catch (Exception exception2)
		{
			Debug.LogException(exception2);
		}
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x00094C34 File Offset: 0x00092E34
	private void PopulateCPULabels()
	{
		this.cpuSystem.text = SystemInfo.processorType;
		this.cpuCores.text = SystemInfo.processorCount.ToString();
		this.samples = null;
		GC.Collect();
		int num = (int)(Profiler.GetTotalAllocatedMemoryLong() / 1000000L);
		this.cpuRAM.text = this.FormatString("{0} / {1} MB", new object[]
		{
			num,
			SystemInfo.systemMemorySize
		});
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x00094CB8 File Offset: 0x00092EB8
	private void PopulateGPULabels()
	{
		this.gpuSystem.text = SystemInfo.graphicsDeviceName;
		this.gpuApi.text = SystemInfo.graphicsDeviceVersion;
		this.gpuVRAM.text = this.FormatString("{0} MB", new object[]
		{
			SystemInfo.graphicsMemorySize
		});
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x0000FD10 File Offset: 0x0000DF10
	private string FormatString(string format, params object[] args)
	{
		return string.Format(CultureInfo.InvariantCulture, format, args);
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x0000ACEF File Offset: 0x00008EEF
	public void OnQuitClicked()
	{
		Application.Quit();
	}

	// Token: 0x04001540 RID: 5440
	public static bool isRunning = false;

	// Token: 0x04001541 RID: 5441
	private const int MAX_ASSUMED_FPS = 1000;

	// Token: 0x04001542 RID: 5442
	private const float WARMUP = 2f;

	// Token: 0x04001543 RID: 5443
	private const float CAMERA_OFFSET = 100f;

	// Token: 0x04001544 RID: 5444
	private const float DURATION = 60f;

	// Token: 0x04001545 RID: 5445
	private const int MAX_SAMPLES = 60000;

	// Token: 0x04001546 RID: 5446
	private const int N_DISPLAY_SAMPLES = 120;

	// Token: 0x04001547 RID: 5447
	private TimedAction benchmarkAction = new TimedAction(60f, false);

	// Token: 0x04001548 RID: 5448
	private TimedAction cameraAction = new TimedAction(62f, false);

	// Token: 0x04001549 RID: 5449
	private float startTimestamp;

	// Token: 0x0400154A RID: 5450
	private Quaternion startRot;

	// Token: 0x0400154B RID: 5451
	private Quaternion endRot;

	// Token: 0x0400154C RID: 5452
	private SpectatorCamera spectatorCamera;

	// Token: 0x0400154D RID: 5453
	private List<Benchmark.FrameSample> samples;

	// Token: 0x0400154E RID: 5454
	private float minFps;

	// Token: 0x0400154F RID: 5455
	private float maxFps;

	// Token: 0x04001550 RID: 5456
	public Text fpsValue;

	// Token: 0x04001551 RID: 5457
	public Text meanFpsValue;

	// Token: 0x04001552 RID: 5458
	public Text minFpsValue;

	// Token: 0x04001553 RID: 5459
	public Text maxFpsValue;

	// Token: 0x04001554 RID: 5460
	public GameObject resultsPanel;

	// Token: 0x04001555 RID: 5461
	public Color maxColor;

	// Token: 0x04001556 RID: 5462
	public Color minColor;

	// Token: 0x04001557 RID: 5463
	public Color meanColor;

	// Token: 0x04001558 RID: 5464
	public Text plotMaxValueLabel;

	// Token: 0x04001559 RID: 5465
	public RectTransform plotContainer;

	// Token: 0x0400155A RID: 5466
	public GameObject plotPointPrefab;

	// Token: 0x0400155B RID: 5467
	public GameObject plotIntervalPrefab;

	// Token: 0x0400155C RID: 5468
	public Text resultsTitle;

	// Token: 0x0400155D RID: 5469
	public Text infoLabel;

	// Token: 0x0400155E RID: 5470
	public Text cpuSystem;

	// Token: 0x0400155F RID: 5471
	public Text cpuCores;

	// Token: 0x04001560 RID: 5472
	public Text cpuRAM;

	// Token: 0x04001561 RID: 5473
	public Text gpuSystem;

	// Token: 0x04001562 RID: 5474
	public Text gpuApi;

	// Token: 0x04001563 RID: 5475
	public Text gpuVRAM;

	// Token: 0x04001564 RID: 5476
	public RectTransform[] fpsThresholds;

	// Token: 0x04001565 RID: 5477
	public RectTransform meanThreshold;

	// Token: 0x04001566 RID: 5478
	public GameObject[] activateWhenModsAreLoaded;

	// Token: 0x04001567 RID: 5479
	private static readonly float[] THRESHOLD_VALUES = new float[]
	{
		30f,
		60f,
		120f
	};

	// Token: 0x04001568 RID: 5480
	private Vector3 start;

	// Token: 0x04001569 RID: 5481
	private Vector3 end;

	// Token: 0x0400156A RID: 5482
	private float plotMaxFPS;

	// Token: 0x020002F1 RID: 753
	private struct FrameSample
	{
		// Token: 0x060013DC RID: 5084 RVA: 0x0000FD66 File Offset: 0x0000DF66
		public FrameSample(float dt, float time)
		{
			this.dt = dt;
			this.time = time;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0000FD76 File Offset: 0x0000DF76
		public float ToFrameRate()
		{
			return 1f / this.dt;
		}

		// Token: 0x0400156B RID: 5483
		public float dt;

		// Token: 0x0400156C RID: 5484
		public float time;
	}
}
