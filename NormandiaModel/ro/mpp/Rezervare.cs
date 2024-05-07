using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormandiaModel.ro.mpp
{
    [Serializable]
    public class Rezervare:Entity<long>
    {
        // public Client Client { get; set; }

        public int Id { get; set; }
        public Client User { get; set; }
        public Cursa Cursa { get; set; }
      
        public int locuri { get; set; }
       

        public Rezervare(Client user,Cursa cursa, int locuri)
        {
            // this.Client = client;
            this.User = user;
            this.Cursa = cursa;
            this.locuri = locuri;
        }
    }
}
