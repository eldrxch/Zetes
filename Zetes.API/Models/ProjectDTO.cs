using Zetes.Data;

namespace Zetes.API.Models;

public class ProjectDTO
{
    public int ProjectId { get; set; }
    public int CustomerId { get; set; }
    public CustomerDTO? Customer { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public static implicit operator ProjectDTO(Project project)
    {
        var customer = project.Customer;
        CustomerDTO? dto = customer != null ? new CustomerDTO 
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone
        } : null;
        
        return new ProjectDTO
        {
            ProjectId = project.ProjectId,
            CustomerId = project.CustomerId,
            Customer = dto,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };
    }

    public static implicit operator Project(ProjectDTO project)
    {
        return new Project
        {
            ProjectId = project.ProjectId,
            CustomerId = project.CustomerId,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };
    }
}