using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200082B RID: 2091
	public class CustomConvertersCollection
	{
		// Token: 0x060033F5 RID: 13301 RVA: 0x001171D4 File Offset: 0x001153D4
		internal CustomConvertersCollection()
		{
			for (int i = 0; i < this.m_Script2Clr.Length; i++)
			{
				this.m_Script2Clr[i] = new Dictionary<Type, Func<DynValue, object>>();
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x0011722C File Offset: 0x0011542C
		public void SetOneToOneConversion(Type from, Type to, Func<object, object> converter)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			CustomConvertersCollection.One2OneKey key = new CustomConvertersCollection.One2OneKey(from, to);
			this.m_One2One.Add(key, converter);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x00117288 File Offset: 0x00115488
		public Func<object, object> GetOneToOne(Type from, Type to)
		{
			CustomConvertersCollection.One2OneKey key = new CustomConvertersCollection.One2OneKey(from, to);
			if (this.m_One2One.ContainsKey(key))
			{
				return this.m_One2One[key];
			}
			return null;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x001172BC File Offset: 0x001154BC
		public void SetScriptToClrCustomConversion(DataType scriptDataType, Type clrDataType, Func<DynValue, object> converter = null)
		{
			if (scriptDataType > (DataType)this.m_Script2Clr.Length)
			{
				throw new ArgumentException("scriptDataType");
			}
			Dictionary<Type, Func<DynValue, object>> dictionary = this.m_Script2Clr[(int)scriptDataType];
			if (converter == null)
			{
				if (dictionary.ContainsKey(clrDataType))
				{
					dictionary.Remove(clrDataType);
					return;
				}
			}
			else
			{
				dictionary[clrDataType] = converter;
			}
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x00023973 File Offset: 0x00021B73
		public Func<DynValue, object> GetScriptToClrCustomConversion(DataType scriptDataType, Type clrDataType)
		{
			if (scriptDataType > (DataType)this.m_Script2Clr.Length)
			{
				return null;
			}
			return this.m_Script2Clr[(int)scriptDataType].GetOrDefault(clrDataType);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x00023990 File Offset: 0x00021B90
		public void SetClrToScriptCustomConversion(Type clrDataType, Func<Script, object, DynValue> converter = null)
		{
			if (converter == null)
			{
				if (this.m_Clr2Script.ContainsKey(clrDataType))
				{
					this.m_Clr2Script.Remove(clrDataType);
					return;
				}
			}
			else
			{
				this.m_Clr2Script[clrDataType] = converter;
			}
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x00117308 File Offset: 0x00115508
		public void SetClrToScriptCustomConversion<T>(Func<Script, T, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(typeof(T), (Script s, object o) => converter(s, (T)((object)o)));
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000239BE File Offset: 0x00021BBE
		public Func<Script, object, DynValue> GetClrToScriptCustomConversion(Type clrDataType)
		{
			return this.m_Clr2Script.GetOrDefault(clrDataType);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x00117340 File Offset: 0x00115540
		[Obsolete("This method is deprecated. Use the overloads accepting functions with a Script argument.")]
		public void SetClrToScriptCustomConversion(Type clrDataType, Func<object, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(clrDataType, (Script s, object o) => converter(o));
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x00117370 File Offset: 0x00115570
		[Obsolete("This method is deprecated. Use the overloads accepting functions with a Script argument.")]
		public void SetClrToScriptCustomConversion<T>(Func<T, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(typeof(T), (object o) => converter((T)((object)o)));
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x001173A8 File Offset: 0x001155A8
		public void Clear()
		{
			this.m_Clr2Script.Clear();
			for (int i = 0; i < this.m_Script2Clr.Length; i++)
			{
				this.m_Script2Clr[i].Clear();
			}
		}

		// Token: 0x04002D97 RID: 11671
		private Dictionary<Type, Func<DynValue, object>>[] m_Script2Clr = new Dictionary<Type, Func<DynValue, object>>[11];

		// Token: 0x04002D98 RID: 11672
		private Dictionary<Type, Func<Script, object, DynValue>> m_Clr2Script = new Dictionary<Type, Func<Script, object, DynValue>>();

		// Token: 0x04002D99 RID: 11673
		private Dictionary<CustomConvertersCollection.One2OneKey, Func<object, object>> m_One2One = new Dictionary<CustomConvertersCollection.One2OneKey, Func<object, object>>();

		// Token: 0x0200082C RID: 2092
		private struct One2OneKey : IEquatable<CustomConvertersCollection.One2OneKey>
		{
			// Token: 0x06003400 RID: 13312 RVA: 0x000239CC File Offset: 0x00021BCC
			public One2OneKey(Type from, Type to)
			{
				this.From = from;
				this.To = to;
			}

			// Token: 0x06003401 RID: 13313 RVA: 0x000239DC File Offset: 0x00021BDC
			public override bool Equals(object obj)
			{
				return obj is CustomConvertersCollection.One2OneKey && this.Equals((CustomConvertersCollection.One2OneKey)obj);
			}

			// Token: 0x06003402 RID: 13314 RVA: 0x000239F4 File Offset: 0x00021BF4
			public bool Equals(CustomConvertersCollection.One2OneKey other)
			{
				return EqualityComparer<Type>.Default.Equals(this.From, other.From) && EqualityComparer<Type>.Default.Equals(this.To, other.To);
			}

			// Token: 0x06003403 RID: 13315 RVA: 0x00023A26 File Offset: 0x00021C26
			public override int GetHashCode()
			{
				return (-1781160927 * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(this.From)) * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(this.To);
			}

			// Token: 0x04002D9A RID: 11674
			public readonly Type From;

			// Token: 0x04002D9B RID: 11675
			public readonly Type To;
		}
	}
}
