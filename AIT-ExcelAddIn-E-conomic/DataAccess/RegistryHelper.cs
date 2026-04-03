using System;
using Microsoft.Win32;

namespace AIT_ExcelAddIn_E_conomic.DataAccess
{
    public static class RegistryHelper
    {
        private const string BasePath = @"Software\AspectIT\ExcelAddInEconomic";

        //public static void CreateKey(string name)
        //{
        //    string fullname = BasePath + @"\" + name;
        //    Registry.CurrentUser.CreateSubKey(fullname);
        //}

        public static void SetValue(string name, object value, RegistryValueKind kind = RegistryValueKind.String)
        {
            var key = Registry.CurrentUser.CreateSubKey(BasePath);
            key?.SetValue(name, value, kind);
        }

        public static T GetValue<T>(string name, T defaultValue = default)
        {
            var key = Registry.CurrentUser.OpenSubKey(BasePath);

            if (key == null)
                return defaultValue;

            object value = key.GetValue(name);

            if (value == null)
                return defaultValue;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static bool ValueExists(string name)
        {
            var key = Registry.CurrentUser.OpenSubKey(BasePath);
            return key?.GetValue(name) != null;
        }

        public static void DeleteValue(string name)
        {
            var key = Registry.CurrentUser.OpenSubKey(BasePath, writable: true);
            key?.DeleteValue(name, throwOnMissingValue: false);
        }

        public static void DeleteAll()
        {
            Registry.CurrentUser.DeleteSubKeyTree(BasePath, throwOnMissingSubKey: false);
        }
    }
}
