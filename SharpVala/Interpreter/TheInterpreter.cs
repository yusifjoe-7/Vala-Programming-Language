using System.Text.Json;
using SharpVala.ErrorHandling;
using SharpVala.Lexing.Tokens;
using expr  = SharpVala.Expr.Expr;
using SharpVala.AStmt;


namespace SharpVala.Interpreter;

public class TheInterpreter : expr.IVisitor<object>, Stmt.IVisitor<object>
{
    private TheEnvironment environment = new TheEnvironment();

    private object Evaluate(expr expr)
    {
        return expr.Accept<object>(this);
    }

    public object VisitExpressionStmt(Stmt.Expression stmt) {
        Evaluate(stmt.expression);
        return null;
    }

      public object VisitPrintStmt(Stmt.Print stmt) {
        object value = Evaluate(stmt.expression);
        Console.WriteLine(Stringify(value));
        return null;
    }

    public object VisitLetStmt(Stmt.Let stmt) {
        object value = null;
        if (stmt.initializer != null) {
        value = Evaluate(stmt.initializer);
        }

        environment.Define(stmt.name.lexeme, value);
        return null;
    }

    public object VisitBlockStmt(Stmt.Block stmt) {
        ExecuteBlock(stmt.statements, new TheEnvironment(environment));
        return null;
    }

    void ExecuteBlock(List<Stmt> statements, TheEnvironment _environment) {
        TheEnvironment previous = environment;
        try {
        environment = _environment;

        foreach (var statement in statements) {
            Execute(statement);
        }
        } finally {
        environment = previous;
        }
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



    public void Interpret(List<Stmt> statements) { 
    try {
      foreach(var stmt in statements)
      {
        Execute(stmt);
      }
    } catch (RuntimeError error) {
      Error.RuntimeError(error);
    }
    }


    void Execute(Stmt stmt) {
        stmt.Accept(this);
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
    public object VisitVariableExpr(expr.Variable expr) {
        return environment.Get(expr.Name);
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

    public object VisitAssignExpr(expr.Assign expr) {
        object value = Evaluate(expr.Value);
        environment.Assign(expr.Name, value);
        return value;
    }
}
