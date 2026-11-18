using SharpVala.Lexing.Tokens;
namespace SharpVala.Expr;

public abstract class Expr
{
    public interface IVisitor<T>
    {
        T VisitBinaryExpr(Expr.Binary expr);
        T VisitGroupingExpr(Expr.Grouping expr);
        T VisitLiteralExpr(Expr.Literal expr);
        T VisitUnaryExpr(Expr.Unary expr);
        T VisitVariableExpr(Expr.Variable expr);
        T VisitAssignExpr(Expr.Assign expr);
    }
    public abstract T Accept<T>(IVisitor<T> visitor);

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

        public override T Accept<T>(IVisitor<T> visitor)
            => visitor.VisitBinaryExpr(this);
    }

    public class Grouping : Expr
    {
        public Expr Expression { get; }

        public Grouping(Expr expression)
        {
            Expression = expression;
        }

        public override T Accept<T>(IVisitor<T> visitor)
            => visitor.VisitGroupingExpr(this);
    }

    public class Literal : Expr
    {
        public object? Value { get; }

        public Literal(object? value)
        {
            Value = value;
        }

        public override T Accept<T>(IVisitor<T> visitor)
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

        public override T Accept<T>(IVisitor<T> visitor)
            => visitor.VisitUnaryExpr(this);
    }

    public class Variable : Expr
    {
        public Token Name { get; }

        public Variable(Token name)
        {
            Name = name;
        }

        public override T Accept<T>(IVisitor<T> visitor)
            => visitor.VisitVariableExpr(this);

    }

    public class Assign : Expr
    {
        public Token Name { get; }
        public Expr Value { get; }

        public Assign(Token name, Expr value)
        {
            Name = name;
            Value = value;
        }

        public override T Accept<T>(IVisitor<T> visitor)
            => visitor.VisitAssignExpr(this);
    }
}
