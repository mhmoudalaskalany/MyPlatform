namespace Domain.Services
{
    public interface ISessionStorage
    {
        long UserId { get; } 
        string Token { get; }

    }
}
