using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteTutorial4.Models;

namespace SQLiteTutorial4.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ImageItem>().Wait();
        }
        // CRUD Operations // All Images , first 
        
        public Task<List<ImageItem>> GetImagesAsync()
        {
            return _database.Table<ImageItem>()
                            .OrderByDescending(i => i.CreatedAt)
                            .ToListAsync();
        }
        public Task<ImageItem> GetImageByIdAsync(int id)
        {
            return _database.Table<ImageItem>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }
        public Task<int> SaveImageAsync(ImageItem item)
        {
            return _database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteImageAsync(ImageItem item)
        {
            return _database.DeleteAsync(item);
        }

    }
}
