using AolFileProject.Managers.Implementations;
using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Entities;
using AolFileProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AolFileProject.Menu
{
    public class RiderMenu
    {
        IRiderManager riderManager = new RiderManager();
        ICustomerManager customerManager = new CustomerManager();
        IOrderManager orderManager = new OrderManager();
        IProductManager productManager = new ProductManager();
        IUserManager userManager = new UserManager();
        public void RiderMain()
        {
            Console.WriteLine("Welcome to AOL Gadgets & Technology\nEnter 1 to view all packed order\nEnter 2 to view customer details 0f packed order\nEnter 3 to register deliver order\nEnter 4 to logout");
            int option = int.Parse(Console.ReadLine());
            if(option == 1)
            {
                ViewAllPackedOrder();
                RiderMain();
            }
            else if (option == 2)
            {
                CustomerDetailsOfPackedOrder();
                RiderMain();
            }
            else if (option == 3)
            {
                RegisterDeliveredOrder();
                RiderMain();
            }
            else if (option == 4)
            {
                Logout();
                RiderMain();
            }
            else
            {
                Console.WriteLine("You enter an invalid option\nKindly enter a valid option");
                RiderMain();
            }

        }
        public void ViewAllPackedOrder()
        {
            List<Order> Packed = new List<Order>();
            foreach (var item in Packed)
            {
                if(item.Status == Status.Packed)
                {
                    Packed.Add(item);
                    Console.WriteLine($"{item.Username}\t{item.ReferenceNumber}");
                }
            }
        }
        public void CustomerDetailsOfPackedOrder()
        {
            var order = orderManager.GetAll();
            foreach (var item in order)
            {
                if(item.Status == Status.Packed)
                {
                    var user = userManager.Get(item.Username);
                    Console.WriteLine($"Mr/Mrs {user.Name}, {user.PhoneNumber} with {item.ReferenceNumber} order on {item.Date} to be deliver to {user.Address}");
                }
            }
        }
        public void RegisterDeliveredOrder()
        {
            var orders = orderManager.GetAll();
            foreach (var order in orders)
            {
                if (order.Status == Status.Packed)
                {
                    var user = userManager.Get(order.Username);
                    Console.WriteLine($"{order.ReferenceNumber}\t{order.Username}\t{user.Address}\t{user.PhoneNumber}");

                }
                Console.WriteLine($"Enter order referenceNumber to change order status to deliver");
                string refenceNumber = Console.ReadLine();
                if(refenceNumber != order.ReferenceNumber)
                {
                    Console.WriteLine("Invalid Reference Number");
                }
                else
                {
                    orderManager.Delivered(refenceNumber);
                    Console.WriteLine($"{order.ReferenceNumber} delivered successfully");
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
