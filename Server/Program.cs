using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

                serverData.socketClient.Send(Encoding.Unicode.GetBytes("Welcome on server!"));
            }
        }
    }
}