using SharpVala.Lexing.Tokens;

namespace SharpVala.Lexing.Interpreter;

public class RuntimeError : Exception
{
    public Token token { get; }

    public RuntimeError(Token _token, string message) : base(message) {
        token = _token;
    }
}
