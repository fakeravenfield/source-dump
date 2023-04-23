using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009FD RID: 2557
	[Proxy(typeof(SgmModifyRulesArgs))]
	public class SgmModifyRulesArgsProxy : IProxy
	{
		// Token: 0x06004E96 RID: 20118 RVA: 0x00038F72 File Offset: 0x00037172
		[MoonSharpHidden]
		public SgmModifyRulesArgsProxy(SgmModifyRulesArgs value)
		{
			this._value = value;
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x00038F81 File Offset: 0x00037181
		// (set) Token: 0x06004E98 RID: 20120 RVA: 0x00038F8E File Offset: 0x0003718E
		public float balance
		{
			get
			{
				return this._value.balance;
			}
			set
			{
				this._value.balance = value;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x00038F9C File Offset: 0x0003719C
		// (set) Token: 0x06004E9A RID: 20122 RVA: 0x00038FA9 File Offset: 0x000371A9
		public bool nightMode
		{
			get
			{
				return this._value.nightMode;
			}
			set
			{
				this._value.nightMode = value;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004E9B RID: 20123 RVA: 0x00038FB7 File Offset: 0x000371B7
		// (set) Token: 0x06004E9C RID: 20124 RVA: 0x00038FC4 File Offset: 0x000371C4
		public bool noTurrets
		{
			get
			{
				return this._value.noTurrets;
			}
			set
			{
				this._value.noTurrets = value;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06004E9D RID: 20125 RVA: 0x00038FD2 File Offset: 0x000371D2
		// (set) Token: 0x06004E9E RID: 20126 RVA: 0x00038FDF File Offset: 0x000371DF
		public bool noVehicles
		{
			get
			{
				return this._value.noVehicles;
			}
			set
			{
				this._value.noVehicles = value;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x00038FED File Offset: 0x000371ED
		// (set) Token: 0x06004EA0 RID: 20128 RVA: 0x00038FFA File Offset: 0x000371FA
		public WTeam playerTeam
		{
			get
			{
				return this._value.playerTeam;
			}
			set
			{
				this._value.playerTeam = value;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x00039008 File Offset: 0x00037208
		// (set) Token: 0x06004EA2 RID: 20130 RVA: 0x00039015 File Offset: 0x00037215
		public bool spawnDeadActors
		{
			get
			{
				return this._value.spawnDeadActors;
			}
			set
			{
				this._value.spawnDeadActors = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x00039023 File Offset: 0x00037223
		public int difficulty
		{
			get
			{
				return this._value.difficulty;
			}
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00039030 File Offset: 0x00037230
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00137F84 File Offset: 0x00136184
		[MoonSharpHidden]
		public static SgmModifyRulesArgsProxy New(SgmModifyRulesArgs value)
		{
			if (value == null)
			{
				return null;
			}
			SgmModifyRulesArgsProxy sgmModifyRulesArgsProxy = (SgmModifyRulesArgsProxy)ObjectCache.Get(typeof(SgmModifyRulesArgsProxy), value);
			if (sgmModifyRulesArgsProxy == null)
			{
				sgmModifyRulesArgsProxy = new SgmModifyRulesArgsProxy(value);
				ObjectCache.Add(typeof(SgmModifyRulesArgsProxy), value, sgmModifyRulesArgsProxy);
			}
			return sgmModifyRulesArgsProxy;
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00039038 File Offset: 0x00037238
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400328C RID: 12940
		[MoonSharpHidden]
		public SgmModifyRulesArgs _value;
	}
}
