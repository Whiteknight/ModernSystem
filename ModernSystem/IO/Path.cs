using System;

namespace ModernSystem.IO
{
    public class Path
    {
        public string PathString { get; }

        public Path(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            PathString = NormalizePath(System.IO.Path.GetFullPath(path));
        }

        public static string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return path;

            return path.Replace(System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar);
        }

        // TODO: We should cache all these accessors
        public string Extension => System.IO.Path.GetExtension(PathString);
        public string FileName => System.IO.Path.GetFileName(PathString);
        public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(PathString);
        public string DirectoryName => System.IO.Path.GetDirectoryName(PathString);
        public bool HasExtension => System.IO.Path.HasExtension(PathString);
        public bool IsRooted => System.IO.Path.IsPathRooted(PathString);

        public Path ChangeExtension(string newExtension)
        {
            return new Path(System.IO.Path.ChangeExtension(PathString, newExtension));
        }

        public Path Append(string[] paths)
        {
            return new Path(System.IO.Path.Combine(paths));
        }

        public static Path GetTemporaryFilePath()
        {
            return new Path(System.IO.Path.GetTempFileName());
        }

        public static Path GetRandomFilePath(string directory)
        {
            string fileName = System.IO.Path.GetRandomFileName();
            string path = System.IO.Path.Combine(directory, fileName);
            return new Path(path);
        }

        public bool IsDirectory
        {
            get
            {
                if (!System.IO.Directory.Exists(PathString))
                    return false;
                System.IO.FileAttributes attr = System.IO.File.GetAttributes(PathString);
                return ((attr & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory);
            }
        }

        public bool IsFile
        {
            get
            {
                if (!System.IO.File.Exists(PathString))
                    return false;
                System.IO.FileAttributes attr = System.IO.File.GetAttributes(PathString);
                return ((attr & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory);
            }
        }

        public Uri AsUri()
        {
            return new Uri(PathString, UriKind.Absolute);
        }
    }
}
