using System;

namespace FrameWork.IOCDI
{
    // relector信息 也是需要绑定在keyType上的 所以这里也可以 继承bind
    // 但是只是bind和to操作 toRefectroValue操作 类似的操作
    public interface IReflectorBinder
    {
        ReflectedClass Get(Type type);

        ReflectedClass Get<T>();
    }
}
