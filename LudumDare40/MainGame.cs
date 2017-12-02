using System;
using LudumDare40.Input;
using LudumDare40.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare40
{
	[Flags]
	public enum Alignments
	{
		Left = 1 << 0,
		Right = 1 << 1,
		Top = 1 << 2,
		Bottom = 1 << 3,
		Center = 0
	}

	public class MainGame : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private SuperBatch superBatch;
		private InputGenerator inputGenerator;
		private Camera camera;
		private GameLoop gameLoop;

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
			ContentLoader.Initialize(Content);
			Primitives.Initialize(GraphicsDevice);

			camera = new Camera();
			inputGenerator = new InputGenerator(camera);
			gameLoop = new GameplayLoop();
			gameLoop.Initialize(camera);

			MessageSystem.ProcessChanges();

			base.Initialize();
		}
		
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			superBatch = new SuperBatch(spriteBatch, GraphicsDevice, camera);
		}
		
		protected override void UnloadContent()
		{
		}
		
		protected override void Update(GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

			inputGenerator.GenerateEvents();
			camera.Update(dt);
			gameLoop.Update(dt);

			MessageSystem.ProcessChanges();
		}
		
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			gameLoop.Draw(superBatch);
		}
	}
}
