using System;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C6 RID: 1990
	[Serializable]
	public class ScriptRuntimeException : InterpreterException
	{
		// Token: 0x0600318E RID: 12686 RVA: 0x000222FB File Offset: 0x000204FB
		public ScriptRuntimeException(Exception ex) : base(ex)
		{
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x00022304 File Offset: 0x00020504
		public ScriptRuntimeException(ScriptRuntimeException ex) : base(ex, ex.DecoratedMessage)
		{
			base.DecoratedMessage = this.Message;
			base.DoNotDecorateMessage = true;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x00022273 File Offset: 0x00020473
		public ScriptRuntimeException(string message) : base(message)
		{
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x0002227C File Offset: 0x0002047C
		public ScriptRuntimeException(string format, params object[] args) : base(format, args)
		{
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x0010F554 File Offset: 0x0010D754
		public static ScriptRuntimeException ArithmeticOnNonNumber(DynValue l, DynValue r = null)
		{
			if (l.Type != DataType.Number && l.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to perform arithmetic on a {0} value", new object[]
				{
					l.Type.ToLuaTypeString()
				});
			}
			if (r != null && r.Type != DataType.Number && r.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to perform arithmetic on a {0} value", new object[]
				{
					r.Type.ToLuaTypeString()
				});
			}
			if (l.Type == DataType.String || (r != null && r.Type == DataType.String))
			{
				return new ScriptRuntimeException("attempt to perform arithmetic on a string value");
			}
			throw new InternalErrorException("ArithmeticOnNonNumber - both are numbers");
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x0010F5F0 File Offset: 0x0010D7F0
		public static ScriptRuntimeException ConcatOnNonString(DynValue l, DynValue r)
		{
			if (l.Type != DataType.Number && l.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to concatenate a {0} value", new object[]
				{
					l.Type.ToLuaTypeString()
				});
			}
			if (r != null && r.Type != DataType.Number && r.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to concatenate a {0} value", new object[]
				{
					r.Type.ToLuaTypeString()
				});
			}
			throw new InternalErrorException("ConcatOnNonString - both are numbers/strings");
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x00022326 File Offset: 0x00020526
		public static ScriptRuntimeException LenOnInvalidType(DynValue r)
		{
			return new ScriptRuntimeException("attempt to get length of a {0} value", new object[]
			{
				r.Type.ToLuaTypeString()
			});
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x0010F66C File Offset: 0x0010D86C
		public static ScriptRuntimeException CompareInvalidType(DynValue l, DynValue r)
		{
			if (l.Type.ToLuaTypeString() == r.Type.ToLuaTypeString())
			{
				return new ScriptRuntimeException("attempt to compare two {0} values", new object[]
				{
					l.Type.ToLuaTypeString()
				});
			}
			return new ScriptRuntimeException("attempt to compare {0} with {1}", new object[]
			{
				l.Type.ToLuaTypeString(),
				r.Type.ToLuaTypeString()
			});
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x00022346 File Offset: 0x00020546
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, string message)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2})", new object[]
			{
				argNum + 1,
				funcName,
				message
			});
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x0010F6E4 File Offset: 0x0010D8E4
		public static ScriptRuntimeException BadArgumentUserData(int argNum, string funcName, Type expected, object got, bool allowNil)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (userdata<{2}>{3} expected, got {4})", new object[]
			{
				argNum + 1,
				funcName,
				expected.Name,
				allowNil ? "nil or " : "",
				(got != null) ? ("userdata<" + got.GetType().Name + ">") : "null"
			});
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x0002236B File Offset: 0x0002056B
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, DataType expected, DataType got, bool allowNil)
		{
			return ScriptRuntimeException.BadArgument(argNum, funcName, expected.ToErrorTypeString(), got.ToErrorTypeString(), allowNil);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x00022382 File Offset: 0x00020582
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, string expected, string got, bool allowNil)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2}{3} expected, got {4})", new object[]
			{
				argNum + 1,
				funcName,
				allowNil ? "nil or " : "",
				expected,
				got
			});
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000223BE File Offset: 0x000205BE
		public static ScriptRuntimeException BadArgumentNoValue(int argNum, string funcName, DataType expected)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2} expected, got no value)", new object[]
			{
				argNum + 1,
				funcName,
				expected.ToErrorTypeString()
			});
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000223E8 File Offset: 0x000205E8
		public static ScriptRuntimeException BadArgumentIndexOutOfRange(string funcName, int argNum)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (index out of range)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00022409 File Offset: 0x00020609
		public static ScriptRuntimeException BadArgumentNoNegativeNumbers(int argNum, string funcName)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (not a non-negative number in proper range)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x0002242A File Offset: 0x0002062A
		public static ScriptRuntimeException BadArgumentValueExpected(int argNum, string funcName)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (value expected)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x0002244B File Offset: 0x0002064B
		public static ScriptRuntimeException IndexType(DynValue obj)
		{
			return new ScriptRuntimeException("attempt to index a {0} value", new object[]
			{
				obj.Type.ToLuaTypeString()
			});
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x0002246B File Offset: 0x0002066B
		public static ScriptRuntimeException LoopInIndex()
		{
			return new ScriptRuntimeException("loop in gettable");
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x00022477 File Offset: 0x00020677
		public static ScriptRuntimeException LoopInNewIndex()
		{
			return new ScriptRuntimeException("loop in settable");
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x00022483 File Offset: 0x00020683
		public static ScriptRuntimeException LoopInCall()
		{
			return new ScriptRuntimeException("loop in call");
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x0002248F File Offset: 0x0002068F
		public static ScriptRuntimeException TableIndexIsNil()
		{
			return new ScriptRuntimeException("table index is nil");
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0002249B File Offset: 0x0002069B
		public static ScriptRuntimeException TableIndexIsNaN()
		{
			return new ScriptRuntimeException("table index is NaN");
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0010F754 File Offset: 0x0010D954
		public static ScriptRuntimeException ConvertToNumberFailed(int stage)
		{
			switch (stage)
			{
			case 1:
				return new ScriptRuntimeException("'for' initial value must be a number");
			case 2:
				return new ScriptRuntimeException("'for' step must be a number");
			case 3:
				return new ScriptRuntimeException("'for' limit must be a number");
			default:
				return new ScriptRuntimeException("value must be a number");
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000224A7 File Offset: 0x000206A7
		public static ScriptRuntimeException ConvertObjectFailed(object obj)
		{
			return new ScriptRuntimeException("cannot convert clr type {0}", new object[]
			{
				obj.GetType()
			});
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000224C2 File Offset: 0x000206C2
		public static ScriptRuntimeException ConvertObjectFailed(DataType t)
		{
			return new ScriptRuntimeException("cannot convert a {0} to a clr type", new object[]
			{
				t.ToString().ToLowerInvariant()
			});
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000224E9 File Offset: 0x000206E9
		public static ScriptRuntimeException ConvertObjectFailed(DataType t, Type t2)
		{
			return new ScriptRuntimeException("cannot convert a {0} to a clr type {1}", new object[]
			{
				t.ToString().ToLowerInvariant(),
				t2.FullName
			});
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00022519 File Offset: 0x00020719
		public static ScriptRuntimeException UserDataArgumentTypeMismatch(DataType t, Type clrType)
		{
			return new ScriptRuntimeException("cannot find a conversion from a MoonSharp {0} to a clr {1}", new object[]
			{
				t.ToString().ToLowerInvariant(),
				clrType.FullName
			});
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00022549 File Offset: 0x00020749
		public static ScriptRuntimeException UserDataMissingField(string typename, string fieldname)
		{
			return new ScriptRuntimeException("cannot access field {0} of userdata<{1}>", new object[]
			{
				fieldname,
				typename
			});
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x00022563 File Offset: 0x00020763
		public static ScriptRuntimeException CannotResumeNotSuspended(CoroutineState state)
		{
			if (state == CoroutineState.Dead)
			{
				return new ScriptRuntimeException("cannot resume dead coroutine");
			}
			return new ScriptRuntimeException("cannot resume non-suspended coroutine");
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x0002257E File Offset: 0x0002077E
		public static ScriptRuntimeException CannotYield()
		{
			return new ScriptRuntimeException("attempt to yield across a CLR-call boundary");
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x0002258A File Offset: 0x0002078A
		public static ScriptRuntimeException CannotYieldMain()
		{
			return new ScriptRuntimeException("attempt to yield from outside a coroutine");
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x0010F7A4 File Offset: 0x0010D9A4
		public static ScriptRuntimeException AttemptToCallNonFunc(DataType type, string debugText = null)
		{
			string text = type.ToErrorTypeString();
			if (debugText != null)
			{
				return new ScriptRuntimeException("attempt to call a {0} value near '{1}'", new object[]
				{
					text,
					debugText
				});
			}
			return new ScriptRuntimeException("attempt to call a {0} value", new object[]
			{
				text
			});
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x00022596 File Offset: 0x00020796
		public static ScriptRuntimeException AccessInstanceMemberOnStatics(IMemberDescriptor desc)
		{
			return new ScriptRuntimeException("attempt to access instance member {0} from a static userdata", new object[]
			{
				desc.Name
			});
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000225B1 File Offset: 0x000207B1
		public static ScriptRuntimeException AccessInstanceMemberOnStatics(IUserDataDescriptor typeDescr, IMemberDescriptor desc)
		{
			return new ScriptRuntimeException("attempt to access instance member {0}.{1} from a static userdata", new object[]
			{
				typeDescr.Name,
				desc.Name
			});
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000225D5 File Offset: 0x000207D5
		public override void Rethrow()
		{
			if (Script.GlobalOptions.RethrowExceptionNested)
			{
				throw new ScriptRuntimeException(this);
			}
		}
	}
}
