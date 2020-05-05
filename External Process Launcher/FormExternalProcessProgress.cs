using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// Run DOS Commands & Show Output in TextBox using Visual Basic 2010 
// https://www.youtube.com/watch?v=APyteDZMpYw
//
// Deutsch/Englisch:
// .NET Framework 4.5.2
// Process.OutputDataReceived Ereignis
// https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.process.outputdatareceived?view=netframework-4.5.2
// https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.outputdatareceived?view=netframework-4.5.2
// BackgroundWorker Klasse
// https://docs.microsoft.com/de-de/dotnet/api/system.componentmodel.backgroundworker?view=netframework-4.5.2
// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?view=netframework-4.5.2
// Process.WaitForExit Methode - WaitForExit(Int32) 
// https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.process.waitforexit?view=netframework-4.5.2#System_Diagnostics_Process_WaitForExit_System_Int32_
// https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexit?view=netframework-4.5.2#System_Diagnostics_Process_WaitForExit_System_Int32_
// Stopwatch.​Stop Method
// https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.stopwatch.stop?view=netframework-4.5.2
// https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.stop?view=netframework-4.5.2


namespace External_Process_Launcher
{
    public partial class FormExternalProcessProgress : Form
    {
        public String[] commandLinesArray { get; set; }
        delegate void InvokeWithParams(string text,Color col, FontStyle fontStyle);
        Stopwatch stopWatch = new Stopwatch();
        public FormExternalProcessProgress()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }
        private void FormExternalProcessProgress_Load(object sender, EventArgs e)
        {
            // Start the asynchronous operation.
            startAsync();
        }

        private bool startExternalProcess(string commandLine, BackgroundWorker backgroundWorker, DoWorkEventArgs e)
        {
            ProcessStartInfo processStartInfo;
            Process process;

            if (richTextBoxProcessOutput.InvokeRequired)
            {
                this.Invoke(new InvokeWithParams(Sync_Output), commandLine, Color.Black, FontStyle.Bold);
            }
            int pos = commandLine.IndexOf(" ");
            if (pos > 0)
            {
                processStartInfo = new ProcessStartInfo(
                    commandLine.Substring(0, pos),
                    commandLine.Substring(pos + 1)
               );
            }
            else
            {
                processStartInfo = new ProcessStartInfo(commandLine);
            }
            Encoding systemencoding = Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.StandardErrorEncoding = systemencoding;
            processStartInfo.StandardOutputEncoding = systemencoding;

            process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.ErrorDataReceived += new DataReceivedEventHandler(Async_Data_Received);
            process.OutputDataReceived += new DataReceivedEventHandler(Async_Data_Received);
            try
            {
                if (!process.Start())
                {
                    process.Close();
                    return false;
                }
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Win32Exception)
            {
                process.Close();
                return false;
            }           
            while (!process.WaitForExit(5))
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    try
                    {                        
                        process.Kill();
                    }
                    catch
                    { }
                    process.Close();                                       
                    return false;
                }
            }
            // Wait for asynchronous events for redirected standard output
            process.WaitForExit();          
            if (richTextBoxProcessOutput.InvokeRequired)
            {
                this.Invoke(new InvokeWithParams(Sync_Output), String.Format("\nExitCode: {0}\n\n", process.ExitCode), Color.Black, FontStyle.Bold);
            }
            // Free resources associated with process.
            process.Close();
            return true; 
        }
        private void Async_Data_Received(Object sender, DataReceivedEventArgs e)
        {
            if (richTextBoxProcessOutput.InvokeRequired)
            { 
                this.Invoke(new InvokeWithParams(Sync_Output), e.Data, Color.Black, FontStyle.Regular);
            }
        }
        private void Sync_Output(String text,Color col, FontStyle fontStyle)
        {        
            appendTextToProcessOutput(text + Environment.NewLine,col,fontStyle);
        }
        private void buttonAbort_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            this.backgroundWorker1.CancelAsync();

            // Disable the Cancel button.
            buttonAbort.Enabled = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Set up the BackgroundWorker object by attaching event handlers. 
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }
        private void startAsync()
        {
            // Reset the text in the result label.
            richTextBoxProcessOutput.Text = String.Empty;

            // Enable the Abort button while the asynchronous operation runs.
            this.buttonAbort.Enabled = true;

            // Disable the Close button
            this.buttonClose.Enabled = false;

            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync(commandLinesArray);
        }

        // This event handler is where the actual, potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            stopWatch.Start();

            // Get the BackgroundWorker that raised this event.
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;

            // Assign the result of the computation to the Result property of the DoWorkEventArgs
            // object. This is will be available to the RunWorkerCompleted eventhandler.            
            string[] commandlinesArr = (string[])e.Argument;
            foreach(string commadLine in commandlinesArr)
            { 
                if (!backgroundWorker.CancellationPending && commadLine.Length > 0) { e.Result = startExternalProcess(commadLine, backgroundWorker, e); }
            }            
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called. 
                appendTextToProcessOutput("\nCanceled", Color.Red, FontStyle.Bold);
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                // Format and display the TimeSpan value.
                // Success (203 ms @ 04.05.2020 09:03:20)
                string elapsedTime = String.Format("Success ({0} ms @ {1})",
                    Math.Round(ts.TotalMilliseconds),
                    DateTime.Now.ToString());
                appendTextToProcessOutput(elapsedTime,Color.Blue,FontStyle.Bold);               
            }

            // Disable the Abort button.
            buttonAbort.Enabled = false;

            // Enable the Close button
            buttonClose.Enabled = true;

            // Report progress 
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 100;
        }
        public void appendTextToProcessOutput(string text,Color col, FontStyle fontStyle)
        {            
            int oldSelectionStart = richTextBoxProcessOutput.SelectionStart;
            int oldSelectionLength = richTextBoxProcessOutput.SelectionLength;
            Color oldColor = richTextBoxProcessOutput.SelectionColor;
            Font oldFont = richTextBoxProcessOutput.SelectionFont;
            int oldTextLength = richTextBoxProcessOutput.TextLength;
            richTextBoxProcessOutput.AppendText(text);           
            richTextBoxProcessOutput.Select(oldTextLength, richTextBoxProcessOutput.TextLength);
            richTextBoxProcessOutput.SelectionFont = new Font(richTextBoxProcessOutput.Font, fontStyle);
            richTextBoxProcessOutput.SelectionColor = col;
            richTextBoxProcessOutput.Select(oldSelectionStart, oldSelectionLength);
            richTextBoxProcessOutput.SelectionFont = oldFont;
            richTextBoxProcessOutput.SelectionColor = oldColor;
            richTextBoxProcessOutput.ScrollToBottom();
        }
    }
    public partial class ReadOnlyRichTextBox : RichTextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_VSCROLL = 277;
        private const int SB_PAGEBOTTOM = 7;

        public void ScrollToBottom()
        {
            SendMessage(this.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
            if (this.SelectionLength == 0)
            { 
               this.SelectionStart = this.Text.Length;
            }
        }
        public ReadOnlyRichTextBox()
        {
            this.ReadOnly = true;
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            HideCaret(this.Handle);
        }
    }

}
