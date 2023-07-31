using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class BaseEntity
    {
        public int Id;
        public bool IsDeleted;

        public BaseEntity(int id, bool isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"{Id}\t{IsDeleted}";
        }

        public static BaseEntity ToBaseEntity(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            bool isDeleted = bool.Parse(info[1]);
            return new BaseEntity(id, isDeleted);
        }
    }
}
