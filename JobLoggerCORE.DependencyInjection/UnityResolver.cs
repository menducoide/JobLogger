using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;


using System.Reflection;
using System.Security.Policy;
using System.IO;
 

namespace JobLoggerCORE.DependencyInjection
{
    public static class UnityResolver
    {
        public static UnityContainer container;
        static UnityResolver()
        {
            container = new UnityContainer();
            var dependencias = DependencyResolver.GetAssembliesDependencies("JobLoggerCORE.");
            try
            {
                foreach (var item in dependencias)
                {
                    container.RegisterType(item.Key, item.Value);
                }

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public static T Resolve<T>()
            where T : class
        {
            return container.Resolve<T>();
        }

        public static T Resolve<T>(out T _out)
           where T : class
        {
            try
            {
                _out = container.Resolve<T>();

                return _out;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

    
    }
}
