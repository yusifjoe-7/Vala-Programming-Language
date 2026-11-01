using SharpVala.ErrorHandling;
using SharpVala.Tokens;

namespace SharpVala.Scanner;

public class Scanner
{
    private string _source { get; set; }
    private List<Token> tokens = new List<Token>();

    private int start = 0;
    private int current = 0;
    private int line = 1;

    public Scanner(string source)
    {
        _source = source;
    }

    // ---------


private static readonly Dictionary<string, TokenType> Keywords = new()
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


    //------------

    private char advance()
    {
        return _source[current++];
    }

    private void addToken(TokenType type)
    {
        addToken(type, null);
    }

    private void addToken(TokenType type, Object literal)
    {
        String text = _source.Substring(start, current);
        tokens.Add(new Token(type, text, literal, line));
    }
    //--

    private bool isAtEnd()
    {
        return current >= _source.Length;
    }
    private bool match(char expected) {
        if (isAtEnd()) return false;
        if (_source[current] != expected) return false;

        current++;
        return true;
        }

    private char peek(int? i = null)
    {
        if (isAtEnd()) return '\0';
        return i != null? _source[current + i.Value] : _source[current];
    }

    private void String()
    {
        while (peek() != '"' && !isAtEnd())
        {
            if (peek() == '\n') line++;
            advance();
        }

        if (isAtEnd())
        {
            Error.addError(line, "Unterminated string.");
            return;
        }

        advance();

        string value = _source.Substring(start + 1, current - 1);
        addToken(TokenType.STRING, value);
    }

    private bool isDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    private void number()
    {
        while (isDigit(peek())) advance();

        if (peek() == '.' && isDigit(peek(1)))
        {
            advance();
            while (isDigit(peek())) advance();
        }
        addToken(
            TokenType.NUMBER,
            Double.Parse(_source.Substring(start, current))
        );
    }
    private bool isAlpha(char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
                c == '_';
    }
    
    private bool isAlphaNumeric(char c) {
    return isAlpha(c) || isDigit(c);
    }
private void identifier() {
        while (isAlphaNumeric(peek())) advance();

        string text = _source.Substring(start, current);
        TokenType? type = Keywords[text];
        if(type == null)
        {
            type = TokenType.IDENTIFIER;
        }

    addToken(TokenType.IDENTIFIER);
  }


  List<Token> ScanTokens() {
    while (!isAtEnd()) {

    char c = advance();
    switch (c) {
      case '(': addToken(TokenType.LEFT_PAREN); break;
      case ')': addToken(TokenType.RIGHT_PAREN); break;
      case '{': addToken(TokenType.LEFT_BRACE); break;
      case '}': addToken(TokenType.RIGHT_BRACE); break;
      case ',': addToken(TokenType.COMMA); break;
      case '.': addToken(TokenType.DOT); break;
      case '-': addToken(TokenType.MINUS); break;
      case '+': addToken(TokenType.PLUS); break;
      case ';': addToken(TokenType.SEMICOLON); break;
      case '*': addToken(TokenType.STAR); break; 

      case '!':
        addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
        break;
      case '=':
        addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
        break;
      case '<':
        addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
        break;
      case '>':
        addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
            break;

        case '/':
                if (match('/'))
                    {
                        while (peek() != '\n' && peek() != '\0') advance();
                    }
                else
                    {
                        addToken(TokenType.SLASH);
                    }break;

        case ' ':
        case '\r':
        case '\t':
            break;

        case '\n':
            line++;
                    break;

        case '"': String(); break;


      default:
                    if (isDigit(c))
                    {
                        number();
                    }
                    else if (isAlpha(c))
                    {
                        identifier();
                    }
                    else
                    {
                        Error.addError(line, "Unexpected character.");
                    }
        break;

    }

      start = current;
      ScanTokens();
    }

    tokens.Add(new Token(TokenType.EOF, "", null, line));
    return tokens;
  }
}
