using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5_Ozierski_PluginApplication
{ 
    public interface IPlugin
    {
        string Author { get; }

        LongNumber DoOp(LongNumber number1, LongNumber number2);
    }
}
