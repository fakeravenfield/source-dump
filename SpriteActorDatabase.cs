using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[CreateAssetMenu(fileName = "SpriteActorDatabase", menuName = "Dialog/Sprite Actor Database")]
public class SpriteActorDatabase : ScriptableObject
{
	// Token: 0x0600062E RID: 1582 RVA: 0x0005ED18 File Offset: 0x0005CF18
	public void InitializeRuntimeData()
	{
		if (this.isRuntimeDatabaseInitialized)
		{
			return;
		}
		foreach (SpriteActorDatabase.Pose pose in this.poses)
		{
			pose.sourceDatabase = this;
			pose.lowercaseNameHash = pose.name.ToLowerInvariant().GetHashCode();
		}
		this.isRuntimeDatabaseInitialized = true;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0005ED6C File Offset: 0x0005CF6C
	public SpriteActorDatabase.Pose GetPose(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			foreach (SpriteActorDatabase.Pose pose in this.poses)
			{
				Debug.Log("SpriteActorDatabase: Trying " + pose.name);
				if (pose.name == name)
				{
					Debug.Log("SpriteActorDatabase: hash matched!");
					return pose;
				}
			}
		}
		Debug.Log("SpriteActorDatabase: string is NULL or no hash matched!");
		return null;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00005DFA File Offset: 0x00003FFA
	public SpriteActorDatabase.Pose DefaultPose()
	{
		return this.poses[0];
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0005EDD4 File Offset: 0x0005CFD4
	public static SpriteActorDatabase.Pose GetDefaultSpriteFromDatabase(SpriteActorDatabase[] databases)
	{
		if (databases != null && databases.Length != 0)
		{
			return databases[0].DefaultPose();
		}
		try
		{
			return GameManager.instance.defaultSpriteActorDatabases[0].DefaultPose();
		}
		catch (Exception)
		{
		}
		return null;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00005E04 File Offset: 0x00004004
	public static SpriteActorDatabase.Pose GetSpritePose(string name)
	{
		return SpriteActorDatabase.GetSpritePoseFromDefaultDatabase(name);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00005E0C File Offset: 0x0000400C
	public static SpriteActorDatabase.Pose GetSpritePoseFromDefaultDatabase(string name)
	{
		return SpriteActorDatabase.GetSpritePoseFromDatabase(GameManager.instance.defaultSpriteActorDatabases, name);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0005EE1C File Offset: 0x0005D01C
	public static SpriteActorDatabase.Pose GetSpritePoseFromDatabase(SpriteActorDatabase[] databases, string name)
	{
		SpriteActorDatabase.Pose spritePoseFromDatabasesNoFallback = SpriteActorDatabase.GetSpritePoseFromDatabasesNoFallback(databases, name);
		if (spritePoseFromDatabasesNoFallback != null)
		{
			return spritePoseFromDatabasesNoFallback;
		}
		try
		{
			spritePoseFromDatabasesNoFallback = SpriteActorDatabase.GetSpritePoseFromDatabasesNoFallback(GameManager.instance.defaultSpriteActorDatabases, name);
			if (spritePoseFromDatabasesNoFallback != null)
			{
				return spritePoseFromDatabasesNoFallback;
			}
		}
		catch (Exception)
		{
		}
		return SpriteActorDatabase.GetDefaultSpriteFromDatabase(databases);
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0005EE6C File Offset: 0x0005D06C
	private static SpriteActorDatabase.Pose GetSpritePoseFromDatabasesNoFallback(SpriteActorDatabase[] databases, string name)
	{
		if (databases == null)
		{
			return null;
		}
		for (int i = 0; i < databases.Length; i++)
		{
			SpriteActorDatabase.Pose pose = databases[i].GetPose(name);
			if (pose != null)
			{
				return pose;
			}
		}
		return null;
	}

	// Token: 0x0400060F RID: 1551
	public int sourceTextureMaxHeight = 2048;

	// Token: 0x04000610 RID: 1552
	public SpriteActorDatabase.Pose[] poses;

	// Token: 0x04000611 RID: 1553
	[NonSerialized]
	private bool isRuntimeDatabaseInitialized;

	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class Pose
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x00005E31 File Offset: 0x00004031
		public bool HasTalkFrames()
		{
			return this.talkOverlaySprites.Length != 0;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0005EEA0 File Offset: 0x0005D0A0
		public Sprite GetRandomTalkFrame()
		{
			if (this.talkOverlaySprites.Length == 0)
			{
				return null;
			}
			if (this.talkOverlaySprites.Length == 1)
			{
				return this.talkOverlaySprites[0];
			}
			Sprite result;
			try
			{
				int num = UnityEngine.Random.Range(this.firstTalkFrameIsIdle ? 1 : 0, this.talkOverlaySprites.Length);
				if (num == this.lastTalkFrameIndex)
				{
					num = (num + 1) % this.talkOverlaySprites.Length;
					if (this.firstTalkFrameIsIdle && num == 0)
					{
						num = 1;
					}
				}
				this.lastTalkFrameIndex = num;
				result = this.talkOverlaySprites[num];
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00005E3D File Offset: 0x0000403D
		public bool HasBlinkFrames()
		{
			return this.blinkOverlaySprites.Length != 0;
		}

		// Token: 0x04000612 RID: 1554
		public string name;

		// Token: 0x04000613 RID: 1555
		public string defaultDisplayName = "???";

		// Token: 0x04000614 RID: 1556
		public bool firstTalkFrameIsIdle;

		// Token: 0x04000615 RID: 1557
		public Sprite baseSprite;

		// Token: 0x04000616 RID: 1558
		public Sprite[] talkOverlaySprites;

		// Token: 0x04000617 RID: 1559
		public Sprite[] blinkOverlaySprites;

		// Token: 0x04000618 RID: 1560
		public SpriteActorDatabase.PoseOverlay[] overlays;

		// Token: 0x04000619 RID: 1561
		public AudioClip blipSound;

		// Token: 0x0400061A RID: 1562
		[NonSerialized]
		private int lastTalkFrameIndex = -1;

		// Token: 0x0400061B RID: 1563
		[NonSerialized]
		public SpriteActorDatabase sourceDatabase;

		// Token: 0x0400061C RID: 1564
		[NonSerialized]
		public int lowercaseNameHash;
	}

	// Token: 0x020000CA RID: 202
	[Serializable]
	public class PoseOverlay
	{
		// Token: 0x0400061D RID: 1565
		public string name;

		// Token: 0x0400061E RID: 1566
		public Sprite sprite;
	}

	// Token: 0x020000CB RID: 203
	[Serializable]
	public class PoseSequence
	{
		// Token: 0x0400061F RID: 1567
		public string name;

		// Token: 0x04000620 RID: 1568
		public int loopTimes;

		// Token: 0x04000621 RID: 1569
		public int loopBegin;

		// Token: 0x04000622 RID: 1570
		public int loopEnd;

		// Token: 0x04000623 RID: 1571
		public SpriteActorDatabase.PoseSequenceFrame[] frames;
	}

	// Token: 0x020000CC RID: 204
	[Serializable]
	public struct PoseSequenceFrame
	{
		// Token: 0x04000624 RID: 1572
		public string triggerPose;

		// Token: 0x04000625 RID: 1573
		public float wait;
	}
}
