using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClientv2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "185.20.226.150";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Connect(tcpEndPoint);
            while (true)
            {
                Console.WriteLine("Введите сообщение: ");
                var message = Console.ReadLine();

                var data = Encoding.UTF8.GetBytes(message);
                tcpSocket.Send(data);

                var buffer = new byte[1024];
                var answer = new StringBuilder();

                do
                {
                    int size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (tcpSocket.Available > 0);

                Console.WriteLine(answer.ToString());
                //tcpSocket.Shutdown(SocketShutdown.Both);
                //tcpSocket.Close();
            }

        }
    }
}
