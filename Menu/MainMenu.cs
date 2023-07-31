using AolFileProject.Managers.Implementations;
using AolFileProject.Managers.Interfacees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AolFileProject.Menu
{
    public class MainMenu
    {
        ICompanyManager companyManager = new CompanyManager();
        ICartManager cartManager = new CartManager();
        ICustomerManager customerManager = new CustomerManager();
        IManagerManager managerManager = new ManagerManager();
        IProductManager productManager = new ProductManager();
        IUserManager userManager = new UserManager();   
        CustomerMenu customerMenu = new CustomerMenu();
        ManagerMenu managerMenu = new ManagerMenu();
        RiderMenu riderMenu = new RiderMenu();
        SuperAdminMenu superAdminMenu = new SuperAdminMenu();
        public void Menu()
        {
            Console.WriteLine("WELCOME TO AOL GADGETS & TECHNOLOGY\nEnter 1 for Company Registeration\nEnter 2 to Register\nEnter 3 to Login");
            int option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                RegisterCompany();
                Menu();
            }
            else if (option == 2)
            {
                Register();
                Menu();
            }
            else if (option == 3)
            {
                Login();
                Menu();
            }
            else
            {
                Console.WriteLine("You enter an invalid input\nEnter a vaid input");
                Menu();
            }
        }

        public void RegisterCompany()
        {
            Console.Write("Enter the name of the company: ");
            string companyName = Console.ReadLine();
            Console.Write("Enter the address of the company: ");
            string address = Console.ReadLine();
            Console.Write("Enter your company approval code: ");
            string companyApprovalCode = Console.ReadLine();

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


            var company = companyManager.Register(companyName, address, companyApprovalCode);
            if (company != null)
            {
                var manager = managerManager.Register(name, email, username, password, phoneNumber, address, companyName);
                if(manager != null)
                {
                    Console.WriteLine(" Manager registered succesful");
                }
                else
                {
                    Console.WriteLine("Manager already exist");
                }
            }
            else
            {
                Console.WriteLine("Company already exist");
            } 
           
            Menu();
        }

        public void Register()
        {
            Console.Write("Enter your Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your Email address: ");
            string email = Console.ReadLine();
            Console.Write("Enter your Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            Console.Write("Enter your Phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter your Address: ");
            string address = Console.ReadLine();

            var customer = customerManager.Register(name, email, username, password, phoneNumber, address);
            if (customer != null)
            {
                Console.WriteLine("Customer registered successfully");
            }
            else
            {
                Console.WriteLine("Customer already exist");
            }
            Menu();
        }

        public void Login()
        {
            Console.Write("Enter your Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            var user = userManager.Login(username, password);
            if (user != null)
            {
                if(user.Role == "SuperAdmin")
                {
                    superAdminMenu.SuperAdminMain();
                }
                else if (user.Role == "Manager")
                {
                    Console.WriteLine($"welcome to {managerManager.Get(username).CompanyName}");
                    managerMenu.ManagerMain();
                }
                else if (user.Role == "Customer")
                {
                    customerMenu.CustomerMain();
                }
                else if (user.Role == "Rider")
                {
                    riderMenu.RiderMain();
                }
                else
                {
                    Console.WriteLine("Incorrect Details\nEnter a valid username and password");
                    Console.WriteLine("Do you want to login again?\nEnter 1 for YES and 2 for NO");
                    int option = int.Parse(Console.ReadLine());
                    if (option == 1)
                    {
                        var menu = new MainMenu();
                        menu.Login();
                    }
                    else
                    {
                        Menu();
                    }
                }

            }
        }
    }
}
