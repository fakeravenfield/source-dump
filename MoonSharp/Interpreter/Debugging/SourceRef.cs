using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x020008C9 RID: 2249
	public class SourceRef
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x000265AD File Offset: 0x000247AD
		// (set) Token: 0x060038AF RID: 14511 RVA: 0x000265B5 File Offset: 0x000247B5
		public bool IsClrLocation { get; private set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000265BE File Offset: 0x000247BE
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000265C6 File Offset: 0x000247C6
		public int SourceIdx { get; private set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000265CF File Offset: 0x000247CF
		// (set) Token: 0x060038B3 RID: 14515 RVA: 0x000265D7 File Offset: 0x000247D7
		public int FromChar { get; private set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000265E0 File Offset: 0x000247E0
		// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000265E8 File Offset: 0x000247E8
		public int ToChar { get; private set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000265F1 File Offset: 0x000247F1
		// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000265F9 File Offset: 0x000247F9
		public int FromLine { get; private set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x00026602 File Offset: 0x00024802
		// (set) Token: 0x060038B9 RID: 14521 RVA: 0x0002660A File Offset: 0x0002480A
		public int ToLine { get; private set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x00026613 File Offset: 0x00024813
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x0002661B File Offset: 0x0002481B
		public bool IsStepStop { get; private set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x00026624 File Offset: 0x00024824
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x0002662C File Offset: 0x0002482C
		public bool CannotBreakpoint { get; private set; }

		// Token: 0x060038BE RID: 14526 RVA: 0x00026635 File Offset: 0x00024835
		internal static SourceRef GetClrLocation()
		{
			return new SourceRef(0, 0, 0, 0, 0, false)
			{
				IsClrLocation = true
			};
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00125C88 File Offset: 0x00123E88
		public SourceRef(SourceRef src, bool isStepStop)
		{
			this.SourceIdx = src.SourceIdx;
			this.FromChar = src.FromChar;
			this.ToChar = src.ToChar;
			this.FromLine = src.FromLine;
			this.ToLine = src.ToLine;
			this.IsStepStop = isStepStop;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x00026649 File Offset: 0x00024849
		public SourceRef(int sourceIdx, int from, int to, int fromline, int toline, bool isStepStop)
		{
			this.SourceIdx = sourceIdx;
			this.FromChar = from;
			this.ToChar = to;
			this.FromLine = fromline;
			this.ToLine = toline;
			this.IsStepStop = isStepStop;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x00125CE0 File Offset: 0x00123EE0
		public override string ToString()
		{
			return string.Format("[{0}]{1} ({2}, {3}) -> ({4}, {5})", new object[]
			{
				this.SourceIdx,
				this.IsStepStop ? "*" : " ",
				this.FromLine,
				this.FromChar,
				this.ToLine,
				this.ToChar
			});
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x00125D5C File Offset: 0x00123F5C
		internal int GetLocationDistance(int sourceIdx, int line, int col)
		{
			if (sourceIdx != this.SourceIdx)
			{
				return int.MaxValue;
			}
			if (this.FromLine == this.ToLine)
			{
				if (line != this.FromLine)
				{
					return Math.Abs(line - this.FromLine) * 1600;
				}
				if (col >= this.FromChar && col <= this.ToChar)
				{
					return 0;
				}
				if (col < this.FromChar)
				{
					return this.FromChar - col;
				}
				return col - this.ToChar;
			}
			else if (line == this.FromLine)
			{
				if (col < this.FromChar)
				{
					return this.FromChar - col;
				}
				return 0;
			}
			else if (line == this.ToLine)
			{
				if (col > this.ToChar)
				{
					return col - this.ToChar;
				}
				return 0;
			}
			else
			{
				if (line > this.FromLine && line < this.ToLine)
				{
					return 0;
				}
				if (line < this.FromLine)
				{
					return (this.FromLine - line) * 1600;
				}
				return (line - this.ToLine) * 1600;
			}
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x00125E48 File Offset: 0x00124048
		public bool IncludesLocation(int sourceIdx, int line, int col)
		{
			if (sourceIdx != this.SourceIdx || line < this.FromLine || line > this.ToLine)
			{
				return false;
			}
			if (this.FromLine == this.ToLine)
			{
				return col >= this.FromChar && col <= this.ToChar;
			}
			if (line == this.FromLine)
			{
				return col >= this.FromChar;
			}
			return line != this.ToLine || col <= this.ToChar;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x0002667E File Offset: 0x0002487E
		public SourceRef SetNoBreakPoint()
		{
			this.CannotBreakpoint = true;
			return this;
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x00125EC8 File Offset: 0x001240C8
		public string FormatLocation(Script script, bool forceClassicFormat = false)
		{
			SourceCode sourceCode = script.GetSourceCode(this.SourceIdx);
			if (this.IsClrLocation)
			{
				return "[clr]";
			}
			if (script.Options.UseLuaErrorLocations || forceClassicFormat)
			{
				return string.Format("{0}:{1}", sourceCode.Name, this.FromLine);
			}
			if (this.FromLine != this.ToLine)
			{
				return string.Format("{0}:({1},{2}-{3},{4})", new object[]
				{
					sourceCode.Name,
					this.FromLine,
					this.FromChar,
					this.ToLine,
					this.ToChar
				});
			}
			if (this.FromChar == this.ToChar)
			{
				return string.Format("{0}:({1},{2})", new object[]
				{
					sourceCode.Name,
					this.FromLine,
					this.FromChar,
					this.ToLine,
					this.ToChar
				});
			}
			return string.Format("{0}:({1},{2}-{4})", new object[]
			{
				sourceCode.Name,
				this.FromLine,
				this.FromChar,
				this.ToLine,
				this.ToChar
			});
		}

		// Token: 0x04002FC9 RID: 12233
		public bool Breakpoint;
	}
}
