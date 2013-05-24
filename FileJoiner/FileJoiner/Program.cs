using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FileJoiner
{
    class Program
    {
        private const string SPLIT_ENDING = ".0000000";
        static void Main(string[] args)
        {
            
            string currentDir= Directory.GetCurrentDirectory();
            // get all files in the current directory
            List<string> allFiles = new List<string>(Directory.GetFiles(currentDir, "*", SearchOption.TopDirectoryOnly));

            // get files that are splits
            string pattern = @"^.*\.\d{"+ (SPLIT_ENDING.Length - 1).ToString() +"}$";
            Regex regex = new Regex(pattern);
            List<string> validSplits = allFiles.Where(str => regex.IsMatch(str)).ToList();

            // get the first split files in each
            pattern = @"^.*\" + SPLIT_ENDING + "$";
            regex = new Regex(pattern);
            List<string> baseSplits = validSplits.Where(str => regex.IsMatch(str)).ToList();

            // check to make sure that the extension is correct
            foreach (string filePath in baseSplits)
            {
                // remove the base split from the total split list
                validSplits.Remove(filePath);

                // [example.txt.0000000] -> [example.txt] -> [example][_fs0][.txt]
                string fullFileName = Path.GetFileNameWithoutExtension(filePath);
                string fileName = Path.GetFileNameWithoutExtension(fullFileName);
                string fileExt = Path.GetExtension(fullFileName);
                int versionNum = 0;

                // search for a unique file name, and copy the first part
                string file_format = @"\{0}_fs{1}{2}";
                while (File.Exists(currentDir + string.Format(file_format, fileName, versionNum, fileExt))) versionNum++;
                File.Copy(filePath, currentDir + string.Format(file_format, fileName, versionNum, fileExt));
                File.Delete(filePath);

                // get all part files associated with full file
                pattern = fullFileName;
                regex = new Regex(pattern);
                List<string> fileParts = validSplits.Where(str => regex.IsMatch(str)).ToList();
                fileParts.Sort();

                // append each file to the base split
                foreach (string filePart in fileParts)
                {
                    // open each file and append
                    using (FileStream source = new FileStream(filePart, FileMode.Open, FileAccess.Read))
                    {
                        long numBytesToRead = source.Length; // bytes in source that haven't been read
                        long numBytesRead = 0; // bytes read in source
                        long stepSize = 8*1024*1024; // 8 MB buffer
                        byte[] bytes; // buffer to hold read data
                        int n;
                        while (numBytesToRead > 0)
                        {
                            // read the step size amount of bytes
                            bytes = new byte[stepSize];
                            n = source.Read(bytes, 0, (int)stepSize);

                            // break when the end of the file is reached. 
                            if (n == 0) break;

                            // adjust the byte size to the actual bytes read
                            if (n < bytes.Length)
                            {
                                byte[] temp = new byte[n];
                                Array.Copy(bytes, temp, n);
                                bytes = new byte[n];
                                bytes = temp;
                            }

                            // append the read bytes to the output file
                            using (FileStream fsApp = new FileStream(currentDir + string.Format(file_format, fileName, versionNum, fileExt), FileMode.Append)) fsApp.Write(bytes, 0, bytes.Length);

                            // update the bytes remaining
                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                    }

                    // delete the partFile
                    File.Delete(filePart);
                }
            }

            /*
            // self delete the exe file
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            assemblyPath = assemblyPath.Replace(@"file:///","");
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del " + assemblyPath;
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            */
        }
    }
}
