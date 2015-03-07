namespace org.eyu.netty.socket{
	public interface IoSessin   {
		/**
		 *	获取SocketConfig 配置
		 * 
		 * */
		SocketConfig GetSocketConfig();
		/**
		 *	启动服务
		 * 
		 * */
		void start();
		/**
		 *	关闭服务 
		 * */
		void close();
        /**
         *	是否已连接
         * */
		bool isConnected();	
	}
}
