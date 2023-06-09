﻿using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x020007A2 RID: 1954
	internal static class VariableInspector
	{
		// Token: 0x06003032 RID: 12338 RVA: 0x0010D468 File Offset: 0x0010B668
		internal static void InspectVariable(DynValue v, List<Variable> variables)
		{
			variables.Add(new Variable("(value)", v.ToPrintString(), 0));
			variables.Add(new Variable("(type)", v.Type.ToLuaDebuggerString(), 0));
			variables.Add(new Variable("(val #id)", v.ReferenceID.ToString(), 0));
			switch (v.Type)
			{
			case DataType.Nil:
			case DataType.Void:
			case DataType.Boolean:
			case DataType.Number:
			case DataType.String:
			case DataType.TailCallRequest:
			case DataType.YieldRequest:
				return;
			case DataType.Function:
				variables.Add(new Variable("(address)", v.Function.EntryPointByteCodeLocation.ToString("X8"), 0));
				variables.Add(new Variable("(upvalues)", v.Function.GetUpvaluesCount().ToString(), 0));
				variables.Add(new Variable("(upvalues type)", v.Function.GetUpvaluesType().ToString(), 0));
				return;
			case DataType.Table:
				if (v.Table.MetaTable != null && v.Table.OwnerScript == null)
				{
					variables.Add(new Variable("(table type)", "prime table with metatable", 0));
				}
				else if (v.Table.MetaTable != null)
				{
					variables.Add(new Variable("(table type)", "has metatable", 0));
				}
				else if (v.Table.OwnerScript == null)
				{
					variables.Add(new Variable("(table type)", "prime table", 0));
				}
				else
				{
					variables.Add(new Variable("(table type)", "standard", 0));
				}
				variables.Add(new Variable("(table #id)", v.Table.ReferenceID.ToString(), 0));
				if (v.Table.MetaTable != null)
				{
					variables.Add(new Variable("(metatable #id)", v.Table.MetaTable.ReferenceID.ToString(), 0));
				}
				variables.Add(new Variable("(length)", v.Table.Length.ToString(), 0));
				using (IEnumerator<TablePair> enumerator = v.Table.Pairs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TablePair tablePair = enumerator.Current;
						variables.Add(new Variable("[" + tablePair.Key.ToDebugPrintString() + "]", tablePair.Value.ToDebugPrintString(), 0));
					}
					return;
				}
				break;
			case DataType.Tuple:
				for (int i = 0; i < v.Tuple.Length; i++)
				{
					variables.Add(new Variable("[i]", (v.Tuple[i] ?? DynValue.Void).ToDebugPrintString(), 0));
				}
				return;
			case DataType.UserData:
				break;
			case DataType.Thread:
				variables.Add(new Variable("(coroutine state)", v.Coroutine.State.ToString(), 0));
				variables.Add(new Variable("(coroutine type)", v.Coroutine.Type.ToString(), 0));
				variables.Add(new Variable("(auto-yield counter)", v.Coroutine.AutoYieldCounter.ToString(), 0));
				return;
			case DataType.ClrFunction:
				variables.Add(new Variable("(name)", v.Callback.Name ?? "(unnamed)", 0));
				return;
			default:
				return;
			}
			if (v.UserData.Descriptor != null)
			{
				variables.Add(new Variable("(descriptor)", v.UserData.Descriptor.Name, 0));
				variables.Add(new Variable("(native type)", v.UserData.Descriptor.Type.ToString(), 0));
			}
			else
			{
				variables.Add(new Variable("(descriptor)", "null!", 0));
			}
			variables.Add(new Variable("(native object)", (v.UserData.Object != null) ? v.UserData.Object.ToString() : "(null)", 0));
		}
	}
}
