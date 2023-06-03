using ShortCut.Models.Apps;
using ShortCut.Models.Interfaces;
using ShortCut.Utilities.Factories;
using System.Data;

namespace ShortCut.Utilities.DataBase.DataBaseInteractionStrategyPattern
{
    public class FetchSoftFromDataBase : IStrategy
    {
        private readonly IDatabaseConnection DBconnection;
        private readonly IDatabaseCommand DBcommand;
        private SoftWareFactory SoftWareFactory = new SoftWareFactory();
        public ISoftWare SoftWareObject;

        public string ConnectionString { get; set; } = @"Data Source=SQL8003.site4now.net;Initial Catalog=db_a9a3ce_shortcut;User Id=db_a9a3ce_shortcut_admin;Password=DePloY123@321";
        public string Query { get; set; } = "SELECT sg.[SoftWareGroupID], sg.[SoftWareGroup], a.[AppID], a.[AppName], sc.[ShortCutID], sc.[Description], sc.[ShortCut] " +
                               "FROM [dbo].[SoftWareGroups] sg " +
                               "LEFT JOIN [dbo].[Apps] a ON sg.[SoftWareGroupID] = a.[SoftWareGroupID] " +
                               "LEFT JOIN [dbo].[ShortCuts] sc ON a.[AppID] = sc.[AppID] " +
                               "WHERE sg.[SoftWareGroupID] = @SoftWareGroupID " +
                               "ORDER BY sg.[SoftWareGroupID] ASC;";


        public FetchSoftFromDataBase(IDatabaseConnection dbConnection, IDatabaseCommand dbCommand)
        {
            DBconnection = dbConnection;
            DBcommand = dbCommand;
        }

        public ISoftWare FetchSoftWare(int id)
        {
            //CREATE OBJECT OF THE GROUP by the id, WHERE id=2 => BROWSERS
            SoftWareObject = SoftWareFactory.CreateSoftWare(id);

            try
            {
                using (IDbConnection connection = DBconnection.CreateConnection(ConnectionString))
                {
                    connection.Open();

                    using (IDbCommand command = DBcommand.CreateCommand(Query, connection))
                    {
                        IDbDataParameter parameter = command.CreateParameter();
                        parameter.ParameterName = "@SoftWareGroupID";
                        parameter.Value = id;
                        command.Parameters.Add(parameter);

                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string softwareGroup = reader.GetString(reader.GetOrdinal("SoftWareGroup"));
                                string appName = reader.GetString(reader.GetOrdinal("AppName"));
                                string shortCutDescription = reader.GetString(reader.GetOrdinal("Description"));
                                string shortCut = reader.GetString(reader.GetOrdinal("ShortCut"));

                                SoftWareObject.GroupName = softwareGroup;

                                // Check if an APP with the same NAME already exists in the software OBJECT
                                //App existingApp = SoftWareObject.AppsCollection.FirstOrDefault(app => app.Name == appName);
                                App existingApp = null;
                                foreach (App app in SoftWareObject.AppsCollection)
                                {
                                    if (app.Name == appName)
                                    {
                                        existingApp = app;
                                        break;
                                    }
                                }

                                //If exists, then add to the list that is inside a new array with new shortcut and its description
                                if (existingApp != null)
                                {
                                    existingApp.CreateAndAddArrayOfShortCutToTheList(shortCutDescription, shortCut);
                                }

                                //If id does not exist then create a new app, add and array to its list
                                else
                                {
                                    App newApp = new App(appName);
                                    newApp.CreateAndAddArrayOfShortCutToTheList(shortCutDescription, shortCut);
                                    //And then add this app to the list of the softwareObject
                                    SoftWareObject.AddNewAppToTheList(newApp);
                                }

                            }
                            return SoftWareObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data from database", ex);
            }
        }

    }

}

