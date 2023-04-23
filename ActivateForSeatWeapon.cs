using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class ActivateForSeatWeapon : MonoBehaviour
{
	// Token: 0x06001502 RID: 5378 RVA: 0x0009987C File Offset: 0x00097A7C
	private void LateUpdate()
	{
		if (this.seat.IsOccupied())
		{
			int num = Mathf.Min(this.seat.weapons.Length, this.objects.Length);
			for (int i = 0; i < num; i++)
			{
				if (this.objects[i] != null)
				{
					this.objects[i].SetActive(this.seat.weapons[i] == this.seat.activeWeapon);
				}
			}
		}
	}

	// Token: 0x040016E4 RID: 5860
	public Seat seat;

	// Token: 0x040016E5 RID: 5861
	public GameObject[] objects;
}
