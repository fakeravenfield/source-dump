using System;
using System.IO;

namespace MapEditor
{
	// Token: 0x020005E6 RID: 1510
	public struct MapDescriptorInfo
	{
		// Token: 0x060026D8 RID: 9944 RVA: 0x0001ADDF File Offset: 0x00018FDF
		public MapDescriptorInfo(string filePath, MapDescriptorDataHeader header)
		{
			this.header = header;
			this.filePath = filePath;
			this.fileName = Path.GetFileNameWithoutExtension(filePath);
		}

		// Token: 0x04002510 RID: 9488
		public readonly MapDescriptorDataHeader header;

		// Token: 0x04002511 RID: 9489
		public readonly string filePath;

		// Token: 0x04002512 RID: 9490
		public readonly string fileName;
	}
}
