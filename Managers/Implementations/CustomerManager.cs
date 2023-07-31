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
    public class CustomerManager : ICustomerManager
    {
        IUserManager userManager = new UserManager();
        public static List<Customer> customerDB = new List<Customer>();
        string fileCustomer = @"C:\Users\USER\Desktop\AolFileProject\Files\customerDB.txt";

        public CustomerManager()
        {
            ReadCustomerFromFile();
        }
        private void ReadCustomerFromFile()
        {
            if(File.Exists(fileCustomer))
            {
                if (customerDB.Count == 0)
                {
                    var customer = File.ReadAllLines(fileCustomer);
                    foreach (var item in customer)
                    {
                        customerDB.Add(Customer.ToCustomer(item));
                    }
                }
                else
                {
                    customerDB.Clear();
                    var customer = File.ReadAllLines(fileCustomer);
                    foreach (var item in customer)
                    {
                        customerDB.Add(Customer.ToCustomer(item));
                    }
                }
                
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "customerDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddCustomerToFile(Customer customer)
        {
            using(StreamWriter aad = new StreamWriter(fileCustomer,true))
            {
                aad.WriteLine(customer.ToString());
            }
        }

        public void RefreshFile()
        {
            using (StreamWriter asd = new StreamWriter(fileCustomer, true))
            {
                foreach (var item in customerDB)
                {
                    asd.WriteLine(item.ToString());
                }
            }
        }
        private bool CheckIfExist(string username)
        {
            foreach (var customer in customerDB)
            {
                if (customer.Username == username)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Delete(string username)
        {
            var customer = Get(username);
            if (customer != null)
            {
                customer.IsDeleted = true;
                Console.WriteLine("Customer deleted successfully");
                return true;
            }
            Console.WriteLine("customer not found");
            return false;

        }

        public Customer Get(int id)
        {
            foreach (var customer in customerDB)
            {
                if(customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer Get(string username)
        {
            foreach (var customer in customerDB)
            {
                if(customer.Username == username)
                {
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> GetAll()
        {
            return customerDB;
        }

        public Customer Register(string name, string email, string username, string password, string phoneNumber, string address)
        {
            var customerExist = CheckIfExist(username);
            if(customerExist == false)
            {
                User user = new User(UserManager.userDB.Count + 1, name, email, username, password, phoneNumber, address, 0, "Customer", false);
                UserManager.userDB.Add(user);
                UserManager.AddUserToFile(user);

                Customer customer = new Customer(customerDB.Count + 1, username, false);
                customerDB.Add(customer);
                AddCustomerToFile(customer);
                return customer;
            }
            Console.WriteLine("Customer already exist");
            return null;
        }

        public Customer Update(string username, string name, string phoneNumber, string address)
        {
            var customer = Get(username);
            if( customer != null )
            {
                if(customer.Username == username)
                {
                    var user = userManager.Get(username);
                    user.Name = name;
                    user.PhoneNumber = phoneNumber;
                    user.Address = address;
                    Console.WriteLine("Update Sucessful");
                    return customer;
                }
                Console.WriteLine("Username doesnot correspond");
            }
            Console.WriteLine("Customer not found");
            return null;
        }
    }
}
