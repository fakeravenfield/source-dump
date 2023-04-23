using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000830 RID: 2096
	public static class DescriptorHelpers
	{
		// Token: 0x0600340A RID: 13322 RVA: 0x001173E0 File Offset: 0x001155E0
		public static bool? GetVisibilityFromAttributes(this MemberInfo mi)
		{
			if (mi == null)
			{
				return new bool?(false);
			}
			MoonSharpVisibleAttribute moonSharpVisibleAttribute = mi.GetCustomAttributes(true).OfType<MoonSharpVisibleAttribute>().SingleOrDefault<MoonSharpVisibleAttribute>();
			MoonSharpHiddenAttribute moonSharpHiddenAttribute = mi.GetCustomAttributes(true).OfType<MoonSharpHiddenAttribute>().SingleOrDefault<MoonSharpHiddenAttribute>();
			if (moonSharpVisibleAttribute != null && moonSharpHiddenAttribute != null && moonSharpVisibleAttribute.Visible)
			{
				throw new InvalidOperationException(string.Format("A member ('{0}') can't have discording MoonSharpHiddenAttribute and MoonSharpVisibleAttribute.", mi.Name));
			}
			if (moonSharpHiddenAttribute != null)
			{
				return new bool?(false);
			}
			if (moonSharpVisibleAttribute != null)
			{
				return new bool?(moonSharpVisibleAttribute.Visible);
			}
			return null;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x00023A90 File Offset: 0x00021C90
		public static bool IsDelegateType(this Type t)
		{
			return Framework.Do.IsAssignableFrom(typeof(Delegate), t);
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x00117468 File Offset: 0x00115668
		public static string GetClrVisibility(this Type type)
		{
			if (type.IsPublic || type.IsNestedPublic)
			{
				return "public";
			}
			if ((type.IsNotPublic && !type.IsNested) || type.IsNestedAssembly)
			{
				return "internal";
			}
			if (type.IsNestedFamORAssem)
			{
				return "protected-internal";
			}
			if (type.IsNestedFamANDAssem || type.IsNestedFamily)
			{
				return "protected";
			}
			if (type.IsNestedPrivate)
			{
				return "private";
			}
			return "unknown";
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x001174E4 File Offset: 0x001156E4
		public static string GetClrVisibility(this FieldInfo info)
		{
			if (info.IsPublic)
			{
				return "public";
			}
			if (info.IsAssembly)
			{
				return "internal";
			}
			if (info.IsFamilyOrAssembly)
			{
				return "protected-internal";
			}
			if (info.IsFamilyAndAssembly || info.IsFamily)
			{
				return "protected";
			}
			if (info.IsPrivate)
			{
				return "private";
			}
			return "unknown";
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x00117544 File Offset: 0x00115744
		public static string GetClrVisibility(this PropertyInfo info)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(info);
			MethodInfo setMethod = Framework.Do.GetSetMethod(info);
			string text = (getMethod != null) ? getMethod.GetClrVisibility() : "private";
			string a = (setMethod != null) ? setMethod.GetClrVisibility() : "private";
			if (text == "public" || a == "public")
			{
				return "public";
			}
			if (text == "internal" || a == "internal")
			{
				return "internal";
			}
			return text;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x001175D8 File Offset: 0x001157D8
		public static string GetClrVisibility(this MethodBase info)
		{
			if (info.IsPublic)
			{
				return "public";
			}
			if (info.IsAssembly)
			{
				return "internal";
			}
			if (info.IsFamilyOrAssembly)
			{
				return "protected-internal";
			}
			if (info.IsFamilyAndAssembly || info.IsFamily)
			{
				return "protected";
			}
			if (info.IsPrivate)
			{
				return "private";
			}
			return "unknown";
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x00117638 File Offset: 0x00115838
		public static bool IsPropertyInfoPublic(this PropertyInfo pi)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(pi);
			MethodInfo setMethod = Framework.Do.GetSetMethod(pi);
			return (getMethod != null && getMethod.IsPublic) || (setMethod != null && setMethod.IsPublic);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x00117684 File Offset: 0x00115884
		public static List<string> GetMetaNamesFromAttributes(this MethodInfo mi)
		{
			return (from a in mi.GetCustomAttributes(typeof(MoonSharpUserDataMetamethodAttribute), true).OfType<MoonSharpUserDataMetamethodAttribute>()
			select a.Name).ToList<string>();
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x001176D0 File Offset: 0x001158D0
		public static Type[] SafeGetTypes(this Assembly asm)
		{
			Type[] result;
			try
			{
				result = Framework.Do.GetAssemblyTypes(asm);
			}
			catch (ReflectionTypeLoadException)
			{
				result = new Type[0];
			}
			return result;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x00117708 File Offset: 0x00115908
		public static string GetConversionMethodName(this Type type)
		{
			StringBuilder stringBuilder = new StringBuilder(type.Name);
			for (int i = 0; i < stringBuilder.Length; i++)
			{
				if (!char.IsLetterOrDigit(stringBuilder[i]))
				{
					stringBuilder[i] = '_';
				}
			}
			return "__to" + stringBuilder.ToString();
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x00023AA7 File Offset: 0x00021CA7
		public static IEnumerable<Type> GetAllImplementedTypes(this Type t)
		{
			Type ot = t;
			while (ot != null)
			{
				yield return ot;
				ot = Framework.Do.GetBaseType(ot);
			}
			ot = null;
			foreach (Type type in Framework.Do.GetInterfaces(t))
			{
				yield return type;
			}
			Type[] array = null;
			yield break;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x0011775C File Offset: 0x0011595C
		public static bool IsValidSimpleIdentifier(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			if (str[0] != '_' && !char.IsLetter(str[0]))
			{
				return false;
			}
			for (int i = 1; i < str.Length; i++)
			{
				if (str[i] != '_' && !char.IsLetterOrDigit(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x001177BC File Offset: 0x001159BC
		public static string ToValidSimpleIdentifier(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return "_";
			}
			if (str[0] != '_' && !char.IsLetter(str[0]))
			{
				str = "_" + str;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			for (int i = 0; i < stringBuilder.Length; i++)
			{
				if (stringBuilder[i] != '_' && !char.IsLetterOrDigit(stringBuilder[i]))
				{
					stringBuilder[i] = '_';
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x00117840 File Offset: 0x00115A40
		public static string Camelify(string name)
		{
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			bool flag = false;
			for (int i = 0; i < name.Length; i++)
			{
				if (name[i] == '_' && i != 0)
				{
					flag = true;
				}
				else
				{
					if (flag)
					{
						stringBuilder.Append(char.ToUpperInvariant(name[i]));
					}
					else
					{
						stringBuilder.Append(name[i]);
					}
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x001178AC File Offset: 0x00115AAC
		public static string UpperFirstLetter(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return char.ToUpperInvariant(name[0]).ToString() + name.Substring(1);
			}
			return name;
		}
	}
}
