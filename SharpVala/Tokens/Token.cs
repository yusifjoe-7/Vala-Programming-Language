namespace SharpVala.Tokens;

public class Token
{
    public TokenType type;
    public string lexeme;
    public object literal;
    public int line;

    public Token(TokenType Type, string Lexeme, object Literal, int Line)
    {
        type = Type;
        lexeme = Lexeme;
        literal = Literal;
        line = Line;
    }
        
    public string toString() {
    return type + " " + lexeme + " " + literal;
  }
}
