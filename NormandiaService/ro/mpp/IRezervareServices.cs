using System;
using System.Collections.Generic;
using NormandiaModel.ro.mpp;

namespace NormandiaService.ro.mpp
{
    public interface IRezervareServices
    {
        Client Connect(string username, string password,IRezervareObserver client);
        
        void Logout(string username);
        
        List<Client> GetAllUsers();
        
        List<Cursa>GetAllCurse();
        
        List<Seat>GetAllSeats();
        
        List<Rezervare>GetAllRezervari();
        
        void registerRezervare(Client user, Cursa cursa, int nrLocuri);

        void registerSeat(Seat seat);

        //AllCompetitionsDTO GetAllCompetitionsWithNrParticipants();

        // Dictionary<Participant, List<long>> GetAllParticipantsByCompetition(long id);

        // void AddParticipant(string name, DateTime birthDate, List<long> competitionsId);
    }
}