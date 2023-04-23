using System;
using MoonSharp.Interpreter.Compatibility;
using UnityEngine;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x0200088B RID: 2187
	internal static class ScriptToClrConversions
	{
		// Token: 0x060036C4 RID: 14020 RVA: 0x0011EF80 File Offset: 0x0011D180
		internal static object DynValueToObject(DynValue value)
		{
			Func<DynValue, object> scriptToClrCustomConversion = Script.GlobalOptions.CustomConverters.GetScriptToClrCustomConversion(value.Type, typeof(object));
			if (scriptToClrCustomConversion != null)
			{
				object obj = scriptToClrCustomConversion(value);
				if (obj != null)
				{
					return obj;
				}
			}
			switch (value.Type)
			{
			case DataType.Nil:
			case DataType.Void:
				return null;
			case DataType.Boolean:
				return value.Boolean;
			case DataType.Number:
				return value.Number;
			case DataType.String:
				return value.String;
			case DataType.Function:
				return value.Function;
			case DataType.Table:
				return value.Table;
			case DataType.Tuple:
				return value.Tuple;
			case DataType.UserData:
				if (value.UserData.Object != null)
				{
					return value.UserData.Object;
				}
				if (value.UserData.Descriptor != null)
				{
					return value.UserData.Descriptor.Type;
				}
				return null;
			case DataType.ClrFunction:
				return value.Callback;
			}
			throw ScriptRuntimeException.ConvertObjectFailed(value.Type);
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x0011F07C File Offset: 0x0011D27C
		internal static object DynValueToObjectOfType(DynValue value, Type desiredType, object defaultValue, bool isOptional)
		{
			if (desiredType.IsByRef)
			{
				desiredType = desiredType.GetElementType();
			}
			Func<DynValue, object> scriptToClrCustomConversion = Script.GlobalOptions.CustomConverters.GetScriptToClrCustomConversion(value.Type, desiredType);
			if (scriptToClrCustomConversion != null)
			{
				object obj = scriptToClrCustomConversion(value);
				if (obj != null)
				{
					return obj;
				}
			}
			if (desiredType == typeof(DynValue))
			{
				return value;
			}
			if (desiredType == typeof(object))
			{
				return ScriptToClrConversions.DynValueToObject(value);
			}
			StringConversions.StringSubtype stringSubtype = StringConversions.GetStringSubtype(desiredType);
			string text = null;
			Type underlyingType = Nullable.GetUnderlyingType(desiredType);
			Type left = null;
			if (underlyingType != null)
			{
				left = desiredType;
				desiredType = underlyingType;
			}
			switch (value.Type)
			{
			case DataType.Nil:
				if (!Framework.Do.IsValueType(desiredType))
				{
					return null;
				}
				if (left != null)
				{
					return null;
				}
				if (isOptional)
				{
					return defaultValue;
				}
				break;
			case DataType.Void:
				if (isOptional)
				{
					return defaultValue;
				}
				if (!Framework.Do.IsValueType(desiredType) || left != null)
				{
					return null;
				}
				break;
			case DataType.Boolean:
				if (desiredType == typeof(bool))
				{
					return value.Boolean;
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					text = value.Boolean.ToString();
				}
				break;
			case DataType.Number:
				if (Framework.Do.IsEnum(desiredType))
				{
					return NumericConversions.DoubleToType(Enum.GetUnderlyingType(desiredType), value.Number);
				}
				if (NumericConversions.NumericTypes.Contains(desiredType))
				{
					return NumericConversions.DoubleToType(desiredType, value.Number);
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					text = value.Number.ToString();
				}
				break;
			case DataType.String:
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					text = value.String;
				}
				break;
			case DataType.Function:
				if (desiredType == typeof(Closure))
				{
					return value.Function;
				}
				if (desiredType == typeof(ScriptFunctionDelegate))
				{
					return value.Function.GetDelegate();
				}
				break;
			case DataType.Table:
			{
				if (desiredType == typeof(Table) || Framework.Do.IsAssignableFrom(desiredType, typeof(Table)))
				{
					return value.Table;
				}
				object obj2 = TableConversions.ConvertTableToType(value.Table, desiredType);
				if (obj2 != null)
				{
					return obj2;
				}
				break;
			}
			case DataType.UserData:
				if (value.UserData.Object != null)
				{
					object @object = value.UserData.Object;
					IUserDataDescriptor descriptor = value.UserData.Descriptor;
					if (descriptor.IsTypeCompatible(desiredType, @object))
					{
						return @object;
					}
					if (stringSubtype != StringConversions.StringSubtype.None)
					{
						text = descriptor.AsString(@object);
					}
					else
					{
						Func<object, object> oneToOne = Script.GlobalOptions.CustomConverters.GetOneToOne(@object.GetType(), desiredType);
						if (oneToOne != null)
						{
							object obj3 = oneToOne(@object);
							if (obj3 != null)
							{
								return obj3;
							}
						}
					}
				}
				break;
			case DataType.ClrFunction:
				if (desiredType == typeof(CallbackFunction))
				{
					return value.Callback;
				}
				if (desiredType == typeof(Func<ScriptExecutionContext, CallbackArguments, DynValue>))
				{
					return value.Callback.ClrCallback;
				}
				break;
			}
			if (stringSubtype != StringConversions.StringSubtype.None && text != null)
			{
				return StringConversions.ConvertString(stringSubtype, text, desiredType, value.Type);
			}
			Debug.Log(string.Format("Type={0}, DesiredType={1}", value.Type, desiredType));
			throw ScriptRuntimeException.ConvertObjectFailed(value.Type, desiredType);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x0011F3A0 File Offset: 0x0011D5A0
		internal static int DynValueToObjectOfTypeWeight(DynValue value, Type desiredType, bool isOptional)
		{
			if (desiredType.IsByRef)
			{
				desiredType = desiredType.GetElementType();
			}
			if (Script.GlobalOptions.CustomConverters.GetScriptToClrCustomConversion(value.Type, desiredType) != null)
			{
				return 100;
			}
			if (desiredType == typeof(DynValue))
			{
				return 100;
			}
			if (desiredType == typeof(object))
			{
				return 100;
			}
			StringConversions.StringSubtype stringSubtype = StringConversions.GetStringSubtype(desiredType);
			Type underlyingType = Nullable.GetUnderlyingType(desiredType);
			Type left = null;
			if (underlyingType != null)
			{
				left = desiredType;
				desiredType = underlyingType;
			}
			switch (value.Type)
			{
			case DataType.Nil:
				if (!Framework.Do.IsValueType(desiredType))
				{
					return 100;
				}
				if (left != null)
				{
					return 100;
				}
				if (isOptional)
				{
					return 25;
				}
				break;
			case DataType.Void:
				if (isOptional)
				{
					return 50;
				}
				if (!Framework.Do.IsValueType(desiredType) || left != null)
				{
					return 25;
				}
				break;
			case DataType.Boolean:
				if (desiredType == typeof(bool))
				{
					return 100;
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					return 5;
				}
				break;
			case DataType.Number:
				if (Framework.Do.IsEnum(desiredType))
				{
					return 90;
				}
				if (NumericConversions.NumericTypes.Contains(desiredType))
				{
					return ScriptToClrConversions.GetNumericTypeWeight(desiredType);
				}
				if (stringSubtype != StringConversions.StringSubtype.None)
				{
					return 50;
				}
				break;
			case DataType.String:
				if (stringSubtype == StringConversions.StringSubtype.String)
				{
					return 100;
				}
				if (stringSubtype == StringConversions.StringSubtype.StringBuilder)
				{
					return 99;
				}
				if (stringSubtype == StringConversions.StringSubtype.Char)
				{
					return 98;
				}
				break;
			case DataType.Function:
				if (desiredType == typeof(Closure))
				{
					return 100;
				}
				if (desiredType == typeof(ScriptFunctionDelegate))
				{
					return 100;
				}
				break;
			case DataType.Table:
				if (desiredType == typeof(Table) || Framework.Do.IsAssignableFrom(desiredType, typeof(Table)))
				{
					return 100;
				}
				if (TableConversions.CanConvertTableToType(value.Table, desiredType))
				{
					return 90;
				}
				break;
			case DataType.UserData:
				if (value.UserData.Object != null)
				{
					object @object = value.UserData.Object;
					IUserDataDescriptor descriptor = value.UserData.Descriptor;
					if (descriptor.IsTypeCompatible(desiredType, @object))
					{
						return 100;
					}
					if (Script.GlobalOptions.CustomConverters.GetOneToOne(descriptor.Type, desiredType) != null)
					{
						return 99;
					}
					if (stringSubtype != StringConversions.StringSubtype.None)
					{
						return 5;
					}
				}
				break;
			case DataType.ClrFunction:
				if (desiredType == typeof(CallbackFunction))
				{
					return 100;
				}
				if (desiredType == typeof(Func<ScriptExecutionContext, CallbackArguments, DynValue>))
				{
					return 100;
				}
				break;
			}
			return 0;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x0002503E File Offset: 0x0002323E
		private static int GetNumericTypeWeight(Type desiredType)
		{
			if (desiredType == typeof(double) || desiredType == typeof(decimal))
			{
				return 100;
			}
			return 99;
		}

		// Token: 0x04002E96 RID: 11926
		internal const int WEIGHT_MAX_VALUE = 100;

		// Token: 0x04002E97 RID: 11927
		internal const int WEIGHT_CUSTOM_CONVERTER_MATCH = 100;

		// Token: 0x04002E98 RID: 11928
		internal const int WEIGHT_EXACT_MATCH = 100;

		// Token: 0x04002E99 RID: 11929
		internal const int WEIGHT_ONE2ONE_MATCH = 99;

		// Token: 0x04002E9A RID: 11930
		internal const int WEIGHT_STRING_TO_STRINGBUILDER = 99;

		// Token: 0x04002E9B RID: 11931
		internal const int WEIGHT_STRING_TO_CHAR = 98;

		// Token: 0x04002E9C RID: 11932
		internal const int WEIGHT_NIL_TO_NULLABLE = 100;

		// Token: 0x04002E9D RID: 11933
		internal const int WEIGHT_NIL_TO_REFTYPE = 100;

		// Token: 0x04002E9E RID: 11934
		internal const int WEIGHT_VOID_WITH_DEFAULT = 50;

		// Token: 0x04002E9F RID: 11935
		internal const int WEIGHT_VOID_WITHOUT_DEFAULT = 25;

		// Token: 0x04002EA0 RID: 11936
		internal const int WEIGHT_NIL_WITH_DEFAULT = 25;

		// Token: 0x04002EA1 RID: 11937
		internal const int WEIGHT_BOOL_TO_STRING = 5;

		// Token: 0x04002EA2 RID: 11938
		internal const int WEIGHT_NUMBER_TO_STRING = 50;

		// Token: 0x04002EA3 RID: 11939
		internal const int WEIGHT_NUMBER_TO_ENUM = 90;

		// Token: 0x04002EA4 RID: 11940
		internal const int WEIGHT_USERDATA_TO_STRING = 5;

		// Token: 0x04002EA5 RID: 11941
		internal const int WEIGHT_TABLE_CONVERSION = 90;

		// Token: 0x04002EA6 RID: 11942
		internal const int WEIGHT_NUMBER_DOWNCAST = 99;

		// Token: 0x04002EA7 RID: 11943
		internal const int WEIGHT_NO_MATCH = 0;

		// Token: 0x04002EA8 RID: 11944
		internal const int WEIGHT_NO_EXTRA_PARAMS_BONUS = 100;

		// Token: 0x04002EA9 RID: 11945
		internal const int WEIGHT_EXTRA_PARAMS_MALUS = 2;

		// Token: 0x04002EAA RID: 11946
		internal const int WEIGHT_BYREF_BONUSMALUS = -10;

		// Token: 0x04002EAB RID: 11947
		internal const int WEIGHT_VARARGS_MALUS = 1;

		// Token: 0x04002EAC RID: 11948
		internal const int WEIGHT_VARARGS_EMPTY = 40;

		// Token: 0x04002EAD RID: 11949
		internal const int WEIGHT_METHOD_PARAMETER_PREFER_FLOAT_OVER_INT = 1;
	}
}
