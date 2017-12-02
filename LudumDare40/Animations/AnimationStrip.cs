using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare40.Animations
{
	public class AnimationStrip
	{
		public AnimationStrip(string name, int frameWidth, int frameHeight, string frames, int frameRate)
		{
			Name = name;
			FrameWidth = frameWidth;
			FrameHeight = frameHeight;
			FrameRate = frameRate;

			// Frames are listed as ranges (i.e. 1-5).
			string[] tokens = frames.Split('-');

			int start = int.Parse(tokens[0]);
			int end = int.Parse(tokens[1]);

			FrameStart = start;
			FrameCount = end - start;
		}

		public string Name { get; }

		public int FrameWidth { get; }
		public int FrameHeight { get; }
		public int FrameStart { get; }
		public int FrameCount { get; }
		public int FrameRate { get; }
	}
}
