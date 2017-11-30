using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace LudumDare40.Input.Data
{
	public class KeyboardData
	{
		public KeyboardData(List<Keys> keysDown, List<Keys> keysPressedThisFrame, List<Keys> keysReleasedThisFrame)
		{
			KeysDown = keysDown;
			KeysPressedThisFrame = keysPressedThisFrame;
			KeysReleasedThisFrame = keysReleasedThisFrame;
		}

		public List<Keys> KeysDown { get; }
		public List<Keys> KeysPressedThisFrame { get; }
		public List<Keys> KeysReleasedThisFrame { get; }
	}
}
