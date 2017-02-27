using System;

namespace ModernSystem.IO
{
    public class File
    {
        public Path Path { get; }
        public System.IO.FileInfo FileInfo { get; }

        public File(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            Path = new Path(fileName);
            FileInfo = new System.IO.FileInfo(Path.PathString);
            // TODO: We should validate that either the path does not exist, or it does exist and is a file
        }

        public File(System.IO.FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            Path = new IO.Path(file.FullName);
            FileInfo = file;
        }

        public File(Path path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Path = path;
            FileInfo = new System.IO.FileInfo(Path.PathString);
            // TODO: We should validate that either the path does not exist, or it does exist and is a file
        }

        public bool Exists => FileInfo.Exists;

        public System.IO.FileStream OpenReadStream()
        {
            return FileInfo.OpenRead();
        }

        public System.IO.StreamReader OpenReadStreamReader()
        {
            return new System.IO.StreamReader(OpenReadStream());
        }

        public System.IO.FileStream OpenWriteStream(bool createIfNotExists = false, bool append = false)
        {
            System.IO.FileMode fileMode;
            if (append)
                fileMode = System.IO.FileMode.Append;
            else
                fileMode = createIfNotExists ? System.IO.FileMode.OpenOrCreate : System.IO.FileMode.Open;
            return FileInfo.Open(fileMode, System.IO.FileAccess.Write, System.IO.FileShare.Write);
        }
    }
}
