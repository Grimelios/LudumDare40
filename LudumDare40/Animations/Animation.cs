using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Json;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40.Animations
{
	public class Animation
	{
		private AnimationStrip[] strips;
		private Texture2D texture;

		private int[] frameYArray;

		public Animation(string texture, AnimationStrip[] strips)
		{
			this.texture = ContentLoader.LoadTexture("Animations/" + texture);
			this.strips = strips;

			frameYArray = new int[strips.Length];

			int frameY = 0;

			for (int i = 0; i < strips.Length; i++)
			{
				// This assumes that each strip is on its own line.
				frameYArray[i] = frameY;
				frameY += strips[i].FrameHeight;
			}
		}

		public AnimationPlayer Play(string animation)
		{
			for (int i = 0; i < strips.Length; i++)
			{
				AnimationStrip strip = strips[i];

				if (strip.Name == animation)
				{
					return new AnimationPlayer(strip, texture, frameYArray[i]);
				}
			}
			
			return null;
		}
	}
}
