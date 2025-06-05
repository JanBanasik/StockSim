using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockSim.Shared;

public class Holding
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public User? User { get; set; }

    [Required]
    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    public Company? Company { get; set; }

    [Required]
    public int SharesQuantity { get; set; }

    [Required]
    public decimal TotalInvested { get; set; }
    
    [NotMapped]
    public decimal AveragePrice => SharesQuantity > 0 ? TotalInvested / SharesQuantity : 0;

    public override string ToString()
    {
        return $"Company: {CompanyId} - User: {UserId} - Shares: {SharesQuantity} - Invested: {TotalInvested:0.00}";
    }
}