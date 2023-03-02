using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ClientHandler
    {
        public TcpClient clientSocket;

        public ClientHandler(TcpClient client)
        {
            clientSocket = client;
        }
        public void RunClient()
        {
            try
            {
                StreamReader rs = new StreamReader(clientSocket.GetStream());
                NetworkStream ws = clientSocket.GetStream();

                string name = ClientConnected(rs);
                string returnData;

                while (true)
                {
                    returnData = rs.ReadLine();

                    if (returnData.IndexOf("quit") > -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Disconnected: " + name);
                        Console.ResetColor();
                        break;
                    }

                    Console.WriteLine(name + " : " + returnData);
                    returnData += "\r\n";

                    byte[] dataWrite = Encoding.ASCII.GetBytes(returnData);
                    ws.Write(dataWrite, 0, dataWrite.Length);
                }

                clientSocket.Close();
            } catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        private string ClientConnected(StreamReader rs)
        {
            string name = rs.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(name + " is connected");
            Console.ResetColor();
            return name;
        }
    }
}
