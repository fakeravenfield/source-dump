using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009CE RID: 2510
	[Proxy(typeof(Input))]
	public class InputProxy : IProxy
	{
		// Token: 0x060045F2 RID: 17906 RVA: 0x00030C57 File Offset: 0x0002EE57
		[MoonSharpHidden]
		public InputProxy(Input value)
		{
			this._value = value;
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00030C66 File Offset: 0x0002EE66
		public InputProxy()
		{
			this._value = new Input();
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x00030C79 File Offset: 0x0002EE79
		public static Vector3Proxy acceleration
		{
			get
			{
				return Vector3Proxy.New(Input.acceleration);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x00030C85 File Offset: 0x0002EE85
		public static int accelerationEventCount
		{
			get
			{
				return Input.accelerationEventCount;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x00030C8C File Offset: 0x0002EE8C
		public static bool anyKey
		{
			get
			{
				return Input.anyKey;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x00030C93 File Offset: 0x0002EE93
		public static bool anyKeyDown
		{
			get
			{
				return Input.anyKeyDown;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x00030C9A File Offset: 0x0002EE9A
		// (set) Token: 0x060045F9 RID: 17913 RVA: 0x00030CA1 File Offset: 0x0002EEA1
		public static bool backButtonLeavesApp
		{
			get
			{
				return Input.backButtonLeavesApp;
			}
			set
			{
				Input.backButtonLeavesApp = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x00030CA9 File Offset: 0x0002EEA9
		// (set) Token: 0x060045FB RID: 17915 RVA: 0x00030CB0 File Offset: 0x0002EEB0
		public static bool compensateSensors
		{
			get
			{
				return Input.compensateSensors;
			}
			set
			{
				Input.compensateSensors = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		// (set) Token: 0x060045FD RID: 17917 RVA: 0x00030CC4 File Offset: 0x0002EEC4
		public static Vector2Proxy compositionCursorPos
		{
			get
			{
				return Vector2Proxy.New(Input.compositionCursorPos);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				Input.compositionCursorPos = value._value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x00030CDF File Offset: 0x0002EEDF
		public static string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x00030CE6 File Offset: 0x0002EEE6
		public static bool imeIsSelected
		{
			get
			{
				return Input.imeIsSelected;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x00030CED File Offset: 0x0002EEED
		public static string inputString
		{
			get
			{
				return Input.inputString;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		public static Vector3Proxy mousePosition
		{
			get
			{
				return Vector3Proxy.New(Input.mousePosition);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x00030D00 File Offset: 0x0002EF00
		public static bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x00030D07 File Offset: 0x0002EF07
		public static Vector2Proxy mouseScrollDelta
		{
			get
			{
				return Vector2Proxy.New(Input.mouseScrollDelta);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x00030D13 File Offset: 0x0002EF13
		// (set) Token: 0x06004605 RID: 17925 RVA: 0x00030D1A File Offset: 0x0002EF1A
		public static bool multiTouchEnabled
		{
			get
			{
				return Input.multiTouchEnabled;
			}
			set
			{
				Input.multiTouchEnabled = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x00030D22 File Offset: 0x0002EF22
		// (set) Token: 0x06004607 RID: 17927 RVA: 0x00030D29 File Offset: 0x0002EF29
		public static bool simulateMouseWithTouches
		{
			get
			{
				return Input.simulateMouseWithTouches;
			}
			set
			{
				Input.simulateMouseWithTouches = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x00030D31 File Offset: 0x0002EF31
		public static bool stylusTouchSupported
		{
			get
			{
				return Input.stylusTouchSupported;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x00030D38 File Offset: 0x0002EF38
		public static int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x00030D3F File Offset: 0x0002EF3F
		public static bool touchPressureSupported
		{
			get
			{
				return Input.touchPressureSupported;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x00030D46 File Offset: 0x0002EF46
		public static bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x00030D4D File Offset: 0x0002EF4D
		public static float mouseSensitivity
		{
			get
			{
				return WInput.GetMouseSensitivity();
			}
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00030D54 File Offset: 0x0002EF54
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00130D54 File Offset: 0x0012EF54
		[MoonSharpHidden]
		public static InputProxy New(Input value)
		{
			if (value == null)
			{
				return null;
			}
			InputProxy inputProxy = (InputProxy)ObjectCache.Get(typeof(InputProxy), value);
			if (inputProxy == null)
			{
				inputProxy = new InputProxy(value);
				ObjectCache.Add(typeof(InputProxy), value, inputProxy);
			}
			return inputProxy;
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00030D5C File Offset: 0x0002EF5C
		[MoonSharpUserDataMetamethod("__call")]
		public static InputProxy Call(DynValue _)
		{
			return new InputProxy();
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00030D63 File Offset: 0x0002EF63
		public static float GetAxis(string axisName)
		{
			return Input.GetAxis(axisName);
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x00030D6B File Offset: 0x0002EF6B
		public static float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00030D73 File Offset: 0x0002EF73
		public static bool GetButton(string buttonName)
		{
			return Input.GetButton(buttonName);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x00030D7B File Offset: 0x0002EF7B
		public static bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x00030D83 File Offset: 0x0002EF83
		public static bool GetButtonUp(string buttonName)
		{
			return Input.GetButtonUp(buttonName);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00030D8B File Offset: 0x0002EF8B
		public static string[] GetJoystickNames()
		{
			return Input.GetJoystickNames();
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x00030D92 File Offset: 0x0002EF92
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKey(key);
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x00030D9A File Offset: 0x0002EF9A
		public static bool GetKey(string name)
		{
			return Input.GetKey(name);
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x00030DA2 File Offset: 0x0002EFA2
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x00030DAA File Offset: 0x0002EFAA
		public static bool GetKeyDown(string name)
		{
			return Input.GetKeyDown(name);
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x00030DB2 File Offset: 0x0002EFB2
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x00030DBA File Offset: 0x0002EFBA
		public static bool GetKeyUp(string name)
		{
			return Input.GetKeyUp(name);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x00030DC2 File Offset: 0x0002EFC2
		public static bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x00030DCA File Offset: 0x0002EFCA
		public static bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x00030DD2 File Offset: 0x0002EFD2
		public static bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x00030DDA File Offset: 0x0002EFDA
		public static void ResetInputAxes()
		{
			Input.ResetInputAxes();
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x00030DE1 File Offset: 0x0002EFE1
		public static void DisableNumberRowInputs()
		{
			WInput.DisableNumberRowInputs();
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x00030DE8 File Offset: 0x0002EFE8
		public static void EnableNumberRowInputs()
		{
			WInput.EnableNumberRowInputs();
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x00030DEF File Offset: 0x0002EFEF
		public static float GetKeyBindAxis(SteelInput.KeyBinds keyBind)
		{
			return WInput.GetKeyBindAxis(keyBind);
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x00030DF7 File Offset: 0x0002EFF7
		public static bool GetKeyBindButton(SteelInput.KeyBinds keyBind)
		{
			return WInput.GetKeyBindButton(keyBind);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00030DFF File Offset: 0x0002EFFF
		public static bool GetKeyBindButtonDown(SteelInput.KeyBinds keyBind)
		{
			return WInput.GetKeyBindButtonDown(keyBind);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00030E07 File Offset: 0x0002F007
		public static bool GetKeyBindButtonUp(SteelInput.KeyBinds keyBind)
		{
			return WInput.GetKeyBindButtonUp(keyBind);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x00030E0F File Offset: 0x0002F00F
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003166 RID: 12646
		[MoonSharpHidden]
		public Input _value;
	}
}
