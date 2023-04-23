using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityStandardAssets.ImageEffects;

// Token: 0x020000D4 RID: 212
public class RuntimePortraitGenerator : MonoBehaviour
{
	// Token: 0x06000676 RID: 1654 RVA: 0x0005F55C File Offset: 0x0005D75C
	private void Awake()
	{
		RuntimePortraitGenerator.instance = this;
		this.portraitSprites = new Sprite[2];
		this.camera.enabled = false;
		this.camera.forceIntoRenderTexture = true;
		this.camera.useOcclusionCulling = false;
		this.camera.allowMSAA = false;
		this.renderTexture = new RenderTexture(512, 512, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		this.renderTexture.useMipMap = true;
		this.renderTexture.autoGenerateMips = true;
		this.renderTexture.Create();
		for (int i = 0; i < 2; i++)
		{
			this.portraitSprites[i] = this.InitializeSprite(RuntimePortraitGenerator.POSE_NAMES[i]);
		}
		this.rootObject.SetActive(true);
		this.portraitLights = base.GetComponentsInChildren<Light>();
		this.rootObject.SetActive(false);
		this.defaultCameraLocalPosition = RuntimePortraitGenerator.instance.camera.transform.localPosition;
		this.defaultCameraParentRotation = this.cameraParent.localRotation;
		if (!false)
		{
			PostEffectsBase[] components = this.camera.GetComponents<PostEffectsBase>();
			for (int j = 0; j < components.Length; j++)
			{
				components[j].enabled = false;
			}
		}
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0005F680 File Offset: 0x0005D880
	private Sprite InitializeSprite(string poseName)
	{
		Sprite sprite = Sprite.Create(new Texture2D(512, 512, TextureFormat.ARGB32, true, true), new Rect(0f, 0f, 512f, 512f), Vector2.zero);
		this.runtimeDatabase.GetPose(poseName).baseSprite = sprite;
		return sprite;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0005F6D8 File Offset: 0x0005D8D8
	public static void ApplyOffset(Vector3 position, Quaternion rotation)
	{
		RuntimePortraitGenerator.instance.camera.transform.localPosition = RuntimePortraitGenerator.instance.defaultCameraLocalPosition + position;
		RuntimePortraitGenerator.instance.cameraParent.localRotation = RuntimePortraitGenerator.instance.defaultCameraParentRotation * rotation;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0000613D File Offset: 0x0000433D
	public static void ResetOffset()
	{
		RuntimePortraitGenerator.instance.camera.transform.localPosition = RuntimePortraitGenerator.instance.defaultCameraLocalPosition;
		RuntimePortraitGenerator.instance.cameraParent.localRotation = RuntimePortraitGenerator.instance.defaultCameraParentRotation;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00006176 File Offset: 0x00004376
	public static void RenderDatabaseTeamPortrait(int team)
	{
		RuntimePortraitGenerator.Render(RuntimePortraitGenerator.instance.portraitSprites[team].texture, null, team);
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00006190 File Offset: 0x00004390
	private static Texture CreatePortraitTexture()
	{
		return new Texture2D(512, 512, TextureFormat.ARGB32, true, false);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x000061A4 File Offset: 0x000043A4
	public static Texture RenderTeamPortrait(int team)
	{
		Texture texture = RuntimePortraitGenerator.CreatePortraitTexture();
		RuntimePortraitGenerator.Render(texture, null, team);
		return texture;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x000061B3 File Offset: 0x000043B3
	public static Texture RenderSkinPortrait(ActorSkin.MeshSkin overrideSkin, int team)
	{
		Texture texture = RuntimePortraitGenerator.CreatePortraitTexture();
		RuntimePortraitGenerator.Render(texture, overrideSkin, team);
		return texture;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0005F728 File Offset: 0x0005D928
	public static void Render(Texture destinationTexture, ActorSkin.MeshSkin overrideSkin, int team)
	{
		Light[] array = RuntimePortraitGenerator.instance.portraitLights;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].cullingMask = int.MinValue;
		}
		AmbientMode ambientMode = RenderSettings.ambientMode;
		Color ambientLight = RenderSettings.ambientLight;
		RenderSettings.ambientMode = AmbientMode.Flat;
		RenderSettings.ambientLight = new Color(0.6f, 0.6f, 0.6f, 1f);
		RuntimePortraitGenerator.instance.teamColorLight.color = ColorScheme.TeamColorBrighter(1 - team);
		RuntimePortraitGenerator.instance.rootObject.SetActive(true);
		RuntimePortraitGenerator.instance.camera.targetTexture = RuntimePortraitGenerator.instance.renderTexture;
		if (overrideSkin == null)
		{
			ActorManager.ApplyGlobalTeamSkin(RuntimePortraitGenerator.instance.actorRenderer, team);
		}
		else
		{
			ActorManager.ApplyOverrideMeshSkin(RuntimePortraitGenerator.instance.actorRenderer, overrideSkin, team);
		}
		RuntimePortraitGenerator.instance.camera.Render();
		Graphics.CopyTexture(RuntimePortraitGenerator.instance.renderTexture, destinationTexture);
		RenderSettings.ambientLight = ambientLight;
		RenderSettings.ambientMode = ambientMode;
		RuntimePortraitGenerator.instance.rootObject.SetActive(false);
		RuntimePortraitGenerator.ResetOffset();
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0005F830 File Offset: 0x0005DA30
	private void OnDisable()
	{
		for (int i = 0; i < 2; i++)
		{
			this.runtimeDatabase.GetPose(RuntimePortraitGenerator.POSE_NAMES[i]).baseSprite = null;
		}
	}

	// Token: 0x04000655 RID: 1621
	private const int RENDER_LAYER = 31;

	// Token: 0x04000656 RID: 1622
	private const int RENDER_MASK = -2147483648;

	// Token: 0x04000657 RID: 1623
	private const int DIMENSIONS = 512;

	// Token: 0x04000658 RID: 1624
	public const int SCENE_CULL_MASK = 2147483647;

	// Token: 0x04000659 RID: 1625
	private const float RENDER_AMBIENT_LIGHT = 0.6f;

	// Token: 0x0400065A RID: 1626
	public static readonly string[] POSE_NAMES = new string[]
	{
		"p runtime blue",
		"p runtime red"
	};

	// Token: 0x0400065B RID: 1627
	public static RuntimePortraitGenerator instance;

	// Token: 0x0400065C RID: 1628
	public Camera camera;

	// Token: 0x0400065D RID: 1629
	public SkinnedMeshRenderer actorRenderer;

	// Token: 0x0400065E RID: 1630
	public SpriteActorDatabase runtimeDatabase;

	// Token: 0x0400065F RID: 1631
	public GameObject rootObject;

	// Token: 0x04000660 RID: 1632
	public RenderTexture renderTexture;

	// Token: 0x04000661 RID: 1633
	public Light teamColorLight;

	// Token: 0x04000662 RID: 1634
	public Transform cameraParent;

	// Token: 0x04000663 RID: 1635
	private Sprite[] portraitSprites;

	// Token: 0x04000664 RID: 1636
	private Light[] portraitLights;

	// Token: 0x04000665 RID: 1637
	private Vector3 defaultCameraLocalPosition;

	// Token: 0x04000666 RID: 1638
	private Quaternion defaultCameraParentRotation;
}
