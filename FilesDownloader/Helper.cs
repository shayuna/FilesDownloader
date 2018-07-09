using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
namespace FilesDownloader
{
    public class Helper
    {
        public void writeToLog(string sLogPath, string sLogOutput)
        {
            File.WriteAllText(sLogPath, sLogOutput);
        }
        public bool compareFiles(string sPath1, string sPath2)
        {
            return File.ReadAllBytes(sPath1).SequenceEqual(File.ReadAllBytes(sPath2));
        }
        public bool isFileExistOnRemote(string sPath, string sSuffix)
        {
            bool bExists = true;
            string[] arLines = readAllLinesInFl(sPath);
            if (sSuffix == "pdf" && arLines[0].ToLower().IndexOf("html") > -1) bExists = false;
            return bExists;
            
        }
        public string getFileSuffix(string sPath)
        {
            string sSuffix = "";
            Regex re = new Regex(@"[.]([^.]+)$");
            Match mt = re.Match(sPath);
            if (mt.Success) sSuffix= mt.Groups[1].Value;
            return sSuffix;
        }
        public string getFileNmFromResourceUrl(string sResourceUrl)
        {
            Regex re = new Regex(@"[^\/]+$");
            Match mt = re.Match(sResourceUrl);
            if (mt != null) sResourceUrl = mt.ToString();
            return sResourceUrl;
        }
        public bool isValidUrl(string sUrl)
        {
            Regex re = new Regex(@"^\s*(http|www)");
            return re.IsMatch(sUrl);
        }
        public bool downloadResource(string sResourceUrl, string sLocalNm)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");//we must imitate a regular browser, otherwise government sites will return badgateway answer instead of the requested resource
            bool bRslt = true;
            try
            {
                wc.DownloadFile(new Uri(sResourceUrl), sLocalNm);
            }
            catch (Exception ex)
            {
                bRslt = false;
            }
            System.Threading.Thread.Sleep(2000);
            return bRslt;
        }
        public string[] readAllLinesInFl(string sFlPath)
        {
            List<string> lines = new List<string>();
            StreamReader file = new StreamReader(sFlPath);
            string line = "";
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();
            return lines.ToArray();
        }
    }
}
