namespace SharpVala;


class Run
{
    public static void run(string source)
    {
        string[] tokens = source.Split(" ");
        foreach(var token in tokens)
        {
            Console.WriteLine(token);
            
        }
    }
}