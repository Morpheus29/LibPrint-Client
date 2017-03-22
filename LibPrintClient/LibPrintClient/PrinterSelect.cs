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
using System.IO;

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

            if(Variables.parsed.Length == 4)
            {
                radioButton2.Hide();
                label2.Hide();
            }
        }

        void SelectOK(Object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                Variables.printername = Variables.parsed[3].Split(',')[0].Trim();
            }
            else if(radioButton2.Checked == true)
            {
                Variables.printername = Variables.parsed[5].Split(',')[0].Trim();
            }

            WebClient webClient = new WebClient();
            webClient.QueryString.Add("request", "printPDF");
            webClient.QueryString.Add("username", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            webClient.QueryString.Add("computer", Environment.MachineName);
            webClient.QueryString.Add("printerName", Variables.printername);
            webClient.QueryString.Add("file", Directory.GetFiles(@"c:/ProgramData/LibPrint/cache/")[0]);
            string result = webClient.DownloadString(Variables.libprinturl);

            Variables.parsedconfirm = result.Split(new[] { ':', '\n' });

            if(Variables.parsedconfirm[1].Trim() == "OK")
            {
                ConfirmationWindow frm = new ConfirmationWindow();
                frm.Show();
            }
            else if(Variables.parsedconfirm[1].Trim() == "Error")
            {
                PrintError frm = new PrintError();
                frm.Show();
            }
            else
            {
                string error = "Response: Error\nError: Invalid response from server";
                Variables.parsed = error.Split(new[] { ':', '\n' });

                PrintError frm = new PrintError();
                frm.Show();
            }
        }

        void SelectCancel(Object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PrinterSelect_Load(object sender, EventArgs e)
        {
            
        }
    }
}
