using System;

namespace FrameWork.IOCDI
{
    // 实例化
    public interface IInjectorFactory
    {
        object Get(IInjectorBinding binding);

        object Get(IInjectorBinding binding, object[] args);
    }
}
