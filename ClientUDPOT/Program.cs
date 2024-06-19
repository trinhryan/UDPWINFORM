using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "UDP Client";

            // Nhập địa chỉ IP và Port của Server
            Console.Write("Server IP address: ");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);

            Console.Write("Server Port: ");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);

            // Thiết lập IPEndpoint của Server
            var serverEndpoint = new IPEndPoint(serverIP, serverPort);
            Console.WriteLine("Type the text to be sent to the server\n");

            var size = 1024;
            var receiveBuffer = new byte[size];

            // Thiết lập UdpClient
            using (var udpClient = new UdpClient())
            {
                while (true)
                {
                    // Gửi dữ liệu tới Server
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("#Text>>>");
                    Console.ResetColor();
                    var text = Console.ReadLine();

                    var sendBuffer = Encoding.ASCII.GetBytes(text);
                    udpClient.Send(sendBuffer, sendBuffer.Length, serverEndpoint);

                    // Nhận dữ liệu từ Server
                    var serverResponseEndpoint = new IPEndPoint(IPAddress.Any, 0);
                    var response = udpClient.Receive(ref serverResponseEndpoint);
                    var result = Encoding.ASCII.GetString(response);
                    Console.WriteLine($"Response from Server <<< {result}");

                    // Xóa bộ nhớ đệm
                    Array.Clear(receiveBuffer, 0, size);
                }
            }
        }
    }
}