using System;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200086A RID: 2154
	public class StandardEnumUserDataDescriptor : DispatchingUserDataDescriptor
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x0002447B File Offset: 0x0002267B
		// (set) Token: 0x06003589 RID: 13705 RVA: 0x00024483 File Offset: 0x00022683
		public Type UnderlyingType { get; private set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x0002448C File Offset: 0x0002268C
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x00024494 File Offset: 0x00022694
		public bool IsUnsigned { get; private set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x0002449D File Offset: 0x0002269D
		// (set) Token: 0x0600358D RID: 13709 RVA: 0x000244A5 File Offset: 0x000226A5
		public bool IsFlags { get; private set; }

		// Token: 0x0600358E RID: 13710 RVA: 0x0011B1F8 File Offset: 0x001193F8
		public StandardEnumUserDataDescriptor(Type enumType, string friendlyName = null, string[] names = null, object[] values = null, Type underlyingType = null) : base(enumType, friendlyName)
		{
			if (!Framework.Do.IsEnum(enumType))
			{
				throw new ArgumentException("enumType must be an enum!");
			}
			this.UnderlyingType = (underlyingType ?? Enum.GetUnderlyingType(enumType));
			this.IsUnsigned = (this.UnderlyingType == typeof(byte) || this.UnderlyingType == typeof(ushort) || this.UnderlyingType == typeof(uint) || this.UnderlyingType == typeof(ulong));
			names = (names ?? Enum.GetNames(base.Type));
			values = (values ?? Enum.GetValues(base.Type).OfType<object>().ToArray<object>());
			this.FillMemberList(names, values);
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x0011B2D4 File Offset: 0x001194D4
		private void FillMemberList(string[] names, object[] values)
		{
			for (int i = 0; i < names.Length; i++)
			{
				string name = names[i];
				DynValue value = UserData.Create(values.GetValue(i), this);
				base.AddDynValue(name, value);
			}
			Attribute[] customAttributes = Framework.Do.GetCustomAttributes(base.Type, typeof(FlagsAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				this.IsFlags = true;
				this.AddEnumMethod("flagsAnd", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_And), null));
				this.AddEnumMethod("flagsOr", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Or), null));
				this.AddEnumMethod("flagsXor", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Xor), null));
				this.AddEnumMethod("flagsNot", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_BwNot), null));
				this.AddEnumMethod("hasAll", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_HasAll), null));
				this.AddEnumMethod("hasAny", DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_HasAny), null));
			}
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000244AE File Offset: 0x000226AE
		private void AddEnumMethod(string name, DynValue dynValue)
		{
			if (!base.HasMember(name))
			{
				base.AddDynValue(name, dynValue);
			}
			if (!base.HasMember("__" + name))
			{
				base.AddDynValue("__" + name, dynValue);
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x0011B3E8 File Offset: 0x001195E8
		private long GetValueSigned(DynValue dv)
		{
			this.CreateSignedConversionFunctions();
			if (dv.Type == DataType.Number)
			{
				return (long)dv.Number;
			}
			if (dv.Type != DataType.UserData || dv.UserData.Descriptor != this || dv.UserData.Object == null)
			{
				throw new ScriptRuntimeException("Enum userdata or number expected, or enum is not of the correct type.");
			}
			return this.m_EnumToLong(dv.UserData.Object);
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x0011B454 File Offset: 0x00119654
		private ulong GetValueUnsigned(DynValue dv)
		{
			this.CreateUnsignedConversionFunctions();
			if (dv.Type == DataType.Number)
			{
				return (ulong)dv.Number;
			}
			if (dv.Type != DataType.UserData || dv.UserData.Descriptor != this || dv.UserData.Object == null)
			{
				throw new ScriptRuntimeException("Enum userdata or number expected, or enum is not of the correct type.");
			}
			return this.m_EnumToULong(dv.UserData.Object);
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000244E6 File Offset: 0x000226E6
		private DynValue CreateValueSigned(long value)
		{
			this.CreateSignedConversionFunctions();
			return UserData.Create(this.m_LongToEnum(value), this);
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x00024500 File Offset: 0x00022700
		private DynValue CreateValueUnsigned(ulong value)
		{
			this.CreateUnsignedConversionFunctions();
			return UserData.Create(this.m_ULongToEnum(value), this);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x0011B4C0 File Offset: 0x001196C0
		private void CreateSignedConversionFunctions()
		{
			if (this.m_EnumToLong != null && this.m_LongToEnum != null)
			{
				return;
			}
			if (this.UnderlyingType == typeof(sbyte))
			{
				this.m_EnumToLong = ((object o) => (long)((sbyte)o));
				this.m_LongToEnum = ((long o) => (sbyte)o);
				return;
			}
			if (this.UnderlyingType == typeof(short))
			{
				this.m_EnumToLong = ((object o) => (long)((short)o));
				this.m_LongToEnum = ((long o) => (short)o);
				return;
			}
			if (this.UnderlyingType == typeof(int))
			{
				this.m_EnumToLong = ((object o) => (long)((int)o));
				this.m_LongToEnum = ((long o) => (int)o);
				return;
			}
			if (this.UnderlyingType == typeof(long))
			{
				this.m_EnumToLong = ((object o) => (long)o);
				this.m_LongToEnum = ((long o) => o);
				return;
			}
			throw new ScriptRuntimeException("Unexpected enum underlying type : {0}", new object[]
			{
				this.UnderlyingType.FullName
			});
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x0011B688 File Offset: 0x00119888
		private void CreateUnsignedConversionFunctions()
		{
			if (this.m_EnumToULong != null && this.m_ULongToEnum != null)
			{
				return;
			}
			if (this.UnderlyingType == typeof(byte))
			{
				this.m_EnumToULong = ((object o) => (ulong)((byte)o));
				this.m_ULongToEnum = ((ulong o) => (byte)o);
				return;
			}
			if (this.UnderlyingType == typeof(ushort))
			{
				this.m_EnumToULong = ((object o) => (ulong)((ushort)o));
				this.m_ULongToEnum = ((ulong o) => (ushort)o);
				return;
			}
			if (this.UnderlyingType == typeof(uint))
			{
				this.m_EnumToULong = ((object o) => (ulong)((uint)o));
				this.m_ULongToEnum = ((ulong o) => (uint)o);
				return;
			}
			if (this.UnderlyingType == typeof(ulong))
			{
				this.m_EnumToULong = ((object o) => (ulong)o);
				this.m_ULongToEnum = ((ulong o) => o);
				return;
			}
			throw new ScriptRuntimeException("Unexpected enum underlying type : {0}", new object[]
			{
				this.UnderlyingType.FullName
			});
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0011B850 File Offset: 0x00119A50
		private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, DynValue> operation)
		{
			if (args.Count != 2)
			{
				throw new ScriptRuntimeException("Enum.{0} expects two arguments", new object[]
				{
					funcName
				});
			}
			long valueSigned = this.GetValueSigned(args[0]);
			long valueSigned2 = this.GetValueSigned(args[1]);
			return operation(valueSigned, valueSigned2);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x0011B8A0 File Offset: 0x00119AA0
		private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, DynValue> operation)
		{
			if (args.Count != 2)
			{
				throw new ScriptRuntimeException("Enum.{0} expects two arguments", new object[]
				{
					funcName
				});
			}
			ulong valueUnsigned = this.GetValueUnsigned(args[0]);
			ulong valueUnsigned2 = this.GetValueUnsigned(args[1]);
			return operation(valueUnsigned, valueUnsigned2);
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x0011B8F0 File Offset: 0x00119AF0
		private DynValue PerformBinaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long, long> operation)
		{
			return this.PerformBinaryOperationS(funcName, ctx, args, (long v1, long v2) => this.CreateValueSigned(operation(v1, v2)));
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x0011B928 File Offset: 0x00119B28
		private DynValue PerformBinaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong, ulong> operation)
		{
			return this.PerformBinaryOperationU(funcName, ctx, args, (ulong v1, ulong v2) => this.CreateValueUnsigned(operation(v1, v2)));
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x0011B960 File Offset: 0x00119B60
		private DynValue PerformUnaryOperationS(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<long, long> operation)
		{
			if (args.Count != 1)
			{
				throw new ScriptRuntimeException("Enum.{0} expects one argument.", new object[]
				{
					funcName
				});
			}
			long valueSigned = this.GetValueSigned(args[0]);
			long value = operation(valueSigned);
			return this.CreateValueSigned(value);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x0011B9AC File Offset: 0x00119BAC
		private DynValue PerformUnaryOperationU(string funcName, ScriptExecutionContext ctx, CallbackArguments args, Func<ulong, ulong> operation)
		{
			if (args.Count != 1)
			{
				throw new ScriptRuntimeException("Enum.{0} expects one argument.", new object[]
				{
					funcName
				});
			}
			ulong valueUnsigned = this.GetValueUnsigned(args[0]);
			ulong value = operation(valueUnsigned);
			return this.CreateValueUnsigned(value);
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0011B9F8 File Offset: 0x00119BF8
		internal DynValue Callback_Or(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("or", ctx, args, (ulong v1, ulong v2) => v1 | v2);
			}
			return this.PerformBinaryOperationS("or", ctx, args, (long v1, long v2) => v1 | v2);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x0011BA68 File Offset: 0x00119C68
		internal DynValue Callback_And(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("and", ctx, args, (ulong v1, ulong v2) => v1 & v2);
			}
			return this.PerformBinaryOperationS("and", ctx, args, (long v1, long v2) => v1 & v2);
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x0011BAD8 File Offset: 0x00119CD8
		internal DynValue Callback_Xor(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("xor", ctx, args, (ulong v1, ulong v2) => v1 ^ v2);
			}
			return this.PerformBinaryOperationS("xor", ctx, args, (long v1, long v2) => v1 ^ v2);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x0011BB48 File Offset: 0x00119D48
		internal DynValue Callback_BwNot(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformUnaryOperationU("not", ctx, args, (ulong v1) => ~v1);
			}
			return this.PerformUnaryOperationS("not", ctx, args, (long v1) => ~v1);
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0011BBB8 File Offset: 0x00119DB8
		internal DynValue Callback_HasAll(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("hasAll", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) == v2));
			}
			return this.PerformBinaryOperationS("hasAll", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) == v2));
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0011BC28 File Offset: 0x00119E28
		internal DynValue Callback_HasAny(ScriptExecutionContext ctx, CallbackArguments args)
		{
			if (this.IsUnsigned)
			{
				return this.PerformBinaryOperationU("hasAny", ctx, args, (ulong v1, ulong v2) => DynValue.NewBoolean((v1 & v2) > 0UL));
			}
			return this.PerformBinaryOperationS("hasAny", ctx, args, (long v1, long v2) => DynValue.NewBoolean((v1 & v2) != 0L));
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x0002451A File Offset: 0x0002271A
		public override bool IsTypeCompatible(Type type, object obj)
		{
			if (obj != null)
			{
				return base.Type == type;
			}
			return base.IsTypeCompatible(type, obj);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00024534 File Offset: 0x00022734
		public override DynValue MetaIndex(Script script, object obj, string metaname)
		{
			if (metaname == "__concat" && this.IsFlags)
			{
				return DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.Callback_Or), null);
			}
			return null;
		}

		// Token: 0x04002E2F RID: 11823
		private Func<object, ulong> m_EnumToULong;

		// Token: 0x04002E30 RID: 11824
		private Func<ulong, object> m_ULongToEnum;

		// Token: 0x04002E31 RID: 11825
		private Func<object, long> m_EnumToLong;

		// Token: 0x04002E32 RID: 11826
		private Func<long, object> m_LongToEnum;
	}
}
