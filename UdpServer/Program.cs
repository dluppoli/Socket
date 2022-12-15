using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdpServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creazione della socket
            //Binding della socket(ip address e porta)
            var serverAddress = new IPEndPoint(IPAddress.Loopback, 8889);
            UdpClient socket = new UdpClient(serverAddress);

  
            while (true)
            {
                //Invio e ricezione dei dati
                byte[] inMsgByte = new byte[100];
                var request = await socket.ReceiveAsync();
                inMsgByte = request.Buffer;
                string inMsg = Encoding.ASCII.GetString(inMsgByte);

                string outMsg = $"Echo di: {inMsg}";
                byte[] outMsgByte = Encoding.ASCII.GetBytes(outMsg);
                await socket.SendAsync(outMsgByte, outMsgByte.Length,request.RemoteEndPoint);
            }

            //Chiusura socket
        }
    }
}
