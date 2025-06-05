using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockSim.Shared;

public class StockHistory
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CompanyId { get; set; }
    
    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}