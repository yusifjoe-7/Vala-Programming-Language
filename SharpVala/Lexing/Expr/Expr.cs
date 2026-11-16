using SharpVala.Lexing.Tokens;
using SharpVala.Lexing.Expr;
namespace SharpVala.Lexing.Expr;

public abstract class Expr
{
    public abstract T Accept<T>(IExprVisitor<T> visitor);

    public class Binary : Expr
    {
        public Expr Left { get; }
        public Token Operator { get; }
        public Expr Right { get; }

        public Binary(Expr left, Token op, Expr right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        public override T Accept<T>(IExprVisitor<T> visitor)
            => visitor.VisitBinaryExpr(this);
    }

    public class Grouping : Expr
    {
        public Expr Expression { get; }

        public Grouping(Expr expression)
        {
            Expression = expression;
        }

        public override T Accept<T>(IExprVisitor<T> visitor)
            => visitor.VisitGroupingExpr(this);
    }

    public class Literal : Expr
    {
        public object? Value { get; }

        public Literal(object? value)
        {
            Value = value;
        }

        public override T Accept<T>(IExprVisitor<T> visitor)
            => visitor.VisitLiteralExpr(this);
    }

    public class Unary : Expr
    {
        public Token Operator { get; }
        public Expr Right { get; }

        public Unary(Token op, Expr right)
        {
            Operator = op;
            Right = right;
        }

        public override T Accept<T>(IExprVisitor<T> visitor)
            => visitor.VisitUnaryExpr(this);
    }
}
