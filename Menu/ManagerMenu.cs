using AolFileProject.Managers.Implementations;
using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Menu
{
    public class ManagerMenu
    {
        IManagerManager managerManager = new ManagerManager();
        IUserManager userManager = new UserManager();
        IBrandManager brandManager = new BrandManager();
        IRiderManager riderManager = new RiderManager();
        ICompanyManager companyManager = new CompanyManager();
        IProductManager productManager = new ProductManager();
        IOrderManager orderManager = new OrderManager();
        ICartManager cartManager = new CartManager();

        public void ManagerMain()
        {
            Console.WriteLine("Enter 1 to register brand\nEnter 2 to create product\nEnter 3 to view all rider\nEnter 4 to view all product\nEnter 5 to view all product available\nEnter 6 to view all purchase\nEnter 7 to change Order status\nEnter 8 to view all delivered orders\nEnter 9 to logout");
            int option = int.Parse(Console.ReadLine());
            if(option == 1)
            {
                RegisterBrand();
                ManagerMain();
            }
            else if(option == 2)
            {
                CreateProduct();
                ManagerMain();
            }
            else if(option == 3)
            {
                ViewAllRider();
                ManagerMain();
            }
            else if(option == 4)
            {
                ViewAllProduct();
                ManagerMain();
            }
            else if (option == 5)
            {
                ViewAllProductAvailable();
                ManagerMain();
            }
            else if (option == 6)
            {
                ViewAllPurchase();
                ManagerMain();
            }
            else if (option == 7)
            {
                ChangeOrderStatus();
                ManagerMain();
            }
            else if(option == 8)
            {
                ViewAllDeliveredOrders();
                ManagerMain();
            }
            else if(option == 9)
            {
                Logout();
                ManagerMain();
            }
            else
            {
                Console.WriteLine("You enter an invalid option!!!Kindly enter a valid option");
                ManagerMain();
            }
        }

        public void RegisterBrand()
        {
            var brands = brandManager.GetAll();
            Console.WriteLine("Existing brands are: ");
            foreach (var item in brands)
            {
                Console.WriteLine($"{item.Name}");
            }
            Console.Write("do you want to register a new brand: y/n");
            char opt = char.Parse(Console.ReadLine());
            if(opt == 'y')
            {
                Console.Write("Enter the Brand Name you want to register: ");
                string name = Console.ReadLine();
                var brand = brandManager.Create(name);
                if (brand != null)
                {
                    Console.WriteLine(" Brand registered successfull");

                }
                else
                {
                    Console.WriteLine("Brand already exist");
                }
            }
            ManagerMain();


        }
        public void CreateProduct()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter the product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the ram of the product: ");
            int ram = int.Parse(Console.ReadLine());
            Console.Write("Enter the rom of the product: ");
            int rom = int.Parse(Console.ReadLine());
            Console.Write("Enter the price of the product: ");
            double price = double.Parse(Console.ReadLine());
            Console.Write("Enter the quantity of the product: ");
            int quantity = int.Parse(Console.ReadLine());
            var brands = brandManager.GetAll();
            foreach (var brand in brands)
            {
                Console.Write($"Enter {brand.Name} to choose the brand name of the product: ");
            } 
            string brandName = Console.ReadLine();
            var companyName = managerManager.Get(username).CompanyName;
           
            var product = productManager.Create(name,ram,rom,price,quantity,brandName,companyName);
            if(product != null)
            {
                Console.WriteLine("Product registered successfully");

            }
            else
            {
                Console.WriteLine("Product already exist");
            }

        }
        public void ViewAllRider()
        {
            var rider = riderManager.GetAll();
            foreach (var item in rider)
            {
                Console.WriteLine($"{item.Username}\t{item.RiderRegNumber}\t{item.PlateNumber}");
            }
        }
        public void ViewAllProduct()
        {
            Console.Write("Enter your company Name :  ");
            var company = Console.ReadLine();
            var products = productManager.GetProductsByCompanyName(company);
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}\t{product.BrandName}\t{product.CompanyName}\t{product.Quantity}\t{product.Price}");
            }
           
            
        }
        public void ViewAllProductAvailable()
        {
            Console.Write("Enter your Company Name :  ");
            string companyName = Console.ReadLine();
            var products = productManager.GetProductsByCompanyName(companyName);
            foreach (var item in products)
            {
                if(item.IsAvailable == true)
                {
                    Console.WriteLine($"{item.CompanyName}\t{item.Name}\t{item.BrandName}\t{item.Quantity}\t{item.Price}");
                }
            }
        }
        public void ViewAllPurchase()
        {
            var purchase = orderManager.GetAll();
            foreach (var item in purchase)
            {
                if (item.Status == Status.Initiated || item.Status == Status.Packed || item.Status == Status.Delivered)
                {
                      Console.WriteLine($"{item.Username}\t{item.ReferenceNumber}\t{item.Date}\t{item.Status}");
                }
            }
            
        }
        public void ChangeOrderStatus()
        {
            Console.WriteLine("Enter your Username");
            string userName = Console.ReadLine();
            var managerx = managerManager.Get(userName);
            var purchase = orderManager.GetAll();
            foreach (var item in purchase)
            {
                if (item.Status == Status.Initiated )
                {
                    var cart = cartManager.Get(item.CartNumber);
                    foreach(var item2 in cart.Product)
                    {
                        var product = productManager.Get(item2.Key);
                        if(product.CompanyName == managerx.CompanyName)
                        {
                            Console.WriteLine($"{item.Username}\t{item.ReferenceNumber}\t{item.Date}");
                            Console.WriteLine($"Enter {item.ReferenceNumber} the reference number to pack the order");
                        }
                    }
                    string refrenceNumber = Console.ReadLine();
                    orderManager.PackOrder(refrenceNumber);
                    Console.WriteLine("Your order as been packed successfully\nAvailable for pickup");
                }
            }
        }

        public void ViewAllDeliveredOrders()
        {
            var delivered = orderManager.GetAll();
            foreach (var order in delivered)
            {
                if(order.Status == Status.Delivered)
                {
                    var rider = riderManager.GetAll();
                    foreach (var item in rider)
                    {
                        Console.WriteLine($"{item.RiderRegNumber} delivered {order.ReferenceNumber}");
                    }
                   
                }
            }
        }
        public void Logout()
        {
            Console.WriteLine("Logout successfully");
            MainMenu mainMenu = new MainMenu();
            mainMenu.Menu();
        }
    }
}
