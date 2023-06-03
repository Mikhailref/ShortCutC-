using Microsoft.Data.SqlClient;
using System.Data;

namespace ShortCut.Utilities.DataBase.DataBaseInteractionStrategyPattern
{
   
        public interface IDatabaseConnection
        {
            IDbConnection CreateConnection(string connectionLink);
        }

        public interface IDatabaseCommand
        {
            IDbCommand CreateCommand(string query, IDbConnection connection);
        }

    
        public class DatabaseConnection : IDatabaseConnection
        {
            public IDbConnection CreateConnection(string connectionLink)
            {
                IDbConnection connection = new SqlConnection(connectionLink);
                return connection;
            }
        }

        public class DatabaseCommand : IDatabaseCommand
        {
            public IDbCommand CreateCommand(string query, IDbConnection connection)
            {
                IDbCommand command = new SqlCommand(query, (SqlConnection)connection);
                return command;
            }
        }
}


