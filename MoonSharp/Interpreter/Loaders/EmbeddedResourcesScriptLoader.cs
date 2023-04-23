using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000821 RID: 2081
	public class EmbeddedResourcesScriptLoader : ScriptLoaderBase
	{
		// Token: 0x060033CC RID: 13260 RVA: 0x00116E64 File Offset: 0x00115064
		public EmbeddedResourcesScriptLoader(Assembly resourceAssembly = null)
		{
			if (resourceAssembly == null)
			{
				resourceAssembly = Assembly.GetCallingAssembly();
			}
			this.m_ResourceAssembly = resourceAssembly;
			this.m_Namespace = this.m_ResourceAssembly.FullName.Split(new char[]
			{
				','
			}).First<string>();
			this.m_ResourceNames = new HashSet<string>(this.m_ResourceAssembly.GetManifestResourceNames());
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x00023801 File Offset: 0x00021A01
		private string FileNameToResource(string file)
		{
			file = file.Replace('/', '.');
			file = file.Replace('\\', '.');
			return this.m_Namespace + "." + file;
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0002382C File Offset: 0x00021A2C
		public override bool ScriptFileExists(string name)
		{
			name = this.FileNameToResource(name);
			return this.m_ResourceNames.Contains(name);
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x00023843 File Offset: 0x00021A43
		public override object LoadFile(string file, Table globalContext)
		{
			file = this.FileNameToResource(file);
			return this.m_ResourceAssembly.GetManifestResourceStream(file);
		}

		// Token: 0x04002D89 RID: 11657
		private Assembly m_ResourceAssembly;

		// Token: 0x04002D8A RID: 11658
		private HashSet<string> m_ResourceNames;

		// Token: 0x04002D8B RID: 11659
		private string m_Namespace;
	}
}
