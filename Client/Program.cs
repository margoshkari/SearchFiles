using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                messages = clientData.GetMsg().Split("^").ToList();
                Console.WriteLine(messages[0]);

                GetFiles();
                StartApp();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void GetFiles()
        {
            messages = clientData.GetMsg().Split("^").ToList();
            Console.WriteLine(messages[0]);
            int filesCount = int.Parse(messages[1]);
            
            for (int i = messages.IndexOf(filesCount.ToString()) + 1; i < messages.Count(); i++)
            {
                Console.WriteLine(messages[i]);
            }
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
