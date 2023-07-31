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
    public class CompanyManager : ICompanyManager
    {
        public static List<Company> companyDB = new List<Company>();
        string fileCompany = @"C:\Users\USER\Desktop\AolFileProject\Files\companyDB.txt";

        public CompanyManager() 
        {
            ReadCompanyFromFile();
        }
        private void ReadCompanyFromFile()
        {
            if(File.Exists(fileCompany))
            {
                if(companyDB.Count == 0)
                {
                    var company = File.ReadAllLines(fileCompany);
                    foreach (var item in company)
                    {
                        companyDB.Add(Company.ToCompany(item));
                    }
                }
                else
                {
                    companyDB.Clear();
                    var company = File.ReadAllLines(fileCompany);
                    foreach (var item in company)
                    {
                        companyDB.Add(Company.ToCompany(item));
                    }
                }

            }
            else
            {
                string path = @"C:\Users\USER\Desktop\AolFileProject\Files";
                Directory.CreateDirectory(path);
                string fileName = "companyDB.txt";
                string fullPath = Path.Combine(path, fileName);
                File.Create(fullPath);
            }
        }

        private void AddCompanyToFile(Company company)
        {
            using (StreamWriter companys = new StreamWriter(fileCompany, true) )
            {
                companys.WriteLine(company.ToString());
            }
        }

        private void RefreshCompany()
        {
            using(StreamWriter asd = new StreamWriter(fileCompany, true))
            {
                foreach (var item in companyDB)
                {
                    asd.WriteLine(item.ToString());
                }
            }
        }
        private bool CheckIfExists(string approvalCode)
        {
            foreach (var item in companyDB)
            {
                if(item.ApprovalCode == approvalCode)
                {
                    return true;
                }
            }
            return false;
        }

        private string GenerateApprovalNumber()
        {
            Random rand = new Random();
            var code = rand.Next(1000,9999).ToString();
            return $"AOL/CMP/{code}";
        }
        public bool Delete(string approvalCode)
        {
            var company = Get(approvalCode);
            if (company != null)
            {
                company.IsDeleted = true;
                Console.WriteLine($"{company.Name} with {company.ApprovalCode} deleted successfully");
                return true;
            }
            Console.WriteLine("company not found");
            return false; 
        }

        public Company Get(string approvalCode)
        {
            foreach (var company in companyDB)
            {
                if(company.ApprovalCode == approvalCode)
                {
                    return company;
                }
            }
            return null;
        }

        public List<Company> GetAll()
        {
            return companyDB;
        }

        public Company Register(string name, string address, string approvalCode)
        {
            var companyExist = CheckIfExists(approvalCode);
            {
                if(companyExist == false)
                {
                    var company = new Company(companyDB.Count +1, name, address, approvalCode, false);
                    companyDB.Add(company);
                    AddCompanyToFile(company);
                    Console.WriteLine($"{company.Name} with {company.ApprovalCode} registered successfully ");
                    return company;
                }
                Console.WriteLine("Company already exist");
                return null;
            }
        }

        public Company Update(string approvalCode, string name, string address)
        {
            var company = Get(approvalCode);
            if(company != null)
            {
                if( company.ApprovalCode == approvalCode)
                {
                    company.Name = name;
                    company.Address = address;
                    Console.WriteLine($"{company.Name} updated successfully");
                    return company;
                }
            }
            return null;
        }
    }
}
