using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare40
{
	public class MainGame : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = Resolution.Width,
				PreferredBackBufferHeight = Resolution.Height
			};

			Content.RootDirectory = "Content";
			Window.Title = "Ludum Dare 40";
			Window.Position = new Point(150);
			IsMouseVisible = true;
		}
		
		protected override void Initialize()
		{
		}
		
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}
		
		protected override void UnloadContent()
		{
		}
		
		protected override void Update(GameTime gameTime)
		{
		}
		
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
		}
	}
}
