using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;
using LudumDare40.Json;
using Microsoft.Xna.Framework;

namespace LudumDare40.UI.Hud
{
	public class HeadsUpDisplay : IDynamic, IRenderable
	{
		private List<HudElement> elements;
		
		public HeadsUpDisplay()
		{
			elements = new List<HudElement>();
		}
		
		public void Load(string filename)
		{
			elements.Clear();
			elements.AddRange(JsonUtilities.Deserialize<HudElement[]>(filename, true));

			foreach (HudElement element in elements)
			{
				Alignments alignment = element.Alignment;

				int offsetX = element.OffsetX;
				int offsetY = element.OffsetY;
				int x = (alignment & Alignments.Left) == Alignments.Left
					? offsetX
					: (alignment & Alignments.Right) == Alignments.Right
						? Resolution.Width - offsetX
						: Resolution.Width / 2 + offsetX;

				int y = (alignment & Alignments.Top) == Alignments.Top
					? offsetY
					: (alignment & Alignments.Bottom) == Alignments.Bottom
						? Resolution.Height - offsetY
						: Resolution.Height / 2 + offsetY;

				element.Position = new Vector2(x, y);
				element.Initialize();
			}
		}
		
		public T GetElement<T>() where T : HudElement
		{
			return (T)elements.FirstOrDefault(e => e is T);
		}
		
		public void Update(float dt)
		{
			elements.ForEach(e =>
			{
				if (e.Visible)
				{
					e.Update(dt);
				}
			});
		}
		
		public void Draw(SuperBatch sb)
		{
			elements.ForEach(e =>
			{
				if (e.Visible)
				{
					e.Draw(sb);
				}
			});
		}
	}
}
