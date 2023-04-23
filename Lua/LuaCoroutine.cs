using System;
using System.Collections;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000920 RID: 2336
	public class LuaCoroutine : IEnumerator
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x00027F73 File Offset: 0x00026173
		// (set) Token: 0x06003B46 RID: 15174 RVA: 0x00027F7B File Offset: 0x0002617B
		public object Current { get; private set; }

		// Token: 0x06003B47 RID: 15175 RVA: 0x00027F84 File Offset: 0x00026184
		public LuaCoroutine(ScriptEngine engine, DynValue coroutine, DynValue[] args)
		{
			this.Current = DynValue.Nil;
			this.console = engine.console;
			this.coroutine = coroutine;
			this.args = (args ?? new DynValue[0]);
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x0012C92C File Offset: 0x0012AB2C
		private object Interpret(DynValue v)
		{
			if (v != null && v.IsNotNil())
			{
				WWaitForSeconds wwaitForSeconds = v.ToObject<WWaitForSeconds>();
				if (wwaitForSeconds != null)
				{
					return new WaitForSeconds(wwaitForSeconds.seconds);
				}
			}
			return null;
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x0012C95C File Offset: 0x0012AB5C
		public bool MoveNext()
		{
			bool result;
			try
			{
				CoroutineState state = this.coroutine.Coroutine.State;
				if (state == CoroutineState.NotStarted || state == CoroutineState.Suspended || state == CoroutineState.ForceSuspended)
				{
					DynValue v = this.coroutine.Coroutine.Resume(this.args);
					this.Current = this.Interpret(v);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (ScriptRuntimeException ex)
			{
				this.console.LogError(ex.DecoratedMessage + " (in coroutine)");
				result = false;
			}
			return result;
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000232E3 File Offset: 0x000214E3
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400306F RID: 12399
		private readonly ScriptConsole console;

		// Token: 0x04003070 RID: 12400
		private readonly DynValue coroutine;

		// Token: 0x04003071 RID: 12401
		private readonly DynValue[] args;
	}
}
