using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Tree.Fast_Interface
{
	// Token: 0x02000801 RID: 2049
	internal static class Loader_Fast
	{
		// Token: 0x06003307 RID: 13063 RVA: 0x00114450 File Offset: 0x00112650
		internal static DynamicExprExpression LoadDynamicExpr(Script script, SourceCode source)
		{
			ScriptLoadingContext scriptLoadingContext = Loader_Fast.CreateLoadingContext(script, source);
			DynamicExprExpression result;
			try
			{
				scriptLoadingContext.IsDynamicExpression = true;
				scriptLoadingContext.Anonymous = true;
				Expression exp;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.AstCreation))
				{
					exp = Expression.Expr(scriptLoadingContext);
				}
				result = new DynamicExprExpression(exp, scriptLoadingContext);
			}
			catch (SyntaxErrorException ex)
			{
				ex.DecorateMessage(script);
				ex.Rethrow();
				throw;
			}
			return result;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000231FF File Offset: 0x000213FF
		private static ScriptLoadingContext CreateLoadingContext(Script script, SourceCode source)
		{
			return new ScriptLoadingContext(script)
			{
				Scope = new BuildTimeScope(),
				Source = source,
				Lexer = new Lexer(source.SourceID, source.Code, true)
			};
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x001144C8 File Offset: 0x001126C8
		internal static int LoadChunk(Script script, SourceCode source, ByteCode bytecode)
		{
			ScriptLoadingContext lcontext = Loader_Fast.CreateLoadingContext(script, source);
			int result;
			try
			{
				Statement statement;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.AstCreation))
				{
					statement = new ChunkStatement(lcontext);
				}
				int num = -1;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.Compilation))
				{
					using (bytecode.EnterSource(null))
					{
						bytecode.Emit_Nop(string.Format("Begin chunk {0}", source.Name));
						num = bytecode.GetJumpPointForLastInstruction();
						statement.Compile(bytecode);
						bytecode.Emit_Nop(string.Format("End chunk {0}", source.Name));
					}
				}
				result = num;
			}
			catch (SyntaxErrorException ex)
			{
				ex.DecorateMessage(script);
				ex.Rethrow();
				throw;
			}
			return result;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x001145B4 File Offset: 0x001127B4
		internal static int LoadFunction(Script script, SourceCode source, ByteCode bytecode, bool usesGlobalEnv)
		{
			ScriptLoadingContext lcontext = Loader_Fast.CreateLoadingContext(script, source);
			int result;
			try
			{
				FunctionDefinitionExpression functionDefinitionExpression;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.AstCreation))
				{
					functionDefinitionExpression = new FunctionDefinitionExpression(lcontext, usesGlobalEnv);
				}
				int num = -1;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.Compilation))
				{
					using (bytecode.EnterSource(null))
					{
						bytecode.Emit_Nop(string.Format("Begin function {0}", source.Name));
						num = functionDefinitionExpression.CompileBody(bytecode, source.Name);
						bytecode.Emit_Nop(string.Format("End function {0}", source.Name));
					}
				}
				result = num;
			}
			catch (SyntaxErrorException ex)
			{
				ex.DecorateMessage(script);
				ex.Rethrow();
				throw;
			}
			return result;
		}
	}
}
