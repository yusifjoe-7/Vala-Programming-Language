using SharpVala.Lexing.Scanner;
using SharpVala.Lexing.Tokens;
using Xunit.Abstractions;

namespace SharpVala.Tests;

public class ScannerTests
{
    readonly ITestOutputHelper _output;

    public ScannerTests(ITestOutputHelper output)
    {
        _output = output;
        
    }
 

    string t1 = "rda 33 !ms =>== ifd";

    [Fact]
    public void test1()
    {
        ScanTokens scanner = new ScanTokens(t1);
        var data = scanner.Scan();
        foreach (var token in data)
        
        {
            _output.WriteLine(token.type.ToString());
            //_output.WriteLine(token.literal.ToString());
            _output.WriteLine("--------");
        }
    }
    

}
