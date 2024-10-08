namespace WebApi.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    
    public string PasswordHash { get; set; }
    
    public List<RefreshToken> RefreshTokens { get; set; }
}