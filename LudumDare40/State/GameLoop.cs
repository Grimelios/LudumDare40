using LudumDare40.Interfaces;

namespace LudumDare40.State
{
	public abstract class GameLoop : IDynamic, IRenderable
	{
		public abstract void Initialize(Camera camera);
		public abstract void Update(float dt);
		public abstract void Draw(SuperBatch sb);
	}
}
