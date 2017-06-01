using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLittera.Model
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }
        public String Id_api { get; set; }
        public String Title { get; set; }
        public String Subtitle { get; set; }
        public String Synopsis { get; set; }
        public String Content { get; set; }

        public override string ToString() => Title;
    }
}
