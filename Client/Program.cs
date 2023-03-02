using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        const int PORT = 8080;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("type server address in: ");
                string ip = Console.ReadLine();

                TcpClient client = new TcpClient(ip, PORT);
                StreamReader rs = new StreamReader(client.GetStream());
                NetworkStream ws = client.GetStream();

                Console.WriteLine("your name: ");
                string name = Console.ReadLine();

                string dataToSend;
                dataToSend = name + "\r\n";

                byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                ws.Write(data, 0, data.Length);

                while(true)
                {
                    dataToSend = "> " + name + " : " + Console.ReadLine();
                    dataToSend += "\r\n";

                    data = Encoding.ASCII.GetBytes(dataToSend);
                    ws.Write(data, 0, data.Length);

                    if (dataToSend.IndexOf("quit") > -1) break;

                    string returnData;
                    returnData = rs.ReadLine();

                    Console.WriteLine(returnData);
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.ReadKey();
        }
    }
}
