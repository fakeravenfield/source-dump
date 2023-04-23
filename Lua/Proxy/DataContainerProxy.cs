using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using Ravenfield.Mods.Data;
using UnityEngine;
using UnityEngine.Video;

namespace Lua.Proxy
{
	// Token: 0x020009C0 RID: 2496
	[Proxy(typeof(DataContainer))]
	public class DataContainerProxy : IProxy
	{
		// Token: 0x060043ED RID: 17389 RVA: 0x0002F153 File Offset: 0x0002D353
		[MoonSharpHidden]
		public DataContainerProxy(DataContainer value)
		{
			this._value = value;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x0002F162 File Offset: 0x0002D362
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x0002F174 File Offset: 0x0002D374
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x0002F186 File Offset: 0x0002D386
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x001303EC File Offset: 0x0012E5EC
		[MoonSharpHidden]
		public static DataContainerProxy New(DataContainer value)
		{
			if (value == null)
			{
				return null;
			}
			DataContainerProxy dataContainerProxy = (DataContainerProxy)ObjectCache.Get(typeof(DataContainerProxy), value);
			if (dataContainerProxy == null)
			{
				dataContainerProxy = new DataContainerProxy(value);
				ObjectCache.Add(typeof(DataContainerProxy), value, dataContainerProxy);
			}
			return dataContainerProxy;
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x0002F18E File Offset: 0x0002D38E
		public ActorSkinProxy GetActorSkin(string id)
		{
			return ActorSkinProxy.New(this._value.GetActorSkin(id));
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x0002F1A1 File Offset: 0x0002D3A1
		public ActorSkin[] GetActorSkinArray(string id)
		{
			return this._value.GetActorSkinArray(id);
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x0002F1AF File Offset: 0x0002D3AF
		public AnimationCurveProxy GetAnimationCurve(string id)
		{
			return AnimationCurveProxy.New(this._value.GetAnimationCurve(id));
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x0002F1C2 File Offset: 0x0002D3C2
		public AnimationCurve[] GetAnimationCurveArray(string id)
		{
			return this._value.GetAnimationCurveArray(id);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x0002F1D0 File Offset: 0x0002D3D0
		public AudioClipProxy GetAudioClip(string id)
		{
			return AudioClipProxy.New(this._value.GetAudioClip(id));
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x0002F1E3 File Offset: 0x0002D3E3
		public AudioClip[] GetAudioClipArray(string id)
		{
			return this._value.GetAudioClipArray(id);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x0002F1F1 File Offset: 0x0002D3F1
		public bool GetBool(string id)
		{
			return this._value.GetBool(id);
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x0002F1FF File Offset: 0x0002D3FF
		public bool[] GetBoolArray(string id)
		{
			return this._value.GetBoolArray(id);
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x0002F20D File Offset: 0x0002D40D
		public ColorProxy GetColor(string id)
		{
			return ColorProxy.New(this._value.GetColor(id));
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x0002F220 File Offset: 0x0002D420
		public Color[] GetColorArray(string id)
		{
			return this._value.GetColorArray(id);
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x0002F22E File Offset: 0x0002D42E
		public float GetFloat(string id)
		{
			return this._value.GetFloat(id);
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x0002F23C File Offset: 0x0002D43C
		public float[] GetFloatArray(string id)
		{
			return this._value.GetFloatArray(id);
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x0002F24A File Offset: 0x0002D44A
		public GameObjectProxy GetGameObject(string id)
		{
			return GameObjectProxy.New(this._value.GetGameObject(id));
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x0002F25D File Offset: 0x0002D45D
		public GameObject[] GetGameObjectArray(string id)
		{
			return this._value.GetGameObjectArray(id);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x0002F26B File Offset: 0x0002D46B
		public GradientProxy GetGradient(string id)
		{
			return GradientProxy.New(this._value.GetGradient(id));
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x0002F27E File Offset: 0x0002D47E
		public Gradient[] GetGradientArray(string id)
		{
			return this._value.GetGradientArray(id);
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0002F28C File Offset: 0x0002D48C
		public int GetInt(string id)
		{
			return this._value.GetInt(id);
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x0002F29A File Offset: 0x0002D49A
		public int[] GetIntArray(string id)
		{
			return this._value.GetIntArray(id);
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x0002F2A8 File Offset: 0x0002D4A8
		public MaterialProxy GetMaterial(string id)
		{
			return MaterialProxy.New(this._value.GetMaterial(id));
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x0002F2BB File Offset: 0x0002D4BB
		public Material[] GetMaterialArray(string id)
		{
			return this._value.GetMaterialArray(id);
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x0002F2C9 File Offset: 0x0002D4C9
		public QuaternionProxy GetRotation(string id)
		{
			return QuaternionProxy.New(this._value.GetRotation(id));
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public Quaternion[] GetRotationArray(string id)
		{
			return this._value.GetRotationArray(id);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x0002F2EA File Offset: 0x0002D4EA
		public SpriteProxy GetSprite(string id)
		{
			return SpriteProxy.New(this._value.GetSprite(id));
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x0002F2FD File Offset: 0x0002D4FD
		public Sprite[] GetSpriteArray(string id)
		{
			return this._value.GetSpriteArray(id);
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x0002F30B File Offset: 0x0002D50B
		public string GetString(string id)
		{
			return this._value.GetString(id);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x0002F319 File Offset: 0x0002D519
		public string[] GetStringArray(string id)
		{
			return this._value.GetStringArray(id);
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x0002F327 File Offset: 0x0002D527
		public TextureProxy GetTexture(string id)
		{
			return TextureProxy.New(this._value.GetTexture(id));
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x0002F33A File Offset: 0x0002D53A
		public Texture[] GetTextureArray(string id)
		{
			return this._value.GetTextureArray(id);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x0002F348 File Offset: 0x0002D548
		public WeaponEntryProxy GetWeaponEntry(string id)
		{
			return WeaponEntryProxy.New(this._value.GetWeaponEntry(id));
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x0002F35B File Offset: 0x0002D55B
		public WeaponManager.WeaponEntry[] GetWeaponEntryArray(string id)
		{
			return this._value.GetWeaponEntryArray(id);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x0002F369 File Offset: 0x0002D569
		public Vector3Proxy GetVector(string id)
		{
			return Vector3Proxy.New(this._value.GetVector(id));
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x0002F37C File Offset: 0x0002D57C
		public Vector3[] GetVectorArray(string id)
		{
			return this._value.GetVectorArray(id);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x0002F38A File Offset: 0x0002D58A
		public VideoClipProxy GetVideoClip(string id)
		{
			return VideoClipProxy.New(this._value.GetVideoClip(id));
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x0002F39D File Offset: 0x0002D59D
		public VideoClip[] GetVideoClipArray(string id)
		{
			return this._value.GetVideoClipArray(id);
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x0002F3AB File Offset: 0x0002D5AB
		public bool HasActorSkin(string id)
		{
			return this._value.HasActorSkin(id);
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x0002F3B9 File Offset: 0x0002D5B9
		public bool HasAnimationCurve(string id)
		{
			return this._value.HasAnimationCurve(id);
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x0002F3C7 File Offset: 0x0002D5C7
		public bool HasAudioClip(string id)
		{
			return this._value.HasAudioClip(id);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x0002F3D5 File Offset: 0x0002D5D5
		public bool HasBool(string id)
		{
			return this._value.HasBool(id);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x0002F3E3 File Offset: 0x0002D5E3
		public bool HasColor(string id)
		{
			return this._value.HasColor(id);
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x0002F3F1 File Offset: 0x0002D5F1
		public bool HasFloat(string id)
		{
			return this._value.HasFloat(id);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x0002F3FF File Offset: 0x0002D5FF
		public bool HasGradient(string id)
		{
			return this._value.HasGradient(id);
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x0002F40D File Offset: 0x0002D60D
		public bool HasInt(string id)
		{
			return this._value.HasInt(id);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0002F41B File Offset: 0x0002D61B
		public bool HasMaterial(string id)
		{
			return this._value.HasMaterial(id);
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x0002F429 File Offset: 0x0002D629
		public bool HasObject(string id)
		{
			return this._value.HasObject(id);
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x0002F437 File Offset: 0x0002D637
		public bool HasRotation(string id)
		{
			return this._value.HasRotation(id);
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x0002F445 File Offset: 0x0002D645
		public bool HasSprite(string id)
		{
			return this._value.HasSprite(id);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x0002F453 File Offset: 0x0002D653
		public bool HasString(string id)
		{
			return this._value.HasString(id);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x0002F461 File Offset: 0x0002D661
		public bool HasTexture(string id)
		{
			return this._value.HasTexture(id);
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x0002F46F File Offset: 0x0002D66F
		public bool HasWeaponEntry(string id)
		{
			return this._value.HasWeaponEntry(id);
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x0002F47D File Offset: 0x0002D67D
		public bool HasVector(string id)
		{
			return this._value.HasVector(id);
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x0002F48B File Offset: 0x0002D68B
		public bool HasVideoClip(string id)
		{
			return this._value.HasVideoClip(id);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x0002F499 File Offset: 0x0002D699
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003159 RID: 12633
		[MoonSharpHidden]
		public DataContainer _value;
	}
}
