using FarseerPhysics.Dynamics;
using LudumDare40.Core;
using LudumDare40.Entities;
using LudumDare40.Entities.Core;
using LudumDare40.Interfaces;
using LudumDare40.Physics;
using LudumDare40.Shapes;
using LudumDare40.State;
using LudumDare40.UI.Hud;
using Microsoft.Xna.Framework;

namespace LudumDare40.State
{
	public class GameplayLoop : GameLoop
	{
		private const int Gravity = 0;

		private Scene scene;
		private PhysicsAccumulator accumulator;

		public override void Initialize(Camera camera)
		{
			World world = new World(new Vector2(0, Gravity));
			PhysicsFactory.Initialize(world);

			scene = new Scene();
			accumulator = new PhysicsAccumulator(world);

			Player player = new Player();
			player.Position = new Vector2(200);

			scene.Add(0, player);
		}
		
		public override void Update(float dt)
		{
			accumulator.Update(dt);
			scene.Update(dt);
		}
		
		public override void Draw(SuperBatch sb)
		{
			sb.Begin(Coordinates.World);
			scene.Draw(sb);
			sb.End();
		}
	}
}
