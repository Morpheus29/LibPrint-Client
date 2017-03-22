using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace LibPrintClient
{
    public partial class PrinterSelect : Form
    {
        public PrinterSelect()
        {
            InitializeComponent();

            radioButton1.Text = Variables.parsed[3].Split(',')[0].Trim();
            radioButton2.Text = Variables.parsed[5].Split(',')[0].Trim();
            label1.Text = Variables.parsed[3].Split(',')[1].Trim();
            label2.Text = Variables.parsed[5].Split(',')[1].Trim();

            button1.Click += new EventHandler(this.SelectOK);
            button2.Click += new EventHandler(this.SelectCancel);
        }

        void SelectOK(Object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.QueryString.Add("request", "printPDF");
            webClient.QueryString.Add("username", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            webClient.QueryString.Add("computer", Environment.MachineName);
            webClient.QueryString.Add("printerName", "color"); //Remember to find out how to choose printer name
            webClient.QueryString.Add("file", "<FileName>"); //Remember to ask about where <FileName> comes from
            string result = webClient.DownloadString(Variables.libprinturl);
            //ConfirmationWindow f = new ConfirmationWindow();
            //f.Show();
        }

        void SelectCancel(Object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void PrinterSelect_Load(object sender, EventArgs e)
        {
            
        }
    }
}
