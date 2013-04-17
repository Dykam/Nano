using System;

namespace Nano
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (NanoGame game = new NanoGame())
            {
                game.Run();
            }
        }
    }
#endif
}

