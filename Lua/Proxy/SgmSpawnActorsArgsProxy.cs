using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009FF RID: 2559
	[Proxy(typeof(SgmSpawnActorsArgs))]
	public class SgmSpawnActorsArgsProxy : IProxy
	{
		// Token: 0x06004EB3 RID: 20147 RVA: 0x000390CC File Offset: 0x000372CC
		[MoonSharpHidden]
		public SgmSpawnActorsArgsProxy(SgmSpawnActorsArgs value)
		{
			this._value = value;
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x000390DB File Offset: 0x000372DB
		// (set) Token: 0x06004EB5 RID: 20149 RVA: 0x000390E8 File Offset: 0x000372E8
		public IList<Actor> actorsToSpawn
		{
			get
			{
				return this._value.actorsToSpawn;
			}
			set
			{
				this._value.actorsToSpawn = value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x000390F6 File Offset: 0x000372F6
		// (set) Token: 0x06004EB7 RID: 20151 RVA: 0x00039103 File Offset: 0x00037303
		public WTeam team
		{
			get
			{
				return this._value.team;
			}
			set
			{
				this._value.team = value;
			}
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x00039111 File Offset: 0x00037311
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00138034 File Offset: 0x00136234
		[MoonSharpHidden]
		public static SgmSpawnActorsArgsProxy New(SgmSpawnActorsArgs value)
		{
			if (value == null)
			{
				return null;
			}
			SgmSpawnActorsArgsProxy sgmSpawnActorsArgsProxy = (SgmSpawnActorsArgsProxy)ObjectCache.Get(typeof(SgmSpawnActorsArgsProxy), value);
			if (sgmSpawnActorsArgsProxy == null)
			{
				sgmSpawnActorsArgsProxy = new SgmSpawnActorsArgsProxy(value);
				ObjectCache.Add(typeof(SgmSpawnActorsArgsProxy), value, sgmSpawnActorsArgsProxy);
			}
			return sgmSpawnActorsArgsProxy;
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00039119 File Offset: 0x00037319
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400328E RID: 12942
		[MoonSharpHidden]
		public SgmSpawnActorsArgs _value;
	}
}
