using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Implementations
{
    public class UserManager : IUserManager
    {
        public static List<User> userDB = new List<User>();
       static  string fileUser = @"C:\Users\USER\Desktop\AolFileProject\Files\userDB.txt";

        public UserManager() 
        {
            ReadUserFromFile();
        }

        private void ReadUserFromFile()
        {
            if (File.Exists(fileUser))
            {
                if(userDB.Count == 0)
                {
                    var users = File.ReadAllLines(fileUser);
                    foreach (var user in users)
                    {
                        userDB.Add(User.ToUser(user));
                    }
                }
                else
                {
                    userDB.Clear();
                    var users = File.ReadAllLines(fileUser);
                    foreach (var user in users)
                    {
                        userDB.Add(User.ToUser(user));
                    }
                }
               
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "userDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        public static void AddUserToFile(User user)
        {
            using(StreamWriter users = new StreamWriter(fileUser, true))
            {
                users.WriteLine(user.ToString());
            }
        }

        private void RefreshFile()
        {
            using (StreamWriter users = new StreamWriter(fileUser, true))
            {
                foreach (var item in userDB)
                {
                    users.WriteLine(item.ToString());
                }
            }
        }

        private bool CheckIfExist(string username)
        {
            foreach (var user in userDB)
            {
                if(user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Delete(string username)
        {
            var user = Get(username);
            if(user != null)
            {
                user.IsDeleted = true;
                Console.WriteLine("User Deleted Successfull");
                return true;
            }
            return false;
        }

        public User FundWallet(string username, double amount)
        {
            var user = Get(username);
            if(user != null)
            {
                if(user.Username == username)
                {
                    if(amount > 0)
                    {
                        user.Wallet += amount;
                        Console.WriteLine($"Mr/Mrs {user.Name} you fund {amount} to your wallet and your wallet balance is {user.Wallet}");
                        return user;
                    }
                    Console.WriteLine("Invalid amount");
                    return null;
                }
                Console.WriteLine("Username not found");
            }
            Console.WriteLine("User not found");
            return null;
        }

        public User Get(int id)
        {
            foreach (var user in userDB)
            {
                if(user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public User Get(string username)
        {
            foreach (var user in userDB)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return null;
        }

        public List<User> GetAll()
        {
            return userDB;
        }

        public User Login(string username, string password)
        {
            var user = Get(username);
            if (user != null)
            {
                if (user.Username == username && user.Password == password)
                {
                    Console.WriteLine("Login Successfull");
                    return user;
                }
                else
                {
                    Console.WriteLine("Incorrect username or password");
                }
            }
            Console.WriteLine("User not found");
            return null;
        }

        public User Update(string username, string name, string phoneNumber, string address)
        {
            var user = Get(username);
            if(user != null && user.Username == username)
            {
                user.Name = name;
                user.PhoneNumber = phoneNumber;
                user.Address = address;
                Console.WriteLine("Update successful");
                return user;
            }
            Console.WriteLine("User not found");
            return null;
        }
    }
}
