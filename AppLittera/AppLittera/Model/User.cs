using SQLite;

namespace AppLittera.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Id_api { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string CoverImage { get; set; }
    }
}
