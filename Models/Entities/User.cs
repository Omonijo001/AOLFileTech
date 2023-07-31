using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class User: BaseEntity
    {
        public string Name; 
        public string Email;
        public string Username;
        public string Password;
        public string PhoneNumber;
        public string Address;
        public double Wallet;
        public string Role;

        public User(int id, string name, string email, string username, string password, string phoneNumber, string address, double wallet, string role, bool isDeleted) : base(id, isDeleted)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
            Wallet = wallet;
            Role = role;
        }
        
        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Email}\t{Username}\t{Password}\t{PhoneNumber}\t{Address}\t{Wallet}\t{Role}\t{IsDeleted}";
        }

        public static User ToUser(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string name = info[1];
            string email = info[2];
            string username = info[3];
            string password = info[4];
            string phoneNumber = info[5];
            string address = info[6];
            double wallet = double.Parse(info[7]);
            string role = info[8];
            bool isDeleted = bool.Parse(info[9]);

            return new User(id, name, email, username, password, phoneNumber, address, wallet, role, isDeleted);
        }
    }
}
