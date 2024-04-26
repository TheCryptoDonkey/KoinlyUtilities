namespace CoinCornerToKoinly;

public class KoinlyTransaction
{
    public DateTime Date { get; init; }
    public decimal? SentAmount { get; init; }
    public string? SentCurrency { get; init; }
    public decimal? ReceivedAmount { get; init; }
    public string? ReceivedCurrency { get; init; }
    public decimal? FeeAmount { get; init; }
    public string? FeeCurrency { get; init; }
    public decimal? NetWorthAmount { get; init; }
    public string? NetWorthCurrency { get; init; }
    public string Label { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string TxHash { get; init; } = string.Empty;
}