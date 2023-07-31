using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface IUserManager
    {
        public User Login(string username, string password);
        public User Get(int id);
        public User Get(string username);
        public List<User> GetAll();
        public User Update(string username, string name, string phoneNumber, string address);
        public bool Delete(string username);
        public User FundWallet(string username, double amount);
    }
}
