using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace Localizer
{
    public partial class Form1 : Form
    {
        //download list of files from repo
        //download files from repo
        //replace files in directory

        private string[] files;
        private const string listFile = "listfile";
        string remoteUri = "https://raw.githubusercontent.com/Dushess0/SS13-Translator/master/";
        public Form1()
        {
            InitializeComponent();
        }
        private void Download(string filename)
        {
            var webclient = new WebClient();
            webclient.DownloadFile(remoteUri+filename, filename);
        }

        //patch
        private void button1_Click(object sender, EventArgs e)
        {
            files = File.ReadAllLines(listFile);
            this.label1.Text = "Начата загрузка файлов\n";
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = files.Length;
            foreach (string file in files)
            {
                Download(file);
                this.label1.Text += file + "\n";
                this.progressBar1.Value++;
            }
            this.label1.Text += "Закончено";

           
        }

       
    }
}
