using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Decal : MonoBehaviour
{
	// Token: 0x060000A4 RID: 164 RVA: 0x00002AA8 File Offset: 0x00000CA8
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0003F76C File Offset: 0x0003D96C
	public Bounds GetBounds()
	{
		Vector3 lossyScale = base.transform.lossyScale;
		Vector3 vector = -lossyScale / 2f;
		Vector3 vector2 = lossyScale / 2f;
		Vector3[] array = new Vector3[]
		{
			new Vector3(vector.x, vector.y, vector.z),
			new Vector3(vector2.x, vector.y, vector.z),
			new Vector3(vector.x, vector2.y, vector.z),
			new Vector3(vector2.x, vector2.y, vector.z),
			new Vector3(vector.x, vector.y, vector2.z),
			new Vector3(vector2.x, vector.y, vector2.z),
			new Vector3(vector.x, vector2.y, vector2.z),
			new Vector3(vector2.x, vector2.y, vector2.z)
		};
		for (int i = 0; i < 8; i++)
		{
			array[i] = base.transform.TransformDirection(array[i]);
		}
		vector2 = (vector = array[0]);
		foreach (Vector3 rhs in array)
		{
			vector = Vector3.Min(vector, rhs);
			vector2 = Vector3.Max(vector2, rhs);
		}
		return new Bounds(base.transform.position, vector2 - vector);
	}

	// Token: 0x04000061 RID: 97
	public Material material;

	// Token: 0x04000062 RID: 98
	public Sprite sprite;

	// Token: 0x04000063 RID: 99
	public float maxAngle = 90f;

	// Token: 0x04000064 RID: 100
	public float pushDistance = 0.009f;

	// Token: 0x04000065 RID: 101
	public LayerMask affectedLayers = -1;
}
