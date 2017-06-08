using System;

namespace FrameWork.IOCDI
{
    public interface IBinding
    {
        IBinding Bind<T>();
        IBinding Bind(object key);
        IBinding To<T>();
        IBinding To(object value);

        object key { get; }
        object name { get; }
        object value { get; }
    }
}
