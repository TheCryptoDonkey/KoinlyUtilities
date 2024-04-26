namespace CoinCornerToKoinly;

public class CoinCorner
{
    public CoinCorner(string fileName)
    {
        var transactions = ReadCsvFile(fileName);

        TransactionsList = new List<CoinCornerTransaction>(transactions.Count);
        foreach (var transaction in transactions)
        {
            var tx = ConvertTransaction(transaction);
            TransactionsList.Add(tx);
        }
    }

    public List<CoinCornerTransaction> TransactionsList { get; }

    private static List<string[]> ReadCsvFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath)[1..];
        return lines.Select(line => line.Split(',')).ToList();
    }

    private static CoinCornerTransaction ConvertTransaction(IReadOnlyList<string> originalTransaction)
    {
        return originalTransaction[3] switch
        {
            "Trade" => GetCoinCornerTrade(originalTransaction),
            "Bank deposit" => GetBankDeposit(originalTransaction),
            _ => throw new Exception($"Unknown transaction type {originalTransaction[3]}")
        };
    }

    private static CoinCornerTransaction GetBankDeposit(IReadOnlyList<string> txn)
    {
        return new CoinCornerTransaction
        {
            DateAndTime = txn[0],
            Type = txn[3],
            Gross = decimal.Parse(txn[7]),
            GrossCurrency = txn[8],
            Net = decimal.Parse(txn[11]),
            NetCurrency = txn[12],
            Balance = decimal.Parse(txn[13]),
            BalanceCurrency = txn[14]
        };
    }

    private static CoinCornerTransaction GetCoinCornerTrade(IReadOnlyList<string> txn)
    {
        var strings = txn[2].Split(' ');

        var isBought = "Bought".Equals(strings[0]);
        var amount = decimal.Parse(strings[1]);
        var currency = strings[2];

        return new CoinCornerTransaction
        {
            IsBuy = isBought,
            Amount = amount,
            Currency = currency,
            DateAndTime = txn[0],
            Detail = txn[2],
            Type = txn[3],
            Price = decimal.Parse(txn[5]),
            PriceCurrency = txn[6],
            Gross = decimal.Parse(txn[7]),
            GrossCurrency = txn[8],
            Fee = decimal.Parse(txn[9]),
            FeeCurrency = txn[10],
            Net = decimal.Parse(txn[11]),
            NetCurrency = txn[12],
            Balance = decimal.Parse(txn[13]),
            BalanceCurrency = txn[14]
        };
    }
}