using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLittera
{
    public static class Url
    {
        //private readonly static String BASE = "http://10.0.2.2:3000/";

        private readonly static String BASE = "http://45.55.221.147/api/";

        public readonly static String AUTHENTICATE = BASE + "mordor/authenticate";

        public readonly static String ALLBOOKS = BASE + "books";

        public readonly static String LIBRARYINFO = BASE + "users/{0}/libraryinfo";

        public readonly static String BOOK = BASE + "books/{0}";

        //public readonly static String USER = BASE + "users/{0}";
        public readonly static String USER = BASE + "mordor/me";
    }
}
