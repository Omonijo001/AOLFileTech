using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Models.Entities
{
    public class Cart : BaseEntity
    {
        public string CustomerUsername;
        public string CartNumber;
        public Dictionary<string, int> Product;
        public double TotalPrice;


        public Cart(int id, string customerUsername, string cartNumber, Dictionary<string,int>product, double totalPrice, bool isDeleted): base(id, isDeleted)
        {
            CustomerUsername = customerUsername;
            CartNumber = cartNumber;
            Product = product;
            TotalPrice = totalPrice;
        }

        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            foreach (var item in Product)
            {
                data.Append($"{item.Key},{item.Value}=>");
            }
            return $"{Id}\t{CustomerUsername}\t{CartNumber}\t{data}\t{TotalPrice}\t{IsDeleted}";
        }

        public static Cart ToCart(string data)
        {
            var info = data.Split('\t');
            var id = int.Parse(info[0]);
            var customerUsername = info[1];
            var cartNumber = info[2];
            var asd = info[3].Split("=>");
            var product = new Dictionary<string, int>();
            for(int i = 0; i < asd.Length - 1; i++)
            {
                var prod = asd[i].Split(',');
                var prod1 = prod[0];
                var prod2 = int.Parse(prod[1]);
                product.Add(prod1, prod2);
            }
            var totalPrice = double.Parse(info[4]);
            var isDeleted = bool.Parse(info[5]);

            return new Cart(id,customerUsername,cartNumber,product,totalPrice,isDeleted);
        }
    }
}
