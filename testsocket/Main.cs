using System;
using IocpServer;
using org.eyu.netty.socket;
using testsocket.eyu.netty.test;
//http://www.xuebuyuan.com/194063.html

//http://www.codeproject.com/Articles/83102/C-SocketAsyncEventArgs-High-Performance-Socket-Cod
//http://www.supersocket.net/

//
namespace testsocket
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//IoServer server= new IoServer(5,1024);
			//server.Start(5999);
			
			//SocketClient client=new SocketClient(SocketConfig.valueOf("127.0.0.1",8889));
           // client.start();

          //  TestByteBuff testPool = new TestByteBuff();
           // testPool.test();

            //new TestByteBuff().test();
           // byte[] value = System.Text.Encoding.BigEndianUnicode.GetBytes("123");
           // string str = System.Text.Encoding.BigEndianUnicode.GetString(value);
           // System.Console.WriteLine(" str :" + str);


            new TestSocketClient2().initServer();
            new TestSocketClient2().initClient();

            System.Console.ReadLine();
		}
	}
	
 
}
