namespace BookManagementAPI.Helpers
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username);
    }
}

