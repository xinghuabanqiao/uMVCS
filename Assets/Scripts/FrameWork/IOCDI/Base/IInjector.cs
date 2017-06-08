using System;

namespace FrameWork.IOCDI
{
    // 提供注入 实例化操作
    public interface IInjector
    {
        object Instantiate(IInjectorBinding binding);

        object Inject(object target);

        IInjectorFactory factory { get; set; }
        IInjectorBinder binder { get; set; }
        IReflectorBinder reflector { get; set; }

    }
}
