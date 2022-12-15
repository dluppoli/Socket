using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creazione della socket
            System.Net.Sockets.TcpClient socket = new System.Net.Sockets.TcpClient();
            
            //Collegamento al server(ip address e porta)
            await socket.ConnectAsync(IPAddress.Loopback, 8888);

            //Generazione dello stream di comunicazione(solo per TCP)
            NetworkStream networkStream = socket.GetStream();

            //Invio e ricezione dei dati
            Console.WriteLine("Inserire il messaggio:");
            string msg = Console.ReadLine();

            byte[] msgByte = Encoding.ASCII.GetBytes(msg);
            await networkStream.WriteAsync(msgByte, 0, msgByte.Length);

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
