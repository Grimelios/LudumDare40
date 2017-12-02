using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare40.Entities.Enemies
{
	public class Archer : Enemy
	{
		public Archer() : base(EnemyTypes.Archer)
		{
		}

		protected override void AI(float dt)
		{
		}
	}
}
