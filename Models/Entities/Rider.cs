using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Rider: BaseEntity
    {
        public string Username;
        public string PlateNumber;
        public string RiderRegNumber;

        public Rider(int id, string username, string plateNumber, string riderRegNumber, bool isDeleted): base(id, isDeleted)
        {
            Username = username;
            PlateNumber = plateNumber;
            RiderRegNumber = riderRegNumber;
        }

        public override string ToString()
        {
            return $"{Id}\t{Username}\t{PlateNumber}\t{RiderRegNumber}\t{IsDeleted}";
        }

        public static Rider ToRider(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string username = info[1];
            string plateNumber = info[2];
            string riderRegNumber = info[3];
            bool isDeleted = bool.Parse(info[4]);

            return new Rider(id, username, plateNumber, riderRegNumber, isDeleted);
        }
    }
}
