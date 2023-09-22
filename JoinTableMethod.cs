using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnectionDB;

public class JoinTableMethod
{
    Employees employ = new Employees();
    Departments depart = new Departments();
    Locations locations = new Locations();
    Countries countries = new Countries();
    Regions regions = new Regions();


    
    public List<EmpCounLocDepRegVM> JoinTable()
    {

        var getEmployees = employ.GetAll();
        var getDepartmen = depart.GetAll();
        var getRegion = regions.GetAll();
        var getLocation = locations.GetAll();
        var getCountries = countries.GetAll();



        var resultJoin = (from r in getRegion
                          join c in getCountries on r.Id equals c.RegionsId
                          join l in getLocation on c.Id equals l.CountryId
                          join d in getDepartmen on l.Id equals d.LocationId
                          join e in getEmployees on d.Id equals e.Department_Id

                          select new EmpCounLocDepRegVM
                          {
                              Id = e.Id,
                              FirstName = e.First_Name,
                              LastName = e.Last_Name,
                              Email = e.Email,
                              PhoneNumber = e.Phone_Number,
                              Salary = e.Salary,
                              DepartementName = d.Name,
                              StreetAddress = l.StreetAddress,
                              CountryName = c.Name,
                              RegionsName = r.Name

                          }).OrderBy(b => b.Id).ToList();

        return resultJoin;
    }

    public List<DeptsvEmpVM> JoinDeptEmp()
    {
        var getEmployees = employ.GetAll();
        var getDepartmen = depart.GetAll();



        var resultJoin = (from d in getDepartmen
                          join e in getEmployees
                          on d.Id equals e.Department_Id into empGroup
                          select new DeptsvEmpVM
                          {

                              DepartementName = d.Name,
                              TotalEmployees = empGroup.Count(),
                              MinSalary = empGroup.Any() ? empGroup.Min(emp => emp.Salary) : 0,
                              MaxSalary = empGroup.Any() ? empGroup.Max(emp => emp.Salary) : 0,
                              AvgSalary = empGroup.Any() ? empGroup.Average(emp => emp.Salary) : 0,

                          }).Where(stat => stat.TotalEmployees > 3)
                            .ToList();

        return resultJoin;
    }






}



