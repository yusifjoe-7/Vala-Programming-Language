using SharpVala.Lexing.Tokens;
using SharpVala.Interpreter;

namespace SharpVala.ErrorHandling;

public class Error
{
    public static bool hadError { get; private set; } = false;
    public static bool hadRuntimeError { get; private set; } = false;

    public static void setNoErrors()
    {
        hadError = false;
    }
    public static void addError(int line, string message)
    {
        report(line,"" , message);
    }

    private static void report(int line, string where, string message)
    {
        Console.WriteLine(
            "[line " + line + "] Error" + where + ": " + message
        );
        hadError = true;
    }

  public static void addError(Token token, string message)
  {
    if (token.type == TokenType.EOF)
    {
      report(token.line, " at end", message);
    }
    else
    {
      report(token.line, " at '" + token.lexeme + "'", message);
    }
  }
    
    public static void RuntimeError(RuntimeError error) {
      Console.Error.WriteLine(error.Message +
          "\n[line " + error.token.line + "]");
      hadRuntimeError = true;
    }
  
}
