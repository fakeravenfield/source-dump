using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000578 RID: 1400
	[GeneratorMenu(menu = "", name = "Group", priority = 2)]
	[Serializable]
	public class Group : Generator
	{
		// Token: 0x060023C3 RID: 9155 RVA: 0x000E591C File Offset: 0x000E3B1C
		public override void OnGUI()
		{
			this.layout.cursor = default(Rect);
			this.layout.change = false;
			this.layout.Element("MapMagic_Group", this.layout.field, new RectOffset(16, 16, 16, 16), new RectOffset(0, 0, 0, 0));
			this.layout.margin = 5;
			this.layout.CheckStyles();
			float num = this.layout.boldLabelStyle.CalcSize(new GUIContent(this.name)).x * 1.1f / this.layout.zoom + 10f;
			float num2 = this.layout.labelStyle.CalcSize(new GUIContent(this.comment)).x / this.layout.zoom + 10f;
			num = Mathf.Min(num, this.guiRect.width - 5f);
			num2 = Mathf.Min(num2, this.guiRect.width - 5f);
			if (!this.locked)
			{
				this.layout.fontSize = 13;
				this.layout.Par(22, default(Layout.Val), default(Layout.Val));
				this.name = this.layout.Field<string>(this.name, null, this.layout.Inset(num, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), true, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), this.layout.boldLabelStyle, null);
				this.layout.fontSize = 11;
				this.layout.Par(18, default(Layout.Val), default(Layout.Val));
				this.comment = this.layout.Field<string>(this.comment, null, this.layout.Inset(num2, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), true, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), this.layout.labelStyle, null);
				return;
			}
			this.layout.fontSize = 13;
			this.layout.Par(22, default(Layout.Val), default(Layout.Val));
			this.layout.Label(this.name, this.layout.Inset(num, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Bold, TextAnchor.UpperLeft, false, null);
			this.layout.fontSize = 11;
			this.layout.Par(18, default(Layout.Val), default(Layout.Val));
			this.layout.Label(this.comment, this.layout.Inset(num2, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000E5CF4 File Offset: 0x000E3EF4
		public void Populate(GeneratorsAsset gens)
		{
			this.generators.Clear();
			for (int i = 0; i < gens.list.Length; i++)
			{
				Generator generator = gens.list[i];
				if (this.layout.field.Contains(generator.layout.field))
				{
					this.generators.Add(generator);
				}
			}
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000E5D54 File Offset: 0x000E3F54
		public override void Move(Vector2 delta, bool moveChildren = true)
		{
			base.Move(delta, true);
			if (moveChildren)
			{
				for (int i = 0; i < this.generators.Count; i++)
				{
					this.generators[i].Move(delta, false);
				}
			}
		}

		// Token: 0x040022F2 RID: 8946
		public string name = "Group";

		// Token: 0x040022F3 RID: 8947
		public string comment = "Drag in generators to group them";

		// Token: 0x040022F4 RID: 8948
		public bool locked;

		// Token: 0x040022F5 RID: 8949
		[NonSerialized]
		public List<Generator> generators = new List<Generator>();
	}
}
