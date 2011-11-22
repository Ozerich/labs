using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5_Ozierski_PluginApplication
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PluginNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public PluginNameAttribute(string name)
        {
            Name = name;
        }
    }
}
