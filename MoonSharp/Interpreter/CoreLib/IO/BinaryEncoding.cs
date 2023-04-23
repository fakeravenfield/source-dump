using System;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020008F3 RID: 2291
	internal class BinaryEncoding : Encoding
	{
		// Token: 0x06003A53 RID: 14931 RVA: 0x000275BE File Offset: 0x000257BE
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0012B728 File Offset: 0x00129928
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				bytes[byteIndex + i] = (byte)chars[charIndex + i];
			}
			return charCount;
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000275BE File Offset: 0x000257BE
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x0012B750 File Offset: 0x00129950
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				chars[charIndex + i] = (char)bytes[byteIndex + i];
			}
			return byteCount;
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000091D3 File Offset: 0x000073D3
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000091D3 File Offset: 0x000073D3
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}
	}
}
