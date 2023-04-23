using System;
using System.Linq;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020008FE RID: 2302
	internal class FrameworkCurrent : FrameworkClrBase
	{
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000091D3 File Offset: 0x000073D3
		public override Type GetTypeInfoFromType(Type t)
		{
			return t;
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x00027918 File Offset: 0x00025B18
		public override bool IsDbNull(object o)
		{
			return o != null && Convert.IsDBNull(o);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x00027925 File Offset: 0x00025B25
		public override bool StringContainsChar(string str, char chr)
		{
			return str.Contains(chr);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0002792E File Offset: 0x00025B2E
		public override Type GetInterface(Type type, string name)
		{
			return type.GetInterface(name);
		}
	}
}
