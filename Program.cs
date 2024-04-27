namespace CoinCornerToKoinly;

public static class Program
{
    public static void Main(string[] args)
    {
        // ensure we have 2 files
        if (args.Length != 2)
        {
            ShowUsage();
            return;
        }
        
        var coinCorner = new CoinCorner(args[0], args[1]);

        var koinly = KoinlyAdapter.Adapt(coinCorner);
        koinly.CreateFile();
    }

    private static void ShowUsage()
    {
        Console.WriteLine("Usage: CoinCornerToKoinly <CoinCorner BTC Transactions Export.csv> <CoinCorner GBP Transactions Export.csv>");
    }
}