using org.eyu.netty.socket;
using org.eyu.netty.util;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading;
/**
 * 例子 : http://www.xuanyusong.com/archives/1948
 * http://www.cnblogs.com/zou90512/p/3907303.html
 * netty http://m.blog.csdn.net/blog/wilsonke/24721057
 * 
 * mac http://blog.csdn.net/kesalin/article/details/6875802
 * */
namespace org.eyu.netty.socket{
public class SocketClient : ISocketClient {
		private static ILogger logger = LoggerManager.buildLogger("socket client");
		/**客户端配置*/
		private SocketConfig socketConfig;

		/**socket对象*/
		private Socket socket;
		//private bool ReceiveFlag = true;
		//private bool asyncConnect=true;
		private  byte[] readBuf = new byte[1024];

		SocketClient(){}
		
		public SocketClient(SocketConfig config){
			this.socketConfig = config;
		}
		
		public void start ()
		{
			//连接参数
            IPAddress ip = IPAddress.Parse(socketConfig.Host);  
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
			//socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
			
			//连接处理
            try  
            {  			 
				//Security.PrefetchSocketPolicy( socketConfig.Host, socketConfig.Port );

				IPEndPoint ipEndPoint=new IPEndPoint(ip, socketConfig.Port);
	 
					IAsyncResult asyncResult=socket.BeginConnect(ipEndPoint,new AsyncCallback (connectAfter),socket);	
// 					bool success = asyncResult.AsyncWaitHandle.WaitOne( 500,true );
//					if(!success){
//						logger.debug("异步连接服务器失败!");
//						return;
//					}
             
               		
            } catch(Exception e) 
            {  
               logger.debug("连接服务器失败!" + e.ToString());  
                return;  
            }  			
 		}
		
		
		  
	   public void startReceive(){
			if(!isConnected()){
				return;
			}
            socket.BeginReceive(readBuf, 0, readBuf.Length, SocketFlags.None, new AsyncCallback(endReceive), socket);
	    }
		 void endReceive(IAsyncResult iar) //接收数据
	    {
			if(!isConnected()){
				return;
			}	
	      //  ReceiveFlag = true;
	        Socket remote = (Socket)iar.AsyncState;
	        int recv = remote.EndReceive(iar);
	        if (recv > 0)
	        {
	            string stringData = Encoding.UTF8.GetString(readBuf, 0, recv);
	            stringData += "\n" + "接收服务器数据:+++++++++++++++" + stringData;
				logger.debug(stringData);
	        }
	
	    }
		
		private void connectAfter(IAsyncResult asyncConnect){
			if(!isConnected()){
				return;
			}			
			logger.debug("连接服务器成功 "  );
            socket.EndConnect(asyncConnect);
            /**
			if(AsyncConnect){
				
				//Thread thread = new Thread(new ThreadStart(receiveData));
	       	 	//thread.IsBackground = true;
	        	//thread.Start();				
			}else{
 				byte[] result = new byte[1024];
	            int receiveLength = socket.Receive(result);  
	            logger.debug("接收服务器消息：{0}",Encoding.ASCII.GetString(result,0,receiveLength));  
	            for (int i = 0; i < 10; i++)  
	            {  
	                try  
	                {  
	                    string sendMessage = "client send Message Hellp_test_";  
	                    socket.Send(Encoding.ASCII.GetBytes(sendMessage));  
	                    logger.debug("向服务器发送消息：{0}" + sendMessage);  
	                }  
	                catch  
	                {  
	                    socket.Shutdown(SocketShutdown.Both);  
	                    socket.Close();  
	                    break;  
	                }  
	            }  */
		 
		}
 
	 
	private void receiveData()
    {
		//在这个线程中接受服务器返回的数据
	    while (true)
        { 
 
			if(!isConnected())
			{
				//与服务器断开连接跳出循环
				logger.debug("Failed to clientSocket server.");
				this.close();
				break;
			}
            try
            {
				logger.debug("接收数据");
                byte[] bytes = new byte[4096];
                //Receive方法中会一直等待服务端回发消息
 				int i = socket.Receive(bytes);
				if(i <= 0)
				{
					this.close();
					break;
				}	
 
				//这里条件可根据你的情况来判断。
				//因为我目前的项目先要监测包头长度，
				//我的包头长度是2，所以我这里有一个判断
				/**
				if(bytes.Length > 2)
				{
					SplitPackage(bytes,0);
				}else
				{
					logger.debug("length is not  >  2");
				}*/
 
             }
             catch (Exception e)
             {
				logger.debug("Failed to clientSocket error." + e);
                this.close();
				break;
             }
        }
    }	
		
		public bool isConnected ()
		{
			return this.socket!=null && this.socket.Connected;
		}
 		public void close ()
		{
			if(!isConnected()){
				return;
			}
			logger.debug("关闭 socket : " + this.socketConfig.ToString());
			this.socket.Close();
		}

		public SocketConfig GetSocketConfig()
		{
			 return this.socketConfig;
		}
 
}
	
}