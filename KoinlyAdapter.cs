namespace CoinCornerToKoinly;

public static class KoinlyAdapter
{
    public static Koinly Adapt(CoinCorner coinCorner)
    {
        var list = new List<KoinlyTransaction>();

        foreach (var transaction in coinCorner.TransactionsList) list.Add(AdaptTransactionByType(transaction));

        return new Koinly(list);
    }

    private static KoinlyTransaction AdaptTransactionByType(CoinCornerTransaction transaction)
    {
        return transaction.Type switch
        {
            "Trade" => AdaptTrade(transaction),
            "Bank deposit" => AdaptDeposit(transaction),
            _ => throw new Exception($"Unknown transaction type {transaction.Type}")
        };
    }

    private static KoinlyTransaction AdaptDeposit(CoinCornerTransaction transaction)
    {
        var koinlyTransaction = new KoinlyTransaction
        {
            Date = DateTime.Parse(transaction.DateAndTime),
            ReceivedAmount = transaction.Net,
            ReceivedCurrency = transaction.NetCurrency,
            NetWorthAmount = transaction.Net,
            NetWorthCurrency = transaction.NetCurrency,
            Label = "Bank Deposit",
            Description = string.Empty,
            TxHash = string.Empty
        };

        return koinlyTransaction;
    }

    private static KoinlyTransaction AdaptTrade(CoinCornerTransaction transaction)
    {
        var koinlyTransaction = new KoinlyTransaction
        {
            Date = DateTime.Parse(transaction.DateAndTime),
            SentAmount = transaction.Gross,
            SentCurrency = transaction.GrossCurrency,
            ReceivedAmount = transaction.Amount,
            ReceivedCurrency = transaction.Currency,
            FeeAmount = transaction.Fee,
            FeeCurrency = transaction.FeeCurrency,
            NetWorthAmount = transaction.Net,
            NetWorthCurrency = transaction.NetCurrency,
            Label = "Trade",
            Description = string.Empty,
            TxHash = transaction.TransactionId
        };

        return koinlyTransaction;
    }
}