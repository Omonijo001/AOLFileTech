using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Interfacees
{
    public interface ICompanyManager
    {
        public Company Register(string name, string address, string approvalCode);
        public Company Get(string approvalCode);
        public List<Company> GetAll();
        public Company Update(string approvalCode, string name, string address);
        public bool Delete(string approvalCode);

    }
}
