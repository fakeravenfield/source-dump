using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000844 RID: 2116
	public class ArrayMemberDescriptor : ObjectCallbackMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x06003470 RID: 13424 RVA: 0x00023D46 File Offset: 0x00021F46
		public ArrayMemberDescriptor(string name, bool isSetter, ParameterDescriptor[] indexerParams) : base(name, isSetter ? new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerSet) : new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerGet), indexerParams)
		{
			this.m_IsSetter = isSetter;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x00023D74 File Offset: 0x00021F74
		public ArrayMemberDescriptor(string name, bool isSetter) : base(name, isSetter ? new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerSet) : new Func<object, ScriptExecutionContext, CallbackArguments, object>(ArrayMemberDescriptor.ArrayIndexerGet))
		{
			this.m_IsSetter = isSetter;
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x00118644 File Offset: 0x00116844
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(base.Name));
			t.Set("setter", DynValue.NewBoolean(this.m_IsSetter));
			if (base.Parameters != null)
			{
				DynValue dynValue = DynValue.NewPrimeTable();
				t.Set("params", dynValue);
				int num = 0;
				foreach (ParameterDescriptor parameterDescriptor in base.Parameters)
				{
					DynValue dynValue2 = DynValue.NewPrimeTable();
					dynValue.Table.Set(++num, dynValue2);
					parameterDescriptor.PrepareForWiring(dynValue2.Table);
				}
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x001186F4 File Offset: 0x001168F4
		private static int[] BuildArrayIndices(CallbackArguments args, int count)
		{
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = args.AsInt(i, "userdata_array_indexer");
			}
			return array;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00118724 File Offset: 0x00116924
		private static object ArrayIndexerSet(object arrayObj, ScriptExecutionContext ctx, CallbackArguments args)
		{
			Array array = (Array)arrayObj;
			int[] indices = ArrayMemberDescriptor.BuildArrayIndices(args, args.Count - 1);
			DynValue value = args[args.Count - 1];
			Type elementType = array.GetType().GetElementType();
			object value2 = ScriptToClrConversions.DynValueToObjectOfType(value, elementType, null, false);
			array.SetValue(value2, indices);
			return DynValue.Void;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x00118778 File Offset: 0x00116978
		private static object ArrayIndexerGet(object arrayObj, ScriptExecutionContext ctx, CallbackArguments args)
		{
			Array array = (Array)arrayObj;
			int[] indices = ArrayMemberDescriptor.BuildArrayIndices(args, args.Count);
			return array.GetValue(indices);
		}

		// Token: 0x04002DD7 RID: 11735
		private bool m_IsSetter;
	}
}
