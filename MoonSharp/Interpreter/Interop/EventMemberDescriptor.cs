using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.DataStructs;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.StandardDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200084C RID: 2124
	public class EventMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x060034B3 RID: 13491 RVA: 0x00118D2C File Offset: 0x00116F2C
		public static EventMemberDescriptor TryCreateIfVisible(EventInfo ei, InteropAccessMode accessMode)
		{
			if (!EventMemberDescriptor.CheckEventIsCompatible(ei, false))
			{
				return null;
			}
			MethodInfo addMethod = Framework.Do.GetAddMethod(ei);
			MethodInfo removeMethod = Framework.Do.GetRemoveMethod(ei);
			if (ei.GetVisibilityFromAttributes() ?? (removeMethod != null && removeMethod.IsPublic && addMethod != null && addMethod.IsPublic))
			{
				return new EventMemberDescriptor(ei, accessMode);
			}
			return null;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x00118DA8 File Offset: 0x00116FA8
		public static bool CheckEventIsCompatible(EventInfo ei, bool throwException)
		{
			if (Framework.Do.IsValueType(ei.DeclaringType))
			{
				if (throwException)
				{
					throw new ArgumentException("Events are not supported on value types");
				}
				return false;
			}
			else if (Framework.Do.GetAddMethod(ei) == null || Framework.Do.GetRemoveMethod(ei) == null)
			{
				if (throwException)
				{
					throw new ArgumentException("Event must have add and remove methods");
				}
				return false;
			}
			else
			{
				MethodInfo method = Framework.Do.GetMethod(ei.EventHandlerType, "Invoke");
				if (method == null)
				{
					if (throwException)
					{
						throw new ArgumentException("Event handler type doesn't seem to be a delegate");
					}
					return false;
				}
				else
				{
					if (!MethodMemberDescriptor.CheckMethodIsCompatible(method, throwException))
					{
						return false;
					}
					if (method.ReturnType != typeof(void))
					{
						if (throwException)
						{
							throw new ArgumentException("Event handler cannot have a return type");
						}
						return false;
					}
					else
					{
						ParameterInfo[] parameters = method.GetParameters();
						if (parameters.Length <= 16)
						{
							ParameterInfo[] array = parameters;
							int i = 0;
							while (i < array.Length)
							{
								ParameterInfo parameterInfo = array[i];
								if (Framework.Do.IsValueType(parameterInfo.ParameterType))
								{
									if (throwException)
									{
										throw new ArgumentException("Event handler cannot have value type parameters");
									}
									return false;
								}
								else if (parameterInfo.ParameterType.IsByRef)
								{
									if (throwException)
									{
										throw new ArgumentException("Event handler cannot have by-ref type parameters");
									}
									return false;
								}
								else
								{
									i++;
								}
							}
							return true;
						}
						if (throwException)
						{
							throw new ArgumentException(string.Format("Event handler cannot have more than {0} parameters", 16));
						}
						return false;
					}
				}
			}
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x00118EF0 File Offset: 0x001170F0
		public EventMemberDescriptor(EventInfo ei, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			EventMemberDescriptor.CheckEventIsCompatible(ei, true);
			this.EventInfo = ei;
			this.m_Add = Framework.Do.GetAddMethod(ei);
			this.m_Remove = Framework.Do.GetRemoveMethod(ei);
			this.IsStatic = this.m_Add.IsStatic;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x00024091 File Offset: 0x00022291
		// (set) Token: 0x060034B7 RID: 13495 RVA: 0x00024099 File Offset: 0x00022299
		public EventInfo EventInfo { get; private set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000240A2 File Offset: 0x000222A2
		// (set) Token: 0x060034B9 RID: 13497 RVA: 0x000240AA File Offset: 0x000222AA
		public bool IsStatic { get; private set; }

		// Token: 0x060034BA RID: 13498 RVA: 0x000240B3 File Offset: 0x000222B3
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.IsStatic)
			{
				obj = this;
			}
			return UserData.Create(new EventFacade(this, obj));
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x00118F70 File Offset: 0x00117170
		internal DynValue AddCallback(object o, ScriptExecutionContext context, CallbackArguments args)
		{
			object @lock = this.m_Lock;
			DynValue @void;
			lock (@lock)
			{
				Closure function = args.AsType(0, string.Format("userdata<{0}>.{1}.add", this.EventInfo.DeclaringType, this.EventInfo.Name), DataType.Function, false).Function;
				if (this.m_Callbacks.Add(o, function))
				{
					this.RegisterCallback(o);
				}
				@void = DynValue.Void;
			}
			return @void;
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x00118FF8 File Offset: 0x001171F8
		internal DynValue RemoveCallback(object o, ScriptExecutionContext context, CallbackArguments args)
		{
			object @lock = this.m_Lock;
			DynValue @void;
			lock (@lock)
			{
				Closure function = args.AsType(0, string.Format("userdata<{0}>.{1}.remove", this.EventInfo.DeclaringType, this.EventInfo.Name), DataType.Function, false).Function;
				if (this.m_Callbacks.RemoveValue(o, function))
				{
					this.UnregisterCallback(o);
				}
				@void = DynValue.Void;
			}
			return @void;
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x00119080 File Offset: 0x00117280
		private void RegisterCallback(object o)
		{
			this.m_Delegates.GetOrCreate(o, delegate
			{
				Delegate @delegate = this.CreateDelegate(o);
				Delegate delegate2 = Delegate.CreateDelegate(this.EventInfo.EventHandlerType, @delegate.Target, @delegate.Method);
				this.m_Add.Invoke(o, new object[]
				{
					delegate2
				});
				return delegate2;
			});
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x001190C0 File Offset: 0x001172C0
		private void UnregisterCallback(object o)
		{
			Delegate orDefault = this.m_Delegates.GetOrDefault(o);
			if (orDefault == null)
			{
				throw new InternalErrorException("can't unregister null delegate");
			}
			this.m_Delegates.Remove(o);
			this.m_Remove.Invoke(o, new object[]
			{
				orDefault
			});
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x0011910C File Offset: 0x0011730C
		private Delegate CreateDelegate(object sender)
		{
			switch (Framework.Do.GetMethod(this.EventInfo.EventHandlerType, "Invoke").GetParameters().Length)
			{
			case 0:
				return new EventMemberDescriptor.EventWrapper00(delegate()
				{
					this.DispatchEvent(sender, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 1:
				return new EventMemberDescriptor.EventWrapper01(delegate(object o1)
				{
					this.DispatchEvent(sender, o1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 2:
				return new EventMemberDescriptor.EventWrapper02(delegate(object o1, object o2)
				{
					this.DispatchEvent(sender, o1, o2, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 3:
				return new EventMemberDescriptor.EventWrapper03(delegate(object o1, object o2, object o3)
				{
					this.DispatchEvent(sender, o1, o2, o3, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 4:
				return new EventMemberDescriptor.EventWrapper04(delegate(object o1, object o2, object o3, object o4)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 5:
				return new EventMemberDescriptor.EventWrapper05(delegate(object o1, object o2, object o3, object o4, object o5)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 6:
				return new EventMemberDescriptor.EventWrapper06(delegate(object o1, object o2, object o3, object o4, object o5, object o6)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, null, null, null, null, null, null, null, null, null, null);
				});
			case 7:
				return new EventMemberDescriptor.EventWrapper07(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, null, null, null, null, null, null, null, null, null);
				});
			case 8:
				return new EventMemberDescriptor.EventWrapper08(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, null, null, null, null, null, null, null, null);
				});
			case 9:
				return new EventMemberDescriptor.EventWrapper09(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, null, null, null, null, null, null, null);
				});
			case 10:
				return new EventMemberDescriptor.EventWrapper10(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, null, null, null, null, null, null);
				});
			case 11:
				return new EventMemberDescriptor.EventWrapper11(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, null, null, null, null, null);
				});
			case 12:
				return new EventMemberDescriptor.EventWrapper12(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, null, null, null, null);
				});
			case 13:
				return new EventMemberDescriptor.EventWrapper13(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, null, null, null);
				});
			case 14:
				return new EventMemberDescriptor.EventWrapper14(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, null, null);
				});
			case 15:
				return new EventMemberDescriptor.EventWrapper15(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, null);
				});
			case 16:
				return new EventMemberDescriptor.EventWrapper16(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15, object o16)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, o16);
				});
			default:
				throw new InternalErrorException("too many args in delegate type");
			}
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x00119288 File Offset: 0x00117488
		private void DispatchEvent(object sender, object o01 = null, object o02 = null, object o03 = null, object o04 = null, object o05 = null, object o06 = null, object o07 = null, object o08 = null, object o09 = null, object o10 = null, object o11 = null, object o12 = null, object o13 = null, object o14 = null, object o15 = null, object o16 = null)
		{
			Closure[] array = null;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				array = this.m_Callbacks.Find(sender).ToArray<Closure>();
			}
			Closure[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Call(new object[]
				{
					o01,
					o02,
					o03,
					o04,
					o05,
					o06,
					o07,
					o08,
					o09,
					o10,
					o11,
					o12,
					o13,
					o14,
					o15,
					o16
				});
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000240D4 File Offset: 0x000222D4
		public string Name
		{
			get
			{
				return this.EventInfo.Name;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060034C2 RID: 13506 RVA: 0x0000476F File Offset: 0x0000296F
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead;
			}
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x00023F1E File Offset: 0x0002211E
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x04002DED RID: 11757
		public const int MAX_ARGS_IN_DELEGATE = 16;

		// Token: 0x04002DEE RID: 11758
		private object m_Lock = new object();

		// Token: 0x04002DEF RID: 11759
		private MultiDictionary<object, Closure> m_Callbacks = new MultiDictionary<object, Closure>(new ReferenceEqualityComparer());

		// Token: 0x04002DF0 RID: 11760
		private Dictionary<object, Delegate> m_Delegates = new Dictionary<object, Delegate>(new ReferenceEqualityComparer());

		// Token: 0x04002DF3 RID: 11763
		private MethodInfo m_Add;

		// Token: 0x04002DF4 RID: 11764
		private MethodInfo m_Remove;

		// Token: 0x0200084D RID: 2125
		// (Invoke) Token: 0x060034C5 RID: 13509
		private delegate void EventWrapper00();

		// Token: 0x0200084E RID: 2126
		// (Invoke) Token: 0x060034C9 RID: 13513
		private delegate void EventWrapper01(object o1);

		// Token: 0x0200084F RID: 2127
		// (Invoke) Token: 0x060034CD RID: 13517
		private delegate void EventWrapper02(object o1, object o2);

		// Token: 0x02000850 RID: 2128
		// (Invoke) Token: 0x060034D1 RID: 13521
		private delegate void EventWrapper03(object o1, object o2, object o3);

		// Token: 0x02000851 RID: 2129
		// (Invoke) Token: 0x060034D5 RID: 13525
		private delegate void EventWrapper04(object o1, object o2, object o3, object o4);

		// Token: 0x02000852 RID: 2130
		// (Invoke) Token: 0x060034D9 RID: 13529
		private delegate void EventWrapper05(object o1, object o2, object o3, object o4, object o5);

		// Token: 0x02000853 RID: 2131
		// (Invoke) Token: 0x060034DD RID: 13533
		private delegate void EventWrapper06(object o1, object o2, object o3, object o4, object o5, object o6);

		// Token: 0x02000854 RID: 2132
		// (Invoke) Token: 0x060034E1 RID: 13537
		private delegate void EventWrapper07(object o1, object o2, object o3, object o4, object o5, object o6, object o7);

		// Token: 0x02000855 RID: 2133
		// (Invoke) Token: 0x060034E5 RID: 13541
		private delegate void EventWrapper08(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8);

		// Token: 0x02000856 RID: 2134
		// (Invoke) Token: 0x060034E9 RID: 13545
		private delegate void EventWrapper09(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9);

		// Token: 0x02000857 RID: 2135
		// (Invoke) Token: 0x060034ED RID: 13549
		private delegate void EventWrapper10(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10);

		// Token: 0x02000858 RID: 2136
		// (Invoke) Token: 0x060034F1 RID: 13553
		private delegate void EventWrapper11(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11);

		// Token: 0x02000859 RID: 2137
		// (Invoke) Token: 0x060034F5 RID: 13557
		private delegate void EventWrapper12(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12);

		// Token: 0x0200085A RID: 2138
		// (Invoke) Token: 0x060034F9 RID: 13561
		private delegate void EventWrapper13(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13);

		// Token: 0x0200085B RID: 2139
		// (Invoke) Token: 0x060034FD RID: 13565
		private delegate void EventWrapper14(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14);

		// Token: 0x0200085C RID: 2140
		// (Invoke) Token: 0x06003501 RID: 13569
		private delegate void EventWrapper15(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15);

		// Token: 0x0200085D RID: 2141
		// (Invoke) Token: 0x06003505 RID: 13573
		private delegate void EventWrapper16(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15, object o16);
	}
}
