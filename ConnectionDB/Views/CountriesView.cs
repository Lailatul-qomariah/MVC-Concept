using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB.Views;

public class CountriesView : GeneralMenu
{


    public Countries InsertUpdate()
    {
        Console.WriteLine("Insert Countries Id");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert CountriesName");
        var name = Console.ReadLine();
        Console.WriteLine("Insert Region Id");
        var regionId = Convert.ToInt32(Console.ReadLine());
        return new Countries
        {
            Id = id,
            Name = name,
            RegionsId = regionId
        };
    }

    public Countries Delete()
    {
        Console.WriteLine("Delete Region Id   :");
        var id = Convert.ToInt32(Console.ReadLine());

        return new Countries
        {
            Id = id

        };
    }
}
