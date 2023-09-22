using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConnectionDB;

class Program
{
    private static readonly string connectionString = "Data Source=LAPTOP-IQK7879R;Database=db_mcc81;Integrated Security=True;Connect Timeout=30; Integrated Security=True";
    /* static SqlConnection connection;
     private static void Main()
     {
         connection = new SqlConnection(connectionString);
         try
         {
             connection.Open();
             Console.WriteLine("Connection Opened Successfully");
             connection.Close(); //untuk melindungi diri dari attacker
         }
         catch (Exception ex)
         {
             Console.WriteLine($"Error : {ex.Message}");
         }
     }*/


    private static void Main()
    {
        // UpdateRegion(7, "RiaLailatulQomariah");
        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. List all regions");
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






        /*  while (true)
          {
              Console.Clear();
              Console.WriteLine("==========================");
              Console.WriteLine("\t MENU CRUD \t");
              Console.WriteLine("==========================");
              Console.WriteLine("\n1. Regions" +
              "\n2. Countries" +
              "\n3. Locations" +
              "\n4. Departments" +
              "\n5. Employees " +
              "\n6. Jobs" +
              "\n7. Job History" +
              "\n8. Exit");
              Console.Write("Inpt : ");
              string inp = Console.ReadLine();
              switch (inp)
              {
                  case "1":
                      Menu();
                      break;
                  case "2":
                      Menu();
                      break;
                  case "3":
                      Menu();
                      break;
                  case "4":
                      Menu();
                      break;
                  case "5":
                      Menu();
                      break;
                  case "6":
                      Menu();
                      break;
                  case "7":
                      Menu();
                      break;
                  case "8":
                      Environment.Exit(0);
                      break;
                  default:
                      Console.Write("Masukkan data yang benar");
                      break;
              }
          }*/




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
                GeneralMenu.List(regions, "regions");

                //Regions.regions.GetAll();
                /*List<Dictionary<string, object>> data = ManageDatabase.GetAll("tbl_regions");
                foreach (var row in data)
                {
                    foreach (var keyValuePair in row)
                    {
                        Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
                    }
                    Console.WriteLine();
                }*/


                break;
            case "2":
                var country = new Countries();
                var countries = country.GetAll();
                GeneralMenu.List(countries, "countries");
                break;
            case "3":
                var location = new Locations();
                var locations = location.GetAll();
                GeneralMenu.List(locations, "locations");
                break;

            case "4":
                var job = new Jobs();
                var jobs = job.GetAll();
                GeneralMenu.List(jobs, "Job");
                break;

            case "5":
                var jobHis = new JobhHistory();
                var jobHist = jobHis.GetAll();
                GeneralMenu.List(jobHist, "JobHistory");
                break;

            case "6":
                var employees = new Employees();
                var emp = employees.GetAll();
                GeneralMenu.List(emp, "Employees");
                break;

            case "7":
                var departments = new Departments();
                var dept = departments.GetAll();
                GeneralMenu.List(dept, "Departments");
                break;
            case "8":
                var join = new JoinTables();
                var joins = join.JoinTable();
                GeneralMenu.List(joins, "Join ");
                break;
            case "9":
                var join1 = new JoinTables();
                var joins1 = join1.JoinDeptEmp();
                GeneralMenu.List(joins1, "Join ");
                break;
            case "10":
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;
    }











    /*static void Menu()
    {
        Regions regions = new Regions();
        Console.WriteLine("============================================");
        Console.WriteLine("\t PILIH ACTION \t");
        Console.WriteLine("============================================");
        Console.WriteLine("1. View Data \n2. View Data by Id \n3.Insert Data \n4. Delet Data \n5. Update Data \n6. Back");
        Console.Write("Input: ");
        string inpMenu = Console.ReadLine();
        switch (inpMenu)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("\t VIEW DATA \t");
                Console.WriteLine("============================================");
                Console.WriteLine("Masukkan table yang ingin ditampilkan : ");
                Console.WriteLine("\n1. Regions" +
                                  "\n2. Countries" +
                                    "\n3. Locations" +
                                     "\n4. Departments" +
                                       "\n5. Employees " +
                                         "\n6. Jobs" +
                                            "\n7. Job History");
                string pilihan = Console.ReadLine();
                switch (pilihan)
                {
                    case "1":
                        Countries.countries.View();

                        break;
                    case "2":
                        Countries.countries.View();

                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;

                }
                regions.View




                Console.ReadLine();
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("\t VIEW DATA BY ID \t");
                Console.WriteLine("============================================");
                Console.Write("Masukkan Id :");
                int noId = int.Parse(Console.ReadLine());
                Countries getRegionsId = countries.GetById(noId);


                if (getRegionsId != null)
                {
                    Console.WriteLine($"Id: {getRegionsId.Id}, Name: {getRegionsId.Name}");
                }
                else
                {
                    Console.WriteLine("No data found");
                }


                Console.ReadLine();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("\t INSERT DATA \t");
                Console.WriteLine("============================================");
                Console.Write("Masukkan id :");
                int noIdInsert = int.Parse(Console.ReadLine());
                Console.Write("Masukkan nama :");
                string nameInsert = Console.ReadLine();
                var insertResult = countries.Insert(noIdInsert, nameInsert);
                int.TryParse(insertResult, out int result);
                if (result > 0)
                {
                    Console.WriteLine("Insert Success");
                }
                else
                {
                    Console.WriteLine("Insert Failed");
                    Console.WriteLine(insertResult);
                }
                Console.ReadLine();
                break;

            case "4":
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("\t DELETE DATA \t");
                Console.WriteLine("============================================");
                Console.Write("Masukkan id :");
                int noIdDel = int.Parse(Console.ReadLine());
                //regions.Delete(noIdDel);

                var delResult = countries.Delete(noIdDel);
                int.TryParse(delResult, out int dResult);
                if (dResult > 0)
                {
                    Console.WriteLine("Delete Success");
                }
                else
                {
                    Console.WriteLine("Delete Failed");
                    Console.WriteLine(delResult);
                }

                Console.ReadLine();
                break;

            case "5":
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("\t update DATA \t");
                Console.WriteLine("============================================");
                Console.Write("Masukkan id :");
                int noIdUpdate = int.Parse(Console.ReadLine());
                Console.Write("Masukkan nama :");
                string nameUpdt = Console.ReadLine();
                //regions.Update(noIdUpdate, nameUpdt);

                var updateResult = countries.Update(noIdUpdate, nameUpdt);
                int.TryParse(updateResult, out int uResult);
                if (uResult > 0)
                {
                    Console.WriteLine("Update Success");
                }
                else
                {
                    Console.WriteLine("Update Failed");
                    Console.WriteLine(updateResult);
                }
                Console.ReadLine();
                break;
            case "6":
                break;
            default:
                Console.Write("Masukkan data yang benar");
                break;
        }


    }
*/






    /*static string ValidationLenght(string name)
    {
        int maxLenght = 10;
        if (name.Length > maxLenght)
        {
            return "Data input terlalu panjang";
        }
        return "Data berhasil ditambahkan";
    }*/

}






