namespace WebApi.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    
    // The bcrypted JWT Token must match the PasswordHash
    public string PasswordHash { get; set; }
    
    // TEST - 19-04-2024 - Now the List of RefreshTokens will be visible
    public List<RefreshToken> RefreshTokens { get; set; }
}