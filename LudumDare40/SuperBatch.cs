using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40
{
	public enum Coordinates
	{
		Screen,
		World
	}
	
	public class SuperBatch
	{
		private Camera camera;
		private SpriteBatch sb;
		private GraphicsDevice graphicsDevice;
		
		public SuperBatch(SpriteBatch sb, GraphicsDevice graphicsDevice, Camera camera)
		{
			this.sb = sb;
			this.camera = camera;
			this.graphicsDevice = graphicsDevice;
		}
		
		public void Begin(Coordinates coordinates, Effect effect = null)
		{
			sb.Begin(SpriteSortMode.Deferred, null, null, null, null, effect, coordinates == Coordinates.World
				? camera.Transform
				: Matrix.Identity);
		}
		
		public void SetRenderTarget(RenderTarget2D renderTarget)
		{
			graphicsDevice.SetRenderTarget(renderTarget);

			if (renderTarget != null)
			{
				graphicsDevice.Clear(Color.Transparent);
			}
		}
		
		public void End()
		{
			sb.End();
		}

		public void Draw(Texture2D texture, Vector2 position, Color color)
		{
			sb.Draw(texture, position, null, color);
		}

		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRect)
		{
			sb.Draw(texture, position, sourceRect, Color.White);
		}

		public void Draw(Texture2D texture, Rectangle destinationRect, Color color)
		{
			sb.Draw(texture, destinationRect, color);
		}

		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation)
		{
			sb.Draw(texture, position, sourceRect, Color.White, rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
		}

		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin,
			float scale, SpriteEffects effects)
		{
			sb.Draw(texture, position, sourceRect, color, rotation, origin, scale, effects, 0);
		}

		public void DrawString(SpriteFont font, string value, Vector2 position, Color color, float rotation, Vector2 origin, float scale)
		{
			sb.DrawString(font, value, position, color, rotation, origin, scale, SpriteEffects.None, 0);
		}
	}
}
