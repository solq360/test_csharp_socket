using System.Net.Sockets;
using System.Collections.Generic;
using System;


namespace org.eyu.netty.util.pool
{
    public abstract class Pool<T> : IPool<T> where T : IPoolElement{ 

        //已使用记录
        private List<Int32> usedRecord;
        //未使用记录
        private List<Int32> unUsedRecord;
        //池子
        private List<T> pool;
        //池子最大容量
        protected int capacity;
        //是否动态扩展容量
       // private bool dynamic = false;

        /**池子初始化*/
        protected void init() {
            this.pool = new List<T>(this.capacity);
            this.usedRecord = new List<Int32>(this.capacity);
            this.unUsedRecord = new List<Int32>(this.capacity);
            for (int i = 0; i < this.capacity; i++) {
                this.unUsedRecord.Add(i);
                T e = OnCreate(i);
                OnPut(e);
                this.pool.Add(e);
            }
        }
        /**子类重载处理**/
        public abstract T OnCreate(int index);
        //protected void OnInitialize(T e){}
        public void OnClear(T e) { }
        public void OnPut(T e) { }
        public void OnPop(T e) { }
  

        ///////////////////公开方法////////////////////////

        /**获取可使用 元素 */
        public T Pop()
        {
            int index = 0;
            lock(this){
                if (GetUsedCount() <= 0)
                {
                    extCapacity();
                }
                index = this.unUsedRecord[0];
                this.unUsedRecord.RemoveAt(0);
                this.usedRecord.Add(index);
                T e = this.pool[index];
                OnPop(e);
                return e;
            }
        }
        /**回收 元素 */
        public void Put(T args)
        {
            int index = 0;
            lock (this)
            {
                index = args.GetIndex();
                this.unUsedRecord.Add(index);
                this.usedRecord.Remove(index);
                OnClear(args);
            }
        }

        /**获取可使用数量**/
        public int GetUsedCount()
        {
            return this.capacity - this.usedRecord.Count;
        }

        /** 扩展容量   */
        private void extCapacity()
        {
            int minNewCapacity = 200;
            int newCapacity = Math.Min(this.capacity, minNewCapacity);

            //每次以minNewCapacity倍数扩展
            if (newCapacity > minNewCapacity)
            {
                newCapacity += minNewCapacity;
            }
            else {
                //以自身双倍扩展空间
                newCapacity = 64;
                while (newCapacity < minNewCapacity)
                {
                    newCapacity <<= 1;
                }
            }


            for (int i = this.capacity; i < newCapacity; i++) {
                this.unUsedRecord.Add(i);
                T e = OnCreate(i);
                OnPut(e);
                this.pool.Add(e);
            }

            this.capacity = newCapacity;
        }


        //getter

        public int GetCapacity() {
            return this.capacity;
        }


    }
}
