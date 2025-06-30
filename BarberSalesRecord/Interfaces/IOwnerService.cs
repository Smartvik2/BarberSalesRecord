namespace BarberSalesRecord.Interfaces
{
    public interface IOwnerService
    {
        Task<string> ApproveSecretaryAsync(string userId);
        Task<bool> DeleteBarberAsync(int id);
        Task<bool> DeleteSecretaryByIdAsync(string userId);

    }
}
