using ShortCut.Models.Interfaces;

namespace ShortCut.Utilities.DataBase.DataBaseInteractionStrategyPattern
{

    public class DatabaseSingleton
    {
        private static readonly DatabaseSingleton instance = new DatabaseSingleton(new DatabaseConnection(), new DatabaseCommand());
        private readonly IStrategy strategyForDataBase;

        private readonly IDatabaseConnection _dbConnection;
        private readonly IDatabaseCommand _dbCommand;

        private DatabaseSingleton(IDatabaseConnection dbConnection, IDatabaseCommand dbCommand)
        {
            _dbConnection = dbConnection;
            _dbCommand = dbCommand;
            strategyForDataBase = new FetchSoftFromDataBase(dbConnection, dbCommand);
            
        }

        public static DatabaseSingleton Instance
        {
            get { return instance; }
        }

        public ISoftWare FetchSoftwareFromDatabase(int id)
        {

            return strategyForDataBase.FetchSoftWare(id);
        }
    }
}


