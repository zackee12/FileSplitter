using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileSplitter
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // select MB as a default
            comboBox_ByteSize.SelectedIndex = 2;
            textBox_Path.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private long getMaxByteSize()
        {
            if (comboBox_ByteSize.SelectedIndex == 0) return (long)numericUpDown_SplitSize.Value;
            else if (comboBox_ByteSize.SelectedIndex == 1) return (long)numericUpDown_SplitSize.Value * 1024;
            else if (comboBox_ByteSize.SelectedIndex == 2) return (long)numericUpDown_SplitSize.Value * 1024 * 1024;
            else return 0;
        }

        private static string openFile(string filter, string directory)
        {
            // open a file dialog asking for an input
            OpenFileDialog ofd = new OpenFileDialog();

            // ofd settings
            ofd.Filter = filter;
            ofd.InitialDirectory = directory;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.CheckFileExists = true;

            // return the path
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        private static string openFolder()
        {
            // open a folder browser
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // get the path to store the output file
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return "";
        }



        private void button_FileBrowse_Click(object sender, EventArgs e)
        {
            textBox_File.Text = openFile("All Files (*.*)|*.*", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private void button_FolderBrowse_Click(object sender, EventArgs e)
        {
            textBox_Path.Text = openFolder();
        }

        private void userInputChanged(object sender, EventArgs e)
        {
            if (textBox_File.TextLength > 0 && File.Exists(textBox_File.Text))
            {
                // update the status label
                FileInfo fi = new FileInfo(textBox_File.Text);
                toolStripStatusLabel_SplitNum.Text = string.Format("{0} split files", Math.Ceiling(fi.Length / (double)getMaxByteSize()));
            }
            else toolStripStatusLabel_SplitNum.Text = "0 split files";

            // check if the split can be enabled
            if (Directory.Exists(textBox_Path.Text) && File.Exists(textBox_File.Text)) button_Split.Enabled = true;
            else button_Split.Enabled = false;
        }

        private void button_Split_Click(object sender, EventArgs e)
        {

            button_Split.Enabled = false;
            toolStripProgressBar_Split.Visible = true;

            List<object> list = new List<object>();
            list.Add((long)numericUpDown_stepSize.Value*1024*1024);
            list.Add(getMaxByteSize());
            list.Add(textBox_File.Text);
            list.Add(textBox_Path.Text);
            splitWorker.RunWorkerAsync(list);
            textBox_File.Text = "";
        }

        private void splitWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var parameters = (List<object>)e.Argument;
                if (parameters == null) throw new ArgumentNullException("Pass 4 arguments to the splitWorker [long, long, string, string]");
                if (parameters.Count != 4) throw new ArgumentException("Pass 4 arguments to the splitWorker [long, long, string, string]");
                SplitFilesAsync((BackgroundWorker)sender, (long)parameters.ElementAt(0), (long)parameters.ElementAt(1), (string)parameters.ElementAt(2), (string)parameters.ElementAt(3));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void splitWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar_Split.Value = e.ProgressPercentage;
        }

        private void SplitFiles(long stepSize, long maxFileSize, string filename, string storagePath)
        {
            // check arguments
            if (filename == null) throw new ArgumentNullException("The file name was not assigned a value");
            if (storagePath == null) throw new ArgumentNullException("The storage path was not assigned a value");
            if (!File.Exists(filename)) throw new ArgumentException(string.Format("The provided file <{0}> does not exist", filename));
            if (!Directory.Exists(storagePath)) throw new ArgumentException(string.Format("The provided directory <{0}> does not exist", storagePath));
            if (stepSize < 1) throw new ArgumentException(string.Format("The provided step size <{0}> must be larger than zero", stepSize));
            if (maxFileSize < 1) throw new ArgumentException(string.Format("The provided maximum file size <{0}> must be larger than zero", maxFileSize));

            // create storage directory [Split_Files_%filename%_%date%_v%number%]
            int dirNum = 0;
            while (Directory.Exists(storagePath + @"\Split_Files_" + DateTime.Now.ToString("_yyyy-MM-dd") + string.Format("_v{0}", dirNum))) dirNum++;
            storagePath += @"\Split_Files_" + DateTime.Now.ToString("_yyyy-MM-dd") + string.Format("_v{0}", dirNum);
            Directory.CreateDirectory(storagePath);
            if (!Directory.Exists(storagePath)) throw new ArgumentException(string.Format("The created directory <{0}> does not exist", storagePath));

            // copy exe joiner to folder
            File.Copy("_FileJoiner.exe",storagePath + @"\_FileJoiner.exe");

            // open the file to be split
            using (FileStream source = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                long numBytesToRead = source.Length; // bytes in source that haven't been read
                long numBytesRead = 0; // bytes read in source
                byte[] bytes; // buffer to hold read data
                int fileNum = 0; // output filenumber
                bool reset = false; // flag 

                while (numBytesToRead > 0)
                {
                    int n = 0;
                    // read the source file
                    if (stepSize < maxFileSize - numBytesRead)
                    {
                        // read stepSize number of bytes
                        bytes = new byte[stepSize];
                        n = source.Read(bytes, 0, (int)stepSize);
                    }
                    else
                    {
                        // read the amount of bytes to reach the max file size
                        bytes = new byte[maxFileSize - numBytesRead];
                        n = source.Read(bytes, 0, (int)(maxFileSize - numBytesRead));
                        reset = true;
                    }

                    // break when the end of the file is reached. 
                    if (n == 0) break;

                    // adjust the byte size to the bytes read
                    if (n < bytes.Length)
                    {
                        byte[] temp = new byte[n];
                        Array.Copy(bytes, temp, n);
                        bytes = new byte[n];
                        bytes = temp;
                    }

                    // outfile name
                    string split_filename = storagePath + @"\" + Path.GetFileNameWithoutExtension(filename)  + Path.GetExtension(filename) + "." + string.Format("{0:0000000}", fileNum);

                    // create new file
                    if (!File.Exists(split_filename))
                    {
                        using (FileStream fsNew = new FileStream(split_filename, FileMode.Create, FileAccess.Write)) fsNew.Write(bytes, 0, bytes.Length);
                    }

                    // append bytes to existing file
                    else
                    {
                        using (FileStream fsApp = new FileStream(split_filename, FileMode.Append)) fsApp.Write(bytes, 0, bytes.Length);
                    }

                    // update the variables so a new file will be created
                    if (reset)
                    {
                        numBytesRead = 0;
                        fileNum++;
                        reset = false;
                    }
                    else numBytesRead += n;

                    // update the bytes remaining
                    numBytesToRead -= n;
                }

            }
        }

        private void SplitFilesAsync(BackgroundWorker worker, long stepSize, long maxFileSize, string filename, string storagePath)
        {
            // check arguments
            if (filename == null) throw new ArgumentNullException("The file name was not assigned a value");
            if (storagePath == null) throw new ArgumentNullException("The storage path was not assigned a value");
            if (!File.Exists(filename)) throw new ArgumentException(string.Format("The provided file <{0}> does not exist", filename));
            if (!Directory.Exists(storagePath)) throw new ArgumentException(string.Format("The provided directory <{0}> does not exist", storagePath));
            if (stepSize < 1) throw new ArgumentException(string.Format("The provided step size <{0}> must be larger than zero", stepSize));
            if (maxFileSize < 1) throw new ArgumentException(string.Format("The provided maximum file size <{0}> must be larger than zero", maxFileSize));

            // create storage directory [Split_Files_%filename%_%date%_v%number%]
            int dirNum = 0;
            while (Directory.Exists(storagePath + @"\Split_Files_" + DateTime.Now.ToString("_yyyy-MM-dd") + string.Format("_v{0}", dirNum))) dirNum++;
            storagePath += @"\Split_Files_" + DateTime.Now.ToString("_yyyy-MM-dd") + string.Format("_v{0}", dirNum);
            Directory.CreateDirectory(storagePath);
            if (!Directory.Exists(storagePath)) throw new ArgumentException(string.Format("The created directory <{0}> does not exist", storagePath));

            worker.ReportProgress(0);

            // copy exe joiner to folder
            File.Copy("_FileJoiner.exe", storagePath + @"\_FileJoiner.exe");

            // open the file to be split
            using (FileStream source = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                long numBytesToRead = source.Length; // bytes in source that haven't been read
                long numBytesRead = 0; // bytes read in source
                byte[] bytes; // buffer to hold read data
                int fileNum = 0; // output filenumber
                bool reset = false; // flag 

                while (numBytesToRead > 0)
                {
                    int n = 0;
                    // read the source file
                    if (stepSize < maxFileSize - numBytesRead)
                    {
                        // read stepSize number of bytes
                        bytes = new byte[stepSize];
                        n = source.Read(bytes, 0, (int)stepSize);
                    }
                    else
                    {
                        // read the amount of bytes to reach the max file size
                        bytes = new byte[maxFileSize - numBytesRead];
                        n = source.Read(bytes, 0, (int)(maxFileSize - numBytesRead));
                        reset = true;
                    }

                    // break when the end of the file is reached. 
                    if (n == 0) break;

                    // adjust the byte size to the bytes read
                    if (n < bytes.Length)
                    {
                        byte[] temp = new byte[n];
                        Array.Copy(bytes, temp, n);
                        bytes = new byte[n];
                        bytes = temp;
                    }

                    // outfile name
                    string split_filename = storagePath + @"\" + Path.GetFileNameWithoutExtension(filename)  + Path.GetExtension(filename) + "." + string.Format("{0:0000000}", fileNum);

                    // create new file
                    if (!File.Exists(split_filename))
                    {
                        using (FileStream fsNew = new FileStream(split_filename, FileMode.Create, FileAccess.Write)) fsNew.Write(bytes, 0, bytes.Length);
                    }

                    // append bytes to existing file
                    else
                    {
                        using (FileStream fsApp = new FileStream(split_filename, FileMode.Append)) fsApp.Write(bytes, 0, bytes.Length);
                    }

                    // update the variables so a new file will be created
                    if (reset)
                    {
                        numBytesRead = 0;
                        fileNum++;
                        reset = false;
                    }
                    else numBytesRead += n;

                    // update the bytes remaining
                    numBytesToRead -= n;
                    worker.ReportProgress((int)((1 - (double)numBytesToRead / source.Length) * 100));
                }

            }
        }

        
    }
}
