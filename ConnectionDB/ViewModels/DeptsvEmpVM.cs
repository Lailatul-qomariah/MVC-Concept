using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public class DeptsvEmpVM
    {
        public int MaxSalary { get; set; }
        public int TotalEmployees { get; set; }
        public int MinSalary { get; set; }
        public double AvgSalary { get; set; }
        public string DepartementName { get; set; }


        public override string ToString()
        {
            return $"DepartementName : {DepartementName}" +
                $" - Total Employee : {TotalEmployees}" +
                $" - Max Salary : {MaxSalary}" +
                $" - MinSalary : {MinSalary}" +
                $" - AvgSalary : {AvgSalary}";

        }

    }
}
