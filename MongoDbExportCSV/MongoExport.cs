using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MongoDbExportCSV
{
    public static class MongoExport
    {
        public static void ExportProcess(string databaseName, string collectionName, string fields, string mongoexportExePath= "C:\\Program Files\\MongoDB\\Server\\4.2\\bin\\mongoexport.exe", string path = ".\\CSV_Export\\")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(mongoexportExePath, $"--collection={collectionName} --db={databaseName} --type=csv  --fields={fields} --out={path}{databaseName}_{collectionName}_{DateTime.Now.Ticks}.csv");
            startInfo.UseShellExecute = false;

            Process exportProcess = new Process();
            exportProcess.StartInfo = startInfo;

            exportProcess.Start();
            exportProcess.WaitForExit();

        }

    }
}
