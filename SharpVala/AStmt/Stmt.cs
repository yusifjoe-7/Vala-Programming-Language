namespace SharpVala.AStmt;

using SharpVala.Lexing.Tokens;
using Expr = global::SharpVala.Expr.Expr;

public abstract class Stmt
{
    public interface IVisitor<R>
    {
        R VisitExpressionStmt(Expression stmt);
        R VisitPrintStmt(Print print);
        R VisitLetStmt(Let print);
        R VisitBlockStmt(Block bock);

    }

    public abstract R Accept<R>(IVisitor<R> visitor);

    public class Expression : Stmt
    {
        public Expr expression {get;}

        public Expression(Expr _expression)
        {
            expression = _expression;
        }
        public override R Accept<R>(IVisitor<R> visitor)
                        => visitor.VisitExpressionStmt(this);

    }

    public class Print : Stmt
    {
        public Expr expression {get;}

        public Print(Expr _expression)
        {
            expression = _expression;
        }
        public override R Accept<R>(IVisitor<R> visitor) 
                        =>visitor.VisitPrintStmt(this);
    }

    public class Let : Stmt
    {
        public Token name {get;}
        public Expr initializer {get;}
        public Let(Token _name, Expr _initializer)
        {
            initializer = _initializer;
            name = _name;
        }
        
        public override R Accept<R>(IVisitor<R> visitor) 
                        =>visitor.VisitLetStmt(this);
    }

    public class Block : Stmt
    {
        public List<Stmt> statements { get; }

        public Block(List<Stmt> _statements) {
        statements = _statements;
        }

         public override R Accept<R> (IVisitor<R> visitor) 
                =>visitor.VisitBlockStmt(this);
    }

}
