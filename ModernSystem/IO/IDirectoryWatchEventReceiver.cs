using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernSystem.IO
{

    public interface IDirectoryWatchEventReceiver
    {
        void Renamed(Path oldPath, Path newPath);
        void Deleted(Path path);
        void Created(Path path);
        void Changed(Path path);
    }
}
