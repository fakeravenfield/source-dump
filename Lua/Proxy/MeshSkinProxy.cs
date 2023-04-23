using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009DB RID: 2523
	[Proxy(typeof(ActorSkin.MeshSkin))]
	public class MeshSkinProxy : IProxy
	{
		// Token: 0x060048BA RID: 18618 RVA: 0x0003330E File Offset: 0x0003150E
		[MoonSharpHidden]
		public MeshSkinProxy(ActorSkin.MeshSkin value)
		{
			this._value = value;
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x00131608 File Offset: 0x0012F808
		public MeshSkinProxy(MeshProxy mesh, Material[] materials, int teamMaterial)
		{
			Mesh mesh2 = null;
			if (mesh != null)
			{
				mesh2 = mesh._value;
			}
			this._value = new ActorSkin.MeshSkin(mesh2, materials, teamMaterial);
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060048BC RID: 18620 RVA: 0x0003331D File Offset: 0x0003151D
		// (set) Token: 0x060048BD RID: 18621 RVA: 0x0003332A File Offset: 0x0003152A
		public Material[] materials
		{
			get
			{
				return this._value.materials;
			}
			set
			{
				this._value.materials = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060048BE RID: 18622 RVA: 0x00033338 File Offset: 0x00031538
		// (set) Token: 0x060048BF RID: 18623 RVA: 0x00131638 File Offset: 0x0012F838
		public MeshProxy mesh
		{
			get
			{
				return MeshProxy.New(this._value.mesh);
			}
			set
			{
				Mesh mesh = null;
				if (value != null)
				{
					mesh = value._value;
				}
				this._value.mesh = mesh;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x0003334A File Offset: 0x0003154A
		// (set) Token: 0x060048C1 RID: 18625 RVA: 0x00033357 File Offset: 0x00031557
		public int teamMaterial
		{
			get
			{
				return this._value.teamMaterial;
			}
			set
			{
				this._value.teamMaterial = value;
			}
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x00033365 File Offset: 0x00031565
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00131660 File Offset: 0x0012F860
		[MoonSharpHidden]
		public static MeshSkinProxy New(ActorSkin.MeshSkin value)
		{
			if (value == null)
			{
				return null;
			}
			MeshSkinProxy meshSkinProxy = (MeshSkinProxy)ObjectCache.Get(typeof(MeshSkinProxy), value);
			if (meshSkinProxy == null)
			{
				meshSkinProxy = new MeshSkinProxy(value);
				ObjectCache.Add(typeof(MeshSkinProxy), value, meshSkinProxy);
			}
			return meshSkinProxy;
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0003336D File Offset: 0x0003156D
		[MoonSharpUserDataMetamethod("__call")]
		public static MeshSkinProxy Call(DynValue _, MeshProxy mesh, Material[] materials, int teamMaterial)
		{
			return new MeshSkinProxy(mesh, materials, teamMaterial);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x001316A4 File Offset: 0x0012F8A4
		public void Apply(SkinnedMeshRendererProxy renderer, WTeam team)
		{
			SkinnedMeshRenderer renderer2 = null;
			if (renderer != null)
			{
				renderer2 = renderer._value;
			}
			this._value.Apply(renderer2, team);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x00033377 File Offset: 0x00031577
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003173 RID: 12659
		[MoonSharpHidden]
		public ActorSkin.MeshSkin _value;
	}
}
