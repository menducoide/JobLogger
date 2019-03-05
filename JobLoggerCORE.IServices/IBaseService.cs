using System.Collections.Generic;
namespace JobLoggerCORE.IServices
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
