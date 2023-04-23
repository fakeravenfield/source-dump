using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Lua;
using Steamworks;
using UnityEngine;

// Token: 0x02000353 RID: 851
[Serializable]
public class SteelInput
{
	// Token: 0x060015A0 RID: 5536 RVA: 0x000112E4 File Offset: 0x0000F4E4
	public static void OnValueMonitorTriggered(SteelInput.InputSource inputSource)
	{
		SteelInput.activeInputSource = inputSource;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x0009D10C File Offset: 0x0009B30C
	public static int GetUnityAxisIndex(string axisName)
	{
		for (int i = 0; i < SteelInput.UNITY_AXES.Length; i++)
		{
			if (string.Equals(axisName, SteelInput.UNITY_AXES[i], StringComparison.InvariantCulture))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x000112EC File Offset: 0x0000F4EC
	public static bool IsInitialized()
	{
		return SteelInput.instance != null;
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x0009D140 File Offset: 0x0009B340
	public static void Initialize()
	{
		SteelInput.instance = new SteelInput();
		SteelInput.keyCodes = Enum.GetValues(typeof(KeyCode));
		SteelInput.allInputs = Enum.GetValues(typeof(SteelInput.KeyBinds));
		List<string> list = new List<string>();
		foreach (string text in SteelInput.UNITY_AXES)
		{
			if (!text.Contains("Joy"))
			{
				list.Add(text);
			}
		}
		SteelInput.unityAxesNoJoystick = list.ToArray();
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x0009D1BC File Offset: 0x0009B3BC
	public static bool LoadUserKeybinds()
	{
		bool result;
		try
		{
			bool flag;
			SteelInput steelInput = SteelInput.Deserialize(SteelInput.GetFilePath(), out flag);
			steelInput.CollectDictionaryUnpackGarbage();
			SteelInput.instance = steelInput;
			result = flag;
		}
		catch (Exception exception)
		{
			Debug.LogError("Could not load SteelInput user keybinds, exception follows:");
			Debug.LogException(exception);
			result = false;
		}
		return result;
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x000112F6 File Offset: 0x0000F4F6
	public static string GetFilePath()
	{
		return Application.persistentDataPath + "/keybinds.xml";
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x0009D208 File Offset: 0x0009B408
	public static void LoadPreset(SteelInputPreset preset)
	{
		Debug.Log("Loading SteelInput preset: " + preset.gameObject.name);
		SteelInput.instance.dictionaryKeys = new SteelInput.KeyBinds[preset.dictionaryKeys.Length];
		preset.dictionaryKeys.CopyTo(SteelInput.instance.dictionaryKeys, 0);
		SteelInput.instance.dictionaryValues = new SteelInput.Axis[preset.dictionaryValues.Length];
		preset.dictionaryValues.CopyTo(SteelInput.instance.dictionaryValues, 0);
		SteelInput.instance.UnpackDictionary();
		SteelInput.instance.CollectDictionaryUnpackGarbage();
		SteelInput.instance.SetupInputArray();
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x0009D2A8 File Offset: 0x0009B4A8
	public static void ReplaceMissingBindsWithPreset(SteelInputPreset preset)
	{
		if (!SteelInput.IsInitialized())
		{
			SteelInput.Initialize();
		}
		SteelInput.instance.dictionaryKeys = new SteelInput.KeyBinds[preset.dictionaryKeys.Length];
		preset.dictionaryKeys.CopyTo(SteelInput.instance.dictionaryKeys, 0);
		SteelInput.instance.dictionaryValues = new SteelInput.Axis[preset.dictionaryValues.Length];
		preset.dictionaryValues.CopyTo(SteelInput.instance.dictionaryValues, 0);
		SteelInput.instance.UnpackDictionaryOnlyReplaceUnbound();
		SteelInput.instance.CollectDictionaryUnpackGarbage();
		SteelInput.instance.SetupInputArray();
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x0009D33C File Offset: 0x0009B53C
	public static SteelInput Deserialize(string filepath, out bool canDeserializeConfiguration)
	{
		SteelInput steelInput = null;
		canDeserializeConfiguration = false;
		try
		{
			using (XmlReader xmlReader = new XmlTextReader(filepath))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(SteelInput));
				canDeserializeConfiguration = xmlSerializer.CanDeserialize(xmlReader);
				if (canDeserializeConfiguration)
				{
					steelInput = (SteelInput)xmlSerializer.Deserialize(xmlReader);
					steelInput.UnpackDictionary();
					steelInput.SetupInputArray();
				}
				xmlReader.Close();
			}
		}
		catch (Exception exception)
		{
			Debug.LogWarning("Could not deserialize SteelInput configuration file at " + filepath);
			Debug.LogException(exception);
		}
		if (!canDeserializeConfiguration)
		{
			steelInput = new SteelInput();
		}
		return steelInput;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
	public static void SaveConfiguration()
	{
		if (!SteelInput.IsInitialized())
		{
			Debug.LogError("SteelInput: Tried to save configuration without first initializing SteelInput.");
			return;
		}
		try
		{
			SteelInput.instance.Serialize(SteelInput.GetFilePath());
		}
		catch (Exception exception)
		{
			Debug.LogError("Could not serialize SteelInput configuration data. Exception follows:");
			Debug.LogException(exception);
		}
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x0009D434 File Offset: 0x0009B634
	public static void Update()
	{
		for (int i = 0; i < SteelInput.instance.inputAxisArray.Length; i++)
		{
			SteelInput.UpdateInputIndex(i);
		}
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x00011307 File Offset: 0x0000F507
	public static void SetScrollEnabled(bool enabled)
	{
		SteelInput.instance.scrollEnabled = enabled;
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x0009D460 File Offset: 0x0009B660
	public static float GetAxis(SteelInput.KeyBinds input)
	{
		return SteelInput.GetInput(input).GetValue();
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x0009D47C File Offset: 0x0009B67C
	public static bool GetButton(SteelInput.KeyBinds input)
	{
		return SteelInput.GetInput(input).GetButton();
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x0009D498 File Offset: 0x0009B698
	public static bool GetButtonDown(SteelInput.KeyBinds input)
	{
		return SteelInput.GetInput(input).GetButtonDown();
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x0009D4B4 File Offset: 0x0009B6B4
	public static bool GetButtonUp(SteelInput.KeyBinds input)
	{
		return SteelInput.GetInput(input).GetButtonUp();
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x0009D4D0 File Offset: 0x0009B6D0
	public static bool HasInputBound(SteelInput.KeyBinds input)
	{
		return SteelInput.GetInput(input).HasBoundInput();
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x00011314 File Offset: 0x0000F514
	public static SteelInput.Axis GetInput(SteelInput.KeyBinds input)
	{
		return SteelInput.instance.inputAxisArray[(int)input];
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x0009D4EC File Offset: 0x0009B6EC
	public static void UpdateInputIndex(int index)
	{
		SteelInput.Axis axis = SteelInput.instance.inputAxisArray[index];
		axis.Update(SteelInput.instance.scrollEnabled);
		SteelInput.instance.inputAxisArray[index] = axis;
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x00011326 File Offset: 0x0000F526
	public static void SetJoystickBindingEnabled(bool enabled)
	{
		SteelInput.instance.allowJoystickBinding = enabled;
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x00011333 File Offset: 0x0000F533
	public static string[] GetBindableUnityAxes()
	{
		if (!SteelInput.instance.allowJoystickBinding)
		{
			return SteelInput.unityAxesNoJoystick;
		}
		return SteelInput.UNITY_AXES;
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x0009D52C File Offset: 0x0009B72C
	public void Serialize(string filepath)
	{
		this.UpdateInputAxisDictionary();
		this.dictionaryKeys = new SteelInput.KeyBinds[this.inputAxis.Keys.Count];
		this.dictionaryValues = new SteelInput.Axis[this.inputAxis.Values.Count];
		this.inputAxis.Keys.CopyTo(this.dictionaryKeys, 0);
		this.inputAxis.Values.CopyTo(this.dictionaryValues, 0);
		XmlWriter xmlWriter = new XmlTextWriter(filepath, Encoding.Unicode);
		new XmlSerializer(typeof(SteelInput)).Serialize(xmlWriter, this);
		xmlWriter.Flush();
		xmlWriter.Close();
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x0009D5D4 File Offset: 0x0009B7D4
	private void UpdateInputAxisDictionary()
	{
		for (int i = 0; i < this.inputAxisArray.Length; i++)
		{
			SteelInput.KeyBinds key = (SteelInput.KeyBinds)i;
			if (this.inputAxis.ContainsKey(key))
			{
				this.inputAxis[key] = this.inputAxisArray[i];
			}
		}
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x0009D61C File Offset: 0x0009B81C
	public void SetupInputArray()
	{
		this.inputAxisArray = new SteelInput.Axis[SteelInput.allInputs.Length];
		int num = 0;
		foreach (object obj in SteelInput.allInputs)
		{
			SteelInput.KeyBinds keyBinds = (SteelInput.KeyBinds)obj;
			if (this.inputAxis.ContainsKey(keyBinds))
			{
				SteelInput.Axis axis = this.inputAxis[keyBinds];
				axis.DeserializeAxisString();
				axis.UpdateBindFlags();
				this.inputAxisArray[(int)keyBinds] = axis;
			}
			else
			{
				this.inputAxisArray[(int)keyBinds] = default(SteelInput.Axis);
			}
			num++;
		}
		this.RegisterAxisSteamAction();
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x0009D6DC File Offset: 0x0009B8DC
	public void UnpackDictionary()
	{
		int num = Mathf.Min(this.dictionaryKeys.Length, this.dictionaryValues.Length);
		for (int i = 0; i < num; i++)
		{
			if (this.inputAxis.ContainsKey(this.dictionaryKeys[i]))
			{
				this.inputAxis[this.dictionaryKeys[i]] = this.dictionaryValues[i];
			}
		}
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x0009D740 File Offset: 0x0009B940
	public void UnpackDictionaryOnlyReplaceUnbound()
	{
		int num = Mathf.Min(this.dictionaryKeys.Length, this.dictionaryValues.Length);
		for (int i = 0; i < num; i++)
		{
			if (this.inputAxis.ContainsKey(this.dictionaryKeys[i]) && !this.inputAxis[this.dictionaryKeys[i]].HasBoundInput())
			{
				this.inputAxis[this.dictionaryKeys[i]] = this.dictionaryValues[i];
			}
		}
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x0001134C File Offset: 0x0000F54C
	private void CollectDictionaryUnpackGarbage()
	{
		this.dictionaryKeys = null;
		this.dictionaryValues = null;
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x0009D7C0 File Offset: 0x0009B9C0
	public SteelInput()
	{
		Array values = Enum.GetValues(typeof(SteelInput.KeyBinds));
		this.inputAxis = new Dictionary<SteelInput.KeyBinds, SteelInput.Axis>(values.Length);
		foreach (object obj in values)
		{
			SteelInput.KeyBinds key = (SteelInput.KeyBinds)obj;
			this.inputAxis.Add(key, default(SteelInput.Axis));
		}
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x0009D84C File Offset: 0x0009BA4C
	public static KeyboardGlyphGenerator.GlyphData GetBindingGlyph(SteelInput.KeyBinds bind, bool alt)
	{
		return SteelInput.GetInput(bind).GetGlyph(alt);
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x0009D868 File Offset: 0x0009BA68
	public static void BindPositiveKeyCode(SteelInput.KeyBinds input, KeyCode keyCode)
	{
		SteelInput.Axis axis = SteelInput.instance.inputAxisArray[(int)input];
		axis.BindPositiveKeyCode(keyCode);
		SteelInput.instance.inputAxisArray[(int)input] = axis;
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x0009D8A4 File Offset: 0x0009BAA4
	public static void BindNegativeKeyCode(SteelInput.KeyBinds input, KeyCode keyCode)
	{
		SteelInput.Axis axis = SteelInput.instance.inputAxisArray[(int)input];
		axis.BindNegativeKeyCode(keyCode);
		SteelInput.instance.inputAxisArray[(int)input] = axis;
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
	public static void BindUnityAxis(SteelInput.KeyBinds input, string unityAxis, bool invert)
	{
		SteelInput.Axis axis = SteelInput.instance.inputAxisArray[(int)input];
		axis.BindUnityAxis(unityAxis, invert);
		SteelInput.instance.inputAxisArray[(int)input] = axis;
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x0001135C File Offset: 0x0000F55C
	private void RegisterSteamPadAction(InputAnalogActionHandle_t action, SteelInput.KeyBinds bindX, SteelInput.KeyBinds bindY)
	{
		this.inputAxisArray[(int)bindX].SetSteamAnalogAction(action, SteelInput.Axis.SteamAnalogInputType.PadX);
		this.inputAxisArray[(int)bindY].SetSteamAnalogAction(action, SteelInput.Axis.SteamAnalogInputType.PadY);
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x00011384 File Offset: 0x0000F584
	private void RegisterSteamAnalogAction(InputAnalogActionHandle_t action, SteelInput.KeyBinds bind)
	{
		this.inputAxisArray[(int)bind].SetSteamAnalogAction(action, SteelInput.Axis.SteamAnalogInputType.Analog);
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x00011399 File Offset: 0x0000F599
	private void RegisterSteamAnalogAction(InputAnalogActionHandle_t action, InputDigitalActionHandle_t digActionPos, InputDigitalActionHandle_t digActionNeg, SteelInput.KeyBinds bind)
	{
		this.RegisterSteamAnalogAction(action, bind);
		this.inputAxisArray[(int)bind].SetSteamDigitalActionAxis(digActionPos, digActionNeg);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x000113B8 File Offset: 0x0000F5B8
	private void RegisterSteamDigitalAction(InputDigitalActionHandle_t action, SteelInput.KeyBinds bind)
	{
		this.inputAxisArray[(int)bind].SetSteamDigitalAction(action);
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000113CC File Offset: 0x0000F5CC
	private void RegisterSteamDigitalAction(InputDigitalActionHandle_t action, InputDigitalActionHandle_t altAction, SteelInput.KeyBinds bind)
	{
		this.inputAxisArray[(int)bind].SetSteamDigitalActionAlt(action, altAction);
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x0009D91C File Offset: 0x0009BB1C
	private void RegisterAxisSteamAction()
	{
		SteamInputWrapper input = GameManager.instance.steamworks.input;
		if (!input.isInitialized)
		{
			return;
		}
		this.RegisterSteamPadAction(input.move, SteelInput.KeyBinds.Horizontal, SteelInput.KeyBinds.Vertical);
		this.RegisterSteamPadAction(input.camera, SteelInput.KeyBinds.AimX, SteelInput.KeyBinds.AimY);
		this.inputAxisArray[19].InvertSteamAnalogAction();
		this.RegisterSteamAnalogAction(input.lean, input.leanLeft, input.leanRight, SteelInput.KeyBinds.Lean);
		this.RegisterSteamAnalogAction(input.vehicleSteer, SteelInput.KeyBinds.CarSteer);
		this.RegisterSteamPadAction(input.vehicleSteer, SteelInput.KeyBinds.HeliRoll, SteelInput.KeyBinds.HeliPitch);
		this.RegisterSteamPadAction(input.vehicleSteer, SteelInput.KeyBinds.PlaneRoll, SteelInput.KeyBinds.PlanePitch);
		this.RegisterSteamAnalogAction(input.throttle, input.throttleUp, input.throttleDown, SteelInput.KeyBinds.CarThrottle);
		this.RegisterSteamAnalogAction(input.throttle, input.throttleUp, input.throttleDown, SteelInput.KeyBinds.PlaneThrottle);
		this.RegisterSteamAnalogAction(input.throttle, input.throttleUp, input.throttleDown, SteelInput.KeyBinds.HeliThrottle);
		this.RegisterSteamAnalogAction(input.aircraftYaw, SteelInput.KeyBinds.PlaneYaw);
		this.RegisterSteamAnalogAction(input.aircraftYaw, SteelInput.KeyBinds.HeliYaw);
		this.RegisterSteamDigitalAction(input.jump, SteelInput.KeyBinds.Jump);
		this.RegisterSteamDigitalAction(input.sprint, SteelInput.KeyBinds.Sprint);
		this.RegisterSteamDigitalAction(input.crouch, SteelInput.KeyBinds.Crouch);
		this.RegisterSteamDigitalAction(input.prone, SteelInput.KeyBinds.Prone);
		this.RegisterSteamDigitalAction(input.fire, SteelInput.KeyBinds.Fire);
		this.RegisterSteamDigitalAction(input.use, SteelInput.KeyBinds.Use);
		this.RegisterSteamDigitalAction(input.aim, SteelInput.KeyBinds.Aim);
		this.RegisterSteamDigitalAction(input.reload, SteelInput.KeyBinds.Reload);
		this.RegisterSteamDigitalAction(input.kick, SteelInput.KeyBinds.Kick);
		this.RegisterSteamDigitalAction(input.firemode, SteelInput.KeyBinds.FireMode);
		this.RegisterSteamDigitalAction(input.nextWeapon, SteelInput.KeyBinds.NextWeapon);
		this.RegisterSteamDigitalAction(input.prevWeapon, SteelInput.KeyBinds.PreviousWeapon);
		this.RegisterSteamDigitalAction(input.nextScope, SteelInput.KeyBinds.NextScope);
		this.RegisterSteamDigitalAction(input.prevScope, SteelInput.KeyBinds.PreviousScope);
		this.RegisterSteamDigitalAction(input.weapon1, SteelInput.KeyBinds.Weapon1);
		this.RegisterSteamDigitalAction(input.weapon2, SteelInput.KeyBinds.Weapon2);
		this.RegisterSteamDigitalAction(input.weapon3, SteelInput.KeyBinds.Weapon3);
		this.RegisterSteamDigitalAction(input.weapon4, SteelInput.KeyBinds.Weapon4);
		this.RegisterSteamDigitalAction(input.weapon5, SteelInput.KeyBinds.Weapon5);
		this.RegisterSteamDigitalAction(input.squadOrder, SteelInput.KeyBinds.Call);
		this.RegisterSteamDigitalAction(input.squadMap, SteelInput.KeyBinds.SquadLeaderKit);
		this.RegisterSteamDigitalAction(input.thirdperson, SteelInput.KeyBinds.ThirdPersonToggle);
		this.RegisterSteamDigitalAction(input.countermeasures, SteelInput.KeyBinds.Countermeasures);
		this.RegisterSteamDigitalAction(input.autoHover, SteelInput.KeyBinds.AutoHover);
		this.RegisterSteamDigitalAction(input.seat1, SteelInput.KeyBinds.Seat1);
		this.RegisterSteamDigitalAction(input.seat2, SteelInput.KeyBinds.Seat2);
		this.RegisterSteamDigitalAction(input.seat3, SteelInput.KeyBinds.Seat3);
		this.RegisterSteamDigitalAction(input.seat4, SteelInput.KeyBinds.Seat4);
		this.RegisterSteamDigitalAction(input.seat5, SteelInput.KeyBinds.Seat5);
		this.RegisterSteamDigitalAction(input.seat6, SteelInput.KeyBinds.Seat6);
		this.RegisterSteamDigitalAction(input.seat7, SteelInput.KeyBinds.Seat7);
		this.RegisterSteamDigitalAction(input.slowmotion, SteelInput.KeyBinds.Slowmotion);
		this.RegisterSteamDigitalAction(input.nightvision, SteelInput.KeyBinds.Goggles);
		this.RegisterSteamDigitalAction(input.minimap, SteelInput.KeyBinds.Map);
		this.RegisterSteamDigitalAction(input.scoreboard, SteelInput.KeyBinds.PeekScoreboard);
		this.RegisterSteamDigitalAction(input.loadout, input.loadoutClose, SteelInput.KeyBinds.OpenLoadout);
		this.RegisterSteamDigitalAction(input.menu, input.menuReturn, SteelInput.KeyBinds.TogglePauseMenu);
	}

	// Token: 0x040017E7 RID: 6119
	public const string FILE = "keybinds.xml";

	// Token: 0x040017E8 RID: 6120
	public static float AXIS_BUTTON_DEADZONE = 0.3f;

	// Token: 0x040017E9 RID: 6121
	private const int NUMBER_ROW_KEY_INDEX_START = 48;

	// Token: 0x040017EA RID: 6122
	private const int NUMBER_ROW_KEY_INDEX_END = 57;

	// Token: 0x040017EB RID: 6123
	public static readonly string[] UNITY_AXES = new string[]
	{
		"Mouse ScrollWheel",
		"Horizontal Joy",
		"Vertical Joy",
		"Joy Axis 3",
		"Joy Axis 4",
		"Joy Axis 5",
		"Joy Axis 6",
		"Joy Axis 7",
		"Joy Button 0",
		"Joy Button 1",
		"Joy Button 2",
		"Joy Button 3",
		"Joy Button 4",
		"Joy Button 5",
		"Joy Button 6",
		"Joy Button 7",
		"Joy Button 8",
		"Joy Button 9",
		"Joy Button 10",
		"Joy Button 11",
		"Joy Button 12",
		"Joy Button 13",
		"Joy Button 14",
		"Joy Button 15",
		"Joy Button 16",
		"Joy Button 17",
		"Joy Button 18",
		"Joy Button 19",
		"Mouse X",
		"Mouse Y"
	};

	// Token: 0x040017EC RID: 6124
	public const string SCROLL_WHEEL_AXIS = "Mouse ScrollWheel";

	// Token: 0x040017ED RID: 6125
	private static readonly string[] MOUSE_AXIS_NAMES = new string[]
	{
		"Mouse X",
		"Mouse Y"
	};

	// Token: 0x040017EE RID: 6126
	public static string[] unityAxesNoJoystick;

	// Token: 0x040017EF RID: 6127
	private static SteelInput instance;

	// Token: 0x040017F0 RID: 6128
	public static Array keyCodes;

	// Token: 0x040017F1 RID: 6129
	public static Array allInputs;

	// Token: 0x040017F2 RID: 6130
	public static bool ignoreNumberRowBinds = false;

	// Token: 0x040017F3 RID: 6131
	public static SteelInput.InputSource activeInputSource = SteelInput.InputSource.Unity;

	// Token: 0x040017F4 RID: 6132
	private Dictionary<SteelInput.KeyBinds, SteelInput.Axis> inputAxis;

	// Token: 0x040017F5 RID: 6133
	private SteelInput.Axis[] inputAxisArray;

	// Token: 0x040017F6 RID: 6134
	public SteelInput.KeyBinds[] dictionaryKeys;

	// Token: 0x040017F7 RID: 6135
	public SteelInput.Axis[] dictionaryValues;

	// Token: 0x040017F8 RID: 6136
	private bool allowJoystickBinding;

	// Token: 0x040017F9 RID: 6137
	private bool scrollEnabled;

	// Token: 0x02000354 RID: 852
	public enum InputSource
	{
		// Token: 0x040017FB RID: 6139
		Unity,
		// Token: 0x040017FC RID: 6140
		Steam
	}

	// Token: 0x02000355 RID: 853
	public enum KeyBinds
	{
		// Token: 0x040017FE RID: 6142
		Horizontal,
		// Token: 0x040017FF RID: 6143
		Vertical,
		// Token: 0x04001800 RID: 6144
		Fire,
		// Token: 0x04001801 RID: 6145
		Aim,
		// Token: 0x04001802 RID: 6146
		Lean,
		// Token: 0x04001803 RID: 6147
		Reload,
		// Token: 0x04001804 RID: 6148
		Use,
		// Token: 0x04001805 RID: 6149
		Crouch,
		// Token: 0x04001806 RID: 6150
		Sprint,
		// Token: 0x04001807 RID: 6151
		Jump,
		// Token: 0x04001808 RID: 6152
		Weapon1,
		// Token: 0x04001809 RID: 6153
		Weapon2,
		// Token: 0x0400180A RID: 6154
		Weapon3,
		// Token: 0x0400180B RID: 6155
		Weapon4,
		// Token: 0x0400180C RID: 6156
		Weapon5,
		// Token: 0x0400180D RID: 6157
		NextWeapon,
		// Token: 0x0400180E RID: 6158
		OpenLoadout,
		// Token: 0x0400180F RID: 6159
		Map,
		// Token: 0x04001810 RID: 6160
		AimX,
		// Token: 0x04001811 RID: 6161
		AimY,
		// Token: 0x04001812 RID: 6162
		Kick,
		// Token: 0x04001813 RID: 6163
		Slowmotion,
		// Token: 0x04001814 RID: 6164
		CarSteer,
		// Token: 0x04001815 RID: 6165
		CarThrottle,
		// Token: 0x04001816 RID: 6166
		HeliPitch,
		// Token: 0x04001817 RID: 6167
		HeliYaw,
		// Token: 0x04001818 RID: 6168
		HeliRoll,
		// Token: 0x04001819 RID: 6169
		HeliThrottle,
		// Token: 0x0400181A RID: 6170
		PlanePitch,
		// Token: 0x0400181B RID: 6171
		PlaneYaw,
		// Token: 0x0400181C RID: 6172
		PlaneRoll,
		// Token: 0x0400181D RID: 6173
		PlaneThrottle,
		// Token: 0x0400181E RID: 6174
		PreviousWeapon,
		// Token: 0x0400181F RID: 6175
		Call,
		// Token: 0x04001820 RID: 6176
		SquadLeaderKit,
		// Token: 0x04001821 RID: 6177
		Goggles,
		// Token: 0x04001822 RID: 6178
		ThirdPersonToggle,
		// Token: 0x04001823 RID: 6179
		Countermeasures,
		// Token: 0x04001824 RID: 6180
		Scoreboard,
		// Token: 0x04001825 RID: 6181
		Prone,
		// Token: 0x04001826 RID: 6182
		FireMode,
		// Token: 0x04001827 RID: 6183
		NextScope,
		// Token: 0x04001828 RID: 6184
		PreviousScope,
		// Token: 0x04001829 RID: 6185
		ScopeModifier,
		// Token: 0x0400182A RID: 6186
		Console,
		// Token: 0x0400182B RID: 6187
		ReloadScripts,
		// Token: 0x0400182C RID: 6188
		AutoHover,
		// Token: 0x0400182D RID: 6189
		TogglePauseMenu,
		// Token: 0x0400182E RID: 6190
		PeekScoreboard,
		// Token: 0x0400182F RID: 6191
		Seat1,
		// Token: 0x04001830 RID: 6192
		Seat2,
		// Token: 0x04001831 RID: 6193
		Seat3,
		// Token: 0x04001832 RID: 6194
		Seat4,
		// Token: 0x04001833 RID: 6195
		Seat5,
		// Token: 0x04001834 RID: 6196
		Seat6,
		// Token: 0x04001835 RID: 6197
		Seat7
	}

	// Token: 0x02000356 RID: 854
	[Serializable]
	public struct Axis
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0009DD80 File Offset: 0x0009BF80
		public Axis(KeyCode posKeyCode, KeyCode negKeyCode)
		{
			this.posKeyCode = posKeyCode;
			this.negKeyCode = negKeyCode;
			this.useUnityAxis = false;
			this.unityAxisIndex = -1;
			this.invertUnityAxis = false;
			this.held = false;
			this.pressed = false;
			this.released = false;
			this.unityAxis = null;
			this.applyDeadzone = false;
			this.isMouseAxis = false;
			this.isScrollWheel = false;
			this.steamDigitalInput = SteelInput.Axis.SteamDigitalInputType.None;
			this.steamDigitalAction = default(InputDigitalActionHandle_t);
			this.steamDigitalAltAction = default(InputDigitalActionHandle_t);
			this.steamAnalogInput = SteelInput.Axis.SteamAnalogInputType.None;
			this.steamAnalogAction = default(InputAnalogActionHandle_t);
			this.steamAnalogMultiplier = 1;
			this.unityMonitor = default(SteelInput.Axis.InputMonitor);
			this.steamMonitor = default(SteelInput.Axis.InputMonitor);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0009DE34 File Offset: 0x0009C034
		public Axis(string unityAxis, bool invert)
		{
			this.posKeyCode = KeyCode.None;
			this.negKeyCode = KeyCode.None;
			this.useUnityAxis = true;
			this.unityAxisIndex = SteelInput.GetUnityAxisIndex(unityAxis);
			this.invertUnityAxis = invert;
			this.held = false;
			this.pressed = false;
			this.released = false;
			this.unityAxis = null;
			this.applyDeadzone = false;
			this.isMouseAxis = false;
			this.isScrollWheel = false;
			this.steamDigitalInput = SteelInput.Axis.SteamDigitalInputType.None;
			this.steamDigitalAction = default(InputDigitalActionHandle_t);
			this.steamDigitalAltAction = default(InputDigitalActionHandle_t);
			this.steamAnalogInput = SteelInput.Axis.SteamAnalogInputType.None;
			this.steamAnalogAction = default(InputAnalogActionHandle_t);
			this.steamAnalogMultiplier = 1;
			this.unityMonitor = default(SteelInput.Axis.InputMonitor);
			this.steamMonitor = default(SteelInput.Axis.InputMonitor);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000113E1 File Offset: 0x0000F5E1
		public bool HasAnalogBind()
		{
			return this.useUnityAxis || this.HasSteamAnalogAction();
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000113F3 File Offset: 0x0000F5F3
		public bool HasSteamAnalogAction()
		{
			return this.steamAnalogInput > SteelInput.Axis.SteamAnalogInputType.None;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000113FE File Offset: 0x0000F5FE
		public void SetSteamDigitalAction(InputDigitalActionHandle_t handle)
		{
			if (handle.m_InputDigitalActionHandle == 0UL)
			{
				return;
			}
			this.steamDigitalInput = SteelInput.Axis.SteamDigitalInputType.Digital;
			this.steamDigitalAction = handle;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00011417 File Offset: 0x0000F617
		public void SetSteamDigitalActionAlt(InputDigitalActionHandle_t handle, InputDigitalActionHandle_t altHandle)
		{
			if (handle.m_InputDigitalActionHandle == 0UL && altHandle.m_InputDigitalActionHandle == 0UL)
			{
				return;
			}
			this.steamDigitalInput = SteelInput.Axis.SteamDigitalInputType.DigitalAlt;
			this.steamDigitalAction = handle;
			this.steamDigitalAltAction = altHandle;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0001143F File Offset: 0x0000F63F
		public void SetSteamDigitalActionAxis(InputDigitalActionHandle_t handle, InputDigitalActionHandle_t negHandle)
		{
			if (handle.m_InputDigitalActionHandle == 0UL && negHandle.m_InputDigitalActionHandle == 0UL)
			{
				return;
			}
			this.steamDigitalInput = SteelInput.Axis.SteamDigitalInputType.DigitalAxis;
			this.steamDigitalAction = handle;
			this.steamDigitalAltAction = negHandle;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00011467 File Offset: 0x0000F667
		public void SetSteamAnalogAction(InputAnalogActionHandle_t handle, SteelInput.Axis.SteamAnalogInputType type)
		{
			if (handle.m_InputAnalogActionHandle == 0UL)
			{
				return;
			}
			this.steamAnalogInput = type;
			this.steamAnalogAction = handle;
			this.steamAnalogMultiplier = 1;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00011487 File Offset: 0x0000F687
		public void InvertSteamAnalogAction()
		{
			this.steamAnalogMultiplier = -1;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0009DEEC File Offset: 0x0009C0EC
		public bool IsNumberRow()
		{
			int num = (int)this.posKeyCode;
			return num >= 48 && num <= 57;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0009DF10 File Offset: 0x0009C110
		public void Update(bool scrollEnabled)
		{
			bool flag = this.held;
			if (this.isScrollWheel && !scrollEnabled)
			{
				this.held = false;
			}
			else
			{
				this.held = (this.GetValue() > 0f);
			}
			this.pressed = (this.held && !flag);
			this.released = (!this.held && flag);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00011490 File Offset: 0x0000F690
		public bool HasBoundInput()
		{
			return this.useUnityAxis || this.posKeyCode != KeyCode.None || this.negKeyCode > KeyCode.None;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0009DF74 File Offset: 0x0009C174
		public float GetValue()
		{
			float valueUnity = this.GetValueUnity();
			float valueSteam = this.GetValueSteam();
			if (this.steamMonitor.OnChangeTrigger(valueSteam))
			{
				SteelInput.OnValueMonitorTriggered(SteelInput.InputSource.Steam);
			}
			if (this.unityMonitor.OnChangeTrigger(valueUnity))
			{
				SteelInput.OnValueMonitorTriggered(SteelInput.InputSource.Unity);
			}
			return valueUnity + valueSteam;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000114AD File Offset: 0x0000F6AD
		public float GetValueSteam()
		{
			return this.GetValueSteamDigital() + (float)this.steamAnalogMultiplier * this.GetValueSteamAnalog();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0009DFBC File Offset: 0x0009C1BC
		public float GetValueSteamDigital()
		{
			SteamInputWrapper input = GameManager.instance.steamworks.input;
			if (!input.isInitialized || this.steamDigitalInput == SteelInput.Axis.SteamDigitalInputType.None)
			{
				return 0f;
			}
			SteelInput.Axis.SteamDigitalInputType steamDigitalInputType = this.steamDigitalInput;
			if (steamDigitalInputType != SteelInput.Axis.SteamDigitalInputType.DigitalAlt)
			{
				if (steamDigitalInputType == SteelInput.Axis.SteamDigitalInputType.DigitalAxis)
				{
					return (input.ReadDigitalValue(this.steamDigitalAction) ? 1f : 0f) + (input.ReadDigitalValue(this.steamDigitalAltAction) ? -1f : 0f);
				}
				if (!input.ReadDigitalValue(this.steamDigitalAction))
				{
					return 0f;
				}
				return 1f;
			}
			else
			{
				if (!input.ReadDigitalValue(this.steamDigitalAction) && !input.ReadDigitalValue(this.steamDigitalAltAction))
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0009E078 File Offset: 0x0009C278
		public float GetValueSteamAnalog()
		{
			SteamInputWrapper input = GameManager.instance.steamworks.input;
			if (!input.isInitialized || this.steamAnalogInput == SteelInput.Axis.SteamAnalogInputType.None)
			{
				return 0f;
			}
			Vector2 vector = input.ReadAnalogValue(this.steamAnalogAction);
			if (this.steamAnalogInput == SteelInput.Axis.SteamAnalogInputType.PadY)
			{
				return vector.y;
			}
			return -vector.x;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0009E0D0 File Offset: 0x0009C2D0
		public float GetValueUnity()
		{
			if (this.useUnityAxis)
			{
				float num;
				if (this.invertUnityAxis)
				{
					num = -Input.GetAxisRaw(SteelInput.UNITY_AXES[this.unityAxisIndex]);
				}
				else
				{
					num = Input.GetAxisRaw(SteelInput.UNITY_AXES[this.unityAxisIndex]);
				}
				if (!this.applyDeadzone)
				{
					return num;
				}
				if (num > SteelInput.AXIS_BUTTON_DEADZONE || num < -SteelInput.AXIS_BUTTON_DEADZONE)
				{
					return num;
				}
				return 0f;
			}
			else
			{
				if (SteelInput.ignoreNumberRowBinds && this.IsNumberRow())
				{
					return 0f;
				}
				return (Input.GetKey(this.posKeyCode) ? 1f : 0f) - (Input.GetKey(this.negKeyCode) ? 1f : 0f);
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public bool GetButton()
		{
			return this.held;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000114CC File Offset: 0x0000F6CC
		public bool GetButtonDown()
		{
			return this.pressed;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public bool GetButtonUp()
		{
			return this.released;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000114DC File Offset: 0x0000F6DC
		public void BindPositiveKeyCode(KeyCode keyCode)
		{
			this.posKeyCode = keyCode;
			this.useUnityAxis = false;
			this.unityAxisIndex = -1;
			this.invertUnityAxis = false;
			this.isScrollWheel = false;
			this.UpdateBindFlags();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00011507 File Offset: 0x0000F707
		public void BindNegativeKeyCode(KeyCode keyCode)
		{
			this.negKeyCode = keyCode;
			this.useUnityAxis = false;
			this.unityAxisIndex = -1;
			this.invertUnityAxis = false;
			this.isScrollWheel = false;
			this.UpdateBindFlags();
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0009E180 File Offset: 0x0009C380
		public void BindUnityAxis(string unityAxis, bool invert)
		{
			this.unityAxisIndex = SteelInput.GetUnityAxisIndex(unityAxis);
			this.invertUnityAxis = invert;
			this.unityAxis = unityAxis;
			this.useUnityAxis = (this.unityAxisIndex >= 0);
			this.posKeyCode = KeyCode.None;
			this.negKeyCode = KeyCode.None;
			this.UpdateBindFlags();
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0009E1D0 File Offset: 0x0009C3D0
		public void DeserializeAxisString()
		{
			if (this.useUnityAxis)
			{
				if (!string.IsNullOrEmpty(this.unityAxis))
				{
					this.unityAxisIndex = SteelInput.GetUnityAxisIndex(this.unityAxis);
				}
				if (this.unityAxisIndex < 0)
				{
					this.useUnityAxis = false;
					try
					{
						ScriptConsole.instance.LogError("Could not find input unity axis {0}", new object[]
						{
							this.unityAxis
						});
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0009E248 File Offset: 0x0009C448
		public void UpdateBindFlags()
		{
			if (!this.useUnityAxis)
			{
				this.isScrollWheel = false;
				this.applyDeadzone = false;
				this.isMouseAxis = false;
				return;
			}
			string axisName = SteelInput.UNITY_AXES[this.unityAxisIndex];
			this.isScrollWheel = string.Equals(axisName, "Mouse ScrollWheel", StringComparison.InvariantCulture);
			this.applyDeadzone = axisName.Contains("Joy");
			this.isMouseAxis = SteelInput.MOUSE_AXIS_NAMES.Any((string name) => string.Equals(axisName, name, StringComparison.InvariantCulture));
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0009E2D4 File Offset: 0x0009C4D4
		public string PositiveLabel()
		{
			if (this.useUnityAxis)
			{
				return SteelInput.UNITY_AXES[this.unityAxisIndex] + " " + (this.invertUnityAxis ? "-" : "+");
			}
			if (this.posKeyCode == KeyCode.None)
			{
				return "";
			}
			return this.posKeyCode.ToString();
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0009E334 File Offset: 0x0009C534
		public string NegativeLabel()
		{
			if (this.useUnityAxis)
			{
				return SteelInput.UNITY_AXES[this.unityAxisIndex] + " " + (this.invertUnityAxis ? "+" : "-");
			}
			if (this.negKeyCode == KeyCode.None)
			{
				return "";
			}
			return this.negKeyCode.ToString();
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0009E394 File Offset: 0x0009C594
		public KeyboardGlyphGenerator.GlyphData GetGlyph(bool alt)
		{
			if (SteelInput.activeInputSource != SteelInput.InputSource.Unity)
			{
				if (this.steamAnalogInput != SteelInput.Axis.SteamAnalogInputType.None)
				{
					bool flag;
					KeyboardGlyphGenerator.GlyphData analogSteamGlyph = this.GetAnalogSteamGlyph(out flag);
					if (!flag)
					{
						return analogSteamGlyph;
					}
				}
				else if (this.steamDigitalInput != SteelInput.Axis.SteamDigitalInputType.None)
				{
					bool flag2;
					KeyboardGlyphGenerator.GlyphData digitalSteamGlyph = this.GetDigitalSteamGlyph(alt && this.steamDigitalInput != SteelInput.Axis.SteamDigitalInputType.Digital, out flag2);
					if (!flag2)
					{
						return digitalSteamGlyph;
					}
				}
				return new KeyboardGlyphGenerator.GlyphData(KeyCode.None);
			}
			if (this.useUnityAxis)
			{
				return this.GetUnityAxisGlyph();
			}
			return new KeyboardGlyphGenerator.GlyphData((alt && this.negKeyCode != KeyCode.None) ? this.negKeyCode : this.posKeyCode);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0009E41C File Offset: 0x0009C61C
		private KeyboardGlyphGenerator.GlyphData GetUnityAxisGlyph()
		{
			if (this.isScrollWheel)
			{
				return new KeyboardGlyphGenerator.GlyphData(KeyboardGlyphGenerator.instance.database.mouseScroll);
			}
			if (this.isMouseAxis)
			{
				return new KeyboardGlyphGenerator.GlyphData(KeyboardGlyphGenerator.instance.database.mouseMove);
			}
			string text = SteelInput.UNITY_AXES[this.unityAxisIndex];
			text = text.TrimStart(new char[]
			{
				'J',
				'o',
				'y'
			});
			text = text.TrimEnd(new char[]
			{
				'J',
				'o',
				'y'
			});
			return new KeyboardGlyphGenerator.GlyphData(KeyboardGlyphGenerator.instance.database.gamepad)
			{
				label = text
			};
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00011532 File Offset: 0x0000F732
		private KeyboardGlyphGenerator.GlyphData GetDigitalSteamGlyph(bool alt, out bool isUnbound)
		{
			return new KeyboardGlyphGenerator.GlyphData(GameManager.instance.steamworks.input.GetActionGlyphFromActiveSet(alt ? this.steamDigitalAltAction : this.steamDigitalAction, out isUnbound));
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0001155F File Offset: 0x0000F75F
		private KeyboardGlyphGenerator.GlyphData GetAnalogSteamGlyph(out bool isUnbound)
		{
			return new KeyboardGlyphGenerator.GlyphData(GameManager.instance.steamworks.input.GetActionGlyphFromActiveSet(this.steamAnalogAction, out isUnbound));
		}

		// Token: 0x04001836 RID: 6198
		public KeyCode posKeyCode;

		// Token: 0x04001837 RID: 6199
		public KeyCode negKeyCode;

		// Token: 0x04001838 RID: 6200
		public string unityAxis;

		// Token: 0x04001839 RID: 6201
		public bool useUnityAxis;

		// Token: 0x0400183A RID: 6202
		public bool invertUnityAxis;

		// Token: 0x0400183B RID: 6203
		public bool applyDeadzone;

		// Token: 0x0400183C RID: 6204
		private SteelInput.Axis.InputMonitor unityMonitor;

		// Token: 0x0400183D RID: 6205
		private SteelInput.Axis.InputMonitor steamMonitor;

		// Token: 0x0400183E RID: 6206
		private int unityAxisIndex;

		// Token: 0x0400183F RID: 6207
		[NonSerialized]
		public bool isMouseAxis;

		// Token: 0x04001840 RID: 6208
		private bool isScrollWheel;

		// Token: 0x04001841 RID: 6209
		private bool pressed;

		// Token: 0x04001842 RID: 6210
		private bool held;

		// Token: 0x04001843 RID: 6211
		private bool released;

		// Token: 0x04001844 RID: 6212
		public SteelInput.Axis.SteamDigitalInputType steamDigitalInput;

		// Token: 0x04001845 RID: 6213
		public SteelInput.Axis.SteamAnalogInputType steamAnalogInput;

		// Token: 0x04001846 RID: 6214
		[XmlIgnore]
		private InputDigitalActionHandle_t steamDigitalAction;

		// Token: 0x04001847 RID: 6215
		[XmlIgnore]
		private InputDigitalActionHandle_t steamDigitalAltAction;

		// Token: 0x04001848 RID: 6216
		[XmlIgnore]
		private InputAnalogActionHandle_t steamAnalogAction;

		// Token: 0x04001849 RID: 6217
		private sbyte steamAnalogMultiplier;

		// Token: 0x02000357 RID: 855
		public enum SteamDigitalInputType
		{
			// Token: 0x0400184B RID: 6219
			None,
			// Token: 0x0400184C RID: 6220
			Digital,
			// Token: 0x0400184D RID: 6221
			DigitalAlt,
			// Token: 0x0400184E RID: 6222
			DigitalAxis
		}

		// Token: 0x02000358 RID: 856
		public enum SteamAnalogInputType
		{
			// Token: 0x04001850 RID: 6224
			None,
			// Token: 0x04001851 RID: 6225
			Analog,
			// Token: 0x04001852 RID: 6226
			PadX,
			// Token: 0x04001853 RID: 6227
			PadY
		}

		// Token: 0x02000359 RID: 857
		private struct InputMonitor
		{
			// Token: 0x060015E6 RID: 5606 RVA: 0x00011581 File Offset: 0x0000F781
			public bool OnChangeTrigger(float newValue)
			{
				if (Mathf.Abs(newValue - this.trigValue) > 0.01f)
				{
					this.trigValue = newValue;
					return true;
				}
				return false;
			}

			// Token: 0x04001854 RID: 6228
			private const float DEADZONE = 0.01f;

			// Token: 0x04001855 RID: 6229
			private float trigValue;
		}
	}
}
