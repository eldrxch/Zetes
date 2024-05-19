using Zetes.Data;

namespace Zetes.API.Models;

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public List<ProjectDTO>? Projects { get; set; }

    public static implicit operator CustomerDTO(Customer customer)
    {
        return new CustomerDTO
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Projects = customer.Projects?.Select(p => (ProjectDTO)p).ToList()
        };
    }

    public static implicit operator Customer(CustomerDTO customer)
    {
        return new Customer
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Projects = customer.Projects?.Select(p => (Project)p).ToList()
        };
    }
}
  

