namespace SharpVala.Lexing.Tokens;

public class TokensDictionary
{
    public static readonly Dictionary<string, TokenType> Keywords = new()
{
    ["and"] = TokenType.AND,
    ["class"] = TokenType.CLASS,
    ["else"] = TokenType.ELSE,
    ["false"] = TokenType.FALSE,
    ["for"] = TokenType.FOR,
    ["fun"] = TokenType.FUN,
    ["if"] = TokenType.IF,
    ["nil"] = TokenType.NIL,
    ["or"] = TokenType.OR,
    ["print"] = TokenType.PRINT,
    ["return"] = TokenType.RETURN,
    ["super"] = TokenType.SUPER,
    ["this"] = TokenType.THIS,
    ["true"] = TokenType.TRUE,
    ["let"] = TokenType.LET,
    ["while"] = TokenType.WHILE
};
}
