using System.ComponentModel.DataAnnotations;
using StockSim.Shared;
namespace StockSim.Shared.ApiModels;

// DTO for User response (excludes sensitive data like PasswordHash)
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsAdmin { get; set; }
    // ApiKey is sensitive, maybe return it only on creation?
    // For simplicity, let's exclude it from standard GET responses.
    // If needed, a separate endpoint could allow admin to retrieve/regenerate keys.
    // public string? ApiKey { get; set; }
}

// DTO for User creation request via API
public class CreateUserApiRequest
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)] // Example minimum length
    public string Password { get; set; } = string.Empty; // Plain password for API input

    public decimal CurrentBalance { get; set; } = 10000; // Default balance

    // Admin status creation should likely be admin-only via API
    // bool? IsAdmin; // Optional, if admin wants to create another admin directly
    // Let's keep it simple and only allow admin creation via Admin UI for now.
    // The API will create standard users.
}

// DTO for User update request via API
public class UpdateUserApiRequest
{
    // Id should be in the URL, not the body
    [MaxLength(100)]
    public string? Username { get; set; } // Make nullable for optional update

    [EmailAddress]
    [MaxLength(200)]
    public string? Email { get; set; } // Make nullable

    // Password change should be a separate action or not allowed via this endpoint
    // public string? NewPassword { get; set; }

    // Balance updates might require admin privileges
    public decimal? CurrentBalance { get; set; } // Make nullable

    // Admin status update should be admin-only
    // public bool? IsAdmin { get; set; } // Make nullable
}