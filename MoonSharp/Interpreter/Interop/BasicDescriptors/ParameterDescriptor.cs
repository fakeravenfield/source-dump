using System;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x0200089A RID: 2202
	public sealed class ParameterDescriptor : IWireableDescriptor
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x00025357 File Offset: 0x00023557
		// (set) Token: 0x06003723 RID: 14115 RVA: 0x0002535F File Offset: 0x0002355F
		public string Name { get; private set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x00025368 File Offset: 0x00023568
		// (set) Token: 0x06003725 RID: 14117 RVA: 0x00025370 File Offset: 0x00023570
		public Type Type { get; private set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x00025379 File Offset: 0x00023579
		// (set) Token: 0x06003727 RID: 14119 RVA: 0x00025381 File Offset: 0x00023581
		public bool HasDefaultValue { get; private set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x0002538A File Offset: 0x0002358A
		// (set) Token: 0x06003729 RID: 14121 RVA: 0x00025392 File Offset: 0x00023592
		public object DefaultValue { get; private set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x0002539B File Offset: 0x0002359B
		// (set) Token: 0x0600372B RID: 14123 RVA: 0x000253A3 File Offset: 0x000235A3
		public bool IsOut { get; private set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000253AC File Offset: 0x000235AC
		// (set) Token: 0x0600372D RID: 14125 RVA: 0x000253B4 File Offset: 0x000235B4
		public bool IsRef { get; private set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000253BD File Offset: 0x000235BD
		// (set) Token: 0x0600372F RID: 14127 RVA: 0x000253C5 File Offset: 0x000235C5
		public bool IsVarArgs { get; private set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000253CE File Offset: 0x000235CE
		public bool HasBeenRestricted
		{
			get
			{
				return this.m_OriginalType != null;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000253DC File Offset: 0x000235DC
		public Type OriginalType
		{
			get
			{
				return this.m_OriginalType ?? this.Type;
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000253EE File Offset: 0x000235EE
		public ParameterDescriptor(string name, Type type, bool hasDefaultValue = false, object defaultValue = null, bool isOut = false, bool isRef = false, bool isVarArgs = false)
		{
			this.Name = name;
			this.Type = type;
			this.HasDefaultValue = hasDefaultValue;
			this.DefaultValue = defaultValue;
			this.IsOut = isOut;
			this.IsRef = isRef;
			this.IsVarArgs = isVarArgs;
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x001206F4 File Offset: 0x0011E8F4
		public ParameterDescriptor(string name, Type type, bool hasDefaultValue, object defaultValue, bool isOut, bool isRef, bool isVarArgs, Type typeRestriction)
		{
			this.Name = name;
			this.Type = type;
			this.HasDefaultValue = hasDefaultValue;
			this.DefaultValue = defaultValue;
			this.IsOut = isOut;
			this.IsRef = isRef;
			this.IsVarArgs = isVarArgs;
			if (typeRestriction != null)
			{
				this.RestrictType(typeRestriction);
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x00120750 File Offset: 0x0011E950
		public ParameterDescriptor(ParameterInfo pi)
		{
			this.Name = pi.Name;
			this.Type = pi.ParameterType;
			this.HasDefaultValue = !Framework.Do.IsDbNull(pi.DefaultValue);
			this.DefaultValue = pi.DefaultValue;
			this.IsOut = pi.IsOut;
			this.IsRef = pi.ParameterType.IsByRef;
			this.IsVarArgs = (pi.ParameterType.IsArray && pi.GetCustomAttributes(typeof(ParamArrayAttribute), true).Any<object>());
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x0002542B File Offset: 0x0002362B
		public override string ToString()
		{
			return string.Format("{0} {1}{2}", this.Type.Name, this.Name, this.HasDefaultValue ? " = ..." : "");
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x001207EC File Offset: 0x0011E9EC
		public void RestrictType(Type type)
		{
			if (this.IsOut || this.IsRef || this.IsVarArgs)
			{
				throw new InvalidOperationException("Cannot restrict a ref/out or varargs param");
			}
			if (!Framework.Do.IsAssignableFrom(this.Type, type))
			{
				throw new InvalidOperationException("Specified operation is not a restriction");
			}
			this.m_OriginalType = this.Type;
			this.Type = type;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x00120850 File Offset: 0x0011EA50
		public void PrepareForWiring(Table table)
		{
			table.Set("name", DynValue.NewString(this.Name));
			if (this.Type.IsByRef)
			{
				table.Set("type", DynValue.NewString(this.Type.GetElementType().FullName));
			}
			else
			{
				table.Set("type", DynValue.NewString(this.Type.FullName));
			}
			if (this.OriginalType.IsByRef)
			{
				table.Set("origtype", DynValue.NewString(this.OriginalType.GetElementType().FullName));
			}
			else
			{
				table.Set("origtype", DynValue.NewString(this.OriginalType.FullName));
			}
			table.Set("default", DynValue.NewBoolean(this.HasDefaultValue));
			table.Set("out", DynValue.NewBoolean(this.IsOut));
			table.Set("ref", DynValue.NewBoolean(this.IsRef));
			table.Set("varargs", DynValue.NewBoolean(this.IsVarArgs));
			table.Set("restricted", DynValue.NewBoolean(this.HasBeenRestricted));
		}

		// Token: 0x04002EDA RID: 11994
		private Type m_OriginalType;
	}
}
