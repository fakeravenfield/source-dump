using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200057B RID: 1403
	[Serializable]
	public class GeneratorsAsset : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x060023D3 RID: 9171 RVA: 0x00018F97 File Offset: 0x00017197
		public IEnumerable<T> GeneratorsOfType<T>(bool onlyEnabled = true, bool checkBiomes = true) where T : Generator
		{
			int num;
			for (int i = 0; i < this.list.Length; i = num + 1)
			{
				Generator generator = this.list[i];
				if ((!onlyEnabled || generator.enabled) && this.list[i] is T)
				{
					generator.biome = null;
					yield return (T)((object)generator);
				}
				num = i;
			}
			if (checkBiomes)
			{
				for (int i = 0; i < this.list.Length; i = num + 1)
				{
					Generator generator2 = this.list[i];
					if ((!onlyEnabled || generator2.enabled) && generator2 is Biome)
					{
						Biome biome = (Biome)generator2;
						if (biome.data != null && biome.mask.linkGen != null)
						{
							foreach (T t in biome.data.GeneratorsOfType<T>(onlyEnabled, true))
							{
								t.biome = biome;
								yield return t;
							}
							IEnumerator<T> enumerator = null;
						}
						biome = null;
					}
					num = i;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00018FB5 File Offset: 0x000171B5
		public IEnumerable<Generator.IOutput> OutputGenerators(bool onlyEnabled = true, bool checkBiomes = true)
		{
			int num;
			for (int i = 0; i < this.list.Length; i = num + 1)
			{
				Generator generator = this.list[i];
				if ((!onlyEnabled || generator.enabled) && this.list[i] is Generator.IOutput)
				{
					yield return (Generator.IOutput)generator;
				}
				num = i;
			}
			if (checkBiomes)
			{
				for (int i = 0; i < this.list.Length; i = num + 1)
				{
					Generator generator2 = this.list[i];
					if ((!onlyEnabled || generator2.enabled) && generator2 is Biome)
					{
						Biome biome = (Biome)generator2;
						if (biome.data != null && biome.mask.linkGen != null)
						{
							foreach (Generator.IOutput output in biome.data.OutputGenerators(onlyEnabled, true))
							{
								yield return output;
							}
							IEnumerator<Generator.IOutput> enumerator = null;
						}
					}
					num = i;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000E6024 File Offset: 0x000E4224
		public HashSet<Type> GetExistingOutputTypes(bool onlyEnabled = true, bool checkBiomes = true)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			for (int i = 0; i < this.list.Length; i++)
			{
				Generator generator = this.list[i];
				if ((!onlyEnabled || generator.enabled) && this.list[i] is Generator.IOutput)
				{
					Type type = this.list[i].GetType();
					if (!hashSet.Contains(type))
					{
						hashSet.Add(type);
					}
				}
			}
			if (checkBiomes)
			{
				for (int j = 0; j < this.list.Length; j++)
				{
					Generator generator2 = this.list[j];
					if ((!onlyEnabled || generator2.enabled) && generator2 is Biome)
					{
						Biome biome = (Biome)generator2;
						if (biome.data != null && biome.mask.linkGen != null)
						{
							hashSet.UnionWith(biome.data.GetExistingOutputTypes(onlyEnabled, true));
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000E6104 File Offset: 0x000E4304
		public void ChangeGenerator(Generator gen)
		{
			if (gen != null && MapMagic.instance.saveIntermediate)
			{
				foreach (Chunk chunk in MapMagic.instance.terrains.Objects())
				{
					chunk.ready.CheckRemove(gen);
				}
			}
			if (!MapMagic.instance.saveIntermediate)
			{
				foreach (Chunk chunk2 in MapMagic.instance.terrains.Objects())
				{
					chunk2.results.Clear();
					chunk2.ready.Clear();
				}
			}
			if (MapMagic.instance.instantGenerate && MapMagic.instance.enabled)
			{
				MapMagic.instance.Generate();
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000E61EC File Offset: 0x000E43EC
		public Generator CreateGenerator(Type type, Vector2 guiPos = default(Vector2))
		{
			Generator generator = (Generator)Activator.CreateInstance(type);
			generator.guiRect.x = guiPos.x - generator.guiRect.width / 2f;
			generator.guiRect.y = guiPos.y - 10f;
			if (generator is Group)
			{
				generator.guiRect.width = 300f;
				generator.guiRect.height = 200f;
			}
			ArrayTools.Add<Generator>(ref this.list, generator);
			return generator;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000E6274 File Offset: 0x000E4474
		public void DeleteGenerator(Generator gen)
		{
			this.ChangeGenerator(gen);
			for (int i = 0; i < this.list.Length; i++)
			{
				if (this.list[i].IsDependentFrom(gen))
				{
					this.ChangeGenerator(this.list[i]);
				}
			}
			this.UnlinkGenerator(gen, false);
			ArrayTools.Remove<Generator>(ref this.list, gen);
			if (MapMagic.instance.previewGenerator == gen)
			{
				MapMagic.instance.previewGenerator = null;
				MapMagic.instance.previewOutput = null;
			}
			if (gen is Generator.IOutput)
			{
				MapMagic.instance.ForceGenerate();
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00018FD3 File Offset: 0x000171D3
		public void ClearGenerators()
		{
			this.list = new Generator[0];
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000E6304 File Offset: 0x000E4504
		public void UnlinkGenerator(Generator gen, bool unlinkGroup = false)
		{
			foreach (Generator.Input input in gen.Inputs())
			{
				if (input != null)
				{
					input.Unlink();
				}
			}
			for (int i = 0; i < this.list.Length; i++)
			{
				foreach (Generator.Input input2 in this.list[i].Inputs())
				{
					if (input2 != null && input2.linkGen == gen)
					{
						input2.Unlink();
					}
				}
			}
			Group group = gen as Group;
			if (group != null && unlinkGroup)
			{
				for (int j = 0; j < this.list.Length; j++)
				{
					if (group.guiRect.Contains(this.list[j].guiRect))
					{
						foreach (Generator.Input input3 in this.list[j].Inputs())
						{
							if (!group.guiRect.Contains(input3.linkGen.guiRect))
							{
								input3.Unlink();
							}
						}
					}
					if (!group.guiRect.Contains(this.list[j].guiRect))
					{
						foreach (Generator.Input input4 in this.list[j].Inputs())
						{
							if (group.guiRect.Contains(input4.linkGen.guiRect))
							{
								input4.Unlink();
							}
						}
					}
				}
			}
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000E64DC File Offset: 0x000E46DC
		public void SortGroups()
		{
			for (int i = this.list.Length - 1; i >= 0; i--)
			{
				Generator generator = this.list[i];
				if (generator is Group)
				{
					for (int j = 0; j < this.list.Length; j++)
					{
						Generator generator2 = this.list[j];
						if (generator2 is Group && generator2.layout.field.Contains(generator.layout.field))
						{
							ArrayTools.Switch<Generator>(this.list, generator, generator2);
						}
					}
				}
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0000296E File Offset: 0x00000B6E
		public void SaveAsset()
		{
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00002FD8 File Offset: 0x000011D8
		public GeneratorsAsset ReleaseAsset()
		{
			return null;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000E6560 File Offset: 0x000E4760
		public static GeneratorsAsset Default()
		{
			GeneratorsAsset generatorsAsset = ScriptableObject.CreateInstance<GeneratorsAsset>();
			NoiseGenerator1 noiseGenerator = (NoiseGenerator1)generatorsAsset.CreateGenerator(typeof(NoiseGenerator1), new Vector2(50f, 50f));
			noiseGenerator.intensity = 0.75f;
			CurveGenerator curveGenerator = (CurveGenerator)generatorsAsset.CreateGenerator(typeof(CurveGenerator), new Vector2(250f, 50f));
			curveGenerator.curve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 0f, 0f),
				new Keyframe(1f, 1f, 2.5f, 1f)
			});
			HeightOutput heightOutput = (HeightOutput)generatorsAsset.CreateGenerator(typeof(HeightOutput), new Vector2(450f, 50f));
			curveGenerator.input.Link(noiseGenerator.output, noiseGenerator);
			heightOutput.input.Link(curveGenerator.output, curveGenerator);
			return generatorsAsset;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000E6664 File Offset: 0x000E4864
		public void OnBeforeSerialize()
		{
			this.serializedVersion = MapMagic.version;
			List<string> list = new List<string>();
			List<UnityEngine.Object> list2 = new List<UnityEngine.Object>();
			List<float> list3 = new List<float>();
			this.references.Clear();
			CustomSerialization.WriteClass(this.list, list, list2, list3, this.references);
			this.classes = list.ToArray();
			this.objects = list2.ToArray();
			this.floats = list3.ToArray();
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000E66D4 File Offset: 0x000E48D4
		public void OnAfterDeserialize()
		{
			if (this.serializedVersion < 10)
			{
				Debug.LogError("MapMagic: trying to load unknow version scene (v." + ((float)this.serializedVersion / 10f).ToString() + "). This may cause errors or drastic drop in performance. Delete this MapMagic object and create the new one from scratch when possible.");
			}
			if (this.classes.Length == 0 && this.serializer.entities.Count != 0)
			{
				this.serializer.ClearLinks();
				this.list = (Generator[])this.serializer.Retrieve(this.listNum);
				this.serializer.ClearLinks();
				this.OnBeforeSerialize();
				this.serializer = null;
			}
			List<string> list = new List<string>();
			list.AddRange(this.classes);
			List<UnityEngine.Object> list2 = new List<UnityEngine.Object>();
			list2.AddRange(this.objects);
			List<float> list3 = new List<float>();
			list3.AddRange(this.floats);
			this.references.Clear();
			this.list = (Generator[])CustomSerialization.ReadClass(0, list, list2, list3, this.references);
			this.references.Clear();
		}

		// Token: 0x040022FC RID: 8956
		public Generator[] list = new Generator[0];

		// Token: 0x040022FD RID: 8957
		public int oldId;

		// Token: 0x040022FE RID: 8958
		public int oldCount;

		// Token: 0x040022FF RID: 8959
		public int serializedVersion;

		// Token: 0x04002300 RID: 8960
		public string[] classes = new string[0];

		// Token: 0x04002301 RID: 8961
		public UnityEngine.Object[] objects = new UnityEngine.Object[0];

		// Token: 0x04002302 RID: 8962
		public float[] floats = new float[0];

		// Token: 0x04002303 RID: 8963
		public List<object> references = new List<object>();

		// Token: 0x04002304 RID: 8964
		public bool setDirty;

		// Token: 0x04002305 RID: 8965
		public Serializer serializer = new Serializer();

		// Token: 0x04002306 RID: 8966
		public int listNum;
	}
}
