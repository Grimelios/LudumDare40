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

		private bool flipHorizontally;

		protected AnimatedEntity(EntityTypes entityType) : base(entityType)
		{
		}

		protected bool FlipHorizontally
		{
			// After an entity has been created, its animation player should never be null.
			set { animationPlayer.FlipHorizontally = value; }
		}

		protected void LoadAnimations(string filename)
		{
			animation = AnimationLoader.Load(filename);
		}

		protected void PlayAnimation(string name)
		{
			bool flipHorizontally = false;

			if (animationPlayer != null)
			{
				flipHorizontally = animationPlayer.FlipHorizontally;
				Components.Remove(animationPlayer);
			}

			animationPlayer = animation.Play(name);
			animationPlayer.FlipHorizontally = flipHorizontally;

			Components.Add(animationPlayer);
		}
	}
}
