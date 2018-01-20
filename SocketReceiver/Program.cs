using System;
using System.Net;
using System.Net.Sockets;

namespace SocketReceiver
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Awaiting connection");
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            var ipAddress = GetIpAddress();
            Console.WriteLine($"IP Address: {ipAddress.ToString()}");

            try
            {
                socket.Bind(new IPEndPoint(ipAddress, 33334));
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            socket.Listen(32);
        }

        public static IPAddress GetIpAddress()
        {
            var addresses = Dns.GetHostAddressesAsync(Dns.GetHostName()).Result;
            foreach (var address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address;
                }
            }

            return null;
        }
    }
}