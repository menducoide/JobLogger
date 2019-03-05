using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

using JobLogger;
using System.Reflection;
using System.Security.Policy;
using System.IO;
using System.Web.Mvc;
using Unity.AspNet.Mvc;


namespace JobLogger.DependencyInjection
{
    public static class UnityResolver
    {
        public static UnityContainer container;
        static UnityResolver()
        {
            container = new UnityContainer();
            var dependencias = GetAssembliesDependencies("JobLogger.");
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

        private static Dictionary<Type, Type> GetAssembliesDependencies(string proyectName)
        {
            var result = new Dictionary<Type, Type>();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            var loadedAssemblies = currentDomain.GetAssemblies().ToList().Where(s => s.FullName.Contains(proyectName)).ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(s => s.Split('\\').Last().Contains(proyectName)).ToList();
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
