using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007C7 RID: 1991
	[Serializable]
	public class SyntaxErrorException : InterpreterException
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000225EA File Offset: 0x000207EA
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x000225F2 File Offset: 0x000207F2
		internal Token Token { get; private set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x000225FB File Offset: 0x000207FB
		// (set) Token: 0x060031B4 RID: 12724 RVA: 0x00022603 File Offset: 0x00020803
		public bool IsPrematureStreamTermination { get; set; }

		// Token: 0x060031B5 RID: 12725 RVA: 0x0002260C File Offset: 0x0002080C
		internal SyntaxErrorException(Token t, string format, params object[] args) : base(format, args)
		{
			this.Token = t;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x0002261D File Offset: 0x0002081D
		internal SyntaxErrorException(Token t, string message) : base(message)
		{
			this.Token = t;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0002262D File Offset: 0x0002082D
		internal SyntaxErrorException(Script script, SourceRef sref, string format, params object[] args) : base(format, args)
		{
			base.DecorateMessage(script, sref, -1);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x00022641 File Offset: 0x00020841
		internal SyntaxErrorException(Script script, SourceRef sref, string message) : base(message)
		{
			base.DecorateMessage(script, sref, -1);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00022653 File Offset: 0x00020853
		private SyntaxErrorException(SyntaxErrorException syntaxErrorException) : base(syntaxErrorException, syntaxErrorException.DecoratedMessage)
		{
			this.Token = syntaxErrorException.Token;
			base.DecoratedMessage = this.Message;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0002267A File Offset: 0x0002087A
		internal void DecorateMessage(Script script)
		{
			if (this.Token != null)
			{
				base.DecorateMessage(script, this.Token.GetSourceRef(false), -1);
			}
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00022698 File Offset: 0x00020898
		public override void Rethrow()
		{
			if (Script.GlobalOptions.RethrowExceptionNested)
			{
				throw new SyntaxErrorException(this);
			}
		}
	}
}
