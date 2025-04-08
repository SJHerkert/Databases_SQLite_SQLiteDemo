using Microsoft.Data.Sqlite;
using System.Text;


namespace SQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {            
            var sqlite_conn = CreateConnection();
            Console.WriteLine("Hello Sqlite!");

            CreateTable(sqlite_conn);
            InsertData(sqlite_conn);
            ReadData(sqlite_conn);
        }

        static SqliteConnection CreateConnection()
        {
            string connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = "database.db",
                Mode = SqliteOpenMode.ReadWriteCreate,
            }.ToString();

            // Create a new database connection:
            var sqlite_conn = new SqliteConnection(connectionString);
         
            try
            {
                sqlite_conn.Open();
                Console.WriteLine("Connection worked!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection didn't work: {ex.Message}");
            }
            return sqlite_conn;
        }

        static void CreateTable(SqliteConnection conn)
        {
            try
            {
                // Ensure the connection is open
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                SqliteCommand sqlite_cmd;
                string Createsql = "CREATE TABLE IF NOT EXISTS SampleTable(Col1 VARCHAR(20), Col2 INT)";
                string Createsql1 = "CREATE TABLE IF NOT EXISTS SampleTable1(Col1 VARCHAR(20), Col2 INT)";
                sqlite_cmd = conn.CreateCommand();

                // Execute the first table creation
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();

                // Execute the second table creation
                sqlite_cmd.CommandText = Createsql1;
                sqlite_cmd.ExecuteNonQuery();

                Console.WriteLine("Tables created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating tables: {ex.Message}");
            }
        }

        static void InsertData(SqliteConnection conn)
        {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ";
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
           sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
           sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;


            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();


            conn.Open();

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable1";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}