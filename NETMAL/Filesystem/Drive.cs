using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NETMAL.Filesystem
{
    internal class Drive
    {
        private string name;

        public Drive(string dName)
        {
            name = dName;

        }

        public string getName()
        {
            return name;
        }

        public DirectoryInfo DrivetoDirectoryInfo(string dname)
        {
            return new DirectoryInfo(getName());
        }

        public void WalkDirectoryTree(string name)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            DirectoryInfo root = new DirectoryInfo(name);
            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
                if (files != null)
                {
                    Directory.SetCurrentDirectory(root.ToString()); //this may not give the full path to the directory to set it at
                }
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                //log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    Console.WriteLine(fi.FullName);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();
                // Console.WriteLine(subDirs);

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo.ToString());
                }
            }

        }
    }
}
