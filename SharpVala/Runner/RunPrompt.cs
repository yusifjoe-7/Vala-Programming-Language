using SharpVala.ErrorHandling;

namespace SharpVala.Runner;


public class PromptRunner
{
    public static void runPrompt()
    {
        while (true)
        {
            Console.Write("==> ");
            string? inputLine = Console.ReadLine();
            if (inputLine == null) break;
            // if (String.IsNullOrEmpty(inputLine)) break;
            Run.run(inputLine);
            Error.setNoErrors();
            
        }
    }
}