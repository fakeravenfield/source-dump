using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class UvAutoScroll : UvOffset
{
	// Token: 0x0600097A RID: 2426 RVA: 0x0000858A File Offset: 0x0000678A
	private void Update()
	{
		base.IncrementOffset(this.speed * Time.deltaTime);
	}

	// Token: 0x04000A61 RID: 2657
	public Vector2 speed;
}
