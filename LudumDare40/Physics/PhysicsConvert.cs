using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LudumDare40.Physics
{
	public static class PhysicsConvert
	{
		private const int PixelsPerMeter = 48;

		public static float ToPixels(float value)
		{
			return value * PixelsPerMeter;
		}
		
		public static float ToMeters(float value)
		{
			return value / PixelsPerMeter;
		}
		
		public static Vector2 ToPixels(Vector2 value)
		{
			return value * PixelsPerMeter;
		}
		
		public static Vector2 ToMeters(Vector2 value)
		{
			return value / PixelsPerMeter;
		}
	}
}
