using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LudumDare40.Shapes
{
	public class Edge
	{
		public Edge(Vector2 start, Vector2 end)
		{
			Start = start;
			End = end;
		}
		
		public Vector2 Start { get; set; }
		public Vector2 End { get; set; }
	}
}
