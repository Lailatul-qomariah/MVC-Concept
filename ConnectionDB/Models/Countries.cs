using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public class Countries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionsId { get; set; }

        public static Countries countries = new Countries();

        public override string ToString()
        {
            return $"{Id} - {Name} - {RegionsId}";
        }

        public List<Countries> GetAll()
        {
            var countries = new List<Countries>();

            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_countries";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        countries.Add(new Countries
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            RegionsId = reader.GetInt32(2)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return countries;
                }
                reader.Close();
                connection.Close();

                return new List<Countries>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Countries>();
        }

        public Countries GetById(int id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_countries WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Countries()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            RegionsId = reader.GetInt32(2)

                        };

                    }
                    reader.Close();
                    connection.Close();

                    return new Countries();

                }
                reader.Close();
                connection.Close();

                return new Countries();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Countries();


        }

        public string Insert(Countries countries)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_countries VALUES (@id, @name, @regions_id);";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", countries.Id));
                command.Parameters.Add(new SqlParameter("@name", countries.Name));
                command.Parameters.Add(new SqlParameter("@regions_id", countries.RegionsId));

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

        public string Update(Countries countries)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_countries SET name = @name, regions_id = @regionsId WHERE id =@id;";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", countries.Id));
                command.Parameters.Add(new SqlParameter("@name", countries.Name));
                command.Parameters.Add(new SqlParameter("@regionsId", countries.RegionsId));

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
            command.CommandText = "DELETE tbl_countries WHERE @id = id";

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
