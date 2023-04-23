using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009AB RID: 2475
	[Proxy(typeof(ActorSkin))]
	public class ActorSkinProxy : IProxy
	{
		// Token: 0x06003FBC RID: 16316 RVA: 0x0002B1C9 File Offset: 0x000293C9
		[MoonSharpHidden]
		public ActorSkinProxy(ActorSkin value)
		{
			this._value = value;
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x0002B1D8 File Offset: 0x000293D8
		public ActorSkinProxy()
		{
			this._value = new ActorSkin();
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0002B1EB File Offset: 0x000293EB
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x0012FA0C File Offset: 0x0012DC0C
		public MeshSkinProxy armSkin
		{
			get
			{
				return MeshSkinProxy.New(this._value.armSkin);
			}
			set
			{
				ActorSkin.MeshSkin armSkin = null;
				if (value != null)
				{
					armSkin = value._value;
				}
				this._value.armSkin = armSkin;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x0002B1FD File Offset: 0x000293FD
		// (set) Token: 0x06003FC1 RID: 16321 RVA: 0x0012FA34 File Offset: 0x0012DC34
		public MeshSkinProxy characterSkin
		{
			get
			{
				return MeshSkinProxy.New(this._value.characterSkin);
			}
			set
			{
				ActorSkin.MeshSkin characterSkin = null;
				if (value != null)
				{
					characterSkin = value._value;
				}
				this._value.characterSkin = characterSkin;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x0002B20F File Offset: 0x0002940F
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x0012FA5C File Offset: 0x0012DC5C
		public MeshSkinProxy kickLegSkin
		{
			get
			{
				return MeshSkinProxy.New(this._value.kickLegSkin);
			}
			set
			{
				ActorSkin.MeshSkin kickLegSkin = null;
				if (value != null)
				{
					kickLegSkin = value._value;
				}
				this._value.kickLegSkin = kickLegSkin;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x0002B221 File Offset: 0x00029421
		// (set) Token: 0x06003FC5 RID: 16325 RVA: 0x0002B22E File Offset: 0x0002942E
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

		// Token: 0x06003FC6 RID: 16326 RVA: 0x0002B23C File Offset: 0x0002943C
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x0012FA84 File Offset: 0x0012DC84
		[MoonSharpHidden]
		public static ActorSkinProxy New(ActorSkin value)
		{
			if (value == null)
			{
				return null;
			}
			ActorSkinProxy actorSkinProxy = (ActorSkinProxy)ObjectCache.Get(typeof(ActorSkinProxy), value);
			if (actorSkinProxy == null)
			{
				actorSkinProxy = new ActorSkinProxy(value);
				ObjectCache.Add(typeof(ActorSkinProxy), value, actorSkinProxy);
			}
			return actorSkinProxy;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0002B244 File Offset: 0x00029444
		[MoonSharpUserDataMetamethod("__call")]
		public static ActorSkinProxy Call(DynValue _)
		{
			return new ActorSkinProxy();
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0002B24B File Offset: 0x0002944B
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003144 RID: 12612
		[MoonSharpHidden]
		public ActorSkin _value;
	}
}
