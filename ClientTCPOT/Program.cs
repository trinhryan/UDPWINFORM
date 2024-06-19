using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Client";
            
            //Nhap dia chii IPAddress vaf Port
            Console.Write("Server IP address:");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);
            
            Console.Write("Server Port:");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);
            
            //Thiet lap IPEndpoint
            var serverEndPort = new IPEndPoint(serverIP, serverPort);
            Console.Write("type the text sent to the server \n");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
              // ket noi socket voi ipendpoint
              var socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
              socket.Connect(serverEndPort);
              
              //Gui du lieu toi Server
              Console.ForegroundColor = ConsoleColor.Green;
              Console.Write("#Text>>>");
              Console.ResetColor();
              var text = Console.ReadLine();
              
              var sendBuffer = Encoding.ASCII.GetBytes(text);
              socket.Send(sendBuffer);
              socket.Shutdown(SocketShutdown.Send);//dong ket noi, khong gui du lieu nuwa
              
              //Nhan du lieu tu Server
              var length = socket.Receive(receiveBuffer);
              var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
              Console.WriteLine($"response from Server <<<{result}");
              socket.Shutdown(SocketShutdown.Receive);//Dong ket noi, khong nhan du lieu nua
              
              socket.Close();
              
                  //xoa bo nho dem
                  Array.Clear(receiveBuffer, 0, size);
            }
           
        }
    }
}

