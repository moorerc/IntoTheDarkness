using System;

namespace EECS494.IntoTheDarkness
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameOne game = new GameOne())
            {
                game.Run();
            }
        }
    }
#endif
}

