using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DMP.Services {
    public class AppUtil {

        private static readonly string TempPath = ConfigurationManager.AppSettings["uploadFilePath"] ?? "/Upload/Files/";

        public static string GetTempPath(string fileName, out string absolutePath) {
            var arr = fileName.Split('\\', '/');
            fileName = arr[arr.Length - 1];
            var directory = string.Format("{0}{1}/", TempPath, Guid.NewGuid());        //make a temp directory for this upload
            var serverDirectory = HttpContext.Current.Server.MapPath(directory);             //get server absolute path
            var filePath = string.Format("{0}{1}", directory, fileName);                     //get relative filepath
            absolutePath = string.Format("{0}{1}", serverDirectory, fileName);                //get absolute filepath
            if (!Directory.Exists(serverDirectory))                                      //create directory if not exists
                Directory.CreateDirectory(serverDirectory);

            return filePath;
        }

        public static string GetRelativeFilePath(string absolutePath) {
            if (string.IsNullOrEmpty(absolutePath)) return null;
            var tempPath = absolutePath;
            var lastIndex = Path.GetDirectoryName(tempPath).LastIndexOf(Path.DirectorySeparatorChar);
            return tempPath.Substring(lastIndex);
        }

        public static string GetTempPath(string relativePath) {
            var path = string.Format("{0}{1}", TempPath, relativePath);
            var result = HttpContext.Current.Server.MapPath(path);
            return result;
        }

        public static void DeleteDirectory(string path) {
            Directory.Delete(path, true);
        }
    }
}
