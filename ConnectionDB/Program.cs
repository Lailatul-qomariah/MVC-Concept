using ConnectionDB.Controllers;
using ConnectionDB.Views;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConnectionDB;

class Program
{


    private static void Main()
    {
        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. Regions CRUD");
            Console.WriteLine("2. List all countries");
            Console.WriteLine("3. List all locations");
            Console.WriteLine("4. List all Job");
            Console.WriteLine("5. List all Job History");
            Console.WriteLine("6. List all Employees");
            Console.WriteLine("7. List all Departments");
            Console.WriteLine("8. Join EmpolyeeDkk");
            Console.WriteLine("9. Join Employee dan Dept");
            Console.WriteLine("10. Exit");

            Console.Write("Enter your choice: ");

            
            var input = Console.ReadLine();
            choice = Menu(input);
        }

    }

    public static bool Menu(string input)
    {
        switch (input)
        {
            case "1":
                //var manageDatabase = new ManageDatabase();
                //var regions = manageDatabase.GetAll("tbl_regions");
                var manageDatabase = new Regions();
                var regions = manageDatabase.GetAll();
                RegionsMenu();


                break;
            case "2":
                var country = new Countries();
                var countries = country.GetAll();
                //GeneralMenu.List(countries, "countries");
                break;
            case "3":
                var location = new Locations();
                var locations = location.GetAll();
               // GeneralMenu.List(locations, "locations");
                break;

            case "4":
                var job = new Jobs();
                var jobs = job.GetAll();
                //GeneralMenu.List(jobs, "Job");
                break;

            case "5":
                var jobHis = new JobhHistory();
                var jobHist = jobHis.GetAll();
                //GeneralMenu.List(jobHist, "JobHistory");
                break;

            case "6":
                var employees = new Employees();
                var emp = employees.GetAll();
               // GeneralMenu.List(emp, "Employees");
                break;

            case "7":
                var departments = new Departments();
                var dept = departments.GetAll();
                //GeneralMenu.List(dept, "Departments");
                break;
            case "8":
                var join = new JoinTables();
                var joins = join.JoinTable();
                //GeneralMenu.List(joins, "Join ");
                break;
            case "9":
                var join1 = new JoinTables();
                var joins1 = join1.JoinDeptEmp();
                //GeneralMenu.List(joins1, "Join ");
                break;
            case "10":
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;
    }

    public static void RegionsMenu()
    {
        var region = new Regions();
        var regionView = new RegionsView();

        var regionController = new RegionsController(region, regionView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. Insert new region");
            Console.WriteLine("3. Update region");
            Console.WriteLine("4. Delete region");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    regionController.GetAllController();
                    break;
                case "2":
                    regionController.Insert();
                    break;
                case "3":
                    regionController.Update();
                    break;
                case "4":
                    regionController.Delete();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }



}






