using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameWork.IOCDI
{
    public interface IInjectorBinding: IBinding
    {
        new IInjectorBinding Bind<T>();
        new IInjectorBinding Bind(object key);

        new IInjectorBinding To<T>();
        new IInjectorBinding To(object o);

        //是否注入
        // false表示已经注入了 ture表示没有注入
        bool toInject { get; }
        IInjectorBinding ToInject(bool value);

        // tyep= singleton / value / 
        InjectionBindingType type { get; set; }

        new object key { get; }
        new object name { get; }
        new object value { get; }
    }
}
