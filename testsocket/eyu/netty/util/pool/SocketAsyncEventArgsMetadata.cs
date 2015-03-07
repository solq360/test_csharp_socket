using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace org.eyu.netty.util.pool
{
   public class SocketAsyncEventArgsMetadata : IPoolElement
    {
       /**记录索引**/
       private int index;
       /**接收 SocketAsyncEventArgs**/
       private SocketAsyncEventArgs receiveArgs;
       /**发送 SocketAsyncEventArgs**/
       private SocketAsyncEventArgs sendArgs;

       public static SocketAsyncEventArgsMetadata valueOf(int index) {
          
           SocketAsyncEventArgsMetadata result = new SocketAsyncEventArgsMetadata();
           result.index = index; 
           result.receiveArgs = createSocketAsyncEventArgs();
           result.sendArgs = createSocketAsyncEventArgs();            
           return result;
       }

       public static SocketAsyncEventArgs createSocketAsyncEventArgs()
       {
           int initByteLeng = 4;
           SocketAsyncEventArgs result = new SocketAsyncEventArgs();
           //绑定ByteBuffer 用来处理粘包 
           ByteBuffer bb= ByteBuffer.Allocate(initByteLeng);
           result.UserToken = bb;            
           return result;
       }

       public int GetIndex()
       {
           return this.index;
       } 
    }
}
