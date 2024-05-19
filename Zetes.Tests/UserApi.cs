using System.Net.Http.Json;
using Zetes.API.Models;
using Zetes.Data;
namespace Zetes.Tests;

public class UserApi
{
    [Test]
    [Category("Integration")]
    public void Get_ShouldReturnUsers()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.GetAsync("/api/users").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);

        var users = response.Content.ReadFromJsonAsync<List<UserDTO>>().Result;
        Assert.That(users?.Count, Is.GreaterThan(0));
    }

    [Test]
    [Category("Integration")]
    public void Delete_ShouldDeleteUser()
    {        
        var application = new ApiFactory();
        var client = application.CreateClient();        
        var userId = "TestUserId1";
        var response = client.DeleteAsync("/api/users/" + userId).Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Delete_ShouldErrorDeleteUser()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.DeleteAsync("/api/users/1000000").Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }

}