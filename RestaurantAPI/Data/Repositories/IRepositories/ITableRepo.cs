
using RestaurantAPI.Models;

namespace RestaurantAPI.Data.Repositories.IRepositories
{
    public interface ITableRepo
    {
        Task<List<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(int tableId);
        Task<Table> GetTableByTableNumberAsync(int tableNumber);

        /*Task<Table> GetAvailableTablesAsync(int numOfGuest, DateTime requiredStartTime,DateTime requiredEndTime);*/// check available table for given time range
        Task<int> AddTableAsync(Table table);
        Task<bool> UpdateTableAsync(Table table);
        Task<bool> DeleteTableAsync(int tableId);

    }
}
