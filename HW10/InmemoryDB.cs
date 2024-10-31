using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10
{
    public static class InmemoryDB
    {
        public static List<Users> users { get; set; } = new List<Users>();
        public static Users currentuser { get; set; }

        static InmemoryDB()
        {
            users.Add(new Users { Username = "armin", Password = "123456", statuse = false });
            users.Add(new Users { Username = "ali", Password = "123456", statuse = false });
            users.Add(new Users { Username = "maryam", Password = "123456", statuse = false });
        }
    }
}
