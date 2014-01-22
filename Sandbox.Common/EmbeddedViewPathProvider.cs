using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Hosting;

namespace Sandbox.Common
{
    public class EmbeddedViewPathProvider : VirtualPathProvider
    {
        private readonly IDictionary<string, Assembly> _virtualPathAssemblyMap;

        public EmbeddedViewPathProvider(IDictionary<string, Assembly> virtualPathAssemblyMap)
        {
            _virtualPathAssemblyMap = virtualPathAssemblyMap;
        }

        private bool ResourceFileExists(string virtualPath)
        {
            var embeddedVirtualFile = GetEmbeddedVirtualFile(virtualPath);

            var exists = embeddedVirtualFile != null && embeddedVirtualFile.Exists();

            return exists;
        }

        private EmbeddedVirtualFile GetEmbeddedVirtualFile(string virtualPath)
        {
            if (!virtualPath.StartsWith("~/") && virtualPath.StartsWith("/"))
                virtualPath = "~" + virtualPath;

            var key = _virtualPathAssemblyMap.Keys.SingleOrDefault(virtualPath.StartsWith);
            if (key == null)
            {
                return null;
            }

            var assembly = _virtualPathAssemblyMap[key];

            var virtualFile = new EmbeddedVirtualFile(virtualPath, key, assembly);

            return virtualFile;
        }

        public override bool FileExists(string virtualPath)
        {
            return base.FileExists(virtualPath) || ResourceFileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (!base.FileExists(virtualPath))
            {
                return GetEmbeddedVirtualFile(virtualPath);
            }
            else
            {
                return base.GetFile(virtualPath);
            }

        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            foreach (var key in _virtualPathAssemblyMap.Keys)
            {
                if(virtualPath.StartsWith(virtualPath)){
                    return null;
                }
            }
            
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);            
        }

    }
}
