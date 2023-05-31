using Microsoft.Data.SqlClient;
using ShortCut.Models.Apps;
using ShortCut.Models.Interfaces;
using ShortCut.Models.SoftWareGroups;
using ShortCut.Utilities.Factories;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace ShortCut.Utilities.DataBase
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public IDbConnection CreateConnection(string connectionLink)
        {
            return new SqlConnection(connectionLink);
        }
    }


    public class DatabaseCommand : IDatabaseCommand
    {
        public IDbCommand CreateCommand(string query, IDbConnection connection)
        {
            return new SqlCommand(query, (SqlConnection)connection);
        }
    }

    public sealed class DatabaseSingleton
    {
        private static readonly DatabaseSingleton instance = new DatabaseSingleton();
        private readonly IDatabaseConnection DBconnection;
        private readonly IDatabaseCommand DBcommand;
        private string ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ShortCut; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        private string Query = "SELECT sg.[SoftWareGroupID], sg.[SoftWareGroup], a.[AppID], a.[AppName], sc.[ShortCutID], sc.[Description], sc.[ShortCut] " +
                               "FROM [dbo].[SoftWareGroups] sg " +
                               "LEFT JOIN [dbo].[Apps] a ON sg.[SoftWareGroupID] = a.[SoftWareGroupID] " +
                               "LEFT JOIN [dbo].[ShortCuts] sc ON a.[AppID] = sc.[AppID] " +
                               "WHERE sg.[SoftWareGroupID] = @SoftWareGroupID " +
                               "ORDER BY sg.[SoftWareGroupID] ASC;";
        private DatabaseSingleton()
        {
            DBconnection = new DatabaseConnection();
            DBcommand = new DatabaseCommand();
        }

        public static DatabaseSingleton Instance
        {
            get
            {
                return instance;
            }
        }


        private SoftWareFactory SoftWareFactory = new SoftWareFactory();
        public ISoftWare SoftWareObject;

        public ISoftWare FetchSoftWareFromDatabase(int id)
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


        //public void WriteSoftWareToDataBase(IPerson person)
        //{

        //    using (IDbConnection connection = DBconnection.CreateConnection(ConnectionString))
        //    {
        //        connection.Open();

        //        using (IDbCommand command = DBcommand.CreateCommand("INSERT INTO [MLM SQL].[dbo].[People] (Id, PersonType, Name, Age, Wallet) VALUES(@Id, @PersonType, @Name, @Age, @Wallet)", connection))
        //        {
        //            IDbDataParameter parameterId = command.CreateParameter();
        //            parameterId.ParameterName = "@Id";
        //            parameterId.Value = person.Id;
        //            command.Parameters.Add(parameterId);

        //            IDbDataParameter parameterPersonType = command.CreateParameter();
        //            parameterPersonType.ParameterName = "@PersonType";
        //            parameterPersonType.Value = person.PersonType;
        //            command.Parameters.Add(parameterPersonType);

        //            IDbDataParameter parameterName = command.CreateParameter();
        //            parameterName.ParameterName = "@Name";
        //            parameterName.Value = person.Name;
        //            command.Parameters.Add(parameterName);

        //            IDbDataParameter parameterAge = command.CreateParameter();
        //            parameterAge.ParameterName = "@Age";
        //            parameterAge.Value = person.Age;
        //            command.Parameters.Add(parameterAge);

        //            IDbDataParameter parameterWallet = command.CreateParameter();
        //            parameterWallet.ParameterName = "@Wallet";
        //            parameterWallet.Value = person.Wallet;
        //            command.Parameters.Add(parameterWallet);


        //            int RowsAffected = command.ExecuteNonQuery();
        //        }
        //    }

        //}
    }

}
