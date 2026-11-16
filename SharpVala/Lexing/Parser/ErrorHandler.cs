using SharpVala.ErrorHandling;
using SharpVala.Lexing.Tokens;

namespace SharpVala.Lexing.Parser;

public class ErrorHandler
{
    private readonly Helpers _helper;
    public class ParseError : Exception {}

    public ErrorHandler(Helpers helpers)
    {
        _helper = helpers;
    }
    public Token Consume(TokenType type, string message) {
        if (_helper.Check(type)) return _helper.Advance();

        throw AnError(_helper.Peek(), message);
    }

    public ParseError AnError(Token token, string message) {
        Error.addError(token, message);
        return new ParseError();
    }

    private void Synchronize() {
        _helper.Advance();

        while (!_helper.IsAtEnd()) {
        if (_helper.Previous().type == TokenType.SEMICOLON) return;

        switch (_helper.Peek().type) {
            case TokenType.CLASS:
            case TokenType.FUN:
            case TokenType.LET:
            case TokenType.FOR:
            case TokenType.IF:
            case TokenType.WHILE:
            case TokenType.PRINT:
            case TokenType.RETURN:
            return;
        }

        _helper.Advance();
        }
    }
}
