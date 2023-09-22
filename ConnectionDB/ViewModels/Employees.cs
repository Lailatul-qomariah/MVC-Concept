using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConnectionDB
{
    public  class Employees
    {
        public static Employees employees = new Employees();

        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public DateTime Hire_Date { get; set; }
        public int Salary { get; set; }
        public int Commission_Pct { get; set; }
        public int? Manager_Id { get; set; }
        public int Department_Id { get; set; }
        public int Job_Id { get; set; }


        public override string ToString()
        {
            return $" Dept Id : {Id} - First_Name : {First_Name} - Last_Name : {Last_Name}, Email : {Email}, Phone_Number {Phone_Number} " +
                $"Hire_Date {Hire_Date}, Salary : {Salary}, Commission_Pct : {Commission_Pct}, Manager_Id : {Manager_Id}, Department_Id : {Department_Id} " +
                $"Job_Id : {Job_Id}";
        }

        public List<Employees> GetAll()
        {
            var employees = new List<Employees>();

            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_employees;";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employees
                        {
                            Id = reader.GetInt32(0),
                            First_Name = reader.GetString(1),
                            Last_Name = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone_Number = reader.GetString(4),
                            Hire_Date = reader.GetDateTime(5),
                            Salary = reader.GetInt32(6),
                            Commission_Pct = reader.GetInt32(7),
                            Manager_Id = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),

                            //Manager_Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Department_Id = reader.GetInt32(9),
                            Job_Id = reader.GetInt32(10)

                        });
                    }
                    reader.Close();
                    connection.Close();

                    return employees;
                }
                reader.Close();
                connection.Close();

                return new List<Employees>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Employees>();
        }

        public Employees GetById(int id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_employees WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Employees()
                        {
                            Id = reader.GetInt32(0),
                            First_Name = reader.GetString(1),
                            Last_Name = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone_Number = reader.GetString(4),
                            Hire_Date = reader.GetDateTime(5),
                            Salary = reader.GetInt32(6),
                            Commission_Pct = reader.GetInt32(7),
                            Manager_Id = reader.GetInt32(8),
                            Department_Id = reader.GetInt32(9),
                            Job_Id = reader.GetInt32(10)
                        };

                    }
                    reader.Close();
                    connection.Close();

                    return new Employees();

                }
                reader.Close();
                connection.Close();

                return new Employees();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Employees();


        }

        public string Insert(int id, string first_name, string last_name, string email, string phone_number, DateTime hire_date, decimal salary, decimal commission_pct, int manager_id, int department_id, int job_id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_employees VALUES (@id, @first_name, @last_name, @email, @phone_number, @hire_date, @salary, @commission_pct, @manager_id, @department_id, @job_id);";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@first_name", first_name));
                command.Parameters.Add(new SqlParameter("@last_name", last_name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@phone_number", phone_number));
                command.Parameters.Add(new SqlParameter("@hire_date", hire_date));
                command.Parameters.Add(new SqlParameter("@salary", salary));
                command.Parameters.Add(new SqlParameter("@commission_pct", commission_pct));
                command.Parameters.Add(new SqlParameter("@manager_id", manager_id));
                command.Parameters.Add(new SqlParameter("@department_id", department_id));
                command.Parameters.Add(new SqlParameter("@job_id", job_id));

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

        public string Update(int id, string first_name, string last_name, string email, string phone_number, DateTime hire_date, decimal salary, decimal commission_pct, int manager_id, int department_id, int job_id)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_employees SET First_Name = @first_name, Last_Name = @last_name, Email = @email, Phone_Number = @phone_number, Hire_Date = @hire_date, Salary = @salary, Commission_Pct = @commission_pct, Manager_Id = @manager_id, Department_Id = @department_id, Job_Id = @job_id, WHERE Id = @id;";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@first_name", first_name));
                command.Parameters.Add(new SqlParameter("@last_name", last_name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@phone_number", phone_number));
                command.Parameters.Add(new SqlParameter("@hire_date", hire_date));
                command.Parameters.Add(new SqlParameter("@salary", salary));
                command.Parameters.Add(new SqlParameter("@commission_pct", commission_pct));
                command.Parameters.Add(new SqlParameter("@manager_id", manager_id));
                command.Parameters.Add(new SqlParameter("@department_id", department_id));
                command.Parameters.Add(new SqlParameter("@job_id", job_id));

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
            command.CommandText = "DELETE tbl_employees WHERE @id = id";

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
