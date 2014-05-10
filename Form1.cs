using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutdownAt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            TimeSpan time = DateTime.Now.TimeOfDay;
            DateTime date = DateTime.Today.Date;
            txtTime.Text = time.Hours.ToString().PadLeft(2, '0') + ":" + time.Minutes.ToString().PadLeft(2, '0');
            datePicker.Text = date.ToString();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            String time = txtTime.Text;
            String date = datePicker.Text;
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "schtasks";
            processInfo.Arguments = "/create /tn \"Shutdown\" /tr \"shutdown -s\" /sc once /st " + time + " /sd " + date + " /F";
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas"; //Run as administrator.
            Process process = Process.Start(processInfo);
            process.WaitForExit();
            if (process.ExitCode == 0)
                MessageBox.Show("The computer will shut down at\n" + date + " " + time);
            else
                MessageBox.Show("Error. " + process.ExitCode);
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "schtasks";
            processInfo.Arguments = "/delete /tn \"Shutdown\" /f";
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas"; //Run as administrator.
            Process process = Process.Start(processInfo);
            process.WaitForExit();
            if (process.ExitCode == 0)
                MessageBox.Show("The scheduled shutdown has been aborted");
            else
                MessageBox.Show("There was no scheduled shutdown to abort");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
