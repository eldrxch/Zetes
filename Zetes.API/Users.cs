
using Microsoft.EntityFrameworkCore;
using Zetes.Data;
using Zetes.API.Models;

namespace Zetes.API;

public class Users
{
    private readonly ZetesDBContext _context;

    public Users(ZetesDBContext context)
    {
        _context = context;
    }

    public async Task<IResult> GetUsers()
    {
        var users = await _context.AppUsers
            .ToListAsync();
        return TypedResults.Ok(users.Select(u => (UserDTO)u).ToList());
    }

    public async Task<IResult> DeleteUser(string id)
    {
        try
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return TypedResults.NotFound();
            }
            _context.AppUsers.Remove(user);
            await _context.SaveChangesAsync();
            return TypedResults.NoContent();            
        }
        catch (System.Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}
