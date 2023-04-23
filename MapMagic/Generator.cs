using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200056A RID: 1386
	[Serializable]
	public abstract class Generator
	{
		// Token: 0x06002369 RID: 9065 RVA: 0x000E4218 File Offset: 0x000E2418
		public virtual void Move(Vector2 delta, bool moveChildren = true)
		{
			Layout layout = this.layout;
			layout.field.position = layout.field.position + delta;
			this.guiRect = this.layout.field;
			foreach (Generator.IGuiInout guiInout in this.Inouts())
			{
				guiInout.guiRect = new Rect(guiInout.guiRect.position + delta, guiInout.guiRect.size);
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00018C9B File Offset: 0x00016E9B
		public virtual IEnumerable<Generator.Output> Outputs()
		{
			yield break;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00018CA4 File Offset: 0x00016EA4
		public virtual IEnumerable<Generator.Input> Inputs()
		{
			yield break;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00018CAD File Offset: 0x00016EAD
		public IEnumerable<Generator.IGuiInout> Inouts()
		{
			foreach (Generator.Output output in this.Outputs())
			{
				yield return output;
			}
			IEnumerator<Generator.Output> enumerator = null;
			foreach (Generator.Input input in this.Inputs())
			{
				yield return input;
			}
			IEnumerator<Generator.Input> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00018CBD File Offset: 0x00016EBD
		public static bool CanConnect(Generator.Output output, Generator.Input input)
		{
			return output.type == input.type;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000E42B8 File Offset: 0x000E24B8
		public bool IsDependentFrom(Generator prior)
		{
			foreach (Generator.Input input in this.Inputs())
			{
				if (input != null && input.linkGen != null)
				{
					if (prior == input.linkGen)
					{
						return true;
					}
					if (input.linkGen.IsDependentFrom(prior))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000E432C File Offset: 0x000E252C
		public void CountPriorsRecursive(Chunk tw, HashSet<Generator> jobs)
		{
			if (!jobs.Contains(this))
			{
				foreach (Generator.Input input in this.Inputs())
				{
					if (input.linkGen != null)
					{
						input.linkGen.CountPriorsRecursive(tw, jobs);
					}
				}
			}
			jobs.Add(this);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000E4398 File Offset: 0x000E2598
		public void CheckClearRecursive(Chunk tw)
		{
			foreach (Generator.Input input in this.Inputs())
			{
				if (input.linkGen != null)
				{
					input.linkGen.CheckClearRecursive(tw);
					if (!tw.ready.Contains(input.linkGen) && tw.ready.Contains(this))
					{
						tw.ready.Remove(this);
					}
				}
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000E4420 File Offset: 0x000E2620
		public void GenerateWithPriors(Chunk tw, Biome biome = null)
		{
			foreach (Generator.Input input in this.Inputs())
			{
				if (input.linkGen != null)
				{
					if (tw.stop)
					{
						return;
					}
					input.linkGen.GenerateWithPriors(tw, biome);
				}
			}
			if (tw.stop)
			{
				return;
			}
			if (!tw.ready.Contains(this))
			{
				if (MapMagic.instance.guiDebug)
				{
					if (tw.timer == null)
					{
						tw.timer = new Stopwatch();
					}
					else
					{
						tw.timer.Reset();
					}
					tw.timer.Start();
				}
				this.Generate(tw, biome);
				if (!tw.stop)
				{
					tw.ready.Add(this);
					tw.numReady = tw.ready.Count;
				}
				if (tw.timer != null)
				{
					tw.timer.Stop();
					this.guiDebugTime = (float)tw.timer.ElapsedMilliseconds;
				}
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void Generate(Chunk chunk, Biome biome = null)
		{
		}

		// Token: 0x06002373 RID: 9075
		public abstract void OnGUI();

		// Token: 0x06002374 RID: 9076 RVA: 0x000E452C File Offset: 0x000E272C
		public void OnGUIBase()
		{
			this.layout.Element("MapMagic_Window", this.layout.field, new RectOffset(34, 34, 34, 34), new RectOffset(33, 33, 33, 33));
			this.layout.field.height = 0f;
			this.layout.field.width = 160f;
			this.layout.cursor = default(Rect);
			this.layout.change = false;
			this.layout.margin = 1;
			this.layout.rightMargin = 1;
			this.layout.fieldSize = 0.4f;
			this.layout.Icon("MapMagic_Window_Header", new Rect(this.layout.field.x, this.layout.field.y, this.layout.field.width, 16f), Layout.IconAligment.resize, Layout.IconAligment.resize, 0, false, false);
			this.layout.Par(14, default(Layout.Val), default(Layout.Val));
			this.layout.Inset(2, default(Layout.Val), default(Layout.Val), default(Layout.Val));
			Rect rect = this.layout.Inset(18, default(Layout.Val), default(Layout.Val), default(Layout.Val));
			GeneratorMenuAttribute generatorMenuAttribute = Attribute.GetCustomAttribute(base.GetType(), typeof(GeneratorMenuAttribute)) as GeneratorMenuAttribute;
			if (generatorMenuAttribute != null && generatorMenuAttribute.disengageable)
			{
				this.layout.Toggle(ref this.enabled, null, rect, default(Layout.Val), default(Layout.Val), "MapMagic_GeneratorEnabled", "MapMagic_GeneratorDisabled", null);
			}
			else
			{
				this.layout.Icon("MapMagic_GeneratorAlwaysOn", rect, Layout.IconAligment.center, Layout.IconAligment.center, 0, false, false);
			}
			if (this.layout.lastChange && !this.enabled && this is Generator.IOutput)
			{
				foreach (Generator.IOutput output in MapMagic.instance.gens.OutputGenerators(false, true))
				{
					Generator generator = (Generator)output;
					if (generator.GetType() == base.GetType())
					{
						foreach (Chunk chunk in MapMagic.instance.terrains.Objects())
						{
							chunk.ready.CheckRemove(generator);
						}
					}
				}
			}
			string text = (generatorMenuAttribute == null) ? "Unknown" : generatorMenuAttribute.name;
			if (MapMagic.instance.guiDebug)
			{
				bool flag = true;
				using (IEnumerator<Chunk> enumerator2 = MapMagic.instance.terrains.Objects().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (!enumerator2.Current.ready.Contains(this))
						{
							flag = false;
						}
					}
				}
				if (!flag)
				{
					text += "*";
				}
			}
			Rect rect2 = this.layout.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			rect2.height = 25f;
			rect2.y -= (1f - this.layout.zoom) * 6f + 2f;
			this.layout.Label(text, rect2, null, false, 19f - this.layout.zoom * 8f, default(Layout.Val), FontStyle.Bold, TextAnchor.UpperLeft, false, null);
			this.layout.Par(1, default(Layout.Val), default(Layout.Val));
			this.layout.Par(3, default(Layout.Val), default(Layout.Val));
			if (!MapMagic.instance.guiDebug)
			{
				try
				{
					this.OnGUI();
					goto IL_46F;
				}
				catch (Exception ex)
				{
					if (ex is ArgumentOutOfRangeException || ex is NullReferenceException)
					{
						string str = "Error drawing generator ";
						Type type = base.GetType();
						string str2 = (type != null) ? type.ToString() : null;
						string str3 = "\n";
						Exception ex2 = ex;
						Debug.LogError(str + str2 + str3 + ((ex2 != null) ? ex2.ToString() : null));
					}
					goto IL_46F;
				}
			}
			this.OnGUI();
			IL_46F:
			this.layout.Par(3, default(Layout.Val), default(Layout.Val));
			if (MapMagic.instance.guiDebug)
			{
				Rect rect3 = new Rect(this.layout.field.x, this.layout.field.y + this.layout.field.height, 200f, 20f);
				string text2 = "g:" + this.guiDebugTime.ToString() + "ms ";
				if (this is Generator.IOutput)
				{
					if (MapMagic.instance.guiDebugProcessTimes.ContainsKey(base.GetType()))
					{
						text2 = text2 + " p:" + MapMagic.instance.guiDebugProcessTimes[base.GetType()].ToString() + "ms ";
					}
					if (MapMagic.instance.guiDebugApplyTimes.ContainsKey(base.GetType()))
					{
						text2 = text2 + " a:" + MapMagic.instance.guiDebugApplyTimes[base.GetType()].ToString() + "ms ";
					}
				}
				this.layout.Label(text2, rect3, null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			}
		}

		// Token: 0x040022C5 RID: 8901
		public bool enabled = true;

		// Token: 0x040022C6 RID: 8902
		[NonSerialized]
		public Layout layout = new Layout();

		// Token: 0x040022C7 RID: 8903
		public Rect guiRect;

		// Token: 0x040022C8 RID: 8904
		public float guiDebugTime;

		// Token: 0x040022C9 RID: 8905
		[NonSerialized]
		public Biome biome;

		// Token: 0x0200056B RID: 1387
		public enum InoutType
		{
			// Token: 0x040022CB RID: 8907
			Map,
			// Token: 0x040022CC RID: 8908
			Objects,
			// Token: 0x040022CD RID: 8909
			Spline
		}

		// Token: 0x0200056C RID: 1388
		public interface IGuiInout
		{
			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06002376 RID: 9078
			// (set) Token: 0x06002377 RID: 9079
			Rect guiRect { get; set; }
		}

		// Token: 0x0200056D RID: 1389
		public interface IOutput
		{
		}

		// Token: 0x0200056E RID: 1390
		public class Input : Generator.IGuiInout
		{
			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06002378 RID: 9080 RVA: 0x00018CE7 File Offset: 0x00016EE7
			// (set) Token: 0x06002379 RID: 9081 RVA: 0x00018CEF File Offset: 0x00016EEF
			public Rect guiRect { get; set; }

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x0600237A RID: 9082 RVA: 0x000E4B34 File Offset: 0x000E2D34
			public Color guiColor
			{
				get
				{
					bool flag = false;
					Generator.InoutType inoutType = this.type;
					if (inoutType != Generator.InoutType.Map)
					{
						if (inoutType != Generator.InoutType.Objects)
						{
							return Color.black;
						}
						if (!flag)
						{
							return new Color(0.1f, 0.4f, 0.1f);
						}
						return new Color(0.15f, 0.6f, 0.15f);
					}
					else
					{
						if (!flag)
						{
							return new Color(0.05f, 0.2f, 0.35f);
						}
						return new Color(0.23f, 0.5f, 0.652f);
					}
				}
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x0600237B RID: 9083 RVA: 0x000E4BB4 File Offset: 0x000E2DB4
			public Vector2 guiConnectionPos
			{
				get
				{
					return new Vector2(this.guiRect.xMin, this.guiRect.center.y);
				}
			}

			// Token: 0x0600237C RID: 9084 RVA: 0x000E4BE8 File Offset: 0x000E2DE8
			public void DrawIcon(Layout layout, string label = null, bool mandatory = false)
			{
				string textureName = "";
				switch (this.type)
				{
				case Generator.InoutType.Map:
					textureName = "MapMagicMatrix";
					break;
				case Generator.InoutType.Objects:
					textureName = "MapMagicScatter";
					break;
				case Generator.InoutType.Spline:
					textureName = "MapMagicSpline";
					break;
				}
				this.guiRect = new Rect(layout.field.x - 5f, layout.cursor.y + layout.field.y, 18f, 18f);
				layout.Icon(textureName, this.guiRect, Layout.IconAligment.resize, Layout.IconAligment.resize, 0, false, false);
				if (label != null)
				{
					Rect guiRect = this.guiRect;
					guiRect.width = 100f;
					guiRect.x += this.guiRect.width + 2f;
					layout.Label(label, guiRect, null, false, 10, default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
				}
				if (mandatory && this.linkGen == null)
				{
					layout.Icon("MapMagic_Mandatory", new Rect(this.guiRect.x + 10f + 2f, this.guiRect.y - 2f, 8f, 8f), Layout.IconAligment.resize, Layout.IconAligment.resize, 0, false, false);
				}
			}

			// Token: 0x0600237D RID: 9085 RVA: 0x0000256A File Offset: 0x0000076A
			public Input()
			{
			}

			// Token: 0x0600237E RID: 9086 RVA: 0x00018CF8 File Offset: 0x00016EF8
			public Input(Generator.InoutType t)
			{
				this.type = t;
			}

			// Token: 0x0600237F RID: 9087 RVA: 0x00018D07 File Offset: 0x00016F07
			public Input(string n, Generator.InoutType t, bool write = false, bool mandatory = false)
			{
				this.type = t;
			}

			// Token: 0x06002380 RID: 9088 RVA: 0x000E4D2C File Offset: 0x000E2F2C
			public Generator GetGenerator(Generator[] gens)
			{
				for (int i = 0; i < gens.Length; i++)
				{
					using (IEnumerator<Generator.Input> enumerator = gens[i].Inputs().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == this)
							{
								return gens[i];
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06002381 RID: 9089 RVA: 0x00018D16 File Offset: 0x00016F16
			public void Link(Generator.Output output, Generator outputGen)
			{
				this.link = output;
				this.linkGen = outputGen;
			}

			// Token: 0x06002382 RID: 9090 RVA: 0x00018D26 File Offset: 0x00016F26
			public void Unlink()
			{
				this.link = null;
				this.linkGen = null;
			}

			// Token: 0x06002383 RID: 9091 RVA: 0x00018D36 File Offset: 0x00016F36
			public object GetObject(Chunk tw)
			{
				if (this.link == null)
				{
					return null;
				}
				if (!tw.results.ContainsKey(this.link))
				{
					return null;
				}
				return tw.results[this.link];
			}

			// Token: 0x040022CE RID: 8910
			public Generator.InoutType type;

			// Token: 0x040022CF RID: 8911
			public Generator.Output link;

			// Token: 0x040022D0 RID: 8912
			public Generator linkGen;
		}

		// Token: 0x0200056F RID: 1391
		public class Output : Generator.IGuiInout, Generator.IOutput
		{
			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06002384 RID: 9092 RVA: 0x00018D68 File Offset: 0x00016F68
			// (set) Token: 0x06002385 RID: 9093 RVA: 0x00018D70 File Offset: 0x00016F70
			public Rect guiRect { get; set; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06002386 RID: 9094 RVA: 0x000E4D90 File Offset: 0x000E2F90
			public Vector2 guiConnectionPos
			{
				get
				{
					return new Vector2(this.guiRect.xMax, this.guiRect.center.y);
				}
			}

			// Token: 0x06002387 RID: 9095 RVA: 0x000E4DC4 File Offset: 0x000E2FC4
			public void DrawIcon(Layout layout, string label = null)
			{
				string textureName = "";
				switch (this.type)
				{
				case Generator.InoutType.Map:
					textureName = "MapMagicMatrix";
					break;
				case Generator.InoutType.Objects:
					textureName = "MapMagicScatter";
					break;
				case Generator.InoutType.Spline:
					textureName = "MapMagicSpline";
					break;
				}
				this.guiRect = new Rect(layout.field.x + layout.field.width - 18f + 5f, layout.cursor.y + layout.field.y, 18f, 18f);
				if (label != null)
				{
					Rect guiRect = this.guiRect;
					guiRect.width = 100f;
					guiRect.x -= 103f;
					layout.Label(label, guiRect, null, false, 10, default(Layout.Val), FontStyle.Normal, TextAnchor.LowerRight, false, null);
				}
				layout.Icon(textureName, this.guiRect, Layout.IconAligment.resize, Layout.IconAligment.resize, 0, false, false);
				if (MapMagic.instance.guiDebug)
				{
					Rect guiRect2 = this.guiRect;
					guiRect2.width = 100f;
					guiRect2.x += 25f;
					Chunk closestObj = MapMagic.instance.terrains.GetClosestObj(new Coord(0, 0));
					if (closestObj != null)
					{
						object obj = closestObj.results.CheckGet(this);
						layout.Label((obj != null) ? obj.GetHashCode().ToString() : "null", guiRect2, null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.LowerLeft, false, null);
					}
				}
			}

			// Token: 0x06002388 RID: 9096 RVA: 0x0000256A File Offset: 0x0000076A
			public Output()
			{
			}

			// Token: 0x06002389 RID: 9097 RVA: 0x00018D79 File Offset: 0x00016F79
			public Output(Generator.InoutType t)
			{
				this.type = t;
			}

			// Token: 0x0600238A RID: 9098 RVA: 0x00018D88 File Offset: 0x00016F88
			public Output(string n, Generator.InoutType t)
			{
				this.type = t;
			}

			// Token: 0x0600238B RID: 9099 RVA: 0x000E4F50 File Offset: 0x000E3150
			public Generator GetGenerator(Generator[] gens)
			{
				for (int i = 0; i < gens.Length; i++)
				{
					using (IEnumerator<Generator.Output> enumerator = gens[i].Outputs().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == this)
							{
								return gens[i];
							}
						}
					}
				}
				return null;
			}

			// Token: 0x0600238C RID: 9100 RVA: 0x000E4FB4 File Offset: 0x000E31B4
			public Generator.Input GetConnectedInput(Generator[] gens)
			{
				for (int i = 0; i < gens.Length; i++)
				{
					foreach (Generator.Input input in gens[i].Inputs())
					{
						if (input.link == this)
						{
							return input;
						}
					}
				}
				return null;
			}

			// Token: 0x0600238D RID: 9101 RVA: 0x00018D97 File Offset: 0x00016F97
			public void SetObject(Chunk terrain, object obj)
			{
				if (!terrain.results.ContainsKey(this))
				{
					if (obj != null)
					{
						terrain.results.Add(this, obj);
					}
					return;
				}
				if (obj == null)
				{
					terrain.results.Remove(this);
					return;
				}
				terrain.results[this] = obj;
			}

			// Token: 0x040022D2 RID: 8914
			public Generator.InoutType type;
		}
	}
}
