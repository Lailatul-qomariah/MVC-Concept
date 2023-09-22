using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public  class EmpCounLocDepRegVM
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public string DepartementName { get; set; }
        public string StreetAddress { get; set; }
        public string CountryName { get; set; }
        public string RegionsName { get; set; }
       

        public override string ToString()
        {
            return $" Employee Id : {Id} " +
                $"- First_Name : {FirstName} - " +
                $"- Last_Name : {LastName}, " +
                $"- Email : {Email}, " +
                $"- Phone_Number : {PhoneNumber} " +
                $"- Salary : {Salary}, " +
                $"- DepartementName : {DepartementName}, " +
                $"- StreetAddress : {StreetAddress}, " +
                $"- CountryName : {CountryName}, " +
                $"- RegionsName : {RegionsName} "
                ;

        }



    }
}
