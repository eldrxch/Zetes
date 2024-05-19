using Microsoft.EntityFrameworkCore;
using Zetes.API.Models;
using Zetes.Data;

namespace Zetes.API;

public class Projects
{
    private readonly ZetesDBContext _context;

    public Projects(ZetesDBContext context)
    {
        _context = context;
    }

    public async Task<IResult> GetProjects()
    {
        var projects = await _context.Projects
            .Include(p => p.Customer)
            .ToListAsync();            
        return TypedResults.Ok(projects.Select(p => (ProjectDTO)p).ToList());
    }

    public async Task<IResult> GetProject(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.ProjectId == id);
            
        if (project == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok((ProjectDTO)project);
    }

    public async Task<IResult> AddProject(ProjectDTO project)
    {
        try
        {
            Project trnProject = project;
            _context.Projects.Add(trnProject);
            await _context.SaveChangesAsync();
            project.ProjectId = trnProject.ProjectId;
            return TypedResults.Created($"/api/projects/{project.ProjectId}", project);            
        }
        catch (System.Exception ex)
        {            
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<IResult> UpdateProject(ProjectDTO project)
    {
        try
        {
            var trnProject = await _context.Projects.FindAsync(project.ProjectId);
            if (trnProject == null)
            {
                return TypedResults.NotFound();
            }
            trnProject.Description = project.Description;
            trnProject.EndDate = project.EndDate;
            trnProject.Name = project.Name;
            trnProject.StartDate = project.StartDate;
            await _context.SaveChangesAsync();        
            return TypedResults.Ok(project);                   
        }
        catch (System.Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<IResult> DeleteProject(int id)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return TypedResults.NotFound();
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return TypedResults.NoContent();

        }
        catch (System.Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);                
        }
    }
}