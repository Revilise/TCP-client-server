using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        const int PORT = 8080;
        public static ClientsList clients = new ClientsList();
        static void Main(string[] args)
        {
            try
            {
                IPAddress ip = GetLocalIPAddress();
                TcpListener tcpListener = new TcpListener(ip, PORT);
                tcpListener.Start();
                Console.WriteLine("Waiting for connections " +  ip.ToString() + ":" + PORT);

                while (clients.Count < 3)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    clients.Add(new ClientHandler(client));
                }

                tcpListener.Stop();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
