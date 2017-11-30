using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using LudumDare40.Entities.Core;
using Microsoft.Xna.Framework;

namespace LudumDare40.Physics
{
	public static class PhysicsUtilities
	{
		private static World world;
		
		public static void Initialize(World w)
		{
			world = w;
		}
		
		public static RayCastResults RayCast(Vector2 start, Vector2 end, Units units)
		{
			if (units == Units.Pixels)
			{
				start = PhysicsConvert.ToMeters(start);
				end = PhysicsConvert.ToMeters(end);
			}

			Vector2 hitPosition = Vector2.Zero;
			Fixture hitFixture = null;

			float closestFraction = float.PositiveInfinity;

			world.RayCast((fixture, point, normal, fraction) =>
			{
				if (fraction < closestFraction)
				{
					hitPosition = point;
					hitFixture = fixture;
					closestFraction = fraction;

					return fraction;
				}

				return 1;
			}, start, end);

			if (hitFixture == null)
			{
				return null;
			}

			hitPosition = PhysicsConvert.ToPixels(hitPosition);

			return new RayCastResults(hitPosition, (Entity)hitFixture.Body.UserData);
		}
	}
}
