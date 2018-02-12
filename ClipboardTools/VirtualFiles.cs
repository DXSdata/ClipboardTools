using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardTools
{
    /// <summary>
    /// From: https://stackoverflow.com/questions/24985239/dropped-zip-file-causes-e-data-getdatafilecontents-to-throw-an-exception
    /// </summary>
    public static class VirtualFiles
    {
        private static List<FileInfo> savedFiles = new List<FileInfo>();
        private static List<MemoryStream> savedStreams = new List<MemoryStream>();

        public static bool ContainsVirtualFiles
        {
            get
            {
                //if (Clipboard.ContainsAudio() ||
                //    Clipboard.ContainsFileDropList() ||
                //    Clipboard.ContainsImage() ||
                //    Clipboard.ContainsText())
                //    return false;

                return Clipboard.ContainsData("FileGroupDescriptorW");
            }
        }

        /// <summary>
        /// Retrieves all streams of the available clipboard content
        /// </summary>
        /// <returns></returns>
        public static MemoryStream [] GetVirtualFilesAsStreams()
        {
            GetVirtualFiles(false);
            return savedStreams.ToArray();
        }

        /// <summary>
        /// Returns ready-to-use files saved to chosen directory.
        /// </summary>
        /// <param name="directory">If not given, temporary directory is used.</param>
        /// <returns></returns>
        public static FileInfo [] GetVirtualFilesAsFiles(String directory = null)
        {
            GetVirtualFiles(true, directory);
            return savedFiles.ToArray();
        }

        private static void GetVirtualFiles(bool saveFiles, String directory = null)
        {
            savedFiles.Clear();
            savedStreams.Clear();

            if (saveFiles && directory == null)
                directory = Path.GetTempPath();

            if (saveFiles)
                Debug.WriteLine("Writing to directory " + directory);
            else
                Debug.WriteLine("Collecting streams");

            
            int i = 0;



            var fileDescriptor = (MemoryStream)Clipboard.GetDataObject().GetData("FileGroupDescriptorW");
            var files = FileDescriptorReader.Read(fileDescriptor);
            foreach (var fileContentFile in files)
            {
                if ((fileContentFile.FileAttributes & FileAttributes.Directory) != 0)
                {
                    //Directories
                    Debug.WriteLine("Is directory -> skipping.");
                    //Note that directories do not have FileContents
                    //And will throw if we try to read them
                }
                else
                {
                    //Files
                    //Debug.WriteLine("Is file.");

                    Debug.WriteLine(fileContentFile.FileName);

                    var fileData = VirtualFileHelper.GetFileContents(System.Windows.Clipboard.GetDataObject(), i);

                    if (saveFiles)
                    {
                        fileData.Position = 0;
                        String fullname = directory + "\\" + (string.IsNullOrWhiteSpace(fileContentFile.FileName) ? "ClipboardFile" + i : fileContentFile.FileName);
                        using (FileStream file = new FileStream(fullname, FileMode.Create, System.IO.FileAccess.Write))
                        {
                            byte[] bytes = new byte[fileData.Length];
                            fileData.Read(bytes, 0, (int)fileData.Length);
                            file.Write(bytes, 0, bytes.Length);
                            fileData.Close();
                        }
                        savedFiles.Add(new FileInfo(fullname));
                        Debug.WriteLine("Saved as " + fullname);
                    }
                    else
                        savedStreams.Add(fileData);

                }
                i++;
            }
        }


        /// <summary>
        /// Retrieves all streams of the available clipboard content
        /// </summary>
        /// <returns></returns>
        public static MemoryStream[] GetOutlookVirtualFilesAsStreams()
        {
            GetOutlookVirtualFiles(false);
            return savedStreams.ToArray();
        }

        /// <summary>
        /// Returns ready-to-use files saved to chosen directory.
        /// </summary>
        /// <param name="directory">If not given, temporary directory is used.</param>
        /// <returns></returns>
        public static FileInfo[] GetOutlookVirtualFilesAsFiles(String directory = null)
        {
            GetOutlookVirtualFiles(true, directory);
            return savedFiles.ToArray();
        }


        private static void GetOutlookVirtualFiles(bool saveFiles, String directory = null)
        {
            savedFiles.Clear();
            savedStreams.Clear();

            if (saveFiles && directory == null)
                directory = Path.GetTempPath();

            Debug.WriteLine("Using Outlook handling alternative");

            //wrap standard IDataObject in OutlookDataObject
            OutlookDataObject dataObject = new OutlookDataObject(Clipboard.GetDataObject());

            //get the names and data streams of the files dropped
            string[] filenames = (string[])dataObject.GetData("FileGroupDescriptorW");
            MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");
                        
            for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
            {
                //use the fileindex to get the name and data stream
                string filename = filenames[fileIndex];
                MemoryStream filestream = filestreams[fileIndex];
                Debug.WriteLine(filename);
                String fullname = directory + "\\" + (string.IsNullOrWhiteSpace(filename) ? "ClipboardFile" + fileIndex : filename);

                if (saveFiles)
                {
                    //save the file stream using its name to the application path
                    FileStream outputStream = File.Create(fullname);
                    filestream.WriteTo(outputStream);
                    outputStream.Close();
                    savedFiles.Add(new FileInfo(fullname));
                }
                else
                    savedStreams.Add(filestream);

            }
        }
        
    }
}
