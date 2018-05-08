using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FilesDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            inputFlPath.Text = AppDomain.CurrentDomain.BaseDirectory + "filesList.txt";
            outputFlPath.Text = AppDomain.CurrentDomain.BaseDirectory + "updatedFilesLog.txt";

  //          System.Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResourceDownloadAndCompareManager oManager = new ResourceDownloadAndCompareManager(inputFlPath.Text, outputFlPath.Text);
            oManager.go();
        }
    }
}
