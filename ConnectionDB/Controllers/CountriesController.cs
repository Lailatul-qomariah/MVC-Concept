using ConnectionDB.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB.Controllers
{
    public class CountriesController
    {
        private Countries _countries;
        private CountriesView _countriesView;
        Countries countries = new Countries();

        public CountriesController(Countries countries, CountriesView countriesView)
        {
            _countries = countries;
            _countriesView = countriesView;
        }
        public void GetAll()
        {
            var result = _countries.GetAll();
            if (!result.Any())
            {
                Console.WriteLine("Data Not Found");
            }
            else
            {
                _countriesView.List(result, "countries");
            }

        }

        public void Insert()
        {
            var isTrue = true;

            while (isTrue)
            {
                try
                {
                    countries = _countriesView.InsertUpdate();

                    if (string.IsNullOrEmpty(countries.Name))
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

            var result = _countries.Insert(new Countries
            {
                Id = countries.Id,
                Name = countries.Name
            });

            _countriesView.Transaction(result);
        }

        public void Update()
        {
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    countries = _countriesView.InsertUpdate();
                    if (string.IsNullOrEmpty(countries.Name))
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

            var result = _countries.Update(countries);
            _countriesView.Transaction(result);
        }

        public void Delete()
        {

            countries = _countriesView.Delete();
            var result = _countries.Delete(countries.Id);
            if (result.Any())
            {
                _countriesView.Transaction(result);

            }
            else
            {
                Console.WriteLine("Data Successfully Deleted");
            }
        }
    }
}
