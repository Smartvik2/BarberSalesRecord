using BarberSalesRecord.DTOs;

namespace BarberSalesRecord.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        //Task<string> ApproveSecretaryAsync(string userId);
    }
}
