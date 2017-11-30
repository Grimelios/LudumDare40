using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40.Core
{
	public class Sprite : Component2D
	{
		private Texture2D texture;
		private Vector2 origin;
		private Rectangle? sourceRect;

		private Alignments alignment;
		
		public Sprite(string texture) :
			this(ContentLoader.LoadTexture(texture), Alignments.Center)
		{
		}

		public Sprite(Texture2D texture) :
			this(texture, Alignments.Center)
		{
		}
		
		public Sprite(string texture, Alignments alignment) :
			this(ContentLoader.LoadTexture(texture), alignment)
		{
		}
		
		public Sprite(Texture2D texture, Alignments alignment)
		{
			this.texture = texture;
			this.alignment = alignment;

			origin = CoreFunctions.ComputeOrigin(alignment, texture.Width, texture.Height);
		}
		
		public Rectangle? SourceRect
		{
			get { return sourceRect; }
			set
			{
				sourceRect = value;

				if (value != null)
				{
					origin = CoreFunctions.ComputeOrigin(alignment, value.Value.Width, value.Value.Height);
				}
			}
		}
		
		public bool FlipHorizontal { get; set; }
		public bool FlipVertical { get; set; }
		
		public override void Draw(SuperBatch sb)
		{
			SpriteEffects effects = SpriteEffects.None;

			if (FlipHorizontal)
			{
				effects |= SpriteEffects.FlipHorizontally;
			}

			if (FlipVertical)
			{
				effects |= SpriteEffects.FlipVertically;
			}

			sb.Draw(texture, CoreFunctions.Integerize(Position), SourceRect, Color, Rotation, origin, Scale, effects);
		}
	}
}
