using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.DataAccessObjects.DTOFactories
{
    public class DTOFactoryBase<In, Out>
          where In : class
          where Out : class
    {
        public Out CreateObject(In _in) {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<In, Out>());
            IMapper iMapper = config.CreateMapper();
            Out ouput = iMapper.Map<Out>(_in);
            return ouput;
        }

        public List<Out> CreateObject(List<In> _in)
        {
            List<Out> result = new List<Out>();
            foreach (var item in _in)
            {
                Out _out = this.CreateObject(item);
                result.Add(_out);
            }
            return result;           
        }


    }
}
