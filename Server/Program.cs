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
        static bool isStartNewTask = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Start server...");
            try
            {
                serverData.socket.Bind(serverData.iPEndPoint);
                serverData.socket.Listen(10);

                 Task.Factory.StartNew(() => Connect());
                //while(true)
                //{
                //    if (isStartNewTask && serverData.socketClient != null)
                //    {
                //        Task.Factory.StartNew(() => StartApp());
                //        isStartNewTask = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        static void Connect()
        {
            while (true)
            {
                serverData.socketClient = serverData.socket.Accept();
                serverData.socketClientsList.Add(serverData.socketClient);


                SearchApps();
                StartApp();
            }
        }
        static void SearchApps()
        {
            string[] tmp = Directory.GetFiles(@$"C:\Users\" + $"{Environment.UserName}" + @"\Desktop", "*", SearchOption.AllDirectories);
            serverData.SendMsg(tmp);
        }
        static void StartApp()
        {
            string appName = serverData.GetMsg();
            string path = string.Empty;
            foreach (var item in Directory.GetFiles(@$"C:\Users\" + $"{Environment.UserName}" + @"\Desktop", "*", SearchOption.AllDirectories).ToList())
            {
                if (item.Contains(appName))
                    path = Path.GetDirectoryName(item);
            }
           
            Process.Start(new ProcessStartInfo(path + @$"\{appName}")
            {
                UseShellExecute = true
            });
        }
    }
}