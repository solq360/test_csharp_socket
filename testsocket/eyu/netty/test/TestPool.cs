using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.eyu.netty.socket;
using System.Threading;
using org.eyu.netty.util.pool;

namespace testsocket.eyu.netty.test
{
    class TestPool
    {
        private int count = 200;
        public void test() {
            SocketAsyncEventArgsPool pool = SocketAsyncEventArgsPool.valueOf(4);

            for (int i = 0; i < count; i++) {
                Thread th = new Thread(pop);
                th.Start(pool);
            }  
        
        }

        private void pop(object msg)
        {
            ((SocketAsyncEventArgsPool)msg).Pop();
        }

    

    }
}
