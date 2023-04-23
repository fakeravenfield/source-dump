using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000863 RID: 2147
	public class OverloadedMethodMemberDescriptor : IOptimizableDescriptor, IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000241CD File Offset: 0x000223CD
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000241D5 File Offset: 0x000223D5
		public bool IgnoreExtensionMethods { get; set; }

		// Token: 0x06003543 RID: 13635 RVA: 0x000241DE File Offset: 0x000223DE
		public OverloadedMethodMemberDescriptor(string name, Type declaringType)
		{
			this.Name = name;
			this.DeclaringType = declaringType;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x0002421D File Offset: 0x0002241D
		public OverloadedMethodMemberDescriptor(string name, Type declaringType, IOverloadableMemberDescriptor descriptor) : this(name, declaringType)
		{
			this.m_Overloads.Add(descriptor);
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x00024233 File Offset: 0x00022433
		public OverloadedMethodMemberDescriptor(string name, Type declaringType, IEnumerable<IOverloadableMemberDescriptor> descriptors) : this(name, declaringType)
		{
			this.m_Overloads.AddRange(descriptors);
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x00024249 File Offset: 0x00022449
		internal void SetExtensionMethodsSnapshot(int version, List<IOverloadableMemberDescriptor> extMethods)
		{
			this.m_ExtOverloads = extMethods;
			this.m_ExtensionMethodVersion = version;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x00024259 File Offset: 0x00022459
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x00024261 File Offset: 0x00022461
		public string Name { get; private set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x0002426A File Offset: 0x0002246A
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x00024272 File Offset: 0x00022472
		public Type DeclaringType { get; private set; }

		// Token: 0x0600354B RID: 13643 RVA: 0x0002427B File Offset: 0x0002247B
		public void AddOverload(IOverloadableMemberDescriptor overload)
		{
			this.m_Overloads.Add(overload);
			this.m_Unsorted = true;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x00024290 File Offset: 0x00022490
		public int OverloadCount
		{
			get
			{
				return this.m_Overloads.Count;
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x0011A2A8 File Offset: 0x001184A8
		private DynValue PerformOverloadedCall(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			bool flag = this.IgnoreExtensionMethods || obj == null || this.m_ExtensionMethodVersion == UserData.GetExtensionMethodsChangeVersion();
			if (this.m_Overloads.Count == 1 && this.m_ExtOverloads.Count == 0 && flag)
			{
				return this.m_Overloads[0].Execute(script, obj, context, args);
			}
			if (this.m_Unsorted)
			{
				this.m_Overloads.Sort(new OverloadedMethodMemberDescriptor.OverloadableMemberDescriptorComparer());
				this.m_Unsorted = false;
			}
			if (flag)
			{
				for (int i = 0; i < this.m_Cache.Length; i++)
				{
					if (this.m_Cache[i] != null && this.CheckMatch(obj != null, args, this.m_Cache[i]))
					{
						return this.m_Cache[i].Method.Execute(script, obj, context, args);
					}
				}
			}
			int num = 0;
			IOverloadableMemberDescriptor overloadableMemberDescriptor = null;
			for (int j = 0; j < this.m_Overloads.Count; j++)
			{
				if (obj != null || this.m_Overloads[j].IsStatic)
				{
					int num2 = this.CalcScoreForOverload(context, args, this.m_Overloads[j], false);
					if (num2 > num)
					{
						num = num2;
						overloadableMemberDescriptor = this.m_Overloads[j];
					}
				}
			}
			if (!this.IgnoreExtensionMethods && obj != null)
			{
				if (!flag)
				{
					this.m_ExtensionMethodVersion = UserData.GetExtensionMethodsChangeVersion();
					this.m_ExtOverloads = UserData.GetExtensionMethodsByNameAndType(this.Name, this.DeclaringType);
				}
				for (int k = 0; k < this.m_ExtOverloads.Count; k++)
				{
					int num3 = this.CalcScoreForOverload(context, args, this.m_ExtOverloads[k], true);
					if (num3 > num)
					{
						num = num3;
						overloadableMemberDescriptor = this.m_ExtOverloads[k];
					}
				}
			}
			if (overloadableMemberDescriptor != null)
			{
				this.Cache(obj != null, args, overloadableMemberDescriptor);
				return overloadableMemberDescriptor.Execute(script, obj, context, args);
			}
			throw new ScriptRuntimeException("function call doesn't match any overload");
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x0011A47C File Offset: 0x0011867C
		private void Cache(bool hasObject, CallbackArguments args, IOverloadableMemberDescriptor bestOverload)
		{
			int num = int.MaxValue;
			OverloadedMethodMemberDescriptor.OverloadCacheItem overloadCacheItem = null;
			for (int i = 0; i < this.m_Cache.Length; i++)
			{
				if (this.m_Cache[i] == null)
				{
					overloadCacheItem = new OverloadedMethodMemberDescriptor.OverloadCacheItem
					{
						ArgsDataType = new List<DataType>(),
						ArgsUserDataType = new List<Type>()
					};
					this.m_Cache[i] = overloadCacheItem;
					break;
				}
				if (this.m_Cache[i].HitIndexAtLastHit < num)
				{
					num = this.m_Cache[i].HitIndexAtLastHit;
					overloadCacheItem = this.m_Cache[i];
				}
			}
			if (overloadCacheItem == null)
			{
				this.m_Cache = new OverloadedMethodMemberDescriptor.OverloadCacheItem[5];
				overloadCacheItem = new OverloadedMethodMemberDescriptor.OverloadCacheItem
				{
					ArgsDataType = new List<DataType>(),
					ArgsUserDataType = new List<Type>()
				};
				this.m_Cache[0] = overloadCacheItem;
				this.m_CacheHits = 0;
			}
			overloadCacheItem.Method = bestOverload;
			OverloadedMethodMemberDescriptor.OverloadCacheItem overloadCacheItem2 = overloadCacheItem;
			int num2 = this.m_CacheHits + 1;
			this.m_CacheHits = num2;
			overloadCacheItem2.HitIndexAtLastHit = num2;
			overloadCacheItem.ArgsDataType.Clear();
			overloadCacheItem.HasObject = hasObject;
			for (int j = 0; j < args.Count; j++)
			{
				overloadCacheItem.ArgsDataType.Add(args[j].Type);
				if (args[j].Type == DataType.UserData)
				{
					overloadCacheItem.ArgsUserDataType.Add(args[j].UserData.Descriptor.Type);
				}
				else
				{
					overloadCacheItem.ArgsUserDataType.Add(null);
				}
			}
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x0011A5D4 File Offset: 0x001187D4
		private bool CheckMatch(bool hasObject, CallbackArguments args, OverloadedMethodMemberDescriptor.OverloadCacheItem overloadCacheItem)
		{
			if (overloadCacheItem.HasObject && !hasObject)
			{
				return false;
			}
			if (args.Count != overloadCacheItem.ArgsDataType.Count)
			{
				return false;
			}
			for (int i = 0; i < args.Count; i++)
			{
				if (args[i].Type != overloadCacheItem.ArgsDataType[i])
				{
					return false;
				}
				if (args[i].Type == DataType.UserData && args[i].UserData.Descriptor.Type != overloadCacheItem.ArgsUserDataType[i])
				{
					return false;
				}
			}
			int num = this.m_CacheHits + 1;
			this.m_CacheHits = num;
			overloadCacheItem.HitIndexAtLastHit = num;
			return true;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x0011A684 File Offset: 0x00118884
		private int CalcScoreForOverload(ScriptExecutionContext context, CallbackArguments args, IOverloadableMemberDescriptor method, bool isExtMethod)
		{
			int num = 100;
			int num2 = args.IsMethodCall ? 1 : 0;
			int num3 = num2;
			bool flag = false;
			for (int i = 0; i < method.Parameters.Length; i++)
			{
				if ((!isExtMethod || i != 0) && !method.Parameters[i].IsOut)
				{
					Type type = method.Parameters[i].Type;
					if (!(type == typeof(Script)) && !(type == typeof(ScriptExecutionContext)) && !(type == typeof(CallbackArguments)))
					{
						if (i == method.Parameters.Length - 1 && method.VarArgsArrayType != null)
						{
							int num4 = 0;
							DynValue dynValue = null;
							int num5 = num;
							for (;;)
							{
								DynValue dynValue2 = args.RawGet(num3, false);
								if (dynValue2 == null)
								{
									break;
								}
								if (dynValue == null)
								{
									dynValue = dynValue2;
								}
								num3++;
								num4++;
								int val = OverloadedMethodMemberDescriptor.CalcScoreForSingleArgument(method.Parameters[i], method.VarArgsElementType, dynValue2, false);
								num = Math.Min(num, val);
							}
							if (num4 == 1 && dynValue.Type == DataType.UserData && dynValue.UserData.Object != null && Framework.Do.IsAssignableFrom(method.VarArgsArrayType, dynValue.UserData.Object.GetType()))
							{
								num = num5;
							}
							else
							{
								if (num4 == 0)
								{
									num = Math.Min(num, 40);
								}
								flag = true;
							}
						}
						else
						{
							DynValue arg = args.RawGet(num3, false) ?? DynValue.Void;
							int val2 = OverloadedMethodMemberDescriptor.CalcScoreForSingleArgument(method.Parameters[i], type, arg, method.Parameters[i].HasDefaultValue);
							num = Math.Min(num, val2);
							num3++;
						}
					}
				}
			}
			if (num > 0)
			{
				if (args.Count - num2 <= method.Parameters.Length)
				{
					num += 100;
					num *= 1000;
				}
				else if (flag)
				{
					num--;
					num *= 1000;
				}
				else
				{
					num *= 1000;
					num -= 2 * (args.Count - num2 - method.Parameters.Length);
					num = Math.Max(1, num);
				}
				ParameterDescriptor[] parameters = method.Parameters;
				for (int j = 0; j < parameters.Length; j++)
				{
					if (parameters[j].OriginalType == typeof(float))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x0011A8D4 File Offset: 0x00118AD4
		private static int CalcScoreForSingleArgument(ParameterDescriptor desc, Type parameterType, DynValue arg, bool isOptional)
		{
			int num = ScriptToClrConversions.DynValueToObjectOfTypeWeight(arg, parameterType, isOptional);
			if (parameterType.IsByRef || desc.IsOut || desc.IsRef)
			{
				num = Math.Max(0, num + -10);
			}
			return num;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x0002429D File Offset: 0x0002249D
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj)
		{
			return (ScriptExecutionContext context, CallbackArguments args) => this.PerformOverloadedCall(script, obj, context, args);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x0011A910 File Offset: 0x00118B10
		void IOptimizableDescriptor.Optimize()
		{
			foreach (IOptimizableDescriptor optimizableDescriptor in this.m_Overloads.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor.Optimize();
			}
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000242C4 File Offset: 0x000224C4
		public CallbackFunction GetCallbackFunction(Script script, object obj = null)
		{
			return new CallbackFunction(this.GetCallback(script, obj), this.Name);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000242D9 File Offset: 0x000224D9
		public bool IsStatic
		{
			get
			{
				return this.m_Overloads.Any((IOverloadableMemberDescriptor o) => o.IsStatic);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00024305 File Offset: 0x00022505
		public DynValue GetValue(Script script, object obj)
		{
			return DynValue.NewCallback(this.GetCallbackFunction(script, obj));
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00023F1E File Offset: 0x0002211E
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x0011A960 File Offset: 0x00118B60
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(this.Name));
			t.Set("decltype", DynValue.NewString(this.DeclaringType.FullName));
			DynValue dynValue = DynValue.NewPrimeTable();
			t.Set("overloads", dynValue);
			int num = 0;
			foreach (IOverloadableMemberDescriptor overloadableMemberDescriptor in this.m_Overloads)
			{
				IWireableDescriptor wireableDescriptor = overloadableMemberDescriptor as IWireableDescriptor;
				if (wireableDescriptor != null)
				{
					DynValue dynValue2 = DynValue.NewPrimeTable();
					dynValue.Table.Set(++num, dynValue2);
					wireableDescriptor.PrepareForWiring(dynValue2.Table);
				}
				else
				{
					dynValue.Table.Set(++num, DynValue.NewString(string.Format("unsupported - {0} is not serializable", overloadableMemberDescriptor.GetType().FullName)));
				}
			}
		}

		// Token: 0x04002E0D RID: 11789
		private const int CACHE_SIZE = 5;

		// Token: 0x04002E0E RID: 11790
		private List<IOverloadableMemberDescriptor> m_Overloads = new List<IOverloadableMemberDescriptor>();

		// Token: 0x04002E0F RID: 11791
		private List<IOverloadableMemberDescriptor> m_ExtOverloads = new List<IOverloadableMemberDescriptor>();

		// Token: 0x04002E10 RID: 11792
		private bool m_Unsorted = true;

		// Token: 0x04002E11 RID: 11793
		private OverloadedMethodMemberDescriptor.OverloadCacheItem[] m_Cache = new OverloadedMethodMemberDescriptor.OverloadCacheItem[5];

		// Token: 0x04002E12 RID: 11794
		private int m_CacheHits;

		// Token: 0x04002E13 RID: 11795
		private int m_ExtensionMethodVersion;

		// Token: 0x02000864 RID: 2148
		private class OverloadableMemberDescriptorComparer : IComparer<IOverloadableMemberDescriptor>
		{
			// Token: 0x0600355A RID: 13658 RVA: 0x00024314 File Offset: 0x00022514
			public int Compare(IOverloadableMemberDescriptor x, IOverloadableMemberDescriptor y)
			{
				return x.SortDiscriminant.CompareTo(y.SortDiscriminant);
			}
		}

		// Token: 0x02000865 RID: 2149
		private class OverloadCacheItem
		{
			// Token: 0x04002E17 RID: 11799
			public bool HasObject;

			// Token: 0x04002E18 RID: 11800
			public IOverloadableMemberDescriptor Method;

			// Token: 0x04002E19 RID: 11801
			public List<DataType> ArgsDataType;

			// Token: 0x04002E1A RID: 11802
			public List<Type> ArgsUserDataType;

			// Token: 0x04002E1B RID: 11803
			public int HitIndexAtLastHit;
		}
	}
}
