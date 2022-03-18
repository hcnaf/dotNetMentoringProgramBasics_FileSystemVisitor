#pragma warning disable

namespace dotNetMentoringProgramBasics_FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private string _directory;
        private string _fileOrDirToFind;
        private Func<string, bool>[] _filters;
        public FileSystemVisitor(string path, string fileOrDirToFind = null, params Func<string, bool>[] filter)
        {
            _directory = path ?? throw new ArgumentNullException(nameof(path));
            _fileOrDirToFind = fileOrDirToFind;
            _filters = filter;
        }

        public IEnumerable<string> GetPaths() => GetPaths(_directory);

        public event Action Started = delegate { };
        public event Action Finished = delegate { };

        public event Action FileFound = delegate { };
        public event Action DirectoryFound = delegate { };

        private IEnumerable<string> GetPaths(string directory, int depth = 0)
        {
            foreach (var file in Directory.GetFiles(directory).Where(file => _filters?.All(x => x(file)) ?? false))
            {
                if (file?.EndsWith(_fileOrDirToFind) ?? false)
                        FileFound();

                yield return Path.GetFileName(file);
            }

            foreach (var dir in Directory.GetDirectories(directory).Where(dir => _filters?.All(x => x(dir)) ?? false))
            {
                if (dir?.EndsWith(_fileOrDirToFind) ?? false)
                        DirectoryFound();

                yield return new DirectoryInfo(Path.GetDirectoryName(dir)).Name;

                foreach(var path in GetPaths(dir, depth + 1))
                    yield return String.Concat(Enumerable.Repeat("  ", depth)) + path;
            }
        }

    }
}