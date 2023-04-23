using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace Lua
{
	// Token: 0x0200091E RID: 2334
	public class LuaClass
	{
		// Token: 0x06003B34 RID: 15156 RVA: 0x00027E69 File Offset: 0x00026069
		public LuaClass(ScriptEngine engine, Table environment, string className)
		{
			this.engine = engine;
			this.self = this.Instantiate(environment, className, out this.prototype);
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x0012C668 File Offset: 0x0012A868
		private DynValue Instantiate(Table environment, string name, out DynValue prototype)
		{
			prototype = DynValue.Nil;
			DynValue result = DynValue.Nil;
			try
			{
				prototype = environment.RawGet(name);
				if (prototype == null || prototype.IsNil())
				{
					throw new ScriptRuntimeException("no such table exists");
				}
				if (prototype.Type != DataType.Table)
				{
					throw new ScriptRuntimeException("class is not a table");
				}
				if (prototype.Table.MetaTable == null)
				{
					throw new ScriptRuntimeException("no metatable");
				}
				DynValue dynValue = prototype.Table.RawGet("new");
				if (dynValue == null || dynValue.Function == null)
				{
					dynValue = prototype.Table.MetaTable.RawGet("__call");
					if (dynValue == null || dynValue.Function == null)
					{
						throw new ScriptRuntimeException("no new/__call function");
					}
				}
				result = this.engine.Call(dynValue, new DynValue[]
				{
					prototype
				});
			}
			catch (Exception ex)
			{
				this.engine.console.LogException(ex, "Unable to instantiate class '{0}'", new object[]
				{
					name
				});
			}
			return result;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00027E8C File Offset: 0x0002608C
		public bool IsNil()
		{
			return this.prototype.IsNil() || this.self.IsNil();
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00027EA8 File Offset: 0x000260A8
		public static implicit operator bool(LuaClass lc)
		{
			return lc != null && !lc.IsNil();
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x00027EB8 File Offset: 0x000260B8
		public void Set(string key, DynValue value)
		{
			if (this.self.IsNotNil())
			{
				this.self.Table.Set(key, value);
			}
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00027ED9 File Offset: 0x000260D9
		public void Set(string key, object value)
		{
			this.Set(key, this.engine.CreateDynValue(value));
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x0012C76C File Offset: 0x0012A96C
		public DynValue Get(string key)
		{
			DynValue dynValue = DynValue.Nil;
			if (this.self.IsNotNil())
			{
				dynValue = this.self.Table.Get(key);
				if (dynValue.IsNil())
				{
					dynValue = this.prototype.Table.Get(key);
				}
			}
			return dynValue;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x0012C7BC File Offset: 0x0012A9BC
		public LuaClass.Method GetMethod(string method_name)
		{
			string text = DescriptorHelpers.UpperFirstLetter(method_name);
			string text2 = DescriptorHelpers.Camelify(method_name);
			string text3 = DescriptorHelpers.UpperFirstLetter(text2);
			return this.GetMethod(new string[]
			{
				text3,
				text2,
				method_name,
				text
			});
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0012C7FC File Offset: 0x0012A9FC
		private LuaClass.Method GetMethod(params string[] methodNames)
		{
			foreach (string methodName in methodNames)
			{
				if (this.HasMethod(methodName))
				{
					return new LuaClass.Method(this, methodName);
				}
			}
			return new LuaClass.Method(this, DynValue.Nil);
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00027EEE File Offset: 0x000260EE
		private bool HasMethod(string methodName)
		{
			return this.prototype.IsNotNil() && this.prototype.Table.Get(methodName).IsNotNil();
		}

		// Token: 0x04003069 RID: 12393
		private readonly ScriptEngine engine;

		// Token: 0x0400306A RID: 12394
		public readonly DynValue prototype;

		// Token: 0x0400306B RID: 12395
		public readonly DynValue self;

		// Token: 0x0200091F RID: 2335
		public class Method
		{
			// Token: 0x06003B3E RID: 15166 RVA: 0x00027F15 File Offset: 0x00026115
			public Method(LuaClass owner, string methodName)
			{
				this.owner = owner;
				this.function = this.owner.prototype.Table.Get(methodName);
			}

			// Token: 0x06003B3F RID: 15167 RVA: 0x00027F40 File Offset: 0x00026140
			public Method(LuaClass owner, DynValue function)
			{
				this.owner = owner;
				this.function = function;
			}

			// Token: 0x06003B40 RID: 15168 RVA: 0x00027F56 File Offset: 0x00026156
			public bool IsNil()
			{
				return this.function.IsNil();
			}

			// Token: 0x06003B41 RID: 15169 RVA: 0x00027F63 File Offset: 0x00026163
			public static implicit operator bool(LuaClass.Method self)
			{
				return self != null && !self.IsNil();
			}

			// Token: 0x06003B42 RID: 15170 RVA: 0x0012C83C File Offset: 0x0012AA3C
			public DynValue Call()
			{
				DynValue result = DynValue.Nil;
				if (this.function.IsNotNil())
				{
					result = this.owner.engine.Call(this.function, new DynValue[]
					{
						this.owner.self
					});
				}
				return result;
			}

			// Token: 0x06003B43 RID: 15171 RVA: 0x0012C888 File Offset: 0x0012AA88
			public DynValue Call(params DynValue[] args)
			{
				DynValue result = DynValue.Nil;
				if (this.function.IsNotNil())
				{
					DynValue[] array = new DynValue[(args == null) ? 1 : (1 + args.Length)];
					array[0] = this.owner.self;
					if (args != null)
					{
						Array.Copy(args, 0, array, 1, args.Length);
					}
					result = this.owner.engine.Call(this.function, array);
				}
				return result;
			}

			// Token: 0x06003B44 RID: 15172 RVA: 0x0012C8F0 File Offset: 0x0012AAF0
			public DynValue Call(params object[] args)
			{
				DynValue result = DynValue.Nil;
				if (this.function.IsNotNil())
				{
					result = this.Call(this.owner.engine.CreateDynValues(args));
				}
				return result;
			}

			// Token: 0x0400306C RID: 12396
			private readonly LuaClass owner;

			// Token: 0x0400306D RID: 12397
			private readonly DynValue function;
		}
	}
}
