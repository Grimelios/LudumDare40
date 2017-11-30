using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;

namespace LudumDare40.Core
{
	public abstract class Component2D : IPositionable, IRotatable, IScalable, IColorable, IDynamic, IRenderable
	{
		protected Component2D()
		{
			Color = Color.White;
			Scale = 1;
		}
		
		public virtual Vector2 Position { get; set; }
		
		public virtual float Rotation { get; set; }
		public virtual float Scale { get; set; }
		
		public virtual Color Color { get; set; }
		
		public virtual void Update(float dt)
		{
		}
		
		public virtual void Draw(SuperBatch sb)
		{
		}
	}
}
