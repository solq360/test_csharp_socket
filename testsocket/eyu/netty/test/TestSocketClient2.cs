 
using System.Collections;
using org.eyu.netty.socket;
using org.eyu.netty.util;
using System.Net.Sockets;
using System;
using System.Net;
using System.Collections.Generic;
using org.eyu.netty.util.pool;
//原生socket 异步处理
//http://www.gfsoso.com/?q=%E4%B8%80%E4%B8%AA.net%E5%AE%A2%E6%88%B7%E7%AB%AF%E9%80%9A%E8%AE%AF%E6%A1%86%E6%9E%B6%E7%9A%84%E8%AE%BE%E8%AE%A1
public class TestSocketClient2 {
    private Socket _socket;
    private List<SocketAsyncEventArgs> _clients = new List<SocketAsyncEventArgs>(500);
    public void initServer() {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint address = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9874);
        _socket.Bind(address);
        _socket.Listen(5000);

        SocketAsyncEventArgs _socketArgs = new SocketAsyncEventArgs();
        _socketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(serverCallCompleted);

        NextAccept(_socketArgs); 
    }

    private bool NextAccept(SocketAsyncEventArgs _socketArgs)
    {
        //SocketAsyncEventArgs 改为从池子获取
        _socketArgs.AcceptSocket = null;
        _socketArgs.BufferList = null;
        bool willRaiseEvent = _socket.AcceptAsync(_socketArgs);
        if (!willRaiseEvent)
        {
            serverCallCompleted(_socket,_socketArgs);
        }
        return willRaiseEvent;
    }
    private void serverCallCompleted(object sender, SocketAsyncEventArgs args)
    {
        System.Console.WriteLine("server :  LastOperation SOCKET :" + args.LastOperation);
        SocketAsyncEventArgs _socketArgs = new SocketAsyncEventArgs();

        if (args.SocketError == SocketError.Success) { 
            //保存socket
            _clients.Add(args);
            System.Console.WriteLine(" AcceptSocket :" + args.AcceptSocket.RemoteEndPoint.ToString());
            ByteBuffer bb = ByteBuffer.Allocate(50);
            byte[] sendBytes=System.Text.Encoding.UTF8.GetBytes("1234567890abcdefg");
            Console.WriteLine("send msg length : " + sendBytes.Length);
            bb.WriteInt(sendBytes.Length);
            bb.WriteBytes(sendBytes);
            Send(bb.ToArray(), _clients.ToArray());
        }
        NextAccept(args);
    }
 
    public void initClient() {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint address = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9874);
        SocketAsyncEventArgs _socketArgs = new SocketAsyncEventArgs();
        _socketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(callConnected);
        _socketArgs.RemoteEndPoint = address;        
        if (!_socket.ConnectAsync(_socketArgs))
        {
           callConnected(_socket, _socketArgs);
        }
    }


    public void Close()
    {
        _socket.Close();
    }

 
    public void Dispose()
    {
        if (_socket != null)
        {
            if (_socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();                
                _socket = null;               
            }
           
        } 
    }

    public void Send(byte[] msg, params  SocketAsyncEventArgs[] sockets)
    {
        System.Console.WriteLine(" Send msg :");
        foreach (SocketAsyncEventArgs s in sockets)
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.SetBuffer(msg, 0, msg.Length);
            //args.RemoteEndPoint = s.RemoteEndPoint;
            args.AcceptSocket = s.AcceptSocket;

            args.Completed += new EventHandler<SocketAsyncEventArgs>(this.callSended);
            System.Console.WriteLine(" AcceptSocket :" + s.AcceptSocket.RemoteEndPoint.ToString());

            args.AcceptSocket.SendAsync(args);
        }
       
    }
     

    ///客户端处理///////////////////
    private void callConnected(object sender, SocketAsyncEventArgs args)
    {
        System.Console.WriteLine("client LastOperation SOCKET :" + args.LastOperation);
        args.Completed -= callConnected;
        if (args.SocketError == SocketError.Success)
        {
            System.Console.WriteLine("Success Connected");
            //TODO
            SocketAsyncEventArgs receivArgs = SocketAsyncEventArgsMetadata.createSocketAsyncEventArgs();
            receivArgs.Completed += new EventHandler<SocketAsyncEventArgs>(callReceived);
            //receivArgs.AcceptSocket = args.AcceptSocket;
            byte[] buff = new byte[8];
            receivArgs.SetBuffer(buff, 0, buff.Length);
            
            if (!_socket.ReceiveAsync(receivArgs))
            {
                callReceived(sender, receivArgs);
            }
        }
        else
        {
            System.Console.WriteLine("error SOCKET");
        }
    }


    private void callCompleted(object sender, SocketAsyncEventArgs args)
    {
        System.Console.WriteLine("client LastOperation SOCKET :" + args.LastOperation);
        switch (args.LastOperation)
        {
            case SocketAsyncOperation.Send:
                System.Console.WriteLine("Send SOCKET");
                break;
            case SocketAsyncOperation.Receive:
                System.Console.WriteLine("Receive SOCKET");
                break;
        }
    }



    private void callReceived(object sender, SocketAsyncEventArgs args)
    {
        var socket = sender as Socket;
        var bb = args.UserToken as ByteBuffer; 
        if (args.SocketError == SocketError.Success)
        {      
          bb.WriteBytes(args.Buffer, args.Offset, args.BytesTransferred);
          bb.MarkReaderIndex();
          int headLength = bb.ReadInt();
          int msgLength = headLength;//长度已-4          
          int readByteLength = bb.ReadableBytes();
          //解决半包
          if (msgLength > readByteLength)
          {
              //还原读取索引记录
              bb.ResetReaderIndex(); 
          }
          else {
              //是否去掉包头
              byte[] filthyBytes= bb.ToArray(); 
              System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(filthyBytes));
              
              //解决粘包剩余
              bb.Clear();
              int useLength = filthyBytes.Length;
              int lastOffSetLength = filthyBytes.Length - useLength;
              if (lastOffSetLength > 0) {
                  bb.WriteBytes(filthyBytes, lastOffSetLength, filthyBytes.Length);
              }              
          }
        }
        else {
            //丢去byte处理
            System.Console.WriteLine("error callReceived");
        }
        _socket.ReceiveAsync(args);
    }
    private void callSended(object sender, SocketAsyncEventArgs args)
    {
        if (args.SocketError == SocketError.Success)
        {
            System.Console.WriteLine("Success callReceived");
        }
        else
        {
            System.Console.WriteLine("error callSended");
        }
        //回收 SocketAsyncEventArgs
        //_asyncSendArgsPool.Put(args);
    }
  
} 
