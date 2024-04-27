namespace CoinCornerToKoinly;

public class CoinCorner
{
    public enum FileType
    {
        Bitcoin,
        Pound,
        Euro // not implemented
    }
    
    public CoinCorner(string file1, string file2)
    {
        TransactionsList = new List<CoinCornerTransaction>();
        
        var file1Transactions = ReadCsvFile(file1);
        var file2Transactions = ReadCsvFile(file2);

        var file1FileType = GetFileType(file1);
        var file2FileType = GetFileType(file2);

        ProcessTransactions(file1Transactions, file1FileType);
        ProcessTransactions(file2Transactions, file2FileType);
    }

    private static FileType GetFileType(string fileName)
    {
        if (fileName.Contains("BTC"))
        {
            return FileType.Bitcoin;
        }

        if (fileName.Contains("GBP"))
        {
            return FileType.Pound;
        }

        if (fileName.Contains("EUR"))
        {
            return FileType.Euro;
        }

        throw new Exception("Unknown file type");
    }

    private void ProcessTransactions(List<string[]> transactions, FileType fileType)
    {
       
        foreach (var transaction in transactions)
        {
            var tx = ConvertTransaction(transaction);
            if (tx == null)
            {
                // ignore pending transactions
                continue;
            }

            if (fileType == FileType.Bitcoin && tx.Type == "Trade")
            {
                // ignore trades in the bitcoin file as we have covered in the sterling file
                continue;
            }
            
            TransactionsList.Add(tx);
        }
    }


    public List<CoinCornerTransaction> TransactionsList { get; set; }

    private static List<string[]> ReadCsvFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath)[1..];
        return lines.Select(line => line.Split(',')).ToList();
    }

    private static CoinCornerTransaction? ConvertTransaction(IReadOnlyList<string> originalTransaction)
    {
        if (originalTransaction[0].Equals("Pending"))
        {
            return null;
        }

        return originalTransaction[3] switch
        {
            "Trade" => GetCoinCornerTrade(originalTransaction),
            "Bank deposit" => GetBankDeposit(originalTransaction),
            "Send" => GetSend(originalTransaction),
            "Bolt Card" => GetBoltCard(originalTransaction),
            "Receive" => GetReceive(originalTransaction),
            _ => throw new Exception($"Unknown transaction type {originalTransaction[3]}")
        };
    }

    private static CoinCornerTransaction? GetReceive(IReadOnlyList<string> txn)
    {
        return new CoinCornerTransaction()
        {
            DateAndTime = txn[0],
            Detail = txn[2],
            Type = txn[3],
            TransactionId = txn[4],
            Price = txn[5] != "" ? decimal.Parse(txn[5]) : null,
            PriceCurrency = txn[6] != "" ? txn[6] : null,
            Gross = decimal.Parse(txn[7]),
            GrossCurrency = txn[8],
            Net = decimal.Parse(txn[11]),
            NetCurrency = txn[12],
            Balance = decimal.Parse(txn[13]),
            BalanceCurrency = txn[14]
        };
    }

    private static CoinCornerTransaction? GetBoltCard(IReadOnlyList<string> txn)
    {
        return new CoinCornerTransaction()
        {
            DateAndTime = txn[0],
            Detail = txn[2],
            Type = txn[3],
            TransactionId = txn[4],
            Gross = decimal.Parse(txn[7]),
            GrossCurrency = txn[8],
            Net = decimal.Parse(txn[11]),
            NetCurrency = txn[12],
            Balance = decimal.Parse(txn[13]),
            BalanceCurrency = txn[14]
        };
    }

    private static CoinCornerTransaction? GetSend(IReadOnlyList<string> txn)
    {
        var fee = txn[9];
        var feeCurrency = txn[10];
        
        return new CoinCornerTransaction()
        {
            DateAndTime = txn[0],
            Detail = txn[2],
            Type = txn[3],
            TransactionId = txn[4],
            Gross = decimal.Parse(txn[7]),
            GrossCurrency = txn[8],
            Fee = fee != "" ? decimal.Parse(fee) : null,
            FeeCurrency = feeCurrency != "" ? feeCurrency : null,
            Net = decimal.Parse(txn[11]),
            NetCurrency = txn[12],
            Balance = decimal.Parse(txn[13]),
            BalanceCurrency = txn[14]
        };
    }

    private static CoinCornerTransaction? GetBankDeposit(IReadOnlyList<string> txn)
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

    private static CoinCornerTransaction? GetCoinCornerTrade(IReadOnlyList<string> txn)
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
            Fee = txn[9] != "" ? decimal.Parse(txn[9]) : null,
            FeeCurrency = txn[10] != "" ? txn[10] : null,
            Net = decimal.Parse(txn[11]),
            NetCurrency = txn[12],
            Balance = decimal.Parse(txn[13]),
            BalanceCurrency = txn[14]
        };
    }
}