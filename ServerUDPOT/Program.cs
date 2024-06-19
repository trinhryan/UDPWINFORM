using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPServer;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Title = "UDP Server";

            // Set up IPEndpoint
            var localIP = IPAddress.Any;
            var localPort = 1308;
            var localEndpoint = new IPEndPoint(localIP, localPort);

            // Set up UdpClient
            var listener = new UdpClient(localPort); // Bind the UDP client to the local port
            Console.WriteLine($"Local UDP client bound to {localEndpoint}. Waiting for requests...");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                // Receive data from client
                var clientEndpoint = new IPEndPoint(IPAddress.Any, 0);
                receiveBuffer = listener.Receive(ref clientEndpoint);
                var text = Encoding.ASCII.GetString(receiveBuffer);
                Console.WriteLine($"Received from client: {text}");

                // Determine response based on client message
                string messageTraVe;

                if (text.ToLower().Trim().Equals("now"))
                {
                    messageTraVe = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                }
                else if (text.ToLower().Trim().Equals("day"))
                {
                    messageTraVe = DateTime.Now.ToString("dd");
                }
                else if (text.ToLower().Trim().Equals("month"))
                {
                    messageTraVe = DateTime.Now.ToString("MM");
                }
                else if
                    (text.ToLower().Trim()
                     .Contains("name")) //Từ Client nhập tên gửi sang Server, Client phản hồi Server theo cú pháp Xin chào + name
                {
                    var name = text.Replace("name", "").Trim(); // lấy tên từ message
                    messageTraVe = $"Xin chào {name}"; // tạo message trả về
                }
                else
                {
                    messageTraVe = "Invalid command";
                }

                Console.WriteLine($"Response from Server: {messageTraVe}");

                // Send response to client
                var sendBuffer = Encoding.ASCII.GetBytes(messageTraVe);
                listener.Send(sendBuffer, sendBuffer.Length, clientEndpoint);
                Console.WriteLine($"Sent to client: {messageTraVe}");
                Console.WriteLine("--------------------------------");

                // Clear the receive buffer
                //Array.Clear(receiveBuffer, 0, size);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}