using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.eyu.netty.core
{
    public interface Coder
    {
 
        byte[] encode(Object obj );
         
        object decode(byte[] bytes);
    }
}
