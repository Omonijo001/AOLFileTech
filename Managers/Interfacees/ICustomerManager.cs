using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface ICustomerManager
    {
        public Customer Register(string name, string email, string username, string password, string phoneNumber, string address);
        public Customer Get(int  id);
        public Customer Get(string username);
        public List<Customer> GetAll();
        public Customer Update( string username, string name, string phoneNumber, string address);
        public bool Delete(string username);

    }
}
