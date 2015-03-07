using System;
using IocpServer;
//http://www.xuebuyuan.com/194063.html
namespace testsocket
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IoServer server= new IoServer(5,1024);
			server.Start(5999);
			
			SocketClient client=new SocketClient(SocketConfig.valueOf("127.0.0.1",8889));
		}
	}
	
	class NettySocket{
		public void init(){
			
		}
		
	}
}
