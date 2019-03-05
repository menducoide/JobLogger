using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.IServices
{
    public interface IBaseService<T>
        where T : class     
    {
       
        T Create(T dto);
        void Remove(T dto);
        void Remove(object id);
        void Edit(T dto);
        T GetBy(object id);
        List<T> List();

    }
}
