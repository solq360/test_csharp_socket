using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.eyu.netty.util.pool
{
    public class SocketAsyncEventArgsPool  : Pool<SocketAsyncEventArgsMetadata> 
    {
       override public SocketAsyncEventArgsMetadata OnCreate(int index)
       {
            return SocketAsyncEventArgsMetadata.valueOf(index);
       }

        /**构建方法*/
        public static SocketAsyncEventArgsPool valueOf(int maxCapacity)
        {
            SocketAsyncEventArgsPool result = new SocketAsyncEventArgsPool();
            result.capacity = maxCapacity;
            result.init();
            return result;
        }
    }
}
