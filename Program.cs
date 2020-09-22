using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Did
{
    class Program
    {

        static async Task ScanPort(string ip, int curentPort)
        {
            var ipAddress = IPAddress.Parse(ip);
            var port = curentPort;
            var tcpScan = new TcpClient();
            
            try
            {
                
                await tcpScan.ConnectAsync(ipAddress, port);

                Console.WriteLine("Порт " + port + " открыт");

            }
            catch
            {
                Console.WriteLine("Порт " + port + " закрыт");
            }
            finally
            {
                tcpScan.Close();
            }
        }

        static async void Scan(string ip, int startPort, int endPort)
        {
            List<Task> tasks = new List<Task>();
            for (int i = startPort; i < endPort; i++)
            {
                var j = i;

                tasks.Add(Task.Factory.StartNew(() => ScanPort(ip, j)));
                
            }
            await Task.WhenAll(tasks);
            

        }

        
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Аргументов долно быть три: ip, startPort, endPort");
                return;
            }
            var startPort = int.Parse(args[1]);
            var endPort = int.Parse(args[2]);
             Scan(args[0],startPort,endPort);
            Console.ReadLine();
        }
    }
}