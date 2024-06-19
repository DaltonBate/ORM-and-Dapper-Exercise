using Microsoft.Extensions.Configuration;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing.Printing;


namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion
            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, here are the present departments:");
            Console.WriteLine("Please press enter.");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            

            foreach (var depo in depos) 
            { 
               Console.WriteLine($"Id: {depo.DepartmentID} Name: {depo.Name}");
            }

            Console.WriteLine("Do you want to add a department?");

            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes") 
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();
               repo.InsertDepartment(userResponse);

                repo.GetAllDepartments();
                Print(repo.GetAllDepartments());
            }

            Console.WriteLine("Have a nice day.");
        }

        private static void Print(IEnumerable<Department> depos) 
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentID} Name: {depo.Name}");
            }
        }
    }
}
