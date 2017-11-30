using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using LudumDare40.Interfaces;

namespace LudumDare40.Physics
{
	public class PhysicsAccumulator : IDynamic
	{
		private const float PhysicsStep = 1f / 120;

		private World world;

		private float accumulator;
		
		public PhysicsAccumulator(World world)
		{
			this.world = world;
		}

		public void Update(float dt)
		{
			accumulator += dt;

			while (accumulator >= PhysicsStep)
			{
				world.Step(PhysicsStep);
				accumulator -= PhysicsStep;
			}
		}
	}
}
