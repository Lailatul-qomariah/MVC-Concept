using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDB
{
    public  class JobhHistory
    {
        public static JobhHistory jobhHistory= new JobhHistory();

        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }

        public override string ToString()
        {
            return $" EmployeeId : {EmployeeId} - StartDate: {StartDate} - EndDate: {EndDate}, DepartmentId {DepartmentId}, JobId : {JobId}";
        }
        public List<JobhHistory> GetAll()
        {
            var jobhHistories = new List<JobhHistory>();

            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_job_history";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        jobhHistories.Add(new JobhHistory
                        {
                            EmployeeId = reader.GetInt32(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(2),
                            DepartmentId = reader.GetInt32(3),
                            JobId = reader.GetInt32(4)

                        });
                    }
                    reader.Close();
                    connection.Close();

                    return jobhHistories;
                }
                reader.Close();
                connection.Close();

                return new List<JobhHistory>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<JobhHistory>();
        }

        public JobhHistory GetById(int employeeId)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_job_history WHERE id =@id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", employeeId));

                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new JobhHistory()
                        {
                            EmployeeId = reader.GetInt32(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(2),
                            DepartmentId = reader.GetInt32(3),
                            JobId = reader.GetInt32(4)
                        };

                    }
                    reader.Close();
                    connection.Close();

                    return new JobhHistory();

                }
                reader.Close();
                connection.Close();

                return new JobhHistory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new JobhHistory();


        }

        public string Insert(int employeeId, DateTime startDate, DateTime endDate, int departmentId, int jobId)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_job_history VALUES (@employee_id, @start_date, @end_date, @department_id, @job_id);";

            try
            {
                command.Parameters.Add(new SqlParameter("@employee_id", employeeId));
                command.Parameters.Add(new SqlParameter("@start_date", startDate));
                command.Parameters.Add(new SqlParameter("@end_date", endDate));
                command.Parameters.Add(new SqlParameter("@department_id", departmentId));
                command.Parameters.Add(new SqlParameter("@job_id", jobId));

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

        public string Update(int employeeId, DateTime startDate, DateTime endDate, int departmentId, int jobId)
        {
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_job_history SET name = @name, employeeId = @employee_id , startDate = @start_date , endDate = @end_date, departmentId = @department_id, jobId = @job_id WHERE @id = id";

            try
            {
                command.Parameters.Add(new SqlParameter("@employee_id", employeeId));
                command.Parameters.Add(new SqlParameter("@start_date", startDate));
                command.Parameters.Add(new SqlParameter("@end_date", endDate));
                command.Parameters.Add(new SqlParameter("@department_id", departmentId));
                command.Parameters.Add(new SqlParameter("@job_id", jobId));

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

        public string Delete(int employeeId)
        {
       
            using var connection = Connections.GetConnection();
            using var command = Connections.GetCommand();

            try
            {
                command.Parameters.Add(new SqlParameter("@id", employeeId));

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
