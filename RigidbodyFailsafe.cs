using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class RigidbodyFailsafe : MonoBehaviour
{
	// Token: 0x0600098C RID: 2444 RVA: 0x0000865B File Offset: 0x0000685B
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0006B8E4 File Offset: 0x00069AE4
	private void FixedUpdate()
	{
		if (float.IsInfinity(this.rigidbody.position.x) || float.IsNaN(this.rigidbody.position.x) || float.IsInfinity(this.rigidbody.position.y) || float.IsNaN(this.rigidbody.position.y) || float.IsInfinity(this.rigidbody.position.z) || float.IsNaN(this.rigidbody.position.z))
		{
			Debug.LogWarning("Rigidbody failsafe triggered on " + base.gameObject.name);
			List<Transform> list = new List<Transform>(base.GetComponentsInChildren<Transform>());
			List<Rigidbody> list2 = new List<Rigidbody>(base.GetComponentsInChildren<Rigidbody>());
			Vector3 position = new Vector3(0f, 10f, 0f);
			list.Add(base.transform);
			list2.Add(this.rigidbody);
			List<Collider> list3 = new List<Collider>();
			foreach (Transform transform in list)
			{
				Collider component = transform.GetComponent<Collider>();
				if (component != null && component.enabled)
				{
					component.enabled = false;
					list3.Add(component);
				}
			}
			foreach (Transform transform2 in list)
			{
				transform2.position = position;
				transform2.rotation = Quaternion.identity;
			}
			foreach (Collider collider in list3)
			{
				collider.enabled = true;
			}
			foreach (Rigidbody rigidbody in list2)
			{
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

	// Token: 0x04000A76 RID: 2678
	private Rigidbody rigidbody;
}
