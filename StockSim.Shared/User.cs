// Models/User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Dodaj to, jeśli używasz NotMapped w Holding

namespace StockSim.Shared
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public decimal CurrentBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreatedAt { get; set; }

        public bool IsAdmin { get; set; } = false;
        public string? ApiKey { get; set; }

        public virtual ICollection<Holding>? Holdings { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}