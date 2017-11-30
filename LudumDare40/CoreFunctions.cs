using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40
{
	public static class CoreFunctions
	{
		public static int EnumCount<T>()
		{
			return Enum.GetValues(typeof(T)).Length;
		}
		
		public static float ComputeAngle(Vector2 vector)
		{
			return ComputeAngle(Vector2.Zero, vector);
		}
		
		public static float ComputeAngle(Vector2 start, Vector2 end)
		{
			float dX = end.X - start.X;
			float dY = end.Y - start.Y;

			return (float)Math.Atan2(dY, dX);
		}

		public static Vector2 ComputeDirection(float angle)
		{
			float x = (float)Math.Cos(angle);
			float y = (float)Math.Sin(angle);

			return new Vector2(x, y);
		}

		public static Vector2 ComputeOrigin(Alignments alignment, Vector2 dimensions)
		{
			return ComputeOrigin(alignment, (int)dimensions.X, (int)dimensions.Y);
		}
		
		public static Vector2 ComputeOrigin(Alignments alignment, int width, int height)
		{
			bool left = (alignment & Alignments.Left) > 0;
			bool right = (alignment & Alignments.Right) > 0;
			bool top = (alignment & Alignments.Top) > 0;
			bool bottom = (alignment & Alignments.Bottom) > 0;

			Vector2 origin;
			origin.X = left ? 0 : (right ? width : width / 2);
			origin.Y = top ? 0 : (bottom ? height : height / 2);

			// Since width and height are integers, the origin is guaranteed to contain integer values.
			return origin;
		}

		public static Vector2 Integerize(Vector2 value)
		{
			return new Vector2((int)value.X, (int)value.Y);
		}
		
		public static Vector3 ToHSV(Color color)
		{
			const float SixtyDegrees = MathHelper.Pi / 3;

			// See https://math.stackexchange.com/questions/556341/rgb-to-hsv-color-conversion-algorithm and
			// http://www.rapidtables.com/convert/color/rgb-to-hsv.htm.
			float rPrime = color.R / 255f;
			float gPrime = color.G / 255f;
			float bPrime = color.B / 255f;
			float max = Math.Max(Math.Max(rPrime, gPrime), bPrime);
			float min = Math.Min(Math.Min(rPrime, gPrime), bPrime);
			float delta = max - min;
			float hue = SixtyDegrees;
			float saturation = max == 0 ? 0 : delta / max;
			float value = max;
			
			if (max == rPrime)
			{
				hue *= ((gPrime - bPrime) / delta) % 6;
			}
			else if (max == gPrime)
			{
				hue *= (bPrime - rPrime) / delta + 2;
			}
			else
			{
				hue *= (rPrime - gPrime) / delta + 4;
			}

			return new Vector3(hue, saturation, value);
		}
		
		public static Color ToRGB(Vector3 value)
		{
			const float SixtyDegrees = MathHelper.Pi / 3;

			// See http://www.rapidtables.com/convert/color/hsv-to-rgb.htm.
			float hue = value.X;
			float saturation = value.Y;
			float brightness = value.Z;
			float c = saturation * brightness;
			float x = c * (1 - Math.Abs(hue / SixtyDegrees % 2 - 1));
			float m = brightness - c;

			Vector3 newValue = Vector3.Zero;

			switch ((int)(hue / SixtyDegrees))
			{
				// Hue < 60 degrees.
				case 0:
					newValue.X = c;
					newValue.Y = x;

					break;

				// Hue < 120 degrees.
				case 1:
					newValue.X = x;
					newValue.Y = c;

					break;

				// Hue < 180 degrees.
				case 2:
					newValue.Y = c;
					newValue.Z = x;

					break;

				// Hue < 240 degrees.
				case 3:
					newValue.Y = x;
					newValue.Z = c;

					break;

				// Hue < 300 degrees.
				case 4:
					newValue.X = x;
					newValue.Z = c;

					break;

				// Hue < 360 degrees.
				case 5:
					newValue.X = c;
					newValue.Z = x;

					break;
			}

			return new Color(newValue + new Vector3(m));
		}
		
		public static void PositionItems<T>(IEnumerable<T> items, Vector2 basePosition, Vector2 spacing) where T : class, IPositionable
		{
			int index = 0;

			foreach (T item in items)
			{
				item.Position = basePosition + spacing * index++;
			}
		}
		
		public static string[] WrapLines(SpriteFont font, string value, int width)
		{
			string[] words = value.Split(' ');

			float lineWidth = 0;
			float spaceWidth = font.MeasureString(" ").X;

			List<string> lines = new List<string>();
			StringBuilder builder = new StringBuilder();

			foreach (string word in words)
			{
				float wordWidth = font.MeasureString(word).X;
				float combinedWidth = wordWidth + spaceWidth;

				if (lineWidth + wordWidth > width)
				{
					lines.Add(builder.ToString());
					builder.Clear();
					lineWidth = 0;
				}

				builder.Append(word + " ");
				lineWidth += combinedWidth;
			}

			if (builder.Length > 0)
			{
				lines.Add(builder.ToString());
			}

			return lines.ToArray();
		}
		
		public static string RemoveExtension(string value)
		{
			return value.Substring(0, value.LastIndexOf('.'));
		}
	}
}
