namespace dotNetMentoringProgramBasics_FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private string _directory;
        public FileSystemVisitor(string path)
        {
            _directory = path;
        }

        public IEnumerable<string> GetAllPossibleDirectories()
        {
            foreach(var dir in Directory.GetDirectories(_directory))
            {
                yield return dir;
            }
        }
    }
}