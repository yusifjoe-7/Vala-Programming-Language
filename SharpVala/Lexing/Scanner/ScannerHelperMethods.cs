using SharpVala.ErrorHandling;
using SharpVala.Lexing.Tokens;

namespace SharpVala.Lexing.Scanner;

public class ScannerHelperMethods
{
    private Scanner scanner;

    public ScannerHelperMethods(string s)
    {
        scanner = new Scanner(s);

    }
    // to pass it
    public int NextLine() => scanner.NextLine();
    public int GetLines() => scanner.GetLines;
    public void MoveStartToCurrent() => scanner.MoveStartToCurrent();
    public void TokensPush(Token token) => scanner.TokensPush(token);

    //---

    public bool isAtEnd()
    {
        return scanner.GetCurrent
                >=
                scanner.GetSource.Length;
    }

    //--

    public char Advance()
    {
        return scanner.GetSource[scanner.NextCurrent()];
    }

    //---

    public void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    public void AddToken(TokenType type, Object literal)
    {
        String text = scanner.GetSource.Substring(scanner.GetStart, scanner.GetCurrent);
        scanner.TokensPush(new Token(type, text, literal, scanner.GetLines));
    }

    //---

    public bool Match(char expected)
    {
        if (isAtEnd()) return false;
        if (scanner.GetSource[scanner.GetCurrent] != expected) return false;
        scanner.NextCurrent();
        return true;
    }

    //---

    public char Peek(int? i = null)
    {
        if (isAtEnd()) return '\0';
        return i != null ? scanner.GetSource[scanner.GetCurrent + i.Value]
        :
         scanner.GetSource[scanner.GetCurrent];
    }

    //---

    public void HandleString()
    {
        while (Peek() != '"' && !isAtEnd())
        {
            if (Peek() == '\n') NextLine();
            Advance();
        }

        if (isAtEnd())
        {
            Error.addError(scanner.GetLines, "Unterminated string.");
            return;
        }


        string value = scanner.GetSource.Substring(scanner.GetStart + 1, scanner.GetCurrent);
        scanner.NextCurrent();
        AddToken(TokenType.STRING, value);

    }

    //----

    public bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    public void Number()
    {
        while (IsDigit(Peek())) Advance();

        if (Peek() == '.' && IsDigit(Peek(1)))
        {
            Advance();
            while (IsDigit(Peek())) Advance();
        }
        AddToken(
            TokenType.NUMBER,
            Double.Parse(scanner.GetSource.Substring(scanner.GetStart, scanner.GetCurrent))
        );
    }
    public bool IsAlpha(char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
                c == '_';
    }
    public bool isAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }

    public void Identifier()
    {
        while (isAlphaNumeric(Peek())) Advance();

        string text = scanner.GetSource.Substring(scanner.GetStart, scanner.GetCurrent);
        TokenType? type = TokensDictionary.Keywords[text];
        if (type == null)
        {
            type = TokenType.IDENTIFIER;
        }

        AddToken(TokenType.IDENTIFIER);
    }
  

  // the finnaly

  public List<Token> TheReturner()
    {
        return scanner.GetTokens;
    }

}
