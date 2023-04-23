using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000799 RID: 1945
	public abstract class ProtocolServer
	{
		// Token: 0x06002FBF RID: 12223 RVA: 0x00020DD9 File Offset: 0x0001EFD9
		public ProtocolServer()
		{
			this._sequenceNumber = 1;
			this._bodyLength = -1;
			this._rawData = new ByteBuffer();
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0010BCD0 File Offset: 0x00109ED0
		public void ProcessLoop(Stream inputStream, Stream outputStream)
		{
			this._outputStream = outputStream;
			byte[] array = new byte[4096];
			this._stopRequested = false;
			while (!this._stopRequested)
			{
				int num = inputStream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				if (num > 0)
				{
					this._rawData.Append(array, num);
					this.ProcessData();
				}
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00020DFA File Offset: 0x0001EFFA
		public void Stop()
		{
			this._stopRequested = true;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00020E03 File Offset: 0x0001F003
		public void SendEvent(Event e)
		{
			this.SendMessage(e);
		}

		// Token: 0x06002FC3 RID: 12227
		protected abstract void DispatchRequest(string command, Table args, Response response);

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0010BD28 File Offset: 0x00109F28
		private void ProcessData()
		{
			for (;;)
			{
				if (this._bodyLength >= 0)
				{
					if (this._rawData.Length < this._bodyLength)
					{
						break;
					}
					byte[] bytes = this._rawData.RemoveFirst(this._bodyLength);
					this._bodyLength = -1;
					this.Dispatch(ProtocolServer.Encoding.GetString(bytes));
				}
				else
				{
					string @string = this._rawData.GetString(ProtocolServer.Encoding);
					int num = @string.IndexOf("\r\n\r\n");
					if (num == -1)
					{
						break;
					}
					Match match = ProtocolServer.CONTENT_LENGTH_MATCHER.Match(@string);
					if (!match.Success || match.Groups.Count != 2)
					{
						break;
					}
					this._bodyLength = Convert.ToInt32(match.Groups[1].ToString());
					this._rawData.RemoveFirst(num + "\r\n\r\n".Length);
				}
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0010BDFC File Offset: 0x00109FFC
		private void Dispatch(string req)
		{
			try
			{
				Table table = JsonTableConverter.JsonToTable(req, null);
				if (table != null && table["type"].ToString() == "request")
				{
					if (this.TRACE)
					{
						Console.Error.WriteLine(string.Format("C {0}: {1}", table["command"], req));
					}
					Response response = new Response(table);
					this.DispatchRequest(table.Get("command").String, table.Get("arguments").Table, response);
					this.SendMessage(response);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0010BEA4 File Offset: 0x0010A0A4
		protected void SendMessage(ProtocolMessage message)
		{
			int sequenceNumber = this._sequenceNumber;
			this._sequenceNumber = sequenceNumber + 1;
			message.seq = sequenceNumber;
			if (this.TRACE_RESPONSE && message.type == "response")
			{
				Console.Error.WriteLine(string.Format(" R: {0}", JsonTableConverter.ObjectToJson(message)));
			}
			if (this.TRACE && message.type == "event")
			{
				Event @event = (Event)message;
				Console.Error.WriteLine(string.Format("E {0}: {1}", @event.@event, JsonTableConverter.ObjectToJson(@event.body)));
			}
			byte[] array = ProtocolServer.ConvertToBytes(message);
			try
			{
				this._outputStream.Write(array, 0, array.Length);
				this._outputStream.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0010BF7C File Offset: 0x0010A17C
		private static byte[] ConvertToBytes(ProtocolMessage request)
		{
			string s = JsonTableConverter.ObjectToJson(request);
			byte[] bytes = ProtocolServer.Encoding.GetBytes(s);
			string s2 = string.Format("Content-Length: {0}{1}", bytes.Length, "\r\n\r\n");
			byte[] bytes2 = ProtocolServer.Encoding.GetBytes(s2);
			byte[] array = new byte[bytes2.Length + bytes.Length];
			Buffer.BlockCopy(bytes2, 0, array, 0, bytes2.Length);
			Buffer.BlockCopy(bytes, 0, array, bytes2.Length, bytes.Length);
			return array;
		}

		// Token: 0x04002B84 RID: 11140
		public bool TRACE;

		// Token: 0x04002B85 RID: 11141
		public bool TRACE_RESPONSE;

		// Token: 0x04002B86 RID: 11142
		protected const int BUFFER_SIZE = 4096;

		// Token: 0x04002B87 RID: 11143
		protected const string TWO_CRLF = "\r\n\r\n";

		// Token: 0x04002B88 RID: 11144
		protected static readonly Regex CONTENT_LENGTH_MATCHER = new Regex("Content-Length: (\\d+)");

		// Token: 0x04002B89 RID: 11145
		protected static readonly Encoding Encoding = Encoding.UTF8;

		// Token: 0x04002B8A RID: 11146
		private int _sequenceNumber;

		// Token: 0x04002B8B RID: 11147
		private Stream _outputStream;

		// Token: 0x04002B8C RID: 11148
		private ByteBuffer _rawData;

		// Token: 0x04002B8D RID: 11149
		private int _bodyLength;

		// Token: 0x04002B8E RID: 11150
		private bool _stopRequested;
	}
}
