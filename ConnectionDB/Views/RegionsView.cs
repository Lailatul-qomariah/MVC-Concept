using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConnectionDB.Views;

public class RegionsView : GeneralMenu
{

    public Regions InsertUpdate()
    {
        Console.WriteLine("Insert Region Id");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert Region Name");
        var name = Console.ReadLine();
        return new Regions
        {
            Id = id,
            Name = name
        };
    }


    public Regions Delete()
    {
        Console.WriteLine("Delete Region Id   :");
        var id = Convert.ToInt32(Console.ReadLine());

        return new Regions
        {
            Id = id
            
        }; 
    }

}
