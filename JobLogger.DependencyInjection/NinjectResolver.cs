using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger.DependencyInjection
{
    public class NinjectResolver : System.Web.Mvc.IDependencyResolver
    {
        public readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {


            foreach (var item in GetAssembliesDependencies("JobLogger"))
            {
                _kernel.Bind(item.Key).To(item.Value);
            }

            // _kernel.Bind<To, From>(); // Registering Types    
        }

        private static Dictionary<Type, Type> GetAssembliesDependencies(string proyectName)
        {
            var result = new Dictionary<Type, Type>();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            var loadedAssemblies = currentDomain.GetAssemblies().ToList().Where(s => s.FullName.Contains(proyectName)).ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(s => s.Contains(proyectName));
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

            //  var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList().Where(s => s.FullName.Contains(proyectName));
            List<Type> ints = new List<Type>();
            List<Type> clases = new List<Type>();
            foreach (var asm in loadedAssemblies)
            {
                clases.AddRange(asm.GetTypes().Where(s => s.IsClass));
                ints.AddRange(asm.GetTypes().Where(i => i.IsInterface));
            }

            foreach (var inter in ints)
            {
                var cls = clases.Where(s => "I" + s.Name == inter.Name).FirstOrDefault();
                if (cls != null) result.Add(inter, cls);
            }
            return result;

        }
    }
}
