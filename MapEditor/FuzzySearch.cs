using System;
using System.Collections.Generic;

namespace MapEditor
{
	// Token: 0x020005FB RID: 1531
	public static class FuzzySearch
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x000F7B48 File Offset: 0x000F5D48
		public static string GetDisplayName(string name)
		{
			string text = "";
			bool flag = false;
			for (int i = 0; i < name.Length; i++)
			{
				string text2 = name[i].ToString();
				bool flag2 = text2.ToUpper() == text2;
				if (flag2 && !flag && i > 0)
				{
					text += " ";
				}
				flag = flag2;
				if (text2 == "_")
				{
					text += " ";
				}
				else
				{
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000F7BC8 File Offset: 0x000F5DC8
		public static int GetScore(string query, string text)
		{
			text = text.ToLower();
			query = query.ToLower();
			List<FuzzySearch.Match> list = new List<FuzzySearch.Match>(8);
			bool[] array = new bool[text.Length];
			int num = 0;
			for (int i = 0; i < query.Length; i++)
			{
				list.Clear();
				char c = query[i];
				if (c != ' ')
				{
					for (int j = 0; j < text.Length; j++)
					{
						bool flag = false;
						for (int k = j; k < text.Length; k++)
						{
							if (text[k] == c && !array[k])
							{
								j = k;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							break;
						}
						int num2 = 1;
						int num3 = 1;
						while (num3 + j < text.Length && num3 + i < query.Length && text[num3 + j] == query[num3 + i])
						{
							num2++;
							num3++;
						}
						list.Add(new FuzzySearch.Match(text, j, i, num2));
						j += num2 - 1;
						if (list.Count == 8)
						{
							break;
						}
					}
					FuzzySearch.Match match = FuzzySearch.Match.empty;
					foreach (FuzzySearch.Match match2 in list)
					{
						if (match2.score > match.score)
						{
							match = match2;
						}
					}
					if (!(match == FuzzySearch.Match.empty))
					{
						num += match.score;
						i += match.matchLength - 1;
						for (int l = 0; l < match.matchLength; l++)
						{
							array[match.textIndex + l] = true;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x020005FC RID: 1532
		private struct Match : IEquatable<FuzzySearch.Match>
		{
			// Token: 0x06002744 RID: 10052 RVA: 0x0001B1B3 File Offset: 0x000193B3
			public Match(string text, int textIndex, int queryIndex, int matchLength)
			{
				this.textIndex = textIndex;
				this.matchLength = matchLength;
				this.score = FuzzySearch.Match.ComputeScore(text, textIndex, queryIndex, matchLength);
			}

			// Token: 0x06002745 RID: 10053 RVA: 0x000F7D84 File Offset: 0x000F5F84
			private static int ComputeScore(string text, int textIndex, int queryIndex, int matchLength)
			{
				int num = textIndex + matchLength;
				bool flag = textIndex == 0 && queryIndex == 0;
				bool flag2 = num == text.Length;
				bool flag3 = flag || (textIndex > 0 && text[textIndex - 1] == ' ');
				if (!flag2)
				{
					if (num < text.Length)
					{
						bool flag4 = text[num] == ' ';
					}
				}
				return matchLength * matchLength + ((flag || flag3) ? ((int)((double)matchLength * 1.5)) : 0) + (flag2 ? ((int)((double)matchLength * 1.2)) : 0);
			}

			// Token: 0x06002746 RID: 10054 RVA: 0x0001B1D4 File Offset: 0x000193D4
			public bool Equals(FuzzySearch.Match other)
			{
				return this.textIndex == other.textIndex && this.matchLength == other.matchLength && this.score == other.score;
			}

			// Token: 0x06002747 RID: 10055 RVA: 0x000F7E10 File Offset: 0x000F6010
			public override int GetHashCode()
			{
				return ((1841149249 * -1521134295 + this.textIndex.GetHashCode()) * -1521134295 + this.matchLength.GetHashCode()) * -1521134295 + this.score.GetHashCode();
			}

			// Token: 0x06002748 RID: 10056 RVA: 0x0001B202 File Offset: 0x00019402
			public override bool Equals(object obj)
			{
				return obj is FuzzySearch.Match && this.Equals((FuzzySearch.Match)obj);
			}

			// Token: 0x06002749 RID: 10057 RVA: 0x0001B21A File Offset: 0x0001941A
			public static bool operator ==(FuzzySearch.Match lhs, FuzzySearch.Match rhs)
			{
				return lhs.Equals(rhs);
			}

			// Token: 0x0600274A RID: 10058 RVA: 0x0001B224 File Offset: 0x00019424
			public static bool operator !=(FuzzySearch.Match lhs, FuzzySearch.Match rhs)
			{
				return !lhs.Equals(rhs);
			}

			// Token: 0x0400256D RID: 9581
			public static readonly FuzzySearch.Match empty;

			// Token: 0x0400256E RID: 9582
			public readonly int textIndex;

			// Token: 0x0400256F RID: 9583
			public readonly int matchLength;

			// Token: 0x04002570 RID: 9584
			public readonly int score;
		}
	}
}
