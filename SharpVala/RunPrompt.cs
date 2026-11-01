using SharpVala.ErrorHandling;

namespace SharpVala;


class PromptRunner
{
    public static void runPrompt()
    {
        while (true)
        {
            Console.Write("==> ");
            string? inputLine = Console.ReadLine();
            if (inputLine == null) break;
            Run.run(inputLine);
            Error.setNoErrors();
            
        }
    }
}