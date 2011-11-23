using System;
using Smiley.Lib;
using System.Windows.Forms;
using System.Drawing;

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
            using (SMH game = new SMH())
            {
#if windows
                ((form)form.fromhandle(game.window.handle)).icon = new icon("game.ico");
#endif
                game.Run();
            }
        }
    }
#endif
}

