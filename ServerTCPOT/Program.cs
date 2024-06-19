using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[]args)
        {
            Console.Title="TCP Server";// cài đặt tiêu đề của cửa sổ console
            //Thiết lập IPEndpoint
            var localIP = IPAddress.Any;//chấp nhận tất cả địa chỉ IPAddress
            var localPort = 1308;// nghe tất cả các gói tin gửi qua cổng 1308
            var localEndpoint = new IPEndPoint(localIP, localPort);
            
            //thiết bị Socket
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//gui dữ liệu qua giao thức tcp, loại Socket Stream, họ đại chỉ IPv4
            listener.Bind(localEndpoint);//kết nối Socket và EndPoint
            listener.Listen(10);//lắng nghe từ Client, tối đa 10 request mỗi lần
            Console.WriteLine($"Local socket bind to {localEndpoint}.Waiting for request...");
             var size = 1024;
             var receiveBuffer = new byte[size];

             while (true)
             {
                 //chấp nhận kết nối từ client
                 var socket = listener.Accept();
                 Console.WriteLine($"Accept connection from {socket.RemoteEndPoint}");
                    
                 //nhận dữ liệu từ client
                 var length = socket.Receive(receiveBuffer);
                 var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                 Console.WriteLine($"Receive from client: {text}");
                 socket.Shutdown(SocketShutdown.Receive);
                 
                 
                 
                 //chuyển dữ liệu sang in hoa và gửi lại cho client
                 var result = text.ToUpper();//in hoa
                 var sendBuffer = Encoding.ASCII.GetBytes(result);
                 socket.Send(sendBuffer);
                    Console.WriteLine($"Send to client: {result}");
                    socket.Shutdown(SocketShutdown.Send);//đóng kết nối, không gửi dữ liệu nữa
                    
                    Console.WriteLine($"Close connection from {socket.RemoteEndPoint}");
                    Console.WriteLine("---------------------------------    ");
                    socket.Close();
                    
                    //Xoa bộ nhớ đệm
                    Array.Clear(receiveBuffer, 0, size);
                    

             }
        }
    }
}