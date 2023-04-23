using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000869 RID: 2153
	public class ValueTypeDefaultCtorMemberDescriptor : IOverloadableMemberDescriptor, IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x0000476F File Offset: 0x0000296F
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x00024403 File Offset: 0x00022603
		// (set) Token: 0x06003579 RID: 13689 RVA: 0x0002440B File Offset: 0x0002260B
		public string Name { get; private set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x00024414 File Offset: 0x00022614
		// (set) Token: 0x0600357B RID: 13691 RVA: 0x0002441C File Offset: 0x0002261C
		public Type ValueTypeDefaultCtor { get; private set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x00024425 File Offset: 0x00022625
		// (set) Token: 0x0600357D RID: 13693 RVA: 0x0002442D File Offset: 0x0002262D
		public ParameterDescriptor[] Parameters { get; private set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x00002FD8 File Offset: 0x000011D8
		public Type ExtensionMethodType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x00002FD8 File Offset: 0x000011D8
		public Type VarArgsArrayType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x00002FD8 File Offset: 0x000011D8
		public Type VarArgsElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00024436 File Offset: 0x00022636
		public ValueTypeDefaultCtorMemberDescriptor(Type valueType)
		{
			if (!Framework.Do.IsValueType(valueType))
			{
				throw new ArgumentException("valueType is not a value type");
			}
			this.Name = "new";
			this.Parameters = new ParameterDescriptor[0];
			this.ValueTypeDefaultCtor = valueType;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x0011B170 File Offset: 0x00119370
		public DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object obj2 = Activator.CreateInstance(this.ValueTypeDefaultCtor);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2, null);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x00024474 File Offset: 0x00022674
		public string SortDiscriminant
		{
			get
			{
				return "@.ctor";
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x0011B170 File Offset: 0x00119370
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object obj2 = Activator.CreateInstance(this.ValueTypeDefaultCtor);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2, null);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x00023F1E File Offset: 0x0002211E
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0011B19C File Offset: 0x0011939C
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("type", DynValue.NewString(this.ValueTypeDefaultCtor.FullName));
			t.Set("name", DynValue.NewString(this.Name));
		}
	}
}
