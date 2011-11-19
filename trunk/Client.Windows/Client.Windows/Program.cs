using System;
using Smiley.Lib;

namespace Smiley.Client.Windows
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SmileyGame game = new SmileyGame())
            {
                game.Run();
            }
        }
    }
#endif
}

