using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009D4 RID: 2516
	[Proxy(typeof(Material))]
	public class MaterialProxy : IProxy
	{
		// Token: 0x060046F0 RID: 18160 RVA: 0x000318CF File Offset: 0x0002FACF
		[MoonSharpHidden]
		public MaterialProxy(Material value)
		{
			this._value = value;
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00131140 File Offset: 0x0012F340
		public MaterialProxy(MaterialProxy source)
		{
			Material source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			this._value = new Material(source2);
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x000318DE File Offset: 0x0002FADE
		// (set) Token: 0x060046F3 RID: 18163 RVA: 0x000318F0 File Offset: 0x0002FAF0
		public ColorProxy color
		{
			get
			{
				return ColorProxy.New(this._value.color);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.color = value._value;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00031911 File Offset: 0x0002FB11
		// (set) Token: 0x060046F5 RID: 18165 RVA: 0x0003191E File Offset: 0x0002FB1E
		public bool doubleSidedGI
		{
			get
			{
				return this._value.doubleSidedGI;
			}
			set
			{
				this._value.doubleSidedGI = value;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x0003192C File Offset: 0x0002FB2C
		// (set) Token: 0x060046F7 RID: 18167 RVA: 0x00031939 File Offset: 0x0002FB39
		public bool enableInstancing
		{
			get
			{
				return this._value.enableInstancing;
			}
			set
			{
				this._value.enableInstancing = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x00031947 File Offset: 0x0002FB47
		// (set) Token: 0x060046F9 RID: 18169 RVA: 0x0013116C File Offset: 0x0012F36C
		public TextureProxy mainTexture
		{
			get
			{
				return TextureProxy.New(this._value.mainTexture);
			}
			set
			{
				Texture mainTexture = null;
				if (value != null)
				{
					mainTexture = value._value;
				}
				this._value.mainTexture = mainTexture;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x00031959 File Offset: 0x0002FB59
		// (set) Token: 0x060046FB RID: 18171 RVA: 0x0003196B File Offset: 0x0002FB6B
		public Vector2Proxy mainTextureOffset
		{
			get
			{
				return Vector2Proxy.New(this._value.mainTextureOffset);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.mainTextureOffset = value._value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x0003198C File Offset: 0x0002FB8C
		// (set) Token: 0x060046FD RID: 18173 RVA: 0x0003199E File Offset: 0x0002FB9E
		public Vector2Proxy mainTextureScale
		{
			get
			{
				return Vector2Proxy.New(this._value.mainTextureScale);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.mainTextureScale = value._value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060046FE RID: 18174 RVA: 0x000319BF File Offset: 0x0002FBBF
		public int passCount
		{
			get
			{
				return this._value.passCount;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x000319CC File Offset: 0x0002FBCC
		// (set) Token: 0x06004700 RID: 18176 RVA: 0x000319D9 File Offset: 0x0002FBD9
		public int renderQueue
		{
			get
			{
				return this._value.renderQueue;
			}
			set
			{
				this._value.renderQueue = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x000319E7 File Offset: 0x0002FBE7
		// (set) Token: 0x06004702 RID: 18178 RVA: 0x000319F4 File Offset: 0x0002FBF4
		public string[] shaderKeywords
		{
			get
			{
				return this._value.shaderKeywords;
			}
			set
			{
				this._value.shaderKeywords = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x00031A02 File Offset: 0x0002FC02
		// (set) Token: 0x06004704 RID: 18180 RVA: 0x00031A0F File Offset: 0x0002FC0F
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x00031A1D File Offset: 0x0002FC1D
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00131194 File Offset: 0x0012F394
		[MoonSharpHidden]
		public static MaterialProxy New(Material value)
		{
			if (value == null)
			{
				return null;
			}
			MaterialProxy materialProxy = (MaterialProxy)ObjectCache.Get(typeof(MaterialProxy), value);
			if (materialProxy == null)
			{
				materialProxy = new MaterialProxy(value);
				ObjectCache.Add(typeof(MaterialProxy), value, materialProxy);
			}
			return materialProxy;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x00031A25 File Offset: 0x0002FC25
		[MoonSharpUserDataMetamethod("__call")]
		public static MaterialProxy Call(DynValue _, MaterialProxy source)
		{
			return new MaterialProxy(source);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00031A2D File Offset: 0x0002FC2D
		public int ComputeCRC()
		{
			return this._value.ComputeCRC();
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x001311E0 File Offset: 0x0012F3E0
		public void CopyPropertiesFromMaterial(MaterialProxy mat)
		{
			Material mat2 = null;
			if (mat != null)
			{
				mat2 = mat._value;
			}
			this._value.CopyPropertiesFromMaterial(mat2);
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00031A3A File Offset: 0x0002FC3A
		public void DisableKeyword(string keyword)
		{
			this._value.DisableKeyword(keyword);
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00031A48 File Offset: 0x0002FC48
		public void EnableKeyword(string keyword)
		{
			this._value.EnableKeyword(keyword);
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x00031A56 File Offset: 0x0002FC56
		public int FindPass(string passName)
		{
			return this._value.FindPass(passName);
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00031A64 File Offset: 0x0002FC64
		public ColorProxy GetColor(string name)
		{
			return ColorProxy.New(this._value.GetColor(name));
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x00031A77 File Offset: 0x0002FC77
		public ColorProxy GetColor(int nameID)
		{
			return ColorProxy.New(this._value.GetColor(nameID));
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x00031A8A File Offset: 0x0002FC8A
		public Color[] GetColorArray(string name)
		{
			return this._value.GetColorArray(name);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x00031A98 File Offset: 0x0002FC98
		public Color[] GetColorArray(int nameID)
		{
			return this._value.GetColorArray(nameID);
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00031AA6 File Offset: 0x0002FCA6
		public void GetColorArray(string name, List<Color> values)
		{
			this._value.GetColorArray(name, values);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00031AB5 File Offset: 0x0002FCB5
		public void GetColorArray(int nameID, List<Color> values)
		{
			this._value.GetColorArray(nameID, values);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00031AC4 File Offset: 0x0002FCC4
		public float GetFloat(string name)
		{
			return this._value.GetFloat(name);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00031AD2 File Offset: 0x0002FCD2
		public float GetFloat(int nameID)
		{
			return this._value.GetFloat(nameID);
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00031AE0 File Offset: 0x0002FCE0
		public float[] GetFloatArray(string name)
		{
			return this._value.GetFloatArray(name);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00031AEE File Offset: 0x0002FCEE
		public float[] GetFloatArray(int nameID)
		{
			return this._value.GetFloatArray(nameID);
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00031AFC File Offset: 0x0002FCFC
		public void GetFloatArray(string name, List<float> values)
		{
			this._value.GetFloatArray(name, values);
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x00031B0B File Offset: 0x0002FD0B
		public void GetFloatArray(int nameID, List<float> values)
		{
			this._value.GetFloatArray(nameID, values);
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x00031B1A File Offset: 0x0002FD1A
		public int GetInt(string name)
		{
			return this._value.GetInt(name);
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00031B28 File Offset: 0x0002FD28
		public int GetInt(int nameID)
		{
			return this._value.GetInt(nameID);
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x00031B36 File Offset: 0x0002FD36
		public Matrix4x4Proxy GetMatrix(string name)
		{
			return Matrix4x4Proxy.New(this._value.GetMatrix(name));
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00031B49 File Offset: 0x0002FD49
		public Matrix4x4Proxy GetMatrix(int nameID)
		{
			return Matrix4x4Proxy.New(this._value.GetMatrix(nameID));
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00031B5C File Offset: 0x0002FD5C
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this._value.GetMatrixArray(name);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00031B6A File Offset: 0x0002FD6A
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return this._value.GetMatrixArray(nameID);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x00031B78 File Offset: 0x0002FD78
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this._value.GetMatrixArray(name, values);
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00031B87 File Offset: 0x0002FD87
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this._value.GetMatrixArray(nameID, values);
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x00031B96 File Offset: 0x0002FD96
		public string GetPassName(int pass)
		{
			return this._value.GetPassName(pass);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00031BA4 File Offset: 0x0002FDA4
		public bool GetShaderPassEnabled(string passName)
		{
			return this._value.GetShaderPassEnabled(passName);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x00031BB2 File Offset: 0x0002FDB2
		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this._value.GetTag(tag, searchFallbacks, defaultValue);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x00031BC2 File Offset: 0x0002FDC2
		public string GetTag(string tag, bool searchFallbacks)
		{
			return this._value.GetTag(tag, searchFallbacks);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x00031BD1 File Offset: 0x0002FDD1
		public TextureProxy GetTexture(string name)
		{
			return TextureProxy.New(this._value.GetTexture(name));
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x00031BE4 File Offset: 0x0002FDE4
		public TextureProxy GetTexture(int nameID)
		{
			return TextureProxy.New(this._value.GetTexture(nameID));
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x00031BF7 File Offset: 0x0002FDF7
		public Vector2Proxy GetTextureOffset(string name)
		{
			return Vector2Proxy.New(this._value.GetTextureOffset(name));
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x00031C0A File Offset: 0x0002FE0A
		public Vector2Proxy GetTextureOffset(int nameID)
		{
			return Vector2Proxy.New(this._value.GetTextureOffset(nameID));
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x00031C1D File Offset: 0x0002FE1D
		public int[] GetTexturePropertyNameIDs()
		{
			return this._value.GetTexturePropertyNameIDs();
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00031C2A File Offset: 0x0002FE2A
		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			this._value.GetTexturePropertyNameIDs(outNames);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x00031C38 File Offset: 0x0002FE38
		public string[] GetTexturePropertyNames()
		{
			return this._value.GetTexturePropertyNames();
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x00031C45 File Offset: 0x0002FE45
		public void GetTexturePropertyNames(List<string> outNames)
		{
			this._value.GetTexturePropertyNames(outNames);
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00031C53 File Offset: 0x0002FE53
		public Vector2Proxy GetTextureScale(string name)
		{
			return Vector2Proxy.New(this._value.GetTextureScale(name));
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00031C66 File Offset: 0x0002FE66
		public Vector2Proxy GetTextureScale(int nameID)
		{
			return Vector2Proxy.New(this._value.GetTextureScale(nameID));
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00031C79 File Offset: 0x0002FE79
		public Vector4Proxy GetVector(string name)
		{
			return Vector4Proxy.New(this._value.GetVector(name));
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00031C8C File Offset: 0x0002FE8C
		public Vector4Proxy GetVector(int nameID)
		{
			return Vector4Proxy.New(this._value.GetVector(nameID));
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x00031C9F File Offset: 0x0002FE9F
		public Vector4[] GetVectorArray(string name)
		{
			return this._value.GetVectorArray(name);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x00031CAD File Offset: 0x0002FEAD
		public Vector4[] GetVectorArray(int nameID)
		{
			return this._value.GetVectorArray(nameID);
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00031CBB File Offset: 0x0002FEBB
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this._value.GetVectorArray(name, values);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00031CCA File Offset: 0x0002FECA
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this._value.GetVectorArray(nameID, values);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x00031CD9 File Offset: 0x0002FED9
		public bool HasProperty(int nameID)
		{
			return this._value.HasProperty(nameID);
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00031CE7 File Offset: 0x0002FEE7
		public bool HasProperty(string name)
		{
			return this._value.HasProperty(name);
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x00031CF5 File Offset: 0x0002FEF5
		public bool IsKeywordEnabled(string keyword)
		{
			return this._value.IsKeywordEnabled(keyword);
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x00131208 File Offset: 0x0012F408
		public void Lerp(MaterialProxy start, MaterialProxy end, float t)
		{
			Material start2 = null;
			if (start != null)
			{
				start2 = start._value;
			}
			Material end2 = null;
			if (end != null)
			{
				end2 = end._value;
			}
			this._value.Lerp(start2, end2, t);
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x00031D03 File Offset: 0x0002FF03
		public void SetColor(string name, ColorProxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetColor(name, value._value);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x00031D25 File Offset: 0x0002FF25
		public void SetColor(int nameID, ColorProxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetColor(nameID, value._value);
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x00031D47 File Offset: 0x0002FF47
		public void SetColorArray(string name, List<Color> values)
		{
			this._value.SetColorArray(name, values);
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00031D56 File Offset: 0x0002FF56
		public void SetColorArray(int nameID, List<Color> values)
		{
			this._value.SetColorArray(nameID, values);
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x00031D65 File Offset: 0x0002FF65
		public void SetColorArray(string name, Color[] values)
		{
			this._value.SetColorArray(name, values);
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00031D74 File Offset: 0x0002FF74
		public void SetColorArray(int nameID, Color[] values)
		{
			this._value.SetColorArray(nameID, values);
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00031D83 File Offset: 0x0002FF83
		public void SetFloat(string name, float value)
		{
			this._value.SetFloat(name, value);
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00031D92 File Offset: 0x0002FF92
		public void SetFloat(int nameID, float value)
		{
			this._value.SetFloat(nameID, value);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00031DA1 File Offset: 0x0002FFA1
		public void SetFloatArray(string name, List<float> values)
		{
			this._value.SetFloatArray(name, values);
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		public void SetFloatArray(int nameID, List<float> values)
		{
			this._value.SetFloatArray(nameID, values);
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x00031DBF File Offset: 0x0002FFBF
		public void SetFloatArray(string name, float[] values)
		{
			this._value.SetFloatArray(name, values);
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x00031DCE File Offset: 0x0002FFCE
		public void SetFloatArray(int nameID, float[] values)
		{
			this._value.SetFloatArray(nameID, values);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00031DDD File Offset: 0x0002FFDD
		public void SetInt(string name, int value)
		{
			this._value.SetInt(name, value);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00031DEC File Offset: 0x0002FFEC
		public void SetInt(int nameID, int value)
		{
			this._value.SetInt(nameID, value);
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00031DFB File Offset: 0x0002FFFB
		public void SetMatrix(string name, Matrix4x4Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetMatrix(name, value._value);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00031E1D File Offset: 0x0003001D
		public void SetMatrix(int nameID, Matrix4x4Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetMatrix(nameID, value._value);
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00031E3F File Offset: 0x0003003F
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this._value.SetMatrixArray(name, values);
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00031E4E File Offset: 0x0003004E
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this._value.SetMatrixArray(nameID, values);
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00031E5D File Offset: 0x0003005D
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this._value.SetMatrixArray(name, values);
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x00031E6C File Offset: 0x0003006C
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this._value.SetMatrixArray(nameID, values);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00031E7B File Offset: 0x0003007B
		public void SetOverrideTag(string tag, string val)
		{
			this._value.SetOverrideTag(tag, val);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00031E8A File Offset: 0x0003008A
		public bool SetPass(int pass)
		{
			return this._value.SetPass(pass);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00031E98 File Offset: 0x00030098
		public void SetShaderPassEnabled(string passName, bool enabled)
		{
			this._value.SetShaderPassEnabled(passName, enabled);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0013123C File Offset: 0x0012F43C
		public void SetTexture(string name, TextureProxy value)
		{
			Texture value2 = null;
			if (value != null)
			{
				value2 = value._value;
			}
			this._value.SetTexture(name, value2);
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00131264 File Offset: 0x0012F464
		public void SetTexture(int nameID, TextureProxy value)
		{
			Texture value2 = null;
			if (value != null)
			{
				value2 = value._value;
			}
			this._value.SetTexture(nameID, value2);
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00031EA7 File Offset: 0x000300A7
		public void SetTextureOffset(string name, Vector2Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetTextureOffset(name, value._value);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00031EC9 File Offset: 0x000300C9
		public void SetTextureOffset(int nameID, Vector2Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetTextureOffset(nameID, value._value);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00031EEB File Offset: 0x000300EB
		public void SetTextureScale(string name, Vector2Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetTextureScale(name, value._value);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00031F0D File Offset: 0x0003010D
		public void SetTextureScale(int nameID, Vector2Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetTextureScale(nameID, value._value);
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00031F2F File Offset: 0x0003012F
		public void SetVector(string name, Vector4Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetVector(name, value._value);
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x00031F51 File Offset: 0x00030151
		public void SetVector(int nameID, Vector4Proxy value)
		{
			if (value == null)
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			this._value.SetVector(nameID, value._value);
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x00031F73 File Offset: 0x00030173
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this._value.SetVectorArray(name, values);
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x00031F82 File Offset: 0x00030182
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this._value.SetVectorArray(nameID, values);
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x00031F91 File Offset: 0x00030191
		public void SetVectorArray(string name, Vector4[] values)
		{
			this._value.SetVectorArray(name, values);
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x00031FA0 File Offset: 0x000301A0
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this._value.SetVectorArray(nameID, values);
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x00031FAF File Offset: 0x000301AF
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x00031FBC File Offset: 0x000301BC
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400316C RID: 12652
		[MoonSharpHidden]
		public Material _value;
	}
}
