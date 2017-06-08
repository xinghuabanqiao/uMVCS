using System;
using System.Collections.Generic;


namespace FrameWork.IOCDI
{
    //实例化接口
    public interface IInstance
    {
        T GetInstance<T>();
        object GetInstance(Type key);
    }
}
