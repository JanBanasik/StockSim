using StockSim.Shared;

namespace StockSim.Shared.ApiModels;

// DTO for Transaction response
public class TransactionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; } // Include company details
    public int Quantity { get; set; }
    public decimal PricePerShare { get; set; }
    public DateTime Timestamp { get; set; }
    public TransactionType Type { get; set; }
}