namespace SharpVala.AStmt;

using Expr = global::SharpVala.Expr.Expr;

public abstract class Stmt
{
    public interface IVisitor<R>
    {
        R VisitExpressionStmt(Expression stmt);
        R VisitPrintStmt(Print print);

    }

    public abstract R Accept<R>(IVisitor<R> visitor);

    public class Expression : Stmt
    {
        public Expr expression;

        public Expression(Expr _expression)
        {
            expression = _expression;
        }
        public override R Accept<R>(IVisitor<R> visitor)
                        => visitor.VisitExpressionStmt(this);

    }

    public class Print : Stmt
    {
        public Expr expression;

        public Print(Expr _expression)
        {
            expression = _expression;
        }
        public override R Accept<R>(IVisitor<R> visitor) 
                        =>visitor.VisitPrintStmt(this);
    }
}
