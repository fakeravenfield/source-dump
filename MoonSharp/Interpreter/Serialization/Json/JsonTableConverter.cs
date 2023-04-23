using System;
using System.Text;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter.Serialization.Json
{
	// Token: 0x02000814 RID: 2068
	public static class JsonTableConverter
	{
		// Token: 0x06003360 RID: 13152 RVA: 0x000234C4 File Offset: 0x000216C4
		public static string TableToJson(this Table table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			JsonTableConverter.TableToJson(stringBuilder, table);
			return stringBuilder.ToString();
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x00116478 File Offset: 0x00114678
		private static void TableToJson(StringBuilder sb, Table table)
		{
			bool flag = true;
			if (table.Length == 0)
			{
				sb.Append("{");
				foreach (TablePair tablePair in table.Pairs)
				{
					if (tablePair.Key.Type == DataType.String && JsonTableConverter.IsValueJsonCompatible(tablePair.Value))
					{
						if (!flag)
						{
							sb.Append(',');
						}
						JsonTableConverter.ValueToJson(sb, tablePair.Key);
						sb.Append(':');
						JsonTableConverter.ValueToJson(sb, tablePair.Value);
						flag = false;
					}
				}
				sb.Append("}");
				return;
			}
			sb.Append("[");
			for (int i = 1; i <= table.Length; i++)
			{
				DynValue value = table.Get(i);
				if (JsonTableConverter.IsValueJsonCompatible(value))
				{
					if (!flag)
					{
						sb.Append(',');
					}
					JsonTableConverter.ValueToJson(sb, value);
					flag = false;
				}
			}
			sb.Append("]");
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000234D7 File Offset: 0x000216D7
		public static string ObjectToJson(object obj)
		{
			return ObjectValueConverter.SerializeObjectToDynValue(null, obj, JsonNull.Create()).Table.TableToJson();
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x00116584 File Offset: 0x00114784
		private static void ValueToJson(StringBuilder sb, DynValue value)
		{
			switch (value.Type)
			{
			case DataType.Boolean:
				sb.Append(value.Boolean ? "true" : "false");
				return;
			case DataType.Number:
				sb.Append(value.Number.ToString("r"));
				return;
			case DataType.String:
				sb.Append(JsonTableConverter.EscapeString(value.String ?? ""));
				return;
			case DataType.Table:
				JsonTableConverter.TableToJson(sb, value.Table);
				return;
			}
			sb.Append("null");
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x00116630 File Offset: 0x00114830
		private static string EscapeString(string s)
		{
			s = s.Replace("\\", "\\\\");
			s = s.Replace("/", "\\/");
			s = s.Replace("\"", "\\\"");
			s = s.Replace("\f", "\\f");
			s = s.Replace("\b", "\\b");
			s = s.Replace("\n", "\\n");
			s = s.Replace("\r", "\\r");
			s = s.Replace("\t", "\\t");
			return "\"" + s + "\"";
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000234EF File Offset: 0x000216EF
		private static bool IsValueJsonCompatible(DynValue value)
		{
			return value.Type == DataType.Boolean || value.IsNil() || value.Type == DataType.Number || value.Type == DataType.String || value.Type == DataType.Table || JsonNull.IsJsonNull(value);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x001166E0 File Offset: 0x001148E0
		public static Table JsonToTable(string json, Script script = null)
		{
			Lexer lexer = new Lexer(0, json, false);
			if (lexer.Current.Type == TokenType.Brk_Open_Curly)
			{
				return JsonTableConverter.ParseJsonObject(lexer, script);
			}
			if (lexer.Current.Type == TokenType.Brk_Open_Square)
			{
				return JsonTableConverter.ParseJsonArray(lexer, script);
			}
			throw new SyntaxErrorException(lexer.Current, "Unexpected token : '{0}'", new object[]
			{
				lexer.Current.Text
			});
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x00023525 File Offset: 0x00021725
		private static void AssertToken(Lexer L, TokenType type)
		{
			if (L.Current.Type != type)
			{
				throw new SyntaxErrorException(L.Current, "Unexpected token : '{0}'", new object[]
				{
					L.Current.Text
				});
			}
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x00116748 File Offset: 0x00114948
		private static Table ParseJsonArray(Lexer L, Script script)
		{
			Table table = new Table(script);
			L.Next();
			while (L.Current.Type != TokenType.Brk_Close_Square)
			{
				DynValue value = JsonTableConverter.ParseJsonValue(L, script);
				table.Append(value);
				L.Next();
				if (L.Current.Type == TokenType.Comma)
				{
					L.Next();
				}
			}
			return table;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x001167A0 File Offset: 0x001149A0
		private static Table ParseJsonObject(Lexer L, Script script)
		{
			Table table = new Table(script);
			L.Next();
			while (L.Current.Type != TokenType.Brk_Close_Curly)
			{
				JsonTableConverter.AssertToken(L, TokenType.String);
				string text = L.Current.Text;
				L.Next();
				JsonTableConverter.AssertToken(L, TokenType.Colon);
				L.Next();
				DynValue value = JsonTableConverter.ParseJsonValue(L, script);
				table.Set(text, value);
				L.Next();
				if (L.Current.Type == TokenType.Comma)
				{
					L.Next();
				}
			}
			return table;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x00116820 File Offset: 0x00114A20
		private static DynValue ParseJsonValue(Lexer L, Script script)
		{
			if (L.Current.Type == TokenType.Brk_Open_Curly)
			{
				return DynValue.NewTable(JsonTableConverter.ParseJsonObject(L, script));
			}
			if (L.Current.Type == TokenType.Brk_Open_Square)
			{
				return DynValue.NewTable(JsonTableConverter.ParseJsonArray(L, script));
			}
			if (L.Current.Type == TokenType.String)
			{
				return DynValue.NewString(L.Current.Text);
			}
			if (L.Current.Type == TokenType.Number)
			{
				return DynValue.NewNumber(L.Current.GetNumberValue()).AsReadOnly();
			}
			if (L.Current.Type == TokenType.True)
			{
				return DynValue.True;
			}
			if (L.Current.Type == TokenType.False)
			{
				return DynValue.False;
			}
			if (L.Current.Type == TokenType.Name && L.Current.Text == "null")
			{
				return JsonNull.Create();
			}
			throw new SyntaxErrorException(L.Current, "Unexpected token : '{0}'", new object[]
			{
				L.Current.Text
			});
		}
	}
}
