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
    public class RiderManager : IRiderManager
    {
        IProductManager productManager = new ProductManager();
        IUserManager userManager = new UserManager();
        public static List<Rider> riderDB = new List<Rider>();
        string fileRider = @"C:\Users\USER\Desktop\AolFileProject\Files\riderDB.txt";

        public RiderManager() 
        {
            ReadRiderFromFile();
        }
        private void ReadRiderFromFile()
        {
            if(File.Exists(fileRider))
            {
                if(riderDB.Count == 0)
                {
                    var rider = File.ReadLines(fileRider);
                    foreach (var item in rider)
                    {
                        riderDB.Add(Rider.ToRider(item));
                    }
                }
                else
                {
                    riderDB.Clear();
                    var rider = File.ReadLines(fileRider);
                    foreach (var item in rider)
                    {
                        riderDB.Add(Rider.ToRider(item));
                    }
                }
                
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "riderDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddRiderToFile(Rider rider)
        {
            using (StreamWriter writer = new StreamWriter(fileRider, true))
            {
                writer.WriteLine(rider.ToString());
            }
        }

        private void RefreshFile()
        {
            using (StreamWriter writer = new StreamWriter(fileRider, true))
            {
                foreach (var item in riderDB)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private string GenerateRiderRegNumber()
        {
            Random rnd = new Random();
            var RegNumber = rnd.Next(1000,9999).ToString();
            return $"AOL/RDR/{RegNumber}";
        }

        private bool CheckIfExist(string username)
        {
            foreach(var rider in riderDB)
            {
                if(rider.Username == username)
                {
                    return true;
                }
            }
            return false ;
        }
        public bool Delete(string riderRegNumber)
        {
            var rider = Get(riderRegNumber);
            if(rider != null)
            {
                rider.IsDeleted = true;
                Console.WriteLine("Rider deleted Successfully");
                return true;
            }
            return false ;
        }

        public Rider Get(int id)
        {
            foreach (var rider in riderDB)
            {
                if(rider.Id == id)
                {
                    return rider;
                }
            }
            return null;
        }

        public Rider Get(string riderRegNumber)
        {
            foreach (var rider in riderDB)
            {
                if(rider.RiderRegNumber == riderRegNumber)
                {
                    return rider;
                }
            }
            return null;
        }

        public List<Rider> GetAll()
        {
            return riderDB;
        }

        public List<Product> GetAllProductAvailable(int ProductId)
        {
            var list = new List<Product>();
            foreach (var rider in riderDB)
            {
                var product = productManager.Get(ProductId);
                if (product.IsAvailable == true)
                {
                    list.Add(product);
                }
            }
            return list;
        }

        public Rider Register(string name, string email, string username, string password, string phoneNumber, string plateNumber, string address)
        {
            var riderExist = CheckIfExist(username);
            if (riderExist == false)
            {
                User user = new User(UserManager.userDB.Count + 1, name, email, username, password, phoneNumber, address, 0, "Rider", false);
                UserManager.userDB.Add(user);   
                UserManager.AddUserToFile(user);

                Rider rider = new Rider(riderDB.Count + 1, username, plateNumber, GenerateRiderRegNumber(), false);
                riderDB.Add(rider);
                AddRiderToFile(rider);
                return rider;
            }
            return null;
        }

        public Rider Update(string riderRegNumber, string name, string plateNumber, string phoneNumber, string address)
        {
            var rider = Get(riderRegNumber);
            if (rider != null) 
            {
                if(rider.RiderRegNumber ==  riderRegNumber)
                {
                    var user = userManager.Get(riderRegNumber);
                    user.Name = name;
                    rider.PlateNumber = plateNumber;
                    user.PhoneNumber = phoneNumber;
                    user.Address = address;
                    Console.WriteLine("Update Successful");
                    return rider;
                }
                Console.WriteLine("rider doesnt correspond");
            }
            Console.WriteLine("Rider not found");
            return null;
        }
    }
}
