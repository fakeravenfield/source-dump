using System;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007E8 RID: 2024
	internal class Token
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x00022E44 File Offset: 0x00021044
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x00022E4C File Offset: 0x0002104C
		public string Text { get; set; }

		// Token: 0x0600329C RID: 12956 RVA: 0x00112298 File Offset: 0x00110498
		public Token(TokenType type, int sourceId, int fromLine, int fromCol, int toLine, int toCol, int prevLine, int prevCol)
		{
			this.Type = type;
			this.SourceId = sourceId;
			this.FromLine = fromLine;
			this.FromCol = fromCol;
			this.ToCol = toCol;
			this.ToLine = toLine;
			this.PrevCol = prevCol;
			this.PrevLine = prevLine;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x001122E8 File Offset: 0x001104E8
		public override string ToString()
		{
			string arg = (this.Type.ToString() + "                                                      ").Substring(0, 16);
			string text = string.Format("{0}:{1}-{2}:{3}", new object[]
			{
				this.FromLine,
				this.FromCol,
				this.ToLine,
				this.ToCol
			});
			text = (text + "                                                      ").Substring(0, 10);
			return string.Format("{0}  - {1} - '{2}'", arg, text, this.Text ?? "");
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x00112394 File Offset: 0x00110594
		public static TokenType? GetReservedTokenType(string reservedWord)
		{
			if (reservedWord != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(reservedWord);
				if (num <= 1646057492U)
				{
					if (num <= 699505802U)
					{
						if (num <= 228849900U)
						{
							if (num != 184981848U)
							{
								if (num == 228849900U)
								{
									if (reservedWord == "nil")
									{
										return new TokenType?(TokenType.Nil);
									}
								}
							}
							else if (reservedWord == "false")
							{
								return new TokenType?(TokenType.False);
							}
						}
						else if (num != 231090382U)
						{
							if (num != 254395046U)
							{
								if (num == 699505802U)
								{
									if (reservedWord == "not")
									{
										return new TokenType?(TokenType.Not);
									}
								}
							}
							else if (reservedWord == "and")
							{
								return new TokenType?(TokenType.And);
							}
						}
						else if (reservedWord == "while")
						{
							return new TokenType?(TokenType.While);
						}
					}
					else if (num <= 1303515621U)
					{
						if (num != 959999494U)
						{
							if (num != 1094220446U)
							{
								if (num == 1303515621U)
								{
									if (reservedWord == "true")
									{
										return new TokenType?(TokenType.True);
									}
								}
							}
							else if (reservedWord == "in")
							{
								return new TokenType?(TokenType.In);
							}
						}
						else if (reservedWord == "if")
						{
							return new TokenType?(TokenType.If);
						}
					}
					else if (num != 1414876295U)
					{
						if (num != 1563699588U)
						{
							if (num == 1646057492U)
							{
								if (reservedWord == "do")
								{
									return new TokenType?(TokenType.Do);
								}
							}
						}
						else if (reservedWord == "or")
						{
							return new TokenType?(TokenType.Or);
						}
					}
					else if (reservedWord == "elseif")
					{
						return new TokenType?(TokenType.ElseIf);
					}
				}
				else if (num <= 2901640080U)
				{
					if (num <= 2246981567U)
					{
						if (num != 1787721130U)
						{
							if (num == 2246981567U)
							{
								if (reservedWord == "return")
								{
									return new TokenType?(TokenType.Return);
								}
							}
						}
						else if (reservedWord == "end")
						{
							return new TokenType?(TokenType.End);
						}
					}
					else if (num != 2621662984U)
					{
						if (num != 2664841801U)
						{
							if (num == 2901640080U)
							{
								if (reservedWord == "for")
								{
									return new TokenType?(TokenType.For);
								}
							}
						}
						else if (reservedWord == "function")
						{
							return new TokenType?(TokenType.Function);
						}
					}
					else if (reservedWord == "local")
					{
						return new TokenType?(TokenType.Local);
					}
				}
				else if (num <= 3378807160U)
				{
					if (num != 3132432719U)
					{
						if (num != 3183434736U)
						{
							if (num == 3378807160U)
							{
								if (reservedWord == "break")
								{
									return new TokenType?(TokenType.Break);
								}
							}
						}
						else if (reservedWord == "else")
						{
							return new TokenType?(TokenType.Else);
						}
					}
					else if (reservedWord == "until")
					{
						return new TokenType?(TokenType.Until);
					}
				}
				else if (num != 3650857002U)
				{
					if (num != 3844270454U)
					{
						if (num == 4121104358U)
						{
							if (reservedWord == "goto")
							{
								return new TokenType?(TokenType.Goto);
							}
						}
					}
					else if (reservedWord == "then")
					{
						return new TokenType?(TokenType.Then);
					}
				}
				else if (reservedWord == "repeat")
				{
					return new TokenType?(TokenType.Repeat);
				}
			}
			return null;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x00022E55 File Offset: 0x00021055
		public double GetNumberValue()
		{
			if (this.Type == TokenType.Number)
			{
				return LexerUtils.ParseNumber(this);
			}
			if (this.Type == TokenType.Number_Hex)
			{
				return LexerUtils.ParseHexInteger(this);
			}
			if (this.Type == TokenType.Number_HexFloat)
			{
				return LexerUtils.ParseHexFloat(this);
			}
			throw new NotSupportedException("GetNumberValue is supported only on numeric tokens");
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x00112780 File Offset: 0x00110980
		public bool IsEndOfBlock()
		{
			TokenType type = this.Type;
			return type == TokenType.Eof || type - TokenType.Else <= 2 || type == TokenType.Until;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x00022E94 File Offset: 0x00021094
		public bool IsUnaryOperator()
		{
			return this.Type == TokenType.Op_MinusOrSub || this.Type == TokenType.Not || this.Type == TokenType.Op_Len;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x001127A8 File Offset: 0x001109A8
		public bool IsBinaryOperator()
		{
			TokenType type = this.Type;
			if (type != TokenType.And)
			{
				switch (type)
				{
				case TokenType.Or:
				case TokenType.Op_Equal:
				case TokenType.Op_LessThan:
				case TokenType.Op_LessThanEqual:
				case TokenType.Op_GreaterThanEqual:
				case TokenType.Op_GreaterThan:
				case TokenType.Op_NotEqual:
				case TokenType.Op_Concat:
					return true;
				case TokenType.Repeat:
				case TokenType.Return:
				case TokenType.Then:
				case TokenType.True:
				case TokenType.Until:
				case TokenType.While:
				case TokenType.Op_Assignment:
					break;
				default:
					if (type - TokenType.Op_Pwr <= 5)
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x00022EB6 File Offset: 0x000210B6
		internal SourceRef GetSourceRef(bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, this.ToCol, this.FromLine, this.ToLine, isStepStop);
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x00022EDC File Offset: 0x000210DC
		internal SourceRef GetSourceRef(Token to, bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, to.ToCol, this.FromLine, to.ToLine, isStepStop);
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x00022F02 File Offset: 0x00021102
		internal SourceRef GetSourceRefUpTo(Token to, bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, to.PrevCol, this.FromLine, to.PrevLine, isStepStop);
		}

		// Token: 0x04002C9B RID: 11419
		public readonly int SourceId;

		// Token: 0x04002C9C RID: 11420
		public readonly int FromCol;

		// Token: 0x04002C9D RID: 11421
		public readonly int ToCol;

		// Token: 0x04002C9E RID: 11422
		public readonly int FromLine;

		// Token: 0x04002C9F RID: 11423
		public readonly int ToLine;

		// Token: 0x04002CA0 RID: 11424
		public readonly int PrevCol;

		// Token: 0x04002CA1 RID: 11425
		public readonly int PrevLine;

		// Token: 0x04002CA2 RID: 11426
		public readonly TokenType Type;
	}
}
