
using SharpVala.Runner;


if (args.Length > 1)
{
    Console.WriteLine("Usage: svala [Script]");
    Environment.Exit(64);
}
else if (args.Length == 1)
{
    FileRunner.runFile(args[0]);
}
else
{
    PromptRunner.runPrompt();
}


