using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace TcpServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Creazione della socket
            //Binding della socket(ip address e porta)
            var serverAddress = new IPEndPoint(IPAddress.Loopback, 8888);
            TcpListener socket = new TcpListener(serverAddress);

            socket.Start();

            //Accettazione della connessione(solo per TCP)
            while (true)
            {
                using (TcpClient clientSocket = await socket.AcceptTcpClientAsync())
                {
                    //Generazione dello stream di comunicazione(solo per TCP)
                    using (NetworkStream networkStream = clientSocket.GetStream())
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
                                Logger logger = LogManager.GetCurrentClassLogger();

                                logger.Debug("Connessione chiusa");

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
