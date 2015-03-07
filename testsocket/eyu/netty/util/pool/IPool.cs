using System.Net.Sockets;
using System.Collections.Generic;
using System;


namespace org.eyu.netty.util.pool
{
    public interface  IPool<T> { 
  
        T OnCreate(int index);

        //void OnInitialize(T e);        
        void OnPop(T e);
        void OnPut(T e);
        void OnClear(T e);
    }
}
