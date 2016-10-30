using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwait
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
            label1.Text = "";
            label2.Text = "";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "txt";            
            openFile.Filter = "Text documents (*.txt)|*.txt";
            openFile.ShowDialog();
            
            if (openFile.FileNames.Length > 0)
            {
                foreach (string filename in openFile.FileNames)
                {
                    string wayToUserFile = filename;
                    try
                    {                        
                        label1.Text = await AddToLogAsync(wayToUserFile);
                        //view file in textbox
                        var pathToLogFile = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
                        string[] text = File.ReadAllLines(pathToLogFile);
                        foreach (string line in text)
                        {
                            textBox1.Text += line + Environment.NewLine;
                        }
                    }

                    catch (Exception ex)
                    {
                        label1.Text = ex.Message;
                    }
                }
            }
            
        }


       
          Task<string> AddToLogAsync(string path)
        {
            // Displaying on user UI
            label2.Text = "Status:";
            label1.Text = "File reading...";
            textBox1.Text = "";
            
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
                var internalName = versionInfo.InternalName;

            if (internalName == null)
            {

            }
            else

            if (internalName.Contains(".exe"))
                //if (internalName.Substring(internalName.Length - 4)==".exe")
            {
                throw new System.InvalidOperationException("Failed. Use only .txt files!!!");
            }
            
            
            string[] text = File.ReadAllLines(path);
            var pathToLogFile = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
           

            return Task.Factory.StartNew(() => 
            {
                Thread.Sleep(2000);
                
                foreach (string str in text)
                {                      
                    File.AppendAllText(pathToLogFile, str + Environment.NewLine);
                }
                return "Added to log!";
            });
            
        }
        
    }
}
