using System.Data;
using MySql.Data.MySqlClient;

namespace db{
    public class DatabaseConnection : IDatabaseConnection
    {
        public string? _connectionString { get ; set; }
        public DatabaseConnection(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection GetConnection(){
            return new MySqlConnection(_connectionString);
        }

    }


    public  interface IDatabaseConnection{
        string? _connectionString {get;set;}

        IDbConnection GetConnection();
    }
}