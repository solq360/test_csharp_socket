namespace org.eyu.netty.socket
{
	public class SocketConfig
	{
		private string host;
		private int port;
		
		public string Host {
			get {
				return this.host;
			}
			set {
				host = value;
			}
		}

		public int Port {
			get {
				return this.port;
			}
			set {
				port = value;
			}
		}

		public static SocketConfig valueOf (string host, int port)
		{
			SocketConfig result = new SocketConfig ();
			result.Host = host;
			result.Port = port;
			return result;
		}
		
	}
}
