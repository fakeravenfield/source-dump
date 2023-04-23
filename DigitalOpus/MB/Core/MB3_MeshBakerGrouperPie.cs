using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000425 RID: 1061
	[Serializable]
	public class MB3_MeshBakerGrouperPie : MB3_MeshBakerGrouperCore
	{
		// Token: 0x06001A68 RID: 6760 RVA: 0x0001445A File Offset: 0x0001265A
		public MB3_MeshBakerGrouperPie(GrouperData data)
		{
			this.d = data;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000AD64C File Offset: 0x000AB84C
		public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
		{
			Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
			if (this.d.pieNumSegments == 0)
			{
				Debug.LogError("pieNumSegments must be greater than zero.");
				return dictionary;
			}
			if (this.d.pieAxis.magnitude <= 1E-06f)
			{
				Debug.LogError("Pie axis must have length greater than zero.");
				return dictionary;
			}
			this.d.pieAxis.Normalize();
			Quaternion rotation = Quaternion.FromToRotation(this.d.pieAxis, Vector3.up);
			Debug.Log("Collecting renderers in each cell");
			foreach (GameObject gameObject in selection)
			{
				if (!(gameObject == null))
				{
					Renderer component = gameObject.GetComponent<Renderer>();
					if (component is MeshRenderer || component is SkinnedMeshRenderer)
					{
						Vector3 vector = component.bounds.center - this.d.origin;
						vector.Normalize();
						vector = rotation * vector;
						float num;
						if (Mathf.Abs(vector.x) < 0.0001f && Mathf.Abs(vector.z) < 0.0001f)
						{
							num = 0f;
						}
						else
						{
							num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
							if (num < 0f)
							{
								num = 360f + num;
							}
						}
						string key = "seg_" + Mathf.FloorToInt(num / 360f * (float)this.d.pieNumSegments).ToString();
						List<Renderer> list;
						if (dictionary.ContainsKey(key))
						{
							list = dictionary[key];
						}
						else
						{
							list = new List<Renderer>();
							dictionary.Add(key, list);
						}
						if (!list.Contains(component))
						{
							list.Add(component);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000AD848 File Offset: 0x000ABA48
		public override void DrawGizmos(Bounds sourceObjectBounds)
		{
			if (this.d.pieAxis.magnitude < 0.1f)
			{
				return;
			}
			if (this.d.pieNumSegments < 1)
			{
				return;
			}
			float magnitude = sourceObjectBounds.extents.magnitude;
			MB3_MeshBakerGrouperPie.DrawCircle(this.d.pieAxis, this.d.origin, magnitude, 24);
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, this.d.pieAxis);
			Quaternion rotation2 = Quaternion.AngleAxis(180f / (float)this.d.pieNumSegments, Vector3.up);
			Vector3 point = Vector3.forward;
			for (int i = 0; i < this.d.pieNumSegments; i++)
			{
				Vector3 a = rotation * point;
				Gizmos.DrawLine(this.d.origin, this.d.origin + a * magnitude);
				point = rotation2 * point;
				point = rotation2 * point;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000AD940 File Offset: 0x000ABB40
		public static void DrawCircle(Vector3 axis, Vector3 center, float radius, int subdiv)
		{
			Quaternion rotation = Quaternion.AngleAxis((float)(360 / subdiv), axis);
			Vector3 vector = new Vector3(axis.y, -axis.x, axis.z);
			vector.Normalize();
			vector *= radius;
			for (int i = 0; i < subdiv + 1; i++)
			{
				Vector3 vector2 = rotation * vector;
				Gizmos.DrawLine(center + vector, center + vector2);
				vector = vector2;
			}
		}
	}
}
