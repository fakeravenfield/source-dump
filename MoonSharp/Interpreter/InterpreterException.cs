using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C5 RID: 1989
	[Serializable]
	public class InterpreterException : Exception
	{
		// Token: 0x06003180 RID: 12672 RVA: 0x00022286 File Offset: 0x00020486
		protected InterpreterException(Exception ex, string message) : base(message, ex)
		{
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00022290 File Offset: 0x00020490
		protected InterpreterException(Exception ex) : base(ex.Message, ex)
		{
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0002229F File Offset: 0x0002049F
		protected InterpreterException(string message) : base(message)
		{
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000222A8 File Offset: 0x000204A8
		protected InterpreterException(string format, params object[] args) : base(string.Format(format, args))
		{
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000222B7 File Offset: 0x000204B7
		// (set) Token: 0x06003185 RID: 12677 RVA: 0x000222BF File Offset: 0x000204BF
		public int InstructionPtr { get; internal set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000222C8 File Offset: 0x000204C8
		// (set) Token: 0x06003187 RID: 12679 RVA: 0x000222D0 File Offset: 0x000204D0
		public IList<WatchItem> CallStack { get; internal set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000222D9 File Offset: 0x000204D9
		// (set) Token: 0x06003189 RID: 12681 RVA: 0x000222E1 File Offset: 0x000204E1
		public string DecoratedMessage { get; internal set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000222EA File Offset: 0x000204EA
		// (set) Token: 0x0600318B RID: 12683 RVA: 0x000222F2 File Offset: 0x000204F2
		public bool DoNotDecorateMessage { get; set; }

		// Token: 0x0600318C RID: 12684 RVA: 0x0010F4E4 File Offset: 0x0010D6E4
		internal void DecorateMessage(Script script, SourceRef sref, int ip = -1)
		{
			if (string.IsNullOrEmpty(this.DecoratedMessage))
			{
				if (this.DoNotDecorateMessage)
				{
					this.DecoratedMessage = this.Message;
					return;
				}
				if (sref != null)
				{
					this.DecoratedMessage = string.Format("{0}: {1}", sref.FormatLocation(script, false), this.Message);
					return;
				}
				this.DecoratedMessage = string.Format("bytecode:{0}: {1}", ip, this.Message);
			}
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void Rethrow()
		{
		}
	}
}
