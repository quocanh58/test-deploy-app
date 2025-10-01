namespace DataAccess.DTO;
public class AccessTokenResponse
{
    public string TokenType { get; } = "Bearer";
    public required string AccessToken { get; init; }
    public required long ExpiresIn { get; init; }
}
