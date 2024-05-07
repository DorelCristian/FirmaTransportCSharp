using System;
using System.Collections.Generic;

namespace NormandiaNetworking.ro.mpp.dto
{
    [Serializable]
    public class RegisterRezervareDTO
    {
        public string _name { get; set; }
        public DateTime _birthDate { get; set; }
        public List<long> _competitionsId { get; set; }
        
        public RegisterRezervareDTO(string name, DateTime birthDate, List<long> competitionsId)
        {
            _name = name;
            _birthDate = birthDate;
            _competitionsId = competitionsId;
        }
    }
}