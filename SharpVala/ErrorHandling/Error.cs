namespace SharpVala.ErrorHandling;

public class Error
{
    public static bool hadError { get; private set; } = false;

    public static void setNoErrors()
    {
        hadError = false;
    }
    public static void addError(int line, string message)
    {
        reporte(line,"" , message);
    }

    private static void reporte(int line, string where, string message)
    {
        Console.WriteLine(
            "[line " + line + "] Error" + where + ": " + message
        );
        hadError = true;
    }
}
