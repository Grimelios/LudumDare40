using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare40
{
	public static class ContentLoader
	{
		private static ContentManager content;
		
		public static void Initialize(ContentManager contentManager)
		{
			content = contentManager;
		}
		
		public static Effect LoadEffect(string filename)
		{
			return content.Load<Effect>("Effects/" + filename);
		}
		
		public static SpriteFont LoadFont(string filename)
		{
			return content.Load<SpriteFont>("Fonts/" + filename);
		}

		public static Texture2D LoadTexture(string filename)
		{
			return content.Load<Texture2D>("Textures/" + filename);
		}
	}
}
