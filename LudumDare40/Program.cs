using System;

namespace LudumDare40
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
	        using (MainGame game = new MainGame())
	        {
		        game.Run();
	        }
        }
    }
}
