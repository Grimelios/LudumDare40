using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Shapes;

namespace LudumDare40.Entities
{
	public class TilemapData
	{
        public TilemapData(int width, int height, int[,] tiles, int tileSize, string tilesheet, Edge[][] edges)
		{
			Width = width;
			Height = height;
			Tiles = tiles;
			TileSize = tileSize;
			Tilesheet = tilesheet;
			Edges = edges;
		}
		
		public int Width { get; }
		public int Height { get; }
		public int[,] Tiles { get; }
		public int TileSize { get; }
		
		public string Tilesheet { get; }
		
		public Edge[][] Edges { get; }
	}
}
