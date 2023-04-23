using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007B1 RID: 1969
	public sealed class DynValue
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000216C8 File Offset: 0x0001F8C8
		public int ReferenceID
		{
			get
			{
				return this.m_RefID;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000216D0 File Offset: 0x0001F8D0
		public DataType Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000216D8 File Offset: 0x0001F8D8
		public Closure Function
		{
			get
			{
				return this.m_Object as Closure;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000216E5 File Offset: 0x0001F8E5
		public double Number
		{
			get
			{
				return this.m_Number;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000216ED File Offset: 0x0001F8ED
		public DynValue[] Tuple
		{
			get
			{
				return this.m_Object as DynValue[];
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000216FA File Offset: 0x0001F8FA
		public Coroutine Coroutine
		{
			get
			{
				return this.m_Object as Coroutine;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x00021707 File Offset: 0x0001F907
		public Table Table
		{
			get
			{
				return this.m_Object as Table;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x00021714 File Offset: 0x0001F914
		public bool Boolean
		{
			get
			{
				return this.Number != 0.0;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x0002172A File Offset: 0x0001F92A
		public string String
		{
			get
			{
				return this.m_Object as string;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x00021737 File Offset: 0x0001F937
		public CallbackFunction Callback
		{
			get
			{
				return this.m_Object as CallbackFunction;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x00021744 File Offset: 0x0001F944
		public TailCallData TailCallData
		{
			get
			{
				return this.m_Object as TailCallData;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x00021751 File Offset: 0x0001F951
		public YieldRequest YieldRequest
		{
			get
			{
				return this.m_Object as YieldRequest;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x0002175E File Offset: 0x0001F95E
		public UserData UserData
		{
			get
			{
				return this.m_Object as UserData;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x0002176B File Offset: 0x0001F96B
		public bool ReadOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x00021773 File Offset: 0x0001F973
		public static DynValue NewNil()
		{
			return new DynValue();
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0002177A File Offset: 0x0001F97A
		public static DynValue NewBoolean(bool v)
		{
			return new DynValue
			{
				m_Number = (double)(v ? 1 : 0),
				m_Type = DataType.Boolean
			};
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x00021796 File Offset: 0x0001F996
		public static DynValue NewNumber(double num)
		{
			return new DynValue
			{
				m_Number = num,
				m_Type = DataType.Number,
				m_HashCode = -1
			};
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000217B2 File Offset: 0x0001F9B2
		public static DynValue NewString(string str)
		{
			return new DynValue
			{
				m_Object = str,
				m_Type = DataType.String
			};
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000217C7 File Offset: 0x0001F9C7
		public static DynValue NewString(StringBuilder sb)
		{
			return new DynValue
			{
				m_Object = sb.ToString(),
				m_Type = DataType.String
			};
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000217E1 File Offset: 0x0001F9E1
		public static DynValue NewString(string format, params object[] args)
		{
			return new DynValue
			{
				m_Object = string.Format(format, args),
				m_Type = DataType.String
			};
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000217FC File Offset: 0x0001F9FC
		public static DynValue NewCoroutine(Coroutine coroutine)
		{
			return new DynValue
			{
				m_Object = coroutine,
				m_Type = DataType.Thread
			};
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x00021812 File Offset: 0x0001FA12
		public static DynValue NewClosure(Closure function)
		{
			return new DynValue
			{
				m_Object = function,
				m_Type = DataType.Function
			};
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x00021827 File Offset: 0x0001FA27
		public static DynValue NewCallback(Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack, string name = null)
		{
			return new DynValue
			{
				m_Object = new CallbackFunction(callBack, name),
				m_Type = DataType.ClrFunction
			};
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x00021843 File Offset: 0x0001FA43
		public static DynValue NewCallback(CallbackFunction function)
		{
			return new DynValue
			{
				m_Object = function,
				m_Type = DataType.ClrFunction
			};
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x00021859 File Offset: 0x0001FA59
		public static DynValue NewTable(Table table)
		{
			return new DynValue
			{
				m_Object = table,
				m_Type = DataType.Table
			};
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0002186E File Offset: 0x0001FA6E
		public static DynValue NewPrimeTable()
		{
			return DynValue.NewTable(new Table(null));
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0002187B File Offset: 0x0001FA7B
		public static DynValue NewTable(Script script)
		{
			return DynValue.NewTable(new Table(script));
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x00021888 File Offset: 0x0001FA88
		public static DynValue NewTable(Script script, params DynValue[] arrayValues)
		{
			return DynValue.NewTable(new Table(script, arrayValues));
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x00021896 File Offset: 0x0001FA96
		public static DynValue NewTailCallReq(DynValue tailFn, params DynValue[] args)
		{
			return new DynValue
			{
				m_Object = new TailCallData
				{
					Args = args,
					Function = tailFn
				},
				m_Type = DataType.TailCallRequest
			};
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000218BE File Offset: 0x0001FABE
		public static DynValue NewTailCallReq(TailCallData tailCallData)
		{
			return new DynValue
			{
				m_Object = tailCallData,
				m_Type = DataType.TailCallRequest
			};
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000218D4 File Offset: 0x0001FAD4
		public static DynValue NewYieldReq(DynValue[] args)
		{
			return new DynValue
			{
				m_Object = new YieldRequest
				{
					ReturnValues = args
				},
				m_Type = DataType.YieldRequest
			};
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000218F5 File Offset: 0x0001FAF5
		internal static DynValue NewForcedYieldReq()
		{
			return new DynValue
			{
				m_Object = new YieldRequest
				{
					Forced = true
				},
				m_Type = DataType.YieldRequest
			};
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x00021916 File Offset: 0x0001FB16
		public static DynValue NewTuple(params DynValue[] values)
		{
			if (values.Length == 0)
			{
				return DynValue.NewNil();
			}
			if (values.Length == 1)
			{
				return values[0];
			}
			return new DynValue
			{
				m_Object = values,
				m_Type = DataType.Tuple
			};
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x0010E254 File Offset: 0x0010C454
		public static DynValue NewTupleNested(params DynValue[] values)
		{
			if (!values.Any((DynValue v) => v.Type == DataType.Tuple))
			{
				return DynValue.NewTuple(values);
			}
			if (values.Length == 1)
			{
				return values[0];
			}
			List<DynValue> list = new List<DynValue>();
			foreach (DynValue dynValue in values)
			{
				if (dynValue.Type == DataType.Tuple)
				{
					list.AddRange(dynValue.Tuple);
				}
				else
				{
					list.Add(dynValue);
				}
			}
			return new DynValue
			{
				m_Object = list.ToArray(),
				m_Type = DataType.Tuple
			};
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0002193F File Offset: 0x0001FB3F
		public static DynValue NewUserData(UserData userData)
		{
			return new DynValue
			{
				m_Object = userData,
				m_Type = DataType.UserData
			};
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x00021954 File Offset: 0x0001FB54
		public DynValue AsReadOnly()
		{
			if (this.ReadOnly)
			{
				return this;
			}
			return this.Clone(true);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x00021967 File Offset: 0x0001FB67
		public DynValue Clone()
		{
			return this.Clone(this.ReadOnly);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x00021975 File Offset: 0x0001FB75
		public DynValue Clone(bool readOnly)
		{
			return new DynValue
			{
				m_Object = this.m_Object,
				m_Number = this.m_Number,
				m_HashCode = this.m_HashCode,
				m_Type = this.m_Type,
				m_ReadOnly = readOnly
			};
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000219B3 File Offset: 0x0001FBB3
		public DynValue CloneAsWritable()
		{
			return this.Clone(false);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000219BC File Offset: 0x0001FBBC
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x000219C3 File Offset: 0x0001FBC3
		public static DynValue Void { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000219CB File Offset: 0x0001FBCB
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x000219D2 File Offset: 0x0001FBD2
		public static DynValue Nil { get; private set; } = new DynValue
		{
			m_Type = DataType.Nil
		}.AsReadOnly();

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000219DA File Offset: 0x0001FBDA
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x000219E1 File Offset: 0x0001FBE1
		public static DynValue True { get; private set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000219E9 File Offset: 0x0001FBE9
		// (set) Token: 0x060030CB RID: 12491 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public static DynValue False { get; private set; }

		// Token: 0x060030CC RID: 12492 RVA: 0x0010E2EC File Offset: 0x0010C4EC
		static DynValue()
		{
			DynValue.Void = new DynValue
			{
				m_Type = DataType.Void
			}.AsReadOnly();
			DynValue.True = DynValue.NewBoolean(true).AsReadOnly();
			DynValue.False = DynValue.NewBoolean(false).AsReadOnly();
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0010E348 File Offset: 0x0010C548
		public string ToPrintString()
		{
			if (this.m_Object != null && this.m_Object is RefIdObject)
			{
				RefIdObject refIdObject = (RefIdObject)this.m_Object;
				string typeString = this.Type.ToLuaTypeString();
				if (this.m_Object is UserData)
				{
					UserData userData = (UserData)this.m_Object;
					string text = userData.Descriptor.AsString(userData.Object);
					if (text != null)
					{
						return text;
					}
				}
				return refIdObject.FormatTypeString(typeString);
			}
			DataType type = this.Type;
			if (type <= DataType.Tuple)
			{
				if (type == DataType.String)
				{
					return this.String;
				}
				if (type == DataType.Tuple)
				{
					return string.Join("\t", (from t in this.Tuple
					select t.ToPrintString()).ToArray<string>());
				}
			}
			else
			{
				if (type == DataType.TailCallRequest)
				{
					return "(TailCallRequest -- INTERNAL!)";
				}
				if (type == DataType.YieldRequest)
				{
					return "(YieldRequest -- INTERNAL!)";
				}
			}
			return this.ToString();
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0010E434 File Offset: 0x0010C634
		public string ToDebugPrintString()
		{
			if (this.m_Object != null && this.m_Object is RefIdObject)
			{
				RefIdObject refIdObject = (RefIdObject)this.m_Object;
				string typeString = this.Type.ToLuaTypeString();
				if (this.m_Object is UserData)
				{
					UserData userData = (UserData)this.m_Object;
					string text = userData.Descriptor.AsString(userData.Object);
					if (text != null)
					{
						return text;
					}
				}
				return refIdObject.FormatTypeString(typeString);
			}
			DataType type = this.Type;
			if (type == DataType.Tuple)
			{
				return string.Join("\t", (from t in this.Tuple
				select t.ToPrintString()).ToArray<string>());
			}
			if (type == DataType.TailCallRequest)
			{
				return "(TailCallRequest)";
			}
			if (type != DataType.YieldRequest)
			{
				return this.ToString();
			}
			return "(YieldRequest)";
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0010E510 File Offset: 0x0010C710
		public override string ToString()
		{
			switch (this.Type)
			{
			case DataType.Nil:
				return "nil";
			case DataType.Void:
				return "void";
			case DataType.Boolean:
				return this.Boolean.ToString().ToLower();
			case DataType.Number:
				return this.Number.ToString(CultureInfo.InvariantCulture);
			case DataType.String:
				return "\"" + this.String + "\"";
			case DataType.Function:
				return string.Format("(Function {0:X8})", this.Function.EntryPointByteCodeLocation);
			case DataType.Table:
				return "(Table)";
			case DataType.Tuple:
				return string.Join(", ", (from t in this.Tuple
				select t.ToString()).ToArray<string>());
			case DataType.UserData:
				return "(UserData)";
			case DataType.Thread:
				return string.Format("(Coroutine {0:X8})", this.Coroutine.ReferenceID);
			case DataType.ClrFunction:
				return string.Format("(Function CLR)", this.Function);
			case DataType.TailCallRequest:
				return "Tail:(" + string.Join(", ", (from t in this.Tuple
				select t.ToString()).ToArray<string>()) + ")";
			default:
				return "(???)";
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0010E684 File Offset: 0x0010C884
		public override int GetHashCode()
		{
			if (this.m_HashCode != -1)
			{
				return this.m_HashCode;
			}
			int num = (int)((int)this.Type << 27);
			switch (this.Type)
			{
			case DataType.Nil:
			case DataType.Void:
				this.m_HashCode = 0;
				goto IL_10B;
			case DataType.Boolean:
				this.m_HashCode = (this.Boolean ? 1 : 2);
				goto IL_10B;
			case DataType.Number:
				this.m_HashCode = (num ^ this.Number.GetHashCode());
				goto IL_10B;
			case DataType.String:
				this.m_HashCode = (num ^ this.String.GetHashCode());
				goto IL_10B;
			case DataType.Function:
				this.m_HashCode = (num ^ this.Function.GetHashCode());
				goto IL_10B;
			case DataType.Table:
				this.m_HashCode = (num ^ this.Table.GetHashCode());
				goto IL_10B;
			case DataType.Tuple:
			case DataType.TailCallRequest:
				this.m_HashCode = (num ^ this.Tuple.GetHashCode());
				goto IL_10B;
			case DataType.ClrFunction:
				this.m_HashCode = (num ^ this.Callback.GetHashCode());
				goto IL_10B;
			}
			this.m_HashCode = 999;
			IL_10B:
			return this.m_HashCode;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0010E7A4 File Offset: 0x0010C9A4
		public override bool Equals(object obj)
		{
			DynValue dynValue = obj as DynValue;
			if (dynValue == null)
			{
				return false;
			}
			if ((dynValue.Type == DataType.Nil && this.Type == DataType.Void) || (dynValue.Type == DataType.Void && this.Type == DataType.Nil))
			{
				return true;
			}
			if (dynValue.Type != this.Type)
			{
				return false;
			}
			switch (this.Type)
			{
			case DataType.Nil:
			case DataType.Void:
				return true;
			case DataType.Boolean:
				return this.Boolean == dynValue.Boolean;
			case DataType.Number:
				return this.Number == dynValue.Number;
			case DataType.String:
				return this.String == dynValue.String;
			case DataType.Function:
				return this.Function == dynValue.Function;
			case DataType.Table:
				return this.Table == dynValue.Table;
			case DataType.Tuple:
			case DataType.TailCallRequest:
				return this.Tuple == dynValue.Tuple;
			case DataType.UserData:
			{
				UserData userData = this.UserData;
				UserData userData2 = dynValue.UserData;
				return userData != null && userData2 != null && userData.Descriptor == userData2.Descriptor && ((userData.Object == null && userData2.Object == null) || (userData.Object != null && userData2.Object != null && userData.Object.Equals(userData2.Object)));
			}
			case DataType.Thread:
				return this.Coroutine == dynValue.Coroutine;
			case DataType.ClrFunction:
				return this.Callback == dynValue.Callback;
			default:
				return this == dynValue;
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0010E910 File Offset: 0x0010CB10
		public string CastToString()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Number)
			{
				return dynValue.Number.ToString();
			}
			if (dynValue.Type == DataType.String)
			{
				return dynValue.String;
			}
			return null;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0010E950 File Offset: 0x0010CB50
		public double? CastToNumber()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Number)
			{
				return new double?(dynValue.Number);
			}
			double value;
			if (dynValue.Type == DataType.String && double.TryParse(dynValue.String, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
			{
				return new double?(value);
			}
			return null;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0010E9AC File Offset: 0x0010CBAC
		public bool CastToBool()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Boolean)
			{
				return dynValue.Boolean;
			}
			return dynValue.Type != DataType.Nil && dynValue.Type != DataType.Void;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000219F8 File Offset: 0x0001FBF8
		public IScriptPrivateResource GetAsPrivateResource()
		{
			return this.m_Object as IScriptPrivateResource;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0010E9E8 File Offset: 0x0010CBE8
		public DynValue ToScalar()
		{
			if (this.Type == DataType.UserData)
			{
				if (!this.UserData.ProxyValueReferenceEvaluatesToNil())
				{
					return this;
				}
				return DynValue.Nil;
			}
			else
			{
				if (this.Type != DataType.Tuple)
				{
					return this;
				}
				if (this.Tuple.Length == 0)
				{
					return DynValue.Void;
				}
				return this.Tuple[0].ToScalar();
			}
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x00021A05 File Offset: 0x0001FC05
		public void Assign(DynValue value)
		{
			if (this.ReadOnly)
			{
				throw new ScriptRuntimeException("Assigning on r-value");
			}
			this.m_Number = value.m_Number;
			this.m_Object = value.m_Object;
			this.m_Type = value.Type;
			this.m_HashCode = -1;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0010EA3C File Offset: 0x0010CC3C
		public DynValue GetLength()
		{
			if (this.Type == DataType.Table)
			{
				return DynValue.NewNumber((double)this.Table.Length);
			}
			if (this.Type == DataType.String)
			{
				return DynValue.NewNumber((double)this.String.Length);
			}
			throw new ScriptRuntimeException("Can't get length of type {0}", new object[]
			{
				this.Type
			});
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x00021A45 File Offset: 0x0001FC45
		public bool IsNil()
		{
			return this.Type == DataType.Nil || this.Type == DataType.Void;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00021A5A File Offset: 0x0001FC5A
		public bool IsNotNil()
		{
			return this.Type != DataType.Nil && this.Type != DataType.Void;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x00021A72 File Offset: 0x0001FC72
		public bool IsVoid()
		{
			return this.Type == DataType.Void;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x00021A7D File Offset: 0x0001FC7D
		public bool IsNotVoid()
		{
			return this.Type != DataType.Void;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x00021A8B File Offset: 0x0001FC8B
		public bool IsNilOrNan()
		{
			return this.Type == DataType.Nil || this.Type == DataType.Void || (this.Type == DataType.Number && double.IsNaN(this.Number));
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0010EAA0 File Offset: 0x0010CCA0
		internal void AssignNumber(double num)
		{
			if (this.ReadOnly)
			{
				throw new InternalErrorException(null, new object[]
				{
					"Writing on r-value"
				});
			}
			if (this.Type != DataType.Number)
			{
				throw new InternalErrorException("Can't assign number to type {0}", new object[]
				{
					this.Type
				});
			}
			this.m_Number = num;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x00021AB6 File Offset: 0x0001FCB6
		public static DynValue FromObject(Script script, object obj)
		{
			return ClrToScriptConversions.ObjectToDynValue(script, obj, null);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public object ToObject()
		{
			return ScriptToClrConversions.DynValueToObject(this);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		public object ToObject(Type desiredType)
		{
			return ScriptToClrConversions.DynValueToObjectOfType(this, desiredType, null, false);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x00021AD3 File Offset: 0x0001FCD3
		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0010EAFC File Offset: 0x0010CCFC
		public DynValue CheckType(string funcName, DataType desiredType, int argNum = -1, TypeValidationFlags flags = TypeValidationFlags.AutoConvert)
		{
			if (this.Type == desiredType)
			{
				return this;
			}
			bool flag = (flags & TypeValidationFlags.AllowNil) > TypeValidationFlags.None;
			if (flag && this.IsNil())
			{
				return this;
			}
			if ((flags & TypeValidationFlags.AutoConvert) > TypeValidationFlags.None)
			{
				if (desiredType == DataType.Boolean)
				{
					return DynValue.NewBoolean(this.CastToBool());
				}
				if (desiredType == DataType.Number)
				{
					double? num = this.CastToNumber();
					if (num != null)
					{
						return DynValue.NewNumber(num.Value);
					}
				}
				if (desiredType == DataType.String)
				{
					string text = this.CastToString();
					if (text != null)
					{
						return DynValue.NewString(text);
					}
				}
			}
			if (this.IsVoid())
			{
				throw ScriptRuntimeException.BadArgumentNoValue(argNum, funcName, desiredType);
			}
			throw ScriptRuntimeException.BadArgument(argNum, funcName, desiredType, this.Type, flag);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x0010EB98 File Offset: 0x0010CD98
		public T CheckUserDataType<T>(string funcName, int argNum = -1, TypeValidationFlags flags = TypeValidationFlags.AutoConvert)
		{
			DynValue dynValue = this.CheckType(funcName, DataType.UserData, argNum, flags);
			bool allowNil = (flags & TypeValidationFlags.AllowNil) > TypeValidationFlags.None;
			if (dynValue.IsNil())
			{
				return default(T);
			}
			object @object = dynValue.UserData.Object;
			if (@object != null && @object is T)
			{
				return (T)((object)@object);
			}
			throw ScriptRuntimeException.BadArgumentUserData(argNum, funcName, typeof(T), @object, allowNil);
		}

		// Token: 0x04002BED RID: 11245
		private static int s_RefIDCounter;

		// Token: 0x04002BEE RID: 11246
		private int m_RefID = ++DynValue.s_RefIDCounter;

		// Token: 0x04002BEF RID: 11247
		private int m_HashCode = -1;

		// Token: 0x04002BF0 RID: 11248
		private bool m_ReadOnly;

		// Token: 0x04002BF1 RID: 11249
		private double m_Number;

		// Token: 0x04002BF2 RID: 11250
		private object m_Object;

		// Token: 0x04002BF3 RID: 11251
		private DataType m_Type;
	}
}
