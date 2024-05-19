using System.ComponentModel.DataAnnotations;

namespace Zetes.Data;

public class Project
{    
    public int ProjectId { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    
    [MinLength(2)]
    [MaxLength(30)]    

    public required string Name { get; set; }
    [MinLength(2)]
    [MaxLength(100)]    

    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
