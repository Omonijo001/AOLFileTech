using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface ICartManager
    {
        public Cart AddCart(string customerUsername, Dictionary<string, int> product, double totalPrice);
        public Cart Get(int id);
        public Cart Get(string cartNumber);

        public List<Cart> GetCartsByUsername(string username);
        public List<Cart> GetAll();
        public bool Delete(string cartNumber);
    }
}
