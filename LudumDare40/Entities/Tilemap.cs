using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using LudumDare40.Entities.Core;
using LudumDare40.Json;
using LudumDare40.Physics;
using LudumDare40.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40.Entities
{
	public class Tilemap : Entity
	{
		private Texture2D tilesheet;
		private Body[] bodies;

		private int tilesPerRow;
		
		public Tilemap(string jsonFile) : this(JsonUtilities.Deserialize<TilemapData>("Tilemaps/" + jsonFile))
		{
		}
		
		public Tilemap(TilemapData data) : base(EntityTypes.World)
		{
			Width = data.Width;
			Height = data.Height;
			Tiles = data.Tiles;
			TileSize = data.TileSize;
			tilesheet = ContentLoader.LoadTexture("Tilesheets/" + data.Tilesheet);
			tilesPerRow = tilesheet.Width / TileSize;

			if (data.Edges != null)
			{
				CreateBodies(data.Edges);
			}
		}
		
		public override Vector2 Position
		{
			set
			{
				if (bodies != null)
				{
					Vector2 convertedPosition = PhysicsConvert.ToMeters(value);

					foreach (Body body in bodies)
					{
						body.Position = convertedPosition;
					}
				}

				base.Position = value;
			}
		}
		
		public int Width { get; set; }
		public int Height { get; set; }
		public int[,] Tiles { get; set; }
		public int TileSize { get; set; }
		
		private void CreateBodies(Edge[][] edges)
		{
			bodies = new Body[edges.Length];

			for (int i = 0; i < edges.Length; i++)
			{
				Body body = PhysicsFactory.CreateBody(this);
				Edge[] edgeArray = edges[i];

				foreach (Edge edge in edgeArray)
				{
					PhysicsFactory.AttachEdge(body, edge, Units.Meters, edge);
				}

				body.CollisionCategories = (Category)PhysicsGroups.World;
				bodies[i] = body;
			}
		}
		
		public override void Draw(SuperBatch sb)
		{
			Rectangle sourceRect = new Rectangle(0, 0, TileSize, TileSize);

			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					int tileValue = Tiles[j, i];

					// -1 represents a blank tile.
					if (tileValue > -1)
					{
						sourceRect.X = tileValue % tilesPerRow * TileSize;
						sourceRect.Y = tileValue / tilesPerRow * TileSize;

						sb.Draw(tilesheet, Position + new Vector2(j, i) * TileSize, sourceRect);
					}
				}
			}
		}
	}
}
