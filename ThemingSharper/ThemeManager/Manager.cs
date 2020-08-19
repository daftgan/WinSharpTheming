using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AdvancedThemeManager.ThemeManager
{
    public class Manager
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static readonly Manager Instance = new Manager();

        /// <summary>
        /// Showing all control
        /// </summary>
        public void ShowAllControl()
        {
            List<Type> controlTypes = GetAllControlTypes();
            DlgControlReferencer dlgReferencer = new DlgControlReferencer(controlTypes);
            dlgReferencer.StartPosition = FormStartPosition.CenterScreen;
            dlgReferencer.Show();
        }

        /// <summary>
        /// Getting all control types
        /// </summary>
        /// <returns>list of all types</returns>
        private static List<Type> GetAllControlTypes()
        {
            List<Type> types = new List<Type>();
            List<Assembly> assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();

            Type controlType = typeof(Control);

            foreach (Assembly assembly in assemblys)
            {
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!controlType.IsAssignableFrom(type))
                        {
                            continue;
                        }
                        types.Add(type);
                    }
                }
                catch (Exception exp)
                {
                    Console.Error.WriteLine(exp.Message);
                }
            }

            return types;
        }
    }
}
