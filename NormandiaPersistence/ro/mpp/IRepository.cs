using NormandiaModel.ro.mpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormandiaPersistence.ro.mpp
{
    public interface IRepository<Id,E> 
    {
        List<E> FindAll();
        E FindOne(Id id);
        E Save(E entity);
        E Delete(Id id);
        E Update(Id id);

    }
}