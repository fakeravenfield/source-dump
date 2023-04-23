using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000484 RID: 1156
	public class MB3_TextureCombinerNonTextureProperties
	{
		// Token: 0x06001D36 RID: 7478 RVA: 0x000BF8A4 File Offset: 0x000BDAA4
		public MB3_TextureCombinerNonTextureProperties(MB2_LogLevel ll, bool considerNonTextureProps)
		{
			this.LOG_LEVEL = ll;
			this._considerNonTextureProperties = considerNonTextureProps;
			this.textureProperty2DefaultColorMap = new Dictionary<string, Color>();
			for (int i = 0; i < this.defaultTextureProperty2DefaultColorMap.Length; i++)
			{
				this.textureProperty2DefaultColorMap.Add(this.defaultTextureProperty2DefaultColorMap[i].name, this.defaultTextureProperty2DefaultColorMap[i].color);
				this._nonTexturePropertiesBlender = new MB3_TextureCombinerNonTextureProperties.NonTexturePropertiesDontBlendProps(this);
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000BFAE0 File Offset: 0x000BDCE0
		internal void CollectAverageValuesOfNonTextureProperties(Material resultMaterial, Material mat)
		{
			for (int i = 0; i < this._nonTextureProperties.Length; i++)
			{
				MB3_TextureCombinerNonTextureProperties.MaterialProperty materialProperty = this._nonTextureProperties[i];
				if (resultMaterial.HasProperty(materialProperty.PropertyName))
				{
					materialProperty.GetAverageCalculator().TryGetPropValueFromMaterialAndBlendIntoAverage(mat, materialProperty);
				}
			}
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x00015CCC File Offset: 0x00013ECC
		internal void LoadTextureBlendersIfNeeded(Material resultMaterial)
		{
			if (this._considerNonTextureProperties)
			{
				this.LoadTextureBlenders();
				this.FindBestTextureBlender(resultMaterial);
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x00015CE3 File Offset: 0x00013EE3
		private static bool InterfaceFilter(Type typeObj, object criteriaObj)
		{
			return typeObj.ToString() == criteriaObj.ToString();
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000BFB24 File Offset: 0x000BDD24
		private void FindBestTextureBlender(Material resultMaterial)
		{
			this.resultMaterialTextureBlender = this.FindMatchingTextureBlender(resultMaterial.shader.name);
			if (this.resultMaterialTextureBlender != null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					string str = "Using _considerNonTextureProperties found a TextureBlender for result material. Using: ";
					TextureBlender textureBlender = this.resultMaterialTextureBlender;
					Debug.Log(str + ((textureBlender != null) ? textureBlender.ToString() : null));
				}
			}
			else
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogWarning("Using _considerNonTextureProperties could not find a TextureBlender that matches the shader on the result material. Using the Fallback Texture Blender.");
				}
				this.resultMaterialTextureBlender = new TextureBlenderFallback();
			}
			this._nonTexturePropertiesBlender = new MB3_TextureCombinerNonTextureProperties.NonTexturePropertiesBlendProps(this, this.resultMaterialTextureBlender);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000BFBAC File Offset: 0x000BDDAC
		private void LoadTextureBlenders()
		{
			string filterCriteria = "DigitalOpus.MB.Core.TextureBlender";
			TypeFilter filter = new TypeFilter(MB3_TextureCombinerNonTextureProperties.InterfaceFilter);
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				IEnumerable enumerable = null;
				try
				{
					enumerable = assembly.GetTypes();
				}
				catch (Exception ex)
				{
					ex.Equals(null);
				}
				if (enumerable != null)
				{
					foreach (Type type in assembly.GetTypes())
					{
						if (type.FindInterfaces(filter, filterCriteria).Length != 0)
						{
							list.Add(type);
						}
					}
				}
			}
			TextureBlender textureBlender = null;
			List<TextureBlender> list2 = new List<TextureBlender>();
			foreach (Type type2 in list)
			{
				if (!type2.IsAbstract && !type2.IsInterface)
				{
					TextureBlender textureBlender2 = (TextureBlender)Activator.CreateInstance(type2);
					if (textureBlender2 is TextureBlenderFallback)
					{
						textureBlender = textureBlender2;
					}
					else
					{
						list2.Add(textureBlender2);
					}
				}
			}
			if (textureBlender != null)
			{
				list2.Add(textureBlender);
			}
			this.textureBlenders = list2.ToArray();
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Format("Loaded {0} TextureBlenders.", this.textureBlenders.Length));
			}
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00015CF6 File Offset: 0x00013EF6
		internal bool NonTexturePropertiesAreEqual(Material a, Material b)
		{
			return this._nonTexturePropertiesBlender.NonTexturePropertiesAreEqual(a, b);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00015D05 File Offset: 0x00013F05
		internal Texture2D TintTextureWithTextureCombiner(Texture2D t, MB_TexSet sourceMaterial, ShaderTextureProperty shaderPropertyName)
		{
			return this._nonTexturePropertiesBlender.TintTextureWithTextureCombiner(t, sourceMaterial, shaderPropertyName);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00015D15 File Offset: 0x00013F15
		internal void AdjustNonTextureProperties(Material resultMat, List<ShaderTextureProperty> texPropertyNames, List<MB_TexSet> distinctMaterialTextures, MB2_EditorMethodsInterface editorMethods)
		{
			if (resultMat == null || texPropertyNames == null)
			{
				return;
			}
			this._nonTexturePropertiesBlender.AdjustNonTextureProperties(resultMat, texPropertyNames, distinctMaterialTextures, editorMethods);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00015D34 File Offset: 0x00013F34
		internal Color GetColorAsItWouldAppearInAtlasIfNoTexture(Material matIfBlender, ShaderTextureProperty texProperty)
		{
			return this._nonTexturePropertiesBlender.GetColorAsItWouldAppearInAtlasIfNoTexture(matIfBlender, texProperty);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00015D43 File Offset: 0x00013F43
		internal Color GetColorForTemporaryTexture(Material matIfBlender, ShaderTextureProperty texProperty)
		{
			return this._nonTexturePropertiesBlender.GetColorForTemporaryTexture(matIfBlender, texProperty);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000BFD10 File Offset: 0x000BDF10
		private TextureBlender FindMatchingTextureBlender(string shaderName)
		{
			for (int i = 0; i < this.textureBlenders.Length; i++)
			{
				if (this.textureBlenders[i].DoesShaderNameMatch(shaderName))
				{
					return this.textureBlenders[i];
				}
			}
			return null;
		}

		// Token: 0x04001DD1 RID: 7633
		public static Color NEUTRAL_NORMAL_MAP_COLOR = new Color(0.5f, 0.5f, 1f);

		// Token: 0x04001DD2 RID: 7634
		private MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair[] defaultTextureProperty2DefaultColorMap = new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair[]
		{
			new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair("_MainTex", new Color(1f, 1f, 1f, 0f)),
			new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair("_MetallicGlossMap", new Color(0f, 0f, 0f, 1f)),
			new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair("_ParallaxMap", new Color(0f, 0f, 0f, 0f)),
			new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair("_OcclusionMap", new Color(1f, 1f, 1f, 1f)),
			new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair("_EmissionMap", new Color(0f, 0f, 0f, 0f)),
			new MB3_TextureCombinerNonTextureProperties.TexPropertyNameColorPair("_DetailMask", new Color(0f, 0f, 0f, 0f))
		};

		// Token: 0x04001DD3 RID: 7635
		private MB3_TextureCombinerNonTextureProperties.MaterialProperty[] _nonTextureProperties = new MB3_TextureCombinerNonTextureProperties.MaterialProperty[]
		{
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyColor("_Color", Color.white),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyFloat("_Glossiness", 0.5f),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyFloat("_GlossMapScale", 1f),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyFloat("_Metallic", 0f),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyFloat("_BumpScale", 0.1f),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyFloat("_Parallax", 0.02f),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyFloat("_OcclusionStrength", 1f),
			new MB3_TextureCombinerNonTextureProperties.MaterialPropertyColor("_EmissionColor", Color.black)
		};

		// Token: 0x04001DD4 RID: 7636
		private MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x04001DD5 RID: 7637
		private bool _considerNonTextureProperties;

		// Token: 0x04001DD6 RID: 7638
		private TextureBlender resultMaterialTextureBlender;

		// Token: 0x04001DD7 RID: 7639
		private TextureBlender[] textureBlenders = new TextureBlender[0];

		// Token: 0x04001DD8 RID: 7640
		private Dictionary<string, Color> textureProperty2DefaultColorMap = new Dictionary<string, Color>();

		// Token: 0x04001DD9 RID: 7641
		private MB3_TextureCombinerNonTextureProperties.NonTextureProperties _nonTexturePropertiesBlender;

		// Token: 0x02000485 RID: 1157
		public interface MaterialProperty
		{
			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06001D43 RID: 7491
			// (set) Token: 0x06001D44 RID: 7492
			string PropertyName { get; set; }

			// Token: 0x06001D45 RID: 7493
			MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveraged GetAverageCalculator();

			// Token: 0x06001D46 RID: 7494
			object GetDefaultValue();
		}

		// Token: 0x02000486 RID: 1158
		public class MaterialPropertyFloat : MB3_TextureCombinerNonTextureProperties.MaterialProperty
		{
			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00015D6D File Offset: 0x00013F6D
			// (set) Token: 0x06001D48 RID: 7496 RVA: 0x00015D75 File Offset: 0x00013F75
			public string PropertyName { get; set; }

			// Token: 0x06001D49 RID: 7497 RVA: 0x00015D7E File Offset: 0x00013F7E
			public MaterialPropertyFloat(string name, float defValue)
			{
				this._averageCalc = new MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveragedFloat();
				this._defaultValue = defValue;
				this.PropertyName = name;
			}

			// Token: 0x06001D4A RID: 7498 RVA: 0x00015D9F File Offset: 0x00013F9F
			public MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveraged GetAverageCalculator()
			{
				return this._averageCalc;
			}

			// Token: 0x06001D4B RID: 7499 RVA: 0x00015DA7 File Offset: 0x00013FA7
			public object GetDefaultValue()
			{
				return this._defaultValue;
			}

			// Token: 0x04001DDB RID: 7643
			private MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveragedFloat _averageCalc;

			// Token: 0x04001DDC RID: 7644
			private float _defaultValue;
		}

		// Token: 0x02000487 RID: 1159
		public class MaterialPropertyColor : MB3_TextureCombinerNonTextureProperties.MaterialProperty
		{
			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00015DB4 File Offset: 0x00013FB4
			// (set) Token: 0x06001D4D RID: 7501 RVA: 0x00015DBC File Offset: 0x00013FBC
			public string PropertyName { get; set; }

			// Token: 0x06001D4E RID: 7502 RVA: 0x00015DC5 File Offset: 0x00013FC5
			public MaterialPropertyColor(string name, Color defaultVal)
			{
				this._averageCalc = new MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveragedColor();
				this._defaultValue = defaultVal;
				this.PropertyName = name;
			}

			// Token: 0x06001D4F RID: 7503 RVA: 0x00015DE6 File Offset: 0x00013FE6
			public MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveraged GetAverageCalculator()
			{
				return this._averageCalc;
			}

			// Token: 0x06001D50 RID: 7504 RVA: 0x00015DEE File Offset: 0x00013FEE
			public object GetDefaultValue()
			{
				return this._defaultValue;
			}

			// Token: 0x04001DDE RID: 7646
			private MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveragedColor _averageCalc;

			// Token: 0x04001DDF RID: 7647
			private Color _defaultValue;
		}

		// Token: 0x02000488 RID: 1160
		public interface MaterialPropertyValueAveraged
		{
			// Token: 0x06001D51 RID: 7505
			void TryGetPropValueFromMaterialAndBlendIntoAverage(Material mat, MB3_TextureCombinerNonTextureProperties.MaterialProperty property);

			// Token: 0x06001D52 RID: 7506
			object GetAverage();

			// Token: 0x06001D53 RID: 7507
			int NumValues();

			// Token: 0x06001D54 RID: 7508
			void SetAverageValueOrDefaultOnMaterial(Material mat, MB3_TextureCombinerNonTextureProperties.MaterialProperty property);
		}

		// Token: 0x02000489 RID: 1161
		public class MaterialPropertyValueAveragedFloat : MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveraged
		{
			// Token: 0x06001D55 RID: 7509 RVA: 0x000BFD4C File Offset: 0x000BDF4C
			public void TryGetPropValueFromMaterialAndBlendIntoAverage(Material mat, MB3_TextureCombinerNonTextureProperties.MaterialProperty property)
			{
				if (mat.HasProperty(property.PropertyName))
				{
					float @float = mat.GetFloat(property.PropertyName);
					this.averageVal = this.averageVal * (float)this.numValues / (float)(this.numValues + 1) + @float / (float)(this.numValues + 1);
					this.numValues++;
				}
			}

			// Token: 0x06001D56 RID: 7510 RVA: 0x00015DFB File Offset: 0x00013FFB
			public object GetAverage()
			{
				return this.averageVal;
			}

			// Token: 0x06001D57 RID: 7511 RVA: 0x00015E08 File Offset: 0x00014008
			public int NumValues()
			{
				return this.numValues;
			}

			// Token: 0x06001D58 RID: 7512 RVA: 0x000BFDAC File Offset: 0x000BDFAC
			public void SetAverageValueOrDefaultOnMaterial(Material mat, MB3_TextureCombinerNonTextureProperties.MaterialProperty property)
			{
				if (mat.HasProperty(property.PropertyName))
				{
					if (this.numValues > 0)
					{
						mat.SetFloat(property.PropertyName, this.averageVal);
						return;
					}
					mat.SetFloat(property.PropertyName, (float)property.GetDefaultValue());
				}
			}

			// Token: 0x04001DE0 RID: 7648
			public float averageVal;

			// Token: 0x04001DE1 RID: 7649
			public int numValues;
		}

		// Token: 0x0200048A RID: 1162
		public class MaterialPropertyValueAveragedColor : MB3_TextureCombinerNonTextureProperties.MaterialPropertyValueAveraged
		{
			// Token: 0x06001D5A RID: 7514 RVA: 0x000BFDFC File Offset: 0x000BDFFC
			public void TryGetPropValueFromMaterialAndBlendIntoAverage(Material mat, MB3_TextureCombinerNonTextureProperties.MaterialProperty property)
			{
				if (mat.HasProperty(property.PropertyName))
				{
					Color color = mat.GetColor(property.PropertyName);
					this.averageVal = this.averageVal * (float)this.numValues / (float)(this.numValues + 1) + color / (float)(this.numValues + 1);
					this.numValues++;
				}
			}

			// Token: 0x06001D5B RID: 7515 RVA: 0x00015E10 File Offset: 0x00014010
			public object GetAverage()
			{
				return this.averageVal;
			}

			// Token: 0x06001D5C RID: 7516 RVA: 0x00015E1D File Offset: 0x0001401D
			public int NumValues()
			{
				return this.numValues;
			}

			// Token: 0x06001D5D RID: 7517 RVA: 0x000BFE6C File Offset: 0x000BE06C
			public void SetAverageValueOrDefaultOnMaterial(Material mat, MB3_TextureCombinerNonTextureProperties.MaterialProperty property)
			{
				if (mat.HasProperty(property.PropertyName))
				{
					if (this.numValues > 0)
					{
						mat.SetColor(property.PropertyName, this.averageVal);
						return;
					}
					mat.SetColor(property.PropertyName, (Color)property.GetDefaultValue());
				}
			}

			// Token: 0x04001DE2 RID: 7650
			public Color averageVal;

			// Token: 0x04001DE3 RID: 7651
			public int numValues;
		}

		// Token: 0x0200048B RID: 1163
		public struct TexPropertyNameColorPair
		{
			// Token: 0x06001D5F RID: 7519 RVA: 0x00015E25 File Offset: 0x00014025
			public TexPropertyNameColorPair(string nm, Color col)
			{
				this.name = nm;
				this.color = col;
			}

			// Token: 0x04001DE4 RID: 7652
			public string name;

			// Token: 0x04001DE5 RID: 7653
			public Color color;
		}

		// Token: 0x0200048C RID: 1164
		private interface NonTextureProperties
		{
			// Token: 0x06001D60 RID: 7520
			bool NonTexturePropertiesAreEqual(Material a, Material b);

			// Token: 0x06001D61 RID: 7521
			Texture2D TintTextureWithTextureCombiner(Texture2D t, MB_TexSet sourceMaterial, ShaderTextureProperty shaderPropertyName);

			// Token: 0x06001D62 RID: 7522
			void AdjustNonTextureProperties(Material resultMat, List<ShaderTextureProperty> texPropertyNames, List<MB_TexSet> distinctMaterialTextures, MB2_EditorMethodsInterface editorMethods);

			// Token: 0x06001D63 RID: 7523
			Color GetColorForTemporaryTexture(Material matIfBlender, ShaderTextureProperty texProperty);

			// Token: 0x06001D64 RID: 7524
			Color GetColorAsItWouldAppearInAtlasIfNoTexture(Material matIfBlender, ShaderTextureProperty texProperty);
		}

		// Token: 0x0200048D RID: 1165
		private class NonTexturePropertiesDontBlendProps : MB3_TextureCombinerNonTextureProperties.NonTextureProperties
		{
			// Token: 0x06001D65 RID: 7525 RVA: 0x00015E35 File Offset: 0x00014035
			public NonTexturePropertiesDontBlendProps(MB3_TextureCombinerNonTextureProperties textureProperties)
			{
				this._textureProperties = textureProperties;
			}

			// Token: 0x06001D66 RID: 7526 RVA: 0x0000476F File Offset: 0x0000296F
			public bool NonTexturePropertiesAreEqual(Material a, Material b)
			{
				return true;
			}

			// Token: 0x06001D67 RID: 7527 RVA: 0x00015E44 File Offset: 0x00014044
			public Texture2D TintTextureWithTextureCombiner(Texture2D t, MB_TexSet sourceMaterial, ShaderTextureProperty shaderPropertyName)
			{
				Debug.LogError("TintTextureWithTextureCombiner should never be called if resultMaterialTextureBlender is null");
				return t;
			}

			// Token: 0x06001D68 RID: 7528 RVA: 0x000BFEBC File Offset: 0x000BE0BC
			public void AdjustNonTextureProperties(Material resultMat, List<ShaderTextureProperty> texPropertyNames, List<MB_TexSet> distinctMaterialTextures, MB2_EditorMethodsInterface editorMethods)
			{
				if (resultMat == null || texPropertyNames == null)
				{
					return;
				}
				for (int i = 0; i < this._textureProperties._nonTextureProperties.Length; i++)
				{
					MB3_TextureCombinerNonTextureProperties.MaterialProperty materialProperty = this._textureProperties._nonTextureProperties[i];
					if (resultMat.HasProperty(materialProperty.PropertyName))
					{
						materialProperty.GetAverageCalculator().SetAverageValueOrDefaultOnMaterial(resultMat, materialProperty);
					}
				}
				if (editorMethods != null)
				{
					editorMethods.CommitChangesToAssets();
				}
			}

			// Token: 0x06001D69 RID: 7529 RVA: 0x00015E51 File Offset: 0x00014051
			public Color GetColorAsItWouldAppearInAtlasIfNoTexture(Material matIfBlender, ShaderTextureProperty texProperty)
			{
				return Color.white;
			}

			// Token: 0x06001D6A RID: 7530 RVA: 0x000BFF24 File Offset: 0x000BE124
			public Color GetColorForTemporaryTexture(Material matIfBlender, ShaderTextureProperty texProperty)
			{
				if (texProperty.isNormalMap)
				{
					return MB3_TextureCombinerNonTextureProperties.NEUTRAL_NORMAL_MAP_COLOR;
				}
				if (this._textureProperties.textureProperty2DefaultColorMap.ContainsKey(texProperty.name))
				{
					return this._textureProperties.textureProperty2DefaultColorMap[texProperty.name];
				}
				return new Color(1f, 1f, 1f, 0f);
			}

			// Token: 0x04001DE6 RID: 7654
			private MB3_TextureCombinerNonTextureProperties _textureProperties;
		}

		// Token: 0x0200048E RID: 1166
		private class NonTexturePropertiesBlendProps : MB3_TextureCombinerNonTextureProperties.NonTextureProperties
		{
			// Token: 0x06001D6B RID: 7531 RVA: 0x00015E58 File Offset: 0x00014058
			public NonTexturePropertiesBlendProps(MB3_TextureCombinerNonTextureProperties textureProperties, TextureBlender resultMats)
			{
				this.resultMaterialTextureBlender = resultMats;
				this._textureProperties = textureProperties;
			}

			// Token: 0x06001D6C RID: 7532 RVA: 0x00015E6E File Offset: 0x0001406E
			public bool NonTexturePropertiesAreEqual(Material a, Material b)
			{
				return this.resultMaterialTextureBlender.NonTexturePropertiesAreEqual(a, b);
			}

			// Token: 0x06001D6D RID: 7533 RVA: 0x000BFF88 File Offset: 0x000BE188
			public Texture2D TintTextureWithTextureCombiner(Texture2D t, MB_TexSet sourceMaterial, ShaderTextureProperty shaderPropertyName)
			{
				this.resultMaterialTextureBlender.OnBeforeTintTexture(sourceMaterial.matsAndGOs.mats[0].mat, shaderPropertyName.name);
				if (this._textureProperties.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("Blending texture {0} mat {1} with non-texture properties using TextureBlender {2}", t.name, sourceMaterial.matsAndGOs.mats[0].mat, this.resultMaterialTextureBlender));
				}
				for (int i = 0; i < t.height; i++)
				{
					Color[] pixels = t.GetPixels(0, i, t.width, 1);
					for (int j = 0; j < pixels.Length; j++)
					{
						pixels[j] = this.resultMaterialTextureBlender.OnBlendTexturePixel(shaderPropertyName.name, pixels[j]);
					}
					t.SetPixels(0, i, t.width, 1, pixels);
				}
				t.Apply();
				return t;
			}

			// Token: 0x06001D6E RID: 7534 RVA: 0x000C0060 File Offset: 0x000BE260
			public void AdjustNonTextureProperties(Material resultMat, List<ShaderTextureProperty> texPropertyNames, List<MB_TexSet> distinctMaterialTextures, MB2_EditorMethodsInterface editorMethods)
			{
				if (resultMat == null || texPropertyNames == null)
				{
					return;
				}
				if (this._textureProperties.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Adjusting non texture properties using TextureBlender for shader: " + resultMat.shader.name);
				}
				this.resultMaterialTextureBlender.SetNonTexturePropertyValuesOnResultMaterial(resultMat);
				if (editorMethods != null)
				{
					editorMethods.CommitChangesToAssets();
				}
			}

			// Token: 0x06001D6F RID: 7535 RVA: 0x000C00BC File Offset: 0x000BE2BC
			public Color GetColorAsItWouldAppearInAtlasIfNoTexture(Material matIfBlender, ShaderTextureProperty texProperty)
			{
				this.resultMaterialTextureBlender.OnBeforeTintTexture(matIfBlender, texProperty.name);
				Color colorForTemporaryTexture = this.GetColorForTemporaryTexture(matIfBlender, texProperty);
				return this.resultMaterialTextureBlender.OnBlendTexturePixel(texProperty.name, colorForTemporaryTexture);
			}

			// Token: 0x06001D70 RID: 7536 RVA: 0x00015E7D File Offset: 0x0001407D
			public Color GetColorForTemporaryTexture(Material matIfBlender, ShaderTextureProperty texProperty)
			{
				return this.resultMaterialTextureBlender.GetColorIfNoTexture(matIfBlender, texProperty);
			}

			// Token: 0x04001DE7 RID: 7655
			private MB3_TextureCombinerNonTextureProperties _textureProperties;

			// Token: 0x04001DE8 RID: 7656
			private TextureBlender resultMaterialTextureBlender;
		}
	}
}
