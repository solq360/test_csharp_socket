namespace org.eyu.netty.socket{
	public interface IoSessint   {
        /**获取绑定ctx*/
        object getBindCtx();

        int write(byte[] bytes);


	}
}
