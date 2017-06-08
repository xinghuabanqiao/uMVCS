using System;
using System.Collections.Generic;

namespace FrameWork.IOCDI
{
    /*
      作为一个颗粒度单位

      存放了 绑定的所有信息 key value type
      任何一个class type都可以获取对应的Binding 
      通过这个Binding 可以获取这个type绑定了什么 以及 什么形式singleton or noraml   
      
      同时维护了一个delegate callback 这个callbakc 后期有用  

    
    */
    public class Binding : IBinding
    {
        //反转
        public Binder.BindingResolver resolver;

        // 关于key vlaue mapping的时候 一key多value 都统一这样处理吧
        private BindingList KeyList;
        private BindingList ValueList;
        private BindingList NameList;
        public object key
        {
            get
            {
                return KeyList.value;
            }
        }

        public object value
        {
            get
            {
                return ValueList.value;
            }
        }

        public object name
        {
            get
            {
                return (NameList.value == null) ? BindingConst.NULLOID : NameList.value;
            }
        }

        public Binding(Binder.BindingResolver resolver)
        {
            this.resolver = resolver;

            KeyList   = new BindingList();
            ValueList = new BindingList();
            NameList = new BindingList();
        }

        // 默认构造 这个提供给派生类 构造的时候 base构造调用这个函数
        public Binding() : this(null)
        {
        }

        // add key 注册
        virtual public IBinding Bind<T>()
        {
            return Bind(typeof(T));
        }

        virtual public IBinding Bind(object o)
        {
            KeyList.Add(o);
            return this;
        }

        //To操作 add value 同时出发绑定callback 一般command 监听事件会有这个一个回调
        virtual public IBinding To<T>()
        {
            return To(typeof(T));
        }

        virtual public IBinding To(object o)
        {
            ValueList.Add(o);
            if (resolver != null)
                resolver(this);
            return this;
        }
    }
}
