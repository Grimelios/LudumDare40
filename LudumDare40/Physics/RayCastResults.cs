using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Entities.Core;
using Microsoft.Xna.Framework;

namespace LudumDare40.Physics
{
	public class RayCastResults
	{
		public RayCastResults(Vector2 position, Entity entity)
		{
			Position = position;
			Entity = entity;
		}
		
		public Vector2 Position { get; }
		public Entity Entity { get; }
	}
}
