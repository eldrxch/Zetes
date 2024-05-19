using System.Net.Http.Json;
using Zetes.API.Models;
using Zetes.Data;

namespace Zetes.Tests;

public class ProjectApi
{
    [Test]
    [Category("Integration")]
    public void Get_ShouldReturnProjects()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.GetAsync("/api/projects").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);

        var projects = response.Content.ReadFromJsonAsync<List<ProjectDTO>>().Result;
        Assert.That(projects?.Count, Is.GreaterThan(0));
    }

    [Test]
    [Category("Integration")]
    public void Get_ShouldReturnProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.GetAsync("/api/projects/1").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);

        var project = response.Content.ReadFromJsonAsync<ProjectDTO>().Result;
        Assert.That(project?.ProjectId, Is.EqualTo(1));
    }

    [Test]
    [Category("Integration")]
    public void Post_ShouldAddProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var project = new ProjectDTO { 
            CustomerId = 1,
            Name = "Test",
            Description = "Test",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };
        var response = client.PostAsJsonAsync("/api/projects", project).Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Post_ShouldErrorAddProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var project = new ProjectDTO { };
        var response = client.PostAsJsonAsync("/api/projects", project).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);

        project = new ProjectDTO {             
            Description = "Test"
        };
        response = client.PostAsJsonAsync("/api/projects", project).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);        
    }


    [Test]
    [Category("Integration")]
    public void Put_ShouldUpdateProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var project = new ProjectDTO
        {
            ProjectId = 1,            
            Name = "Updated",
            Description = "Updated",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)        
        };
        var response = client.PutAsJsonAsync("/api/projects/1", project).Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Put_ShouldErrorUpdateProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var project = new ProjectDTO
        {
            ProjectId = 1,
        };
        var response = client.PutAsJsonAsync("/api/projects/", project).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);

        project = new ProjectDTO { };
        response = client.PutAsJsonAsync("/api/projects/1000000", project).Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }

    [Test]
    [Category("Integration")]
    public void Delete_ShouldDeleteProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.DeleteAsync("/api/projects/1").Result;
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    [Category("Integration")]
    public void Delete_ShouldErrorDeleteProject()
    {
        var application = new ApiFactory();
        var client = application.CreateClient();
        var response = client.DeleteAsync("/api/projects/1000000").Result;
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }
}