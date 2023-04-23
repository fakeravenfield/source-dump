using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000263 RID: 611
public class SteamInputWrapper
{
	// Token: 0x060010AD RID: 4269 RVA: 0x0008A66C File Offset: 0x0008886C
	public bool Initialize()
	{
		this.isInitialized = SteamInput.Init();
		Debug.LogFormat("Initializing SteamInput, success={0}", new object[]
		{
			this.isInitialized
		});
		if (this.isInitialized)
		{
			this.analogInput = new Dictionary<InputAnalogActionHandle_t, InputAnalogActionData_t>();
			this.digitalInput = new Dictionary<InputDigitalActionHandle_t, InputDigitalActionData_t>();
			this.analogInput.Add(new InputAnalogActionHandle_t(0UL), default(InputAnalogActionData_t));
			this.digitalInput.Add(new InputDigitalActionHandle_t(0UL), default(InputDigitalActionData_t));
			this.RegisterHandles();
			this.allAnalogActionHandles = this.analogInput.Keys.ToArray<InputAnalogActionHandle_t>();
			this.allDigitalActionHandles = this.digitalInput.Keys.ToArray<InputDigitalActionHandle_t>();
			this.SetUpInputHandle();
			this.UpdateGameState(SteamInputWrapper.ActionSet.Menu, SteamInputWrapper.VehicleLayer.None, true);
		}
		return this.isInitialized;
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0008A740 File Offset: 0x00088940
	private void SetUpInputHandle()
	{
		SteamInput.RunFrame();
		InputHandle_t[] array = new InputHandle_t[16];
		if (SteamInput.GetConnectedControllers(array) > 0)
		{
			this.activeInputHandle = array[0];
		}
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0000D539 File Offset: 0x0000B739
	public void Shutdown()
	{
		SteamInput.Shutdown();
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0008A770 File Offset: 0x00088970
	public void PollInputStatus()
	{
		foreach (InputDigitalActionHandle_t inputDigitalActionHandle_t in this.allDigitalActionHandles)
		{
			this.digitalInput[inputDigitalActionHandle_t] = SteamInput.GetDigitalActionData(this.activeInputHandle, inputDigitalActionHandle_t);
		}
		foreach (InputAnalogActionHandle_t inputAnalogActionHandle_t in this.allAnalogActionHandles)
		{
			this.analogInput[inputAnalogActionHandle_t] = SteamInput.GetAnalogActionData(this.activeInputHandle, inputAnalogActionHandle_t);
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0008A7EC File Offset: 0x000889EC
	private InputAnalogActionHandle_t RegisterAnalogHandle(string name)
	{
		InputAnalogActionHandle_t analogActionHandle = SteamInput.GetAnalogActionHandle(name);
		if (analogActionHandle.m_InputAnalogActionHandle != 0UL)
		{
			this.analogInput.Add(analogActionHandle, default(InputAnalogActionData_t));
		}
		return analogActionHandle;
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0008A820 File Offset: 0x00088A20
	private InputDigitalActionHandle_t RegisterDigitalHandle(string name)
	{
		InputDigitalActionHandle_t digitalActionHandle = SteamInput.GetDigitalActionHandle(name);
		if (digitalActionHandle.m_InputDigitalActionHandle != 0UL)
		{
			this.digitalInput.Add(digitalActionHandle, default(InputDigitalActionData_t));
		}
		return digitalActionHandle;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0008A854 File Offset: 0x00088A54
	private void RegisterHandles()
	{
		this.infantrySet = SteamInput.GetActionSetHandle("Infantry");
		this.vehicleSet = SteamInput.GetActionSetHandle("Vehicle");
		this.menuSet = SteamInput.GetActionSetHandle("Menu");
		this.carSet = SteamInput.GetActionSetHandle("Car");
		this.airplaneSet = SteamInput.GetActionSetHandle("Airplane");
		this.helicopterSet = SteamInput.GetActionSetHandle("Helicopter");
		this.move = this.RegisterAnalogHandle("Move");
		this.camera = this.RegisterAnalogHandle("Camera");
		this.lean = this.RegisterAnalogHandle("Lean");
		this.vehicleSteer = this.RegisterAnalogHandle("VehicleSteer");
		this.throttle = this.RegisterAnalogHandle("Throttle");
		this.aircraftYaw = this.RegisterAnalogHandle("AircraftYaw");
		this.menuMove = this.RegisterAnalogHandle("MenuMove");
		this.jump = this.RegisterDigitalHandle("Jump");
		this.crouch = this.RegisterDigitalHandle("Crouch");
		this.prone = this.RegisterDigitalHandle("Prone");
		this.sprint = this.RegisterDigitalHandle("Sprint");
		this.fire = this.RegisterDigitalHandle("Fire");
		this.use = this.RegisterDigitalHandle("Use");
		this.aim = this.RegisterDigitalHandle("Aim");
		this.reload = this.RegisterDigitalHandle("Reload");
		this.kick = this.RegisterDigitalHandle("Kick");
		this.leanLeft = this.RegisterDigitalHandle("LeanLeft");
		this.leanRight = this.RegisterDigitalHandle("LeanRight");
		this.firemode = this.RegisterDigitalHandle("Firemode");
		this.nextWeapon = this.RegisterDigitalHandle("NextWeapon");
		this.prevWeapon = this.RegisterDigitalHandle("PrevWeapon");
		this.nextScope = this.RegisterDigitalHandle("NextScope");
		this.prevScope = this.RegisterDigitalHandle("PrevScope");
		this.weapon1 = this.RegisterDigitalHandle("Weapon1");
		this.weapon2 = this.RegisterDigitalHandle("Weapon2");
		this.weapon3 = this.RegisterDigitalHandle("Weapon3");
		this.weapon4 = this.RegisterDigitalHandle("Weapon4");
		this.weapon5 = this.RegisterDigitalHandle("Weapon5");
		this.squadOrder = this.RegisterDigitalHandle("SquadOrder");
		this.squadMap = this.RegisterDigitalHandle("SquadMap");
		this.thirdperson = this.RegisterDigitalHandle("ThirdPerson");
		this.countermeasures = this.RegisterDigitalHandle("Countermeasures");
		this.seat1 = this.RegisterDigitalHandle("Seat1");
		this.seat2 = this.RegisterDigitalHandle("Seat2");
		this.seat3 = this.RegisterDigitalHandle("Seat3");
		this.seat4 = this.RegisterDigitalHandle("Seat4");
		this.seat5 = this.RegisterDigitalHandle("Seat5");
		this.seat6 = this.RegisterDigitalHandle("Seat6");
		this.seat7 = this.RegisterDigitalHandle("Seat7");
		this.menu = this.RegisterDigitalHandle("Menu");
		this.slowmotion = this.RegisterDigitalHandle("Slowmotion");
		this.nightvision = this.RegisterDigitalHandle("NightVision");
		this.minimap = this.RegisterDigitalHandle("Minimap");
		this.scoreboard = this.RegisterDigitalHandle("Scoreboard");
		this.loadout = this.RegisterDigitalHandle("Loadout");
		this.throttleUp = this.RegisterDigitalHandle("ThrottleUp");
		this.throttleDown = this.RegisterDigitalHandle("ThrottleDown");
		this.autoHover = this.RegisterDigitalHandle("HelicopterAutoHover");
		this.menuUp = this.RegisterDigitalHandle("MenuUp");
		this.menuDown = this.RegisterDigitalHandle("MenuDown");
		this.menuLeft = this.RegisterDigitalHandle("MenuLeft");
		this.menuRight = this.RegisterDigitalHandle("MenuRight");
		this.menuSelect = this.RegisterDigitalHandle("MenuSelect");
		this.menuCancel = this.RegisterDigitalHandle("MenuCancel");
		this.menuReturn = this.RegisterDigitalHandle("MenuReturn");
		this.loadoutClose = this.RegisterDigitalHandle("LoadoutClose");
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0008AC7C File Offset: 0x00088E7C
	public void UpdateGameState(SteamInputWrapper.ActionSet actionSet, SteamInputWrapper.VehicleLayer vehicleLayer, bool force = false)
	{
		if (force || this.currentActionSet != actionSet)
		{
			this.currentActionSet = actionSet;
			switch (this.currentActionSet)
			{
			case SteamInputWrapper.ActionSet.Menu:
				this.ActivateActionSet(this.menuSet);
				break;
			case SteamInputWrapper.ActionSet.Infantry:
				this.ActivateActionSet(this.infantrySet);
				break;
			case SteamInputWrapper.ActionSet.Vehicle:
				this.ActivateActionSet(this.vehicleSet);
				break;
			}
		}
		if (force || this.currentVehicleLayer != vehicleLayer)
		{
			this.currentVehicleLayer = vehicleLayer;
			SteamInput.DeactivateAllActionSetLayers(this.activeInputHandle);
			switch (this.currentVehicleLayer)
			{
			case SteamInputWrapper.VehicleLayer.Car:
				this.ActivateActionSetLayer(this.carSet);
				return;
			case SteamInputWrapper.VehicleLayer.Airplane:
				this.ActivateActionSetLayer(this.airplaneSet);
				break;
			case SteamInputWrapper.VehicleLayer.Helicopter:
				this.ActivateActionSetLayer(this.helicopterSet);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0000D541 File Offset: 0x0000B741
	private void ActivateActionSet(InputActionSetHandle_t setHandle)
	{
		this.currentActionSetHandle = setHandle;
		SteamInput.ActivateActionSet(this.activeInputHandle, setHandle);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0000D556 File Offset: 0x0000B756
	private void ActivateActionSetLayer(InputActionSetHandle_t setHandle)
	{
		this.currentActionSetLayerHandle = setHandle;
		SteamInput.ActivateActionSetLayer(this.activeInputHandle, setHandle);
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0000D56B File Offset: 0x0000B76B
	private InputActionSetHandle_t GetActiveActionSetHandle()
	{
		if (this.currentVehicleLayer != SteamInputWrapper.VehicleLayer.None)
		{
			return this.currentActionSetLayerHandle;
		}
		return this.currentActionSetHandle;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0008AD40 File Offset: 0x00088F40
	public Texture2D GetActionGlyphFromActiveSet(InputDigitalActionHandle_t handle, out bool isUnbound)
	{
		SteamInput.GetDigitalActionOrigins(this.activeInputHandle, this.GetActiveActionSetHandle(), handle, this.origins);
		EInputActionOrigin einputActionOrigin = this.origins[0];
		isUnbound = (einputActionOrigin == EInputActionOrigin.k_EInputActionOrigin_None);
		if (isUnbound)
		{
			return KeyboardGlyphGenerator.instance.database.unbound;
		}
		string glyphForActionOrigin = SteamInput.GetGlyphForActionOrigin(einputActionOrigin);
		Texture2D texture2D = new Texture2D(2, 2);
		GameManager.instance.StartCoroutine(this.LoadControllerGlyph(glyphForActionOrigin, texture2D));
		return texture2D;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0008ADAC File Offset: 0x00088FAC
	public Texture2D GetActionGlyphFromActiveSet(InputAnalogActionHandle_t handle, out bool isUnbound)
	{
		SteamInput.GetAnalogActionOrigins(this.activeInputHandle, this.GetActiveActionSetHandle(), handle, this.origins);
		EInputActionOrigin einputActionOrigin = this.origins[0];
		isUnbound = (einputActionOrigin == EInputActionOrigin.k_EInputActionOrigin_None);
		if (isUnbound)
		{
			return KeyboardGlyphGenerator.instance.database.unbound;
		}
		string glyphForActionOrigin = SteamInput.GetGlyphForActionOrigin(einputActionOrigin);
		Texture2D texture2D = new Texture2D(2, 2);
		GameManager.instance.StartCoroutine(this.LoadControllerGlyph(glyphForActionOrigin, texture2D));
		return texture2D;
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0000D582 File Offset: 0x0000B782
	private IEnumerator LoadControllerGlyph(string path, Texture2D destinationTexture)
	{
		UnityWebRequest request = UnityWebRequest.Get(path);
		yield return request.SendWebRequest();
		if (request.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError(string.Format("Controller Glyph Request failed: {0}: {1} - path={2}", request.result, request.error, path));
		}
		else
		{
			try
			{
				destinationTexture.LoadImage(request.downloadHandler.data);
				yield break;
			}
			catch (Exception exception)
			{
				Debug.LogError("Could not load glyph at " + path);
				Debug.LogException(exception);
				yield break;
			}
		}
		yield break;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0008AE18 File Offset: 0x00089018
	public bool ReadDigitalValue(InputDigitalActionHandle_t handle)
	{
		InputDigitalActionData_t inputDigitalActionData_t = this.digitalInput[handle];
		return inputDigitalActionData_t.bActive > 0 && inputDigitalActionData_t.bState > 0;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0008AE48 File Offset: 0x00089048
	public Vector2 ReadAnalogValue(InputAnalogActionHandle_t handle)
	{
		InputAnalogActionData_t inputAnalogActionData_t = this.analogInput[handle];
		if (inputAnalogActionData_t.eMode == EInputSourceMode.k_EInputSourceMode_AbsoluteMouse)
		{
			inputAnalogActionData_t.x *= 0.01f;
			inputAnalogActionData_t.y *= 0.01f;
		}
		if (inputAnalogActionData_t.bActive <= 0)
		{
			return Vector2.zero;
		}
		return new Vector2(inputAnalogActionData_t.x, inputAnalogActionData_t.y);
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0008AEAC File Offset: 0x000890AC
	private static void Log(string prefix, InputAnalogActionData_t analogData)
	{
		Debug.LogFormat("{4}: ({0:0.00}, {1:0.00}), Mode={2}, Active={3}", new object[]
		{
			analogData.x,
			analogData.y,
			analogData.eMode,
			analogData.bActive,
			prefix
		});
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0000D598 File Offset: 0x0000B798
	private static void Log(string prefix, InputDigitalActionData_t digitalData)
	{
		Debug.LogFormat("{2}: State={0}, Active={1}", new object[]
		{
			digitalData.bState,
			digitalData.bActive,
			prefix
		});
	}

	// Token: 0x040011E6 RID: 4582
	public bool isInitialized;

	// Token: 0x040011E7 RID: 4583
	private InputHandle_t activeInputHandle;

	// Token: 0x040011E8 RID: 4584
	private Dictionary<InputAnalogActionHandle_t, InputAnalogActionData_t> analogInput;

	// Token: 0x040011E9 RID: 4585
	private Dictionary<InputDigitalActionHandle_t, InputDigitalActionData_t> digitalInput;

	// Token: 0x040011EA RID: 4586
	private InputAnalogActionHandle_t[] allAnalogActionHandles;

	// Token: 0x040011EB RID: 4587
	private InputDigitalActionHandle_t[] allDigitalActionHandles;

	// Token: 0x040011EC RID: 4588
	private SteamInputWrapper.ActionSet currentActionSet;

	// Token: 0x040011ED RID: 4589
	private InputActionSetHandle_t currentActionSetHandle;

	// Token: 0x040011EE RID: 4590
	private InputActionSetHandle_t currentActionSetLayerHandle;

	// Token: 0x040011EF RID: 4591
	private SteamInputWrapper.VehicleLayer currentVehicleLayer;

	// Token: 0x040011F0 RID: 4592
	public InputActionSetHandle_t infantrySet;

	// Token: 0x040011F1 RID: 4593
	public InputActionSetHandle_t vehicleSet;

	// Token: 0x040011F2 RID: 4594
	public InputActionSetHandle_t carSet;

	// Token: 0x040011F3 RID: 4595
	public InputActionSetHandle_t airplaneSet;

	// Token: 0x040011F4 RID: 4596
	public InputActionSetHandle_t helicopterSet;

	// Token: 0x040011F5 RID: 4597
	public InputAnalogActionHandle_t move;

	// Token: 0x040011F6 RID: 4598
	public InputAnalogActionHandle_t camera;

	// Token: 0x040011F7 RID: 4599
	public InputAnalogActionHandle_t lean;

	// Token: 0x040011F8 RID: 4600
	public InputAnalogActionHandle_t vehicleSteer;

	// Token: 0x040011F9 RID: 4601
	public InputAnalogActionHandle_t throttle;

	// Token: 0x040011FA RID: 4602
	public InputAnalogActionHandle_t aircraftYaw;

	// Token: 0x040011FB RID: 4603
	public InputDigitalActionHandle_t jump;

	// Token: 0x040011FC RID: 4604
	public InputDigitalActionHandle_t crouch;

	// Token: 0x040011FD RID: 4605
	public InputDigitalActionHandle_t prone;

	// Token: 0x040011FE RID: 4606
	public InputDigitalActionHandle_t sprint;

	// Token: 0x040011FF RID: 4607
	public InputDigitalActionHandle_t fire;

	// Token: 0x04001200 RID: 4608
	public InputDigitalActionHandle_t use;

	// Token: 0x04001201 RID: 4609
	public InputDigitalActionHandle_t aim;

	// Token: 0x04001202 RID: 4610
	public InputDigitalActionHandle_t reload;

	// Token: 0x04001203 RID: 4611
	public InputDigitalActionHandle_t kick;

	// Token: 0x04001204 RID: 4612
	public InputDigitalActionHandle_t leanLeft;

	// Token: 0x04001205 RID: 4613
	public InputDigitalActionHandle_t leanRight;

	// Token: 0x04001206 RID: 4614
	public InputDigitalActionHandle_t firemode;

	// Token: 0x04001207 RID: 4615
	public InputDigitalActionHandle_t nextWeapon;

	// Token: 0x04001208 RID: 4616
	public InputDigitalActionHandle_t prevWeapon;

	// Token: 0x04001209 RID: 4617
	public InputDigitalActionHandle_t nextScope;

	// Token: 0x0400120A RID: 4618
	public InputDigitalActionHandle_t prevScope;

	// Token: 0x0400120B RID: 4619
	public InputDigitalActionHandle_t weapon1;

	// Token: 0x0400120C RID: 4620
	public InputDigitalActionHandle_t weapon2;

	// Token: 0x0400120D RID: 4621
	public InputDigitalActionHandle_t weapon3;

	// Token: 0x0400120E RID: 4622
	public InputDigitalActionHandle_t weapon4;

	// Token: 0x0400120F RID: 4623
	public InputDigitalActionHandle_t weapon5;

	// Token: 0x04001210 RID: 4624
	public InputDigitalActionHandle_t squadOrder;

	// Token: 0x04001211 RID: 4625
	public InputDigitalActionHandle_t squadMap;

	// Token: 0x04001212 RID: 4626
	public InputDigitalActionHandle_t thirdperson;

	// Token: 0x04001213 RID: 4627
	public InputDigitalActionHandle_t countermeasures;

	// Token: 0x04001214 RID: 4628
	public InputDigitalActionHandle_t seat1;

	// Token: 0x04001215 RID: 4629
	public InputDigitalActionHandle_t seat2;

	// Token: 0x04001216 RID: 4630
	public InputDigitalActionHandle_t seat3;

	// Token: 0x04001217 RID: 4631
	public InputDigitalActionHandle_t seat4;

	// Token: 0x04001218 RID: 4632
	public InputDigitalActionHandle_t seat5;

	// Token: 0x04001219 RID: 4633
	public InputDigitalActionHandle_t seat6;

	// Token: 0x0400121A RID: 4634
	public InputDigitalActionHandle_t seat7;

	// Token: 0x0400121B RID: 4635
	public InputDigitalActionHandle_t menu;

	// Token: 0x0400121C RID: 4636
	public InputDigitalActionHandle_t slowmotion;

	// Token: 0x0400121D RID: 4637
	public InputDigitalActionHandle_t nightvision;

	// Token: 0x0400121E RID: 4638
	public InputDigitalActionHandle_t minimap;

	// Token: 0x0400121F RID: 4639
	public InputDigitalActionHandle_t scoreboard;

	// Token: 0x04001220 RID: 4640
	public InputDigitalActionHandle_t loadout;

	// Token: 0x04001221 RID: 4641
	public InputDigitalActionHandle_t throttleUp;

	// Token: 0x04001222 RID: 4642
	public InputDigitalActionHandle_t throttleDown;

	// Token: 0x04001223 RID: 4643
	public InputDigitalActionHandle_t autoHover;

	// Token: 0x04001224 RID: 4644
	public InputActionSetHandle_t menuSet;

	// Token: 0x04001225 RID: 4645
	public InputAnalogActionHandle_t menuMove;

	// Token: 0x04001226 RID: 4646
	public InputDigitalActionHandle_t menuUp;

	// Token: 0x04001227 RID: 4647
	public InputDigitalActionHandle_t menuDown;

	// Token: 0x04001228 RID: 4648
	public InputDigitalActionHandle_t menuLeft;

	// Token: 0x04001229 RID: 4649
	public InputDigitalActionHandle_t menuRight;

	// Token: 0x0400122A RID: 4650
	public InputDigitalActionHandle_t menuSelect;

	// Token: 0x0400122B RID: 4651
	public InputDigitalActionHandle_t menuCancel;

	// Token: 0x0400122C RID: 4652
	public InputDigitalActionHandle_t menuReturn;

	// Token: 0x0400122D RID: 4653
	public InputDigitalActionHandle_t loadoutClose;

	// Token: 0x0400122E RID: 4654
	private EInputActionOrigin[] origins = new EInputActionOrigin[8];

	// Token: 0x02000264 RID: 612
	public enum ActionSet
	{
		// Token: 0x04001230 RID: 4656
		Menu,
		// Token: 0x04001231 RID: 4657
		Infantry,
		// Token: 0x04001232 RID: 4658
		Vehicle
	}

	// Token: 0x02000265 RID: 613
	public enum VehicleLayer
	{
		// Token: 0x04001234 RID: 4660
		None,
		// Token: 0x04001235 RID: 4661
		Car,
		// Token: 0x04001236 RID: 4662
		Airplane,
		// Token: 0x04001237 RID: 4663
		Helicopter
	}
}
