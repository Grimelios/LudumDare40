using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40.Core
{
	public class SpriteText : Component2D
	{
		private SpriteFont font;
		private Vector2 origin;

		private string value;

		private Alignments alignment;
		
		public SpriteText(string font, string value) :
			this(ContentLoader.LoadFont(font), value, Alignments.Left | Alignments.Top)
		{
		}
		
		public SpriteText(string font, string value, Alignments alignment) :
			this(ContentLoader.LoadFont(font), value, alignment)
		{
		}
		
		public SpriteText(SpriteFont font, string value) :
			this(font, value, Alignments.Left | Alignments.Top)
		{
		}
		
		public SpriteText(SpriteFont font, string value, Alignments alignment)
		{
			this.font = font;
			this.alignment = alignment;

			Value = value;
			Color = Color.White;
		}
		
		public string Value
		{
			get { return value; }
			set
			{
				this.value = value;

				if (value != null)
				{
					origin = CoreFunctions.ComputeOrigin(alignment, font.MeasureString(value));
				}
			}
		}
		
		public int Width => Value == null ? 0 : (int)font.MeasureString(Value).X;
		public int Height => Value == null ? 0 : (int)font.MeasureString(Value).Y;

		public Vector2 Dimensions => Value == null ? Vector2.Zero : font.MeasureString(Value);
		
		public override void Draw(SuperBatch sb)
		{
			sb.DrawString(font, Value, CoreFunctions.Integerize(Position), Color, Rotation, origin, Scale);
		}
	}
}
