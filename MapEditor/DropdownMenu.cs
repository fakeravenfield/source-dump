using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200069D RID: 1693
	public class DropdownMenu : MonoBehaviour, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x06002AFC RID: 11004 RVA: 0x0001D84C File Offset: 0x0001BA4C
		private void Start()
		{
			this.dropdownContainer.SetActive(false);
			this.dropdownButton = base.GetComponent<Button>();
			this.dropdownButton.onClick.AddListener(new UnityAction(this.DropdownButtonClicked));
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0001D882 File Offset: 0x0001BA82
		private void DropdownButtonClicked()
		{
			this.dropdownContainer.SetActive(true);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0001D890 File Offset: 0x0001BA90
		public void OnPointerExit(PointerEventData eventData)
		{
			this.dropdownContainer.SetActive(false);
		}

		// Token: 0x040027F0 RID: 10224
		public GameObject dropdownContainer;

		// Token: 0x040027F1 RID: 10225
		private Button dropdownButton;
	}
}
