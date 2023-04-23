using System;

namespace MapEditor
{
	// Token: 0x020005DF RID: 1503
	public class MapDescriptorException : Exception
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x0001AD7F File Offset: 0x00018F7F
		public MapDescriptorException(string message, Exception inner = null) : base(message, inner)
		{
		}

		// Token: 0x020005E0 RID: 1504
		public class NoHeader : MapDescriptorException
		{
			// Token: 0x060026D2 RID: 9938 RVA: 0x0001AD89 File Offset: 0x00018F89
			public NoHeader() : base("No header found in map descriptor. Is it a valid JSON file?", null)
			{
			}
		}

		// Token: 0x020005E1 RID: 1505
		public class UnsupportedVersion : MapDescriptorException
		{
			// Token: 0x060026D3 RID: 9939 RVA: 0x0001AD97 File Offset: 0x00018F97
			public UnsupportedVersion(int version) : base("Map descriptor version " + version.ToString() + " is not supported.", null)
			{
			}
		}

		// Token: 0x020005E2 RID: 1506
		public class InvalidFormat : MapDescriptorException
		{
			// Token: 0x060026D4 RID: 9940 RVA: 0x0001ADB6 File Offset: 0x00018FB6
			public InvalidFormat(int version, Exception inner = null) : base("JSON does not represent a valid map descriptor of version " + version.ToString() + ".", inner)
			{
			}
		}

		// Token: 0x020005E3 RID: 1507
		public class InvalidTerrainData : MapDescriptorException
		{
			// Token: 0x060026D5 RID: 9941 RVA: 0x0001ADD5 File Offset: 0x00018FD5
			public InvalidTerrainData(string message, Exception inner = null) : base(message, inner)
			{
			}
		}

		// Token: 0x020005E4 RID: 1508
		public class InvalidPathfinding : MapDescriptorException
		{
			// Token: 0x060026D6 RID: 9942 RVA: 0x0001ADD5 File Offset: 0x00018FD5
			public InvalidPathfinding(string message, Exception inner = null) : base(message, inner)
			{
			}
		}

		// Token: 0x020005E5 RID: 1509
		public class InvalidCoverPoints : MapDescriptorException
		{
			// Token: 0x060026D7 RID: 9943 RVA: 0x0001ADD5 File Offset: 0x00018FD5
			public InvalidCoverPoints(string message, Exception inner = null) : base(message, inner)
			{
			}
		}
	}
}
