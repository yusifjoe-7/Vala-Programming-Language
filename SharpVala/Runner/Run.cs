using SharpVala.ErrorHandling;
using SharpVala.Lexing.Parser;
using SharpVala.Lexing.Scanner;
using SharpVala.Interpreter;

namespace SharpVala.Runner;


class Run
{
    public static void run(string source)
    {
        TheInterpreter interpreter = new TheInterpreter();

        var tokens = new ScanTokens(source).Scan();

        var parser = new Parsing(tokens);
        var statments = parser.parse();


        if (Error.hadError) Environment.Exit(65);
        
        interpreter.Interpret(statments);

        if (Error.hadRuntimeError) Environment.Exit(70);

        }
}