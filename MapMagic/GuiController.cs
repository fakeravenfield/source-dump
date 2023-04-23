using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapMagic
{
	// Token: 0x020004B4 RID: 1204
	public class GuiController : MonoBehaviour
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0001651C File Offset: 0x0001471C
		public CharController charController
		{
			get
			{
				if (this._charController == null)
				{
					this._charController = UnityEngine.Object.FindObjectOfType<CharController>();
				}
				return this._charController;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0001653D File Offset: 0x0001473D
		public FlybyController demoController
		{
			get
			{
				if (this._demoController == null)
				{
					this._demoController = UnityEngine.Object.FindObjectOfType<FlybyController>();
				}
				return this._demoController;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0001655E File Offset: 0x0001475E
		public CameraController cameraController
		{
			get
			{
				if (this._cameraController == null)
				{
					this._cameraController = UnityEngine.Object.FindObjectOfType<CameraController>();
				}
				return this._cameraController;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0001657F File Offset: 0x0001477F
		public MapMagic mapMagic
		{
			get
			{
				if (this._mapMagic == null)
				{
					this._mapMagic = UnityEngine.Object.FindObjectOfType<MapMagic>();
				}
				return this._mapMagic;
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000C66E0 File Offset: 0x000C48E0
		private T GetObject<T>(string name) where T : UnityEngine.Object
		{
			if (this.objects.ContainsKey(name))
			{
				return (T)((object)this.objects[name]);
			}
			Transform transform = base.transform.FindChildRecursive(name);
			if (transform == null)
			{
				return default(T);
			}
			T component = transform.GetComponent<T>();
			if (component == null)
			{
				return default(T);
			}
			this.objects.Add(name, component);
			return component;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000C6760 File Offset: 0x000C4960
		public void Update()
		{
			Toggle @object = this.GetObject<Toggle>("Fullscreen");
			if (Input.GetKeyDown("f"))
			{
				@object.isOn = !@object.isOn;
			}
			if (@object.isOn && !Screen.fullScreen)
			{
				this.oldScreenWidth = Screen.width;
				this.oldScreenHeight = Screen.height;
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
			}
			if (!@object.isOn && Screen.fullScreen)
			{
				Screen.SetResolution(this.oldScreenWidth, this.oldScreenHeight, false);
			}
			@object.isOn = Screen.fullScreen;
			Toggle object2 = this.GetObject<Toggle>("MouseLook");
			if (Input.GetKeyDown("m"))
			{
				object2.isOn = !object2.isOn;
			}
			this.cameraController.lockCursor = object2.isOn;
			if (Input.GetKeyDown("1") || this.GetObject<Toggle>("Walk").isOn)
			{
				this.charController.enabled = true;
				this.charController.gravity = true;
				this.charController.speed = 5f;
				this.charController.acceleration = 50f;
				this.demoController.enabled = false;
				this.cameraController.follow = 0f;
				this.GetObject<Toggle>("Fly").isOn = false;
				this.GetObject<Toggle>("Demo").isOn = false;
			}
			if (Input.GetKeyDown("2") || this.GetObject<Toggle>("Fly").isOn)
			{
				this.charController.enabled = true;
				this.charController.gravity = false;
				this.charController.speed = 50f;
				this.charController.acceleration = 150f;
				this.demoController.enabled = false;
				this.cameraController.follow = 0f;
				this.GetObject<Toggle>("Walk").isOn = false;
				this.GetObject<Toggle>("Demo").isOn = false;
			}
			if (Input.GetKeyDown("3") || this.GetObject<Toggle>("Demo").isOn)
			{
				this.charController.enabled = false;
				this.demoController.enabled = true;
				this.cameraController.follow = 5f;
				this.GetObject<Toggle>("Fly").isOn = false;
				this.GetObject<Toggle>("Walk").isOn = false;
			}
			GameObject gameObject = this.GetObject<RectTransform>("GenerateMark").gameObject;
			if (this.mapMagic.terrains.complete && gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
			if (!this.mapMagic.terrains.complete && !gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			if (gameObject.activeSelf)
			{
				Transform object3 = this.GetObject<RectTransform>("GenerateMarkIcon");
				this.iconRotation -= Time.deltaTime * 100f;
				this.iconRotation %= 360f;
				object3.rotation = Quaternion.Euler(0f, 0f, this.iconRotation);
			}
			this.fpsTimeleft -= Time.deltaTime;
			this.frames++;
			if ((double)this.fpsTimeleft <= 0.0)
			{
				this.GetObject<Text>("FpsCounter").text = "FPS: " + ((float)this.frames / this.fpsUpdateInterval).ToString();
				this.frames = 0;
				this.fpsTimeleft = this.fpsUpdateInterval;
			}
			if (this.camPos.y < 0f)
			{
				this.camPos = Camera.main.transform.position;
			}
			this.distTravalled += (Camera.main.transform.position - this.camPos).magnitude;
			this.camPos = Camera.main.transform.position;
			this.GetObject<Text>("DistTravalled").text = "Distance travelled:\n" + ((int)this.distTravalled).ToString();
		}

		// Token: 0x04001ED7 RID: 7895
		private CharController _charController;

		// Token: 0x04001ED8 RID: 7896
		private FlybyController _demoController;

		// Token: 0x04001ED9 RID: 7897
		private CameraController _cameraController;

		// Token: 0x04001EDA RID: 7898
		private MapMagic _mapMagic;

		// Token: 0x04001EDB RID: 7899
		private int oldScreenWidth;

		// Token: 0x04001EDC RID: 7900
		private int oldScreenHeight;

		// Token: 0x04001EDD RID: 7901
		private float iconRotation;

		// Token: 0x04001EDE RID: 7902
		public float fpsUpdateInterval = 0.5f;

		// Token: 0x04001EDF RID: 7903
		private int frames;

		// Token: 0x04001EE0 RID: 7904
		private float fpsTimeleft;

		// Token: 0x04001EE1 RID: 7905
		private Vector3 camPos = new Vector3(0f, -1f, 0f);

		// Token: 0x04001EE2 RID: 7906
		private float distTravalled;

		// Token: 0x04001EE3 RID: 7907
		private Dictionary<string, UnityEngine.Object> objects = new Dictionary<string, UnityEngine.Object>();
	}
}
