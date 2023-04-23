using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x0200087C RID: 2172
	public abstract class HardwiredMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x0002493A File Offset: 0x00022B3A
		// (set) Token: 0x06003612 RID: 13842 RVA: 0x00024942 File Offset: 0x00022B42
		public Type MemberType { get; private set; }

		// Token: 0x06003613 RID: 13843 RVA: 0x0002494B File Offset: 0x00022B4B
		protected HardwiredMemberDescriptor(Type memberType, string name, bool isStatic, MemberDescriptorAccess access)
		{
			this.IsStatic = isStatic;
			this.Name = name;
			this.MemberAccess = access;
			this.MemberType = memberType;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x00024970 File Offset: 0x00022B70
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x00024978 File Offset: 0x00022B78
		public bool IsStatic { get; private set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x00024981 File Offset: 0x00022B81
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x00024989 File Offset: 0x00022B89
		public string Name { get; private set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x00024992 File Offset: 0x00022B92
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x0002499A File Offset: 0x00022B9A
		public MemberDescriptorAccess MemberAccess { get; private set; }

		// Token: 0x0600361A RID: 13850 RVA: 0x0011CDA0 File Offset: 0x0011AFA0
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object valueImpl = this.GetValueImpl(script, obj);
			return ClrToScriptConversions.ObjectToDynValue(script, valueImpl, null);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0011CDC8 File Offset: 0x0011AFC8
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			object value2 = ScriptToClrConversions.DynValueToObjectOfType(value, this.MemberType, null, false);
			this.SetValueImpl(script, obj, value2);
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000249A3 File Offset: 0x00022BA3
		protected virtual object GetValueImpl(Script script, object obj)
		{
			throw new InvalidOperationException("GetValue on write-only hardwired descriptor " + this.Name);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000249BA File Offset: 0x00022BBA
		protected virtual void SetValueImpl(Script script, object obj, object value)
		{
			throw new InvalidOperationException("SetValue on read-only hardwired descriptor " + this.Name);
		}
	}
}
