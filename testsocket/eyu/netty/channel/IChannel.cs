using System.Collections;

/**
 * @author solq 管道对象
 * */
namespace org.eyu.netty.channel{
	public interface IChannel {
	    IChannel flush();
	    IChannel read();
		IChannelConfig getChannelConfig();
	}
}
