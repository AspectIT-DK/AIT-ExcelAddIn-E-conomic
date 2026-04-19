using System;
using System.Diagnostics;

namespace AIT_ExcelAddIn_E_conomic.Logging
{
    public static class Logger
    {
        private const string DefaultCategory = "E-Conomic Add-in";

        public static void WriteLine(string message)
        {
            //string TimePrefix = $"[{DateTime.Now.ToString("dd-MM-yyyy mm:HH:ss")}]";
            //Debug.WriteLine(message, TimePrefix + " " + DefaultCategory);
            Debug.WriteLine(message, DefaultCategory);
        }

        public static void WriteLine(string message, string category)
        {
            Debug.WriteLine(message, category);
        }

    }
}
