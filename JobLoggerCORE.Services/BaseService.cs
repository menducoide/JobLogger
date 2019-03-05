using JobLoggerCORE.DataAccessObjects.DTOFactories;
using JobLoggerCORE.IRepositories;
using JobLoggerCORE.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLoggerCORE.Services
{
    public class BaseService<T, K> : IBaseService<T>
         where T : class
        where K : class
    {
        protected IBaseRepository<K> repository { get; set; }
        protected DTOFactoryBase<T, K> factoryEntity { get; set; }
        protected DTOFactoryBase<K, T> factoryDTO { get; set; }

        public BaseService()
        {
            factoryEntity = new DTOFactoryBase<T, K>();
            factoryDTO = new DTOFactoryBase<K, T>();
        }

        public virtual T Create(T dto)
        {
            K entity = repository.Insert(factoryEntity.CreateObject(dto));
            return factoryDTO.CreateObject(entity);
        }

        public virtual void Edit(T dto)
        {
            repository.Update(factoryEntity.CreateObject(dto));
        }

        public virtual T GetBy(object id)
        {
            K result = repository.Find(id);
            return factoryDTO.CreateObject(result);
        }

        public virtual List<T> List()
        {
            List<K> result = repository.FindAll();
            return factoryDTO.CreateObject(result);
        }

        public virtual void Remove(T dto)
        {
            repository.Delete(factoryEntity.CreateObject(dto));
        }

        public virtual void Remove(object id)
        {
            repository.Delete(id);
        }
    }
}
