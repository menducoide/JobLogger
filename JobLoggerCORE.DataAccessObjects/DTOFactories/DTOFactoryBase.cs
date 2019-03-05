using AutoMapper;
using System.Collections.Generic;

namespace JobLoggerCORE.DataAccessObjects.DTOFactories
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

        public System.Collections.Generic.List<Out> CreateObject(List<In> _in)
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
