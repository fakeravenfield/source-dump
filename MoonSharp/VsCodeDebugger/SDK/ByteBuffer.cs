using System;
using System.Text;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x0200079A RID: 1946
	internal class ByteBuffer
	{
		// Token: 0x06002FC9 RID: 12233 RVA: 0x00020E27 File Offset: 0x0001F027
		public ByteBuffer()
		{
			this._buffer = new byte[0];
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x00020E3B File Offset: 0x0001F03B
		public int Length
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00020E45 File Offset: 0x0001F045
		public string GetString(Encoding enc)
		{
			return enc.GetString(this._buffer);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0010BFEC File Offset: 0x0010A1EC
		public void Append(byte[] b, int length)
		{
			byte[] array = new byte[this._buffer.Length + length];
			Buffer.BlockCopy(this._buffer, 0, array, 0, this._buffer.Length);
			Buffer.BlockCopy(b, 0, array, this._buffer.Length, length);
			this._buffer = array;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x0010C038 File Offset: 0x0010A238
		public byte[] RemoveFirst(int n)
		{
			byte[] array = new byte[n];
			Buffer.BlockCopy(this._buffer, 0, array, 0, n);
			byte[] array2 = new byte[this._buffer.Length - n];
			Buffer.BlockCopy(this._buffer, n, array2, 0, this._buffer.Length - n);
			this._buffer = array2;
			return array;
		}

		// Token: 0x04002B8F RID: 11151
		private byte[] _buffer;
	}
}
