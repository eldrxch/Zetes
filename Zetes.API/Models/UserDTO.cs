using Zetes.Data;

namespace Zetes.API.Models;

public class UserDTO
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }

    public static implicit operator UserDTO(AppUser user)
    {
        return new UserDTO
        {
            UserId = user.Id,
            Email = user.Email,
            Username = user.UserName
        };
    }

    public static implicit operator AppUser(UserDTO user)
    {
        return new AppUser
        {
            Id = user.UserId,
            Email = user.Email,
            UserName = user.Username
        };
    }
}