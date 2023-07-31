using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface IBrandManager
    {
        public Brand Create(string name);
        public Brand Get(int id);
        public Brand Get(string name);
        public List<Brand> GetAll();
        public bool Delete(string name);

    }
}
