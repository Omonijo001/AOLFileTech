using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface IRiderManager
    {
        public Rider Register(string name, string email, string username, string password, string phoneNumber, string plateNumber, string address);
        public Rider Get(int id);
        public Rider Get(string riderRegNumber);
        public List<Rider> GetAll();
        public List<Product> GetAllProductAvailable(int ProductId);
        public Rider Update(string riderRegNumber, string name, string plateNumber, string phoneNumber, string address);
        public bool Delete(string riderRegNumber);
    }
}
