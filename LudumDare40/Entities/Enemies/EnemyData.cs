using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LudumDare40.Entities.Enemies
{
	public class EnemyData
	{
		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		public string Animations { get; set; }

		[JsonProperty]
		public int Health { get; set; }

		[JsonProperty]
		public int HitboxWidth { get; set; }

		[JsonProperty]
		public int HitboxHeight { get; set; }
	}
}
