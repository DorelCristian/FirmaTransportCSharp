using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormandiaClient.ro.mpp;
using NormandiaModel.ro.mpp;
using NormandiaNetworking.ro.mpp;
using NormandiaService.ro.mpp;
using NormandiaPersistence.ro.mpp;
namespace NormandiaClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IRezervareServices server = new NormandiaServicesRpcProxy("127.0.0.1", 44444);
            string connectionString = "Data Source=C:\\Users\\Asus\\source\\repos\\proiectMPP\\proiectMPP\\src\\identifiercsharps;Version=3";
           // ClientDBRepository clientDBRepository = new ClientDBRepository(@"Data Source=C:\Users\Asus\source\repos\proiectMPP\proiectMPP\src\identifiercsharps;Version=3;");
          
           CursaDBRepository cursaDbRepository =new CursaDBRepository( connectionString);
           ClientDBRepository clientDbRepository = new ClientDBRepository(connectionString); 
           RezervareDBRepository rezervareDBRepository=new RezervareDBRepository(connectionString);
           
          /* Console.WriteLine("-----");
           Console.WriteLine(clientDbRepository.FindByUsername("Ionel").username);
            Console.WriteLine(clientDbRepository.FindByUsername("Ionel").password);
            Console.WriteLine(clientDbRepository.FindByUsername("Ionel").Id);
            Console.WriteLine("-----");*/
            
 
           List<Client>clients=clientDbRepository.FindAll();
            
           /* foreach (Client client in clients)
            {
                Console.WriteLine(client.username);
                Console.WriteLine(client.password);
                Console.WriteLine(client.Id);
                
            }*/
            
            List<Cursa>curse=cursaDbRepository.FindAll();
            foreach (Cursa cursa in curse)
            {
                Console.WriteLine(cursa.Id);
                Console.WriteLine(cursa.destinatie);
                Console.WriteLine(cursa.data);
                
            }
            Console.WriteLine("Rezervari--------");
            List<Rezervare>rezervari=rezervareDBRepository.FindAll();
            foreach (Rezervare rezervare in rezervari)
            {
                Console.WriteLine(rezervare.Id);
                Console.WriteLine(rezervare.User.username);
                //Console.WriteLine(rezervare.Cursa.destinatie);
                Console.WriteLine(rezervare.locuri);
                Console.WriteLine("--------");
            }

            SeatDBRepository seatDbRepository = new SeatDBRepository(connectionString);
            Seat sst = new Seat(rezervari[0], 3);
           // seatDbRepository.Save(sst);
            
            
            Rezervare re = new Rezervare(clients[0], curse[0], 2);
          //  rezervareDBRepository.Save(re);
            
            
           /* Cursa cct = new Cursa("teste", "33-33-33");
            cursaDbRepository.Save(cct);

            Client ct = new Client("tests", "teste");
            clientDbRepository.Save(ct);
            Console.WriteLine("--------");
            
            Rezervare re=new Rezervare(clients[1],curse[1],2);
            rezervareDBRepository.Save(re);*/
            
           /* SeatDBRepository seatDbRepository=new SeatDBRepository(connectionString);
            Seat seat=new Seat(re,3);
            List<Seat>seats=seatDbRepository.FindAll();
            foreach (var s in seats)
            {
                Console.WriteLine(s.ToString());
            }*/
           
           
            /*
            //IEnumerable<Client> clients = clientDBRepository.FindAll();
           List<Client> clients = server.GetAllUsers();
            Console.WriteLine("Clients:");
            foreach (Client I in clients)
            {
                Console.WriteLine(I.username);
                Console.WriteLine(I.password);
                Console.WriteLine(I.Id);
            }
            Console.WriteLine("Curses:");
          /* List<Cursa>curse=server.GetAllCurse();
            foreach (Cursa cursa in curse)
            {
                Console.WriteLine(cursa.Id);
                Console.WriteLine(cursa.destinatie);
                Console.WriteLine(cursa.data);
                
            }*/
            Application.Run(new LogIn(server));
            //Application.Run(new Form1());
        }
    }
}