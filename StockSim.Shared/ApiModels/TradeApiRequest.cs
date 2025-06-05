using System.ComponentModel.DataAnnotations;

namespace StockSim.Shared.ApiModels;

// DTO for Trade (Buy/Sell) request
public class TradeApiRequest
{
    [Required]
    public string? Ticker { get; set; } // Use Ticker to identify the company

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}

// DTO for Trade response (optional, could return user/holding/transaction details)
// For simplicity, let's just return a success message or updated balance/holding via a generic success object.
// Or maybe return the created transaction?
public class TradeApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public decimal NewBalance { get; set; }
    // Optionally include updated holding details or transaction details
    // public HoldingDto? UpdatedHolding { get; set; }
    // public TransactionDto? NewTransaction { get; set; }
}