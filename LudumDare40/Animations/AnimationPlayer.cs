using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Core;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40.Animations
{
	public class AnimationPlayer : Component2D
	{
		private AnimationStrip strip;
		private Texture2D texture;
		private Vector2 origin;
		private Rectangle sourceRect;
		
		private int currentFrame;

		private float frameElapsed;
		private float frameDuration;

		public AnimationPlayer(AnimationStrip strip, Texture2D texture, int frameY)
		{
			this.strip = strip;
			this.texture = texture;

			int width = strip.FrameWidth;
			int height = strip.FrameHeight;

			// Every frame in an animation is assumed to have the same dimensions.
			sourceRect = new Rectangle(0, frameY, width, height);

			// Animation frames are assumed centered.
			origin = new Vector2(width, height) / 2;

			// Each frame is also assumed to have the same duration.
			frameDuration = strip.FrameRate;
			Color = Color.White;
			Scale = 1;
		}

		public bool FlipHorizontally { get; set; }

		public override void Update(float dt)
		{
			frameElapsed += dt * 1000;

			if (frameElapsed >= frameDuration)
			{
				currentFrame = currentFrame == strip.FrameCount - 1 ? 0 : ++currentFrame;
				sourceRect.X = currentFrame * strip.FrameWidth;
				frameElapsed -= frameDuration;
			}
		}

		public override void Draw(SuperBatch sb)
		{
			SpriteEffects effects = FlipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			sb.Draw(texture, Position, sourceRect, Color, Rotation, origin, Scale, effects);
		}
	}
}
