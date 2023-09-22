using ConnectionDB.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB.Controllers
{
    public class LocationsController
    {
        private Locations _locations;
        private LocationsView _locationView;
        Locations locations = new Locations();

        public LocationsController(Locations locations, LocationsView locationsView)
        {
            _locations = locations;
            _locationView = locationsView;
        }

        public void GetAll()
        {
            var result = _locations.GetAll();
            if (!result.Any())
            {
                Console.WriteLine("Data Not Found");
            }
            else
            {
                _locationView.List(result, "regions");
            }

        }

        public void Insert()
        {
            var isTrue = true;

            while (isTrue)
            {
                try
                {
                    locations = _locationView.InsertUpdate();

                    if (string.IsNullOrEmpty(locations.StreetAddress))
                    {
                        Console.WriteLine("Region name cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var result = _locations.Insert(new Locations
            {
                Id = locations.Id,
                StreetAddress = locations.StreetAddress,
                PostalCode = locations.PostalCode,
                City = locations.City,
                StateProvince = locations.StateProvince,
                CountryId = locations.CountryId,

            });

            _locationView.Transaction(result);
        }

        public void Update()
        {
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    locations = _locationView.InsertUpdate();
                    if (string.IsNullOrEmpty(locations.StreetAddress))
                    {
                        Console.WriteLine("Region name cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var result = _locations.Update(locations);
            _locationView.Transaction(result);
        }

        public void Delete()
        {

            locations = _locationView.Delete();
            var result = _locations.Delete(locations.Id);
            if (result.Any())
            {
                _locationView.Transaction(result);

            }
            else
            {
                Console.WriteLine("Data Successfully Deleted");
            }
        }
    }
}
