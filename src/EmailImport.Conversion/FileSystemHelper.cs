using System;
using System.IO;
using System.Threading;

namespace EmailImport.Conversion
{
    public static class FileSystemHelper
    {
        /// <summary>
        /// Recreates all directories and subdirectories as specified by the path.
        /// </summary>
        /// <param name="path">The directory path to recreate.</param>
        public static void RecreateDirectory(String path)
        {
            CleanDirectory(path, true);
        }

        /// <summary>
        /// Removes all files and directories from the specified path.
        /// </summary>
        /// <param name="path">The directory path to clean.</param>
        /// <param name="create">If set to <c>true</c> create the directory if it doesn't exist.</param>
        public static void CleanDirectory(String path, Boolean create)
        {
            if (String.IsNullOrEmpty(path))
                return;

            if (Directory.Exists(path))
            {
                foreach (var d in Directory.GetDirectories(path))
                    Directory.Delete(d, true);

                foreach (var f in Directory.GetFiles(path))
                    File.Delete(f);
            }
            else if (create)
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Create a directory if it doesn't exist
        /// </summary>
        /// <param name="path">The directory path to create.</param>
        public static void CreateDirectory(String path)
        {
            CreateDirectory(path, false);
        }

        /// <summary>
        /// Create a directory if it doesn't exist and wait for the file system to register it.
        /// </summary>
        /// <param name="path">The directory path to create.</param>
        /// <param name="wait">If set to <c>true</c> wait for the directory to be created.</param>
        public static void CreateDirectory(String path, Boolean wait)
        {
            if (String.IsNullOrEmpty(path))
                return;

            if (Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);

            if (!wait)
                return;

            while (!Directory.Exists(path))
                Thread.Sleep(10);
        }

        /// <summary>
        /// Delete a directory if it exists recursively
        /// </summary>
        /// <param name="path">The directory path to delete.</param>
        public static void DeleteDirectory(String path)
        {
            DeleteDirectory(path, true);
        }

        /// <summary>
        /// Delete a directory if it exists
        /// </summary>
        /// <param name="path">The directory path to delete.</param>
        /// <param name="recursive">If set to <c>true</c> will delete all directories, sub-directories and files in the path.</param>
        public static void DeleteDirectory(String path, Boolean recursive)
        {
            if (String.IsNullOrEmpty(path))
                return;

            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }
    }
}
