using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;

namespace LudumDare40.Core
{
	[DebuggerDisplay("{X}, {Y}, {Width}, {Height}")]
	public class Bounds
	{
		public static Bounds Enclose<T>(IEnumerable<T> items) where T : IBoundable
		{
			int x = int.MaxValue;
			int y = int.MaxValue;
			int width = 0;
			int height = 0;

			foreach (T item in items)
			{
				Bounds bounds = item.Bounds;

				x = MathHelper.Min(x, bounds.X);
				y = MathHelper.Min(y, bounds.Y);
				width = MathHelper.Max(width, bounds.Right - x);
				height = MathHelper.Max(height, bounds.Bottom - y);
			}

			return new Bounds(x, y, width, height);
		}
		
		public Bounds()
		{
		}
		
		public Bounds(int width, int height) :
			this(0, 0, width, height)
		{
		}
		
		public Bounds(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
		
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		
		public Point Position
		{
			get { return new Point(X, Y); }
			set
			{
				X = value.X;
				Y = value.Y;
			}
		}
		
		public Point Center
		{
			get { return new Point(X + Width / 2, Y + Height / 2); }
			set
			{
				X = value.X - Width / 2;
				Y = value.Y - Height / 2;
			}
		}
		
		public int Left
		{
			get { return X; }
			set { X = value; }
		}
		
		public int Right
		{
			get { return X + Width - 1; }
			set { X = value - Width + 1; }
		}
		
		public int Top
		{
			get { return Y; }
			set { Y = value; }
		}

		public int Bottom
		{
			get { return Y + Height - 1; }
			set { Y = value - Height + 1; }
		}
		
		public void Expand(int amount)
		{
			X -= amount;
			Y -= amount;
			Width += amount * 2;
			Height += amount * 2;
		}
		
		public bool Contains(Point point)
		{
			return Contains(point.ToVector2());
		}
		
		public bool Contains(Vector2 position)
		{
			return position.X >= Left && position.X <= Right && position.Y >= Top && position.Y <= Bottom;
		}
		
		public bool Overlaps(Bounds other)
		{
			int dX = Math.Abs(Center.X - other.Center.X);
			int dY = Math.Abs(Center.Y - other.Center.Y);

			return dX < (Width + other.Width) / 2 && dY < (Height + other.Height) / 2;
		}
		
		public Rectangle ToRectangle()
		{
			return new Rectangle(X, Y, Width, Height);
		}
	}
}
