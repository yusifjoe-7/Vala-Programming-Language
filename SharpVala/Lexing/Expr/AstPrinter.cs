using System.Text;
using SharpVala.Lexing.Expr;

namespace SharpVala.Lexing.Expr;

public class AstPrinter : IExprVisitor<string>
{
    public string Print(Expr expr)
    {
        return expr.Accept(this);
    }

    public string VisitBinaryExpr(Expr.Binary expr)
    {
        return Parenthesize(
            expr.Operator.lexeme,
            expr.Left,
            expr.Right
        );
    }

    public string VisitGroupingExpr(Expr.Grouping expr)
    {
        return Parenthesize("group", expr.Expression);
    }

    public string VisitLiteralExpr(Expr.Literal expr)
    {
        return expr.Value?.ToString() ?? "nil";
    }

    public string VisitUnaryExpr(Expr.Unary expr)
    {
        return Parenthesize(expr.Operator.lexeme, expr.Right);
    }

    private string Parenthesize(string name, params Expr[] expressions)
    {
        var builder = new StringBuilder();

        builder.Append("(");
        builder.Append(name);

        foreach (var expression in expressions)
        {
            builder.Append(" ");
            builder.Append(expression.Accept(this));
        }

        builder.Append(")");

        return builder.ToString();
    }
}