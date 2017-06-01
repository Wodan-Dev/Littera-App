using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using AppLittera.Model;

namespace AppLittera.Database
{
    public class DBUser
    {
        readonly SQLiteAsyncConnection database;

        public DBUser(String dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(Int32 id)
        {
            return database.Table<User>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.ID != 0)
                return database.UpdateAsync(user);
            else
                return database.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return database.DeleteAsync(user);
        }

    }
}
