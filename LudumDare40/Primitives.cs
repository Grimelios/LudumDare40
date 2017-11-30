using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40
{
	public static class Primitives
	{
		private static Texture2D pixel;
		private static GraphicsDevice graphicsDevice;
		
		public static void Initialize(GraphicsDevice device)
		{
			graphicsDevice = device;
			pixel = ContentLoader.LoadTexture("Pixel");
		}
		
		public static void DrawPoint(SuperBatch sb, Vector2 point, Color color)
		{
			sb.Draw(pixel, point, color);
		}
		
		public static void DrawLine(SuperBatch sb, Vector2 start, Vector2 end, Color color, int thickness = 1)
		{
			float length = Vector2.Distance(start, end);
			float rotation = CoreFunctions.ComputeAngle(start, end);

			Rectangle sourceRect = new Rectangle(0, 0, (int)length, 1);

			sb.Draw(pixel, start, sourceRect, color, rotation);
		}

		public static void DrawBounds(SuperBatch sb, Bounds bounds, Color color, int thickness = 1)
		{
			Bounds topBounds = new Bounds(bounds.Left, bounds.Top, bounds.Width, thickness);
			Bounds bottomBounds = new Bounds(bounds.Left, bounds.Bottom - thickness + 1, bounds.Width, thickness);
			Bounds leftBounds = new Bounds(bounds.Left, topBounds.Bottom + 1, thickness, bounds.Height - thickness * 2);
			Bounds rightBounds = new Bounds(bounds.Right - thickness + 1, topBounds.Bottom + 1, thickness, leftBounds.Height);

			FillBounds(sb, topBounds, color);
			FillBounds(sb, bottomBounds, color);
			FillBounds(sb, leftBounds, color);
			FillBounds(sb, rightBounds, color);
		}
		
		public static void FillBounds(SuperBatch sb, Bounds bounds, Color color)
		{
			sb.Draw(pixel, bounds.ToRectangle(), color);
		}
		
		public static void DrawPrimitives<T>(PrimitiveType primitiveType, T[] vertices) where T : struct, IVertexType
		{
			int primitiveCount = 0;

			switch (primitiveType)
			{
				case PrimitiveType.LineList: primitiveCount = vertices.Length / 2; break;
				case PrimitiveType.LineStrip: primitiveCount = vertices.Length - 1; break;
				case PrimitiveType.TriangleList: primitiveCount = vertices.Length / 3; break;
				case PrimitiveType.TriangleStrip: primitiveCount = vertices.Length - 2; break;
			}

			DrawPrimitives(primitiveType, vertices, primitiveCount);
		}

		public static void DrawPrimitives<T>(PrimitiveType primitiveType, T[] vertices, int primitiveCount) where T :
			struct, IVertexType
		{
			graphicsDevice.RasterizerState = RasterizerState.CullNone;
			graphicsDevice.DrawUserPrimitives(primitiveType, vertices, 0, primitiveCount);
		}
	}
}
