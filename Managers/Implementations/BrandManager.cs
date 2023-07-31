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
    public class BrandManager : IBrandManager
    {
        public static List<Brand> brandDB = new List<Brand>();
        string fileBrand = @"C:\Users\USER\Desktop\AolFileProject\Files\brandDB.txt";

        public BrandManager()
        {
            ReadBrandFromFile();
        }
        private void ReadBrandFromFile()
        {
            if(File.Exists(fileBrand))
            {
                if(brandDB.Count == 0)
                {
                    var brands = File.ReadAllLines(fileBrand);
                    foreach (var item in brands)
                    {
                        brandDB.Add(Brand.ToBrand(item));
                    }
                }
                else
                {
                    brandDB.Clear();
                    var brands = File.ReadAllLines(fileBrand);
                    foreach (var item in brands)
                    {
                        brandDB.Add(Brand.ToBrand(item));
                    }

                }
                
            }

            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "brandDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddBrandToFile(Brand brand)
        {
            using(StreamWriter brands = new StreamWriter(fileBrand, true))
            {
                brands.WriteLine(brand.ToString());
            }
        }

        private void RefreshFile()
        {
            using(StreamWriter brands = new StreamWriter(fileBrand, true))
            {
                foreach (var item in brandDB)
                {
                    brands.WriteLine(item.ToString());
                }
            }
        }

        private bool CheckIfExist(string name)
        {
            foreach (var item in brandDB)
            {
                if(item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public Brand Create(string name)
        {
            var brandExists = CheckIfExist(name);
            if(brandExists == false)
            {
                var brand = new Brand(brandDB.Count + 1, name, false);
                brandDB.Add(brand);
                AddBrandToFile(brand);
                Console.WriteLine("Created Successfully");
                return brand;
            }
            else
            {
                Console.WriteLine("Brand already Exist");
                return null;
            }
           
        }

        public bool Delete(string name)
        {
           var brand = Get(name); 
            if(brand != null)
            {
                brand.IsDeleted = true;
                Console.WriteLine("Brand deleted Successfully");
                return true;
            }
             Console.WriteLine("Unable to delete brand");
             return false;
        }

        public Brand Get(int id)
        {
            foreach (var brand in brandDB)
            {
                if(brand.Id == id)
                {
                    return brand;
                }
            }
            return null;
        }

        public Brand Get(string name)
        {
            foreach (var brand in brandDB)
            {
                if(brand.Name == name)
                {
                    return brand;
                }
            }
            return null;
        }

        public List<Brand> GetAll()
        {
            return brandDB;
        }
    }
}
