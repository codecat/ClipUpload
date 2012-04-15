using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ClipUpload3
{
    public class Addon
    {
        public Settings Settings;
        public string Directory;

        public Assembly Assembly;
        public Type Type;
        public object ApplyObject;

        public Addon(string Directory)
        {
            if (File.Exists(Directory + "/info.txt"))
                Settings = new Settings(Directory + "/info.txt");

            this.Directory = Directory;
        }

        public void LoadAssembly()
        {
            string Filename = Directory + "\\" + Settings.GetString("Entry");
            if (File.Exists(Filename))
            {
                Assembly = Assembly.LoadFrom(Filename);
                Type = Assembly.GetType(Settings.GetString("Namespace") + "." + Settings.GetString("Class"));
                ConstructorInfo constructor = this.Type.GetConstructor(Type.EmptyTypes);
                ApplyObject = constructor.Invoke(new object[] { });
            }
            else
                MessageBox.Show("Assembly file \"" + Settings.GetString("Entry") + "\" not found!");
        }

        public object CallHook(string method, params object[] parameters)
        {
            if (Assembly != null && Type != null)
            {
                MethodInfo m = Type.GetMethod(method);
                if (m != null)
                    return m.Invoke(ApplyObject, parameters);
            }
            return null;
        }

        public T CallHook2<T>(string method, T def, params object[] parameters)
        {
            if (Assembly != null && Type != null)
            {
                MethodInfo m = Type.GetMethod(method);
                if (m != null)
                    return (T)m.Invoke(ApplyObject, parameters);
            }
            return def;
        }
    }
}
