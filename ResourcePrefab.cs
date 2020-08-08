using System;

namespace Modules.UIManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourcePrefab : Attribute
    {
        public string path;

        public ResourcePrefab(string path)
        {
            this.path = path;
        }
    }
}