using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare40.Input
{
	public enum LockKeys
	{
		CapsLock,
		NumLock,
		ScrollLock
	}
	
	public static class InputUtilities
	{
		private static int[] lockCodes =
		{
			0x14,
			0x90,
			0x91
		};

		// Taken from http://stackoverflow.com/questions/577411/how-can-i-find-the-state-of-numlock-capslock-and-scrolllock-in-net.
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);
		
		public static bool CheckLock(LockKeys key)
		{
			return ((ushort)GetKeyState(lockCodes[(int)key]) & 0xffff) != 0;
		}
	}
}
