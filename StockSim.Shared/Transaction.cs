using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StockSim.Shared;

namespace StockSim.Shared;

public class Transaction
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
    public int Quantity { get; set; }

    [Required]
    public decimal PricePerShare { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.Now;

    [Required]
    public TransactionType Type { get; set; }
}
