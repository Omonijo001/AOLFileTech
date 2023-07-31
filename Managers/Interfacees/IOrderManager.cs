using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface IOrderManager
    {
        public Order MakeOrder(string username, string cartNumber);
        public Order Get(string referenceNumber);
        public List<Order> GetAll();
        public Order PackOrder(string referenceNumber);
        public Order Enroute(string referenceNumber);
        public Order ReceiveOrder(string referenceNumber);
        public Order Delivered(string referenceNumber);
 
    }
}
