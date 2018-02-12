using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClipboardTools;

namespace ClipboardToolsSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Notification.ClipboardUpdate += Notification_ClipboardUpdate;
        }

        private void Notification_ClipboardUpdate(object sender, EventArgs e)
        {
            labelResult.Text = "";

            Log("New content in clipboard!");

            if (!VirtualFiles.ContainsVirtualFiles)
            {
                Log("Clipboard does not contain virtual files. Please handle this data with common Clipboard.GetXXXX methods.");
                return;
            }

            try
            {
                if (!checkBoxOutlook.Checked) //Default way
                {

                    Log("Option 1: Get file streams");
                    var streams = VirtualFiles.GetVirtualFilesAsStreams();

                    Log("Option 2: Get files saved to disk");
                    var files = VirtualFiles.GetVirtualFilesAsFiles();

                    foreach (var file in files)
                    {
                        Log("Saved file " + file.FullName);
                    }
                }
                else //Outlook special handling
                {
                    Log("Option 1: Get file streams");
                    var streams = VirtualFiles.GetOutlookVirtualFilesAsStreams();

                    Log("Option 2: Get files saved to disk");
                    var files = VirtualFiles.GetOutlookVirtualFilesAsFiles();

                    foreach (var file in files)
                    {
                        Log("Saved Outlook msg file " + file.FullName);
                    }
                }
            }
            catch(Exception ex)
            {
                Log("Got exception: " + ex.Message + Environment.NewLine + "Stacktrace: " + ex.StackTrace);
            }

        }

        private void Log(String s)
        {
            Console.WriteLine(s);
            labelResult.Text += s + Environment.NewLine;
        }

        private void linkLabelManualStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Notification_ClipboardUpdate(null, null);
        }
    }
}
