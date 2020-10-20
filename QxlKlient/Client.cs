using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace QxlKlient
{
    class Client
    {
        private static NetworkStream _networkStream;
        private static BinaryReader _reader;
        private static BinaryWriter _writer;

        public Client()
        {

            try
            {
                var tcpClient = new TcpClient();
                Console.WriteLine("Connecting");
                tcpClient.Connect("127.0.0.1", 8001);
                Console.WriteLine("Connected");
                _networkStream = tcpClient.GetStream();
                _reader = new BinaryReader(_networkStream);
                _writer = new BinaryWriter(_networkStream);

                var thread = new Thread(HandleInput);
                thread.Start();
                var s = "s";
                while(s != "")
                {
                    s = Console.ReadLine();
                    _writer.Write(s);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        private void HandleInput()
        {
            while(true)
            {
                var txt = _reader.ReadString();
                Console.WriteLine(txt);
            }
        }
    }
}
