using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public  class GeneralMenu
    {
        public static void List<T>(List<T> items, string title)
        {
            Console.WriteLine($"List of {title}");
            Console.WriteLine("---------------");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }

        }

        //tahap pengembangan, method generic untuk keperluan CRUD
        public static void LooopData(List<Dictionary<string, object>> data, string tableName)
        {
            data = JoinTables.manageDatabase.GetAll(tableName);

            foreach (var row in data)
            {
                foreach (var keyValuePair in row)
                {
                    Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
                }
                Console.WriteLine();
            }
        }



    }
}
