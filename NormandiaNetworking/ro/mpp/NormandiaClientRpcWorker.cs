using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using NormandiaModel.ro.mpp;
//using CompetitionNetworking.ro.mpp.dto;
using NormandiaService.ro.mpp;

namespace NormandiaNetworking.ro.mpp
{
    public class NormandiaClientRpcWorker: IRezervareObserver
    {
        private IRezervareServices _server;
        private TcpClient _connection;
        private NetworkStream _stream;
        private IFormatter _formatter;
        private volatile bool _connected;
        
        public NormandiaClientRpcWorker(IRezervareServices server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = connection.GetStream();
                _formatter = new BinaryFormatter();
                _connected = true;
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        public void Run()
        {
            try
            {
                while (_connected)
                {
                    if (_stream.CanRead && _stream.DataAvailable)
                    {
                        try
                        {
                            var request = _formatter.Deserialize(_stream);
                            var response = HandleRequest((Request) request);
                            if (response != null)
                            {
                                SendResponse((Response) response);
                            }
                        }
                        catch (System.Exception e)
                        {
                            System.Console.WriteLine(e.StackTrace);
                            _connected = false;
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
            finally
            {
                try
                {
                    _stream.Close();
                    _connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        
        private Response HandleRequest(Request request)
        {
            Response response = null;
            RequestType requestType = request._type;
            switch (requestType)
            {
                case RequestType.LOGIN:
                    Console.WriteLine("Login request");
                   // User user = (User)request._data;
                    Client user = (Client)request._data;
                    try
                    {
                        var userOptional = _server.Connect(user.username, user.password, this);
                        if (userOptional != null)
                        {
                            return new Response.Builder().SetType(ResponseType.OK).SetData(userOptional).Build();
                        }
                        else
                        {
                            _connected = false;
                            return new Response.Builder().SetType(ResponseType.ERROR)
                                .SetData("Invalid username or password").Build();
                        }
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                    } 
                case RequestType.LOGOUT:
                    Console.WriteLine("Logout request");
                    String username = (String)request._data;
                    try
                    {
                        _server.Logout(username);
                        _connected = false;
                        return new Response.Builder().SetType(ResponseType.OK).Build();
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                    }
                case RequestType.GET_ALL_CURSE:
                    Console.WriteLine("Get curse request");
                    try
                    {
                            return new Response.Builder().SetType(ResponseType.OK).SetData(_server.GetAllCurse())
                                .Build();
                        }
                        catch (Exception e)
                        {
                            _connected = false;
                            return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                        }
                case RequestType.GET_ALL_CLIENTS:
                         Console.WriteLine("Get clients request");
                            try
                            {
                                return new Response.Builder().SetType(ResponseType.OK).SetData(_server.GetAllUsers())
                                    .Build();
                            }
                            catch (Exception e)
                            {
                                _connected = false;
                                return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                            }
                case RequestType.GET_ALL_REZERVARI:
                                Console.WriteLine("Get rezervari request");
                                try
                                {
                                    return new Response.Builder().SetType(ResponseType.OK).SetData(_server.GetAllRezervari())
                                        .Build();
                                }
                                catch (Exception e)
                                {
                                    _connected = false;
                                    return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                                }
                    case RequestType.REGISTER_REZERVARE:
                        Console.WriteLine("Register rezervare request");
                        Rezervare rezervare = (Rezervare)request._data;
                        try
                        {
                            _server.registerRezervare(rezervare.User, rezervare.Cursa, rezervare.locuri);
                            return new Response.Builder().SetType(ResponseType.OK).Build();
                        }
                        catch (Exception e)
                        {
                            _connected = false;
                            return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                        }
                        case RequestType.GET_ALL_SEATS:
                            Console.WriteLine("Get seats request");
                            try
                            {
                                return new Response.Builder().SetType(ResponseType.OK).SetData(_server.GetAllSeats())
                                    .Build();
                            }
                            catch (Exception e)
                            {
                                _connected = false;
                                return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                            }
                        case RequestType.REGISTER_SEAT:
                            Console.WriteLine("Register seat request");
                            Seat seat = (Seat)request._data;
                            try
                            {
                                _server.registerSeat(seat);
                                return new Response.Builder().SetType(ResponseType.OK).Build();
                            }
                            catch (Exception e)
                            {
                                _connected = false;
                                return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                            }
                /*case RequestType.GET_COMPETITIONS:
                    Console.WriteLine("Get shows request");
                    try
                    {
                        return new Response.Builder().SetType(ResponseType.OK).SetData(_server.GetAllCompetitionsWithNrParticipants())
                            .Build();
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                    }*/

                    break;
               /* case RequestType.GET_PARTICIPANTS_BY_COMPETITION:
                    Console.WriteLine("Get shows by day request");
                    long competitionId =(long) request._data;
                    try
                    {
                        return new Response.Builder().SetType(ResponseType.OK)
                            .SetData(_server.GetAllParticipantsByCompetition(competitionId)).Build();
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                    }*/
               /* case RequestType.REGISTER_PARTICIPANT:
                    Console.WriteLine("Buy tickets request");
                    RegisterParticipantDTO register = (RegisterParticipantDTO)request._data;
                    try
                    {
                        _server.AddParticipant(register._name, register._birthDate, register._competitionsId);
                        return new Response.Builder().SetType(ResponseType.OK).Build();
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new Response.Builder().SetType(ResponseType.ERROR).SetData(e.Message).Build();
                    }*/
                default:
                    return new Response.Builder().SetType(ResponseType.ERROR).SetData("Invalid request").Build();
            }
        }
        
        private void SendResponse(Response response)
        {
            lock (_stream)
            {
                try
                {
                    if (_connection.Connected && _stream.CanWrite)
                    {
                        _formatter.Serialize(_stream, response);
                        _stream.Flush();
                    }
                    else
                    {
                        Console.WriteLine("Connection is not open or stream is not writable.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        
        
        public void registerParticipant()
        {
            
            Console.WriteLine("register rezervare");
            Response response = new Response.Builder().SetType(ResponseType.UPDATE).SetData(null).Build();
            SendResponse(response);
        }
    }
}