using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Company: BaseEntity
    {
        public string Name;
        public string Address;
        public string ApprovalCode;

        public Company(int id, string name, string address, string approvalCode, bool isDeleted): base(id, isDeleted) 
        { 
            Name = name;
            Address = address;
            ApprovalCode = approvalCode;
        }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Address}\t{ApprovalCode}\t{IsDeleted}";
        }
        
        public static Company ToCompany(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string name = info[1];
            string address = info[2];
            string approvalCode = info[3];
            bool isDeleted = bool.Parse(info[4]);

            return new Company(id,name,address, approvalCode, isDeleted);
        }
    }
}
