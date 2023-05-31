using System.Data;

namespace ShortCut.Utilities.DataBase
{
  
        public interface IDatabaseConnection
        {
            IDbConnection CreateConnection(string connectionLink);
        }
   
}
