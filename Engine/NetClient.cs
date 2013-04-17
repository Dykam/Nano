using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

/*
 * How to use the NetClient:
 * 
 * The NetClient uses a deligate method.
 * This is the only parameter of the constructor.
 * 
 * Example:
 * 
 * private void HandlePacketFromClient(Packet receivedPacket)
 * {
 *      //Handle the packet
 *      receivedPacket.DoSomethingFabulous();
 * }
 * 
 * NetClient myClient = new NetClient( HandlePacketFromClient );
 * myClient.Connect("<IPADRESS>", int port);
 * 
 * //the method given in the constructor will be called whenever a packet arrives
 */

namespace Engine
{
    public class NetClient
    {
        IPEndPoint serverLocation;
        Socket clientSocket;
        bool allowSending = false;
        Action<Packet> dataParser;
        Action onConnectMethod;

        //create the netclient and set the deligate
        public NetClient(Action<Packet> dataParse, Action OnConnectMethod)
        {
            this.dataParser = dataParse;
            this.onConnectMethod = OnConnectMethod;
        }

        //connect to an IP + Port
        public void Connect(string IP, int port)
        {
            try
            {
                serverLocation = new IPEndPoint(IPAddress.Parse(IP), port);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //start actual connecting
                clientSocket.BeginConnect(serverLocation, new AsyncCallback(OnConnect), clientSocket);
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine("\n\nCould not connect to the server\n\n\n" + E.ToString());
            }
        }


        public void Disconnect()
        {
            try
            {
                if(clientSocket != null && clientSocket.Connected)
                    clientSocket.Disconnect(true);
                allowSending = false;
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.ToString());
            }
        }

        //we have a connection to the server!
        private void OnConnect(IAsyncResult Result)
        {
            try
            {
                clientSocket = (Socket)Result.AsyncState;
                clientSocket.EndConnect(Result);
                //now we can start sending
                allowSending = true;
                onConnectMethod();
                //and receiving
                StartReceiving();
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine("\n\nCould not connect to the server\n\n\n" + E.ToString());
            }
        }

        //open up our "mailbox" to allow packets to flow in
        private void StartReceiving()
        {
            byte[] data = new byte[128];
            clientSocket.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(OnReceiveData), (object)data);
        }

        //when data comes in, handle it using the deligate
        private void OnReceiveData(IAsyncResult Result)
        {
            try
            {
                clientSocket.EndReceive(Result);
                byte[] packetData = (byte[])Result.AsyncState;

                Packet receivedPacket = new Packet(packetData);
                //here comes the important part, when the data is collected, call the method that was parsed in the constructor!
                dataParser(receivedPacket);
                //open mailbox again
                if (clientSocket != null && clientSocket.Connected)
                    StartReceiving();
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.ToString());
            }
        }
        
        //send a packet
        public void Send(Packet sendPacket)
        {
            if(allowSending)
                clientSocket.Send(sendPacket.Retrieve());
        }
    }
}
