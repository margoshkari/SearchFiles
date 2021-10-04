using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static ServerData serverData = new ServerData();
        static void Main(string[] args)
        {
            Console.WriteLine("Start server...");
            try
            {
                serverData.socket.Bind(serverData.iPEndPoint);
                serverData.socket.Listen(10);

                Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Connect()
        {
            while (true)
            {
                serverData.socketClient = serverData.socket.Accept();
                serverData.socketClientsList.Add(serverData.socketClient);

                serverData.socketClient.Send(Encoding.Unicode.GetBytes("Welcome on server!^"));

                SearchApps();
            }
        }

        static void SearchApps()
        {
            List<string> files = new List<string>();
            files = Directory.GetFiles(@$"C:\Users\" + $"{Environment.UserName}" + @"\Desktop", "*", SearchOption.AllDirectories).ToList();

            serverData.socketClient.Send(Encoding.Unicode.GetBytes("Your apps:^"));
            serverData.socketClient.Send(Encoding.Unicode.GetBytes($"{files.Count()}^"));
            foreach (var item in files)
            {
                serverData.socketClient.Send(Encoding.Unicode.GetBytes($"{Path.GetFileName(item)}^"));
            }
        }
    }
}