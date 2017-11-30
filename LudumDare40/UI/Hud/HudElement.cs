using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Core;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace LudumDare40.UI.Hud
{
	public abstract class HudElement : Container2D
	{
		protected HudElement()
		{
			KeyboardEnabled = false;
			MouseEnabled = false;
		}
		
		[JsonProperty]
		public Alignments Alignment { get; set; }
		
		[JsonProperty]
		public int OffsetX { get; set; }
		
		[JsonProperty]
		public int OffsetY { get; set; }
		
		public virtual void Initialize()
		{
		}
	}
}
