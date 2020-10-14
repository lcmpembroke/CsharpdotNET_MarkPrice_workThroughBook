using System;
using System.IO;
using System.Xml;
using System.IO.Compression;
using static System.Console;
using static System.Environment;
using static System.IO.Path;

// Book page 313
// NOTE High performance streams for the real world use PIPELINES (introduced in .NET Core 2.1) - these are powerful, but get complicated, so learn if you need them....

namespace WorkingWithStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWorking with Streams program running...");
            // WorkWithText();
            WorkWithXml();
            WorkWithCompression();                  // denser compression by around 20% with Brotli algorithm
            WorkWithCompression(useBrotli: false);
        }

        // define array of Viper pilot call signs
        static string[] callsigns = new string[] {
                "Husker", "Starbuck", "Apollo", "Boomer", "Bulldog", "Athena", "Helo", "Racetrack"
        };

        static void WorkWithText()
        {
            Console.WriteLine("\nWorkWithText() method running...");
            StreamWriter textWriter = null;
            try
            {
                // define file to write to
                string textFile = Combine(CurrentDirectory, "streams.txt");

                // create text file and return helper writer
                textWriter = File.CreateText(textFile);

                // enumerate the strings, writing each one to the strea on separate line
                foreach (string item in callsigns)
                {
                    textWriter.WriteLine(item);
                }
                textWriter.Close(); // release resources

                // output content of the file:
                WriteLine($"\n{textFile} contains {new FileInfo(textFile).Length} bytes");
                WriteLine(File.ReadAllText(textFile));
                
            }
            catch (System.Exception ex)
            {
                // if path doesn't exist, exception will be caught
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            finally
            {
                if (textWriter != null)
                {
                    textWriter.Dispose();
                }
            }
        }

        static void WorkWithXml()
        {
            Console.WriteLine("\nWorkWithXml() method running...");

            FileStream xmlFileStream = null;
            XmlWriter xmlWriter = null;

            try
            {
                
                // define file to write to
                string xmlFile = Combine(CurrentDirectory, "streams.xml");

                // create a file stream  (note XmlWriter inherits from IDisposable and is an abstract class)
                xmlFileStream = File.Create(xmlFile);
                xmlWriter = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

                // write the XML declaration
                xmlWriter.WriteStartDocument();

                // write a root element
                xmlWriter.WriteStartElement("callsigns");

                // 
                foreach (var item in callsigns)
                {
                    xmlWriter.WriteElementString("callsign", item);
                }

                // write the close root element
                xmlWriter.WriteEndElement();

                //close helper and stream
                xmlWriter.Close();
                xmlFileStream.Close();

                // output contents of the file
                WriteLine($"\n{xmlFile} contains {new FileInfo(xmlFile).Length} bytes");
                WriteLine(File.ReadAllText(xmlFile));
            }
            catch (System.Exception ex)
            {
                // if path doesn't exist, exception will be caught
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Dispose();
                    WriteLine("The xml writer's unmanaged resources have been disposed.");
                }
                if (xmlFileStream != null)
                {
                    xmlFileStream.Dispose();
                    WriteLine("The xml file stream's unmanaged resources have been disposed.");
                }

            }
        }

        // BOOK page 309
        static void WorkWithCompression(bool useBrotli = true)
        {
            Console.WriteLine("\nWorkWithCompression() method running...");

            string fileExtension = useBrotli ? "brotli" : "gzip";
            // compress the XML output
            string compressedFilePath = Combine(CurrentDirectory, $"streams.{fileExtension}");

            FileStream compressedFileStream = File.Create(compressedFilePath);

            Stream compressor;

            if (useBrotli)
            {
                compressor = new BrotliStream(compressedFileStream,CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(compressedFileStream,CompressionMode.Compress);
            }

            // NOTE the nested "using" stmts which generate a correspondong finally statement that calls dispose() on the object that implements IDisposable
            // in the outer using case that object is a Stream object which implements ths IDisposable interface
            // in the inner using case that object is an XMLWriter object which implements ths IDisposable interface
            using(compressor)
            {
                using(XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");
                    foreach (string sign in callsigns)
                    {
                        xmlGzip.WriteElementString("callsign", sign);
                    }
                    // the normal call the WriteEndElemt is not necessary because whe the XMLWriter disposes (due to using stmt)
                    // it will automatically end any elements of any depth
                }
            } // also closes the underlying stream

            // output content of compressed file
            WriteLine("\n{0} contains {1:N0} bytes",compressedFilePath, new FileInfo(compressedFilePath).Length);

            WriteLine("\nREAD contents of the compressed file:");
            WriteLine(File.ReadAllText(compressedFilePath));

            WriteLine("\nREAD contents of the decompressed XML file:");
            compressedFileStream = File.Open(compressedFilePath,FileMode.Open);

            Stream decompressor;

            if (useBrotli)
            {
                decompressor = new BrotliStream(compressedFileStream,CompressionMode.Decompress);
            }
            else
            {
                decompressor = new GZipStream(compressedFileStream,CompressionMode.Decompress);
            }


            using(decompressor)
            {
                // note XmlReader is an abstract class that implements IDispoable
                using(XmlReader reader = XmlReader.Create(decompressor))
                {
                    while (reader.Read()) // read the next XML node
                    {
                        // check if on element notes named callsign
                        if((reader.NodeType == XmlNodeType.Element) 
                        && (reader.Name == "callsign"))
                        {
                            reader.Read(); // move to the text inside the element
                            WriteLine($"{reader.Value}");
                        }
                    }
                }
            }


            WriteLine("...end of WorkWithCompression() method.");
        }
    }
}
