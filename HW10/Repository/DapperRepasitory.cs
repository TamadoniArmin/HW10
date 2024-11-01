using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW10.Repository
{
    public class DapperRepasitory
    {
        string connectionstrting = @"Data Source=DESKTOP-6RE5DJR\SQLEXPRESS;Initial Catalog=MaktabDB;Integrated Security=True;TrustServerCertificate=True;";
        //string sql = $"select * from dbo.Users where Username='{username}'";
        public List<Users> GetUsers()
        {
            using var cn = new SqlConnection(connectionstrting);
            var sql = $"Select * from dbo.Users";
            var cmd = new CommandDefinition(sql);
            var result = cn.Query<Users>(cmd);
            return result.ToList();
        }
        public bool Register(string username, string Password)
        {
            try
            {
                using (var cn = new SqlConnection(connectionstrting))
                {
                    string sql = $"select * from dbo.Users where Username=@Username";
                    var command = new CommandDefinition(sql, new { Username = username });
                    var result = cn.QueryFirstOrDefault<Users>(command);
                    if (result is not null)
                    {
                        Console.WriteLine("This username is alrady taken");
                        return false;
                    }
                    else
                    {
                        string newsql = "INSERT INTO dbo.Users(Username, Password,status) VALUES (@Username,@Password,@status)";
                        var newcommand = new CommandDefinition(newsql, new { Username = username, Password= Password,status=0 });
                        cn.Execute(newcommand);
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong! Please try again");
            }
            
        }
        public bool Login(string username, string password)
        {
            try
            {
                using (var cn = new SqlConnection(connectionstrting))
                {
                    string sql = $"select * from dbo.Users where Username=@Username";
                    var command = new CommandDefinition(sql, new { Username = username });
                    var result = cn.QueryFirstOrDefault<Users>(command);
                    if (result is not null)
                    {
                        Console.WriteLine("Login successfully.");
                        InmemoryDB.currentuser = result;
                        return true;
                    }
                    Console.WriteLine("Login Failed!");
                    return false;
                }
            }
            catch (Exception)
            {

                throw new Exception("you can not login now");
            }
        }
        public List<Users> Search(string username)
        {
            string sql = $"select * from dbo.Users where Username like @Username + '%'";
            using var cn = new SqlConnection(connectionstrting);
            var command = new CommandDefinition(sql,new { Username = username});
            var result = cn.Query<Users>(command);
            return result.ToList();
        }
        public bool ChangePassword(string username,string oldpassword, string newpassword)
        {
            try
            {
                string sql = $"select * from dbo.Users where Username = @Username and Password= @Password";
                using (var cn = new SqlConnection(connectionstrting))
                {
                    var command = new CommandDefinition(sql,new { Username =username, Password = oldpassword });
                    var result = cn.QueryFirstOrDefault<string>(command);
                    if (result is null)
                    {
                        Console.WriteLine("There is no user with this information");
                        return false;
                    }
                    else
                    {
                        string newsql = $"Update dbo.Users set Password = @Password where Username = @Username";
                        var Updatecommand = new CommandDefinition(newsql, new { Password = newpassword, Username = username});
                        cn.Execute(Updatecommand);
                        return true;
                    }

                }

            }
            catch (Exception)
            {

                throw new Exception("Oop.... Something went wrong!");
            }
        }
        public bool ChangeStatus(string username, bool action)
        {
            try
            {
                using (var connection = new SqlConnection(connectionstrting))
                {
                    if (action)
                    {
                        connection.Execute("Update dbo.Users set status = @number where Username = @Username",new { number=1, Username =username});
                    }
                    else
                    {
                        connection.Execute("Update dbo.Users set status = @number where Username = @Username", new { number=0, Username = username });
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw new Exception("Error! You can not change your status.");
            }
        }

    }
}
