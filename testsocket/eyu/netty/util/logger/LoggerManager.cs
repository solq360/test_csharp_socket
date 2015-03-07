namespace org.eyu.netty.util
{
	public abstract class LoggerManager
	{
		//public static ILogger DEFAULT_UNITY_LOGGER = new UnityLogger ("DEFAULT_UNITY_LOGGER");
		public const bool UNITY=true;

		public static ILogger buildUnityLogger (string name)
		{
            return null;
			//return new UnityLogger (name);			
		}
		
		public static ILogger buildLogger (string name)
		{
            return null;
            /***
             if(UNITY){
                return new UnityLogger (name);
            }else{
                return null;
            }
             */
        }
		
	}
}
