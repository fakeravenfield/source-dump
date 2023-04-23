using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000890 RID: 2192
	public abstract class DispatchingUserDataDescriptor : IUserDataDescriptor, IOptimizableDescriptor
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x000250F3 File Offset: 0x000232F3
		// (set) Token: 0x060036DE RID: 14046 RVA: 0x000250FB File Offset: 0x000232FB
		public string Name { get; private set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x00025104 File Offset: 0x00023304
		// (set) Token: 0x060036E0 RID: 14048 RVA: 0x0002510C File Offset: 0x0002330C
		public Type Type { get; private set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x00025115 File Offset: 0x00023315
		// (set) Token: 0x060036E2 RID: 14050 RVA: 0x0002511D File Offset: 0x0002331D
		public string FriendlyName { get; private set; }

		// Token: 0x060036E3 RID: 14051 RVA: 0x0011FD0C File Offset: 0x0011DF0C
		protected DispatchingUserDataDescriptor(Type type, string friendlyName = null)
		{
			this.Type = type;
			this.Name = type.FullName;
			this.FriendlyName = (friendlyName ?? type.Name);
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x00025126 File Offset: 0x00023326
		public void AddMetaMember(string name, IMemberDescriptor desc)
		{
			if (desc != null)
			{
				this.AddMemberTo(this.m_MetaMembers, name, desc);
			}
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x0011FD5C File Offset: 0x0011DF5C
		public void AddDynValue(string name, DynValue value)
		{
			DynValueMemberDescriptor desc = new DynValueMemberDescriptor(name, value);
			this.AddMemberTo(this.m_Members, name, desc);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x00025139 File Offset: 0x00023339
		public void AddMember(string name, IMemberDescriptor desc)
		{
			if (desc != null)
			{
				this.AddMemberTo(this.m_Members, name, desc);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x0002514C File Offset: 0x0002334C
		public IEnumerable<string> MemberNames
		{
			get
			{
				return this.m_Members.Keys;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x00025159 File Offset: 0x00023359
		public IEnumerable<KeyValuePair<string, IMemberDescriptor>> Members
		{
			get
			{
				return this.m_Members;
			}
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x00025161 File Offset: 0x00023361
		public IMemberDescriptor FindMember(string memberName)
		{
			return this.m_Members.GetOrDefault(memberName);
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x0002516F File Offset: 0x0002336F
		public void RemoveMember(string memberName)
		{
			this.m_Members.Remove(memberName);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x0002517E File Offset: 0x0002337E
		public IEnumerable<string> MetaMemberNames
		{
			get
			{
				return this.m_MetaMembers.Keys;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x0002518B File Offset: 0x0002338B
		public IEnumerable<KeyValuePair<string, IMemberDescriptor>> MetaMembers
		{
			get
			{
				return this.m_MetaMembers;
			}
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x00025193 File Offset: 0x00023393
		public IMemberDescriptor FindMetaMember(string memberName)
		{
			return this.m_MetaMembers.GetOrDefault(memberName);
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000251A1 File Offset: 0x000233A1
		public void RemoveMetaMember(string memberName)
		{
			this.m_MetaMembers.Remove(memberName);
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x0011FD80 File Offset: 0x0011DF80
		private void AddMemberTo(Dictionary<string, IMemberDescriptor> members, string name, IMemberDescriptor desc)
		{
			IOverloadableMemberDescriptor overloadableMemberDescriptor = desc as IOverloadableMemberDescriptor;
			if (overloadableMemberDescriptor != null)
			{
				if (!members.ContainsKey(name))
				{
					members.Add(name, new OverloadedMethodMemberDescriptor(name, this.Type, overloadableMemberDescriptor));
					return;
				}
				OverloadedMethodMemberDescriptor overloadedMethodMemberDescriptor = members[name] as OverloadedMethodMemberDescriptor;
				if (overloadedMethodMemberDescriptor != null)
				{
					overloadedMethodMemberDescriptor.AddOverload(overloadableMemberDescriptor);
					return;
				}
				throw new ArgumentException(string.Format("Multiple members named {0} are being added to type {1} and one or more of these members do not support overloads.", name, this.Type.FullName));
			}
			else
			{
				if (members.ContainsKey(name))
				{
					throw new ArgumentException(string.Format("Multiple members named {0} are being added to type {1} and one or more of these members do not support overloads.", name, this.Type.FullName));
				}
				members.Add(name, desc);
				return;
			}
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x0011FE18 File Offset: 0x0011E018
		public virtual DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			if (!isDirectIndexing)
			{
				IMemberDescriptor memberDescriptor = this.m_Members.GetOrDefault("get_Item").WithAccessOrNull(MemberDescriptorAccess.CanExecute);
				if (memberDescriptor != null)
				{
					return this.ExecuteIndexer(memberDescriptor, script, obj, index, null);
				}
			}
			index = index.ToScalar();
			if (index.Type != DataType.String)
			{
				return null;
			}
			DynValue dynValue = this.TryIndex(script, obj, index.String);
			if (dynValue == null)
			{
				dynValue = this.TryIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(index.String));
			}
			if (dynValue == null)
			{
				dynValue = this.TryIndex(script, obj, DispatchingUserDataDescriptor.Camelify(index.String));
			}
			if (dynValue == null)
			{
				dynValue = this.TryIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(DispatchingUserDataDescriptor.Camelify(index.String)));
			}
			if (dynValue == null && this.m_ExtMethodsVersion < UserData.GetExtensionMethodsChangeVersion())
			{
				this.m_ExtMethodsVersion = UserData.GetExtensionMethodsChangeVersion();
				dynValue = this.TryIndexOnExtMethod(script, obj, index.String);
				if (dynValue == null)
				{
					dynValue = this.TryIndexOnExtMethod(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(index.String));
				}
				if (dynValue == null)
				{
					dynValue = this.TryIndexOnExtMethod(script, obj, DispatchingUserDataDescriptor.Camelify(index.String));
				}
				if (dynValue == null)
				{
					dynValue = this.TryIndexOnExtMethod(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(DispatchingUserDataDescriptor.Camelify(index.String)));
				}
			}
			return dynValue;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x0011FF30 File Offset: 0x0011E130
		private DynValue TryIndexOnExtMethod(Script script, object obj, string indexName)
		{
			List<IOverloadableMemberDescriptor> extensionMethodsByNameAndType = UserData.GetExtensionMethodsByNameAndType(indexName, this.Type);
			if (extensionMethodsByNameAndType != null && extensionMethodsByNameAndType.Count > 0)
			{
				OverloadedMethodMemberDescriptor overloadedMethodMemberDescriptor = new OverloadedMethodMemberDescriptor(indexName, this.Type);
				overloadedMethodMemberDescriptor.SetExtensionMethodsSnapshot(UserData.GetExtensionMethodsChangeVersion(), extensionMethodsByNameAndType);
				this.m_Members.Add(indexName, overloadedMethodMemberDescriptor);
				return DynValue.NewCallback(overloadedMethodMemberDescriptor.GetCallback(script, obj), null);
			}
			return null;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000251B0 File Offset: 0x000233B0
		public bool HasMember(string exactName)
		{
			return this.m_Members.ContainsKey(exactName);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000251BE File Offset: 0x000233BE
		public bool HasMetaMember(string exactName)
		{
			return this.m_MetaMembers.ContainsKey(exactName);
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x0011FF8C File Offset: 0x0011E18C
		protected virtual DynValue TryIndex(Script script, object obj, string indexName)
		{
			IMemberDescriptor memberDescriptor;
			if (this.m_Members.TryGetValue(indexName, out memberDescriptor))
			{
				return memberDescriptor.GetValue(script, obj);
			}
			return null;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x0011FFB4 File Offset: 0x0011E1B4
		public virtual bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			if (!isDirectIndexing)
			{
				IMemberDescriptor memberDescriptor = this.m_Members.GetOrDefault("set_Item").WithAccessOrNull(MemberDescriptorAccess.CanExecute);
				if (memberDescriptor != null)
				{
					this.ExecuteIndexer(memberDescriptor, script, obj, index, value);
					return true;
				}
			}
			index = index.ToScalar();
			if (index.Type != DataType.String)
			{
				return false;
			}
			bool flag = this.TrySetIndex(script, obj, index.String, value);
			if (!flag)
			{
				flag = this.TrySetIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(index.String), value);
			}
			if (!flag)
			{
				flag = this.TrySetIndex(script, obj, DispatchingUserDataDescriptor.Camelify(index.String), value);
			}
			if (!flag)
			{
				flag = this.TrySetIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(DispatchingUserDataDescriptor.Camelify(index.String)), value);
			}
			return flag;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x00120064 File Offset: 0x0011E264
		protected virtual bool TrySetIndex(Script script, object obj, string indexName, DynValue value)
		{
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault(indexName);
			if (orDefault != null)
			{
				orDefault.SetValue(script, obj, value);
				return true;
			}
			return false;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x00120090 File Offset: 0x0011E290
		void IOptimizableDescriptor.Optimize()
		{
			foreach (IOptimizableDescriptor optimizableDescriptor in this.m_MetaMembers.Values.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor.Optimize();
			}
			foreach (IOptimizableDescriptor optimizableDescriptor2 in this.m_Members.Values.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor2.Optimize();
			}
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000251CC File Offset: 0x000233CC
		protected static string Camelify(string name)
		{
			return DescriptorHelpers.Camelify(name);
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000251D4 File Offset: 0x000233D4
		protected static string UpperFirstLetter(string name)
		{
			return DescriptorHelpers.UpperFirstLetter(name);
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x00023D39 File Offset: 0x00021F39
		public virtual string AsString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x00120128 File Offset: 0x0011E328
		protected virtual DynValue ExecuteIndexer(IMemberDescriptor mdesc, Script script, object obj, DynValue index, DynValue value)
		{
			IList<DynValue> list;
			if (index.Type == DataType.Tuple)
			{
				if (value == null)
				{
					list = index.Tuple;
				}
				else
				{
					list = new List<DynValue>(index.Tuple);
					list.Add(value);
				}
			}
			else if (value == null)
			{
				list = new DynValue[]
				{
					index
				};
			}
			else
			{
				list = new DynValue[]
				{
					index,
					value
				};
			}
			CallbackArguments arg = new CallbackArguments(list, false);
			ScriptExecutionContext arg2 = script.CreateDynamicExecutionContext(null);
			DynValue value2 = mdesc.GetValue(script, obj);
			if (value2.Type != DataType.ClrFunction)
			{
				throw new ScriptRuntimeException("a clr callback was expected in member {0}, while a {1} was found", new object[]
				{
					mdesc.Name,
					value2.Type
				});
			}
			return value2.Callback.ClrCallback(arg2, arg);
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x001201E4 File Offset: 0x0011E3E4
		public virtual DynValue MetaIndex(Script script, object obj, string metaname)
		{
			IMemberDescriptor orDefault = this.m_MetaMembers.GetOrDefault(metaname);
			if (orDefault != null)
			{
				return orDefault.GetValue(script, obj);
			}
			if (metaname != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(metaname);
				if (num <= 983266344U)
				{
					if (num <= 586211335U)
					{
						if (num != 444955513U)
						{
							if (num != 470922707U)
							{
								if (num == 586211335U)
								{
									if (metaname == "__sub")
									{
										return this.DispatchMetaOnMethod(script, obj, "op_Subtraction");
									}
								}
							}
							else if (metaname == "__mul")
							{
								return this.DispatchMetaOnMethod(script, obj, "op_Multiply");
							}
						}
						else if (metaname == "__eq")
						{
							return this.MultiDispatchEqual(script, obj);
						}
					}
					else if (num != 698343734U)
					{
						if (num != 731602059U)
						{
							if (num == 983266344U)
							{
								if (metaname == "__le")
								{
									return this.MultiDispatchLessThanOrEqual(script, obj);
								}
							}
						}
						else if (metaname == "__lt")
						{
							return this.MultiDispatchLessThan(script, obj);
						}
					}
					else if (metaname == "__div")
					{
						return this.DispatchMetaOnMethod(script, obj, "op_Division");
					}
				}
				else if (num <= 2173486251U)
				{
					if (num != 1204900801U)
					{
						if (num != 1795331225U)
						{
							if (num == 2173486251U)
							{
								if (metaname == "__unm")
								{
									return this.DispatchMetaOnMethod(script, obj, "op_UnaryNegation");
								}
							}
						}
						else if (metaname == "__iterator")
						{
							return ClrToScriptConversions.EnumerationToDynValue(script, obj);
						}
					}
					else if (metaname == "__mod")
					{
						return this.DispatchMetaOnMethod(script, obj, "op_Modulus");
					}
				}
				else if (num <= 2463902914U)
				{
					if (num != 2293762610U)
					{
						if (num == 2463902914U)
						{
							if (metaname == "__tobool")
							{
								return this.TryDispatchToBool(script, obj);
							}
						}
					}
					else if (metaname == "__len")
					{
						return this.TryDispatchLength(script, obj);
					}
				}
				else if (num != 3367840379U)
				{
					if (num == 4200909926U)
					{
						if (metaname == "__add")
						{
							return this.DispatchMetaOnMethod(script, obj, "op_Addition");
						}
					}
				}
				else if (metaname == "__tonumber")
				{
					return this.TryDispatchToNumber(script, obj);
				}
			}
			return null;
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x00120480 File Offset: 0x0011E680
		private int PerformComparison(object obj, object p1, object p2)
		{
			IComparable comparable = (IComparable)obj;
			if (comparable != null)
			{
				if (obj == p1)
				{
					return comparable.CompareTo(p2);
				}
				if (obj == p2)
				{
					return -comparable.CompareTo(p1);
				}
			}
			throw new InternalErrorException("unexpected case");
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x001204BC File Offset: 0x0011E6BC
		private DynValue MultiDispatchLessThanOrEqual(Script script, object obj)
		{
			if (obj is IComparable)
			{
				return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.PerformComparison(obj, args[0].ToObject(), args[1].ToObject()) <= 0), null);
			}
			return null;
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x00120500 File Offset: 0x0011E700
		private DynValue MultiDispatchLessThan(Script script, object obj)
		{
			if (obj is IComparable)
			{
				return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.PerformComparison(obj, args[0].ToObject(), args[1].ToObject()) < 0), null);
			}
			return null;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x00120544 File Offset: 0x0011E744
		private DynValue TryDispatchLength(Script script, object obj)
		{
			if (obj == null)
			{
				return null;
			}
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault("Length");
			if (orDefault != null && orDefault.CanRead() && !orDefault.CanExecute())
			{
				return orDefault.GetGetterCallbackAsDynValue(script, obj);
			}
			IMemberDescriptor orDefault2 = this.m_Members.GetOrDefault("Count");
			if (orDefault2 != null && orDefault2.CanRead() && !orDefault2.CanExecute())
			{
				return orDefault2.GetGetterCallbackAsDynValue(script, obj);
			}
			return null;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000251DC File Offset: 0x000233DC
		private DynValue MultiDispatchEqual(Script script, object obj)
		{
			return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.CheckEquality(obj, args[0].ToObject(), args[1].ToObject())), null);
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x00025202 File Offset: 0x00023402
		private bool CheckEquality(object obj, object p1, object p2)
		{
			if (obj != null)
			{
				if (obj == p1)
				{
					return obj.Equals(p2);
				}
				if (obj == p2)
				{
					return obj.Equals(p1);
				}
			}
			if (p1 != null)
			{
				return p1.Equals(p2);
			}
			return p2 == null || p2.Equals(p1);
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x001205B4 File Offset: 0x0011E7B4
		private DynValue DispatchMetaOnMethod(Script script, object obj, string methodName)
		{
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault(methodName);
			if (orDefault != null)
			{
				return orDefault.GetValue(script, obj);
			}
			return null;
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x001205DC File Offset: 0x0011E7DC
		private DynValue TryDispatchToNumber(Script script, object obj)
		{
			Type[] numericTypesOrdered = NumericConversions.NumericTypesOrdered;
			for (int i = 0; i < numericTypesOrdered.Length; i++)
			{
				string conversionMethodName = numericTypesOrdered[i].GetConversionMethodName();
				DynValue dynValue = this.DispatchMetaOnMethod(script, obj, conversionMethodName);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return null;
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x00120618 File Offset: 0x0011E818
		private DynValue TryDispatchToBool(Script script, object obj)
		{
			string conversionMethodName = typeof(bool).GetConversionMethodName();
			DynValue dynValue = this.DispatchMetaOnMethod(script, obj, conversionMethodName);
			if (dynValue != null)
			{
				return dynValue;
			}
			return this.DispatchMetaOnMethod(script, obj, "op_True");
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x00022955 File Offset: 0x00020B55
		public virtual bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04002EBC RID: 11964
		private int m_ExtMethodsVersion;

		// Token: 0x04002EBD RID: 11965
		private Dictionary<string, IMemberDescriptor> m_MetaMembers = new Dictionary<string, IMemberDescriptor>();

		// Token: 0x04002EBE RID: 11966
		private Dictionary<string, IMemberDescriptor> m_Members = new Dictionary<string, IMemberDescriptor>();

		// Token: 0x04002EBF RID: 11967
		protected const string SPECIALNAME_INDEXER_GET = "get_Item";

		// Token: 0x04002EC0 RID: 11968
		protected const string SPECIALNAME_INDEXER_SET = "set_Item";

		// Token: 0x04002EC1 RID: 11969
		protected const string SPECIALNAME_CAST_EXPLICIT = "op_Explicit";

		// Token: 0x04002EC2 RID: 11970
		protected const string SPECIALNAME_CAST_IMPLICIT = "op_Implicit";
	}
}
