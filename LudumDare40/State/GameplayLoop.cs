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
		private Scene scene;

		public override void Initialize(Camera camera)
		{
			scene = new Scene();

			Player player = new Player();
			player.Position = new Vector2(200);

			scene.Add(0, player);
		}
		
		public override void Update(float dt)
		{
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
