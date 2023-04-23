using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000213 RID: 531
public class FolderModContent
{
	// Token: 0x06000E07 RID: 3591 RVA: 0x0000B448 File Offset: 0x00009648
	public FolderModContent(string path)
	{
		this.path = path;
		this.scroll = Vector2.zero;
		this.Scan();
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0000B468 File Offset: 0x00009668
	public bool HasIconImage()
	{
		return this.iconFile != null;
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0007E450 File Offset: 0x0007C650
	private void GetFilesInDirectoryRecursively(string path, List<string> files)
	{
		files.AddRange(Directory.GetFiles(path));
		foreach (string text in Directory.GetDirectories(path))
		{
			this.GetFilesInDirectoryRecursively(text, files);
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0007E48C File Offset: 0x0007C68C
	public void Scan()
	{
		List<string> list = new List<string>();
		try
		{
			this.GetFilesInDirectoryRecursively(this.path, list);
		}
		catch
		{
		}
		this.updated = new Dictionary<FileInfo, bool>();
		this.allContent = new List<FileInfo>();
		this.disallowedFiles = new List<FileInfo>();
		this.totalContentModSizeBytes = 0L;
		foreach (string fileName in list)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (this.IsContentExtension(fileInfo.Extension))
			{
				this.allContent.Add(fileInfo);
			}
			else if (this.IsIconFile(fileInfo))
			{
				this.iconFile = fileInfo;
			}
			else if (!this.IsAllowedExtension(fileInfo.Extension))
			{
				this.disallowedFiles.Add(fileInfo);
			}
			if (this.IsGameContent(fileInfo))
			{
				this.totalContentModSizeBytes += fileInfo.Length;
			}
		}
		this.allContent.Sort(new Comparison<FileInfo>(this.SortByName));
		foreach (FileInfo key in this.allContent)
		{
			this.updated.Add(key, false);
		}
		this.scroll = Vector2.zero;
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0000B473 File Offset: 0x00009673
	private bool IsIconFile(FileInfo file)
	{
		return file.Name == "icon.png";
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0007E5F8 File Offset: 0x0007C7F8
	public bool IsContentExtension(string extension)
	{
		extension = extension.ToLower();
		for (int i = 0; i < FolderModContent.CONTENT_EXTENSIONS.Length; i++)
		{
			if (extension == FolderModContent.CONTENT_EXTENSIONS[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x0007E634 File Offset: 0x0007C834
	public bool IsAllowedExtension(string extension)
	{
		for (int i = 0; i < FolderModContent.ALLOWED_EXTENSIONS.Length; i++)
		{
			if (extension == FolderModContent.ALLOWED_EXTENSIONS[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x0000B485 File Offset: 0x00009685
	public bool ContainsFileOfName(string name)
	{
		return this.GetFileOfName(name) != null;
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x0007E668 File Offset: 0x0007C868
	public FileInfo GetFileOfName(string name)
	{
		foreach (FileInfo fileInfo in this.allContent)
		{
			if (fileInfo.Name == name)
			{
				return fileInfo;
			}
		}
		return null;
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x0000B491 File Offset: 0x00009691
	public bool IsEmpty()
	{
		return this.allContent.Count == 0;
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x0007E6CC File Offset: 0x0007C8CC
	public void MarkAsUpdated(FileInfo file, FileInfo newFile)
	{
		int index = this.allContent.IndexOf(file);
		this.allContent[index] = newFile;
		this.updated[newFile] = true;
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x0007E700 File Offset: 0x0007C900
	public void MarkAsUpdated(string name)
	{
		FileInfo fileOfName = this.GetFileOfName(name);
		this.MarkAsUpdated(fileOfName, fileOfName);
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x0000B4A1 File Offset: 0x000096A1
	public bool IsMarkedAsUpdated(FileInfo file)
	{
		return this.updated.ContainsKey(file) && this.updated[file];
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0000B4BF File Offset: 0x000096BF
	public bool HasAnyContent()
	{
		return this.allContent.Count > 0;
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0000B4CF File Offset: 0x000096CF
	public bool HasDisallowedFiles()
	{
		return this.disallowedFiles.Count > 0;
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0007E720 File Offset: 0x0007C920
	public bool HasNewerVersionOfFile(FileInfo file, out FileInfo newerFile)
	{
		newerFile = null;
		foreach (FileInfo fileInfo in this.allContent)
		{
			if (fileInfo.Name == file.Name)
			{
				if (fileInfo.LastWriteTimeUtc > file.LastWriteTimeUtc)
				{
					newerFile = fileInfo;
					return true;
				}
				return false;
			}
		}
		return false;
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0000B4DF File Offset: 0x000096DF
	private bool IsMap(FileInfo file)
	{
		return this.IsLevelDescriptor(file) || file.Extension.ToLower() == ".rfl";
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x0000B501 File Offset: 0x00009701
	private bool IsGameContent(FileInfo file)
	{
		return file.Extension.ToLower() == ".rfc";
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x0000B518 File Offset: 0x00009718
	private bool IsGameConfiguration(FileInfo file)
	{
		return file.Extension.ToLower() == ".rgc";
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0000B52F File Offset: 0x0000972F
	private bool IsLevelDescriptor(FileInfo file)
	{
		return file.Extension.ToLower() == ".rfld" && !file.Name.StartsWith("Autosave #");
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0007E7A4 File Offset: 0x0007C9A4
	public List<FileInfo> GetMaps()
	{
		List<FileInfo> list = new List<FileInfo>();
		foreach (FileInfo fileInfo in this.allContent)
		{
			if (this.IsMap(fileInfo))
			{
				list.Add(fileInfo);
			}
		}
		return list;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0007E808 File Offset: 0x0007CA08
	public List<FileInfo> GetGameContent()
	{
		List<FileInfo> list = new List<FileInfo>();
		foreach (FileInfo fileInfo in this.allContent)
		{
			if (this.IsGameContent(fileInfo))
			{
				list.Add(fileInfo);
			}
		}
		return list;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0007E86C File Offset: 0x0007CA6C
	public List<FileInfo> GetGameConfiguration()
	{
		List<FileInfo> list = new List<FileInfo>();
		foreach (FileInfo fileInfo in this.allContent)
		{
			if (this.IsGameConfiguration(fileInfo))
			{
				list.Add(fileInfo);
			}
		}
		return list;
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x0007E8D0 File Offset: 0x0007CAD0
	public List<FileInfo> GetLevelDescriptor()
	{
		List<FileInfo> list = new List<FileInfo>();
		foreach (FileInfo fileInfo in this.allContent)
		{
			if (this.IsLevelDescriptor(fileInfo))
			{
				list.Add(fileInfo);
			}
		}
		return list;
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x0000B55D File Offset: 0x0000975D
	public bool HasGameContent()
	{
		return this.GetGameContent().Count > 0;
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0000B56D File Offset: 0x0000976D
	public bool HasLevelDescriptor()
	{
		return this.GetLevelDescriptor().Count > 0;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0000B57D File Offset: 0x0000977D
	private int SortByName(FileInfo x, FileInfo y)
	{
		return x.Name.CompareTo(y.Name);
	}

	// Token: 0x04000F00 RID: 3840
	public const string ICON_FILE_NAME = "icon.png";

	// Token: 0x04000F01 RID: 3841
	public static readonly string[] CONTENT_EXTENSIONS = new string[]
	{
		".rfl",
		".rfc",
		".rgc",
		".rfld"
	};

	// Token: 0x04000F02 RID: 3842
	public static readonly string[] ALLOWED_EXTENSIONS = new string[]
	{
		".txt",
		".png",
		".json"
	};

	// Token: 0x04000F03 RID: 3843
	private string path;

	// Token: 0x04000F04 RID: 3844
	public List<FileInfo> allContent;

	// Token: 0x04000F05 RID: 3845
	public List<FileInfo> disallowedFiles;

	// Token: 0x04000F06 RID: 3846
	public FileInfo iconFile;

	// Token: 0x04000F07 RID: 3847
	public Dictionary<FileInfo, bool> updated;

	// Token: 0x04000F08 RID: 3848
	public Vector2 scroll;

	// Token: 0x04000F09 RID: 3849
	public long totalContentModSizeBytes;
}
