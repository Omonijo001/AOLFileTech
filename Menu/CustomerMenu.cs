using AolFileProject.Managers.Implementations;
using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Entities;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Menu
{
    public class CustomerMenu
    {
        IUserManager userManager = new UserManager();
        IProductManager productManager = new ProductManager();
        ICustomerManager customerManager = new CustomerManager();
        ICartManager cartManager = new CartManager();
        IOrderManager orderManager = new OrderManager();
        IBrandManager brandManager = new BrandManager();
        public void CustomerMain()
        {
            Console.WriteLine("Welcome to AOL GADGETS & TECHNOLOGY\nEnter 1 to view All Product Available\nEnter 2 to fund wallet\nEnter 3 view wallet balance\nEnter 4 to cart\nEnter 5 to make order\nEnter 6 to logout");
            int option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                ViewAllProductAvailable();
                CustomerMain();
            }
            else if (option == 2)
            {
                FundWallet();
                CustomerMain();
            }
            else if (option == 3)
            {
                WalletBalance();
                CustomerMain();
            }
            else if(option == 4)
            {
                Cart();
                CustomerMain();
            }
            else if (option == 5)
            {
                Order();
                CustomerMain();
            }
            else if (option == 6)
            {
                Logout();
                CustomerMain();
            }
            else
            {
                Console.WriteLine("You enter an Invalid option\nKindly enter a valid option");
                CustomerMain();
            }
        }

        public void ViewAllProductAvailable()
        {
            var brands = brandManager.GetAll();
            foreach (var brand in brands)
            {
                Console.WriteLine($"{brand.Name.ToUpper()} ==>");
                var products = productManager.GetProductsByBrandName(brand.Name);
                foreach (var item in products)
                {
                    Console.WriteLine($"{item.Name}\t{item.Price}\t{item.Ram}\t{item.Rom}");
                }
            }
        }
        public void FundWallet()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter amount to deposit : ");
            double amount = double.Parse(Console.ReadLine());
            var user = userManager.Get(username);
            if(amount > 0)
            {
                double wallet = user.Wallet + amount;
                Console.WriteLine($"Your wallet balance is {wallet}");
                userManager.FundWallet(username,amount);
            }
            else
            {
                Console.WriteLine("Invalid amount");
            }
        }
        public void WalletBalance()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            var user = userManager.GetAll();
            foreach (var item in user)
            {
                if(item.Username == username)
                {
                    Console.WriteLine($"Mr/Mrs {item.Name} your wallet balance is {item.Wallet}");
                }
            }

        }
        public void Cart()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            var user = userManager.Get(username);
            var brands = brandManager.GetAll();
            Dictionary<string, int> chartProducts = new Dictionary<string, int>();
            bool carting = true;
            while (carting)
            {
                foreach (var brand in brands)
                {
                    Console.WriteLine($"{brand.Name.ToUpper()} ==>");
                    var product = productManager.GetProductsByBrandName(brand.Name);
                    foreach (var item in product)
                    {
                        Console.WriteLine($"enter{item.Id} to cart {item.Name}\t{item.Price}\t{item.Ram}\t{item.Rom}");
                    }
                }
                int productId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter the quantity you want: ");
                int quantity = int.Parse(Console.ReadLine());
                var productName = productManager.Get(productId).Name;
                if (chartProducts.ContainsKey(productName))
                {
                    chartProducts[productName] += quantity;
                }
                else
                {
                    chartProducts.Add($"{productName}", quantity);
                }
                foreach (var prod in chartProducts)
                {
                    Console.WriteLine($"{prod.Key}\t{prod.Value}");
                }
                var price = CalculateTotalPrice(chartProducts);
                Console.WriteLine($"the total price is {price} and your wallet balance {user.Wallet}");

                Console.WriteLine("still want to cart? Enter y for Yes and n for No y/n");
                char opt = char.Parse(Console.ReadLine());
                if (opt == 'n')
                {
                    carting = false;
                }
            }
            var totalPrice = CalculateTotalPrice(chartProducts);
            cartManager.AddCart(username, chartProducts, totalPrice);

        }

        private double CalculateTotalPrice(Dictionary<string, int> chartProducts)
        {
            double price = 0;
            foreach (var item in chartProducts)
            {
                var product = productManager.Get(item.Key).Price;
                price += (product * item.Value);
            }
            return price;
        }
        public void Order()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            var carts = cartManager.GetCartsByUsername(username);
            foreach (var cart in carts)
            {
                Console.WriteLine($"enter {cart.CartNumber} to make order the cart itens");

            }
            string cartNumber = Console.ReadLine();
            var order = orderManager.MakeOrder(username,cartNumber);
            if (order != null)
            {
                Console.WriteLine($"Order successful");
            }
            else
            {
                Console.WriteLine("Order failed");
            }
        }
        public void Logout()
        {
            /*Console.WriteLine("Logout Successfully");
            MainMenu mainMenu = new MainMenu();
            mainMenu.Menu();*/
        }
    }
}
