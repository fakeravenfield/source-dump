using System;
using System.Collections.Generic;
using System.Linq;
using Lua;
using UnityEngine;
using UnityEngine.Video;

namespace Ravenfield.Mods.Data
{
	// Token: 0x0200038C RID: 908
	[Doc("The DataStructure component can be placed on any GameObject using the Ravenfield Mod Tools. The component has several field arrays with different variable types where values can be assigned together with their id. The values can be read by id via the Ravenscript API, which is useful for configuration or mod<->mod functionality. For example, the float value with id ``myId`` can be accessed by ``GetFloat(\"myId\")``. It is also possible to read arrays of values using ``GetFloatArray(\"myId\")`` which will return an array of the values with ids: ``myId1, myId2, myId3, myId4 ...``. The Array will automatically end when no more valid ids are found.")]
	public class DataContainer : MonoBehaviour
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x000029BB File Offset: 0x00000BBB
		[Ignore]
		public DataContainer()
		{
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000A0F30 File Offset: 0x0009F130
		private void Awake()
		{
			if (this.weaponEntries != null)
			{
				WeaponEntryEntry[] array = this.weaponEntries;
				for (int i = 0; i < array.Length; i++)
				{
					ModManager.PreprocessWeaponEntryPrefab(array[i].value, null);
				}
			}
			if (this.skins != null)
			{
				ActorSkinEntry[] array2 = this.skins;
				for (int i = 0; i < array2.Length; i++)
				{
					RetargetSkin.Retarget(array2[i].value);
				}
			}
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000A0F94 File Offset: 0x0009F194
		public static T GetValueByIdNoExceptionHandling<T>(string id, IEnumerable<DataEntry<T>> entries)
		{
			int hash = DataEntryBase.IdToHash(id);
			return entries.First((DataEntry<T> e) => e.MatchesIdHash(hash)).value;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000A0FCC File Offset: 0x0009F1CC
		public static T GetValueById<T>(string id, IEnumerable<DataEntry<T>> entries)
		{
			T result;
			try
			{
				result = DataContainer.GetValueByIdNoExceptionHandling<T>(id, entries);
			}
			catch (Exception)
			{
				ScriptConsole.instance.LogError("Could not get value with id {0}, no such value exists.", new object[]
				{
					id
				});
				result = default(T);
			}
			return result;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000A101C File Offset: 0x0009F21C
		public static T[] GetValueArrayById<T>(string baseId, IEnumerable<DataEntry<T>> entries)
		{
			int num = DataContainer.Contains<T>(baseId + 0.ToString(), entries) ? 0 : 1;
			List<T> list = new List<T>();
			for (int i = num; i < 1024; i++)
			{
				string id = baseId + i.ToString();
				try
				{
					T valueByIdNoExceptionHandling = DataContainer.GetValueByIdNoExceptionHandling<T>(id, entries);
					list.Add(valueByIdNoExceptionHandling);
				}
				catch (Exception)
				{
					break;
				}
			}
			return list.ToArray();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000A1094 File Offset: 0x0009F294
		public static bool Contains<T>(string id, IEnumerable<DataEntry<T>> entries)
		{
			int hash = DataEntryBase.IdToHash(id);
			if (entries == null)
			{
				return false;
			}
			using (IEnumerator<DataEntry<T>> enumerator = entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MatchesIdHash(hash))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00011EC8 File Offset: 0x000100C8
		public bool GetBool(string id)
		{
			return DataContainer.GetValueById<bool>(id, this.bools);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00011ED6 File Offset: 0x000100D6
		public bool[] GetBoolArray(string id)
		{
			return DataContainer.GetValueArrayById<bool>(id, this.bools);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00011EE4 File Offset: 0x000100E4
		public bool HasBool(string id)
		{
			return DataContainer.Contains<bool>(id, this.bools);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00011EF2 File Offset: 0x000100F2
		public int GetInt(string id)
		{
			return DataContainer.GetValueById<int>(id, this.ints);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00011F00 File Offset: 0x00010100
		public int[] GetIntArray(string id)
		{
			return DataContainer.GetValueArrayById<int>(id, this.ints);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00011F0E File Offset: 0x0001010E
		public bool HasInt(string id)
		{
			return DataContainer.Contains<int>(id, this.ints);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00011F1C File Offset: 0x0001011C
		public float GetFloat(string id)
		{
			return DataContainer.GetValueById<float>(id, this.floats);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00011F2A File Offset: 0x0001012A
		public float[] GetFloatArray(string id)
		{
			return DataContainer.GetValueArrayById<float>(id, this.floats);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00011F38 File Offset: 0x00010138
		public bool HasFloat(string id)
		{
			return DataContainer.Contains<float>(id, this.floats);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00011F46 File Offset: 0x00010146
		public string GetString(string id)
		{
			return DataContainer.GetValueById<string>(id, this.strings);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00011F54 File Offset: 0x00010154
		public string[] GetStringArray(string id)
		{
			return DataContainer.GetValueArrayById<string>(id, this.strings);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00011F62 File Offset: 0x00010162
		public bool HasString(string id)
		{
			return DataContainer.Contains<string>(id, this.strings);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00011F70 File Offset: 0x00010170
		public Vector3 GetVector(string id)
		{
			return DataContainer.GetValueById<Vector3>(id, this.vectors);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00011F7E File Offset: 0x0001017E
		public Vector3[] GetVectorArray(string id)
		{
			return DataContainer.GetValueArrayById<Vector3>(id, this.vectors);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00011F8C File Offset: 0x0001018C
		public bool HasVector(string id)
		{
			return DataContainer.Contains<Vector3>(id, this.vectors);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00011F9A File Offset: 0x0001019A
		public Quaternion GetRotation(string id)
		{
			return DataContainer.GetValueById<Quaternion>(id, this.rotations);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00011FA8 File Offset: 0x000101A8
		public Quaternion[] GetRotationArray(string id)
		{
			return DataContainer.GetValueArrayById<Quaternion>(id, this.rotations);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00011FB6 File Offset: 0x000101B6
		public bool HasRotation(string id)
		{
			return DataContainer.Contains<Quaternion>(id, this.rotations);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00011FC4 File Offset: 0x000101C4
		public Color GetColor(string id)
		{
			return DataContainer.GetValueById<Color>(id, this.colors);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00011FD2 File Offset: 0x000101D2
		public Color[] GetColorArray(string id)
		{
			return DataContainer.GetValueArrayById<Color>(id, this.colors);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00011FE0 File Offset: 0x000101E0
		public bool HasColor(string id)
		{
			return DataContainer.Contains<Color>(id, this.colors);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00011FEE File Offset: 0x000101EE
		public Gradient GetGradient(string id)
		{
			return DataContainer.GetValueById<Gradient>(id, this.gradients);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00011FFC File Offset: 0x000101FC
		public Gradient[] GetGradientArray(string id)
		{
			return DataContainer.GetValueArrayById<Gradient>(id, this.gradients);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0001200A File Offset: 0x0001020A
		public bool HasGradient(string id)
		{
			return DataContainer.Contains<Gradient>(id, this.gradients);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00012018 File Offset: 0x00010218
		public Texture GetTexture(string id)
		{
			return DataContainer.GetValueById<Texture>(id, this.textures);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00012026 File Offset: 0x00010226
		public Texture[] GetTextureArray(string id)
		{
			return DataContainer.GetValueArrayById<Texture>(id, this.textures);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00012034 File Offset: 0x00010234
		public bool HasTexture(string id)
		{
			return DataContainer.Contains<Texture>(id, this.textures);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00012042 File Offset: 0x00010242
		public Sprite GetSprite(string id)
		{
			return DataContainer.GetValueById<Sprite>(id, this.sprites);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00012050 File Offset: 0x00010250
		public Sprite[] GetSpriteArray(string id)
		{
			return DataContainer.GetValueArrayById<Sprite>(id, this.sprites);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0001205E File Offset: 0x0001025E
		public bool HasSprite(string id)
		{
			return DataContainer.Contains<Sprite>(id, this.sprites);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0001206C File Offset: 0x0001026C
		public AudioClip GetAudioClip(string id)
		{
			return DataContainer.GetValueById<AudioClip>(id, this.audioClips);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0001207A File Offset: 0x0001027A
		public AudioClip[] GetAudioClipArray(string id)
		{
			return DataContainer.GetValueArrayById<AudioClip>(id, this.audioClips);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00012088 File Offset: 0x00010288
		public bool HasAudioClip(string id)
		{
			return DataContainer.Contains<AudioClip>(id, this.audioClips);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00012096 File Offset: 0x00010296
		public Material GetMaterial(string id)
		{
			return DataContainer.GetValueById<Material>(id, this.materials);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000120A4 File Offset: 0x000102A4
		public Material[] GetMaterialArray(string id)
		{
			return DataContainer.GetValueArrayById<Material>(id, this.materials);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000120B2 File Offset: 0x000102B2
		public bool HasMaterial(string id)
		{
			return DataContainer.Contains<Material>(id, this.materials);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000120C0 File Offset: 0x000102C0
		public GameObject GetGameObject(string id)
		{
			return DataContainer.GetValueById<GameObject>(id, this.gameObjects);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000120CE File Offset: 0x000102CE
		public GameObject[] GetGameObjectArray(string id)
		{
			return DataContainer.GetValueArrayById<GameObject>(id, this.gameObjects);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000120DC File Offset: 0x000102DC
		public bool HasObject(string id)
		{
			return DataContainer.Contains<GameObject>(id, this.gameObjects);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000120EA File Offset: 0x000102EA
		public AnimationCurve GetAnimationCurve(string id)
		{
			return DataContainer.GetValueById<AnimationCurve>(id, this.animationCurves);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000120F8 File Offset: 0x000102F8
		public AnimationCurve[] GetAnimationCurveArray(string id)
		{
			return DataContainer.GetValueArrayById<AnimationCurve>(id, this.animationCurves);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00012106 File Offset: 0x00010306
		public bool HasAnimationCurve(string id)
		{
			return DataContainer.Contains<AnimationCurve>(id, this.animationCurves);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00012114 File Offset: 0x00010314
		public VideoClip GetVideoClip(string id)
		{
			return DataContainer.GetValueById<VideoClip>(id, this.videoClips);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00012122 File Offset: 0x00010322
		public VideoClip[] GetVideoClipArray(string id)
		{
			return DataContainer.GetValueArrayById<VideoClip>(id, this.videoClips);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00012130 File Offset: 0x00010330
		public bool HasVideoClip(string id)
		{
			return DataContainer.Contains<VideoClip>(id, this.videoClips);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0001213E File Offset: 0x0001033E
		public ActorSkin GetActorSkin(string id)
		{
			return DataContainer.GetValueById<ActorSkin>(id, this.skins);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0001214C File Offset: 0x0001034C
		public ActorSkin[] GetActorSkinArray(string id)
		{
			return DataContainer.GetValueArrayById<ActorSkin>(id, this.skins);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0001215A File Offset: 0x0001035A
		public bool HasActorSkin(string id)
		{
			return DataContainer.Contains<ActorSkin>(id, this.skins);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00012168 File Offset: 0x00010368
		public WeaponManager.WeaponEntry GetWeaponEntry(string id)
		{
			return DataContainer.GetValueById<WeaponManager.WeaponEntry>(id, this.weaponEntries);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00012176 File Offset: 0x00010376
		public WeaponManager.WeaponEntry[] GetWeaponEntryArray(string id)
		{
			return DataContainer.GetValueArrayById<WeaponManager.WeaponEntry>(id, this.weaponEntries);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00012184 File Offset: 0x00010384
		public bool HasWeaponEntry(string id)
		{
			return DataContainer.Contains<WeaponManager.WeaponEntry>(id, this.weaponEntries);
		}

		// Token: 0x0400191B RID: 6427
		[Ignore]
		public BoolEntry[] bools;

		// Token: 0x0400191C RID: 6428
		[Ignore]
		public IntEntry[] ints;

		// Token: 0x0400191D RID: 6429
		[Ignore]
		public FloatEntry[] floats;

		// Token: 0x0400191E RID: 6430
		[Ignore]
		public StringEntry[] strings;

		// Token: 0x0400191F RID: 6431
		[Ignore]
		public VectorEntry[] vectors;

		// Token: 0x04001920 RID: 6432
		[Ignore]
		public RotationEntry[] rotations;

		// Token: 0x04001921 RID: 6433
		[Ignore]
		public ColorEntry[] colors;

		// Token: 0x04001922 RID: 6434
		[Ignore]
		public GradientEntry[] gradients;

		// Token: 0x04001923 RID: 6435
		[Ignore]
		public TextureEntry[] textures;

		// Token: 0x04001924 RID: 6436
		[Ignore]
		public SpriteEntry[] sprites;

		// Token: 0x04001925 RID: 6437
		[Ignore]
		public AudioClipEntry[] audioClips;

		// Token: 0x04001926 RID: 6438
		[Ignore]
		public MaterialEntry[] materials;

		// Token: 0x04001927 RID: 6439
		[Ignore]
		public AnimationCurveEntry[] animationCurves;

		// Token: 0x04001928 RID: 6440
		[Ignore]
		public VideoClipEntry[] videoClips;

		// Token: 0x04001929 RID: 6441
		[Ignore]
		public GameObjectEntry[] gameObjects;

		// Token: 0x0400192A RID: 6442
		[Ignore]
		public ActorSkinEntry[] skins;

		// Token: 0x0400192B RID: 6443
		[Ignore]
		public WeaponEntryEntry[] weaponEntries;
	}
}
