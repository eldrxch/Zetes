using System.ComponentModel.DataAnnotations;

namespace Zetes.Data;

public class Customer
{
    public int CustomerId { get; set; }
    [MinLength(2)]
    [MaxLength(30)]    
    public required string FirstName { get; set; }
    [MinLength(2)]
    [MaxLength(30)]        
    public required string LastName { get; set; }
    [EmailAddress]
    public required string? Email { get; set; }
    [Phone]
    public required string? Phone { get; set; }
    public virtual List<Project>? Projects { get; set; }
}
