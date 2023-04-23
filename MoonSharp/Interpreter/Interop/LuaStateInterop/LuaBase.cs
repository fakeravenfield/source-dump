using System;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000884 RID: 2180
	public class LuaBase
	{
		// Token: 0x06003658 RID: 13912 RVA: 0x00024C6E File Offset: 0x00022E6E
		protected static DynValue GetArgument(LuaState L, int pos)
		{
			return L.At(pos);
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x00024C77 File Offset: 0x00022E77
		protected static DynValue ArgAsType(LuaState L, int pos, DataType type, bool allowNil = false)
		{
			return LuaBase.GetArgument(L, pos).CheckType(L.FunctionName, type, pos - 1, allowNil ? (TypeValidationFlags.AllowNil | TypeValidationFlags.AutoConvert) : TypeValidationFlags.AutoConvert);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x0011D000 File Offset: 0x0011B200
		protected static int LuaType(LuaState L, int p)
		{
			switch (LuaBase.GetArgument(L, p).Type)
			{
			case DataType.Nil:
				return 0;
			case DataType.Void:
				return -1;
			case DataType.Boolean:
				return 0;
			case DataType.Number:
				return 3;
			case DataType.String:
				return 4;
			case DataType.Function:
				return 6;
			case DataType.Table:
				return 5;
			case DataType.UserData:
				return 7;
			case DataType.Thread:
				return 8;
			case DataType.ClrFunction:
				return 6;
			}
			throw new ScriptRuntimeException("Can't call LuaType on any type");
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x0011D074 File Offset: 0x0011B274
		protected static string LuaLCheckLString(LuaState L, int argNum, out uint l)
		{
			string @string = LuaBase.ArgAsType(L, argNum, DataType.String, false).String;
			l = (uint)@string.Length;
			return @string;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x00024C96 File Offset: 0x00022E96
		protected static void LuaPushInteger(LuaState L, int val)
		{
			L.Push(DynValue.NewNumber((double)val));
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x00024CA5 File Offset: 0x00022EA5
		protected static int LuaToBoolean(LuaState L, int p)
		{
			if (!LuaBase.GetArgument(L, p).CastToBool())
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x00024CB8 File Offset: 0x00022EB8
		protected static string LuaToLString(LuaState luaState, int p, out uint l)
		{
			return LuaBase.LuaLCheckLString(luaState, p, out l);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x0011D09C File Offset: 0x0011B29C
		protected static string LuaToString(LuaState luaState, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(luaState, p, out num);
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x00024CC2 File Offset: 0x00022EC2
		protected static void LuaLAddValue(LuaLBuffer b)
		{
			b.StringBuilder.Append(b.LuaState.Pop().ToPrintString());
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x00024CE0 File Offset: 0x00022EE0
		protected static void LuaLAddLString(LuaLBuffer b, CharPtr s, uint p)
		{
			b.StringBuilder.Append(s.ToString((int)p));
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x00024CF5 File Offset: 0x00022EF5
		protected static void LuaLAddString(LuaLBuffer b, string s)
		{
			b.StringBuilder.Append(s.ToString());
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x0011D0B4 File Offset: 0x0011B2B4
		protected static int LuaLOptInteger(LuaState L, int pos, int def)
		{
			DynValue dynValue = LuaBase.ArgAsType(L, pos, DataType.Number, true);
			if (dynValue.IsNil())
			{
				return def;
			}
			return (int)dynValue.Number;
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x00024D09 File Offset: 0x00022F09
		protected static int LuaLCheckInteger(LuaState L, int pos)
		{
			return (int)LuaBase.ArgAsType(L, pos, DataType.Number, false).Number;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x00024D1A File Offset: 0x00022F1A
		protected static void LuaLArgCheck(LuaState L, bool condition, int argNum, string message)
		{
			if (!condition)
			{
				LuaBase.LuaLArgError(L, argNum, message);
			}
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x00024D27 File Offset: 0x00022F27
		protected static int LuaLCheckInt(LuaState L, int argNum)
		{
			return LuaBase.LuaLCheckInteger(L, argNum);
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x00024D30 File Offset: 0x00022F30
		protected static int LuaGetTop(LuaState L)
		{
			return L.Count;
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x00024D38 File Offset: 0x00022F38
		protected static int LuaLError(LuaState luaState, string message, params object[] args)
		{
			throw new ScriptRuntimeException(message, args);
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x00024D41 File Offset: 0x00022F41
		protected static void LuaLAddChar(LuaLBuffer b, char p)
		{
			b.StringBuilder.Append(p);
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x0000296E File Offset: 0x00000B6E
		protected static void LuaLBuffInit(LuaState L, LuaLBuffer b)
		{
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x00024D50 File Offset: 0x00022F50
		protected static void LuaPushLiteral(LuaState L, string literalString)
		{
			L.Push(DynValue.NewString(literalString));
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x00024D5E File Offset: 0x00022F5E
		protected static void LuaLPushResult(LuaLBuffer b)
		{
			LuaBase.LuaPushLiteral(b.LuaState, b.StringBuilder.ToString());
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0011D0DC File Offset: 0x0011B2DC
		protected static void LuaPushLString(LuaState L, CharPtr s, uint len)
		{
			string str = s.ToString((int)len);
			L.Push(DynValue.NewString(str));
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x0000296E File Offset: 0x00000B6E
		protected static void LuaLCheckStack(LuaState L, int n, string message)
		{
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x00024D76 File Offset: 0x00022F76
		protected static string LUA_QL(string p)
		{
			return "'" + p + "'";
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x00024D88 File Offset: 0x00022F88
		protected static void LuaPushNil(LuaState L)
		{
			L.Push(DynValue.Nil);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0000296E File Offset: 0x00000B6E
		protected static void LuaAssert(bool p)
		{
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x00024D95 File Offset: 0x00022F95
		protected static string LuaLTypeName(LuaState L, int p)
		{
			return L.At(p).Type.ToErrorTypeString();
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x0011D100 File Offset: 0x0011B300
		protected static int LuaIsString(LuaState L, int p)
		{
			DynValue dynValue = L.At(p);
			if (dynValue.Type != DataType.String && dynValue.Type != DataType.Number)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x0011D12C File Offset: 0x0011B32C
		protected static void LuaPop(LuaState L, int p)
		{
			for (int i = 0; i < p; i++)
			{
				L.Pop();
			}
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x0011D14C File Offset: 0x0011B34C
		protected static void LuaGetTable(LuaState L, int p)
		{
			DynValue key = L.Pop();
			DynValue dynValue = L.At(p);
			if (dynValue.Type != DataType.Table)
			{
				throw new NotImplementedException();
			}
			DynValue v = dynValue.Table.Get(key);
			L.Push(v);
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x00024DA8 File Offset: 0x00022FA8
		protected static int LuaLOptInt(LuaState L, int pos, int def)
		{
			return LuaBase.LuaLOptInteger(L, pos, def);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x0011D18C File Offset: 0x0011B38C
		protected static CharPtr LuaLCheckString(LuaState L, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(L, p, out num);
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x0011D09C File Offset: 0x0011B29C
		protected static string LuaLCheckStringStr(LuaState L, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(L, p, out num);
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x00024DB2 File Offset: 0x00022FB2
		protected static void LuaLArgError(LuaState L, int arg, string p)
		{
			throw ScriptRuntimeException.BadArgument(arg - 1, L.FunctionName, p);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x00024DC3 File Offset: 0x00022FC3
		protected static double LuaLCheckNumber(LuaState L, int pos)
		{
			return LuaBase.ArgAsType(L, pos, DataType.Number, false).Number;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x0011D1A8 File Offset: 0x0011B3A8
		protected static void LuaPushValue(LuaState L, int arg)
		{
			DynValue v = L.At(arg);
			L.Push(v);
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x0011D1C4 File Offset: 0x0011B3C4
		protected static void LuaCall(LuaState L, int nargs, int nresults = -1)
		{
			DynValue[] topArray = L.GetTopArray(nargs);
			L.Discard(nargs);
			DynValue func = L.Pop();
			DynValue dynValue = L.ExecutionContext.Call(func, topArray);
			if (nresults != 0)
			{
				if (nresults == -1)
				{
					nresults = ((dynValue.Type == DataType.Tuple) ? dynValue.Tuple.Length : 1);
				}
				DynValue[] array;
				if (dynValue.Type != DataType.Tuple)
				{
					(array = new DynValue[1])[0] = dynValue;
				}
				else
				{
					array = dynValue.Tuple;
				}
				DynValue[] array2 = array;
				int i = 0;
				int j = 0;
				while (j < array2.Length)
				{
					if (i >= nresults)
					{
						break;
					}
					L.Push(array2[j]);
					j++;
					i++;
				}
				while (i < nresults)
				{
					L.Push(DynValue.Nil);
				}
			}
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x00024DD3 File Offset: 0x00022FD3
		protected static int memcmp(CharPtr ptr1, CharPtr ptr2, uint size)
		{
			return LuaBase.memcmp(ptr1, ptr2, (int)size);
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x0011D26C File Offset: 0x0011B46C
		protected static int memcmp(CharPtr ptr1, CharPtr ptr2, int size)
		{
			int i = 0;
			while (i < size)
			{
				if (ptr1[i] != ptr2[i])
				{
					if (ptr1[i] < ptr2[i])
					{
						return -1;
					}
					return 1;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0011D2AC File Offset: 0x0011B4AC
		protected static CharPtr memchr(CharPtr ptr, char c, uint count)
		{
			for (uint num = 0U; num < count; num += 1U)
			{
				if (ptr[num] == c)
				{
					return new CharPtr(ptr.chars, (int)((long)ptr.index + (long)((ulong)num)));
				}
			}
			return null;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0011D2E8 File Offset: 0x0011B4E8
		protected static CharPtr strpbrk(CharPtr str, CharPtr charset)
		{
			int num = 0;
			while (str[num] != '\0')
			{
				int num2 = 0;
				while (charset[num2] != '\0')
				{
					if (str[num] == charset[num2])
					{
						return new CharPtr(str.chars, str.index + num);
					}
					num2++;
				}
				num++;
			}
			return null;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x00024DDD File Offset: 0x00022FDD
		protected static bool isalpha(char c)
		{
			return char.IsLetter(c);
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x00024DE5 File Offset: 0x00022FE5
		protected static bool iscntrl(char c)
		{
			return char.IsControl(c);
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x00024DED File Offset: 0x00022FED
		protected static bool isdigit(char c)
		{
			return char.IsDigit(c);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x00024DF5 File Offset: 0x00022FF5
		protected static bool islower(char c)
		{
			return char.IsLower(c);
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x00024DFD File Offset: 0x00022FFD
		protected static bool ispunct(char c)
		{
			return char.IsPunctuation(c);
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x00024E05 File Offset: 0x00023005
		protected static bool isspace(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r');
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x00024E1D File Offset: 0x0002301D
		protected static bool isupper(char c)
		{
			return char.IsUpper(c);
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x00024E25 File Offset: 0x00023025
		protected static bool isalnum(char c)
		{
			return char.IsLetterOrDigit(c);
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x00024E2D File Offset: 0x0002302D
		protected static bool isxdigit(char c)
		{
			return "0123456789ABCDEFabcdef".IndexOf(c) >= 0;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x00024E40 File Offset: 0x00023040
		protected static bool isgraph(char c)
		{
			return !char.IsControl(c) && !char.IsWhiteSpace(c);
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x00024E55 File Offset: 0x00023055
		protected static bool isalpha(int c)
		{
			return char.IsLetter((char)c);
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x00024E5E File Offset: 0x0002305E
		protected static bool iscntrl(int c)
		{
			return char.IsControl((char)c);
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x00024E67 File Offset: 0x00023067
		protected static bool isdigit(int c)
		{
			return char.IsDigit((char)c);
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x00024E70 File Offset: 0x00023070
		protected static bool islower(int c)
		{
			return char.IsLower((char)c);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x00024E79 File Offset: 0x00023079
		protected static bool ispunct(int c)
		{
			return (ushort)c != 32 && !LuaBase.isalnum((char)c);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x00024E8D File Offset: 0x0002308D
		protected static bool isspace(int c)
		{
			return (ushort)c == 32 || ((ushort)c >= 9 && (ushort)c <= 13);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x00024EA8 File Offset: 0x000230A8
		protected static bool isupper(int c)
		{
			return char.IsUpper((char)c);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x00024EB1 File Offset: 0x000230B1
		protected static bool isalnum(int c)
		{
			return char.IsLetterOrDigit((char)c);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x00024EBA File Offset: 0x000230BA
		protected static bool isgraph(int c)
		{
			return !char.IsControl((char)c) && !char.IsWhiteSpace((char)c);
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x00024ED1 File Offset: 0x000230D1
		protected static char tolower(char c)
		{
			return char.ToLower(c);
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x00024ED9 File Offset: 0x000230D9
		protected static char toupper(char c)
		{
			return char.ToUpper(c);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x00024EE1 File Offset: 0x000230E1
		protected static char tolower(int c)
		{
			return char.ToLower((char)c);
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x00024EEA File Offset: 0x000230EA
		protected static char toupper(int c)
		{
			return char.ToUpper((char)c);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x0011D33C File Offset: 0x0011B53C
		protected static CharPtr strchr(CharPtr str, char c)
		{
			int num = str.index;
			while (str.chars[num] != '\0')
			{
				if (str.chars[num] == c)
				{
					return new CharPtr(str.chars, num);
				}
				num++;
			}
			return null;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0011D37C File Offset: 0x0011B57C
		protected static CharPtr strcpy(CharPtr dst, CharPtr src)
		{
			int num = 0;
			while (src[num] != '\0')
			{
				dst[num] = src[num];
				num++;
			}
			dst[num] = '\0';
			return dst;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0011D3B4 File Offset: 0x0011B5B4
		protected static CharPtr strncpy(CharPtr dst, CharPtr src, int length)
		{
			int i = 0;
			while (src[i] != '\0')
			{
				if (i >= length)
				{
					break;
				}
				dst[i] = src[i];
				i++;
			}
			while (i < length)
			{
				dst[i++] = '\0';
			}
			return dst;
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x0011D3F8 File Offset: 0x0011B5F8
		protected static int strlen(CharPtr str)
		{
			int num = 0;
			while (str[num] != '\0')
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x0011D418 File Offset: 0x0011B618
		public static void sprintf(CharPtr buffer, CharPtr str, params object[] argv)
		{
			string str2 = Tools.sprintf(str.ToString(), argv);
			LuaBase.strcpy(buffer, str2);
		}

		// Token: 0x04002E82 RID: 11906
		protected const int LUA_TNONE = -1;

		// Token: 0x04002E83 RID: 11907
		protected const int LUA_TNIL = 0;

		// Token: 0x04002E84 RID: 11908
		protected const int LUA_TBOOLEAN = 1;

		// Token: 0x04002E85 RID: 11909
		protected const int LUA_TLIGHTUSERDATA = 2;

		// Token: 0x04002E86 RID: 11910
		protected const int LUA_TNUMBER = 3;

		// Token: 0x04002E87 RID: 11911
		protected const int LUA_TSTRING = 4;

		// Token: 0x04002E88 RID: 11912
		protected const int LUA_TTABLE = 5;

		// Token: 0x04002E89 RID: 11913
		protected const int LUA_TFUNCTION = 6;

		// Token: 0x04002E8A RID: 11914
		protected const int LUA_TUSERDATA = 7;

		// Token: 0x04002E8B RID: 11915
		protected const int LUA_TTHREAD = 8;

		// Token: 0x04002E8C RID: 11916
		protected const int LUA_MULTRET = -1;

		// Token: 0x04002E8D RID: 11917
		protected const string LUA_INTFRMLEN = "l";
	}
}
