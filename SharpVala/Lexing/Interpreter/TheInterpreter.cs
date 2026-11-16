using SharpVala.Lexing.Tokens;
using expr  = SharpVala.Lexing.Expr.Expr;


namespace SharpVala.Lexing.Interpreter;

public class TheInterpreter : expr.IExprVisitor<object>
{
    private object Evaluate(expr expr)
    {
        return expr.Accept<object>(this);
    }

    private bool isTruthy(object Object) {
        if (Object == null) return false;
        if (Object is bool) return (bool)Object;
        return true;
    }

    public object VisitLiteralExpr(expr.Literal expr)
    {
        return expr.Value;
    }
    
    public object VisitGroupingExpr(expr.Grouping expr)
    {
        return Evaluate(expr.Expression);
    }

    public object VisitUnaryExpr(expr.Unary expr)
    {
        object right = Evaluate(expr.Right);

        switch (expr.Operator.type)
        {
            case TokenType.MINUS:
                return -(double)right;
            case TokenType.BANG:
                return !isTruthy(right);
        }

        return null;
    }

    public object VisitBinaryExpr(expr.Binary expr) {
        object left = Evaluate(expr.Left);
        object right = Evaluate(expr.Right);

        switch (expr.Operator.type) {
            case TokenType.MINUS:
                return (double)left - (double)right;
            case TokenType.SLASH:
                return (double)left / (double)right;
            case TokenType.STAR:
                return (double)left * (double)right;

            case TokenType.PLUS:
                if (left is double && right is double) {
                    return (double)left + (double)right;
                } 

                if (left is string && right is string) {
                    return (string)left + (string)right;
                }
                break;

            case TokenType.GREATER:
                return (double)left > (double)right;
            case TokenType.GREATER_EQUAL:
                return (double)left >= (double)right;
            case TokenType.LESS:
                return (double)left < (double)right;
            case TokenType.LESS_EQUAL:
                return (double)left <= (double)right;

            case TokenType.BANG_EQUAL: return left != right;
            case TokenType.EQUAL_EQUAL: return left == right;

            }
            



        return null;
    }
}
