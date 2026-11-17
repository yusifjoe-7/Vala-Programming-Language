using System.Text.Json;
using SharpVala.ErrorHandling;
using SharpVala.Lexing.Tokens;
using expr  = SharpVala.Lexing.Expr.Expr;


namespace SharpVala.Lexing.Interpreter;

public class TheInterpreter : expr.IExprVisitor<object>
{
    private object Evaluate(expr expr)
    {
        return expr.Accept<object>(this);
    }

    private bool isTruthy(object Object)
    {
        if (Object == null) return false;
        if (Object is bool) return (bool)Object;
        return true;
    }

    private string Stringify(object Object) {
        if (Object == null) return "nil";

        if (Object is double) {
        string text = Object.ToString();
        if (text.EndsWith(".0")) {
            text = text.Substring(0, text.Length - 2);
        }
        return text;
        }

        return Object.ToString();
    }

    public void Interpret(expr expression) { 
    try {
      object value = Evaluate(expression);
      Console.WriteLine(Stringify(value));
    } catch (RuntimeError error) {
      Error.RuntimeError(error);
    }
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

    private void CheckNumberOperands(Token op, object left, object right) {
        if (right is double && left is double) return;
        throw new RuntimeError(op, "Operand must be a number.");
    }

    public object VisitBinaryExpr(expr.Binary expr) {
        object left = Evaluate(expr.Left);
        object right = Evaluate(expr.Right);

        switch (expr.Operator.type) {
            case TokenType.MINUS:
                CheckNumberOperands(expr.Operator, left, right);
                return (double)left - (double)right;
            case TokenType.SLASH:
                CheckNumberOperands(expr.Operator, left, right);
                return (double)left / (double)right;
            case TokenType.STAR:
                CheckNumberOperands(expr.Operator, left, right);
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
                 CheckNumberOperands(expr.Operator, left, right);
                return (double)left > (double)right;
            case TokenType.GREATER_EQUAL:
                CheckNumberOperands(expr.Operator, left, right);
                return (double)left >= (double)right;
            case TokenType.LESS:
                CheckNumberOperands(expr.Operator, left, right);
                return (double)left < (double)right;
            case TokenType.LESS_EQUAL:
                CheckNumberOperands(expr.Operator, left, right);
                return (double)left <= (double)right;

            case TokenType.BANG_EQUAL: return JsonSerializer.Serialize(left) != JsonSerializer.Serialize(right);
            case TokenType.EQUAL_EQUAL: return JsonSerializer.Serialize(left) == JsonSerializer.Serialize(right);

            }

        return null;
    }
}
