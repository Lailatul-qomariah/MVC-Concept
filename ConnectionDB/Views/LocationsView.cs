using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB.Views
{
    public  class LocationsView : GeneralMenu
    {
        public Locations InsertUpdate()
        {
            Console.Write("Masukkan ID : ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Masukkan Street Address : ");
            string street = Console.ReadLine();

            Console.Write("Masukkan Postal Code : ");
            string postal = Console.ReadLine();

            Console.Write("Masukkan City : ");
            string city = Console.ReadLine();

            Console.Write("Masukkan State Province : ");
            string state = Console.ReadLine();

            Console.Write("Masukkan Country_id : ");
            int country_id = int.Parse(Console.ReadLine());
            return new Locations
            {
                Id = id,
                StreetAddress = street,
                PostalCode = postal,
                City = city,
                StateProvince = state,
                CountryId = country_id,

            };
        }


        public Locations Delete()
        {
            Console.WriteLine("Delete Region Id   :");
            var id = Convert.ToInt32(Console.ReadLine());

            return new Locations
            {
                Id = id

            };
        }

    }
}
