using SharpVala.Lexing.Tokens;

namespace SharpVala.ErrorHandling;

public class Error
{
    public static bool hadError { get; private set; } = false;

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

    public static void addError(Token token, String message) {
    if (token.type == TokenType.EOF) {
      report(token.line, " at end", message);
    } else {
      report(token.line, " at '" + token.lexeme + "'", message);
    }
  }
}
