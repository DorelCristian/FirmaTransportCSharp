using System;
using System.Collections;
using NormandiaPersistence.ro.mpp;
using log4net.Config;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using NormandiaNetworking.ro.mpp;
//using NormandiaNetworking.ro.mpp.protobuffprotocol;
using NormandiaNetworking.ro.mpp.utils;

using NormandiaService.ro.mpp;


namespace NormandiaServer.ro.mpp
{
    public class StartRpcServer
    {
        public static void Main(string[] args)
        {
            //string connectionString = @"Data Source=C:\Users\Asus\source\repos\proiectMPP\proiectMPP\identifiercsharps";
           // ClientDBRepository clientDBRepository = new ClientDBRepository(connectionString);
          // string connectionString = @"Data Source=C:\\Users\\Asus\\source\\repos\\proiectMPP\\proiectMPP\\src\\identifiercsharps;Version=3;";
          string connectionString = "Data Source=C:\\Users\\Asus\\source\\repos\\proiectMPP\\proiectMPP\\src\\identifiercsharps";
           ClientDBRepository clientDBRepository = new ClientDBRepository(connectionString);
           // C:\Users\Asus\source\repos\proiectMPP\proiectMPP\src\identifiercsharps
            CursaDBRepository cursaDBRepository = new CursaDBRepository(connectionString);
            RezervareDBRepository rezervareDBRepository=new RezervareDBRepository(connectionString);
            //SeatDBRepository seatDBRepository = new SeatDBRepository(@"Data Source=C:\Users\Asus\source\repos\proiectMPP\proiectMPP\src\identifiercsharps;Version=3");
            SeatDBRepository seatDBRepository = new SeatDBRepository(connectionString);

            
            /*IUserRepository userRepository;
            ICompetitionRepository competitionRepository;
            IParticipantRepository participantRepository;
            IRegistrationRepository registrationRepository;*/
            
            
            XmlConfigurator.Configure(new System.IO.FileInfo("App.config"));
            
            IDictionary<String, string> props = new SortedList<String, String>();
            
            props.Add("ConnectionString", GetConnectionStringByName("competitionDB"));
            
            /*userRepository = new UserDbRepository(props);
            competitionRepository = new CompetitionDbRepository(props);
            participantRepository = new ParticipantDbRepository(props);
            registrationRepository = new RegistrationDbRepository(props);*/
            IRezervareServices rezervareServices = new RezervareServices(clientDBRepository, cursaDBRepository, rezervareDBRepository, seatDBRepository);
            //ICompetitionServices competitionServices = new CompetitionServices(userRepository, competitionRepository, participantRepository, registrationRepository);
            SerialServer server=new SerialServer("127.0.0.1",44444,rezervareServices);
           // SerialServerProto server=new SerialServerProto("127.0.0.1",44444,rezervareServices);
            server.Start();
            Console.WriteLine("Server started...");
            Console.ReadLine();
            
        }
        
        
        
        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
        
        public class SerialServer : ConcurrentServer
        {
            private IRezervareServices server;
            
            private NormandiaClientRpcWorker worker;

            public SerialServer(string host, int port, IRezervareServices server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialServer...");
            }

            protected override Thread createWorker(TcpClient client)
            {
                worker = new NormandiaClientRpcWorker(server, client);
                return new Thread(new ThreadStart(worker.Run));
            }
        }
        
        /*public class SerialServerProto : ConcurrentServer
        {
            private IRezervareServices server;
           // private NormandiaClientRpcWorkerProto worker;

            public SerialServerProto(string host, int port, IRezervareServices server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialServerProto...");
            }

            protected override Thread createWorker(TcpClient client)
            {
                worker = new NormandiaClientRpcWorkerProto(server, client);
                return new Thread(new ThreadStart(worker.Run));
                return null;
            }
        }*/
    }
}