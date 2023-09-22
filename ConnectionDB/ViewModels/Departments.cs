using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public  class Departments
    {
        public static Departments departments = new Departments();

        public int Id {  get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public int LocationId {  get; set; }
        public override string ToString()
        {
            return $" Dept Id : {Id} - Name : {Name} - Manager Id : {ManagerId}, Location Id : {LocationId}";
        }

        public List<Departments> GetAll()
        {
            var departments = new List<Departments>();

            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_departments";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        departments.Add(new Departments
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ManagerId = reader.GetInt32(2),
                            LocationId = reader.GetInt32(3)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return departments;
                }
                reader.Close();
                connection.Close();

                return new List<Departments>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Departments>();
        }

        public Departments GetById(int id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_departments WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Departments()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ManagerId = reader.GetInt32(2),
                            LocationId = reader.GetInt32(3)
                        };

                    }
                    reader.Close();
                    connection.Close();

                    return new Departments();

                }
                reader.Close();
                connection.Close();

                return new Departments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Departments();


        }

        public string Insert(int id, string name, int managerId, int locationId)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_departments VALUES (@id, @name, @managerId, @locationId);";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@managerId", managerId));
                command.Parameters.Add(new SqlParameter("@locationId", locationId));

                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;

                    var result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    return result.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error Transaction: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Update(int id, string name, int managerId, int locationId)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_departments SET name = @name, managerId = @managerId, locationID = @locationId WHERE @id = id";

            try
            {
                
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@managerId", managerId));
                command.Parameters.Add(new SqlParameter("@locationId", locationId));

                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;

                    var result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    return result.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error Transaction: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

        public string Delete(int id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();


            command.Connection = connection;
            command.CommandText = "DELETE tbl_departments WHERE @id = id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;

                    var result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    return result.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error Transaction: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }


        }
    }
}
