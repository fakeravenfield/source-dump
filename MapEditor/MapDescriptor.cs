using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005DC RID: 1500
	public static class MapDescriptor
	{
		// Token: 0x060026C4 RID: 9924 RVA: 0x000F55F4 File Offset: 0x000F37F4
		public static string GetFilePathToSave(string mapName)
		{
			if (mapName == null)
			{
				throw new ArgumentNullException("mapName");
			}
			mapName = mapName.Trim(new char[]
			{
				' ',
				'\t',
				'.'
			});
			foreach (char oldChar in Path.GetInvalidPathChars())
			{
				mapName = mapName.Replace(oldChar, '-');
			}
			if (string.IsNullOrEmpty(mapName))
			{
				throw new ArgumentException("Map name is empty (or contains only spaces.)", "mapName");
			}
			string path = mapName + ".rfld";
			return Path.Combine(MapDescriptor.DATA_PATH, path);
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000F567C File Offset: 0x000F387C
		public static void SaveScene(string filePath, MapDescriptorSettings settings)
		{
			string directoryName = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryName))
			{
				Debug.Log("Creating directory: " + directoryName);
				Directory.CreateDirectory(directoryName);
			}
			Debug.Log("Saving map descriptor: " + filePath);
			string contents = JsonUtility.ToJson(DescriptorConstructorV1.ConstructFromScene(settings), false);
			File.WriteAllText(filePath, contents, Encoding.UTF8);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0001AD3B File Offset: 0x00018F3B
		public static void SaveScene(string filePath)
		{
			MapDescriptor.SaveScene(filePath, MapDescriptorSettings.Defaults);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000F56D8 File Offset: 0x000F38D8
		public static MapDescriptorInfo[] FindMapDescriptors(bool includeAutosaves = true)
		{
			List<MapDescriptorInfo> list = new List<MapDescriptorInfo>();
			foreach (string text in MapDescriptor.FindMapDescriptorsFast(includeAutosaves))
			{
				try
				{
					MapDescriptorDataHeader mapDescriptorDataHeader = MapDescriptor.ParseHeader(File.ReadAllText(text, Encoding.UTF8));
					if (includeAutosaves || !mapDescriptorDataHeader.isAutosave)
					{
						MapDescriptorInfo item = new MapDescriptorInfo(text, mapDescriptorDataHeader);
						list.Add(item);
					}
				}
				catch (Exception ex)
				{
					Debug.LogErrorFormat("Unable to parse '{0}' while enumerating descriptor files. {1}", new object[]
					{
						text,
						ex
					});
				}
			}
			return list.ToArray();
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000F576C File Offset: 0x000F396C
		public static string[] FindMapDescriptorsFast(bool includeAutosaves = true)
		{
			List<string> list = new List<string>();
			if (Directory.Exists(MapDescriptor.DATA_PATH))
			{
				List<string> list2 = new List<string>();
				list2.AddRange(Directory.GetFiles(MapDescriptor.DATA_PATH, "*.rfld"));
				list2.AddRange(Directory.GetFiles(MapDescriptor.DATA_PATH, "*.json"));
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in list2)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					if (!hashSet.Contains(fileNameWithoutExtension))
					{
						hashSet.Add(fileNameWithoutExtension);
						if (includeAutosaves || !fileNameWithoutExtension.StartsWith("Autosave #"))
						{
							list.Add(text);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000F5838 File Offset: 0x000F3A38
		public static string GetFilePathToLoad(string mapName)
		{
			string filePathToSave = MapDescriptor.GetFilePathToSave(mapName);
			if (!File.Exists(filePathToSave))
			{
				string text = Path.ChangeExtension(filePathToSave, ".json");
				if (File.Exists(text))
				{
					return text;
				}
			}
			return filePathToSave;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000F586C File Offset: 0x000F3A6C
		public static MapDescriptor.ParseResult LoadFile(string filePath)
		{
			Debug.Log("Loading map descriptor: " + filePath);
			MapDescriptor.ParseResult result;
			try
			{
				result = MapDescriptor.ParseDescriptor(File.ReadAllText(filePath, Encoding.UTF8));
			}
			catch (Exception ex)
			{
				Debug.LogErrorFormat("Unable to read map descriptor file: {0}. An exception occured: {1}", new object[]
				{
					filePath,
					ex
				});
				throw;
			}
			return result;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0001AD48 File Offset: 0x00018F48
		private static MapDescriptorDataHeader ParseHeader(string json)
		{
			MapDescriptorDataHeader mapDescriptorDataHeader = JsonUtility.FromJson<MapDescriptorDataHeader>(json);
			if (mapDescriptorDataHeader == null)
			{
				throw new MapDescriptorException.NoHeader();
			}
			return mapDescriptorDataHeader;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000F58C8 File Offset: 0x000F3AC8
		private static MapDescriptor.ParseResult ParseDescriptor(string json)
		{
			MapDescriptorDataHeader mapDescriptorDataHeader = MapDescriptor.ParseHeader(json);
			if (mapDescriptorDataHeader.version >= 1 || mapDescriptorDataHeader.version <= 3)
			{
				return MapDescriptor.ParseDescriptorV1(json, mapDescriptorDataHeader);
			}
			throw new MapDescriptorException.UnsupportedVersion(mapDescriptorDataHeader.version);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000F5904 File Offset: 0x000F3B04
		private static MapDescriptor.ParseResult ParseDescriptorV1(string json, MapDescriptorDataHeader header)
		{
			MapDescriptorDataV1 mapDescriptorDataV = null;
			try
			{
				mapDescriptorDataV = JsonUtility.FromJson<MapDescriptorDataV1>(json);
			}
			catch (Exception inner)
			{
				throw new MapDescriptorException.InvalidFormat(header.version, inner);
			}
			if (mapDescriptorDataV == null)
			{
				throw new MapDescriptorException.InvalidFormat(header.version, null);
			}
			mapDescriptorDataV.PostProcessDeserialization();
			return new MapDescriptor.ParseResult(new EditorSceneConstructorV1(mapDescriptorDataV), new GameSceneConstructorV1(mapDescriptorDataV));
		}

		// Token: 0x04002506 RID: 9478
		public const string FILE_EXTENSION = ".rfld";

		// Token: 0x04002507 RID: 9479
		private const string LEGACY_EXTENSION = ".json";

		// Token: 0x04002508 RID: 9480
		public const string AUTOSAVE_PREFIX = "Autosave #";

		// Token: 0x04002509 RID: 9481
		private const bool PRETTY_PRINT = false;

		// Token: 0x0400250A RID: 9482
		public static readonly string DATA_PATH = Application.persistentDataPath + "/MapDescriptors";

		// Token: 0x020005DD RID: 1501
		public struct ParseResult
		{
			// Token: 0x060026CF RID: 9935 RVA: 0x0001AD6F File Offset: 0x00018F6F
			public ParseResult(ISceneConstructor editorConstructor, ISceneConstructor gameConstructor)
			{
				this.editorConstructor = editorConstructor;
				this.gameConstructor = gameConstructor;
			}

			// Token: 0x0400250B RID: 9483
			public readonly ISceneConstructor editorConstructor;

			// Token: 0x0400250C RID: 9484
			public readonly ISceneConstructor gameConstructor;
		}
	}
}
