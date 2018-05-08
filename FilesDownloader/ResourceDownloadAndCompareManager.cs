using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace FilesDownloader
{
    public class ResourceDownloadAndCompareManager
    {
        private string sListOfResourcesFlPath, sOutputFlPath;
        Helper oHelper = new Helper();
        public ResourceDownloadAndCompareManager(string sListOfResourcesFlPath1,string sOutputFlPath1)
        {
            sListOfResourcesFlPath = sListOfResourcesFlPath1;
            sOutputFlPath = sOutputFlPath1;
        }
        public void go()
        {
            string[] arResourcesList = oHelper.readAllLinesInFl(sListOfResourcesFlPath);

            string sDemaFlNm = "blabla", sOutput = "";
            
            foreach (string sResourceUrl in arResourcesList)
            {
                if (oHelper.isValidUrl(sResourceUrl))
                {
                    bool bRslt = oHelper.downloadResource(sResourceUrl, sDemaFlNm);
                    if (!bRslt)
                    {
                        sOutput += sResourceUrl + " - not found on server" + "\r\n";
                    }
                    else
                    {
                        string sFileNm = oHelper.getFileNmFromResourceUrl(sResourceUrl);
                        string sPath1 = AppDomain.CurrentDomain.BaseDirectory + sFileNm,
                            sPath2 = AppDomain.CurrentDomain.BaseDirectory + sDemaFlNm;
                        bool bOverrideLocalFile = false;

                        if (!File.Exists(sPath1))
                        {
                            sOutput += sFileNm + " - not found on local computer" + "\r\n";
                            bOverrideLocalFile = true;
                        }
                        else
                        {
                            bool bEqual = oHelper.compareFiles(sPath1, sPath2);
                            if (!bEqual)
                            {
                                sOutput += sFileNm + " - updated" + "\r\n";
                                bOverrideLocalFile = true;
                            }
                        }
                        if (bOverrideLocalFile)
                        {
                            if (File.Exists(sPath1)) File.Delete(sPath1);
                            File.Move(sPath2, sPath1);
                        }
                    }
                }
            }
            oHelper.writeToLog(AppDomain.CurrentDomain.BaseDirectory + "updatedFilesLog.txt", sOutput);
            MessageBox.Show("סיימנו, אפשר ללכת הביתה");
        }
    }
}
