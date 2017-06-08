using System;

namespace FrameWork.IOCDI
{
    /*
     基类绑定接口
     bind
     to  
     getBinding
     unbind 
     
     内部调用binding实现add key add value   
     所有返回都是IBinding
     */
    public interface IBinder
    {
        IBinding Bind<T>();
        IBinding Bind(object value);

        void Unbind<T>();
        void Unbind(object key);

        /// Remove a binding based on the provided Key / Name combo
        void Unbind(object key, object name);

        IBinding GetBinding<T>();

        IBinding GetBinding(object key);
    }
}
