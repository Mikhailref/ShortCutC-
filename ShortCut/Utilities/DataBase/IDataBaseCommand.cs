using System.Data;

namespace ShortCut.Utilities.DataBase
{
    public interface IDatabaseCommand
    {
        IDbCommand CreateCommand(string query, IDbConnection connection);
    }
}
