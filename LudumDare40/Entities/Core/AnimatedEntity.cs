using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Animations;

namespace LudumDare40.Entities.Core
{
	public abstract class AnimatedEntity : Entity
	{
		private Animation animation;
		private AnimationPlayer animationPlayer;

		protected AnimatedEntity(EntityTypes entityType) : base(entityType)
		{
		}

		protected void LoadAnimations(string filename)
		{
			animation = AnimationLoader.Load(filename);
		}

		protected void PlayAnimation(string name)
		{
			if (animationPlayer != null)
			{
				Components.Remove(animationPlayer);
			}

			animationPlayer = animation.Play(name);
			Components.Add(animationPlayer);
		}
	}
}
