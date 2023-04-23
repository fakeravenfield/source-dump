using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Lua;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02000889 RID: 2185
	internal static class ClrToScriptConversions
	{
		// Token: 0x060036BD RID: 14013 RVA: 0x0011E794 File Offset: 0x0011C994
		internal static DynValue TryObjectToTrivialDynValue(Script script, object obj)
		{
			if (obj == null)
			{
				return DynValue.Nil;
			}
			if (obj is DynValue)
			{
				return (DynValue)obj;
			}
			Type type = obj.GetType();
			if (obj is bool)
			{
				return DynValue.NewBoolean((bool)obj);
			}
			if (obj is string || obj is StringBuilder || obj is char)
			{
				return DynValue.NewString(obj.ToString());
			}
			if (NumericConversions.NumericTypes.Contains(type))
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(type, obj));
			}
			if (obj is Table)
			{
				return DynValue.NewTable((Table)obj);
			}
			return null;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x0011E828 File Offset: 0x0011CA28
		internal static DynValue TryObjectToSimpleDynValue(Script script, object obj, Type targetType = null)
		{
			if (obj == null)
			{
				return DynValue.Nil;
			}
			if (obj is DynValue)
			{
				return (DynValue)obj;
			}
			Type type = obj.GetType();
			if (targetType == null)
			{
				ResolveDynValueAsAttribute customAttribute = type.GetCustomAttribute<ResolveDynValueAsAttribute>();
				if (customAttribute != null)
				{
					targetType = customAttribute.targetType;
				}
			}
			Func<Script, object, DynValue> clrToScriptCustomConversion = Script.GlobalOptions.CustomConverters.GetClrToScriptCustomConversion(type);
			if (clrToScriptCustomConversion != null)
			{
				DynValue dynValue = clrToScriptCustomConversion(script, obj);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			else if (targetType != null)
			{
				Type baseType = type.BaseType;
				while (baseType != null && targetType.IsAssignableFrom(baseType))
				{
					clrToScriptCustomConversion = Script.GlobalOptions.CustomConverters.GetClrToScriptCustomConversion(baseType);
					if (clrToScriptCustomConversion != null)
					{
						DynValue dynValue2 = clrToScriptCustomConversion(script, obj);
						if (dynValue2 != null)
						{
							return dynValue2;
						}
					}
					baseType = baseType.BaseType;
				}
			}
			if (obj is bool)
			{
				return DynValue.NewBoolean((bool)obj);
			}
			if (obj is string || obj is StringBuilder || obj is char)
			{
				return DynValue.NewString(obj.ToString());
			}
			if (obj is Closure)
			{
				return DynValue.NewClosure((Closure)obj);
			}
			if (NumericConversions.NumericTypes.Contains(type))
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(type, obj));
			}
			if (obj is Table)
			{
				return DynValue.NewTable((Table)obj);
			}
			if (obj is CallbackFunction)
			{
				return DynValue.NewCallback((CallbackFunction)obj);
			}
			if (obj is Delegate)
			{
				Delegate @delegate = (Delegate)obj;
				if (CallbackFunction.CheckCallbackSignature(@delegate.Method, false))
				{
					return DynValue.NewCallback((Func<ScriptExecutionContext, CallbackArguments, DynValue>)@delegate, null);
				}
			}
			return null;
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x0011E9A4 File Offset: 0x0011CBA4
		internal static DynValue ObjectToDynValue(Script script, object obj, Type targetType = null)
		{
			DynValue dynValue = ClrToScriptConversions.TryObjectToSimpleDynValue(script, obj, targetType);
			if (dynValue != null)
			{
				return dynValue;
			}
			dynValue = UserData.Create(obj);
			if (dynValue != null)
			{
				return dynValue;
			}
			if (obj is Type)
			{
				dynValue = UserData.CreateStatic(obj as Type);
			}
			if (obj is Enum)
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(Enum.GetUnderlyingType(obj.GetType()), obj));
			}
			if (dynValue != null)
			{
				return dynValue;
			}
			if (obj is Delegate)
			{
				return DynValue.NewCallback(CallbackFunction.FromDelegate(script, (Delegate)obj, InteropAccessMode.Default));
			}
			if (obj is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)obj;
				if (methodInfo.IsStatic)
				{
					return DynValue.NewCallback(CallbackFunction.FromMethodInfo(script, methodInfo, null, InteropAccessMode.Default));
				}
			}
			if (obj is IList)
			{
				Type type = obj.GetType();
				Type elementType = null;
				if (type.IsGenericType)
				{
					Type[] genericArguments = type.GetGenericArguments();
					if (genericArguments.Length == 1)
					{
						elementType = genericArguments[0];
					}
				}
				else if (type.IsArray)
				{
					elementType = type.GetElementType();
				}
				return DynValue.NewTable(TableConversions.ConvertIListToTable(script, (IList)obj, elementType));
			}
			if (obj is IDictionary)
			{
				Type keyType = null;
				Type valueType = null;
				Type type2 = obj.GetType();
				if (type2.IsGenericType)
				{
					Type[] genericArguments2 = type2.GetGenericArguments();
					if (genericArguments2.Length == 2)
					{
						keyType = genericArguments2[0];
						valueType = genericArguments2[1];
					}
				}
				return DynValue.NewTable(TableConversions.ConvertIDictionaryToTable(script, (IDictionary)obj, keyType, valueType));
			}
			DynValue dynValue2 = ClrToScriptConversions.EnumerationToDynValue(script, obj);
			if (dynValue2 != null)
			{
				return dynValue2;
			}
			throw ScriptRuntimeException.ConvertObjectFailed(obj);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x0011EB00 File Offset: 0x0011CD00
		public static DynValue EnumerationToDynValue(Script script, object obj)
		{
			Type elementType = null;
			Type type = obj.GetType();
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					elementType = genericArguments[0];
				}
			}
			if (obj is IEnumerable)
			{
				IEnumerable enumerable = (IEnumerable)obj;
				return EnumerableWrapper.ConvertIterator(script, enumerable.GetEnumerator(), elementType);
			}
			if (obj is IEnumerator)
			{
				IEnumerator enumerator = (IEnumerator)obj;
				return EnumerableWrapper.ConvertIterator(script, enumerator, elementType);
			}
			return null;
		}
	}
}
