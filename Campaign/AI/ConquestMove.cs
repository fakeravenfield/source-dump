using System;
using System.Collections.Generic;

namespace Campaign.AI
{
	// Token: 0x02000408 RID: 1032
	public class ConquestMove
	{
		// Token: 0x060019F1 RID: 6641 RVA: 0x00013FFE File Offset: 0x000121FE
		public ConquestMove(LevelClickable destination)
		{
			this.destination = destination;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000AB780 File Offset: 0x000A9980
		public void AddSource(LevelClickable level, int count)
		{
			foreach (ConquestMove.Source source in this.sources)
			{
				if (source.level == level)
				{
					source.count += count;
					return;
				}
			}
			this.sources.Add(new ConquestMove.Source(level, count));
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00014018 File Offset: 0x00012218
		public List<ConquestMove.Source> GetSources()
		{
			return this.sources;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000AB7FC File Offset: 0x000A99FC
		public override string ToString()
		{
			string text = "From ";
			foreach (ConquestMove.Source source in this.sources)
			{
				text = string.Concat(new string[]
				{
					text,
					source.level.displayName,
					"(",
					source.count.ToString(),
					"), "
				});
			}
			return text + "to " + this.destination.displayName;
		}

		// Token: 0x04001B9A RID: 7066
		public LevelClickable destination;

		// Token: 0x04001B9B RID: 7067
		private List<ConquestMove.Source> sources = new List<ConquestMove.Source>();

		// Token: 0x02000409 RID: 1033
		public class Source
		{
			// Token: 0x060019F5 RID: 6645 RVA: 0x00014020 File Offset: 0x00012220
			public Source(LevelClickable level, int count)
			{
				this.level = level;
				this.count = count;
			}

			// Token: 0x04001B9C RID: 7068
			public LevelClickable level;

			// Token: 0x04001B9D RID: 7069
			public int count;
		}
	}
}
