namespace Common.Services
{
    public interface ISessionStorage
    {
        long UserId { get; } 
        string Token { get; }

    }
}
