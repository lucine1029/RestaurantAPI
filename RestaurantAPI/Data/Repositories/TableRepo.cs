using RestaurantAPI.Models;
using RestaurantAPI.Data;
using RestaurantAPI.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace RestaurantAPI.Data.Repositories
{
    public class TableRepo : ITableRepo
    {
        private readonly ApplicationDbContext _context;
        public TableRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddTableAsync(Table table)
        {
            await _context.Table.AddAsync(table);
            await _context.SaveChangesAsync();
            return table.Id;
        }

        public async Task<bool> DeleteTableAsync(int tableId) 
        {
            var rowsAffected = await _context.Table
                .Where(t => t.Id == tableId)
                .ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Table>> GetAllTablesAsync()
        {
            var tables = await _context.Table.ToListAsync();
            return tables;
        }

        //public async Task<Table> GetAvailableTablesAsync(int numOfGuest, DateTime requiredStartTime, DateTime requiredEndTime)
        //{
        //    var availableTable = await _context.Table
        //        .Where (t => t.Capacity == numOfGuest)
        //        .Where (t => !t.Bookings.Any(b=> 
        //        (b.EndTime > requiredStartTime) && (b.StartTime < requiredEndTime)))
        //        .OrderByDescending(t => t.Capacity).FirstOrDefajltAsync();
        //    return availableTable;
        //}
       
        //public async Task<Table> GetAvailableTablesAsync(int numOfGuest, DateTime requiredStartTime, DateTime requiredEndTime)
        //{
        //    var availableTable = await _context.Table




        //    //var availableTable = await _context.Table
        //    //    .Where(t => t.Capacity >= numOfGuest)
        //    //    .Where(t => !_context.Booking
        //    //        .Where(b => b.BookingDate == DateTime.Today) // Assuming we are checking for today's date
        //    //        .Where(b => (b.StartDateTime < requiredEndTime) && (b.EndDateTime > requiredStartTime))
        //    //        .Select(b => b.FK_TableId)
        //    //        .Contains(t.Id))
        //    //    .FirstOrDefaultAsync();
        //    //return availableTable;

        //}

        public async Task<Table> GetTableByIdAsync(int tableId)
        {
            var table = await _context.Table.FirstOrDefaultAsync(t => t.Id == tableId);
            return table;
        }

        public async Task<Table> GetTableByTableNumberAsync(int tableNumber)
        {
            var table = await _context.Table.FirstOrDefaultAsync(t => t.TableNumber == tableNumber);
            return table;
        }

        public async Task<bool> UpdateTableAsync(Table table)
        {
            _context.Table.Update(table);
            var result = await _context.SaveChangesAsync();
            return true;
        }
    }
}
