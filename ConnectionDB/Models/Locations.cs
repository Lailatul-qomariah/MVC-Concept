using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConnectionDB
{
    public class Locations
    {
        public static Locations locations = new Locations();
        
        public int Id { get; set; }
        public string StreetAddress {get; set;}
        public string PostalCode { get; set;}
        public string City { get; set;}
        public string StateProvince { get; set;}
        public int CountryId { get; set;}

        public override string ToString()
        {
            return $"Id : {Id} - StreetAddress : {StreetAddress} - PostalCode : {PostalCode} - StateProvince : {StateProvince} - CountryId : {CountryId}";
        }


        public List<Locations> GetAll()
        {
            var locations = new List<Locations>();

            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_locations";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        locations.Add(new Locations
                        {
                            Id = reader.GetInt32(0),
                            StreetAddress = reader.GetString(1),
                            PostalCode = reader.GetString(2),
                            City = reader.GetString(3),
                            StateProvince = reader.GetString(4),
                            CountryId = reader.GetInt32(5)
                            
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return locations;
                }
                reader.Close();
                connection.Close();

                return new List<Locations>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Locations>();
        }

        public Regions GetById(int id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_locations WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Regions()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                    }
                    reader.Close();
                    connection.Close();

                    return new Regions();

                }
                reader.Close();
                connection.Close();

                return new Regions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Regions();


        }

        public string Insert(Locations locations)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_locations VALUES (@id, @street_address, @postal_code, @city, @state_province, @country_id);";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", locations.Id));
                command.Parameters.Add(new SqlParameter("@street_address", locations.StreetAddress));
                command.Parameters.Add(new SqlParameter("@postal_code", locations.PostalCode));
                command.Parameters.Add(new SqlParameter("@city", locations.City));
                command.Parameters.Add(new SqlParameter("@state_province", locations.StateProvince));
                command.Parameters.Add(new SqlParameter("@country_id", locations.CountryId));

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

        public string Update(Locations locations)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_locations SET streetAddress = @street_address, postal_code = @postal_code, city = @city, state_province = @state_province, country_id = @country_id  WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", locations.Id));
                command.Parameters.Add(new SqlParameter("@street_address", locations.StreetAddress));
                command.Parameters.Add(new SqlParameter("@postal_code", locations.PostalCode));
                command.Parameters.Add(new SqlParameter("@city", locations.City));
                command.Parameters.Add(new SqlParameter("@state_province", locations.StateProvince));
                command.Parameters.Add(new SqlParameter("@country_id", locations.CountryId));


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
            command.CommandText = "DELETE tbl_locations WHERE @id = id";

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
