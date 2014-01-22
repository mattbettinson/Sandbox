using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Sandbox.Common
{
    public class EmbeddedVirtualFile : VirtualFile
    {
        private readonly string _virtualPathPrefix;
        private readonly Assembly _assembly;

        public EmbeddedVirtualFile(string virtualPath, string virtualPathPrefix, Assembly assembly)
            : base(virtualPath)
        {
            _virtualPathPrefix = virtualPathPrefix;
            _assembly = assembly;
        }

        internal string GetResourceName()
        {

            var resourceVirtualPath = this.VirtualPath.Substring(TrimVirtualPath(_virtualPathPrefix).Length);

            resourceVirtualPath = TrimVirtualPath(resourceVirtualPath);


            var resourcename = resourceVirtualPath
                .Replace("/", ".");

            return resourcename;

        }

        private static string TrimVirtualPath(string resourceVirtualPath)
        {
            if (resourceVirtualPath.StartsWith("~"))
                resourceVirtualPath = resourceVirtualPath.Substring(1);

            if (resourceVirtualPath.StartsWith("/"))
                resourceVirtualPath = resourceVirtualPath.Substring(1);

            return resourceVirtualPath;
        }


        public override Stream Open()
        {
            var resourcename = GetResourceName();

            var actualResourceName = _assembly.GetManifestResourceNames()
                .SingleOrDefault(x => x.EndsWith(resourcename));

            return _assembly.GetManifestResourceStream(actualResourceName);
        }

        public bool Exists()
        {
            var resourceName = GetResourceName();

            var result = _assembly.GetManifestResourceNames()
                .Any(x => x.EndsWith(resourceName));

            return result;
        }
    }
    /*
     
    public class EmbeddedVirtualDirectory : VirtualDirectory
    {
        private readonly Assembly _assembly;
        private VirtualDirectory FileDir { get; set; }

        public EmbeddedVirtualDirectory(string virtualPath, VirtualDirectory filedir, Assembly assembly)
            : base(virtualPath)
        {
            _assembly = assembly;
            FileDir = filedir;
        }

        public override System.Collections.IEnumerable Children
        {
            get { return FileDir.Children; }
        }

        public override System.Collections.IEnumerable Directories
        {
            get { return FileDir.Directories; }
        }

        public override System.Collections.IEnumerable Files
        {
            get
            {
                var directoryResourceName = EmbeddedVirtualFile.GetResourceName();
                if (!_assembly.GetManifestResourceNames().Any(x => x.StartsWith(directoryResourceName)))
                {
                    return FileDir.Files;
                }

                var fl = new List<VirtualFile>(FileDir.Files.OfType<VirtualFile>());

                var resourcenames = _assembly.GetManifestResourceNames().Where(x => x.StartsWith(directoryResourceName));

                var embeddedVirtualFiles = resourcenames.Select(x => new EmbeddedVirtualFile(BuildVirtualPath(x), _assembly));

                fl.AddRange(embeddedVirtualFiles);

                return fl;
            }
        }

        private string BuildVirtualPath(string resourceName)
        {
            return "~/" + resourceName.Substring(0, resourceName.LastIndexOf(".", System.StringComparison.Ordinal))
                       .Replace(".", "/")
                   + resourceName.Substring(resourceName.LastIndexOf(".", System.StringComparison.Ordinal));
        }
    }*/
}
