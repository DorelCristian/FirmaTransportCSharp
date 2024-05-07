using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormandiaModel.ro.mpp
{
    [Serializable]
    public class User:Entity<long>
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public User(string username,string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
