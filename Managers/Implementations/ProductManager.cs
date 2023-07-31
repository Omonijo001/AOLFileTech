using AolFileProject.Managers.Interfacees;
using AolFileProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AolFileProject.Managers.Implementations
{
    public class ProductManager : IProductManager
    {
        public static List<Product> productDB = new List<Product>();
        string fileProduct = @"C:\Users\USER\Desktop\AolFileProject\Files\productDB.txt";

        public ProductManager() 
        {
            ReadProductFromFile();
        }
        private void ReadProductFromFile()
        {
            if(File.Exists(fileProduct))
            {
                if(productDB.Count == 0)
                {
                    var product = File.ReadAllLines(fileProduct);
                    foreach (var item in product)
                    {
                        productDB.Add(Product.ToProduct(item));
                    }
                }
                else
                {
                    productDB.Clear();
                    var product = File.ReadAllLines(fileProduct);
                    foreach (var item in product)
                    {
                        productDB.Add(Product.ToProduct(item));
                    }
                }
                
            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "productDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddProductToFile(Product product)
        {
            using (StreamWriter products = new StreamWriter(fileProduct, true))
            {
                products.WriteLine(product.ToString());
            }
        }

        private void RefreshFile()
        {
            using (StreamWriter products = new StreamWriter(fileProduct, true))
            {
                foreach (var item in productDB)
                {
                    products.WriteLine(item.ToString());
                }
            }
        }
        private bool CheckIfExist(string name, string companyName)
        {
            foreach (var product in productDB)
            {
                if(product.Name == name && product.CompanyName == companyName)
                {
                    return true;
                }
            }
            return false;
        }
        public Product Create(string name,int ram, int rom, double price, int quantity, string brandName, string companyName)
        {
            var productExist = CheckIfExist(name, companyName);
            if(productExist == false)
            {
                Product product = new Product (productDB.Count +1,name, ram, rom, price, quantity, brandName, true, companyName, false);
                productDB.Add(product);
                AddProductToFile(product);
                return product;
            }
            var a = Get(name);
            a.Quantity += quantity;
            Console.WriteLine("Product already exist");
            return null;
        }

        public bool Delete(string name)
        {
            var product = Get(name);
            if (product != null)
            {
                product.IsDeleted = true;
                Console.WriteLine("Product deleted successfully");
                return true;
            }
            Console.WriteLine($"{product.Name} not found");
            return false;
        }

        public Product Get(string name)
        {
            foreach (var product in productDB)
            {
                if(product.Name == name)
                {
                    return product;
                }
            }
            return null;
        }


        public Product Get(int id)
        {
            foreach (var product in productDB)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            return null;
        }

        public List<Product> GetAll()
        {
            return productDB;
        }

        public List<Product> GetProductsByBrandName(string brandName)
        {
            var list = new List<Product>();
            foreach (var product in productDB)
            {
                if(product.BrandName == brandName)
                {
                    list.Add(product);
                }
            }
            return list;
        }

        public List<Product> GetProductsByCompanyName(string companyName)
        {
            var list = new List<Product>();
            foreach (var product in productDB)
            {
                if (product.CompanyName == companyName)
                {
                    list.Add(product);
                }
            }
            return list;
        }

        public Product Update(string name, int ram, int rom, double price, int quantity)
        {
           var product = Get(name);
            if (product != null)
            {
                if (product.Name == name)
                {
                    product.Ram = ram;
                    product.Rom = rom;
                    product.Price = price;
                    product.Quantity = quantity;
                    Console.WriteLine("Update successful");
                    return product;
                }
                Console.WriteLine("Details Incorrect");
            }
            Console.WriteLine("Product not found");
            return null;
        }
    }
}
