using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Json;

namespace LudumDare40.Animations
{
	public static class AnimationLoader
	{
		private static Dictionary<string, Animation> cache = new Dictionary<string, Animation>();

		public static Animation Load(string filename)
		{
			Animation animation;

			if (!cache.TryGetValue(filename, out animation))
			{
				animation = JsonUtilities.Deserialize<Animation>("Animations/" + filename);
				cache.Add(filename, animation);
			}

			return animation;
		}
	}
}
