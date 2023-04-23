using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200014B RID: 331
[RequireComponent(typeof(Renderer))]
public class HideOnFire : MonoBehaviour
{
	// Token: 0x0600090B RID: 2315 RVA: 0x00069DBC File Offset: 0x00067FBC
	private void Start()
	{
		this.renderer = base.GetComponent<Renderer>();
		if (this.weapon == null || this.renderer == null)
		{
			base.enabled = false;
			return;
		}
		this.weapon.onFire.AddListener(new UnityAction(this.OnFire));
		this.hideAction = new TimedAction(this.hideTime, false);
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00007F59 File Offset: 0x00006159
	private void OnFire()
	{
		this.hideAction.Start();
		this.renderer.enabled = false;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00007F72 File Offset: 0x00006172
	private void Update()
	{
		if (!this.renderer.enabled && this.hideAction.TrueDone())
		{
			this.renderer.enabled = true;
		}
	}

	// Token: 0x040009D6 RID: 2518
	public float hideTime = 1f;

	// Token: 0x040009D7 RID: 2519
	public Weapon weapon;

	// Token: 0x040009D8 RID: 2520
	private Renderer renderer;

	// Token: 0x040009D9 RID: 2521
	private TimedAction hideAction;
}
