namespace CoinCornerToKoinly;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: CoinCornerToKoinly <path-to-coincorner-csv>");
            return;
        }
        
        var coinCorner = new CoinCorner(args[0]);

        var koinly = KoinlyAdapter.Adapt(coinCorner);
        koinly.CreateFile();
    }
}