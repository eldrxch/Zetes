using System.Net.Http.Json;
using Zetes.API.Models;
using Zetes.Data;

namespace Zetes.Tests;

public class CustomerApi
{
    [Test]
    [Category("Integration")]
    public void Get_ShouldReturnCustomers()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.GetAsync("/api/customers").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);

        var customers = response.Content.ReadFromJsonAsync<List<CustomerDTO>>().Result;
        Assert.That(customers?.Count, Is.GreaterThan(0));
    }

    [Test]
    [Category("Integration")]
    public void Get_ShouldReturnCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.GetAsync("/api/customers/1").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);

        var customer = response.Content.ReadFromJsonAsync<CustomerDTO>().Result;
        Assert.That(customer?.CustomerId, Is.EqualTo(1));
    }

    [Test]
    [Category("Integration")]
    public void Post_ShouldAddCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var customer = new CustomerDTO { 
            FirstName = "Test",
            LastName = "Test",
            Email = "test@mail.com",
            Phone = "1234567890"
        };
        var response = client.PostAsJsonAsync("/api/customers", customer).Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Post_ShouldErrorAddCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var customer = new CustomerDTO { };
        var response = client.PostAsJsonAsync("/api/customers", customer).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);

        customer = new CustomerDTO { 
            FirstName = "John",
            Email = "johnmail"
        };
        response = client.PostAsJsonAsync("/api/customers", customer).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }

    [Test]
    [Category("Integration")]
    public void Put_ShouldUpdateCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var customer = new CustomerDTO
        {
            CustomerId = 1,
            FirstName = "Updated",
            LastName = "Updated",
            Email = "updated.mail@updated.com",
            Phone = "0987654321"
        };
        var response = client.PutAsJsonAsync("/api/customers/1", customer).Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Put_ShouldErrorUpdateCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var customer = new CustomerDTO
        {
            CustomerId = 1,
            FirstName = "T",
            LastName = "T"
        };
        var response = client.PutAsJsonAsync("/api/customers/", customer).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);

        customer = new CustomerDTO { };
        response = client.PutAsJsonAsync("/api/customers/1000000", customer).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }

    [Test]
    [Category("Integration")]
    public void Delete_ShouldDeleteCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.DeleteAsync("/api/customers/1").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Delete_ShouldErrorDeleteCustomer()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        // no customer id
        var response = client.DeleteAsync("/api/customers/").Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);

        // customer id does not exist
        response = client.DeleteAsync("/api/customers/10000000").Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }    
}