using AolFileProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Order : BaseEntity
    {
        public string Username;
        public string CartNumber;
        public string ReferenceNumber;
        public DateTime Date;
        public Status Status;


        public Order(int id, string username, string cartNumber, string referenceNumber, DateTime date, Status status, bool isDeleted): base(id, isDeleted) 
        {
            Username = username;
            CartNumber = cartNumber;
            ReferenceNumber = referenceNumber;
            Date = date;
            Status = status;
        }

        public override string ToString()
        {
            return $"{Id}\t{Username}\t{CartNumber}\t{ReferenceNumber}\t{Date}\t{Status}\t{IsDeleted}";
        }

        public static Order ToOrder(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string username = info[1];
            string cartNumber = info[2];
            string referenceNumber = info[3];
            DateTime date = DateTime.Parse(info[4]);
            Status status = (Status)Enum.Parse(typeof(Status), info[5]);
            bool isDeleted = bool.Parse(info[6]);

            return new Order(id, username, cartNumber, referenceNumber, date, status, isDeleted);
        }
    }
}
