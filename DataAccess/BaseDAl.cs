using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseDAl
    {
        public MemberDataProvider dataProvider { get; set; }

        public SqlConnection connection = null;

        public BaseDAl()
        {
            var connectionString = GetConnectionString();
            dataProvider = new MemberDataProvider(connectionString);
        }

        public string GetConnectionString()
        {
            string connectionString;
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            connectionString = config["ConnectionStrings:MemberDB"];
            return connectionString;
        }

        public void CloseConnection() => dataProvider.CloseConnection(connection);
    }
}
