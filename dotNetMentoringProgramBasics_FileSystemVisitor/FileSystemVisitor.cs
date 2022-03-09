#pragma warning disable

namespace dotNetMentoringProgramBasics_FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private string _directory;
        private string _fileOrDirToFind;
        private Func<string, bool> _filter;
        public FileSystemVisitor(string path, string fileOrDirToFind = null, Func<string, bool> filter = null)
        {
            _directory = path ?? throw new ArgumentNullException(nameof(path));
            _fileOrDirToFind = fileOrDirToFind;
            _filter = filter;
        }

        public IEnumerable<string> GetPaths()
        {
            Started();

            var res = GetPaths(_directory);

            Finished();

            return res;
        }

        public event Action Started = delegate { };
        public event Action Finished = delegate { };

        public event Action FileFound = delegate { };
        public event Action DirectoryFound = delegate { };

        public event Action FilteredFileFound = delegate { };
        public event Action FilteredDirectoryFound = delegate { };

        private IEnumerable<string> GetPaths(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                if (file.EndsWith(_fileOrDirToFind))
                    FileFound();

                if (_filter(file))
                {
                    if (file.EndsWith(_fileOrDirToFind))
                        FilteredFileFound();

                    yield return file;
                }
            }

            foreach (var dir in Directory.GetDirectories(directory))
            {
                if (dir.EndsWith(_fileOrDirToFind))
                    DirectoryFound();

                if (_filter(dir))
                {
                    if (dir.EndsWith(_fileOrDirToFind))
                        FilteredDirectoryFound();

                    yield return dir;
                }

                foreach(var path in GetPaths(dir))
                    yield return path;
            }
        }

    }
}