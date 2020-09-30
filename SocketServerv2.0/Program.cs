using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServerv2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: 
            const string ip = "185.20.226.150";
            const int port = 8080;
            var tcpEndPoint = new IPEndPoint(IPAddress.Any, port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(10);
            var listener = tcpSocket.Accept();

            while (true)
            {
                Console.WriteLine("Готов слушать");
                var buffer = new byte[1024];
                var data = new StringBuilder();
                do
                {
                    int size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(
                        buffer, 0, size));


                }
                while(listener.Available > 0);

                Console.WriteLine(data);

                listener.Send(Encoding.UTF8.GetBytes("Чётко все!"));

                //listener.Shutdown(SocketShutdown.Both);
                //listener.Close();
                //tcpSocket.Dispose();
            }
        }
    }
}
