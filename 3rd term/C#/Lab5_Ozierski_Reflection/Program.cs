using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Lab5_Ozierski_Reflection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Assembly a = Assembly.GetAssembly(typeof(Lab2_Ozierski_Train.Train));
            Console.WriteLine(Show(a));
            Console.ReadLine();
        }

        public static String Show(Assembly asm)
        {
            string res = "";
            foreach (Module m in asm.GetModules())
            {
                res += String.Format("{0} [{1}]\n", m.Name, m.ModuleVersionId);
                foreach (Type t in m.GetTypes())
                {
                    string mods = "";
                    if (t.IsPublic) mods += "public ";
                    else if (t.IsNotPublic) mods += "private ";
                    else mods += "internal ";
                    if (t.IsSealed) mods += "sealed ";
                    if (t.IsAbstract && !t.IsInterface) mods += "abstract ";
                    if (t.IsClass) mods += "class ";
                    if (t.IsEnum) mods += "enum ";
                    if (t.IsInterface) mods += "interface ";
                    res += String.Format("\n{0}{1}\n", mods, t.Name);

                    foreach (MethodInfo mi in t.GetMethods())
                        if (mi.DeclaringType == t)
                        {
                            string n = mi.Name;
                            string mmods = "";
                            if (mi.IsPublic) mmods += "public ";
                            if (mi.IsPrivate) mmods += "private ";
                            if (mi.IsConstructor) mmods += "constructor ";
                            if (mi.IsGenericMethod) n += "<>";
                            if (mi.IsStatic) mmods += "static ";
                            if (mi.IsVirtual) mmods += "virtual ";
                            List<string> prms = new List<string>();
                            foreach (ParameterInfo pi in mi.GetParameters())
                                prms.Add(pi.ParameterType.Name);
                            res += String.Format(" - {0}{1} {2}({3})\n", mmods, mi.ReturnType.Name, n, string.Join(", ", prms));
                        }
                }
            }
            return res;
        }
    }
}

