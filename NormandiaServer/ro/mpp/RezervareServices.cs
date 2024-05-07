using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using NormandiaModel.ro.mpp;
using NormandiaPersistence.ro.mpp;
using NormandiaService.ro.mpp;

namespace NormandiaServer.ro.mpp
{
    public class RezervareServices:IRezervareServices
    {
        /*private IUserRepository _userRepo;
        private ICompetitionRepository _competitionRepo;
        private IParticipantRepository _participantRepo;
        private IRegistrationRepository _registrationRepo;*/
        private ClientDBRepository _clientDBRepository;
        private CursaDBRepository _cursaDBRepository;
        private RezervareDBRepository _rezervareDBRepository;
        private SeatDBRepository _seatDBRepository;
        private ConcurrentDictionary<string, IRezervareObserver> _loggedClients;
        private readonly int defaultThreads = 5;
        private readonly object _lock = new object();
        
        
        public RezervareServices(ClientDBRepository clientRepo, CursaDBRepository cursaRepo, RezervareDBRepository rezervareRepo, SeatDBRepository seatRepo)
        {
            this._clientDBRepository = clientRepo;
            this._cursaDBRepository = cursaRepo;
            this._rezervareDBRepository = rezervareRepo;
            this._seatDBRepository = seatRepo;
            this._loggedClients = new ConcurrentDictionary<string, IRezervareObserver>();
            
        }
        
        
        public Client Connect(string username, string password,IRezervareObserver client)
        {
            lock (_lock)
            {
                Client user = _clientDBRepository.FindByUsername(username);
                Console.WriteLine("User:"+user.Id+" "+user.username+" "+user.password);
                //User user = _userRepo.FindByUsername(username);
                if (user != null && user.password.Equals(password))
                {
                    if (_loggedClients.ContainsKey(username))
                    {
                        throw new Exception("User already logged in");
                    }
                    else
                    {
                        _loggedClients[username] = client;
                        return user;
                    }
                }
                return null;
            }
        }

        public void Logout(string username)
        {
            lock (_lock)
            {
                bool removed = _loggedClients.TryRemove(username, out IRezervareObserver removedClient);
                if (removed)
                {
                    Console.WriteLine("Client " + username + " logged out");
                }
            }
        }

        /*public AllCompetitionsDTO GetAllCompetitionsWithNrParticipants()
        {
            IEnumerable<Competition> competitions = _competitionRepo.FindAll();
            SortedDictionary<Competition, int> ret = new SortedDictionary<Competition, int>(new CompetitionComparator());
            foreach (Competition competition in competitions)
            {
                int count = _registrationRepo.CountRegistrationsForCompetition(competition.Id);
                ret.Add(competition, count);
            }
            AllCompetitionsDTO allCompetitionsDto = new AllCompetitionsDTO(ret);
            
            return allCompetitionsDto;
        }

        
        public Dictionary<Participant, List<long>> GetAllParticipantsByCompetition(long id)
        {
            List<long> competitions = _registrationRepo.FindParticipantsByCompetition(id);
            Dictionary<Participant, List<long>> ret = new Dictionary<Participant, List<long>>();
            foreach (long idParticipant in competitions)
            {
                Participant participant = _participantRepo.findOne(idParticipant);
                ret.Add(participant, _registrationRepo.FindCompetitionsByParticipant(participant.Id));
            }
            return ret;
        }*/
        
        public List<Client> GetAllUsers()
        {
            return _clientDBRepository.FindAll();
        }
        public List<Cursa>GetAllCurse()
        {
            return _cursaDBRepository.FindAll();
        }

        public List<Seat> GetAllSeats()
        {
            return _seatDBRepository.FindAll();
        }

        public List<Rezervare>GetAllRezervari()
        {
            return _rezervareDBRepository.FindAll();
        }

        public void registerRezervare(Client user, Cursa cursa, int nrLocuri)
        {
            Rezervare rezervare = new Rezervare(user, cursa, nrLocuri);
            _rezervareDBRepository.Save(rezervare);
         
            NotifyAllClients();
            
        }

        public void registerSeat(Seat seat)
        {
            _seatDBRepository.Save(seat);
            
            NotifyAllClients();
        }

        /* public void AddParticipant(string name, DateTime birthDate, List<long> competitionsId)
        {
            Participant participant = new Participant(name, birthDate);
            _participantRepo.save(participant);
            participant = _participantRepo.FindByNameAndBirthDate(participant.Name, participant.BirthDate);

            foreach (long competition in competitionsId)
            {
                Registration registration = new Registration(participant, _competitionRepo.findOne(competition));
                _registrationRepo.save(registration);
            }

            NotifyAllClients();
        }*/
        
        
        
        private void NotifyAllClients()
        {
            Console.WriteLine($"Notifying clients {_loggedClients.Count}");
            foreach (var client in _loggedClients.Values)
            {
                Task.Run(() => client.registerParticipant());
            }
        }
        
        // [Serializable]
        // private class CompetitionComparator : IComparer<Competition>
        // {
        //     public int Compare(Competition c1, Competition c2)
        //     {
        //         int distanceComparison = c1.Id.CompareTo(c2.Id);
        //         if (distanceComparison != 0)
        //         {
        //             return distanceComparison;
        //         }
        //         else
        //         {
        //             return string.Compare(c1.Style, c2.Style, StringComparison.Ordinal);
        //         }
        //     }
        // }
    }
}