using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;

namespace LudumDare40.Core
{
	public class Timer : IDynamic
	{
		// Trigger functions can return false to break a repeating timer early.
		private Action<float> tick;
		private Action<float> nonRepeatingTrigger;
		private Func<float, bool> repeatingTrigger;
		
		public Timer(float duration, Func<float, bool> repeatingTrigger, float initialElapsed = 0) :
			this(duration, null, repeatingTrigger, initialElapsed)
		{
		}
		
		public Timer(float duration, Action<float> tick, Func<float, bool> repeatingTrigger, float initialElapsed = 0)
		{
			this.tick = tick;
			this.repeatingTrigger = repeatingTrigger;

			Duration = duration;
			Elapsed = initialElapsed;
		}
		
		public Timer(float duration, Action<float> nonRepeatingTrigger, float initialElapsed = 0) :
			this(duration, null, nonRepeatingTrigger, initialElapsed)
		{
		}
		
		public Timer(float duration, Action<float> tick, Action<float> nonRepeatingTrigger, float initialElapsed = 0)
		{
			this.tick = tick;
			this.nonRepeatingTrigger = nonRepeatingTrigger;

			Duration = duration;
			Elapsed = initialElapsed;
		}
		
		public float Elapsed { get; private set; }
		public float Duration { get; set; }
		
		public bool Paused { get; set; }
		public bool Complete { get; private set; }
		
		public void Reset()
		{
			Elapsed = 0;
			Complete = false;
		}
		
		public void Update(float dt)
		{
			if (Paused)
			{
				return;
			}

			Elapsed += dt * 1000;

			if (Elapsed >= Duration)
			{
				// This while loop is necessary to properly handle timers with duration less than a frame (i.e. the timer could trigger
				// multiple timers per frame). Note that trigger functions can pause the timer in some cases (like weapon cooldowns).
				while (Elapsed >= Duration && !Paused)
				{
					// Trigger functions can change duration.
					float previousDuration = Duration;
					float leftover = Elapsed - Duration;

					Elapsed -= previousDuration;

					if (nonRepeatingTrigger != null)
					{
						// Calling the tick function here avoids having to handle the final tick in the trigger function.
						tick?.Invoke(1);
						nonRepeatingTrigger(leftover);

						return;
					}
					
					// Repeating trigger functions should return false when the timer should end.
					if (!repeatingTrigger(leftover))
					{
						Complete = true;

						return;
					}
				}
			}
			else
			{
				tick?.Invoke(Elapsed / Duration);
			}
		}
	}
}
