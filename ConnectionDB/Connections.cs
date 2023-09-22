using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public class Connections
    {
        private static readonly string connectionString = "Data Source=LAPTOP-IQK7879R;Database=db_mcc81;Integrated Security=True;Connect Timeout=30; Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand GetCommand()
        {
            return new SqlCommand();
        }

        public static SqlParameter SetParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }
    }
}
