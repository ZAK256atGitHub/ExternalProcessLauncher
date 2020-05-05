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

namespace External_Process_Launcher
{
    public partial class FormExternalProcessLauncher : Form
    {


        public FormExternalProcessLauncher()
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("tracert www.google.de");
            sb.AppendLine("ping www.google.de");
            sb.AppendLine("ipconfig");
            sb.AppendLine("calc.exe");
            textBoxExternalProcesses.Text = sb.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FormExternalProcessProgress formExternalProcessLauncher = new FormExternalProcessProgress();
            formExternalProcessLauncher.commandLinesArray = textBoxExternalProcesses.Lines;
            formExternalProcessLauncher.ShowDialog();
        }

    }
}
