#pragma warning disable

using dotNetMentoringProgramBasics_FileSystemVisitor;
using System.Linq.Expressions;

Console.Write("Enter path: ");
var path = Console.ReadLine();

Console.Write("File to find: ");
var fileToFind = Console.ReadLine();

Console.Write("Files to except: ");
var pathToExcept = Console.ReadLine();

Func<string, Func<string, bool>> filterExpression = fileToExcept => (x => !x.Contains(fileToExcept));

var visitor = new FileSystemVisitor(path, fileToFind, pathToExcept.Split(' ').Select(str => filterExpression(str)).ToArray());

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