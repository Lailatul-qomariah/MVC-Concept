using ConnectionDB.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB.Controllers;

public class RegionsController
{
    private Regions _regions;
    private RegionsView _regionsView;
    Regions regions = new Regions();

    public RegionsController (Regions region, RegionsView regionsView)
    {
        _regions = region;
        _regionsView = regionsView;

    }

    public void GetAll()
    {
        var result = _regions.GetAll();
        if (!result.Any()) 
        {
            Console.WriteLine("Data Not Found");
        }
        else
        {
            _regionsView.List(result, "regions");
        }

    }

    public void Insert()
    {
        var isTrue = true;
        
        while (isTrue)
        {
            try
            {
                 regions = _regionsView.InsertUpdate();

                if (string.IsNullOrEmpty(regions.Name))
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
        
        var result = _regions.Insert(new Regions {
            Id = regions.Id,
            Name = regions.Name
        });
        
        _regionsView.Transaction(result);
    }

    public void Update()
    {
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                regions = _regionsView.InsertUpdate();
                if (string.IsNullOrEmpty(regions.Name))
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

        var result = _regions.Update(regions);
        _regionsView.Transaction(result);
    }

    public void Delete()
    {

        regions = _regionsView.Delete();
        var result = _regions.Delete(regions.Id);
        if (result.Any())
        {
            _regionsView.Transaction(result);

        }
        else
        {
            Console.WriteLine("Data Successfully Deleted");
        }
    }


}
