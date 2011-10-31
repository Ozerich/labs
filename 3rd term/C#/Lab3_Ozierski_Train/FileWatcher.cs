using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train
{
    public static class FileWatcher
    {

        private static FileSystemWatcher watcher;

        public static void Init(string dir, string filter)
        {
            watcher = new FileSystemWatcher(dir, filter);
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
   | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Changed += (o, e) => Console.WriteLine("File {0} was {1}", e.FullPath, e.ChangeType);
            watcher.Deleted += (o, e) => Console.WriteLine("File {0} was {1}", e.FullPath, e.ChangeType);
            watcher.Created += (o, e) => Console.WriteLine("File {0} was {1}", e.FullPath, e.ChangeType);
            watcher.Renamed += (o, e) => Console.WriteLine("Renamed: {0} -> {1}", e.OldFullPath, e.FullPath);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Monitoring  ON \n");
        }
    }
}
