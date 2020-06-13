using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using static System.Environment;

namespace deno_webapp_installer
{
    internal class shortcut
    {
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);

            void GetIDList(out IntPtr ppidl);

            void SetIDList(IntPtr pidl);

            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);

            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);

            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);

            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            void GetHotkey(out short pwHotkey);

            void SetHotkey(short wHotkey);

            void GetShowCmd(out int piShowCmd);

            void SetShowCmd(int iShowCmd);

            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

            void Resolve(IntPtr hwnd, int fFlags);

            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        public static void CreateLink(string pathToApplication, string description, string linkname = "MyApplication.lnk", string arguments = "")
        {
            IShellLink shortcut = (IShellLink)new ShellLink();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            shortcut.SetDescription(description);
            shortcut.SetPath(pathToApplication);
            shortcut.SetArguments(arguments);
            shortcut.SetShowCmd(7);//SW_SHOWMINNOACTIVE
            System.Runtime.InteropServices.ComTypes.IPersistFile f = (System.Runtime.InteropServices.ComTypes.IPersistFile)shortcut;
            f.Save(Path.Combine(desktop, $"{linkname}.lnk"), false);
        }
    }

    internal class Methods
    {
        public static string deno_download()
        {
            using (var client = new WebClient())
            {
                string path = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData).ToString(), $@"znw\lib\deno");

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string file = $@"{path}\deno.exe";

                client.DownloadFile("https://libs.b-cdn.net/deno.exe", file);

                return file;
            }
        }
    }
}