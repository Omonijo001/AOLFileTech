using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AolFileProject.Managers.Implementations
{
    public class CartManager : ICartManager
    {
        public static List<Cart> cartDB = new List<Cart>();
        string fileCart = @"C:\Users\USER\Desktop\AolFileProject\Files\cartDB.txt";

        public CartManager() 
        {
            ReadCartFromFile();
        }
        public void ReadCartFromFile()
        {
            if(File.Exists(fileCart))
            {
                if(cartDB.Count == 0)
                {
                    var carts = File.ReadAllLines(fileCart);
                    foreach (var item in carts)
                    {
                        cartDB.Add(Cart.ToCart(item));
                    }
                }
                else
                {
                    cartDB.Clear();
                    var carts = File.ReadAllLines(fileCart);
                    foreach (var item in carts)
                    {
                        cartDB.Add(Cart.ToCart(item));
                    }
                }
                
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "cartDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddCartToFile(Cart cart)
        {
            using (StreamWriter asd = new StreamWriter(fileCart, true))
            {
                asd.WriteLine(cart.ToString());
            }
        }

        private void RefreshFile()
        {
            using (StreamWriter asd = new StreamWriter(fileCart, true))
            {
                foreach (var item in cartDB)
                {
                    asd.WriteLine(item.ToString());
                }
            }
        }

        private bool CheckIfExist(string cartNumber)
        {
            foreach (var item in cartDB)
            {
                if(item.CartNumber == cartNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private string GenerateCartNumber()
        {
            Random rand = new Random();
            var asd = rand.Next(1000, 9999).ToString();
            return $"AOL/Cart/{asd}";
        }
        public Cart AddCart(string customerUsername, Dictionary<string, int> product, double totalPrice)
        {
                var cart = new Cart (cartDB.Count +1,customerUsername, GenerateCartNumber(), product, totalPrice, false);
                cartDB.Add(cart);
                AddCartToFile(cart);
                Console.WriteLine("cart added successfully");
                return cart;
          
        }

        public bool Delete(string cartNumber)
        {
            var cart = Get(cartNumber);
            if(cart != null)
            {
                cart.IsDeleted = true;
                Console.WriteLine("cart deleted successfully");
                return true;
            }
            Console.WriteLine("cart not found");
            return false;
        }

        public Cart Get(int id)
        {
            foreach (var cart in cartDB)
            {
                if(cart.Id == id)
                {
                    return cart;
                }
            }
            return null;
        }
        public Cart Get(string cartNumber)
        {
            foreach (var cart in cartDB)
            {
                if (cart.CartNumber == cartNumber)
                {
                    return cart;
                }
            }
            return null;
        }

        public List<Cart> GetAll()
        {
            return cartDB;
        }

        public List<Cart> GetCartsByUsername(string username)
        {
           var list = new List<Cart>();
            foreach (var cart in cartDB)
            {
                if(cart.CustomerUsername == username)
                {
                   list.Add(cart);
                }
            }
            return list;
        }
    }
}
