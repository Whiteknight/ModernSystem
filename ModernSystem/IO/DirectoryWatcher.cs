using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernSystem.IO
{

    public class DirectoryWatcher
    {
        public System.IO.FileSystemWatcher Watcher { get; private set; }
        private readonly Path _path;
        private readonly IDirectoryWatchEventReceiver _receiver;
        private readonly bool _includeSubdirectories;
        private readonly string _filter;

        public DirectoryWatcher(Directory dir, IDirectoryWatchEventReceiver receiver, bool includeSubdirectories, string filter)
        {
            _path = dir.Path;
            _receiver = receiver;
            _includeSubdirectories = includeSubdirectories;
            _filter = filter;
        }

        public void StartWatching(bool includeSubdirectories = false, string filter = "*")
        {
            if (Watcher != null)
                return;

            Watcher = new System.IO.FileSystemWatcher();
            Watcher.Path = _path.PathString;
            Watcher.Filter = filter;
            Watcher.IncludeSubdirectories = includeSubdirectories;
            Watcher.Changed += Watcher_Changed;
            Watcher.Created += Watcher_Created;
            Watcher.Deleted += Watcher_Deleted;
            Watcher.Error += Watcher_Error;
            Watcher.Renamed += Watcher_Renamed;

            Watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            if (Watcher == null)
                return;

            Watcher.EnableRaisingEvents = false;
            Watcher.Dispose();
            Watcher = null;
        }

        private void Watcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            _receiver.Renamed(new Path(e.OldFullPath), new Path(e.FullPath));
        }

        private void Watcher_Error(object sender, System.IO.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Watcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            _receiver.Deleted(new Path(e.FullPath));
        }

        private void Watcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            _receiver.Created(new Path(e.FullPath));
        }

        private void Watcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            _receiver.Changed(new Path(e.FullPath));
        }
    }
}
