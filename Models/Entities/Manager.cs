using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Manager: BaseEntity
    {
        public string Username;
        public string StaffNumber;
        public string CompanyName;

        public Manager(int id, string username, string staffNumber,string companyName, bool isDeleted): base(id, isDeleted)
        {
            Username = username;
            StaffNumber = staffNumber;
            CompanyName = companyName;
        }    

        public override string ToString()
        {
            return $"{Id}\t{Username}\t{StaffNumber}\t{CompanyName}\t{IsDeleted}";
        }

        public static Manager ToManager(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string username = info[1];
            string staffNumber = info[2];
            string companyName = info[3];
            bool isDeleted = bool.Parse(info[4]);

            return new Manager(id,username, staffNumber,companyName,isDeleted);
        }
    }
}
