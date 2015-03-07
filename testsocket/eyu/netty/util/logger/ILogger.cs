namespace org.eyu.netty.util
{
	public interface ILogger
	{
		string getName();
		
		void debug (object obj);

		void error (object obj);

		void info (object obj);

		void warn (object obj);
		
		void debug (string format, params object[] objs);

		void error (string format, params object[] objs);

		void info (string format, params object[] objs);

		void warn (string format, params object[] objs);
 
	}
}
