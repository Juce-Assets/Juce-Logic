using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.Logic.Types
{
    public static class SelectTypesPopup
    {
        public static void Show(Action<Type> selected)
        {
            List<Type> avaliableTypes = GetAllSerializableTypes();

            GenericMenu.MenuFunction2 Selected = delegate (object o)
            {
                if (o is Type)
                {
                    selected?.Invoke((Type)o);
                }
            };

            var menu = new GenericMenu();

            var namespaces = new List<string>();

            menu.AddItem(new GUIContent("Classes/System/Object"), false, Selected, typeof(object));

            foreach (var t in avaliableTypes)
            {
                var a = (string.IsNullOrEmpty(t.Namespace) ? "No Namespace/" : t.Namespace.Replace(".", "/") + "/") + t.Name;
                var b = string.IsNullOrEmpty(t.Namespace) ? string.Empty : " (" + t.Namespace + ")";
                var friendlyName = a + b;

                var category = "Classes/";

                if (t.IsValueType) category = "Structs/";
                if (t.IsInterface) category = "Interfaces/";
                if (t.IsEnum) category = "Enumerations/";

                menu.AddItem(new GUIContent(category + friendlyName), false, Selected, t);

                if (t.Namespace != null && !namespaces.Contains(t.Namespace))
                {
                    namespaces.Add(t.Namespace);
                }
            }

            menu.AddSeparator("/");
            foreach (var ns in namespaces)
            {
                var path = "Whole Namespaces/" + ns.Replace(".", "/") + "/Add " + ns;
                menu.AddItem(new GUIContent(path), false, Selected, ns);
            }

            menu.ShowAsContext();
        }

        private static List<Type> GetAllSerializableTypes()
        {
            List<Type> ret = new List<Type>();

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in a.GetTypes())
                {
                    if (!type.IsSerializable || type.IsGenericType || type.IsGenericTypeDefinition)
                    {
                        continue;
                    }

                    ret.Add(type);
                }
            }

            return ret.OrderBy(t => t.Namespace).ToList();
        }
    }
}
