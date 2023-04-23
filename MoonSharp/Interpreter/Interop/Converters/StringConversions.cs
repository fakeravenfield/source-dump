using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x0200088C RID: 2188
	internal static class StringConversions
	{
		// Token: 0x060036C8 RID: 14024 RVA: 0x00025069 File Offset: 0x00023269
		internal static StringConversions.StringSubtype GetStringSubtype(Type desiredType)
		{
			if (desiredType == typeof(string))
			{
				return StringConversions.StringSubtype.String;
			}
			if (desiredType == typeof(StringBuilder))
			{
				return StringConversions.StringSubtype.StringBuilder;
			}
			if (desiredType == typeof(char))
			{
				return StringConversions.StringSubtype.Char;
			}
			return StringConversions.StringSubtype.None;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000250A8 File Offset: 0x000232A8
		internal static object ConvertString(StringConversions.StringSubtype stringSubType, string str, Type desiredType, DataType dataType)
		{
			switch (stringSubType)
			{
			case StringConversions.StringSubtype.String:
				return str;
			case StringConversions.StringSubtype.StringBuilder:
				return new StringBuilder(str);
			case StringConversions.StringSubtype.Char:
				if (!string.IsNullOrEmpty(str))
				{
					return str[0];
				}
				break;
			}
			throw ScriptRuntimeException.ConvertObjectFailed(dataType, desiredType);
		}

		// Token: 0x0200088D RID: 2189
		internal enum StringSubtype
		{
			// Token: 0x04002EAF RID: 11951
			None,
			// Token: 0x04002EB0 RID: 11952
			String,
			// Token: 0x04002EB1 RID: 11953
			StringBuilder,
			// Token: 0x04002EB2 RID: 11954
			Char
		}
	}
}
