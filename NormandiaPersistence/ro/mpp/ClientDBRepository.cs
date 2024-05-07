
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using log4net;
using NormandiaModel.ro.mpp;

using System.Data.SQLite;

namespace NormandiaPersistence.ro.mpp
{
    public class ClientDBRepository : IRepository<int, Client>
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ClientDBRepository));
        private string connectionString;

        public ClientDBRepository(String connection)
        {
           // logger.Info("Initializing ClientDBRepository");
            //connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            connectionString = connection;
            
        }

        public Client Save(Client entity)
        {
            logger.Debug($"Saving client {entity}");

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "INSERT INTO Client(nume, password) VALUES (@Nume, @Password)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nume", entity.username);
                    cmd.Parameters.AddWithValue("@Password", entity.password);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    logger.Debug($"Saved {result} instances");
                    con.Close();
                }
            }

            return entity;
        }

        public Client Update(int id, Client entity)
        {
            logger.Debug($"Updating client {entity}");

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE Client SET username = @Nume, password = @Password WHERE id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nume", entity.username);
                    cmd.Parameters.AddWithValue("@Password", entity.password);
                    cmd.Parameters.AddWithValue("@Id", id);

                   // con.Open();
                    int result = cmd.ExecuteNonQuery();
                    logger.Debug($"Updated {result} instances");
                }
            }

            return entity;
        }

        public Client Delete(int id)
        {
            // Implementation for Delete method
            return null;
        }

        public List<Client> FindAll()
        {
            List<Client> clients = new List<Client>();

            // Implementation for FindAll method
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "SELECT id, nume, password FROM Client";
                //List<Client> clients = new List<Client>();
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = Convert.ToInt32(reader["id"]);
                            string nume = Convert.ToString(reader["nume"]);
                            string password = Convert.ToString(reader["password"]);
                            Client client = new Client(nume, password);
                            client.Id = Id;
                            clients.Add(client);
                        }
                        reader.Close();
                    }
                   
                }
                con.Close();
            }
            return clients;
        }

        public Client FindByUsername(string username)
        {
            logger.Debug($"Finding client with username: {username}");
            Client c = null;
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "SELECT id, nume, password FROM Client WHERE nume = @Username";
                con.Open();
                
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ensure that both 'nume' and 'password' parameters are provided 

                            int Id = Convert.ToInt32(reader["id"]);
                            string nume= Convert.ToString(reader["nume"]);
                            string password = Convert.ToString(reader["password"]);
                            Client client = new Client(nume, password);
                            client.Id = Id;
                            c = client;
                            logger.Debug($"Found client: {client}");
                            /*Console.WriteLine("afisare client in clientDBRepository");
                            Console.WriteLine(client.password+client.username+client.Id);
                            Console.WriteLine("?");*/
                            
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }

            return c;

            logger.Debug($"Client with username {username} not found");
            return null; // Client with the specified username not found
        }
        public Client FindOne(int id)
        {
            logger.Debug($"Finding client with ID: {id}");

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "SELECT id, username, password FROM Client WHERE id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    //con.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ensure that both 'nume' and 'password' parameters are provided
                            Client client = new Client(
                                Convert.ToString(reader["username"]), // Provide value for 'nume' parameter
                                Convert.ToString(reader["password"]) // Provide value for 'password' parameter
                            );
                            
                             int   Id = Convert.ToInt32(reader["id"]);
                            client.Id = Id;
                            logger.Debug($"Found client: {client}");
                            return client;
                        }
                        reader.Close();
                    }
                }
            }

            logger.Debug($"Client with ID {id} not found");
            return null; // Client with the specified ID not found
        }

        List<Client> IRepository<int, Client>.FindAll()
        {
            List<Client> users = new List<Client>();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Client", new SQLiteConnection(connectionString)))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id=reader.GetInt32(0);
                        String username = reader.GetString(1);
                        String password = reader.GetString(2);
                        Client client = new Client(username, password);
                        client.Id = Id;
                        users.Add(client);
                        
                    }
                    reader.Close();
                }
            }

            return users;
        }

        public Client Update(int id)
        {
            throw new NotImplementedException();
        }

        public string ToString(Client c)
        {
            return c.Id+" "+c.username+" "+c.password;
        }
    }
}
