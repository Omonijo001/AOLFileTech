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
    public class SuperAdminMenu
    {
        IOrderManager orderManager = new OrderManager();
        IManagerManager managerManager = new ManagerManager();
        ICustomerManager customerManager = new CustomerManager();
        ICartManager cartManager = new CartManager();
        IProductManager productManager = new ProductManager();
        IRiderManager riderManager = new RiderManager();
        ICompanyManager companyManager = new CompanyManager();
        IUserManager userManager = new UserManager();


        public void SuperAdminMain()
        {
            Console.WriteLine("Enter 1 to Register Rider\nEnter 2 to view all Manager\nEnter 3 to view all Customer\nEnter 4 to view all Rider\nEnter 5 to view all company\nEnter 6 to view all Products\nEnter 7 to view all Products Available\nEnter 8 to view all Orders\nEnter 9 to Logout ");
            int option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                RegisterRider();
                SuperAdminMain();
            }
            else if (option == 2)
            {
                ViewAllManager();
                SuperAdminMain();
            }
            else if (option == 3)
            {
                ViewAllCustomer();
                SuperAdminMain();
            }
            else if (option == 4)
            {
                ViewAllRider();
                SuperAdminMain();
            }
            else if (option == 5)
            {
                ViewAllCompany();
                SuperAdminMain();
            }
            else if (option == 6)
            {
                ViewAllProduct();
                SuperAdminMain();
            }
            else if (option == 7)
            {
                ViewAllProductAvailable();
                SuperAdminMain();
            }
            else if (option == 8)
            {
                ViewAllOrder();
                SuperAdminMain();
            }
            else if (option == 9)
            {
                Logout();
                SuperAdminMain();
            }
            else
            {
                Console.WriteLine("You entered an invalid option\nKindly Enter a valid option");
                SuperAdminMain();
            }
        }

        public void RegisterManager()
        {
            Console.Write("Enter your name:  ");
            string name = Console.ReadLine();
            Console.Write("Enter your email:  ");
            string email = Console.ReadLine();
            Console.Write("Enter your username:  ");
            string username = Console.ReadLine();
            Console.Write("Enter your password:  ");
            string password = Console.ReadLine();
            Console.Write("Enter your phone number:  ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter your address:  ");
            string address = Console.ReadLine();
            var companies = companyManager.GetAll();
            foreach (var company in companies)
            {
                Console.Write($"Enter {company.Name} to choose as your company name:  ");
            }
            string companyName = Console.ReadLine();
            var maanager = managerManager.Register(name,email,username,password,phoneNumber,address,companyName);
            if(maanager != null)
            {
                Console.WriteLine("Manager register Sucessfully");
            }
            else
            {
                Console.WriteLine("Manager already exist");
            }
            SuperAdminMain();
        }
        public void RegisterRider()
        {
            Console.Write("Enter your name:  ");
            string name = Console.ReadLine();
            Console.Write("Enter your email:  ");
            string email = Console.ReadLine();
            Console.Write("Enter your username:  ");
            string username = Console.ReadLine();
            Console.Write("Enter your password:  ");
            string password = Console.ReadLine();
            Console.Write("Enter your phone number:  ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter your plateNumber:  ");
            string plateNumber = Console.ReadLine();
            Console.Write("Enter your address:  ");
            string address = Console.ReadLine();
            var rider = riderManager.Register(name,email,username,password,phoneNumber,plateNumber,address);
            if(rider != null)
            {
                Console.WriteLine("Rider registered successful");
            }
            else
            {
                Console.WriteLine("Rider already exist");
            }
            SuperAdminMain();
        }
        public void ViewAllManager()
        {
            var managers = managerManager.GetAll();
            foreach (var manager in managers)
            {
                var user = userManager.Get(manager.Username);
                Console.WriteLine($"{user.Name}\t{user.Username}\t{user.PhoneNumber}\t{manager.StaffNumber}\t{manager.CompanyName}");
            }
        }
        public void ViewAllCustomer()
        {
            var customers = customerManager.GetAll();
            foreach (var customer in customers)
            {
                var user = userManager.Get(customer.Username);
                Console.WriteLine($"{user.Name}\t{user.Username}\t{user.PhoneNumber}\t{user.Address}");
            }
        }
        public void ViewAllRider()
        {
            var riders = riderManager.GetAll();
            foreach (var rider in riders)
            {
                var user = userManager.Get(rider.Username);
                Console.WriteLine($"{user.Name}\t{user.Username}\t{user.PhoneNumber}\t{rider.RiderRegNumber}\t{rider.PlateNumber}");
            }
        }

        public void ViewAllCompany()
        {
            var companies = companyManager.GetAll();
            foreach (var company in companies)
            {
                Console.WriteLine($"{company.Name}\t{company.Address}\t{company.ApprovalCode}");
            }
        }
        public void ViewAllProduct()
        {
            var products = productManager.GetAll();
            foreach(var product in products)
            {
                Console.WriteLine($"{product.Name}\t{product.BrandName}\t{product.CompanyName}\t{product.Quantity}\t{product.Price}");
            }
        }
        public void ViewAllProductAvailable()
        {
            var products = productManager.GetAll();
            foreach (var product in products)
            {
                if(product.IsAvailable == true)
                {
                    Console.WriteLine($"{product.Name}\t{product.BrandName}\t{product.CompanyName}\t{product.Quantity}\t{product.Price}");
                }
            }
        }
        public void ViewAllOrder()
        {
            var order = orderManager.GetAll();
            foreach (var item in order)
            {
                if (item.Status == Status.Initiated)
                {
                    Console.WriteLine($"{item.Username}\t{item.ReferenceNumber}\t{item.Date}");
                }
            }
        }
        public void Logout()
        {
            Console.WriteLine("Logout Successful");
            MainMenu mainMenu = new MainMenu();
            mainMenu.Menu();
        }
    }
}
