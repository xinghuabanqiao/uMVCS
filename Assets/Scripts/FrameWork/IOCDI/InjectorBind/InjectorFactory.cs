using System;

namespace FrameWork.IOCDI
{
    public class InjectorFactory : IInjectorFactory
    {
        public InjectorFactory()
        {
        }

        public object Get(IInjectorBinding binding)
        {
            return Get(binding, null);
        }

        public object Get(IInjectorBinding binding, object[] args)
        {
            object retv = null;
            InjectionBindingType type = binding.type;
            switch (type)
            {
                case InjectionBindingType.SINGLETON:
                    //return singletonOf(binding, args); // 单利先不忙处理
                case InjectionBindingType.VALUE:
                    return binding.value;
                default:
                    break;
            }

            // 如果既不是单利 也不是已经有值了 那么new实例化一次
            return instanceOf(binding, args);

        }

        // 
        protected object instanceOf(IInjectorBinding binding,object[] args)
        {
            if(binding.value != null)
            {
                //需要判断value是type还是instacne
                return createFromValue(binding.value, args);
            }
            else
            {

                return null;
            }
        }

        protected object createFromValue(object o, object[] args)
        {
            Type value = (o is Type) ? o as Type : o.GetType();
            // value 要么是type 要么是instacne
            object retv = null;

            if (args == null || args.Length == 0)
            {
                retv = Activator.CreateInstance(value);
                //UnityEngine.Debug.LogError("反射实例化 " + value.ToString());
            }
            else
            {
                retv = Activator.CreateInstance(value, args);
                //UnityEngine.Debug.LogError("反射实例化 " + value.ToString());
            }

            return retv;
        }

    }
}
