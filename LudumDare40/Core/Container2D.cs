using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Input.Data;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;

namespace LudumDare40.Core
{
	public abstract class Container2D : IPositionable, IBoundable, IDynamic, IRenderable, IDisposable
	{
		private Vector2 position;
		private List<Timer> timers;
		
		protected List<Timer> Timers => timers ?? (timers = new List<Timer>());
		
		protected bool Centered { get; set; }

		public bool Visible { get; set; } = true;
		
		public virtual Vector2 Position
		{
			get { return position; }
			set
			{
				Point point = value.ToPoint();

				if (!Centered)
				{
					position = value;
					Bounds.Position = point;
				}
				else
				{
					position = value - new Vector2(Bounds.Width, Bounds.Height) / 2;
					Bounds.Center = point;
				}
			}
		}
		
		public Bounds Bounds { get; protected set; } = new Bounds();
		
		public bool KeyboardEnabled { get; protected set; } = true;
		public bool MouseEnabled { get; protected set; } = true;
		
		public virtual void HandleKeyboard(KeyboardData data)
		{
		}
		
		public virtual void HandleMouse(MouseData data)
		{
		}
		
		public virtual void Dispose()
		{
		}
		
		public virtual void Update(float dt)
		{
			if (timers != null)
			{
				// Timers can remove themselves when they trigger. Looping in reverse allows that to happen safely.
				for (int i = timers.Count - 1; i >= 0; i--)
				{
					timers[i].Update(dt);
				}
			}
		}
		
		public virtual void Draw(SuperBatch sb)
		{
		}
	}
}
