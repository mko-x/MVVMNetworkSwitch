using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.Util
{
    public class StoragePath
    {
        /// <summary>
        /// FileName to store fullName in
        /// </summary>
        public static string FileName 
        { 
            get{return "profiles.mko";} 
        }

        /// <summary>
        /// File path including file name to store fullName in
        /// </summary>
        public static string FilePathWithFileName
        {
            get
            {
                string pathToAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
                int lastIndexOfSeperator = pathToAssembly.LastIndexOf(CurrentSeperator);
                string directoryPath = pathToAssembly.Substring(0, lastIndexOfSeperator + 1);
                return directoryPath + FileName;
            }
        }

        public static string CurrentSeperator
        {
            get { return "\\"; }
        }
    }
}
