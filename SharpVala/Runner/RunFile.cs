using SharpVala.ErrorHandling;
using static SharpVala.ErrorHandling.Error;
using static SharpVala.Runner.Run;

namespace SharpVala.Runner;



public class FileRunner
{
    public static void runFile(string path)
    {
        if (File.Exists(path))
        {
            if (!path.EndsWith(".val"))
            {
                Console.WriteLine("Unknown Format");
                Environment.Exit(3);
            }
            string fileContent = File.ReadAllText(path);

            if (fileContent.Length > 0)
            {
                Run.run(fileContent);
                if (Error.hadError) Environment.Exit(65);
            }
            else
            {
                Console.WriteLine("file is emty");
                Environment.Exit(1);
            }
        }
        else
        {
            Console.WriteLine("file is emty");
                Environment.Exit(2);
        }
    }

}

