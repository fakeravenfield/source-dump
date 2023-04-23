using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200057E RID: 1406
	public class Layout
	{
		// Token: 0x060023F4 RID: 9204 RVA: 0x000E6D48 File Offset: 0x000E4F48
		public static Rect GetInspectorRect()
		{
			return default(Rect);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0001907A File Offset: 0x0001727A
		public static void SetInspectorRect(Rect rect)
		{
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.GetRect(1f, rect.height, "TextField");
			}
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000E6D60 File Offset: 0x000E4F60
		public void Par(Layout.Val height = default(Layout.Val), Layout.Val margin = default(Layout.Val), Layout.Val padding = default(Layout.Val))
		{
			int num = height.ovd ? ((int)height.val) : this.lineHeight;
			int num2 = margin.ovd ? ((int)margin.val) : this.margin;
			int num3 = padding.ovd ? ((int)padding.val) : this.verticalPadding;
			this.cursor = new Rect(this.field.x + (float)num2, this.cursor.y + this.cursor.height + (float)num3, 0f, (float)(num - num3));
			this.field = new Rect(this.field.x, this.field.y, this.field.width, Mathf.Max(this.field.height, this.cursor.y + this.cursor.height));
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000E6E44 File Offset: 0x000E5044
		public Rect Inset(Layout.Val width = default(Layout.Val), Layout.Val margin = default(Layout.Val), Layout.Val rightMargin = default(Layout.Val), Layout.Val padding = default(Layout.Val))
		{
			int num = margin.ovd ? ((int)margin.val) : this.margin;
			int num2 = rightMargin.ovd ? ((int)rightMargin.val) : this.rightMargin;
			int num3 = padding.ovd ? ((int)padding.val) : this.verticalPadding;
			float num4 = width.ovd ? width.val : 1f;
			if (num4 < 1.0001f)
			{
				num4 *= this.field.width - (float)num - (float)num2;
			}
			this.cursor.x = this.cursor.x + num4;
			this.lastRect = new Rect(this.cursor.x - num4, this.cursor.y + this.field.y, num4 - (float)num3, this.cursor.height);
			return this.lastRect;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000190A5 File Offset: 0x000172A5
		public Rect ParInset(Layout.Val height = default(Layout.Val), Layout.Val width = default(Layout.Val), Layout.Val margin = default(Layout.Val), Layout.Val rightMargin = default(Layout.Val), Layout.Val verticalPadding = default(Layout.Val), Layout.Val horizontalPadding = default(Layout.Val))
		{
			this.Par(height, margin, verticalPadding);
			return this.Inset(width, margin, rightMargin, horizontalPadding);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000E6F24 File Offset: 0x000E5124
		public void Zoom()
		{
			if (Event.current == null)
			{
				return;
			}
			float num = 0f;
			if (Event.current.type == EventType.ScrollWheel)
			{
				num = Event.current.delta.y / 3f;
			}
			else if (Event.current.type == EventType.MouseDrag && Event.current.button == 0 && (Event.current.control || Event.current.command))
			{
				num = Event.current.delta.y / 15f;
			}
			if (Mathf.Abs(num) < 0.001f)
			{
				return;
			}
			float num2 = -this.zoom * this.zoomStep * num;
			if (this.zoom + num2 > this.maxZoom)
			{
				num2 = this.maxZoom - this.zoom;
			}
			if (this.zoom + num2 < this.minZoom)
			{
				num2 = this.minZoom - this.zoom;
			}
			Vector2 a = (Event.current.mousePosition - this.scroll) / this.zoom;
			this.zoom += num2;
			if (this.zoom >= this.minZoom && this.zoom <= this.maxZoom)
			{
				this.scroll -= a * num2;
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000E706C File Offset: 0x000E526C
		public void Scroll()
		{
			if (Event.current == null || Event.current.type != EventType.MouseDrag)
			{
				return;
			}
			if (Event.current.button != this.scrollButton && (Event.current.button != 0 || !Event.current.alt))
			{
				return;
			}
			this.scroll += Event.current.delta;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000E70D4 File Offset: 0x000E52D4
		public Rect ToDisplay(Rect rect)
		{
			return new Rect(rect.x * this.zoom + this.scroll.x, rect.y * this.zoom + this.scroll.y, rect.width * this.zoom, rect.height * this.zoom);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000E7138 File Offset: 0x000E5338
		public Rect ToInternal(Rect rect)
		{
			return new Rect((rect.x - this.scroll.x) / this.zoom, (rect.y - this.scroll.y) / this.zoom, rect.width / this.zoom, rect.height / this.zoom);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000190BD File Offset: 0x000172BD
		public Vector2 ToInternal(Vector2 pos)
		{
			return (pos - this.scroll) / this.zoom;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000190D6 File Offset: 0x000172D6
		public Vector2 ToDisplay(Vector2 pos)
		{
			return pos * this.zoom + this.scroll;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000E719C File Offset: 0x000E539C
		public void Focus(Vector2 pos)
		{
			pos *= this.zoom;
			this.scroll = -pos;
			this.scroll += new Vector2(this.field.width / 2f, this.field.height / 2f);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0000296E File Offset: 0x00000B6E
		public void CheckStyles()
		{
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000E71FC File Offset: 0x000E53FC
		public Texture2D GetIcon(string textureName)
		{
			Texture2D texture2D;
			if (!this.icons.ContainsKey(textureName))
			{
				texture2D = (Resources.Load(textureName) as Texture2D);
				if (texture2D == null)
				{
					texture2D = (Resources.Load(textureName) as Texture2D);
				}
				this.icons.Add(textureName, texture2D);
			}
			else
			{
				texture2D = this.icons[textureName];
			}
			return texture2D;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000E725C File Offset: 0x000E545C
		public bool Icon(string textureName, Rect rect, Layout.IconAligment horizontalAlign = Layout.IconAligment.resize, Layout.IconAligment verticalAlign = Layout.IconAligment.resize, int animationFrames = 0, bool tile = false, bool clickable = false)
		{
			if (animationFrames != 0)
			{
				DateTime now = DateTime.Now;
				int num = (int)(((float)now.Second * 5f + (float)now.Millisecond * 5f / 1000f) % (float)animationFrames);
				string str = ((num + 1 < 10) ? "0" : "") + (num + 1).ToString();
				return this.Icon(textureName + str, rect, Layout.IconAligment.resize, Layout.IconAligment.resize, 0, false, false);
			}
			return this.Icon(this.GetIcon(textureName), rect, horizontalAlign, verticalAlign, tile, clickable);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000E72EC File Offset: 0x000E54EC
		public bool Icon(Texture2D texture, Rect rect, Layout.IconAligment horizontalAlign = Layout.IconAligment.resize, Layout.IconAligment verticalAlign = Layout.IconAligment.resize, bool tile = false, bool clickable = false)
		{
			if (rect.width > (float)texture.width)
			{
				switch (horizontalAlign)
				{
				case Layout.IconAligment.min:
					rect.width = (float)texture.width;
					break;
				case Layout.IconAligment.max:
					rect.x += rect.width;
					rect.x -= (float)texture.width;
					rect.width = (float)texture.width;
					break;
				case Layout.IconAligment.center:
					rect.x += rect.width / 2f;
					rect.x -= (float)(texture.width / 2);
					rect.width = (float)texture.width;
					break;
				}
			}
			if (rect.height > (float)texture.height)
			{
				switch (verticalAlign)
				{
				case Layout.IconAligment.min:
					rect.height = (float)texture.height;
					break;
				case Layout.IconAligment.max:
					rect.y += rect.height;
					rect.y -= (float)texture.height;
					rect.height = (float)texture.height;
					break;
				case Layout.IconAligment.center:
					rect.y += rect.height / 2f;
					rect.y -= (float)(texture.height / 2);
					rect.height = (float)texture.height;
					break;
				}
			}
			bool result = false;
			if (!tile)
			{
				GUI.DrawTexture(this.ToDisplay(rect), texture, ScaleMode.ScaleAndCrop);
			}
			else
			{
				Rect rect2 = this.ToDisplay(rect);
				for (float num = 0f; num < rect.width; num += (float)texture.width * this.zoom)
				{
					for (float num2 = 0f; num2 < rect.height; num2 += (float)texture.height * this.zoom)
					{
						GUI.DrawTexture(new Rect(num + rect2.x, num2 + rect2.y, (float)texture.width * this.zoom, (float)texture.height * this.zoom), texture);
					}
				}
			}
			return result;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000E7508 File Offset: 0x000E5708
		public void Element(string textureName, Rect rect, RectOffset borders, RectOffset offset)
		{
			if (Event.current.type != EventType.Repaint)
			{
				return;
			}
			GUIStyle guistyle = this.elementStyles.CheckGet(textureName);
			if (guistyle == null || guistyle.normal.background == null || guistyle.hover.background == null)
			{
				guistyle = new GUIStyle();
				guistyle.normal.background = this.GetIcon(textureName);
				guistyle.hover.background = this.GetIcon(textureName + "_pro");
				this.elementStyles.CheckAdd(textureName, guistyle, true);
			}
			guistyle.border = borders;
			Rect rect2 = this.ToDisplay(rect);
			rect2 = new Rect(rect2.x - (float)offset.left, rect2.y - (float)offset.top, rect2.width + (float)offset.left + (float)offset.right, rect2.height + (float)offset.top + (float)offset.bottom);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000E7604 File Offset: 0x000E5804
		public float DragChangeField(float val, Rect sliderRect, float min = 0f, float max = 0f, float minStep = 0.2f)
		{
			sliderRect = this.ToDisplay(sliderRect);
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			if (Event.current.type == EventType.MouseDown && sliderRect.Contains(Event.current.mousePosition))
			{
				this.sliderClickPos = Event.current.mousePosition;
				this.sliderOriginalValue = val;
				this.sliderDraggingId = controlID;
			}
			if (this.sliderDraggingId == controlID)
			{
				int num = (int)((Event.current.mousePosition.x - this.sliderClickPos.x) / 5f);
				val = this.sliderOriginalValue;
				for (int i = 0; i < Mathf.Abs(num); i++)
				{
					object obj = (val >= 0f) ? val : (-val);
					float num2 = 0.01f;
					object obj2 = obj;
					if (obj2 != 0.99f)
					{
						num2 = 0.02f;
					}
					if (obj2 != 1.99f)
					{
						num2 = 0.1f;
					}
					if (obj2 != 4.999f)
					{
						num2 = 0.2f;
					}
					if (obj2 != 9.999f)
					{
						num2 = 0.5f;
					}
					if (obj2 != 39.999f)
					{
						num2 = 1f;
					}
					if (obj2 != 99.999f)
					{
						num2 = 2f;
					}
					if (obj2 != 199.999f)
					{
						num2 = 5f;
					}
					if (obj2 != 499.999f)
					{
						num2 = 10f;
					}
					if (num2 < minStep)
					{
						num2 = minStep;
					}
					val = ((num > 0) ? (val + num2) : (val - num2));
					val = Mathf.Round(val * 10000f) / 10000f;
					if (Mathf.Abs(min) > 0.001f && val < min)
					{
						val = min;
					}
					if (Mathf.Abs(max) > 0.001f && val > max)
					{
						val = max;
					}
				}
			}
			if (Event.current.rawType == EventType.MouseUp)
			{
				this.sliderDraggingId = -20000000;
			}
			if (Event.current.isMouse && this.sliderDraggingId == controlID)
			{
				Event.current.Use();
			}
			return val;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06002406 RID: 9222 RVA: 0x000E77BC File Offset: 0x000E59BC
		// (remove) Token: 0x06002407 RID: 9223 RVA: 0x000E77F4 File Offset: 0x000E59F4
		public event Layout.ChangeAction OnBeforeChange;

		// Token: 0x06002408 RID: 9224 RVA: 0x000190EF File Offset: 0x000172EF
		public void SetChange(bool change)
		{
			if (change)
			{
				this.change = true;
				this.lastChange = true;
				if (this.OnBeforeChange != null)
				{
					this.OnBeforeChange();
					return;
				}
			}
			else
			{
				this.lastChange = false;
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000E782C File Offset: 0x000E5A2C
		public void Field<T>(ref T src, string label = null, Rect rect = default(Rect), float min = -200000000f, float max = 200000000f, Layout.Val fieldSize = default(Layout.Val), Layout.Val sliderSize = default(Layout.Val), Layout.Val monitorChange = default(Layout.Val), Layout.Val useEvent = default(Layout.Val), Layout.Val disabled = default(Layout.Val), Layout.Val dragChange = default(Layout.Val), Layout.Val slider = default(Layout.Val), Layout.Val quadratic = default(Layout.Val), Layout.Val allowSceneObject = default(Layout.Val), GUIStyle style = null, string tooltip = null)
		{
			src = this.Field<T>(src, label, rect, min, max, fieldSize, sliderSize, monitorChange, useEvent, disabled, dragChange, slider, quadratic, allowSceneObject, style, tooltip);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000E7868 File Offset: 0x000E5A68
		public T Field<T>(T src, string label = null, Rect rect = default(Rect), float min = -200000000f, float max = 200000000f, Layout.Val fieldSize = default(Layout.Val), Layout.Val sliderSize = default(Layout.Val), Layout.Val monitorChange = default(Layout.Val), Layout.Val useEvent = default(Layout.Val), Layout.Val disabled = default(Layout.Val), Layout.Val dragChange = default(Layout.Val), Layout.Val slider = default(Layout.Val), Layout.Val quadratic = default(Layout.Val), Layout.Val allowSceneObject = default(Layout.Val), GUIStyle style = null, string tooltip = null)
		{
			fieldSize.Verify(this.fieldSize);
			sliderSize.Verify(this.sliderSize);
			useEvent.Verify(this.useEvent);
			disabled.Verify(this.disabled);
			dragChange.Verify(this.dragChange);
			slider.Verify(this.slider);
			this.CheckStyles();
			disabled.Verify(this.disabled);
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			if (label == null)
			{
				fieldSize = 1;
			}
			Rect rect2 = rect.Clamp(1f - fieldSize);
			Rect r = rect.ClampFromLeft(fieldSize);
			Rect r2 = r.Clamp(sliderSize);
			r2 = r2.Clamp((int)r2.width - 4);
			if (slider)
			{
				r = r.ClampFromLeft(1f - sliderSize);
			}
			if (label != null && this.zoom > 0.3f)
			{
				this.Label(label, rect2, null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, tooltip);
			}
			T t = (T)((object)default(T));
			monitorChange.Verify(this.monitorChange);
			if (monitorChange && !EqualityComparer<T>.Default.Equals(src, t))
			{
				this.SetChange(true);
			}
			else
			{
				this.SetChange(false);
			}
			return t;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000E7A30 File Offset: 0x000E5C30
		public void Curve(Curve curve, Rect rect, Color color = default(Color), string tooltip = null)
		{
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			if (color.a < 0.001f)
			{
				color = Color.black;
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000E7ABC File Offset: 0x000E5CBC
		public void Curve(AnimationCurve src, Rect rect, float min = -200000000f, float max = 200000000f, Color color = default(Color), string tooltip = null)
		{
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			if (color.a < 0.001f)
			{
				color = Color.white;
			}
			this.lastChange = false;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000E7B50 File Offset: 0x000E5D50
		public void Label(string label = null, Rect rect = default(Rect), string url = null, bool helpbox = false, Layout.Val fontSize = default(Layout.Val), Layout.Val disabled = default(Layout.Val), FontStyle fontStyle = FontStyle.Normal, TextAnchor textAnchor = TextAnchor.UpperLeft, bool prefix = false, string tooltip = null)
		{
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			this.CheckStyles();
			GUIStyle guistyle = this.labelStyle;
			if (url != null)
			{
				guistyle = this.urlStyle;
			}
			if (helpbox)
			{
				guistyle = this.helpBoxStyle;
			}
			fontSize.Verify(this.fontSize);
			if (guistyle.fontSize != Mathf.RoundToInt(fontSize * this.zoom))
			{
				guistyle.fontSize = Mathf.RoundToInt(fontSize * this.zoom);
			}
			if (guistyle.alignment != textAnchor)
			{
				this.labelStyle.alignment = textAnchor;
			}
			if (guistyle.fontStyle != fontStyle)
			{
				this.labelStyle.fontStyle = fontStyle;
			}
			new GUIContent(label, tooltip);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000E7C60 File Offset: 0x000E5E60
		public bool Button(string label = null, Rect rect = default(Rect), Layout.Val monitorChange = default(Layout.Val), Layout.Val disabled = default(Layout.Val), string icon = null, GUIStyle style = null, string tooltip = null)
		{
			this.CheckStyles();
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			GUIContent content = new GUIContent(label, tooltip);
			disabled.Verify(this.disabled);
			bool flag;
			if (style == null)
			{
				flag = GUI.Button(this.ToDisplay(rect), content, this.buttonStyle);
			}
			else
			{
				flag = GUI.Button(this.ToDisplay(rect), content, style);
			}
			monitorChange.Verify(this.monitorChange);
			if (monitorChange)
			{
				if (flag)
				{
					this.SetChange(true);
				}
				else
				{
					this.SetChange(false);
				}
			}
			if (icon != null)
			{
				this.Icon(icon, new Rect(rect.x + 4f, rect.y, rect.width - 8f, rect.height), Layout.IconAligment.min, Layout.IconAligment.center, 0, false, false);
			}
			return flag;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000E7D88 File Offset: 0x000E5F88
		public void CheckButton(ref bool src, string label = null, Rect rect = default(Rect), Layout.Val monitorChange = default(Layout.Val), Layout.Val disabled = default(Layout.Val), string icon = null, string tooltip = null)
		{
			src = this.CheckButton(src, label, rect, monitorChange, disabled, icon, tooltip);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000E7DAC File Offset: 0x000E5FAC
		public bool CheckButton(bool src, string label = null, Rect rect = default(Rect), Layout.Val monitorChange = default(Layout.Val), Layout.Val disabled = default(Layout.Val), string icon = null, string tooltip = null)
		{
			this.CheckStyles();
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			GUIContent content = new GUIContent(label, tooltip);
			bool flag = GUI.Toggle(this.ToDisplay(rect), src, content, this.buttonStyle);
			monitorChange.Verify(this.monitorChange);
			if (monitorChange)
			{
				if (flag != src)
				{
					this.SetChange(true);
				}
				else
				{
					this.SetChange(false);
				}
			}
			if (icon != null)
			{
				this.Icon(icon, new Rect(rect.x + 4f, rect.y, rect.width - 8f, rect.height), Layout.IconAligment.min, Layout.IconAligment.center, 0, false, false);
			}
			return flag;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000E7EB0 File Offset: 0x000E60B0
		public void Toggle(ref bool src, string label = null, Rect rect = default(Rect), Layout.Val monitorChange = default(Layout.Val), Layout.Val disabled = default(Layout.Val), string onIcon = null, string offIcon = null, string tooltip = null)
		{
			src = this.Toggle(src, label, rect, monitorChange, disabled, onIcon, offIcon, tooltip);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000E7ED4 File Offset: 0x000E60D4
		public bool Toggle(bool src, string label = null, Rect rect = default(Rect), Layout.Val monitorChange = default(Layout.Val), Layout.Val disabled = default(Layout.Val), string onIcon = null, string offIcon = null, string tooltip = null)
		{
			this.CheckStyles();
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			new Rect(rect.x, rect.y, 20f, rect.height);
			Rect rect2 = new Rect(rect.x + 20f, rect.y, rect.width - 20f, rect.height);
			if (label != null)
			{
				this.Label(label, rect2, null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, null);
			}
			monitorChange.Verify(this.monitorChange);
			if (monitorChange)
			{
				if (src != src)
				{
					this.SetChange(true);
				}
				else
				{
					this.SetChange(false);
				}
			}
			return src;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0001911D File Offset: 0x0001731D
		public void Foldout(ref bool src, string label = null, Rect rect = default(Rect), Layout.Val disabled = default(Layout.Val), string tooltip = null)
		{
			src = this.Foldout(src, label, rect, disabled, tooltip);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000E7FF0 File Offset: 0x000E61F0
		public bool Foldout(bool src, string label = null, Rect rect = default(Rect), Layout.Val disabled = default(Layout.Val), string tooltip = null)
		{
			this.CheckStyles();
			if (rect.width < 0.9f && rect.height < 0.9f)
			{
				this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
				rect = this.Inset(default(Layout.Val), default(Layout.Val), default(Layout.Val), default(Layout.Val));
			}
			new GUIContent(label, tooltip);
			return false;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x0000296E File Offset: 0x00000B6E
		public void Spline(Vector2 pos1, Vector2 pos2, Color color = default(Color), bool invert = false)
		{
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000E8078 File Offset: 0x000E6278
		public bool DragDrop(Rect initialRect, int id, Action<Vector2, Rect> onDrag = null, Action<Vector2, Rect> onPress = null, Action<Vector2, Rect> onRelease = null)
		{
			Vector2 vector = this.ToInternal(Event.current.mousePosition);
			if (id == this.dragId)
			{
				this.dragState = Layout.DragState.Drag;
			}
			if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && initialRect.Contains(vector))
			{
				this.dragOffset = new Vector2(initialRect.x, initialRect.y) - vector;
				this.dragId = id;
				this.dragState = Layout.DragState.Pressed;
			}
			if (Event.current.rawType == EventType.MouseUp && id == this.dragId)
			{
				this.dragState = Layout.DragState.Released;
			}
			if (id != this.dragId)
			{
				return false;
			}
			this.dragDelta = vector - this.dragPos;
			this.dragPos = vector;
			this.dragRect = new Rect(vector.x + this.dragOffset.x, vector.y + this.dragOffset.y, initialRect.width, initialRect.height);
			switch (this.dragState)
			{
			case Layout.DragState.Pressed:
				if (onPress != null)
				{
					onPress(this.dragPos, this.dragRect);
				}
				break;
			case Layout.DragState.Drag:
				if (onDrag != null)
				{
					onDrag(this.dragPos, this.dragRect);
				}
				break;
			case Layout.DragState.Released:
				if (onRelease != null)
				{
					onRelease(this.dragPos, this.dragRect);
				}
				break;
			}
			if (this.dragState == Layout.DragState.Released)
			{
				this.dragId = -2000000000;
			}
			return true;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000E81EC File Offset: 0x000E63EC
		public Rect ResizeRect(Rect rectBase, int id, int border = 6, bool sideResize = true)
		{
			Rect rect = this.ToDisplay(rectBase);
			Rect rect2 = new Rect(rect.x + rect.width - (float)(border / 2), rect.y, (float)border, rect.height);
			Rect rect3 = new Rect(rect.x - (float)(border / 2), rect.y, (float)border, rect.height);
			Rect rect4 = new Rect(rect.x, rect.y - (float)(border / 2), rect.width, (float)border);
			Rect rect5 = new Rect(rect.x, rect.y + rect.height - (float)(border / 2), rect.width, (float)border);
			Rect rect6 = new Rect(rect.x + rect.width - (float)border, rect.y - (float)border, (float)(border * 2), (float)(border * 2));
			Rect rect7 = new Rect(rect.x - (float)border, rect.y - (float)border, (float)(border * 2), (float)(border * 2));
			Rect rect8 = new Rect(rect.x + rect.width - (float)border, rect.y + rect.height - (float)border, (float)(border * 2), (float)(border * 2));
			Rect rect9 = new Rect(rect.x - (float)border, rect.y + rect.height - (float)border, (float)(border * 2), (float)(border * 2));
			Vector2 mousePosition = Event.current.mousePosition;
			bool flag = rect6.Contains(mousePosition) || rect7.Contains(mousePosition) || rect8.Contains(mousePosition) || rect9.Contains(mousePosition);
			if (sideResize)
			{
				flag = (flag || rect2.Contains(mousePosition) || rect3.Contains(mousePosition) || rect4.Contains(mousePosition) || rect5.Contains(mousePosition));
			}
			if (Event.current.type == EventType.MouseDown && flag)
			{
				this.dragId = id;
				this.dragPos = Event.current.mousePosition;
				this.dragInitialRect = rect;
				if (sideResize)
				{
					if (rect2.Contains(mousePosition))
					{
						this.dragSide = Layout.DragSide.right;
					}
					else if (rect3.Contains(mousePosition))
					{
						this.dragSide = Layout.DragSide.left;
					}
					else if (rect4.Contains(mousePosition))
					{
						this.dragSide = Layout.DragSide.top;
					}
					else if (rect5.Contains(mousePosition))
					{
						this.dragSide = Layout.DragSide.bottom;
					}
				}
				if (rect6.Contains(mousePosition))
				{
					this.dragSide = Layout.DragSide.rightTop;
				}
				if (rect7.Contains(mousePosition))
				{
					this.dragSide = Layout.DragSide.leftTop;
				}
				if (rect8.Contains(mousePosition))
				{
					this.dragSide = Layout.DragSide.rightBottom;
				}
				if (rect9.Contains(mousePosition))
				{
					this.dragSide = Layout.DragSide.leftBottom;
				}
			}
			if (id == this.dragId)
			{
				Vector2 vector = Event.current.mousePosition - this.dragPos;
				if (this.dragSide == Layout.DragSide.right || this.dragSide == Layout.DragSide.rightTop || this.dragSide == Layout.DragSide.rightBottom)
				{
					rect.width = this.dragInitialRect.width + vector.x;
				}
				if (this.dragSide == Layout.DragSide.left || this.dragSide == Layout.DragSide.leftTop || this.dragSide == Layout.DragSide.leftBottom)
				{
					rect.width = this.dragInitialRect.width - vector.x;
					rect.x = this.dragInitialRect.x + vector.x;
				}
				if (this.dragSide == Layout.DragSide.top || this.dragSide == Layout.DragSide.leftTop || this.dragSide == Layout.DragSide.rightTop)
				{
					rect.height = this.dragInitialRect.height - vector.y;
					rect.y = this.dragInitialRect.y + vector.y;
				}
				if (this.dragSide == Layout.DragSide.bottom || this.dragSide == Layout.DragSide.leftBottom || this.dragSide == Layout.DragSide.rightBottom)
				{
					rect.height = this.dragInitialRect.height + vector.y;
				}
			}
			if (Event.current.rawType == EventType.MouseUp && id == this.dragId)
			{
				this.dragId = -2000000000;
			}
			if (id == this.dragId)
			{
				return this.ToInternal(rect);
			}
			return rectBase;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000E85E8 File Offset: 0x000E67E8
		public void DrawLayered(Layout.ILayered splatOut, string label = "", string tooltip = "")
		{
			this.Par(default(Layout.Val), default(Layout.Val), default(Layout.Val));
			if (label.Length != 0)
			{
				this.Label(label, this.Inset(0.4f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), null, false, default(Layout.Val), default(Layout.Val), FontStyle.Normal, TextAnchor.UpperLeft, false, tooltip);
			}
			if (this.Button(null, this.Inset(0.15f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, "Add new array element"))
			{
				if (this.OnBeforeChange != null)
				{
					this.OnBeforeChange();
				}
				splatOut.layers = ArrayTools.Add<Layout.ILayer>(splatOut.layers, splatOut.selected, splatOut.def);
				int selected = splatOut.selected;
				splatOut.selected = selected + 1;
				splatOut.selected = Mathf.Clamp(splatOut.selected, 0, splatOut.layers.Length - 1);
				splatOut.layers[splatOut.selected].OnAdd();
				this.change = true;
				this.lastChange = true;
			}
			this.Icon("DPLayout_Add", this.lastRect, Layout.IconAligment.center, Layout.IconAligment.center, 0, false, false);
			if (this.Button(null, this.Inset(0.15f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, "Remove element") && splatOut.selected < splatOut.layers.Length && !splatOut.layers[splatOut.selected].pinned)
			{
				if (this.OnBeforeChange != null)
				{
					this.OnBeforeChange();
				}
				splatOut.layers[splatOut.selected].OnRemove();
				splatOut.layers = ArrayTools.RemoveAt<Layout.ILayer>(splatOut.layers, splatOut.selected);
				int selected = splatOut.selected;
				splatOut.selected = selected - 1;
				splatOut.selected = Mathf.Max(splatOut.selected, 0);
				this.change = true;
				this.lastChange = true;
			}
			this.Icon("DPLayout_Remove", this.lastRect, Layout.IconAligment.center, Layout.IconAligment.center, 0, false, false);
			if (this.Button(null, this.Inset(0.15f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, "Move selected up") && splatOut.selected < splatOut.layers.Length - 1 && !splatOut.layers[splatOut.selected].pinned && !splatOut.layers[splatOut.selected + 1].pinned)
			{
				if (this.OnBeforeChange != null)
				{
					this.OnBeforeChange();
				}
				ArrayTools.Switch<Layout.ILayer>(splatOut.layers, splatOut.selected, splatOut.selected + 1);
				int selected = splatOut.selected;
				splatOut.selected = selected + 1;
				this.change = true;
				this.lastChange = true;
			}
			this.Icon("DPLayout_Up", this.lastRect, Layout.IconAligment.center, Layout.IconAligment.center, 0, false, false);
			if (this.Button(null, this.Inset(0.15f, default(Layout.Val), default(Layout.Val), default(Layout.Val)), default(Layout.Val), default(Layout.Val), null, null, "Move selected down") && splatOut.selected != 0 && !splatOut.layers[splatOut.selected].pinned && !splatOut.layers[splatOut.selected - 1].pinned)
			{
				if (this.OnBeforeChange != null)
				{
					this.OnBeforeChange();
				}
				ArrayTools.Switch<Layout.ILayer>(splatOut.layers, splatOut.selected, splatOut.selected - 1);
				int selected = splatOut.selected;
				splatOut.selected = selected - 1;
				this.change = true;
				this.lastChange = true;
			}
			this.Icon("DPLayout_Down", this.lastRect, Layout.IconAligment.center, Layout.IconAligment.center, 0, false, false);
			this.Par(2, default(Layout.Val), default(Layout.Val));
			if (splatOut.selected < splatOut.layers.Length)
			{
				Rect rect = this.cursor;
				int num = splatOut.layers.Length - 1 - splatOut.selected;
				this.Par(Mathf.Max(1, num * (splatOut.collapsedHeight + 8) + 3), default(Layout.Val), default(Layout.Val));
				this.Par(splatOut.extendedHeight, 3, 0);
				this.margin = 0;
				this.rightMargin = 7;
				Rect rect2 = this.Inset(default(Layout.Val), 3, 3, 0);
				rect2.y -= 3f;
				rect2.height += 8f;
				GUI.Box(this.ToDisplay(rect2), "");
				this.cursor = rect;
			}
			int selected2 = splatOut.selected;
			this.margin += 4;
			this.rightMargin++;
			for (int i = splatOut.layers.Length - 1; i >= 0; i--)
			{
				Layout.ILayer layer = splatOut.layers[i];
				bool flag = i == splatOut.selected;
				this.Par(3, default(Layout.Val), default(Layout.Val));
				if (!flag)
				{
					Rect rect3 = this.cursor;
					layer.OnCollapsedGUI(this);
					Rect rect4 = this.cursor;
					splatOut.collapsedHeight = (int)(rect4.y + rect4.height - (rect3.y + rect3.height));
				}
				else
				{
					Rect rect5 = this.cursor;
					layer.OnExtendedGUI(this);
					Rect rect6 = this.cursor;
					splatOut.extendedHeight = (int)(rect6.y + rect6.height - (rect5.y + rect5.height));
				}
				this.Par(5, default(Layout.Val), default(Layout.Val));
			}
			if (splatOut.selected != selected2)
			{
				splatOut.selected = selected2;
			}
		}

		// Token: 0x0400231C RID: 8988
		public Rect field;

		// Token: 0x0400231D RID: 8989
		public Rect cursor;

		// Token: 0x0400231E RID: 8990
		public Rect lastRect;

		// Token: 0x0400231F RID: 8991
		public int margin = 10;

		// Token: 0x04002320 RID: 8992
		public int rightMargin = 10;

		// Token: 0x04002321 RID: 8993
		public int lineHeight = 18;

		// Token: 0x04002322 RID: 8994
		public int verticalPadding = 2;

		// Token: 0x04002323 RID: 8995
		public int horizontalPadding = 3;

		// Token: 0x04002324 RID: 8996
		public UnityEngine.Object undoObject;

		// Token: 0x04002325 RID: 8997
		public string undoName = "";

		// Token: 0x04002326 RID: 8998
		public Vector2 scroll = new Vector2(0f, 0f);

		// Token: 0x04002327 RID: 8999
		public float zoom = 1f;

		// Token: 0x04002328 RID: 9000
		public float zoomStep = 0.0625f;

		// Token: 0x04002329 RID: 9001
		public float minZoom = 0.25f;

		// Token: 0x0400232A RID: 9002
		public float maxZoom = 2f;

		// Token: 0x0400232B RID: 9003
		public int scrollButton = 2;

		// Token: 0x0400232C RID: 9004
		[NonSerialized]
		public GUIStyle labelStyle;

		// Token: 0x0400232D RID: 9005
		[NonSerialized]
		public GUIStyle boldLabelStyle;

		// Token: 0x0400232E RID: 9006
		[NonSerialized]
		public GUIStyle foldoutStyle;

		// Token: 0x0400232F RID: 9007
		[NonSerialized]
		public GUIStyle fieldStyle;

		// Token: 0x04002330 RID: 9008
		[NonSerialized]
		public GUIStyle buttonStyle;

		// Token: 0x04002331 RID: 9009
		[NonSerialized]
		public GUIStyle enumZoomStyle;

		// Token: 0x04002332 RID: 9010
		[NonSerialized]
		public GUIStyle urlStyle;

		// Token: 0x04002333 RID: 9011
		[NonSerialized]
		public GUIStyle toolbarStyle;

		// Token: 0x04002334 RID: 9012
		[NonSerialized]
		public GUIStyle toolbarButtonStyle;

		// Token: 0x04002335 RID: 9013
		[NonSerialized]
		public GUIStyle helpBoxStyle;

		// Token: 0x04002336 RID: 9014
		[NonSerialized]
		private Dictionary<string, Texture2D> icons = new Dictionary<string, Texture2D>();

		// Token: 0x04002337 RID: 9015
		[NonSerialized]
		private Dictionary<string, GUIStyle> elementStyles = new Dictionary<string, GUIStyle>();

		// Token: 0x04002338 RID: 9016
		private Vector2 sliderClickPos;

		// Token: 0x04002339 RID: 9017
		private int sliderDraggingId = -20000000;

		// Token: 0x0400233A RID: 9018
		private float sliderOriginalValue;

		// Token: 0x0400233C RID: 9020
		public bool change;

		// Token: 0x0400233D RID: 9021
		public bool lastChange;

		// Token: 0x0400233E RID: 9022
		public float fieldSize = 0.5f;

		// Token: 0x0400233F RID: 9023
		public float sliderSize = 0.5f;

		// Token: 0x04002340 RID: 9024
		public bool monitorChange = true;

		// Token: 0x04002341 RID: 9025
		public bool useEvent;

		// Token: 0x04002342 RID: 9026
		public bool disabled;

		// Token: 0x04002343 RID: 9027
		public int fontSize = 11;

		// Token: 0x04002344 RID: 9028
		public int iconOffset = 4;

		// Token: 0x04002345 RID: 9029
		public bool dragChange;

		// Token: 0x04002346 RID: 9030
		public bool slider;

		// Token: 0x04002347 RID: 9031
		private Type curveWindowType;

		// Token: 0x04002348 RID: 9032
		private AnimationCurve windowCurveRef;

		// Token: 0x04002349 RID: 9033
		public Layout.DragState dragState;

		// Token: 0x0400234A RID: 9034
		public Rect dragRect;

		// Token: 0x0400234B RID: 9035
		public Vector2 dragPos;

		// Token: 0x0400234C RID: 9036
		public Vector2 dragDelta;

		// Token: 0x0400234D RID: 9037
		public Vector2 dragOffset;

		// Token: 0x0400234E RID: 9038
		public int dragId = -2000000000;

		// Token: 0x0400234F RID: 9039
		public Layout.DragSide dragSide;

		// Token: 0x04002350 RID: 9040
		public Rect dragInitialRect;

		// Token: 0x0200057F RID: 1407
		public struct Val
		{
			// Token: 0x0600241A RID: 9242 RVA: 0x000E8D20 File Offset: 0x000E6F20
			public static implicit operator Layout.Val(bool b)
			{
				return new Layout.Val
				{
					val = (float)(b ? 1 : 0),
					ovd = true
				};
			}

			// Token: 0x0600241B RID: 9243 RVA: 0x000E8D50 File Offset: 0x000E6F50
			public static implicit operator Layout.Val(float f)
			{
				return new Layout.Val
				{
					val = f,
					ovd = true
				};
			}

			// Token: 0x0600241C RID: 9244 RVA: 0x000E8D78 File Offset: 0x000E6F78
			public static implicit operator Layout.Val(int i)
			{
				return new Layout.Val
				{
					val = (float)i,
					ovd = true
				};
			}

			// Token: 0x0600241D RID: 9245 RVA: 0x0001912F File Offset: 0x0001732F
			public static implicit operator bool(Layout.Val v)
			{
				return v.val > 0.5f;
			}

			// Token: 0x0600241E RID: 9246 RVA: 0x00019141 File Offset: 0x00017341
			public static implicit operator float(Layout.Val v)
			{
				return v.val;
			}

			// Token: 0x0600241F RID: 9247 RVA: 0x00019149 File Offset: 0x00017349
			public static implicit operator int(Layout.Val v)
			{
				return (int)v.val;
			}

			// Token: 0x06002420 RID: 9248 RVA: 0x00019152 File Offset: 0x00017352
			public void Verify(float def)
			{
				if (!this.ovd)
				{
					this.val = def;
				}
			}

			// Token: 0x06002421 RID: 9249 RVA: 0x00019163 File Offset: 0x00017363
			public void Verify(int def)
			{
				if (!this.ovd)
				{
					this.val = (float)def;
				}
			}

			// Token: 0x06002422 RID: 9250 RVA: 0x00019175 File Offset: 0x00017375
			public void Verify(bool def)
			{
				if (!this.ovd)
				{
					this.val = (float)(def ? 1 : 0);
				}
			}

			// Token: 0x04002351 RID: 9041
			public float val;

			// Token: 0x04002352 RID: 9042
			public bool ovd;
		}

		// Token: 0x02000580 RID: 1408
		public enum IconAligment
		{
			// Token: 0x04002354 RID: 9044
			resize,
			// Token: 0x04002355 RID: 9045
			min,
			// Token: 0x04002356 RID: 9046
			max,
			// Token: 0x04002357 RID: 9047
			center
		}

		// Token: 0x02000581 RID: 1409
		// (Invoke) Token: 0x06002424 RID: 9252
		public delegate void ChangeAction();

		// Token: 0x02000582 RID: 1410
		public enum HelpboxType
		{
			// Token: 0x04002359 RID: 9049
			off,
			// Token: 0x0400235A RID: 9050
			empty,
			// Token: 0x0400235B RID: 9051
			info,
			// Token: 0x0400235C RID: 9052
			warning,
			// Token: 0x0400235D RID: 9053
			error
		}

		// Token: 0x02000583 RID: 1411
		public enum DragState
		{
			// Token: 0x0400235F RID: 9055
			Pressed,
			// Token: 0x04002360 RID: 9056
			Drag,
			// Token: 0x04002361 RID: 9057
			Released
		}

		// Token: 0x02000584 RID: 1412
		public enum DragSide
		{
			// Token: 0x04002363 RID: 9059
			right,
			// Token: 0x04002364 RID: 9060
			left,
			// Token: 0x04002365 RID: 9061
			top,
			// Token: 0x04002366 RID: 9062
			bottom,
			// Token: 0x04002367 RID: 9063
			rightTop,
			// Token: 0x04002368 RID: 9064
			leftTop,
			// Token: 0x04002369 RID: 9065
			rightBottom,
			// Token: 0x0400236A RID: 9066
			leftBottom
		}

		// Token: 0x02000585 RID: 1413
		public interface ILayered
		{
			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06002427 RID: 9255
			// (set) Token: 0x06002428 RID: 9256
			int selected { get; set; }

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06002429 RID: 9257
			// (set) Token: 0x0600242A RID: 9258
			int collapsedHeight { get; set; }

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x0600242B RID: 9259
			// (set) Token: 0x0600242C RID: 9260
			int extendedHeight { get; set; }

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x0600242D RID: 9261
			// (set) Token: 0x0600242E RID: 9262
			Layout.ILayer[] layers { get; set; }

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x0600242F RID: 9263
			Layout.ILayer def { get; }
		}

		// Token: 0x02000586 RID: 1414
		public interface ILayer
		{
			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06002430 RID: 9264
			bool pinned { get; }

			// Token: 0x06002431 RID: 9265
			void OnCollapsedGUI(Layout layout);

			// Token: 0x06002432 RID: 9266
			void OnExtendedGUI(Layout layout);

			// Token: 0x06002433 RID: 9267
			void OnAdd();

			// Token: 0x06002434 RID: 9268
			void OnRemove();
		}
	}
}
