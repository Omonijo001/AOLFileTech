using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface IManagerManager
    {
        public Manager Register(string name, string email, string username, string password, string phoneNumber, string address, string companyName);
        public Manager Get(int id);
        public Manager Get(string username);
        public List<Manager> GetAll();
        public Manager Update(string username, string name, string phoneNumber, string address);
        public bool Delete(string username);
    }
}
