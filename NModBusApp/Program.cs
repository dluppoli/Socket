using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NModbus;
using NModbus.Serial;

namespace NModBusApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using( SerialPort port = new SerialPort("COM2") )
            {
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.Even;
                port.StopBits = StopBits.One;

                port.Open();

                var factory = new ModbusFactory();

                var bus = factory.CreateRtuMaster(port);

                ushort[] lettura = await bus.ReadHoldingRegistersAsync(1, 3, 1);

                short temp = (short)lettura[0];
                bool accendi = temp < 200 ? true : false;

                await bus.WriteSingleCoilAsync(1, 5, accendi);

                Console.ReadLine();
            }
        }
    }
}
