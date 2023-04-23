using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200063F RID: 1599
	public class PathfindingColliders : MonoBehaviour
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x000FB5A8 File Offset: 0x000F97A8
		public GameObject CopyColliders(GameObject source)
		{
			this.ClearColliders();
			List<Type> list = new List<Type>();
			list.Add(typeof(Transform));
			list.AddRange(SelectableObject.COLLIDER_TYPES);
			this.container = Utils.Instantiate(source, base.transform, list.ToArray(), false);
			Utils.SetStatic(this.container, false);
			this.container.name = "Pathfinding colliders";
			this.container.transform.localPosition = Vector3.zero;
			this.container.transform.localRotation = Quaternion.identity;
			this.container.transform.localScale = Vector3.one;
			return this.container;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0001C1EA File Offset: 0x0001A3EA
		public void ClearColliders()
		{
			if (this.container)
			{
				UnityEngine.Object.Destroy(this.container);
				this.container = null;
			}
		}

		// Token: 0x040026BD RID: 9917
		private GameObject container;
	}
}
