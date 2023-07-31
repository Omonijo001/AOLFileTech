using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Brand : BaseEntity
    {
        public string Name;

        public Brand(int id, string name, bool isDeleted): base(id, isDeleted) 
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{IsDeleted}";
        }

        public static Brand ToBrand(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string name = info[1];
            bool isDeleted = bool.Parse(info[2]);
            return new Brand(id,name,isDeleted);
        }
    }
}
