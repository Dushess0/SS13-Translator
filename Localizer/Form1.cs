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
        private const string listFile = "filelist";
        string downloadBase = "https://raw.githubusercontent.com/Dushess0/SS13-Translator/master/";
        public Form1()
        {
            InitializeComponent();
        }
        private void Download(string filename)
        {

            Download("", filename);
            
        }
        private void Download(string folder, string file)
        {
            var webclient = new WebClient();
            try
            {
                webclient.DownloadFile(downloadBase + folder + file, file);
            }
            catch (WebException)
            {

                this.label1.Text += "Ошибка скачивания: " + file;
            }
           
        }
        


        //patch
        private void button1_Click(object sender, EventArgs e)
        {
            Download(listFile);
            files = File.ReadAllLines(listFile);
            this.label1.Text = "Начата загрузка файлов\n";
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = files.Length;
            this.progressBar1.Value = 0;

            var byondDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BYOND\\cache";
            var cacheFolder = Directory.GetDirectories(byondDir).OrderByDescending(dir => Directory.GetLastWriteTime(dir)).First();

            foreach (string file in files)
            {
                Download("Translations/", file);

                if (File.Exists(cacheFolder+"\\" + file))
                    File.Delete(cacheFolder+"\\" + file);

                if (File.Exists(file))
                   File.Move(file, cacheFolder+"\\" + file);

                this.label1.Text += file + "\n";

                this.progressBar1.Value++;
            }
            this.label1.Text += "Закончено\n";

           
            
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var current = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            var list = current.GetFiles().Where(file=>file.Name==listFile).First();
            var translations = current.GetDirectories().Where(dir => dir.Name == "Translations").First().GetFiles().Select(file=>file.Name).ToArray();

            File.WriteAllLines(list.FullName, translations);
           
            


        }

 

       
    }
}
