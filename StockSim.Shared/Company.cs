using System.ComponentModel.DataAnnotations;

namespace StockSim.Shared;

public class Company
{
    [Key]
    public int Id { get; set; }

    public string? Ticker { get; set; } = string.Empty;

    public string? Name { get; set; } = string.Empty;

    public decimal CurrentPrice { get; set; }

    public DateTime LastUpdated { get; set; }

    public ICollection<Holding>? Holdings { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }


    public override string ToString()
    {
        return "Company: " + Id + " - " + Name + " - " + CurrentPrice;
    }
}