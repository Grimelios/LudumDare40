using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using LudumDare40.Core;
using LudumDare40.Interfaces;
using LudumDare40.Physics;
using Microsoft.Xna.Framework;

namespace LudumDare40.Entities.Core
{
	public enum EntityTypes
	{
		World,

		// This type should be used for entities not directly managed by the scene.
		None
	}

	public abstract class Entity : IPositionable, IRotatable, IBoundable, IDynamic, IRenderable, IDisposable
	{
		private Vector2 position;

		private float rotation;

		private Body body;
		private List<Component2D> components;
		private List<Timer> timers;

		private bool selfUpdate;
		
		protected Entity(EntityTypes type = EntityTypes.None)
		{
			EntityType = type;
			Bounds = new Bounds();
		}
		
		protected List<Component2D> Components => components ?? (components = new List<Component2D>());
		protected List<Timer> Timers => timers ?? (timers = new List<Timer>());
		
		protected bool UseCustomComponentPositioning { get; set; }
		protected bool OnGround { get; set; }
		
		public virtual Vector2 Position
		{
			get { return position; }
			set
			{
				PreviousPosition = position;
				position = value;

				if (Body != null && !selfUpdate)
				{
					Body.Position = PhysicsConvert.ToMeters(value);
				}

				if (!UseCustomComponentPositioning)
				{
					components?.ForEach(c => c.Position = value);
				}
			}
		}
		
		public Vector2 PreviousPosition { get; private set; }
		
		public Vector2 Velocity
		{
			get { return PhysicsConvert.ToPixels(Body.LinearVelocity); }
			set { Body.LinearVelocity = PhysicsConvert.ToMeters(value); }
		}
		
		public virtual float Rotation
		{
			get { return rotation; }
			set
			{
				rotation = value;

				if (Body != null && !selfUpdate)
				{
					Body.Rotation = value;
				}

				components?.ForEach(c => c.Rotation = value);
			}
		}
		
		public Bounds Bounds { get; protected set; }
		
		public Body Body
		{
			get { return body; }
			set
			{
				body = value;
				body.OnCollision += (fixtureA, fixtureB, contact) =>
				{
					Manifold manifold = contact.Manifold;

					return OnCollision(fixtureB.Body.UserData as Entity, fixtureB.UserData, manifold.LocalPoint, manifold.LocalNormal);
				};
			}
		}
		
		public Scene Scene { get; set; }
		
		public EntityTypes EntityType { get; }
		
	    protected virtual bool OnCollision(Entity entity, object fixtureData, Vector2 position, Vector2 normal)
	    {
	        return false;
	    }
		
        public virtual void Dispose()
		{
		}
		
		public virtual void Update(float dt)
		{
			if (Body != null)
			{
				if (OnGround)
				{
					Body.Position = PhysicsConvert.ToMeters(Position);
					Body.Rotation = Rotation;
				}
				else
				{
					selfUpdate = true;
					Position = PhysicsConvert.ToPixels(Body.Position);
					Rotation = Body.Rotation;
					selfUpdate = false;
				}
			}

			timers?.ForEach(t => t.Update(dt));
			components?.ForEach(c => c.Update(dt));
		}
		
		public virtual void Draw(SuperBatch sb)
		{
			components?.ForEach(c => c.Draw(sb));
		}
	}
}
