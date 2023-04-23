using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000921 RID: 2337
	public class ObjectCache
	{
		// Token: 0x06003B4B RID: 15179 RVA: 0x0012C9E4 File Offset: 0x0012ABE4
		public static void Add(Type type, object obj, object proxy)
		{
			if ((type != null & obj != null) && proxy != null)
			{
				ObjectCache.Key key = new ObjectCache.Key(type, obj);
				ObjectCache.cache[key] = new WeakReference(proxy, false);
			}
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x0012CA20 File Offset: 0x0012AC20
		public static object Get(Type type, object obj)
		{
			object result = null;
			if (type != null && obj != null)
			{
				ObjectCache.Key key = new ObjectCache.Key(type, obj);
				if (ObjectCache.cache.ContainsKey(key))
				{
					result = ObjectCache.cache[key].Target;
				}
			}
			return result;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x0012CA64 File Offset: 0x0012AC64
		public static void Clean()
		{
			List<ObjectCache.Key> list = new List<ObjectCache.Key>();
			foreach (ObjectCache.Key key in ObjectCache.cache.Keys)
			{
				if (ObjectCache.CanRemove(key) || ObjectCache.CanRemove(ObjectCache.cache[key].Target))
				{
					list.Add(key);
				}
			}
			foreach (ObjectCache.Key key2 in list)
			{
				ObjectCache.cache.Remove(key2);
			}
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x00027FBB File Offset: 0x000261BB
		private static bool CanRemove(object proxy)
		{
			return proxy == null || (proxy is UnityEngine.Object && !(UnityEngine.Object)proxy);
		}

		// Token: 0x04003072 RID: 12402
		private static Dictionary<ObjectCache.Key, WeakReference> cache = new Dictionary<ObjectCache.Key, WeakReference>();

		// Token: 0x02000922 RID: 2338
		private struct Key : IEquatable<ObjectCache.Key>
		{
			// Token: 0x06003B51 RID: 15185 RVA: 0x00027FE6 File Offset: 0x000261E6
			public Key(Type type, object key)
			{
				this.type = type;
				this.obj = key;
			}

			// Token: 0x06003B52 RID: 15186 RVA: 0x00027FF6 File Offset: 0x000261F6
			public override bool Equals(object obj)
			{
				return obj is ObjectCache.Key && this.Equals((ObjectCache.Key)obj);
			}

			// Token: 0x06003B53 RID: 15187 RVA: 0x0002800E File Offset: 0x0002620E
			public bool Equals(ObjectCache.Key other)
			{
				return EqualityComparer<Type>.Default.Equals(this.type, other.type) && EqualityComparer<object>.Default.Equals(this.obj, other.obj);
			}

			// Token: 0x06003B54 RID: 15188 RVA: 0x00028040 File Offset: 0x00026240
			public override int GetHashCode()
			{
				return (1781202625 * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(this.type)) * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this.obj);
			}

			// Token: 0x04003073 RID: 12403
			private readonly Type type;

			// Token: 0x04003074 RID: 12404
			private readonly object obj;
		}
	}
}
