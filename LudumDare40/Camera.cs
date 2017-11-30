using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Entities.Core;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;

namespace LudumDare40
{
	public class Camera : IPositionable, IRotatable, IDynamic
	{
		public Camera()
		{
			Origin = new Vector2(Resolution.Width, Resolution.Height) / 2;
			Transform = Matrix.Identity;
		}
		
		public Vector2 Position { get; set; }
		public Vector2 Origin { get; set; }
		
		public float Rotation { get; set; }

		public Matrix Transform { get; private set; }
		public Entity Target { get; set; }
		
		public void Update(float dt)
		{
			if (Target != null)
			{
				Position = CoreFunctions.Integerize(Target.Position);
			}

			Transform =
				Matrix.CreateTranslation(new Vector3(Origin - Position, 0)) *
				Matrix.CreateRotationZ(Rotation);
		}
	}
}
