using System.Text;

namespace CoinCornerToKoinly;

public class Koinly(List<KoinlyTransaction> koinlyTransactionsList)
{
    public void CreateFile()
    {
        var csv = new StringBuilder();
        csv.AppendLine(
            "Date, Sent Amount, Sent Currency, Received Amount, Received Currency, Fee Amount, Fee Currency, Net Worth Amount, Net Worth Currency, Label, Description, Tx Hash");

        foreach (var transaction in koinlyTransactionsList)
        {
            var newLine =
                $"{transaction.Date:yyyy-MM-dd HH:mm:ss} UTC, {transaction.SentAmount}, {transaction.SentCurrency}, {transaction.ReceivedAmount}, {transaction.ReceivedCurrency}, {transaction.FeeAmount}, {transaction.FeeCurrency}, {transaction.NetWorthAmount}, {transaction.NetWorthCurrency}, {transaction.Label}, {transaction.Description}, {transaction.TxHash}";
            csv.AppendLine(newLine);
        }

        File.WriteAllText("KoinlyTransactions.csv", csv.ToString());
    }
}