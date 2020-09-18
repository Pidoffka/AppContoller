using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                SendMessageFromSocket(80);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
        static void SendMessageFromSocket(int port)
        {
            // ����� ��� �������� ������
            byte[] bytes = new byte[1024];

            // ����������� � ��������� �����������
            // 31.31.196.199
            // ������������� ��������� ����� ��� ������
            IPHostEntry ipHost = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // ��������� ����� � ��������� ������
            sender.Connect(ipEndPoint);

            Console.Write("������� ���������: ");
            string message = Console.ReadLine();

            Console.WriteLine("����� ����������� � {0} ", sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // ���������� ������ ����� �����
            int bytesSent = sender.Send(msg);

            // �������� ����� �� �������
            int bytesRec = sender.Receive(bytes);

            Console.WriteLine("\n����� �� �������: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            // ���������� �������� ��� �������������� ������ SendMessageFromSocket()
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessageFromSocket(port);

            // ����������� �����
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
