using System;
using System.Collections.Generic;
using System.Data;
using NormandiaModel.ro.mpp;
using log4net;

using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SQLite;
//using System.Data.SQLite;

namespace NormandiaPersistence.ro.mpp
{
    public class SeatDBRepository : IRepository<int,Seat>
    {
        //private JdbcUtils dbUtils;
        private static readonly ILog logger = LogManager.GetLogger(typeof(SeatDBRepository));
        public string connectionString;

        public SeatDBRepository(String con)
        {
            logger.Info("Initializing ClientDBRepository");
            //connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            connectionString = con;
        }

       

        public void delete(object o)
        {
            // Implementarea metodei Delete
        }

        public List<object> findAll()
        {
            return null; // Implementarea metodei FindAll
        }

        public object findOne(object o)
        {
            return null; // Implementarea metodei FindOne
        }

        public List<Seat> FindAll()
        {
            
            List<Seat>seats = new List<Seat>();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Seat";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int id = Convert.ToInt32(dataReader["id"]);
                            int id_rezervare = Convert.ToInt32(dataReader["id_rezervare"]);
                            int seatNumber = Convert.ToInt32(dataReader["seatNumber"]);
                            Rezervare r= findRezervare(id_rezervare);
                            
                            Seat seat = new Seat(r, seatNumber);
                            seat.Id = id;
                            seats.Add(seat);
                        }
                        dataReader.Close();
                    }
                }
                con.Close();
            }

            return seats;
        }

        private Rezervare findRezervare(int id)
        {
            Rezervare r = null;
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Rezervare WHERE id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            int idRezervare = Convert.ToInt32(dataReader["id"]);
                            int idClient = Convert.ToInt32(dataReader["id_client"]);
                            int idCursa = Convert.ToInt32(dataReader["id_cursa"]);
                            int nrLocuri = Convert.ToInt32(dataReader["locuri"]);
                            Client t= findClient(idClient);
                            Cursa c = findCursa(idCursa);
                            Rezervare rezervare = new Rezervare(t, c, nrLocuri);
                            rezervare.Id = idRezervare;
                            r = rezervare;
                            
                        }
                        dataReader.Close();
                    }
                }
                con.Close();
            }
            return r;

            return null;

        }
        public Seat FindOne(int id)
        {
            throw new NotImplementedException();
        }
        private Client findClient(int id)
        {
            Client c = null;
            
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
            return null;
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
            return null;
        }

        public Seat Save(Seat entity)
        {
            logger.Debug($"Saving seat {entity}");

            //string connectionString = "Data Source=path_to_your_database_file.sqlite;Version=3;";
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Seat(id_rezervare, seatNumber) VALUES (@IdRezervare, @SeatNumber)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdRezervare", entity.rezervare.Id); // presupunând că entity.id_rezervare este id-ul rezervării
                    cmd.Parameters.AddWithValue("@SeatNumber", entity.seatNumber);

                    int result = cmd.ExecuteNonQuery();
                    logger.Debug($"Saved {result} instances");
                }
               con.Close();
            }

            return entity;
            /*IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", connectionString);
            var con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Seat (id_rezervare, seatNumber) values (@id_rezervare, @seatNumber)";
                var paramIdRezervare = comm.CreateParameter();
                paramIdRezervare.ParameterName = "@id_rezervare";
                paramIdRezervare.Value = entity.rezervare.Id;
                comm.Parameters.Add(paramIdRezervare);
                var paramSeatNumber = comm.CreateParameter();
                paramSeatNumber.ParameterName = "@seatNumber";
                paramSeatNumber.Value = entity.seatNumber;
                comm.Parameters.Add(paramSeatNumber);
                int rez = comm.ExecuteNonQuery();
                
                Console.WriteLine("Saved {0} instances", rez);
            }
            return entity;*/

        }



        public Seat Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Seat Update(int id, Seat entity)
        {
            logger.Debug($"Updating seat with ID {id}");

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE Seat SET id_rezervare = @IdRezervare, seatNumber = @SeatNumber WHERE id = @Id";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRezervare", entity);
                cmd.Parameters.AddWithValue("@SeatNumber", entity.seatNumber);
                cmd.Parameters.AddWithValue("@Id", id);

              //  con.Open();
                int result = cmd.ExecuteNonQuery();
                logger.Debug($"Updated {result} instances");
              //  con.Close();
            }

            return entity;
        }

        public Seat Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}

