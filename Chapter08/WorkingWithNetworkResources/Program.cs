using System;
using System.Net;
using System.Net.NetworkInformation;
using static System.Console;

namespace WorkingWithNetworkResources
{
    class Program
    {
        static void Main(string[] args)
        {

            // BOOK: page 277 Working with URIs, DNS and IP addresses

            try
            {
                Write("Enter a valid web address: ");
                string url = ReadLine();

                if (string.IsNullOrWhiteSpace(url))
                {
                    url = "https://world.episerver.com/cms/?q=pagetype";
                }             
                
                var uri = new Uri(url);

                WriteLine($"URL: {url}");
                WriteLine($"Scheme: {uri.Scheme}");
                WriteLine($"Port: {uri.Port}");
                WriteLine($"Host: {uri.Host}");
                WriteLine($"Path: {uri.AbsolutePath}");
                WriteLine($"Query: {uri.Query}");

                
                IPHostEntry entry = Dns.GetHostEntry(uri.Host);
                WriteLine();
                WriteLine($"{entry.HostName} has the following IP addresses: ");
                foreach (IPAddress address in entry.AddressList)
                {
                    WriteLine($" {address}");
                }   

            // BOOK: page 278 Pinging a server
            // BOOK: page 278 Pinging a server


                var ping = new Ping();
                WriteLine("Pinging server...please wait...");
                PingReply reply = ping.Send(uri.Host);
                WriteLine($"{uri.Host} was pinged and replied: {reply.Status}");

                if (reply.Status == IPStatus.Success)
                {
                    WriteLine($"Reply from { reply.Address } took { reply.RoundtripTime:N0}ms");
                }
            }
            catch (System.Exception ex) 
            {
                WriteLine($"{ex.GetType().ToString()} says {ex.Message}");
            }

        }
    }
}
