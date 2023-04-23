using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020008F5 RID: 2293
	internal abstract class FileUserDataBase : RefIdObject
	{
		// Token: 0x06003A5A RID: 14938 RVA: 0x0012B7D0 File Offset: 0x001299D0
		public DynValue lines(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			List<DynValue> list = new List<DynValue>();
			DynValue dynValue;
			do
			{
				dynValue = this.read(executionContext, args);
				list.Add(dynValue);
			}
			while (dynValue.IsNotNil());
			return DynValue.FromObject(executionContext.GetScript(), from s in list
			select s);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x0012B830 File Offset: 0x00129A30
		public DynValue read(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args.Count != 0)
			{
				List<DynValue> list = new List<DynValue>();
				for (int i = 0; i < args.Count; i++)
				{
					DynValue item;
					if (args[i].Type == DataType.Number)
					{
						if (this.Eof())
						{
							return DynValue.Nil;
						}
						int p = (int)args[i].Number;
						item = DynValue.NewString(this.ReadBuffer(p));
					}
					else
					{
						string @string = args.AsType(i, "read", DataType.String, false).String;
						if (this.Eof())
						{
							item = (@string.StartsWith("*a") ? DynValue.NewString("") : DynValue.Nil);
						}
						else if (@string.StartsWith("*n"))
						{
							double? num = this.ReadNumber();
							if (num != null)
							{
								item = DynValue.NewNumber(num.Value);
							}
							else
							{
								item = DynValue.Nil;
							}
						}
						else if (@string.StartsWith("*a"))
						{
							item = DynValue.NewString(this.ReadToEnd());
						}
						else if (@string.StartsWith("*l"))
						{
							item = DynValue.NewString(this.ReadLine().TrimEnd(new char[]
							{
								'\n',
								'\r'
							}));
						}
						else
						{
							if (!@string.StartsWith("*L"))
							{
								throw ScriptRuntimeException.BadArgument(i, "read", "invalid option");
							}
							item = DynValue.NewString(this.ReadLine().TrimEnd(new char[]
							{
								'\n',
								'\r'
							}) + "\n");
						}
					}
					list.Add(item);
				}
				return DynValue.NewTuple(list.ToArray());
			}
			string text = this.ReadLine();
			if (text == null)
			{
				return DynValue.Nil;
			}
			text = text.TrimEnd(new char[]
			{
				'\n',
				'\r'
			});
			return DynValue.NewString(text);
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x0012B9F8 File Offset: 0x00129BF8
		public DynValue write(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				for (int i = 0; i < args.Count; i++)
				{
					string @string = args.AsType(i, "write", DataType.String, false).String;
					this.Write(@string);
				}
				result = UserData.Create(this);
			}
			catch (ScriptRuntimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x0012BA80 File Offset: 0x00129C80
		public DynValue close(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				string text = this.Close();
				if (text == null)
				{
					result = DynValue.True;
				}
				else
				{
					result = DynValue.NewTuple(new DynValue[]
					{
						DynValue.Nil,
						DynValue.NewString(text)
					});
				}
			}
			catch (ScriptRuntimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0012BB04 File Offset: 0x00129D04
		private double? ReadNumber()
		{
			string text = "";
			while (!this.Eof())
			{
				char c = this.Peek();
				if (char.IsWhiteSpace(c))
				{
					this.ReadBuffer(1);
				}
				else
				{
					if (!this.IsNumericChar(c, text))
					{
						break;
					}
					this.ReadBuffer(1);
					text += c.ToString();
				}
			}
			double value;
			if (double.TryParse(text, out value))
			{
				return new double?(value);
			}
			return null;
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x0012BB74 File Offset: 0x00129D74
		private bool IsNumericChar(char c, string numAsFar)
		{
			if (char.IsDigit(c))
			{
				return true;
			}
			if (c == '-')
			{
				return numAsFar.Length == 0;
			}
			if (c == '.')
			{
				return !Framework.Do.StringContainsChar(numAsFar, '.');
			}
			return (c == 'E' || c == 'e') && !Framework.Do.StringContainsChar(numAsFar, 'E') && !Framework.Do.StringContainsChar(numAsFar, 'e');
		}

		// Token: 0x06003A60 RID: 14944
		protected abstract bool Eof();

		// Token: 0x06003A61 RID: 14945
		protected abstract string ReadLine();

		// Token: 0x06003A62 RID: 14946
		protected abstract string ReadBuffer(int p);

		// Token: 0x06003A63 RID: 14947
		protected abstract string ReadToEnd();

		// Token: 0x06003A64 RID: 14948
		protected abstract char Peek();

		// Token: 0x06003A65 RID: 14949
		protected abstract void Write(string value);

		// Token: 0x06003A66 RID: 14950
		protected internal abstract bool isopen();

		// Token: 0x06003A67 RID: 14951
		protected abstract string Close();

		// Token: 0x06003A68 RID: 14952
		public abstract bool flush();

		// Token: 0x06003A69 RID: 14953
		public abstract long seek(string whence, long offset);

		// Token: 0x06003A6A RID: 14954
		public abstract bool setvbuf(string mode);

		// Token: 0x06003A6B RID: 14955 RVA: 0x000275C1 File Offset: 0x000257C1
		public override string ToString()
		{
			if (this.isopen())
			{
				return string.Format("file ({0:X8})", base.ReferenceID);
			}
			return "file (closed)";
		}
	}
}
