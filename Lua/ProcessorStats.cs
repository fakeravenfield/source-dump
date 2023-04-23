using System;

namespace Lua
{
	// Token: 0x02000923 RID: 2339
	public struct ProcessorStats
	{
		// Token: 0x06003B55 RID: 15189 RVA: 0x00028075 File Offset: 0x00026275
		public void Log(int currentValueStackSize, int currentExecutionStackSize)
		{
			this.valueStack.Push(currentValueStackSize);
			this.executionStack.Push(currentExecutionStackSize);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x0002808F File Offset: 0x0002628F
		public void PushClear()
		{
			this.valueStackClears += 1U;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x0002809F File Offset: 0x0002629F
		public void ResetLog()
		{
			this.valueStack.Reset();
			this.executionStack.Reset();
			this.valueStackClears = 0U;
		}

		// Token: 0x04003075 RID: 12405
		public ProcessorStats.StackSize valueStack;

		// Token: 0x04003076 RID: 12406
		public ProcessorStats.StackSize executionStack;

		// Token: 0x04003077 RID: 12407
		public uint valueStackClears;

		// Token: 0x02000924 RID: 2340
		public struct StackSize
		{
			// Token: 0x06003B58 RID: 15192 RVA: 0x000280BE File Offset: 0x000262BE
			public void Reset()
			{
				this.maxSize = 0;
				this.current = 0;
			}

			// Token: 0x06003B59 RID: 15193 RVA: 0x000280CE File Offset: 0x000262CE
			public void Push(int currentSize)
			{
				this.current = currentSize;
				if (currentSize > this.maxSize)
				{
					this.maxSize = currentSize;
				}
			}

			// Token: 0x04003078 RID: 12408
			public int maxSize;

			// Token: 0x04003079 RID: 12409
			public int current;
		}
	}
}
