using System;
using System.IO;

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

            //Console.WriteLine(root);
            //Console.WriteLine(nef_folder);

            Execute(root, nef_folder, "nef");

            //Console.ReadLine();
        }

        static void Execute(string root, string nef_folder, string format)
        {
            string[] nefs = Directory.GetFiles(root, "*." + format, SearchOption.TopDirectoryOnly);

            CutMoveFiles(nefs, nef_folder);

        }

        static string CreateFolder(string path)
        {
            string ret = "";
            if (!Directory.Exists(path))
            {
                ret = Directory.CreateDirectory(path).FullName;
            };
            return ret;
        }

        static void CutMoveFiles(string[] files, string dest)
        {
            foreach (string item in files)
            {
                try
                {
                    FileInfo fi = new FileInfo(item);
                    File.Copy(item, dest + @"\" + fi.Name);
                    //Console.WriteLine("Copy " + item.ToString());

                    try
                    {
                        File.Delete(item);
                        //Console.WriteLine("Delete " + item.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Delete error " + e.ToString());
                        Console.WriteLine("Press Enter");
                        //Console.ReadLine();
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
