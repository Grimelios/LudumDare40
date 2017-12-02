using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Entities.Core;
using LudumDare40.Json;

namespace LudumDare40.Entities.Enemies
{
	public enum EnemyTypes
	{
		Archer
	}

	public abstract class Enemy : LivingEntity
	{
		private static EnemyData[] dataArray = JsonUtilities.Deserialize<EnemyData[]>("Enemies.json");

		protected Enemy(EnemyTypes enemyType) : base(EntityTypes.Enemy)
		{
			Initialize(dataArray[(int)enemyType]);
		}

		protected virtual void Initialize(EnemyData data)
		{
			// All enemies are assumed to spawn at full health.
			MaxHealth = data.Health;
			Health = MaxHealth;

			LoadAnimations(data.Animations);

			// Every enemy is assumed to have an idle animation.
			PlayAnimation("Idle");
		}

		protected abstract void AI(float dt);

		public override void Update(float dt)
		{
			AI(dt);

			base.Update(dt);
		}
	}
}
