using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.REPL;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lua
{
	// Token: 0x02000929 RID: 2345
	public class RavenscriptRepl : ScriptConsole
	{
		// Token: 0x06003BA0 RID: 15264 RVA: 0x00028373 File Offset: 0x00026573
		private void SetPrompt()
		{
			this.prompt.text = ((this.repl != null) ? this.repl.ClassicPrompt : ">") + " ";
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000283A4 File Offset: 0x000265A4
		private string GetPrompt()
		{
			return this.prompt.text;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000283B1 File Offset: 0x000265B1
		private void GiveFocusToInput()
		{
			EventSystem.current.SetSelectedGameObject(this.input.gameObject, null);
			this.input.OnPointerClick(new PointerEventData(EventSystem.current));
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x0012CFE8 File Offset: 0x0012B1E8
		public override void Initialize()
		{
			base.Initialize();
			this.output.text = "";
			this.SetPrompt();
			this.input.text = "";
			this.input.gameObject.SetActive(false);
			this.prompt.gameObject.SetActive(false);
			this.RenderText();
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000283DE File Offset: 0x000265DE
		private void LateUpdate()
		{
			if (this.enqueuedScrollToBottom)
			{
				this.enqueuedScrollToBottom = false;
				this.ScrollToBottom();
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x0012D04C File Offset: 0x0012B24C
		private void Update()
		{
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000283F5 File Offset: 0x000265F5
		private void ScrollToBottomDeferred()
		{
			this.enqueuedScrollToBottom = true;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000283FE File Offset: 0x000265FE
		private void ScrollToBottom()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.container);
			this.scrollView.normalizedPosition = new Vector2(0f, 0f);
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0012D05C File Offset: 0x0012B25C
		private void RenderText()
		{
			bool flag = !this.scrollView.verticalScrollbar.gameObject.activeSelf || this.scrollView.normalizedPosition.y < 0.1f;
			string text = "";
			foreach (string text2 in this.lines.Reverse<string>())
			{
				if (text.Length + text2.Length > 5000)
				{
					break;
				}
				text = text2 + "\n" + text;
			}
			if (SteelInput.IsInitialized())
			{
				text += string.Format("<i><color=grey>Press {0} to close the console</color></i>", SteelInput.GetInput(SteelInput.KeyBinds.Console).PositiveLabel());
			}
			this.output.text = text.Trim();
			if (flag)
			{
				this.ScrollToBottomDeferred();
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0012D144 File Offset: 0x0012B344
		private void AddEntry(string message, string color = null, bool canStack = false)
		{
			message = " " + message.Replace("\r", "").Trim();
			if (canStack && message == this.prevMessage)
			{
				this.messageStackLength++;
				this.lines[this.lines.Count - 1] = string.Format("{0} <color=red>({1})</color>", this.stackedLineString, this.messageStackLength);
			}
			else
			{
				foreach (string text in message.Split(new char[]
				{
					'\n'
				}))
				{
					if (color != null)
					{
						this.lines.Add(string.Concat(new string[]
						{
							"<color=",
							color,
							">",
							text,
							"</color>"
						}));
					}
					else
					{
						this.lines.Add(text);
					}
				}
				if (this.lines.Count > 100)
				{
					this.lines.RemoveRange(0, this.lines.Count - 100);
				}
				this.prevMessage = message;
				this.stackedLineString = this.lines.Last<string>();
				Debug.Log(this.stackedLineString);
				this.messageStackLength = 1;
			}
			if (this.IsVisible())
			{
				this.RenderText();
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x00028425 File Offset: 0x00026625
		public override void OnScriptEngineReady(ReplInterpreter repl)
		{
			this.repl = repl;
			this.repl.HandleClassicExprsSyntax = true;
			this.LogInfo("Ready");
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x00028445 File Offset: 0x00026645
		public override bool IsVisible()
		{
			return this.container.gameObject.activeSelf;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x00028457 File Offset: 0x00026657
		public override void Show()
		{
			this.container.gameObject.SetActive(true);
			this.GiveFocusToInput();
			this.RenderText();
			this.ScrollToBottomDeferred();
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x0002847C File Offset: 0x0002667C
		public override void Hide()
		{
			this.container.gameObject.SetActive(false);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x0002848F File Offset: 0x0002668F
		public override void LogInfo(string message)
		{
			this.AddEntry(message, null, false);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x0002849A File Offset: 0x0002669A
		public override void LogError(string message)
		{
			this.unseenErrorCount++;
			Debug.LogError("Lua Error: " + message);
			this.AddEntry("Error: " + message, "red", true);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x0012D298 File Offset: 0x0012B498
		public void OnGUI()
		{
			if (GameManager.IsTestingContentMod())
			{
				if (this.IsVisible())
				{
					this.unseenErrorCount = 0;
					return;
				}
				if (this.unseenErrorCount > 0)
				{
					string text = (this.unseenErrorCount < 1000) ? "{0} Errors ({1} to view)" : "1000+ Errors ({1} to view)";
					text = string.Format(text, this.unseenErrorCount, SteelInput.GetInput(SteelInput.KeyBinds.Console).PositiveLabel());
					GUI.color = Color.red;
					GUI.Label(new Rect(5f, 5f, 400f, 50f), text);
				}
			}
		}

		// Token: 0x0400309E RID: 12446
		public RectTransform container;

		// Token: 0x0400309F RID: 12447
		public Text output;

		// Token: 0x040030A0 RID: 12448
		public ScrollRect scrollView;

		// Token: 0x040030A1 RID: 12449
		public InputField input;

		// Token: 0x040030A2 RID: 12450
		public Text prompt;

		// Token: 0x040030A3 RID: 12451
		private const int MAX_LINES = 100;

		// Token: 0x040030A4 RID: 12452
		private const int MAX_CHARS = 5000;

		// Token: 0x040030A5 RID: 12453
		private const bool ENABLE_REPL = false;

		// Token: 0x040030A6 RID: 12454
		private List<string> lines = new List<string>();

		// Token: 0x040030A7 RID: 12455
		private ReplInterpreter repl;

		// Token: 0x040030A8 RID: 12456
		private string prevMessage = "";

		// Token: 0x040030A9 RID: 12457
		private string stackedLineString = "";

		// Token: 0x040030AA RID: 12458
		private int messageStackLength = 1;

		// Token: 0x040030AB RID: 12459
		private bool enqueuedScrollToBottom;

		// Token: 0x040030AC RID: 12460
		private int unseenErrorCount;
	}
}
