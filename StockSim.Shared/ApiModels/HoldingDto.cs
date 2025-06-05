namespace StockSim.Shared.ApiModels;

using StockSim.Shared;


// DTO for Holding response
public class HoldingDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserDto? User { get; set; } // Can include user details if needed, or just ID
    public int CompanyId { get; set; }
    public Company? Company { get; set; } // Include company details
    public int SharesQuantity { get; set; }
    public decimal TotalInvested { get; set; }
    public decimal AveragePrice { get; set; } // Calculated property
}