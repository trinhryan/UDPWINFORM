using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server_nch_Client
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
                 
                 
                 //Client gửi sang Server theo các cú pháp “now”, “day”, “month”. Server nhận và phản hồi theo thứ tự hiển thị giờ hiện tại, hiển thị ngày hiện tại, hiển thị tháng hiện tại. 
                
                 string messageTraVe; // khởi tạo biến message trả về

                 if (text.ToLower().Trim().Equals("now"))
                 {
                     messageTraVe = DateTime.Now.ToString("hh:mm dd/MM/yyyy"); // lấy thời gian hiện tại
                 }
                 else if (text.ToLower().Trim().Equals("day"))
                 {
                     messageTraVe = DateTime.Now.ToString("dd"); // lấy ngày hiện tại
                 }
                 else if (text.ToLower().Trim() == "month")
                 {
                     messageTraVe = DateTime.Now.ToString("MM"); // lấy tháng hiện tại
                 }
                 // else if (text.ToLower().Trim().StartsWith("name:"))//Từ Client nhập tên gửi sang Server, Client phản hồi Server theo cú pháp Xin chào + name
                 // {
                 //     var name = text.Substring(5).Trim(); // lấy tên từ text
                 //     
                 //     messageTraVe = $"Xin chào {name}"; // tạo message trả về
                 // }
                 else if(text.ToLower().Trim().Contains("name")) //Từ Client nhập tên gửi sang Server, Client phản hồi Server theo cú pháp Xin chào + name
                 {
                     var name = text.Replace("name", "").Trim(); // lấy tên từ message
                     messageTraVe = $"Xin chào  {name}"; // tạo message trả về
                 }
                 else
                 {
                     messageTraVe = "Invalid command"; // lệnh không hợp lệ
                 }
                 Console.WriteLine($"response from Server: {messageTraVe}");//in ra màn hình
                 // Gửi phản hồi cho client
                 var sendBuffer = Encoding.ASCII.GetBytes(messageTraVe);
                 socket.Send(sendBuffer);
                 Console.WriteLine($"Send to client: {messageTraVe}");
                 socket.Shutdown(SocketShutdown.Send); // đóng kết nối, không gửi dữ liệu nữa

                 Console.WriteLine($"Close connection from {socket.RemoteEndPoint}");
                 Console.WriteLine("--------------------------------");
                
                 //server gửi tin nhắn trả lời lại client
                 
                 socket.Close();
                    
                 //xoa bo nho dem
                 Array.Clear(receiveBuffer, 0, size);
                    
               
                 
                 //chuyển dữ liệu sang in hoa và gửi lại cho client
                 // var result = text.ToUpper();//in hoa
                 // var sendBuffer = Encoding.ASCII.GetBytes(result);
                 // socket.Send(sendBuffer);
                 //    Console.WriteLine($"Send to client: {result}");
                 //    socket.Shutdown(SocketShutdown.Send);//đóng kết nối, không gửi dữ liệu nữar
                 //    
                 //    Console.WriteLine($"Close connection from {socket.RemoteEndPoint}");
                 //    Console.WriteLine("---------------------------------    ");
                 //    socket.Close();
                 //    
                 //    //Xoa bộ nhớ đệm
                 //    Array.Clear(receiveBuffer, 0, size);
                 //    
                    // //trả lời lại client
                    // Console.ForegroundColor = ConsoleColor.Blue;//đổi màu chữ
                    // Console.Write("#Text>>>");
                    // Console.ResetColor();
                    // var Text = Console.ReadLine();
                    //
                    // //gui du lieu toi Client
                    // var sendBuffer1 = Encoding.ASCII.GetBytes(Text);
                    // socket.Send(sendBuffer1);
                    // socket.Shutdown(SocketShutdown.Send);
                    //
                    // //nhan du lieu tu Client
                    // var length1 = socket.Receive(receiveBuffer);
                    // var result1 = Encoding.ASCII.GetString(receiveBuffer, 0, length1);
                    // Console.WriteLine($"response from Server <<<{result1}");
                    // Console.WriteLine("---------------------------------    ");
                    // socket.Shutdown(SocketShutdown.Receive);
                    //
                    // socket.Close();
                    //
                    // //xoa bo nho dem
                    // Array.Clear(receiveBuffer, 0, size);
             }
        }
    }
}