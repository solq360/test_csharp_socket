/**using UnityEngine;
namespace org.eyu.netty.util
{
	public class UnityLogger : ILogger
	{
		private string name;
		UnityLogger(){}
		
		
		public UnityLogger(string name){
			this.name=name;	 
		}
		
		private static object convert(object obj){
			if(obj is  Object){
				return obj;
			}
			return obj.ToString();
		}
		
		public void debug (object obj)
		{
			Debug.Log(convert(obj));
		}

		public void error (object obj)
		{
			Debug.LogError(convert(obj));
		}

		public void info (object obj)
		{
			Debug.Log(convert(obj));
		}

		public void warn (object obj)
		{
			Debug.LogWarning(convert(obj));
		}

		public void debug (string format, params object[] objs)
		{
			Debug.Log(format);
		}

		public void error (string format, params object[] objs)
		{
			Debug.LogError(format);
		}

		public void info (string format, params object[] objs)
		{
			Debug.Log(format);
		}

		public void warn (string format, params object[] objs)
		{
			Debug.LogWarning(format);
		}
		public string getName ()
		{
			return this.name;
		}
		 
	}
}
*/
