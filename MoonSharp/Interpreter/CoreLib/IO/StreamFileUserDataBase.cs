using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020008F8 RID: 2296
	internal abstract class StreamFileUserDataBase : FileUserDataBase
	{
		// Token: 0x06003A74 RID: 14964 RVA: 0x0002762B File Offset: 0x0002582B
		protected void Initialize(Stream stream, StreamReader reader, StreamWriter writer)
		{
			this.m_Stream = stream;
			this.m_Reader = reader;
			this.m_Writer = writer;
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x00027642 File Offset: 0x00025842
		private void CheckFileIsNotClosed()
		{
			if (this.m_Closed)
			{
				throw new ScriptRuntimeException("attempt to use a closed file");
			}
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x00027657 File Offset: 0x00025857
		protected override bool Eof()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader != null && this.m_Reader.EndOfStream;
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x00027674 File Offset: 0x00025874
		protected override string ReadLine()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader.ReadLine();
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x00027687 File Offset: 0x00025887
		protected override string ReadToEnd()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader.ReadToEnd();
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0012BBE0 File Offset: 0x00129DE0
		protected override string ReadBuffer(int p)
		{
			this.CheckFileIsNotClosed();
			char[] array = new char[p];
			int length = this.m_Reader.ReadBlock(array, 0, p);
			return new string(array, 0, length);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0002769A File Offset: 0x0002589A
		protected override char Peek()
		{
			this.CheckFileIsNotClosed();
			return (char)this.m_Reader.Peek();
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000276AE File Offset: 0x000258AE
		protected override void Write(string value)
		{
			this.CheckFileIsNotClosed();
			this.m_Writer.Write(value);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0012BC14 File Offset: 0x00129E14
		protected override string Close()
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.Dispose();
			}
			if (this.m_Reader != null)
			{
				this.m_Reader.Dispose();
			}
			this.m_Stream.Dispose();
			this.m_Closed = true;
			return null;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000276C2 File Offset: 0x000258C2
		public override bool flush()
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.Flush();
			}
			return true;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0012BC60 File Offset: 0x00129E60
		public override long seek(string whence, long offset)
		{
			this.CheckFileIsNotClosed();
			if (whence != null)
			{
				if (whence == "set")
				{
					this.m_Stream.Seek(offset, SeekOrigin.Begin);
				}
				else if (whence == "cur")
				{
					this.m_Stream.Seek(offset, SeekOrigin.Current);
				}
				else
				{
					if (!(whence == "end"))
					{
						throw ScriptRuntimeException.BadArgument(0, "seek", "invalid option '" + whence + "'");
					}
					this.m_Stream.Seek(offset, SeekOrigin.End);
				}
			}
			return this.m_Stream.Position;
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000276DE File Offset: 0x000258DE
		public override bool setvbuf(string mode)
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.AutoFlush = (mode == "no" || mode == "line");
			}
			return true;
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x00027715 File Offset: 0x00025915
		protected internal override bool isopen()
		{
			return !this.m_Closed;
		}

		// Token: 0x04003033 RID: 12339
		protected Stream m_Stream;

		// Token: 0x04003034 RID: 12340
		protected StreamReader m_Reader;

		// Token: 0x04003035 RID: 12341
		protected StreamWriter m_Writer;

		// Token: 0x04003036 RID: 12342
		protected bool m_Closed;
	}
}
