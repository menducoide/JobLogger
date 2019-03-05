using System;
using System.Collections.Generic;
using System.Linq;  
using System.Reflection;
 

namespace JobLoggerCORE.DependencyInjection
{
  public class DependencyResolver
    {
        public static Dictionary<Type, Type> GetAssembliesDependencies(string proyectName)
        {
            var result = new Dictionary<Type, Type>();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            var loadedAssemblies = currentDomain.GetAssemblies().ToList().Where(la=>la.FullName.Contains(proyectName)).ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            //   var referencedPaths = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(s => s.Split('\\').Last().Contains(proyectName)).ToList();
            var referencedPaths = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").ToList();
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
