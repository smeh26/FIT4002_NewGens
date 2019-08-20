using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;

/*
 * This program is created for creating admin accounts on the server
 * Build and run the binary to create the account. Make sure thhe settings of the database is 
 */

namespace MyNursingFuture.AdminUser
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("//Create a new user administration user for My Nursing Future CMS//");

            string _val = "";
            Console.WriteLine("Enter your username (must be between 6 and 20 characters): ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    if ((char.IsLetter(key.KeyChar) || char.IsNumber(key.KeyChar))&& key.Key != ConsoleKey.Enter)
                    {
                        if (_val.Length < 20)
                        {
                            _val += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                        
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
                if (key.Key == ConsoleKey.Enter && _val.Length > 5)
                {
                    break;
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (true);

            string userName = _val;
            _val = "";
            Console.WriteLine("");
            Console.WriteLine("Enter your password (must be between 6 and 20 characters): ");
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    if (_val.Length < 20)
                    {
                        _val += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
                if (key.Key == ConsoleKey.Enter && _val.Length > 5)
                {
                    break;
                }
            }
            while (true);

            var password = _val;

            var adminManager = new AdministratorsManager();
            var result = adminManager.Insert(
                new AdministratorEntity
                {
                    Password = password,
                    Username = userName,
                    Name = "admin"
                }, true
            );
            Console.WriteLine("");
            Console.WriteLine(result.Success ? "Administrator account created, now can be used" : result.Message);
            Console.ReadKey();
        }
    }
}
