using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000573 RID: 1395
	[GeneratorMenu(menu = "", name = "Portal", disengageable = true, priority = 1)]
	[Serializable]
	public class Portal : Generator
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x00018E8C File Offset: 0x0001708C
		public override IEnumerable<Generator.Input> Inputs()
		{
			yield return this.input;
			yield break;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00018E9C File Offset: 0x0001709C
		public override IEnumerable<Generator.Output> Outputs()
		{
			yield return this.output;
			yield break;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060023AA RID: 9130 RVA: 0x000E5298 File Offset: 0x000E3498
		// (remove) Token: 0x060023AB RID: 9131 RVA: 0x000E52CC File Offset: 0x000E34CC
		public static event Portal.ChooseEnter OnChooseEnter;

		// Token: 0x060023AC RID: 9132 RVA: 0x000E5300 File Offset: 0x000E3500
		public override void Generate(Chunk chunk, Biome biome = null)
		{
			object obj = null;
			if (this.input.link != null && this.enabled)
			{
				obj = this.input.GetObject(chunk);
			}
			else
			{
				if (this.type == Generator.InoutType.Map)
				{
					obj = chunk.defaultMatrix;
				}
				if (this.type == Generator.InoutType.Objects)
				{
					obj = chunk.defaultSpatialHash;
				}
			}
			if (chunk.stop)
			{
				return;
			}
			this.output.SetObject(chunk, obj);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000E5368 File Offset: 0x000E3568
		public override void OnGUI()
		{
			this.layout.margin = 18;
			this.layout.rightMargin = 15;
			this.layout.Par(17, default(Layout.Val), default(Layout.Val));
			if (this.form == Portal.PortalForm.In)
			{
				this.input.DrawIcon(this.layout, null, false);
			}
			else
			{
				this.output.DrawIcon(this.layout, null);
			}
			this.layout.Field<Generator.InoutType>(ref this.type, null, this.layout.Inset(0.48f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.type != this.input.type)
			{
				this.input.Unlink();
				this.input.type = this.type;
				this.output.type = this.type;
			}
			this.layout.Field<Portal.PortalForm>(ref this.form, null, this.layout.Inset(0.35f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			if (this.form == Portal.PortalForm.Out && this.layout.Button("", this.layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), false, default(Layout.Val), "MapMagic_Focus_Small", null, "Focus on input portal") && this.input.linkGen != null)
			{
				MapMagic.instance.layout.Focus(this.input.linkGen.guiRect.center);
			}
			if (this.form == Portal.PortalForm.In)
			{
				this.layout.CheckButton(ref this.drawConnections, "", this.layout.Inset(20, default(Layout.Val), default(Layout.Val), default(Layout.Val)), false, default(Layout.Val), "MapMagic_ShowConnections", "Show portal connections");
			}
			this.layout.Par(20, default(Layout.Val), default(Layout.Val));
			if (this.form == Portal.PortalForm.In)
			{
				this.name = this.layout.Field<string>(this.name, null, this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val)), -200000000f, 200000000f, default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val), null, null);
			}
			if (this.form == Portal.PortalForm.Out)
			{
				string label = "Select";
				if (this.input.linkGen != null)
				{
					if (!(this.input.linkGen is Portal))
					{
						this.input.Link(null, null);
					}
					else
					{
						label = ((Portal)this.input.linkGen).name;
					}
				}
				Rect rect = this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect.height -= 3f;
				if (this.layout.Button(label, rect, default(Layout.Val), default(Layout.Val), null, null, null) && Portal.OnChooseEnter != null)
				{
					Portal.OnChooseEnter(this, this.type);
				}
			}
		}

		// Token: 0x040022E0 RID: 8928
		public Generator.Input input = new Generator.Input(Generator.InoutType.Map);

		// Token: 0x040022E1 RID: 8929
		public Generator.Output output = new Generator.Output(Generator.InoutType.Map);

		// Token: 0x040022E2 RID: 8930
		public string name;

		// Token: 0x040022E3 RID: 8931
		public Generator.InoutType type;

		// Token: 0x040022E4 RID: 8932
		public Portal.PortalForm form;

		// Token: 0x040022E5 RID: 8933
		public bool drawConnections;

		// Token: 0x02000574 RID: 1396
		public enum PortalForm
		{
			// Token: 0x040022E8 RID: 8936
			In,
			// Token: 0x040022E9 RID: 8937
			Out
		}

		// Token: 0x02000575 RID: 1397
		// (Invoke) Token: 0x060023B0 RID: 9136
		public delegate void ChooseEnter(Portal sender, Generator.InoutType type);
	}
}
