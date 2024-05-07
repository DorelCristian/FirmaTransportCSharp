using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormandiaModel.ro.mpp
{
    [Serializable]
    public class Entity<Long>
    {
        protected Long id;
        public Long getId()
        {
            return id;
        }
        public void setId(Long id)
        {
            this.id = id;
        }
        override
            public String ToString()
        {
          // return id.ToString();
            return "Entity{" +
                "id=" + id +
                '}';
        }
    }
}
