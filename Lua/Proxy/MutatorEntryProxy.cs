using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009DF RID: 2527
	[Proxy(typeof(MutatorEntry))]
	public class MutatorEntryProxy : IProxy
	{
		// Token: 0x06004A25 RID: 18981 RVA: 0x0003465C File Offset: 0x0003285C
		[MoonSharpHidden]
		public MutatorEntryProxy(MutatorEntry value)
		{
			this._value = value;
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x0003466B File Offset: 0x0003286B
		public string description
		{
			get
			{
				return WMutatorEntry.GetDescription(this._value);
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x00034678 File Offset: 0x00032878
		public string name
		{
			get
			{
				return WMutatorEntry.GetName(this._value);
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00034685 File Offset: 0x00032885
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00131A30 File Offset: 0x0012FC30
		[MoonSharpHidden]
		public static MutatorEntryProxy New(MutatorEntry value)
		{
			if (value == null)
			{
				return null;
			}
			MutatorEntryProxy mutatorEntryProxy = (MutatorEntryProxy)ObjectCache.Get(typeof(MutatorEntryProxy), value);
			if (mutatorEntryProxy == null)
			{
				mutatorEntryProxy = new MutatorEntryProxy(value);
				ObjectCache.Add(typeof(MutatorEntryProxy), value, mutatorEntryProxy);
			}
			return mutatorEntryProxy;
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x0003468D File Offset: 0x0003288D
		public bool GetConfigurationBool(string id)
		{
			return WMutatorEntry.GetConfigurationBool(this._value, id);
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x0003469B File Offset: 0x0003289B
		public int GetConfigurationDropdown(string id)
		{
			return WMutatorEntry.GetConfigurationDropdown(this._value, id);
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x000346A9 File Offset: 0x000328A9
		public float GetConfigurationFloat(string id)
		{
			return WMutatorEntry.GetConfigurationFloat(this._value, id);
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x000346B7 File Offset: 0x000328B7
		public float GetConfigurationInt(string id)
		{
			return WMutatorEntry.GetConfigurationInt(this._value, id);
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x000346C5 File Offset: 0x000328C5
		public float GetConfigurationRange(string id)
		{
			return WMutatorEntry.GetConfigurationRange(this._value, id);
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x000346D3 File Offset: 0x000328D3
		public string GetConfigurationString(string id)
		{
			return WMutatorEntry.GetConfigurationString(this._value, id);
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x000346E1 File Offset: 0x000328E1
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003177 RID: 12663
		[MoonSharpHidden]
		public MutatorEntry _value;
	}
}
