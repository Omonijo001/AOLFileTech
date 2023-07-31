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
    public class ManagerManager : IManagerManager
    {
        IUserManager userManager = new UserManager();
        public static List<Manager> managerDB = new List<Manager>();
        string fileManager = @"C:\Users\USER\Desktop\AolFileProject\Files\managerDB.txt";

        public ManagerManager() 
        {
            ReadManagerFromFile();
        }
        private void ReadManagerFromFile()
        {
            if (File.Exists(fileManager))
            {
                if(managerDB.Count == 0)
                {
                    var manager = File.ReadAllLines(fileManager);
                    foreach (var item in manager)
                    {
                        managerDB.Add(Manager.ToManager(item));
                    }
                }
                else
                {
                    managerDB.Clear();
                    var manager = File.ReadAllLines(fileManager);
                    foreach (var item in manager)
                    {
                        managerDB.Add(Manager.ToManager(item));
                    }
                }
                
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "managerDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddManagerToFile(Manager manager)
        {
            using (StreamWriter managers = new StreamWriter(fileManager, true))
            {
                managers.WriteLine(manager.ToString());
            }
        }

        private void RefreshManager()
        {
            using (StreamWriter asd = new StreamWriter(fileManager, true))
            {
                foreach (var item in managerDB)
                {
                    asd.WriteLine(item.ToString());
                }
            }
        }

        private string GenerateStaffNumber()
        {
            Random rnd = new Random();
            var staffNumber = rnd.Next(1000,9999).ToString();
            return $"AOL/STF/{staffNumber}";
        }
        private bool CheckIfExist(string username)
        {
            foreach (var manager in managerDB)
            {
                if(manager.Username == username)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Delete(string username)
        {
            var manager = Get(username);
            if (manager != null)
            {
                manager.IsDeleted = true;
                Console.WriteLine("Manager deleted successfully");
                return true;
            }
            Console.WriteLine("Manager not found");
            return false;
        }

        public Manager Get(int id)
        {
            foreach (var manager in managerDB)
            {
                if (manager.Id == id)
                {
                    return manager;
                }
            }
            return null;
        }

        public Manager Get(string username)
        {
            foreach (var manager in managerDB)
            {
                if(manager.Username == username)
                {
                    return manager;
                }
            }
            return null;
        }

        public List<Manager> GetAll()
        {
            return managerDB;
        }

        public Manager Register(string name, string email, string username, string password, string phoneNumber, string address, string companyName)
        {
            var ManagerExist = CheckIfExist(username);
            if(ManagerExist == false)
            {
                User user = new User(UserManager.userDB.Count +1, name, email, username, password, phoneNumber, address,0, "Manager", false);
                UserManager.userDB.Add(user);
                UserManager.AddUserToFile(user);

                Manager manager = new Manager(managerDB.Count + 1, username, GenerateStaffNumber(), companyName, false);
                managerDB.Add(manager);
                AddManagerToFile(manager);
                return manager;
            }
            Console.WriteLine("Manager already exist");
            return null;
        }

        public Manager Update(string username, string name, string phoneNumber, string address)
        {
            var manager = Get(username);
            if(manager != null)
            {
                if(manager.Username == username)
                {
                    var user = userManager.Get(username);
                    user.Name = name;
                    user.PhoneNumber = phoneNumber;
                    user.Address = address;
                    Console.WriteLine("Update Sucessful");
                    return manager;
                }
                Console.WriteLine("Username doesnot correspond");
            }
            Console.WriteLine("Manager not found");
            return null;

        }
    }
}
