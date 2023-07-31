using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Entities;
using AolFileProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Implementations
{
    public class OrderManager : IOrderManager
    {
        ICartManager cartManager = new CartManager();   
        IUserManager userManager = new UserManager();
        IProductManager productManager = new ProductManager();
        ICompanyManager companyManager = new CompanyManager();
        IManagerManager managerManager = new ManagerManager();

        public static List<Order> orderDB = new List<Order>();
        string fileOrder = @"C:\Users\USER\Desktop\AolFileProject\Files\orderDB.txt";

        public OrderManager() 
        {
            ReadOrderFromFile();
        }

        private void ReadOrderFromFile()
        {
            if (File.Exists(fileOrder))
            {
                if (orderDB.Count == 0)
                {
                    var order = File.ReadAllLines(fileOrder);
                    foreach (var item in order)
                    {
                        orderDB.Add(Order.ToOrder(item));
                    }
                }
                else
                {
                    orderDB.Clear();
                    var order = File.ReadAllLines(fileOrder);
                    foreach (var item in order)
                    {
                        orderDB.Add(Order.ToOrder(item));
                    }
                }
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "orderDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddOrderToFile(Order order)
        {
            using (StreamWriter orders = new StreamWriter(fileOrder, true))
            {
                orders.WriteLine(order.ToString());
            }
        }

        private void RefreshOrder()
        {
            using (StreamWriter asd = new StreamWriter(fileOrder, true))
            {
                foreach (var item in orderDB)
                {
                    asd.WriteLine(item.ToString());
                }
            }
        }
        private string GenerateOrderReferenceNumber()
        {
            Random rnd = new Random();
            var orderRefNumber = rnd.Next(1000, 9999).ToString();
            return $"AOL/ORD/{orderRefNumber}";
        }

        private bool CheckIfExist(string referenceNumber)
        {
            foreach (var order in orderDB)
            {
                if(order.ReferenceNumber == referenceNumber)
                {
                    return true;
                }
            }
            return false ;
        }
        public Order Delivered(string referenceNumber)
        {
            var order = Get(referenceNumber);
            if (order != null)
            {
                order.Status = Status.Delivered;
                return order;
            }
            return null;
        }

        public Order Enroute(string referenceNumber)
        {
            var order = Get(referenceNumber);
            if (order != null)
            {
                order.Status = Status.Enroute;
                return order;
            }
            return null;
        }

        public Order Get(string referenceNumber)
        {
            foreach (var order in orderDB)
            {
                if (order.ReferenceNumber == referenceNumber)
                {
                    return order;
                }
            }
            return null;
        }

        public List<Order> GetAll()
        {
            return orderDB;
        }

        public Order MakeOrder(string username, string cartNumber)
        {
            var user = userManager.Get(username);
            var cart = cartManager.Get(cartNumber);
            foreach(var item in cart.Product)
            {
                var pro = productManager.Get(item.Key);
                var company = companyManager.Get(pro.CompanyName);

            }

            if (user != null)
            {
                if(cart  != null)
                {
                    foreach (var item in cart.Product)
                    {
                        Console.WriteLine($"The name of the product is {item.Key}, Quantity: {item.Value}");
                    }
                    Console.WriteLine($"The total price of cart items is {cart.TotalPrice}");
                    Console.WriteLine($"Your wallet balance is {user.Wallet}");
                    if (user.Wallet > cart.TotalPrice)
                    {
                        user.Wallet -= cart.TotalPrice;
                        Console.WriteLine($"Mr/Mrs {user.Name}, your wallent balance is {user.Wallet}");
                        Order order = new Order(orderDB.Count +1, username, cartNumber, GenerateOrderReferenceNumber(), DateTime.Now, Status.Initiated, false);
                        orderDB.Add(order);
                        AddOrderToFile(order);
                        foreach (var item in cart.Product)
                        {
                            var product = productManager.Get(item.Key);
                            product.Quantity -= item.Value;
                        }
                        cart.IsDeleted = true;
                        return order;
                    }
                    Console.WriteLine("Insufficient Balance");
                    return null;
                }
                Console.WriteLine("Cart not found");
                return null;
            }
            Console.WriteLine("User not found");
            return null;
           
        }
        public Order PackOrder(string referenceNumber)
        {
            var order = Get(referenceNumber);
            if (order != null)
            {
                order.Status = Status.Packed;
                return order;
            }
            return null;
        }

        public Order ReceiveOrder(string referenceNumber)
        {
            var order = Get(referenceNumber);
            if (order != null)
            {
                order.Status = Status.Receive;
                return order;
            }
            return null;
        }
    }
}
