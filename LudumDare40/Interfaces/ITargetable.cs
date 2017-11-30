using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Entities.Core;
using Microsoft.Xna.Framework;

namespace LudumDare40.Interfaces
{
	public interface ITargetable
	{
		void RegisterHit(int damage, int knockback, Vector2 direction, Entity source);
	}
}
