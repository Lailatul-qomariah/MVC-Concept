using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConnectionDB
{
    public  class CrudSql
    {

        private static readonly string connectionString = "Data Source=LAPTOP-IQK7879R;Database=db_mcc81;Integrated Security=True;Connect Timeout=30; Integrated Security=True";

        // GET ALL: Region
        public static void GetAllRegions()
        {
            //using untuk otomatis dibersihkan (disposisikan) setelah selesai digunakan
            using var connection = new SqlConnection(connectionString); // objek untuk connect ke db
            using var command = new SqlCommand(); //objek kosong untuk akses command SQL
            command.Connection = connection; //command dihubungkan pada objek connection
            command.CommandText = "SELECT * FROM tbl_regions"; //menagmbil semua data dari tabel regions

            try //proses yang berpotensi menghasilkan kesalahan dilakukan
            {
                connection.Open(); //membuka koneksi ke database

                using var reader = command.ExecuteReader();
                //membaca hasil dari perintah SQL yang dieksekusi. Hasil dari perintah SQL disimpan dalam objek reader

                if (reader.HasRows) //hasrows = memiliki hasil baris?
                    while (reader.Read()) // membaca hasil baris
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0)); //get id nya, berdasarkan urutan kolom (indexnya) disesuaikan dengan tipe datanya
                        Console.WriteLine("Name: " + reader.GetString(1));
                    }
                else
                    Console.WriteLine("No rows found.");

                reader.Close(); //objek reader ditutup
                connection.Close(); //menutup koneksi ke database
            }
            catch (Exception ex)    //menangasi eror
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // GET BY ID: Region
        public static void GetRegionById(int id)
        {
            using var connection = new SqlConnection(connectionString); //objek untuk connection string
            using var command = new SqlCommand(); // bikin objek buat command

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_regions where id = @id;";
            command.Parameters.AddWithValue("@id", id); //id = inputan disistem, @id nya inputan sql
            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read()) // membaca per baris data yang diambil 
                    {
                        Console.WriteLine(($"id              : {reader.GetInt32(0)}"));
                        Console.WriteLine(($"regions name    : {reader.GetString(1)}"));
                    }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }

        }

        // INSERT: Region
        public static void InsertRegion(int id, string name)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_regions VALUES (@id, @name);";

            try
            {
                var pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.Value = id;
                pId.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(pId);

                var pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.Value = name;
                //ValidationLenght(name);
                pName.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(pName);

                connection.Open();

                using var transaction = connection.BeginTransaction(); //memulai transaksinya
                                                                       //dipake untuk memastikan beberapa operasi database terjadi secara bersamaan, entah semua gagal atau berhasil
                try
                {
                    command.Transaction = transaction;

                    var result = command.ExecuteNonQuery(); //= execute, jadi nanti return dari row affectednya ada berapa

                    transaction.Commit(); //jika tidak ada masalah di query dll nya maka transaksi di commit/disimpan di db
                    connection.Close();

                    switch (result) //cek apakah ada row affected
                    {
                        case >= 1:
                            Console.WriteLine("Insert Success");
                            break;
                        default:
                            Console.WriteLine("Insert Failed");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); //jika ada kesalahan dari query atau data yg lain tidak keinput, maka transaksi/proses digagalkan
                    Console.WriteLine($"Error Transaction: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // UPDATE: Region
        public static void UpdateRegion(int id, string name)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_regions SET name = @name WHERE id = @id;";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);

            try
            {
                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;
                    var result = command.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                    switch (result)
                    {
                        case >= 1:
                            Console.WriteLine("Update Success");
                            break;
                        default:
                            Console.WriteLine("Update Failed");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error Transaction: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");

            }
        }

        // DELETE: Region
        public static void DeleteRegion(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "DELETE tbl_regions WHERE id = @id;";
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                int transaction = command.ExecuteNonQuery();
                connection.Close();
                switch (transaction)
                {
                    case >= 1:
                        Console.WriteLine("Deleted Success");
                        break;
                    default:
                        Console.WriteLine("Deleted Failed");
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");

            }
        }
    }
}
