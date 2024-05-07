using System.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using Mono.Data.Sqlite;
using System.Data.SQLite;
namespace NormandiaPersistence.ro.mpp
{

    public static class DBUtils
    {


        private static IDbConnection instance = null;

        public static IDbConnection getConnection(IDictionary<String, string> props)
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                instance = getNewConnection(props);
                instance.Open();
            }
            return instance;
        }

        private static IDbConnection getNewConnection(IDictionary<String, string> props)
        {

            return ConnectionFactory.getInstance().createConnection(props);


        }
    }
    public abstract class ConnectionFactory
    {
        protected ConnectionFactory()
        {
        }

        private static ConnectionFactory instance;

        public static ConnectionFactory getInstance()
        {
            if (instance == null)
            {

                Assembly assem = Assembly.GetExecutingAssembly();
                Type[] types = assem.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(ConnectionFactory)))
                        instance = (ConnectionFactory)Activator.CreateInstance(type);
                }
            }
            return instance;
        }

        public abstract  IDbConnection createConnection(IDictionary<String, string> props);
    }

    public class SqliteConnectionFactory : ConnectionFactory
	{
		public override IDbConnection createConnection(IDictionary<String, string> props)
		{
            Console.WriteLine("creating ... sqlite connection");
           
            //	String connectionString = "URI=file:ChatMPP2017.db,Version=3";
             // String connectionString = props["ConnectionString"];
             String connectionString="Data Source=C:\\Users\\Asus\\source\\repos\\proiectMPP\\proiectMPP\\src\\identifiercsharps;Version=3";
			//String connectionString="URI=file:identifiercsharps.db,Version=3"; // "Data Source=identifiercsharps.db;Version=3;
            return new SQLiteConnection(connectionString);


		}
	}
}