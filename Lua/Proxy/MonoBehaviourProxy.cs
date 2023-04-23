using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009DC RID: 2524
	[Proxy(typeof(MonoBehaviour))]
	public class MonoBehaviourProxy : IProxy
	{
		// Token: 0x060048C7 RID: 18631 RVA: 0x00033384 File Offset: 0x00031584
		[MoonSharpHidden]
		public MonoBehaviourProxy(MonoBehaviour value)
		{
			this._value = value;
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x00033393 File Offset: 0x00031593
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x000333A5 File Offset: 0x000315A5
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000333B7 File Offset: 0x000315B7
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x001316CC File Offset: 0x0012F8CC
		[MoonSharpHidden]
		public static MonoBehaviourProxy New(MonoBehaviour value)
		{
			if (value == null)
			{
				return null;
			}
			MonoBehaviourProxy monoBehaviourProxy = (MonoBehaviourProxy)ObjectCache.Get(typeof(MonoBehaviourProxy), value);
			if (monoBehaviourProxy == null)
			{
				monoBehaviourProxy = new MonoBehaviourProxy(value);
				ObjectCache.Add(typeof(MonoBehaviourProxy), value, monoBehaviourProxy);
			}
			return monoBehaviourProxy;
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000333BF File Offset: 0x000315BF
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003174 RID: 12660
		[MoonSharpHidden]
		public MonoBehaviour _value;
	}
}
