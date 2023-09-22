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
            Console.WriteLine("2. Countries CRUD");
            Console.WriteLine("3. Locations CRUD");
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
                RegionsMenu();
                break;
            case "2":
                CountriesMenu();
                break;
            case "3":
                LocationsMenu();
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
                    regionController.GetAll();
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

    public static void CountriesMenu()
    {
        var countries = new Countries();
        var countriesView = new CountriesView();

        var countriesController = new CountriesController(countries, countriesView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all countries");
            Console.WriteLine("2. Insert new countries");
            Console.WriteLine("3. Update countries");
            Console.WriteLine("4. Delete countries");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    countriesController.GetAll();
                    break;
                case "2":
                    countriesController.Insert();
                    break;
                case "3":
                    countriesController.Update();
                    break;
                case "4":
                    countriesController.Delete();
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


    public static void LocationsMenu()
    {
        var locations = new Locations();
        var locationView = new LocationsView();

        var locationsController = new LocationsController(locations, locationView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all countries");
            Console.WriteLine("2. Insert new countries");
            Console.WriteLine("3. Update countries");
            Console.WriteLine("4. Delete countries");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    locationsController.GetAll();
                    break;
                case "2":
                    locationsController.Insert();
                    break;
                case "3":
                    locationsController.Update();
                    break;
                case "4":
                    locationsController.Delete();
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






