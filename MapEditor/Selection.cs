using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000660 RID: 1632
	public struct Selection : IEquatable<Selection>
	{
		// Token: 0x06002967 RID: 10599 RVA: 0x0001C6BE File Offset: 0x0001A8BE
		public Selection(params SelectableObject[] objects)
		{
			if (objects != null && objects.Length != 0)
			{
				this.objects = objects;
				return;
			}
			this.objects = null;
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0001C6D6 File Offset: 0x0001A8D6
		public bool Any()
		{
			return this.objects != null && this.objects.Any<SelectableObject>();
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0001C6ED File Offset: 0x0001A8ED
		public bool Empty()
		{
			return !this.Any();
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		public int GetLength()
		{
			if (!this.Any())
			{
				return 0;
			}
			return this.objects.Length;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000FCACC File Offset: 0x000FACCC
		public Selection AddRange(IEnumerable<SelectableObject> enumerable)
		{
			if (enumerable == null)
			{
				return this;
			}
			SelectableObject[] array = enumerable.ToArray<SelectableObject>();
			if (array.Length == 0)
			{
				return this;
			}
			if (this.Any())
			{
				return new Selection(this.objects.Union(array).Distinct<SelectableObject>().ToArray<SelectableObject>());
			}
			return new Selection(array);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0001C70C File Offset: 0x0001A90C
		public Selection Add(SelectableObject obj)
		{
			return this.AddRange(new SelectableObject[]
			{
				obj
			});
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000FCB20 File Offset: 0x000FAD20
		public Selection Remove(SelectableObject obj)
		{
			if (this.Any() && this.objects.Contains(obj))
			{
				return new Selection((from x in this.objects
				where x != obj
				select x).ToArray<SelectableObject>());
			}
			return this;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0001C71E File Offset: 0x0001A91E
		public SelectableObject[] GetObjects()
		{
			if (this.Any())
			{
				return this.objects;
			}
			return new SelectableObject[0];
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000FCB80 File Offset: 0x000FAD80
		public Vector3 GetPivot()
		{
			Vector3 vector = Vector3.zero;
			if (this.Any())
			{
				foreach (SelectableObject selectableObject in this.objects)
				{
					vector += selectableObject.transform.position;
				}
				vector /= (float)this.objects.Length;
			}
			return vector;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000FCBD8 File Offset: 0x000FADD8
		public Selection RemoveDestroyed(out bool changed)
		{
			changed = false;
			if (this.Any())
			{
				IEnumerable<SelectableObject> enumerable = from obj in this.objects
				where !obj || !obj.gameObject || obj.IsDeleted()
				select obj;
				if (enumerable.Any<SelectableObject>())
				{
					changed = true;
					return new Selection(this.objects.Except(enumerable).ToArray<SelectableObject>());
				}
			}
			return this;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000FCC44 File Offset: 0x000FAE44
		public Selection RemoveDestroyed()
		{
			bool flag;
			return this.RemoveDestroyed(out flag);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000FCC5C File Offset: 0x000FAE5C
		public bool IsActionDisabled(MapEditor.Action action)
		{
			return this.Any() && this.objects.Any((SelectableObject obj) => obj.IsActionDisabled(action));
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0001C735 File Offset: 0x0001A935
		public override bool Equals(object obj)
		{
			return obj is Selection && this.Equals((Selection)obj);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000FCC98 File Offset: 0x000FAE98
		public bool Equals(Selection other)
		{
			bool flag = this.objects == null;
			bool flag2 = other.objects == null;
			if (flag || flag2)
			{
				return flag && flag2;
			}
			return this.objects.SequenceEqual(other.objects);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0001C74D File Offset: 0x0001A94D
		public override int GetHashCode()
		{
			return -1962559453 + EqualityComparer<SelectableObject[]>.Default.GetHashCode(this.objects);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0001C765 File Offset: 0x0001A965
		public static bool operator ==(Selection lhs, Selection rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0001C76F File Offset: 0x0001A96F
		public static bool operator !=(Selection lhs, Selection rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x040026FD RID: 9981
		public static readonly Selection empty;

		// Token: 0x040026FE RID: 9982
		private readonly SelectableObject[] objects;
	}
}
