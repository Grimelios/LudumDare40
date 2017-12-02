using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare40.Entities
{
	public class PlayerData
	{
		public const int HitboxWidth = 48;
		public const int HitboxHeight = 96;

		public int Acceleration { get; set; } = 2000;
		public int Deceleration { get; set; } = 1500;
		public int MaxSpeed { get; set; } = 300;
	}
}
