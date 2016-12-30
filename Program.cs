using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace photo_folder
{
    class Program
    {
        static void Main(string[] args)
        {

            string root = Directory.GetCurrentDirectory();
            string nef_folder = CreateFolder(root + @"\nef");

            if (args.Length > 0)
            {
                nef_folder = @args[0];
            }

            FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.Read, root);
            fp.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, nef_folder);

            try
            {
                fp.Demand();

                Execute(root, nef_folder, "nef");
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }

            //Console.ReadLine();
        }

        static void Execute(string root, string nef_folder, string format)
        {
            string[] nefs = Directory.GetFiles(root, "*." + format, SearchOption.TopDirectoryOnly);

            CutMoveFiles(nefs, nef_folder);

        }

        static string CreateFolder(string path)
        {
            string ret = path;

            if (!Directory.Exists(path))
            {
                ret = Directory.CreateDirectory(path).FullName;
            }

            return ret;
        }

        static void CutMoveFiles(string[] files, string dest)
        {
            string dst_name = "";

            foreach (string item in files)
            {
                try
                {
                    FileInfo fi = new FileInfo(item);
                    dst_name = dest + @"\" + fi.Name;

                    if (!File.Exists(dst_name))
                    {
                        File.Move(item, dst_name);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Copy error " + e.ToString());
                    Console.WriteLine("Press Enter");
                    //Console.ReadLine();
                }

            }
        }
    }
}
