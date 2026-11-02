using SharpVala.ErrorHandling;
using SharpVala.Lexing.Tokens;

namespace SharpVala.Lexing.Scanner;

public class Scanner
{
    private string _source { get; }
    private List<Token> tokens = new List<Token>();

    private int start = 0;
    private int current = 0;
    private int line = 1;

    public Scanner(string source)
    {
        _source = source;
    }

   
    // ---------
    // main helper methods

    public string GetSource => _source;
    public int GetCurrent => current;
    public int GetStart => start;
    public int GetLines => line;
    public int NextLine() => line++;
    public int NextCurrent() => current++;
    public void MoveStartToCurrent() => start = current;
    public void TokensPush(Token token) => tokens.Add(token);
    public List<Token> GetTokens => tokens;



}
