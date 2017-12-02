using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;

namespace LudumDare40.Entities.Core
{
	// All living entities are assumed to animate themselves.
	public abstract class LivingEntity : AnimatedEntity, ITargetable
	{
		public delegate void HealthEventHandler(int value, int previousValue);
		public delegate void DeathEventHandler(LivingEntity entity);

		private int health;
		private int maxHealth;
		
		protected LivingEntity(EntityTypes type) : base(type)
		{
		}
		
		public event HealthEventHandler OnHealthChange;
		public event HealthEventHandler OnMaxHealthChange;
		public event DeathEventHandler OnDeath;
		
		public virtual int Health
		{
			get { return health; }
			set
			{
				if (health != value)
				{
					int previousValue = health;

					health = value;
					OnHealthChange?.Invoke(value, previousValue);
				}
			}
		}
		
		public virtual int MaxHealth
		{
			get { return maxHealth; }
			set
			{
				if (maxHealth != value)
				{
					int previousValue = maxHealth;

					maxHealth = value;
					OnMaxHealthChange?.Invoke(value, previousValue);
				}
			}
		}
		
		public virtual void RegisterHit(int damage, int knockback, Vector2 direction, Entity source)
		{
			// Knockback can be applied even with zero damage.
			Body?.ApplyLinearImpulse(knockback * direction);

			if (damage == 0)
			{
				return;
			}

			int newHealth = health - damage;

            // Both OnHealthChange and OnDeath can be called on the same frame.
		    if (newHealth <= 0)
		    {
		        Health = 0;
		        OnDeath?.Invoke(this);
		    }
		    else
		    {
		        Health = newHealth;
		    }
		}
		
		public void Kill()
		{
			Health = 0;
			OnDeath?.Invoke(this);
		}
	}
}
