using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernSystem.IO
{
    public class Directory
    {
        public Path Path { get; }
        public System.IO.DirectoryInfo DirectoryInfo { get; }

        public Directory(System.IO.DirectoryInfo dir)
        {
            if (dir == null)
                throw new ArgumentNullException(nameof(dir));
            Path = new Path(dir.FullName);
            DirectoryInfo = dir;
        }
        public Directory(Path path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Path = path;
            DirectoryInfo = new System.IO.DirectoryInfo(Path.PathString);
            // TODO: We should validate that either the path does not exist, or it does exist and is a directory
        }

        public IEnumerable<Directory> Directories
        {
            get
            {
                return DirectoryInfo.EnumerateDirectories().Select(d => new Directory(d));
            }
        }

        public IEnumerable<File> Files
        {
            get
            {
                return DirectoryInfo.EnumerateFiles().Select(f => new File(f));
            }
        }
    }
}
