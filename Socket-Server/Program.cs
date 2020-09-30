using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Socket_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ������������� ��� ������ ��������� �������� �����
            IPHostEntry ipHost = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 5000);
                // ������� ����� Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // ��������� ����� ��������� �������� ����� � ������� �������� ������
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // �������� ������� ����������
                while (true)
                {
                    //Console.WriteLine("������� ���������� ����� ���� {0}", ipEndPoint);

                    // ��������� ������������������, ������ �������� ����������
                    Socket handler = sListener.Accept();

                    // �� ��������� �������, ����������� � ���� �����������

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    // �������� �� ������
                    var data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // ���������� ������ �� �������
                    //Console.Write("���������� �����: " + data + "\n\n");
                    string reply = "";
                    if(data == "�������")
                    {
                        reply = "����� �����, ������";
                    }
                    else
                    {
                        // ���������� ����� �������\
                        reply = "������� �� ������ � " + data.Length.ToString()
                                + " ��������";
                    }
                    
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        //Console.WriteLine("������ �������� ���������� � ��������.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter f = new StreamWriter("test.txt"))
                {
                    string str = ex.ToString();
                    f.Write(str);
                }
            }
            finally
            {
                //Console.ReadLine();
            }
        }
    }
}
