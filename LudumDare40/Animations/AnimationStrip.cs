using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare40.Animations
{
	public class AnimationStrip
	{
		public AnimationStrip(string name, int frameWidth, int frameHeight, int frameCount, int frameRate)
		{
			Name = name;
			FrameWidth = frameWidth;
			FrameHeight = frameHeight;
			FrameCount = frameCount;
			FrameRate = frameRate;
		}

		public string Name { get; }

		public int FrameWidth { get; }
		public int FrameHeight { get; }
		public int FrameCount { get; }
		public int FrameRate { get; }
	}
}
