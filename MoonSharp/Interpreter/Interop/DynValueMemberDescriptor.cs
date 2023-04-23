using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000845 RID: 2117
	public class DynValueMemberDescriptor : IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x06003476 RID: 13430 RVA: 0x001187A0 File Offset: 0x001169A0
		protected DynValueMemberDescriptor(string name, string serializedTableValue)
		{
			DynValue dynValue = new Script().CreateDynamicExpression(serializedTableValue).Evaluate(null);
			this.m_Value = dynValue.Table.Get(1);
			this.Name = name;
			this.MemberAccess = MemberDescriptorAccess.CanRead;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00023DA1 File Offset: 0x00021FA1
		protected DynValueMemberDescriptor(string name)
		{
			this.MemberAccess = MemberDescriptorAccess.CanRead;
			this.m_Value = null;
			this.Name = name;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00023DBE File Offset: 0x00021FBE
		public DynValueMemberDescriptor(string name, DynValue value)
		{
			this.m_Value = value;
			this.Name = name;
			if (value.Type == DataType.ClrFunction)
			{
				this.MemberAccess = (MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute);
				return;
			}
			this.MemberAccess = MemberDescriptorAccess.CanRead;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x0000476F File Offset: 0x0000296F
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x00023DED File Offset: 0x00021FED
		// (set) Token: 0x0600347B RID: 13435 RVA: 0x00023DF5 File Offset: 0x00021FF5
		public string Name { get; private set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x00023DFE File Offset: 0x00021FFE
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x00023E06 File Offset: 0x00022006
		public MemberDescriptorAccess MemberAccess { get; private set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x00023E0F File Offset: 0x0002200F
		public virtual DynValue Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00023E17 File Offset: 0x00022017
		public DynValue GetValue(Script script, object obj)
		{
			return this.Value;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00023E1F File Offset: 0x0002201F
		public void SetValue(Script script, object obj, DynValue value)
		{
			throw new ScriptRuntimeException("userdata '{0}' cannot be written to.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x001187E8 File Offset: 0x001169E8
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(this.Name));
			switch (this.Value.Type)
			{
			case DataType.Nil:
			case DataType.Void:
			case DataType.Boolean:
			case DataType.Number:
			case DataType.String:
			case DataType.Tuple:
				t.Set("value", this.Value);
				return;
			case DataType.Table:
				if (this.Value.Table.OwnerScript == null)
				{
					t.Set("value", this.Value);
					return;
				}
				t.Set("error", DynValue.NewString("Wiring of non-prime table value members not supported."));
				return;
			case DataType.UserData:
				if (this.Value.UserData.Object == null)
				{
					t.Set("type", DynValue.NewString("userdata"));
					t.Set("staticType", DynValue.NewString(this.Value.UserData.Descriptor.Type.FullName));
					t.Set("visibility", DynValue.NewString(this.Value.UserData.Descriptor.Type.GetClrVisibility()));
					return;
				}
				t.Set("error", DynValue.NewString("Wiring of non-static userdata value members not supported."));
				return;
			}
			t.Set("error", DynValue.NewString(string.Format("Wiring of '{0}' value members not supported.", this.Value.Type.ToErrorTypeString())));
		}

		// Token: 0x04002DD8 RID: 11736
		private DynValue m_Value;
	}
}
