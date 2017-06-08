using System;
using System.Collections.Generic;
using System.Reflection;

namespace FrameWork.IOCDI
{
    //这里默认使用属性注入
    /*public interface IReflectedClass
    {
        KeyValuePair<Type, PropertyInfo>[] Setters { get; set; }
        object[] SetterNames { get; set; }
    }*/

    public class ReflectedClass
    {
        public ConstructorInfo Constructor { get; set; }
        public Type[] ConstructorParameters { get; set; }
        public KeyValuePair<Type, PropertyInfo>[] Setters { get; set; }
        public object[] SetterNames { get; set; }
    }
}
