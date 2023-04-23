using System;
using System.Collections.Generic;

namespace Campaign.AI
{
	// Token: 0x0200040A RID: 1034
	public class ConquestMoveSequence
	{
		// Token: 0x060019F6 RID: 6646 RVA: 0x00014036 File Offset: 0x00012236
		public void EnqueueMove(ConquestMove move)
		{
			this.moves.Enqueue(move);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00014044 File Offset: 0x00012244
		public ConquestMove Dequeue()
		{
			return this.moves.Dequeue();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00014051 File Offset: 0x00012251
		public bool IsCompleted()
		{
			return this.moves.Count == 0;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000AB8A0 File Offset: 0x000A9AA0
		public override string ToString()
		{
			return "Moves remaining: " + this.moves.Count.ToString();
		}

		// Token: 0x04001B9E RID: 7070
		public Queue<ConquestMove> moves = new Queue<ConquestMove>(8);
	}
}
