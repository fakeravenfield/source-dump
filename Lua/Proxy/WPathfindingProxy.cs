using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A21 RID: 2593
	[Proxy(typeof(WPathfinding))]
	public class WPathfindingProxy : IProxy
	{
		// Token: 0x06005288 RID: 21128 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x0003CDC6 File Offset: 0x0003AFC6
		public static WPathfindingNodeProxy FindNearestNode(Vector3Proxy position, WPathfindingNodeType type, bool underWater)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WPathfindingNodeProxy.New(WPathfinding.FindNearestNode(position._value, type, underWater));
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		public static WPathfindingNodeProxy FindNearestNode(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return WPathfindingNodeProxy.New(WPathfinding.FindNearestNode(position._value));
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0003CE08 File Offset: 0x0003B008
		public static IList<WPathfindingNode> FindNodes(Vector3Proxy center, float radius, WPathfindingNodeType type, bool underWater)
		{
			if (center == null)
			{
				throw new ScriptRuntimeException("argument 'center' is nil");
			}
			return WPathfinding.FindNodes(center._value, radius, type, underWater);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0003CE26 File Offset: 0x0003B026
		public static IList<WPathfindingNode> FindNodes(Vector3Proxy center, float radius)
		{
			if (center == null)
			{
				throw new ScriptRuntimeException("argument 'center' is nil");
			}
			return WPathfinding.FindNodes(center._value, radius);
		}
	}
}
