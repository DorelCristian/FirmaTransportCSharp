using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormandiaModel.ro.mpp
{
    [Serializable]
    public class Seat:Entity<long>
    {
        public int Id { get; set; }
        public Rezervare rezervare { get; set; }

        public long seatNumber { get; set; }

        public Seat(Rezervare rezervare,long seatNumber)
        {
            this.rezervare = rezervare;
            this.seatNumber = seatNumber;
        }
        public String ToString()
        {
            return "Seat{" +
                   "id=" + Id +
                   ", rezervare=" + rezervare+
                   ", seatNumber=" + seatNumber +
                   '}';
        }
    }
}
