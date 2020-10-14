﻿using System;
using System.IO;                    // types for managing the filesystem
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;

namespace WorkingWithFileSystems
{
    class Program
    {
        private const bool Recursive = true;

        static void Main(string[] args)
        {
            //OutputFileSystemInfo();
            //WorkWithDrives();
            //WorkWithDirectories();
            WorkWithFiles();
            
        }

        static void OutputFileSystemInfo()
        {
            WriteLine("{0,-33} {1}", "Path.PathSeparator", PathSeparator);
            WriteLine("{0,-33} {1}", "Path.DirectorySeparatorChar", DirectorySeparatorChar);
            WriteLine("{0,-33} {1}", "Directory.GetCurrentDirectory()", GetCurrentDirectory());
            WriteLine("{0,-33} {1}", "Environment.OSVersion", OSVersion);
            WriteLine("{0,-33} {1}", "Environment.MachineName", MachineName);
            WriteLine("{0,-33} {1}", "Environment.CommandLine", Environment.CommandLine);
            
            
            WriteLine("{0,-33} {1}", "Environment.CurrentDirectory", CurrentDirectory);
            WriteLine("{0,-33} {1}", "Environment.SystemDirectory", SystemDirectory);
            WriteLine("{0,-33} {1}", "Path.GetTempPath()",  GetTempPath());
            WriteLine();
            WriteLine("GetFolderPath(SpecialFolder");
            WriteLine("{0,-33} {1}", "  .System)",          GetFolderPath(SpecialFolder.System));
            WriteLine("{0,-33} {1}", "  .ApplicationData)", GetFolderPath(SpecialFolder.ApplicationData));
            WriteLine("{0,-33} {1}", "  .MyDocuments)",     GetFolderPath(SpecialFolder.MyDocuments));
            WriteLine("{0,-33} {1}", "  .Personal)",        GetFolderPath(SpecialFolder.Personal));
        }

        static void WorkWithDrives()
        {
            WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}",
            "NAME", "TYPE", "FORMAT", "SIZE (BYTES)", "FREE SPACE");

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                // good practice to check a drive is ready before reading properties (or see exception with removable drives)
                if (drive.IsReady)
                {
                    WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}",
                    drive.Name, drive.DriveType, drive.DriveFormat, drive.TotalSize, drive.AvailableFreeSpace);
                }
                else
                {
                    WriteLine("{0,-30} | {1,-10}", drive.Name, drive.DriveType);
                }
            }
        }

        static void WorkWithDirectories()
        {
            // define custom path under user's home directory by creating an array of strings for dir names...
            //...then properly combine them with the "Combine" method of the Path class
            //string[] dirNames;
            WriteLine($"SpecialFolder.Personal: {SpecialFolder.Personal}");
            WriteLine($"SpecialFolder.MyDocuments: {SpecialFolder.MyDocuments}");
            
            WriteLine($"Folder Path of SpecialFolder.Personal: {GetFolderPath(SpecialFolder.Personal)}");
            WriteLine($"Folder Path of SpecialFolder.MyDocuments: {GetFolderPath(SpecialFolder.MyDocuments)}");

            // define a directory path for a new folder
            // starting in the user's folder
            string newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "Code", "Chapter09", "NewFolder");

            WriteLine($"Working with: {newFolder}");                 
            WriteLine($"Does {newFolder} exist? {Exists(newFolder)}");            

            WriteLine($"Creating it...");            
            CreateDirectory(newFolder);
            WriteLine($"Does {newFolder} exist? {Exists(newFolder)}");     

            WriteLine($"Confirm directory exists, then press enter:");            
            ReadKey();

            WriteLine($"Deleting it...");            
            Delete(newFolder, recursive: true);
            WriteLine($"Does {newFolder} exist? {Exists(newFolder)}");  
        }

        static void WorkWithFiles()
        {
            /*  USEFUL info regarding different between FileStream and StreamWriter. 
                --------------------------------------------------------------------
                /// <summary>
                /// NOTE NEED TO LOOK AT using   e.g.
                ///    using (FileStream f = new FileStream(@"C:\users\jaredpar\using.md")) 
                ///    {
                ///        program statements here...
                ///    }
                /// which may be better practice than calling a manual fileReader.Close() or fileWriter.Close()
                /// </summary>

            We need StreamWriter here in this method.

            FileStream and StreamWriter are two different levels used in outputting information to known data sources.

            The type of streams includes −
                Byte Streams − have classes that consider data in the stream as byte.It includes Stream, FileStream, MemoryStream and BufferedStream
                Character Streams − It includes Textreader-TextWriter, StreamReader, StraemWriter and other streams


            Byte Streams: following classes inherit from Stream to read/write bytes from a particular source
                FileStream reads or writes bytes from/to a physical file, whether it is a .txt, .exe, .jpg, or any other file. FileStream is derived from the Stream class.
                MemoryStream: MemoryStream reads or writes bytes that are stored in memory.
                BufferedStream: BufferedStream reads or writes bytes from other Streams to improve certain I/O operations' performance.
                NetworkStream: NetworkStream reads or writes bytes from a network socket.
                PipeStream: PipeStream reads or writes bytes from different processes.
                CryptoStream: CryptoStream is for linking data streams to cryptographic transformations. 


            Character Streams: 
                StreamWriter- a wrapper for a Stream that simplifies using that stream to output plain text. It exposes methods that take strings instead of bytes, and performs the necessary conversions to and from byte arrays. There are other Writers; the other main one you'd use is the XmlTextWriter, which facilitates writing data in XML format. There are also Reader counterparts to the Writers that similarly wrap a Stream and facilitate getting the data back out.
                A StreamWriter : TextWriter, is a Stream-decorator. A TextWriter encodes Text data like string or char to byte[] and then writes it to the linked Stream.         
                You always need a Stream to create a StreamWriter. 
                The helper method System.IO.File.CreateText("path") will create a FileStream and StreamWriter in combination and then you only have to Dispose() the outer writer.
                
            Use a bare FileStream when you have byte[] data.
            Add a StreamWriter when you want to write text.
            Use a Formatter or a Serializer to write more complex data

            */

            try  
            {  
                // define dir path to output files to, starting in the user's folder
                string filePath = Combine(GetFolderPath(SpecialFolder.Personal),"Code", "Chapter09", "OutputFiles");
                CreateDirectory(filePath);

                // Define full path of files to be output
                string textFile = Combine(filePath, "Dummy.txt");
                string backupFile = Combine(filePath, "Dummy.bak");

                WriteLine($"Working with {textFile}");

                // check file exists
                WriteLine(File.Exists(textFile) ? $"File {textFile} exists." : $"File {textFile} does not exist.");
                WriteLine("Creating file...");

                // create new file and write lines to it (NOTE IF FILE EXISTS, current lines will OVERWRITE what'S there UNLESS append parameter set to true)
                StreamWriter fileStreamWriter = new StreamWriter(textFile,append: false);
                
                for (int i = 0; i < 8; i++)
                {
                    fileStreamWriter.WriteLine($"This is line number {i} in my new text file.");
                }
                fileStreamWriter.Close();
                fileStreamWriter.Dispose();
                
                // check file exists
                WriteLine(File.Exists(textFile) ? $"File {textFile} exists." : $"File {textFile} does not exist.");


                // copy the file and overwrite it if already exists
                File.Copy(textFile, backupFile,overwrite: true);

                // ask user to check file has been created, then hit enter
                WriteLine($"Check {backupFile} has is created then hit enter:");
                ReadKey();

                // delete the file
                File.Delete(textFile);

                // check the file no longer exists
                WriteLine(File.Exists(textFile) ? $"File {textFile} exists." : $"File {textFile} does not exist.");
                
                // read from the backup file and output contents to screen
                StreamReader fileStreamReader = new StreamReader(backupFile);
                Write(fileStreamReader.ReadToEnd());
                fileStreamReader.Close();


                // BOOK: p299 MANAGING PATHS    - working with parts of a path
                WriteLine($"Folder name: {GetDirectoryName(textFile)}");
                WriteLine($"File name: {GetFileName(textFile)}");
                WriteLine($"File name without extension: {GetFileNameWithoutExtension(textFile)}");
                WriteLine($"File extension: {GetExtension(textFile)}");
                WriteLine($"Random file name: {GetRandomFileName()}");  //  h55p5t4n.2u4
                WriteLine($"Temporary file name: {GetTempFileName()}"); //  /tmp/tmpvKUUsf.tmp   

                // BOOK: p300 Getting File Information - FileInfo and DirectoryInfo classes (both inherit from FileSystemInfo)
                var info = new FileInfo(backupFile);
                WriteLine($"{backupFile}:");
                WriteLine($"contains: {info.Length} bytes");
                WriteLine($"was last accessed: {info.LastAccessTime}");
                WriteLine($"has readonly set to: {info.IsReadOnly}");
                WriteLine($"has been compressed: {info.Attributes.HasFlag(FileAttributes.Compressed)}");

                
            }  
            catch (IOException iox)  
            {  
                WriteLine(iox.Message);  
            }  

        }

    }
}