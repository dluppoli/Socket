using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creazione della socket
            //Collegamento al server(ip address e porta)
            var serverAddress = new IPEndPoint(IPAddress.Loopback, 8888);
            Socket socket = new Socket(serverAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            await socket.ConnectAsync(serverAddress);

            //Generazione dello stream di comunicazione(solo per TCP)
            NetworkStream networkStream = new NetworkStream(socket);

            //Invio e ricezione dei dati
            Console.WriteLine("Inserire il messaggio:");
            string msg = Console.ReadLine();
            
            byte[] msgByte = Encoding.ASCII.GetBytes(msg);
            await networkStream.WriteAsync(msgByte,0, msgByte.Length);

            byte[] inMsgByte = new byte[100];
            await networkStream.ReadAsync(inMsgByte, 0, inMsgByte.Length);
            string inMsg = Encoding.ASCII.GetString(inMsgByte);

            Console.WriteLine($"Il server ha risposto con: {inMsg}");

            //Chiusura socket
            networkStream.Close();
            socket.Dispose();

            Console.ReadLine();
        }
    }
}
