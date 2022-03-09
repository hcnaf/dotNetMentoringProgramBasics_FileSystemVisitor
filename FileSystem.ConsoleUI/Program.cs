#pragma warning disable

using dotNetMentoringProgramBasics_FileSystemVisitor;

var visitor = new FileSystemVisitor("../../../../../", fileOrDirToFind: "FileSystemVisitor.cs", filter: x => !x.Contains(".git"));
visitor.Started += OnStart;
visitor.Finished += OnFinish;
visitor.FileFound += OnFileFound;
visitor.FilteredFileFound += OnFilteredFileFound;

foreach(var dir in visitor.GetPaths())
{
    Console.WriteLine(dir);
}

void OnStart() =>
    Console.WriteLine("Started at: " + DateTime.Now);

void OnFinish() =>
    Console.WriteLine("Finished at: " + DateTime.Now);

void OnFileFound() =>
    Console.WriteLine("Your file was found.");

void OnFilteredFileFound() =>
    Console.WriteLine("And it satisfies filter!");