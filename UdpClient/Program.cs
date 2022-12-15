using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdpClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creazione della socket
            System.Net.Sockets.UdpClient socket = new System.Net.Sockets.UdpClient();

            //Collegamento al server(ip address e porta)
            var serverAddress = new IPEndPoint(IPAddress.Loopback, 8889);
            socket.Connect(serverAddress);

            //Invio e ricezione dei dati
            Console.WriteLine("Inserire il messaggio:");
            string msg = Console.ReadLine();

            byte[] msgByte = Encoding.ASCII.GetBytes(msg);
            await socket.SendAsync(msgByte, msgByte.Length);

            byte[] inMsgByte = new byte[100];
            var result = await socket.ReceiveAsync();
            inMsgByte = result.Buffer;
            string inMsg = Encoding.ASCII.GetString(inMsgByte);

            Console.WriteLine($"Il server ha risposto con: {inMsg}");

            //Chiusura socket
            socket.Dispose();

            Console.ReadLine();
        }
    }
}
