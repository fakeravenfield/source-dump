using System;
using System.IO;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x0200089E RID: 2206
	public class UndisposableStream : Stream
	{
		// Token: 0x06003743 RID: 14147 RVA: 0x000254AE File Offset: 0x000236AE
		public UndisposableStream(Stream stream)
		{
			this.m_Stream = stream;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x0000296E File Offset: 0x00000B6E
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void Close()
		{
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000254BD File Offset: 0x000236BD
		public override bool CanRead
		{
			get
			{
				return this.m_Stream.CanRead;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000254CA File Offset: 0x000236CA
		public override bool CanSeek
		{
			get
			{
				return this.m_Stream.CanSeek;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000254D7 File Offset: 0x000236D7
		public override bool CanWrite
		{
			get
			{
				return this.m_Stream.CanWrite;
			}
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000254E4 File Offset: 0x000236E4
		public override void Flush()
		{
			this.m_Stream.Flush();
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000254F1 File Offset: 0x000236F1
		public override long Length
		{
			get
			{
				return this.m_Stream.Length;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000254FE File Offset: 0x000236FE
		// (set) Token: 0x0600374C RID: 14156 RVA: 0x0002550B File Offset: 0x0002370B
		public override long Position
		{
			get
			{
				return this.m_Stream.Position;
			}
			set
			{
				this.m_Stream.Position = value;
			}
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x00025519 File Offset: 0x00023719
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_Stream.Read(buffer, offset, count);
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x00025529 File Offset: 0x00023729
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_Stream.Seek(offset, origin);
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x00025538 File Offset: 0x00023738
		public override void SetLength(long value)
		{
			this.m_Stream.SetLength(value);
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x00025546 File Offset: 0x00023746
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_Stream.Write(buffer, offset, count);
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x00025556 File Offset: 0x00023756
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_Stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x0002556A File Offset: 0x0002376A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_Stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0002557E File Offset: 0x0002377E
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_Stream.EndWrite(asyncResult);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x0002558C File Offset: 0x0002378C
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_Stream.EndRead(asyncResult);
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x0002559A File Offset: 0x0002379A
		public override bool CanTimeout
		{
			get
			{
				return this.m_Stream.CanTimeout;
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000255A7 File Offset: 0x000237A7
		public override bool Equals(object obj)
		{
			return this.m_Stream.Equals(obj);
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000255B5 File Offset: 0x000237B5
		public override int GetHashCode()
		{
			return this.m_Stream.GetHashCode();
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000255C2 File Offset: 0x000237C2
		public override int ReadByte()
		{
			return this.m_Stream.ReadByte();
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000255CF File Offset: 0x000237CF
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000255DC File Offset: 0x000237DC
		public override int ReadTimeout
		{
			get
			{
				return this.m_Stream.ReadTimeout;
			}
			set
			{
				this.m_Stream.ReadTimeout = value;
			}
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000255EA File Offset: 0x000237EA
		public override string ToString()
		{
			return this.m_Stream.ToString();
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000255F7 File Offset: 0x000237F7
		public override void WriteByte(byte value)
		{
			this.m_Stream.WriteByte(value);
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x00025605 File Offset: 0x00023805
		// (set) Token: 0x0600375E RID: 14174 RVA: 0x00025612 File Offset: 0x00023812
		public override int WriteTimeout
		{
			get
			{
				return this.m_Stream.WriteTimeout;
			}
			set
			{
				this.m_Stream.WriteTimeout = value;
			}
		}

		// Token: 0x04002EDD RID: 11997
		private Stream m_Stream;
	}
}
