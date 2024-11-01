using HW10;
using HW10.Repository;
using HW10.Service;
using System.Security.Cryptography.X509Certificates;

//var logingcommand = "login --username armin --password 123456";
//var registercommand = "register --username armin --password 123456";
while (true)
{
    Console.WriteLine("***** Wellcome *****");
    Console.Write("Please enter your command: ");
    var command = Console.ReadLine();
    var acc = command.Split(" ");
    var check1 = acc[0].ToLower();
    if (check1 != "login" && check1 != "register")
    {
        Console.WriteLine("Command is invalid");
        continue;
    }
    try
    {
        if (check1 == "login")
        {
            DapperRepasitory dapperRepasitory = new DapperRepasitory();
            bool login = dapperRepasitory.Login(acc[2], acc[4]);
            if (!login)
            {
                Console.WriteLine("The information is not correct!");
                continue;
            }
            else
            {
                Console.WriteLine($"***** Hi {acc[2]} *****");
                Usermeniue();
            }
        }
        else if (check1 == "register")
        {
            DapperRepasitory dapperRepasitory = new DapperRepasitory();
            bool register = dapperRepasitory.Register(acc[2], acc[4]);
            if (!register)
            {
                Console.WriteLine("Oops.... There might be some problems.");
                continue;
            }
            else
            {
                Console.WriteLine("Done.");
                Console.WriteLine("Now please login.");
                continue;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }



    //var parameters = new Dictionary<string, string>();
    //parameters["username"] = "armin";
    //parameters["password"] = "123456";



    void Usermeniue()
    {
        DapperRepasitory dapperRepasitory = new DapperRepasitory();
        Console.Write("Please enter your command: ");
        var usercommand = Console.ReadLine();
        var newacc = usercommand.Split(" ");
        var answer1 = newacc[0].ToLower();
        if (answer1 == "change")
        {
            if (newacc[2] == "available")
            {
                dapperRepasitory.ChangeStatus(acc[2], true);
            }
            else if (newacc[2] == "not available")
            {
                dapperRepasitory.ChangeStatus(acc[2], false);
            }
            else
            {
                throw new Exception("Command is invalid!");
            }
        }
        else if (answer1 == "search")
        {
            var users = dapperRepasitory.Search(newacc[2]);
            foreach (var user in users)
            {
                Console.WriteLine(user.Username+" | "+user.statuse);
            }
            
        }
        else if (answer1 == "changepassword")
        {
            bool res3 = dapperRepasitory.ChangePassword(acc[2], newacc[2], newacc[4]);
        }
        else if (answer1 == "logout")
        {
            InmemoryDB.currentuser = null;
        }
    }
}