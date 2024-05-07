using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using NormandiaModel.ro.mpp;
using NormandiaNetworking.ro.mpp.dto;
using NormandiaService.ro.mpp;

namespace NormandiaNetworking.ro.mpp
{
    public class NormandiaServicesRpcProxy: IRezervareServices
    {
        private string host;
        private int port;
        private IRezervareObserver client;
        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool finished;
        private BlockingCollection<Response> queueResponses;
        
        
        public NormandiaServicesRpcProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            this.formatter = new BinaryFormatter();
            queueResponses = new BlockingCollection<Response>();
        }
        
        
        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        private void StartReader()
        {
            Thread tw = new Thread(Run);
            tw.Start();
        }
        
        public virtual void Run()
        {
            while (!finished)
            {
                try
                {
                    if (stream.CanRead && stream.DataAvailable)
                    {
                        Response response = (Response)formatter.Deserialize(stream);
                        Console.WriteLine("Response received " + response);
                        if (IsUpdate(response))
                        {
                            HandleUpdate(response);
                        }
                        else
                        {
                            queueResponses.Add(response);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    CloseConnection();
                    finished = true;
                }
            }
        }
        
        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                connection.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        
        public Client Connect(string username, string password, IRezervareObserver client)
        {
            InitializeConnection();
            Client user = new Client(username, password);
           // user.setId(id);
            Request request = new Request.Builder().Type(RequestType.LOGIN).Data(user).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.type == ResponseType.OK)
            {
                this.client = client;
               // return (User) response.data;
               return new Client(username, password);
            }
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                CloseConnection();
                throw new Exception(message);
            }

            return null;
        }
        
        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                response = queueResponses.Take();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }
        
        private void SendRequest(Request request)
        {
            
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Logout(string username)
        {
            Request request = new Request.Builder().Type(RequestType.LOGOUT).Data(username).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                String message = (String)response.data;
                throw new Exception(message);
            }
            
        }
        public List<Client> GetAllUsers()
        {
            Request request = new Request.Builder().Type(RequestType.GET_ALL_CLIENTS).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
            
            return (List<Client>) response.data;
        }
        public List<Cursa>GetAllCurse()
        {
            Request request = new Request.Builder().Type(RequestType.GET_ALL_CURSE).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
            
            return (List<Cursa>) response.data;
        }

        public List<Seat> GetAllSeats()
        {
            Request request = new Request.Builder().Type(RequestType.GET_ALL_SEATS).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
            
            return (List<Seat>) response.data;
        }

        public List<Rezervare>GetAllRezervari()
        {
            Request request = new Request.Builder().Type(RequestType.GET_ALL_REZERVARI).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
            
            return (List<Rezervare>) response.data;
        }

        public void registerRezervare(Client user, Cursa cursa, int nrLocuri)
        {
            Rezervare rezervare = new Rezervare(user, cursa, nrLocuri);
            Request request = new Request.Builder().Type(RequestType.REGISTER_REZERVARE).Data(rezervare).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
        }

        public void registerSeat(Seat seat)
        {
            Seat seat1 = new Seat(seat.rezervare,seat.seatNumber);
            Request request = new Request.Builder().Type(RequestType.REGISTER_SEAT).Data(seat1).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
        }
        /*public AllCompetitionsDTO GetAllCompetitionsWithNrParticipants()
        {
            Request request = new Request.Builder().Type(RequestType.GET_COMPETITIONS).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
            
            return (AllCompetitionsDTO) response.data;
        }*/

        /*public Dictionary<Participant, List<long>> GetAllParticipantsByCompetition(long id)
        {
            Request request = new Request.Builder().Type(RequestType.GET_PARTICIPANTS_BY_COMPETITION).Data(id).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
            
            return (Dictionary<Participant, List<long>>) response.data;
        }*/

        /*public void AddParticipant(string name, DateTime birthDate, List<long> competitionsId)
        {
            RegisterParticipantDTO register = new RegisterParticipantDTO(name, birthDate, competitionsId);
            Request request = new Request.Builder().Type(RequestType.REGISTER_PARTICIPANT).Data(register).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if(response.type == ResponseType.ERROR)
            {
                string message = (string)response.data;
                throw new Exception(message);
            }
        }*/
        
        
        
        
        private bool IsUpdate(Response response)
        {
            return response.type == ResponseType.UPDATE;
        }

        private void HandleUpdate(Response response)
        {
            if (response.type == ResponseType.UPDATE)
            {
                client.registerParticipant();
            }
        }
    }
}