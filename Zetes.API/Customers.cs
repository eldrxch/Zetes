using Microsoft.EntityFrameworkCore;
using Zetes.API.Models;
using Zetes.Data;

namespace Zetes.API;

public class Customers
{
    private readonly ZetesDBContext _context;

    public Customers(ZetesDBContext context)
    {
        _context = context;
    }

    public async Task<IResult> GetCustomers()
    {
        var customers = await _context.Customers
            .ToListAsync();
        return TypedResults.Ok(customers.Select(c => (CustomerDTO)c).ToList());
    }

    public async Task<IResult> GetCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok((CustomerDTO)customer);
    }

    public async Task<IResult> AddCustomer(CustomerDTO customer)
    {
        try
        {
            Customer trnCustomer = customer;
            _context.Customers.Add(trnCustomer);
            await _context.SaveChangesAsync();
            customer.CustomerId = trnCustomer.CustomerId;
            return TypedResults.Created($"/api/customers/{customer.CustomerId}", customer);            
        }
        catch (System.Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<IResult> UpdateCustomer(Customer customer)
    {
        try
        {
            var trnCustomer = await _context.Customers.FindAsync(customer.CustomerId);
            if (trnCustomer == null)
            {
                return TypedResults.NotFound();
            }
            trnCustomer.Email = customer.Email;
            trnCustomer.FirstName = customer.FirstName;
            trnCustomer.LastName = customer.LastName;
            trnCustomer.Phone = customer.Phone;
            await _context.SaveChangesAsync();
            return TypedResults.Ok(trnCustomer);            
        }
        catch (System.Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<IResult> DeleteCustomer(int id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return TypedResults.NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return TypedResults.NoContent();            
        }
        catch (System.Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}