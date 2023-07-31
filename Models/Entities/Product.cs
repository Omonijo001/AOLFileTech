using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Product: BaseEntity
    {
        public string Name;
        public int Ram;
        public int Rom;
        public double Price;
        public int Quantity;
        public string BrandName;
        public bool IsAvailable;
        public string CompanyName;

        public Product(int id, string name, int ram, int rom, double price, int quantity, string brandName, bool isAvailable, string companyName, bool isDeleted): base(id,isDeleted)
        {
            Name = name;
            Ram = ram;
            Rom = rom;
            Price = price;
            Quantity = quantity;
            BrandName = brandName;
            IsAvailable = isAvailable;
            CompanyName = companyName;
        }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Ram}\t{Rom}\t{Price}\t{Quantity}\t{BrandName}\t{IsAvailable}\t{CompanyName}\t{IsDeleted}";
        }

        public static Product ToProduct(string data)
        {
            var info = data.Split('\t');
            int id = int.Parse(info[0]);
            string name = info[1];
            int ram = int.Parse(info[2]);
            int rom = int.Parse(info[3]);
            double price = double.Parse(info[4]);
            int quantity = int.Parse(info[5]);
            string brandName = info[6];
            bool isAvailable = bool.Parse(info[7]);
            string companyName = info[8];
            bool isDeleted = bool.Parse(info[9]);

            return new Product(id,name, ram, rom, price, quantity, brandName, isAvailable, companyName, isDeleted);

        }
    }
}
