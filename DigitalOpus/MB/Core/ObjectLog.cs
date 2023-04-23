using System;
using System.Text;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000442 RID: 1090
	public class ObjectLog
	{
		// Token: 0x06001AD1 RID: 6865 RVA: 0x000146F9 File Offset: 0x000128F9
		private void _CacheLogMessage(string msg)
		{
			if (this.logMessages.Length == 0)
			{
				return;
			}
			this.logMessages[this.pos] = msg;
			this.pos++;
			if (this.pos >= this.logMessages.Length)
			{
				this.pos = 0;
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00014738 File Offset: 0x00012938
		public ObjectLog(short bufferSize)
		{
			this.logMessages = new string[(int)bufferSize];
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0001474C File Offset: 0x0001294C
		public void Log(MB2_LogLevel l, string msg, MB2_LogLevel currentThreshold)
		{
			MB2_Log.Log(l, msg, currentThreshold);
			this._CacheLogMessage(msg);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0001475D File Offset: 0x0001295D
		public void Error(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Error(msg, args));
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0001476C File Offset: 0x0001296C
		public void Warn(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Warn(msg, args));
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0001477B File Offset: 0x0001297B
		public void Info(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Info(msg, args));
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0001478A File Offset: 0x0001298A
		public void LogDebug(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.LogDebug(msg, args));
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00014799 File Offset: 0x00012999
		public void Trace(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Trace(msg, args));
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000B053C File Offset: 0x000AE73C
		public string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			if (this.logMessages[this.logMessages.Length - 1] != null)
			{
				num = this.pos;
			}
			for (int i = 0; i < this.logMessages.Length; i++)
			{
				int num2 = (num + i) % this.logMessages.Length;
				if (this.logMessages[num2] == null)
				{
					break;
				}
				stringBuilder.AppendLine(this.logMessages[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001C97 RID: 7319
		private int pos;

		// Token: 0x04001C98 RID: 7320
		private string[] logMessages;
	}
}
