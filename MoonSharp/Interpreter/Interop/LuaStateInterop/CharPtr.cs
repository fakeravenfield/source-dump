using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000883 RID: 2179
	public class CharPtr
	{
		// Token: 0x170004A0 RID: 1184
		public char this[int offset]
		{
			get
			{
				return this.chars[this.index + offset];
			}
			set
			{
				this.chars[this.index + offset] = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		public char this[uint offset]
		{
			get
			{
				return this.chars[(int)(checked((IntPtr)(unchecked((long)this.index + (long)((ulong)offset)))))];
			}
			set
			{
				this.chars[(int)(checked((IntPtr)(unchecked((long)this.index + (long)((ulong)offset)))))] = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		public char this[long offset]
		{
			get
			{
				return this.chars[this.index + (int)offset];
			}
			set
			{
				this.chars[this.index + (int)offset] = value;
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x00024A80 File Offset: 0x00022C80
		public static implicit operator CharPtr(string str)
		{
			return new CharPtr(str);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x00024A88 File Offset: 0x00022C88
		public static implicit operator CharPtr(char[] chars)
		{
			return new CharPtr(chars);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x00024A90 File Offset: 0x00022C90
		public static implicit operator CharPtr(byte[] bytes)
		{
			return new CharPtr(bytes);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x00024A98 File Offset: 0x00022C98
		public CharPtr()
		{
			this.chars = null;
			this.index = 0;
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x00024AAE File Offset: 0x00022CAE
		public CharPtr(string str)
		{
			this.chars = (str + "\0").ToCharArray();
			this.index = 0;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x00024AD3 File Offset: 0x00022CD3
		public CharPtr(CharPtr ptr)
		{
			this.chars = ptr.chars;
			this.index = ptr.index;
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x00024AF3 File Offset: 0x00022CF3
		public CharPtr(CharPtr ptr, int index)
		{
			this.chars = ptr.chars;
			this.index = index;
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x00024B0E File Offset: 0x00022D0E
		public CharPtr(char[] chars)
		{
			this.chars = chars;
			this.index = 0;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x00024B24 File Offset: 0x00022D24
		public CharPtr(char[] chars, int index)
		{
			this.chars = chars;
			this.index = index;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0011CE78 File Offset: 0x0011B078
		public CharPtr(byte[] bytes)
		{
			this.chars = new char[bytes.Length];
			for (int i = 0; i < bytes.Length; i++)
			{
				this.chars[i] = (char)bytes[i];
			}
			this.index = 0;
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x00024B3A File Offset: 0x00022D3A
		public CharPtr(IntPtr ptr)
		{
			this.chars = new char[0];
			this.index = 0;
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x00024B55 File Offset: 0x00022D55
		public static CharPtr operator +(CharPtr ptr, int offset)
		{
			return new CharPtr(ptr.chars, ptr.index + offset);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x00024B6A File Offset: 0x00022D6A
		public static CharPtr operator -(CharPtr ptr, int offset)
		{
			return new CharPtr(ptr.chars, ptr.index - offset);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x00024B55 File Offset: 0x00022D55
		public static CharPtr operator +(CharPtr ptr, uint offset)
		{
			return new CharPtr(ptr.chars, ptr.index + (int)offset);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x00024B6A File Offset: 0x00022D6A
		public static CharPtr operator -(CharPtr ptr, uint offset)
		{
			return new CharPtr(ptr.chars, ptr.index - (int)offset);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x00024B7F File Offset: 0x00022D7F
		public void inc()
		{
			this.index++;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x00024B8F File Offset: 0x00022D8F
		public void dec()
		{
			this.index--;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x00024B9F File Offset: 0x00022D9F
		public CharPtr next()
		{
			return new CharPtr(this.chars, this.index + 1);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x00024BB4 File Offset: 0x00022DB4
		public CharPtr prev()
		{
			return new CharPtr(this.chars, this.index - 1);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x00024B55 File Offset: 0x00022D55
		public CharPtr add(int ofs)
		{
			return new CharPtr(this.chars, this.index + ofs);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x00024B6A File Offset: 0x00022D6A
		public CharPtr sub(int ofs)
		{
			return new CharPtr(this.chars, this.index - ofs);
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x00024BC9 File Offset: 0x00022DC9
		public static bool operator ==(CharPtr ptr, char ch)
		{
			return ptr[0] == ch;
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x00024BD5 File Offset: 0x00022DD5
		public static bool operator ==(char ch, CharPtr ptr)
		{
			return ptr[0] == ch;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x00024BE1 File Offset: 0x00022DE1
		public static bool operator !=(CharPtr ptr, char ch)
		{
			return ptr[0] != ch;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x00024BF0 File Offset: 0x00022DF0
		public static bool operator !=(char ch, CharPtr ptr)
		{
			return ptr[0] != ch;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x0011CEBC File Offset: 0x0011B0BC
		public static CharPtr operator +(CharPtr ptr1, CharPtr ptr2)
		{
			string text = "";
			int num = 0;
			while (ptr1[num] != '\0')
			{
				text += ptr1[num].ToString();
				num++;
			}
			int num2 = 0;
			while (ptr2[num2] != '\0')
			{
				text += ptr2[num2].ToString();
				num2++;
			}
			return new CharPtr(text);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x00024BFF File Offset: 0x00022DFF
		public static int operator -(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index - ptr2.index;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x00024C0E File Offset: 0x00022E0E
		public static bool operator <(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index < ptr2.index;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x00024C1E File Offset: 0x00022E1E
		public static bool operator <=(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index <= ptr2.index;
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x00024C31 File Offset: 0x00022E31
		public static bool operator >(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index > ptr2.index;
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x00024C41 File Offset: 0x00022E41
		public static bool operator >=(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index >= ptr2.index;
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x0011CF24 File Offset: 0x0011B124
		public static bool operator ==(CharPtr ptr1, CharPtr ptr2)
		{
			return (ptr1 == null && ptr2 == null) || (ptr1 != null && ptr2 != null && ptr1.chars == ptr2.chars && ptr1.index == ptr2.index);
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x00024C54 File Offset: 0x00022E54
		public static bool operator !=(CharPtr ptr1, CharPtr ptr2)
		{
			return !(ptr1 == ptr2);
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x00024C60 File Offset: 0x00022E60
		public override bool Equals(object o)
		{
			return this == o as CharPtr;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x0000257D File Offset: 0x0000077D
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x0011CF68 File Offset: 0x0011B168
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.index;
			while (num < this.chars.Length && this.chars[num] != '\0')
			{
				stringBuilder.Append(this.chars[num]);
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x0011CFB4 File Offset: 0x0011B1B4
		public string ToString(int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.index;
			while (num < this.chars.Length && num < length + this.index)
			{
				stringBuilder.Append(this.chars[num]);
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002E80 RID: 11904
		public char[] chars;

		// Token: 0x04002E81 RID: 11905
		public int index;
	}
}
