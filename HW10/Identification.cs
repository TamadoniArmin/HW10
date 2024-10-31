using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HW10
{
    public class Identification
    {
        public bool Login(string username,string password)
        {
            foreach (var user in InmemoryDB.users)
            {
                if (user.Username==username && user.Password==password)
                {
                    InmemoryDB.currentuser = user;
                    return true;
                }
            }
            return false;
        }
        public bool Register(string username, string password)
        {
            foreach (var user in InmemoryDB.users)
            {
                if (user.Username==username)
                {
                    throw new Exception("Error! This username is already taken.");
                    return false;
                }
            }
            Users users = new Users();
            users.Username = username;
            users.Password = password;
            InmemoryDB.users.Add(users);
            return true;
        }
    }
}
