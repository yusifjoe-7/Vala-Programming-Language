using SharpVala.AStmt;
using SharpVala.Lexing.Tokens;
namespace SharpVala.Lexing.Parser;

public class Helpers
{
    private readonly List<Token> _tokens;
    private int current = 0;

    public Helpers(List<Token> tokens) {
        _tokens = tokens;
    }

    public bool Match(params TokenType[] types) {
        foreach (var type in types) {
        if (Check(type)) {
            Advance();
            return true;
        }
        }

        return false;
    }

    public bool Check(TokenType type) {
        if (IsAtEnd()) return false;
        return Peek().type == type;
    }

    public Token Advance() {
        if (!IsAtEnd()) current++;
        return Previous();
    }
    public bool IsAtEnd() {
        return Peek().type == TokenType.EOF;
    }

    public Token Peek() {
        return _tokens[current];
    }

    public Token Previous() {
        return _tokens[current - 1];
    }
    


}
