using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.eyu.netty.socket;
using System.Threading;
using org.eyu.netty.util.pool;

namespace testsocket.eyu.netty.test
{
    class TestByteBuff
    {
        private int count = 200;
        public void test() {
            ByteBuffer bb = ByteBuffer.Allocate(40);
            bb.WriteByte(1);
            bb.WriteByte(2);
            bb.WriteByte(3);
            bb.WriteByte(4);
            bb.WriteString("aersrs");
            System.Console.WriteLine(bb.ReadByte());
            System.Console.WriteLine(bb.ReadByte());
            System.Console.WriteLine(bb.ReadByte());
            System.Console.WriteLine(bb.ReadByte());
            System.Console.WriteLine(bb.ReadString());
        }
    }
}
