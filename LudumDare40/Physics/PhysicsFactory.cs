using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using LudumDare40.Entities.Core;
using LudumDare40.Shapes;
using Microsoft.Xna.Framework;

namespace LudumDare40.Physics
{
	public enum Units
	{
		Pixels,
		Meters
	}
	
	public enum PhysicsGroups
	{
	}
	
	public static class PhysicsFactory
	{
		private static World world;
		
		public static void Initialize(World w)
		{
			world = w;
		}
		
		public static Body CreateBody(Entity entity)
		{
			Body body = BodyFactory.CreateBody(world);
			body.UserData = entity;

			return body;
		}

		public static Body CreateCircle(float radius, Units units, BodyType bodyType, Entity entity)
		{
			return CreateCircle(radius, Vector2.Zero, units, bodyType, entity);
		}

		public static Body CreateCircle(float radius, Vector2 position, Units units, BodyType bodyType, Entity entity)
		{
			if (units == Units.Pixels)
			{
				radius = PhysicsConvert.ToMeters(radius);
			}

			Body body = BodyFactory.CreateCircle(world, radius, 1);
			body.BodyType = bodyType;
			body.UserData = entity;

			return body;
		}
		
		public static Body CreateRectangle(float width, float height, Units units, BodyType bodyType, Entity entity)
		{
			return CreateRectangle(width, height, Vector2.Zero, units, bodyType, entity);
		}

		public static Body CreateRectangle(float width, float height, Vector2 position, Units units, BodyType bodyType, Entity entity)
		{
			if (units == Units.Pixels)
			{
				width = PhysicsConvert.ToMeters(width);
				height = PhysicsConvert.ToMeters(height);
				position = PhysicsConvert.ToMeters(position);
			}

			Body body = BodyFactory.CreateRectangle(world, width, height, 1, position);
			body.BodyType = bodyType;
			body.UserData = entity;

			return body;
		}
		
		public static Fixture AttachEdge(Body body, Line line, Units units, object userData = null)
		{
			Vector2 start = line.Start;
			Vector2 end = line.End;

			if (units == Units.Pixels)
			{
				start = PhysicsConvert.ToMeters(start);
				end = PhysicsConvert.ToMeters(end);
			}

			Fixture fixture = FixtureFactory.AttachEdge(start, end, body);
			fixture.UserData = userData;

			return fixture;
		}
	}
}
