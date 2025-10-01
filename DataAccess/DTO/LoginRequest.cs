using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO;
public class LoginRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
