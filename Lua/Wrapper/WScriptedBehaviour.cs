using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000990 RID: 2448
	[Wrapper(typeof(ScriptedBehaviour), includeTarget = true)]
	public static class WScriptedBehaviour
	{
		// Token: 0x06003E30 RID: 15920 RVA: 0x0002A096 File Offset: 0x00028296
		[Getter]
		[Doc("Gets the script's Lua table aka. ``self``.")]
		public static DynValue GetSelf(ScriptedBehaviour self)
		{
			return self.GetSelf();
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x0012F2E0 File Offset: 0x0012D4E0
		[Doc("Finds and returns the first ScriptedBehaviour on the GameObject.[..] Equivalent to ``GameObject.GetComponent(ScriptedBehaviour).script``. Nil is return if non is found.")]
		public static DynValue GetScript(GameObject go)
		{
			ScriptedBehaviour script = ScriptedBehaviour.GetScript(go);
			if (script != null)
			{
				return script.GetSelf();
			}
			return DynValue.Nil;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x0002A09E File Offset: 0x0002829E
		[Getter]
		[Doc("The mutator associated with this ScriptedBehaviour.[..] Returns nil if no mutator is associated with this script.")]
		public static MutatorEntry GetMutator(ScriptedBehaviour self)
		{
			return self.sourceMutator;
		}
	}
}
