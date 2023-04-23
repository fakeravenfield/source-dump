using System;
using Pathfinding.Serialization;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200076B RID: 1899
	[Serializable]
	public class AstarDataV1
	{
		// Token: 0x06002EE9 RID: 12009 RVA: 0x0010A3A8 File Offset: 0x001085A8
		public AstarDataV1(AstarPath astarPath)
		{
			SerializeSettings settings = new SerializeSettings
			{
				nodes = true
			};
			byte[] bytes = astarPath.data.SerializeGraphs(settings);
			this.data = UtilsIO.Base64Encode(bytes);
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x0010A3E4 File Offset: 0x001085E4
		public void CopyTo(AstarPath astarPath)
		{
			try
			{
				if (string.IsNullOrEmpty(this.data))
				{
					throw new Exception("No bytes found in Base64 encoded data string.");
				}
				byte[] bytes = UtilsIO.Base64Decode(this.data);
				astarPath.data.DeserializeGraphs(bytes);
			}
			catch (Exception inner)
			{
				throw new MapDescriptorException.InvalidPathfinding("Unable to decompress or deserialize AstarPath data from descriptor.", inner);
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000204C1 File Offset: 0x0001E6C1
		public static bool IsNullOrEmpty(AstarDataV1 astar)
		{
			return astar == null || string.IsNullOrEmpty(astar.data);
		}

		// Token: 0x04002B12 RID: 11026
		public string data;
	}
}
