//using SharpVala.Expr;
using SharpVala.Expr;
using SharpVala.Lexing.Tokens;
using Xunit.Abstractions;

namespace SharpVala.Tests;

using Expr = SharpVala.Expr.Expr;

public class ExprTests
{
    readonly ITestOutputHelper _output;

    public ExprTests(ITestOutputHelper output)
    {
        _output = output;
        
    }

    [Fact]
    public void test1()
    {
        Expr expression = new Expr.Binary(
            new Expr.Grouping(
                new Expr.Binary(
                    new Expr.Literal(1),
                    new Token(TokenType.PLUS, "+", null, 1),
                    new Expr.Literal(2)
                )
            ),
            new Token(TokenType.STAR, "*", null, 1),
            new Expr.Literal(3)
        );

        var printer = new AstPrinter();

        _output.WriteLine(printer.Print(expression));
    }
}
