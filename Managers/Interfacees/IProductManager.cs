using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface IProductManager
    {
        public Product Create(string name,int ram, int rom, double price, int quantity, string brandName, string companyName);
        public Product Get(string name);
        public Product Get(int id);
        public List<Product> GetProductsByBrandName(string brandName);
        public List<Product> GetProductsByCompanyName(string companyName);
        public List<Product> GetAll();
        public Product Update(string name, int ram, int rom, double price, int quantity);
        public bool Delete(string name);
    }
}
