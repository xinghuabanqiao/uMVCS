using System;
using System.Collections.Generic;


namespace FrameWork.IOCDI
{
    public interface IInjectorBinder : IInstance
    {
        IInjectorBinding Bind<T>();
        IInjectorBinding Bind(Type key);

        void Unbind<T>();
        void Unbind(object key);

        // 获取实例
        object GetInstance(Type key, object name);
        T GetInstance<T>(object name); 

        IInjectorBinding GetBinding<T>();
        IInjectorBinding GetBinding(object key);
        IInjectorBinding GetBinding(object key, object name);


    }
}
