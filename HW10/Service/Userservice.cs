using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Service
{
    public class Userservice
    {
        public void ChangeStatus(string username,bool action)
        {
            foreach (var user in InmemoryDB.users)
            {
                if (user.Username==username)
                {
                    user.statuse = action;
                }
            }
        }
        public List<Users> Search(string Partofname)
        {
            return InmemoryDB.users.Where(user => user.Username.StartsWith(Partofname)).ToList();
        }
        public bool ChangePassword(string username,string oldpass, string newpass)
        {
            foreach (var user in InmemoryDB.users)
            {
                if (user.Username==username && user.Password==oldpass)
                {
                    user.Password = newpass;
                    return true;
                }
            }
            Console.WriteLine("user not found");
            return false;
        }
    }
}
