using SharpVala.ErrorHandling;
using SharpVala.Lexing.Expr;
using SharpVala.Lexing.Interpreter;
using SharpVala.Lexing.Parser;
using SharpVala.Lexing.Scanner;

namespace SharpVala.Runner;


class Run
{
    public static void run(string source)
    {
        TheInterpreter interpreter = new TheInterpreter();

        var tokens = new ScanTokens(source).Scan();

        var parser = new Parsing(tokens);
        var expression = parser.parse();


        if (Error.hadError) Environment.Exit(65);
        if (Error.hadRuntimeError) Environment.Exit(70);

        interpreter.Interpret(expression);
        }
}