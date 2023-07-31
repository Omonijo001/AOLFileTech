using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Customer : BaseEntity
    {
        public string Username;

        public Customer(int id, string username, bool isDeleted): base(id, isDeleted) 
        {
            Username = username;
        }

        public override string ToString()
        {
            return $"{Id}\t{Username}\t{IsDeleted}";
        }

        public static Customer ToCustomer(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string username = info[1];
            bool isDeleted = bool.Parse(info[2]);

            return new Customer(id,username,isDeleted);
        }
    }
}
