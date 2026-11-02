using SharpVala.ErrorHandling;
using SharpVala.Lexing.Tokens;

namespace SharpVala.Lexing.Scanner;

public class ScanTokens
{
    private ScannerHelperMethods Helper;


    public ScanTokens(string source)
    {
        Helper = new ScannerHelperMethods(source);
    }

    public List<Token> Scan()
    {
        while (!Helper.isAtEnd())
        {
            char c = Helper.Advance();
            switch (c)
            {
                case '(': Helper.AddToken(TokenType.LEFT_PAREN); break;
                case ')': Helper.AddToken(TokenType.RIGHT_PAREN); break;
                case '{': Helper.AddToken(TokenType.LEFT_BRACE); break;
                case '}': Helper.AddToken(TokenType.RIGHT_BRACE); break;
                case ',': Helper.AddToken(TokenType.COMMA); break;
                case '.': Helper.AddToken(TokenType.DOT); break;
                case '-': Helper.AddToken(TokenType.MINUS); break;
                case '+': Helper.AddToken(TokenType.PLUS); break;
                case ';': Helper.AddToken(TokenType.SEMICOLON); break;
                case '*': Helper.AddToken(TokenType.STAR); break;

                case '!':
                    Helper.AddToken(Helper.Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    Helper.AddToken(Helper.Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    Helper.AddToken(Helper.Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    Helper.AddToken(Helper.Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;

                // just splitting IF/ELSE casses that other casses

                case '/':
                    if (Helper.Match('/'))
                    {
                        while (Helper.Peek() != '\n' && Helper.Peek() != '\0') Helper.Advance();
                    }
                    else
                    {
                        Helper.AddToken(TokenType.SLASH);
                    }
                    break;


                //----
                // avoiding white spasces
                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    Helper.NextLine();
                    break;

                case '"': Helper.HandleString(); break;


                // end
                default:
                    if (Helper.IsDigit(c))
                    {
                        Helper.Number();
                    }
                    else if (Helper.IsAlpha(c))
                    {
                        Helper.Identifier();
                    }
                    else
                    {
                        Error.addError(Helper.GetLines(), "Unexpected character.");
                    }
                    break;
            }
            Helper.MoveStartToCurrent();
            Scan();
        }
        Helper.TokensPush(new Token(TokenType.EOF, "", null, Helper.GetLines()));
        return Helper.TheReturner();
    }
    
}
