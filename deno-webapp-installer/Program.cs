using System;
using static deno_webapp_installer.Methods;

namespace deno_webapp_installer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            var file = deno_download();

            shortcut.CreateLink(file, "start Launcher", "Launcher", $"run -A -r --unstable https://deno-webapp.glitch.me/client");
        }
    }
}