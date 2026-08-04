using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ConnectionRepository
    {
        private readonly string _connectionString;

        public ConnectionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
