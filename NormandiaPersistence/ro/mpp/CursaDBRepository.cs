using System;
using System.Collections.Generic;
using System.Configuration;

using System.Data.SQLite;
using log4net;
using NormandiaModel.ro.mpp;
using System.Data.SQLite;

namespace NormandiaPersistence.ro.mpp
{
    public class CursaDBRepository : IRepository<int, Cursa>
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CursaDBRepository));
        private string connectionString;

        public CursaDBRepository(String connection)
        {
           // logger.Info("Initializing CursaDBRepository");
            //connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            connectionString = connection;
        }

        /*public Cursa Save(Cursa entity)
        {
            logger.Debug($"Saving cursa {entity}");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Cursa(destinatia,date, time) VALUES (@Destinatia,@Data, @Time)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Destinatia", entity.destinatie);
                cmd.Parameters.AddWithValue("@Data", entity.data);
                cmd.Parameters.AddWithValue("@Time", entity.time);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                logger.Debug($"Saved {result} instances");
            }

            return entity;
        }*/
        public Cursa Save(Cursa entity)
        {
            logger.Debug($"Saving cursa {entity}");

            //string connectionString = "Data Source=path_to_your_database_file.sqlite;Version=3;";
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
               // con.Open();
                string query = "INSERT INTO Cursa(destinatie, date) VALUES (@Destinatia, @Data)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Destinatia", entity.destinatie);
                    cmd.Parameters.AddWithValue("@Data", entity.data);
                 

                    int result = cmd.ExecuteNonQuery();
                    logger.Debug($"Saved {result} instances");
                }
             //   con.Close();
            }

            return entity;
        }
        /*public Cursa Update(int id, Cursa entity)
        {
            logger.Debug($"Updating cursa with ID {id}");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Cursa SET destinatia=@Destinatia, date=@Data,time=@Time WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Destinatia", entity.destinatie);
                cmd.Parameters.AddWithValue("@Data", entity.data);
                cmd.Parameters.AddWithValue("@Time", entity.time);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                logger.Debug($"Updated {result} instances");
            }

            return entity;
        }*/
        public Cursa Update(int id, Cursa entity)
        {
            logger.Debug($"Updating cursa with ID {id}");

            //string connectionString = "Data Source=path_to_your_database_file.sqlite;Version=3;";
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
             //   con.Open();
                string query = "UPDATE Cursa SET destinatia=@Destinatia, date=@Data WHERE id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Destinatia", entity.destinatie);
                    cmd.Parameters.AddWithValue("@Data", entity.data);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int result = cmd.ExecuteNonQuery();
                    logger.Debug($"Updated {result} instances");
                }
            }

            return entity;
        }
        /* public void Delete(int id)
         {
             logger.Debug($"Deleting cursa with ID {id}");

             using (SqlConnection con = new SqlConnection(connectionString))
             {
                 string query = "DELETE FROM Cursa WHERE id = @Id";
                 SqlCommand cmd = new SqlCommand(query, con);
                 cmd.Parameters.AddWithValue("@Id", id);

                 con.Open();
                 int result = cmd.ExecuteNonQuery();
                 logger.Debug($"Deleted {result} instances");
             }
         }*/

         public List<Cursa> FindAll()
         {
             List<Cursa> curse = new List<Cursa>();

             /*using (SQLiteCommand command =new SQLiteCommand("SELECT * FROM Cursa", new SQLiteConnection(connectionString)))
             {
                 using (SQLiteDataReader reader=command.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         int id = reader.GetInt32(0);
                         string destinatie = reader.GetString(1);
                         string date = reader.GetString(2);
                         Cursa cursa = new Cursa(destinatie, date);
                         cursa.Id = id;
                         curse.Add(cursa);
                     }
                 }
             }*/
             using (SQLiteConnection con = new SQLiteConnection(connectionString))
             {
                 string query = "SELECT id,destinatie,date FROM Cursa";
                 con.Open();
                 using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                 {
                     using (SQLiteDataReader reader = cmd.ExecuteReader())
                     {
                         while (reader.Read())
                         {
                             int id = Convert.ToInt32(reader["id"]);
                             string destinatie = Convert.ToString(reader["destinatie"]);
                             string date = Convert.ToString(reader["date"]);
                             Cursa cursa = new Cursa(destinatie, date);
                             cursa.Id = id;
                             curse.Add(cursa);

                         }

                         reader.Close();
                     }
                 }

                 con.Close();
             }
             return curse;
         }

      /*   public Cursa FindOne(int id)
         {
             Cursa cursa = null;

             using (SqlConnection con = new SqlConnection(connectionString))
             {
                 string query = "SELECT * FROM Cursa WHERE id = @Id";
                 SqlCommand cmd = new SqlCommand(query, con);
                 cmd.Parameters.AddWithValue("@Id", id);

                 con.Open();
                 SqlDataReader reader = cmd.ExecuteReader();

                 if (reader.Read())
                 {
                     cursa = new Cursa
                     {
                         Id = Convert.ToInt32(reader["id"]),
                         DataPlecarii = Convert.ToDateTime(reader["data_plecarii"]),
                         Destinatia = reader["destinatia"].ToString()
                     };
                 }
             }

             return cursa;
         }*/

        /*IEnumerable<Cursa> IRepository<int, Cursa>.FindAll()
        {
            throw new NotImplementedException();
        }*/

        Cursa IRepository<int, Cursa>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Cursa Update(int id)
        {
            throw new NotImplementedException();
        }

        public Cursa FindOne(int id)
        {
            throw new NotImplementedException();
        }
    }
}
