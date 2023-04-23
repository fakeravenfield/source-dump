using System;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000643 RID: 1603
	public class SelectableObject : MonoBehaviour
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x0001C270 File Offset: 0x0001A470
		private void OnEnable()
		{
			if (this.container)
			{
				this.container.SetActive(true);
			}
			this.NotifyOfSelectionChange();
			MapEditor.instance.NotifyObjectCreated();
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0001C29B File Offset: 0x0001A49B
		private void OnDisable()
		{
			if (this.container)
			{
				this.container.SetActive(false);
			}
			this.DisableOutline();
			this.NotifyDestroyed();
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0001C2C2 File Offset: 0x0001A4C2
		private void OnDestroy()
		{
			this.NotifyDestroyed();
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000FB988 File Offset: 0x000F9B88
		private void NotifyDestroyed()
		{
			MapEditor instance = MapEditor.instance;
			if (instance)
			{
				instance.NotifyObjectDestroyed();
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0001C2CA File Offset: 0x0001A4CA
		public bool IsDeleted()
		{
			return !base.gameObject.activeSelf;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0001C2DA File Offset: 0x0001A4DA
		public void Delete()
		{
			if (this.IsActionDisabled(MapEditor.Action.Delete))
			{
				Debug.LogError("Delete action is disabled");
				return;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public void Undelete()
		{
			if (this.IsActionDisabled(MapEditor.Action.Delete))
			{
				Debug.LogError("Delete action is disabled");
				return;
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0001C31E File Offset: 0x0001A51E
		public void Destroy()
		{
			if (this.IsActionDisabled(MapEditor.Action.Delete))
			{
				Debug.LogError("Delete action is disabled");
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000FB9AC File Offset: 0x000F9BAC
		public GameObject GenerateColliders(GameObject source)
		{
			if (this.container)
			{
				UnityEngine.Object.Destroy(this.container);
				this.container = null;
			}
			SelectableObjectSettings componentInChildren = source.GetComponentInChildren<SelectableObjectSettings>();
			object obj = !componentInChildren || componentInChildren.addMeshColliders;
			bool flag = !componentInChildren || componentInChildren.stripColliders;
			List<Type> list = new List<Type>();
			list.Add(typeof(Transform));
			object obj2 = obj;
			if (obj2 != null)
			{
				list.Add(typeof(MeshFilter));
			}
			if (!flag)
			{
				list.AddRange(SelectableObject.COLLIDER_TYPES);
			}
			this.container = Utils.Instantiate(source, base.transform, list.ToArray(), true);
			Utils.SetStatic(this.container, false);
			this.container.name = "Selectable object";
			this.container.transform.localPosition = Vector3.zero;
			this.container.transform.localRotation = Quaternion.identity;
			this.container.transform.localScale = Vector3.one;
			Utils.MoveToLayer(this.container, Layers.GetSelectableLayer());
			if (obj2 != null)
			{
				this.AddMeshColliders(this.container);
			}
			return this.container;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000FBAD4 File Offset: 0x000F9CD4
		private void AddMeshColliders(GameObject go)
		{
			MeshFilter[] componentsInChildren = go.GetComponentsInChildren<MeshFilter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.AddComponent<MeshCollider>();
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0001C33F File Offset: 0x0001A53F
		public void Scan()
		{
			if (base.isActiveAndEnabled)
			{
				this.objectsToNotify = base.GetComponentsInChildren<ISelectedNotify>(true);
				return;
			}
			this.objectsToNotify = null;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000FBB04 File Offset: 0x000F9D04
		private void NotifyOfSelectionChange()
		{
			if (this.objectsToNotify == null)
			{
				this.Scan();
			}
			if (this.objectsToNotify != null)
			{
				ISelectedNotify[] array;
				if (this.IsOutlineEnabled())
				{
					array = this.objectsToNotify;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].OnSelected();
					}
					return;
				}
				array = this.objectsToNotify;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnDeselected();
				}
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000FBB6C File Offset: 0x000F9D6C
		public bool IsOutlineEnabled()
		{
			Outline component = base.GetComponent<Outline>();
			return component && component.enabled;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0001C35E File Offset: 0x0001A55E
		public void EnableOutline()
		{
			this.GetOrCreateComponent<Outline>().enabled = true;
			this.NotifyOfSelectionChange();
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000FBB90 File Offset: 0x000F9D90
		public void DisableOutline()
		{
			Outline component = base.GetComponent<Outline>();
			if (component)
			{
				component.enabled = false;
			}
			this.NotifyOfSelectionChange();
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0001C372 File Offset: 0x0001A572
		public virtual bool IsActionDisabled(MapEditor.Action action)
		{
			return (this.disabledActions & action) > (MapEditor.Action)0;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x0001C37F File Offset: 0x0001A57F
		public void DisableAction(MapEditor.Action action)
		{
			this.disabledActions |= action;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000FBBBC File Offset: 0x000F9DBC
		public static SelectableObject RayCast(Ray ray)
		{
			SelectableObject selectableObject = null;
			int layerMask = Layers.GetSelectableLayerMask() | Layers.GetTerrainLayerMask();
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 1000f, layerMask))
			{
				selectableObject = raycastHit.transform.GetComponentInChildren<SelectableObject>();
				if (!selectableObject)
				{
					selectableObject = raycastHit.transform.GetComponentInParent<SelectableObject>();
				}
			}
			return selectableObject;
		}

		// Token: 0x040026CA RID: 9930
		public static readonly Type[] COLLIDER_TYPES = new Type[]
		{
			typeof(BoxCollider),
			typeof(MeshCollider),
			typeof(CapsuleCollider),
			typeof(SphereCollider),
			typeof(WheelCollider)
		};

		// Token: 0x040026CB RID: 9931
		private const float RAYCAST_MAX_DISTANCE = 1000f;

		// Token: 0x040026CC RID: 9932
		private MapEditor.Action disabledActions;

		// Token: 0x040026CD RID: 9933
		private GameObject container;

		// Token: 0x040026CE RID: 9934
		private ISelectedNotify[] objectsToNotify;
	}
}
