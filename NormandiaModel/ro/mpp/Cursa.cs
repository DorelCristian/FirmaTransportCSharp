using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormandiaModel.ro.mpp
{
    [Serializable]
    public class Cursa:Entity<long>
    {
        public int Id { get; set; }
        public string destinatie { get; set; }
        public string data {  get; set; }
      //  public DateTime data { get; set; }
      //  public TimeSpan time { get; set; }
       // public long locuri { get; set; }

        public Cursa(string destinatie,string data)
        {
            this.destinatie = destinatie;
            this.data = data;
            
           // this.locuri = locuri;
        }

        public Cursa()
        {
            
        }
        public String ToString()
        {
            return "Cursa{" +
                "id=" + Id +
                ", destinatie='" + destinatie + '\'' +
                ", data='" + data + '\'' +
                '}';
        }
    }
}
