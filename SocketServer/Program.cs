using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creazione della socket
            //Binding della socket(ip address e porta)
            var serverAddress = new IPEndPoint(IPAddress.Loopback, 8888);
            Socket socket = new Socket(serverAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(serverAddress);
            socket.Listen(128);

            //Accettazione della connessione(solo per TCP)
            while (true)
            {
                using (Socket clientSocket = await socket.AcceptAsync())
                {
                    //Generazione dello stream di comunicazione(solo per TCP)
                    using (NetworkStream networkStream = new NetworkStream(clientSocket))
                    {
                        while (true)
                        {
                            try
                            {
                                //Invio e ricezione dei dati
                                byte[] inMsgByte = new byte[100];
                                await networkStream.ReadAsync(inMsgByte, 0, inMsgByte.Length);
                                string inMsg = Encoding.ASCII.GetString(inMsgByte);

                                string outMsg = $"Echo di: {inMsg}";
                                byte[] outMsgByte = Encoding.ASCII.GetBytes(outMsg);
                                await networkStream.WriteAsync(outMsgByte, 0, outMsgByte.Length);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Connessione client chiusa");
                                break;
                            }
                        }
                    }
                }
            }

            //Chiusura socket
        }
    }
}
