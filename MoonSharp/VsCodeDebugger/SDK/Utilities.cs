using System;
using System.Reflection;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200079B RID: 1947
	internal class Utilities
	{
		// Token: 0x06002FCE RID: 12238 RVA: 0x0010C08C File Offset: 0x0010A28C
		public static string ExpandVariables(string format, object variables, bool underscoredOnly = true)
		{
			if (variables == null)
			{
				variables = new
				{

				};
			}
			Type type = variables.GetType();
			return Utilities.VARIABLE.Replace(format, delegate(Match match)
			{
				string value = match.Groups[1].Value;
				if (underscoredOnly && !value.StartsWith("_"))
				{
					return match.Groups[0].Value;
				}
				PropertyInfo property = Framework.Do.GetProperty(type, value);
				if (property != null)
				{
					return property.GetValue(variables, null).ToString();
				}
				return "{" + value + ": not found}";
			});
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x00020E53 File Offset: 0x0001F053
		public static string MakeRelativePath(string dirPath, string absPath)
		{
			if (!dirPath.EndsWith("/"))
			{
				dirPath += "/";
			}
			if (absPath.StartsWith(dirPath))
			{
				return absPath.Replace(dirPath, "");
			}
			return absPath;
		}

		// Token: 0x04002B90 RID: 11152
		private static readonly Regex VARIABLE = new Regex("\\{(\\w+)\\}");
	}
}
