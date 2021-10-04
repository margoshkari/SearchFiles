using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static ClientData clientData = new ClientData();
        static List<string> messages = new List<string>();
        static void Main(string[] args)
        {
            try
            {
                clientData.socket.Connect(clientData.iPEndPoint);

                GetFiles();
                StartApp();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        static void GetFiles()
        {
            messages = clientData.GetMsg().Split("\n").ToList();
            messages.ForEach(Console.WriteLine);
        }
        static void StartApp()
        {
            string appName = string.Empty;
            Console.WriteLine("Enter name of app you want to start:");
            do
            {
                appName = Console.ReadLine();
            } while (!messages.Contains(appName));

            clientData.socket.Send(Encoding.Unicode.GetBytes(appName));
        }
    }
}
