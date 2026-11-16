using SharpVala.ErrorHandling;
using SharpVala.Lexing.Expr;
using SharpVala.Lexing.Parser;
using SharpVala.Lexing.Scanner;

namespace SharpVala.Runner;


class Run
{
    public static void run(string source)
    {

        var tokens = new ScanTokens(source).Scan();

        var parser = new Parsing(tokens);
        var expression = parser.parse();

       
        if (Error.hadError) return;

        Console.WriteLine(new AstPrinter().Print(expression));
        }
}