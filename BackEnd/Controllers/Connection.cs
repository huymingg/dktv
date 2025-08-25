using Npgsql;

namespace apithethuvien.Controllers
{
    public class Connection
    {
        private string _connectionString;
        private NpgsqlConnection _connection;

        public Connection()
        {
            _connectionString = $"Host=localhost;Port=5432;Username=postgres;Password=123;Database=OCRP";
            _connection = new NpgsqlConnection(_connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                _connection.Open();
                Console.WriteLine("Kết nối thành công tới PostgreSQL.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi kết nối: " + ex.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                Console.WriteLine("Đã đóng kết nối.");
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
