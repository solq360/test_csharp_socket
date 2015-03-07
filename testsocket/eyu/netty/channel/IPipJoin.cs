using System.Collections;
/**
 * @author solq 管道拼接,channels 管理
 * */
namespace org.eyu.netty.channel{
	public interface IPipeLine   {
		IChannel[] getChannels();
		void write(object buff);
		void read(object buff);
	}
}

