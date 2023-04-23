using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ravenfield.Dialog
{
	// Token: 0x020003D6 RID: 982
	public class SpriteDialogActor : BaseDialogActor
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x000A5280 File Offset: 0x000A3480
		private void Awake()
		{
			this.image = base.GetComponent<Image>();
			this.aspectRatio = base.GetComponent<AspectRatioFitter>();
			this.mouthOverlay = this.CreateOverlay("Mouth Overlay");
			this.eyesOverlay = this.CreateOverlay("Eyes Overlay");
			this.overlay = this.CreateOverlay("Overlay");
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00012D14 File Offset: 0x00010F14
		public void SetActiveDatabases(SpriteActorDatabase[] spriteDatabases)
		{
			this.spriteDatabases = spriteDatabases;
			this.ApplyPose(SpriteActorDatabase.GetDefaultSpriteFromDatabase(this.spriteDatabases));
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00012D2E File Offset: 0x00010F2E
		private Image CreateOverlay(string name)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.SetParent(this.image.rectTransform, false);
			Image image = gameObject.AddComponent<Image>();
			image.enabled = false;
			image.rectTransform.anchoredPosition = Vector2.zero;
			return image;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000A52D8 File Offset: 0x000A34D8
		public override void TriggerPose(string name)
		{
			this.Show();
			SpriteActorDatabase.Pose spritePoseFromDatabase = SpriteActorDatabase.GetSpritePoseFromDatabase(this.spriteDatabases, name);
			if (spritePoseFromDatabase != null)
			{
				this.ApplyPose(spritePoseFromDatabase);
			}
			base.TriggerPose(name);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00012D69 File Offset: 0x00010F69
		public void Hide()
		{
			this.image.enabled = false;
			this.overlay.enabled = false;
			this.eyesOverlay.enabled = false;
			this.mouthOverlay.enabled = false;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00012D9B File Offset: 0x00010F9B
		public void Show()
		{
			this.image.enabled = true;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000A530C File Offset: 0x000A350C
		private void ApplyPose(SpriteActorDatabase.Pose pose)
		{
			this.pose = pose;
			this.SetSprite(this.pose.baseSprite, this.pose.sourceDatabase.sourceTextureMaxHeight);
			this.blipSound = pose.blipSound;
			this.SetDefaultEyes();
			this.SetMouthIdleFrame();
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000A535C File Offset: 0x000A355C
		public override void NextTalkFrame()
		{
			base.NextTalkFrame();
			if (this.pose.HasTalkFrames())
			{
				Sprite randomTalkFrame = this.pose.GetRandomTalkFrame();
				this.SetOverlayImage(this.mouthOverlay, randomTalkFrame);
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00012DA9 File Offset: 0x00010FA9
		public override void Blink()
		{
			base.Blink();
			if (this.pose != null && this.pose.HasBlinkFrames())
			{
				base.StartCoroutine(this.BlinkRoutine());
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00012DD3 File Offset: 0x00010FD3
		public IEnumerator BlinkRoutine()
		{
			int num;
			for (int i = 1; i < this.pose.blinkOverlaySprites.Length; i = num + 1)
			{
				this.SetOverlayImage(this.eyesOverlay, this.pose.blinkOverlaySprites[i]);
				yield return new WaitForSeconds(0.05f);
				num = i;
			}
			this.SetDefaultEyes();
			yield break;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00012DE2 File Offset: 0x00010FE2
		public override void SetMouthIdleFrame()
		{
			base.SetMouthIdleFrame();
			if (this.pose.firstTalkFrameIsIdle)
			{
				this.SetOverlayImage(this.mouthOverlay, this.pose.talkOverlaySprites[0]);
				return;
			}
			this.mouthOverlay.enabled = false;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00012E1D File Offset: 0x0001101D
		public void SetDefaultEyes()
		{
			if (this.pose.HasBlinkFrames())
			{
				this.SetOverlayImage(this.eyesOverlay, this.pose.blinkOverlaySprites[0]);
				return;
			}
			this.eyesOverlay.enabled = false;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x000A5398 File Offset: 0x000A3598
		public void SetSprite(Sprite sprite, int sourceTextureMaxHeight)
		{
			this.image.sprite = sprite;
			float y = this.image.sprite.rect.height / (float)sourceTextureMaxHeight;
			this.image.rectTransform.anchorMax = new Vector2(1f, y);
			if (this.aspectRatio != null)
			{
				this.aspectRatio.aspectRatio = this.image.sprite.rect.width / this.image.sprite.rect.height;
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000A5434 File Offset: 0x000A3634
		public void SetOverlayImage(Image image, Sprite sprite)
		{
			if (sprite == null)
			{
				image.enabled = false;
				return;
			}
			image.enabled = true;
			image.sprite = sprite;
			float num = this.image.rectTransform.rect.height / this.image.sprite.rect.height;
			Vector2 normalizedPivot = this.GetNormalizedPivot(this.image.sprite);
			image.rectTransform.anchorMin = normalizedPivot;
			image.rectTransform.anchorMax = normalizedPivot;
			image.rectTransform.pivot = this.GetNormalizedPivot(image.sprite);
			image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, image.sprite.rect.width * num);
			image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, image.sprite.rect.height * num);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x000A5518 File Offset: 0x000A3718
		public Vector2 GetNormalizedPivot(Sprite sprite)
		{
			return new Vector2(sprite.pivot.x / sprite.rect.width, sprite.pivot.y / sprite.rect.height);
		}

		// Token: 0x04001A3C RID: 6716
		private const float BLINK_FRAME_TIME = 0.05f;

		// Token: 0x04001A3D RID: 6717
		[NonSerialized]
		public SpriteActorDatabase.Pose pose;

		// Token: 0x04001A3E RID: 6718
		private Image image;

		// Token: 0x04001A3F RID: 6719
		private Image mouthOverlay;

		// Token: 0x04001A40 RID: 6720
		private Image eyesOverlay;

		// Token: 0x04001A41 RID: 6721
		private Image overlay;

		// Token: 0x04001A42 RID: 6722
		private AspectRatioFitter aspectRatio;

		// Token: 0x04001A43 RID: 6723
		private SpriteActorDatabase[] spriteDatabases;
	}
}
