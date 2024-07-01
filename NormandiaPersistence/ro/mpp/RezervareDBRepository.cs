using System;
using System.Collections.Generic;
using System.Configuration;

using System.Data.SQLite;
using log4net;
using NormandiaModel.ro.mpp;


namespace NormandiaPersistence.ro.mpp
{
    public class RezervareDBRepository : IRepository<long, Rezervare>
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RezervareDBRepository));
        private string connectionString;

        public RezervareDBRepository(String connection)
        {
           // logger.Info("Initializing RezervareDBRepository");
            //connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            connectionString = connection;
        }

        public Rezervare Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<Rezervare> FindAll()
        {
            List<Rezervare>rezervari= new List<Rezervare>();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                
                string query = "SELECT * FROM Rezervare";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            int Id=Convert.ToInt32(reader["id"]);
                            int id_client =Convert.ToInt32(reader["id_client"]);
                            Client c = findClient(id_client);
                            int id_cursa = Convert.ToInt32(reader["id_cursa"]);
                            Console.WriteLine("Id cursa DBRepo"+id_cursa);
                            Cursa cursa = findCursa(id_cursa);
                            Console.WriteLine("Cursa DBRepo"+cursa);
                           // cursa.Id = id_cursa;
                           // c.Id = id_client;
                            int locuri = Convert.ToInt32(reader["locuri"]);
                            Rezervare rezervare = new Rezervare(c, cursa, locuri);
                            rezervare.Id = Id;
                            rezervari.Add(rezervare);
                        }
                        reader.Close();
                        
                        
                        
                    }
                    
                    
                }
                con.Close();
            }
            return rezervari;
            
            
            /*List<Rezervare>rezervari= new List<Rezervare>();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Rezervare", new SQLiteConnection(connectionString)))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id=reader.GetInt32(0);
                        int id_client =reader.GetInt32(1);
                        Client c = findClient(id_client);
                        int id_cursa = reader.GetInt32(2);
                        Cursa cursa = findCursa(id_cursa);
                        int locuri = reader.GetInt32(3);
                        Rezervare rezervare = new Rezervare(c, cursa, locuri);
                        rezervare.Id = Id;
                        //String username = reader.GetString(1);
                       // String password = reader.GetString(2);
                       // Client client = new Client(username, password);
                        
                        rezervari.Add(rezervare);
                        
                    }
                }
            }*/
          
          
           
        }

        private Client findClient(int id)
        {
            Client c=null;
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "SELECT * FROM Client WHERE id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    
                    int idN= Convert.ToInt32(reader["id"]);
                    string username = reader["nume"].ToString();
                    string password = reader["password"].ToString();
                    Client client = new Client(username, password);
                    client.Id = idN;
                    client.setId(idN);
                    c = client;
                    
                }
                reader.Close();
                con.Close();
            }

            return c;
        }
        private Cursa findCursa(int id)
        {
            Cursa c = null;
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "SELECT * FROM Cursa WHERE id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                
                con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int idN = Convert.ToInt32(reader["id"]);
                    string destinatie = reader["destinatie"].ToString();
                    string date = reader["date"].ToString();
                    Cursa cursa = new Cursa(destinatie, date);
                    cursa.Id=idN;
                    cursa.setId(idN);
                    c = cursa;
                    
                }
                reader.Close();
                con.Close();
            }
            return c;
            
        }

        public Rezervare FindOne(long id)
        {
            throw new NotImplementedException();
        }

        public Rezervare Save(Rezervare entity)
        {
            /*logger.Debug($"Saving rezervare {entity}");

            //string connectionString = "Data Source=path_to_your_database_file.sqlite;Version=3;";
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Rezervare(id_client, id_cursa, locuri) VALUES (@IdClient, @IdCursa, @Locuri)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdClient", entity.User.Id);
                    cmd.Parameters.AddWithValue("@IdCursa", entity.Cursa.Id);
                    cmd.Parameters.AddWithValue("@Locuri", entity.locuri);

                    int result = cmd.ExecuteNonQuery();
                    logger.Debug($"Saved {result} instances");
                }
            }*/

            
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", connectionString);
            var con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Rezervare (id_client, id_cursa, locuri) values (@id_client, @id_cursa, @locuri)";
                var paramIdClient = comm.CreateParameter();
                paramIdClient.ParameterName = "@id_client";
                paramIdClient.Value = entity.User.Id;
                comm.Parameters.Add(paramIdClient);
                var paramIdCursa = comm.CreateParameter();
                paramIdCursa.ParameterName = "@id_cursa";
                paramIdCursa.Value = entity.Cursa.Id;
                comm.Parameters.Add(paramIdCursa);
                var paramLocuri = comm.CreateParameter();
                paramLocuri.ParameterName = "@locuri";
                paramLocuri.Value = entity.locuri;
                comm.Parameters.Add(paramLocuri);
                int rez = comm.ExecuteNonQuery();
                Console.WriteLine("Saved {0} instances", rez);
            }
          //  con.Close();
            return entity;
        }

        public Rezervare Update(long id, Rezervare entity)
        {
            logger.Debug($"Updating rezervare with ID {id}");

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE Rezervare SET id_client = @IdClient, id_cursa = @IdCursa, locuri = @Locuri WHERE id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@IdClient", entity.User.getId());
                cmd.Parameters.AddWithValue("@IdCursa", entity.Cursa.getId());
                cmd.Parameters.AddWithValue("@Locuri", entity.locuri);
                cmd.Parameters.AddWithValue("@Id", id);

                //con.Open();
                int result = cmd.ExecuteNonQuery();
                logger.Debug($"Updated {result} instances");
               // con.Close();
            }

            return entity;
        }

        public Rezervare Update(long id)
        {
            throw new NotImplementedException();
        }

        /*public void Delete(long id)
        {
            logger.Debug($"Deleting rezervare with ID {id}");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Rezervare WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                logger.Debug($"Deleted {result} instances");
            }
        }*/

        /*public List<Rezervare> FindAll()
        {
            List<Rezervare> rezervari = new List<Rezervare>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Rezervare";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Rezervare rezervare = new Rezervare
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        Client = new Client { Id = Convert.ToInt64(reader["id_client"]) },
                        Cursa = new Cursa { Id = Convert.ToInt64(reader["id_cursa"]) },
                        Locuri = Convert.ToInt64(reader["locuri"])
                    };
                    rezervari.Add(rezervare);
                }
            }

            return rezervari;
        }*/

        /*public Rezervare FindOne(long id)
        {
            Rezervare rezervare = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Rezervare WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    rezervare = new Rezervare
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        Client = new Client { Id = Convert.ToInt64(reader["id_client"]) },
                        Cursa = new Cursa { Id = Convert.ToInt64(reader["id_cursa"]) },
                        Locuri = Convert.ToInt64(reader["locuri"])
                    };
                }
            }

            return rezervare;
        }*/
    }
}
