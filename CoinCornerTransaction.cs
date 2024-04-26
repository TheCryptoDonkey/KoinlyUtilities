namespace CoinCornerToKoinly;

public class CoinCornerTransaction
{
    public string DateAndTime { get; init; }
    public string StoreOrWebsite { get; set; }
    public string Detail { get; set; }
    public string Type { get; init; }
    public string TransactionId { get; set; }
    public decimal? Price { get; set; }
    public string? PriceCurrency { get; set; }
    public decimal Gross { get; init; }
    public string GrossCurrency { get; init; }
    public decimal Fee { get; init; }
    public string FeeCurrency { get; init; }
    public decimal Net { get; set; }
    public string NetCurrency { get; init; }
    public decimal Balance { get; set; }
    public string BalanceCurrency { get; set; }
    public bool IsBuy { get; set; }
    public decimal? Amount { get; init; }
    public string? Currency { get; init; }
}