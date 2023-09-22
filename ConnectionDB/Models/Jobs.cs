using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ConnectionDB
{
    public  class Jobs
    {
        private readonly string connectionString = "Data Source=LAPTOP-IQK7879R;Database=db_mcc81;Integrated Security=True;Connect Timeout=30; Integrated Security=True";
        public int Id { get; set; }
        public string JobTitle {get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }

        public override string ToString()
        {
            return $" Job Id : {Id} - Job Title : {JobTitle} - Min Salary : {MinSalary}, MaxSalary {MaxSalary}";
        }

        public List<Jobs> GetAll()
        {
            var jobs = new List<Jobs>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_jobs";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        jobs.Add(new Jobs
                        {
                            Id = reader.GetInt32(0),
                            JobTitle = reader.GetString(1),
                            MinSalary = reader.GetInt32(2),
                            MaxSalary = reader.GetInt32(3)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return jobs;
                }
                reader.Close();
                connection.Close();

                return new List<Jobs>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Jobs>();
        }

        public Jobs GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_jobs WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Jobs()
                        {
                            Id = reader.GetInt32(0),
                            JobTitle = reader.GetString(1),
                            MinSalary = reader.GetInt32(2),
                            MaxSalary = reader.GetInt32(3)
                        };

                    }
                    reader.Close();
                    connection.Close();

                    return new Jobs();

                }
                reader.Close();
                connection.Close();

                return new Jobs();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Jobs();


        }

        public string Insert(int id, string jobTitle, int minsalary, int maxSalary)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_jobs VALUES (@Id, @jobTitle, @minSalary, @maxSalary);";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@jobTitle", jobTitle));
                command.Parameters.Add(new SqlParameter("@minSalary", minsalary));
                command.Parameters.Add(new SqlParameter("@maxSalary", maxSalary));

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

        public string Update(int id, string jobTitle, int minSalary, int maxSalary)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_jobs SET id = @Id, jobTitle = @jobTitle, minSalary = @minSalary, maxSalary @maxSalary WHERE @id = id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@jobTitle", jobTitle));
                command.Parameters.Add(new SqlParameter("@minSalary", minSalary));
                command.Parameters.Add(new SqlParameter("@maxSalary", maxSalary));

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
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "DELETE tbl_jobs WHERE @id = id";

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
