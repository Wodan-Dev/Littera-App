using AppLittera.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLittera.Database
{
    public class DBBook
    {
        readonly SQLiteAsyncConnection database;

        public DBBook(String dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Book>().Wait();
        }

        public Task<List<Book>> GetBookAsync()
        {
            return database.Table<Book>().ToListAsync();
        }

        public Task<Book> GetBookAsync(Int32 id)
        {
            return database.Table<Book>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Book> GetBookAsync(String idApi)
        {
            return database.Table<Book>().Where(i => i.Id_api == idApi).FirstOrDefaultAsync();
        }

        public Task<int> SaveBookAsync(Book book)
        {
            if (book.ID != 0)
                return database.UpdateAsync(book);
            else
                return database.InsertAsync(book);
        }

        public Task<int> DeleteBookAsync(Book book)
        {
            return database.DeleteAsync(book);
        }
    }
}
