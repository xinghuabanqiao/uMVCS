using System;
using System.Collections.Generic;
using System.Reflection;

namespace FrameWork.IOCDI
{
    public class Injector : IInjector
    {
        public IInjectorFactory factory { get; set; }
        public IInjectorBinder binder { get; set; }
        public IReflectorBinder reflector { get; set; }

        public Injector()
        {
            factory = new InjectorFactory();
        }

        // 实例化操作 核心的依赖注入
        public object Instantiate(IInjectorBinding binding)
        {
            object retv = null;
            Type reflectionType = null;
            if(binding.value is Type)
            {
                reflectionType = binding.value as Type;
            }
            else if(binding.value == null)
            {
                // 这里how
            }
            else // value以及存在了
            {
                retv = binding.value;
            }


            if(retv == null)
            {
                ReflectedClass refection = reflector.Get(reflectionType);
                // 首先通过通过构造参数 实例化当前type ==> instacne
                // 同时每个参数 每个参数如果是type的话 也需要实例化
                Type[] parameters = refection.ConstructorParameters;
                int aa = parameters.Length;
                object[] args = new object[aa];
                for (int a = 0; a < aa; a++)
                {
                    args[a] = getValueInjection(parameters[a] as Type,null);
                }
                retv = factory.Get(binding, args);// 实例化构造


                // 这里可以处理如果 Activator.CreateInstance returns null
                // 但是如果实例化都失败了 那么重新？这样处理 合理吗？
                // 不是应该控制 进来让他不出问题
                if (retv != null)
                {
                    if (binding.toInject)
                    {
                        retv = Inject(retv);
                        if (binding.type == InjectionBindingType.SINGLETON || binding.type == InjectionBindingType.VALUE)
                        {
                            //prevent double-injection
                            binding.ToInject(false);
                        }
                    }
                }

            }
            return retv;
        }

        public object Inject(object target)
        {
            //Some things can't be injected into. Bail out.
            Type t = target.GetType();
            if (t.IsPrimitive || t == typeof(Decimal) || t == typeof(string))
            {
                return target;
            }

            ReflectedClass reflection = reflector.Get(t);

            // 执行属性依赖注入
            performSetterInjection(target, reflection);

            return target;
        }

        //属性注入
        private void performSetterInjection(object target, ReflectedClass reflection)
        {
            int aa = reflection.Setters.Length;
            for (int a = 0; a < aa; a++)
            {
                KeyValuePair<Type, PropertyInfo> pair = reflection.Setters[a];
                object value = getValueInjection(pair.Key, reflection.SetterNames[a]);
                injectValueIntoPoint(value, target, pair.Value);
            }
        }

        private void injectValueIntoPoint(object value, object target, PropertyInfo point)
        {
            point.SetValue(target, value, null);
        }

        // 不过一般下 A依赖B 如果B的构造参数很多
        // 那么A实例化的时候  实例化依赖对象B 需要知道B的所有参数的实例而不是type
        // 那么这些参数从哪里来呢？
        // 这个问题有待探究 
            // t是属性type
            // naem是属性name
        // 感觉适当的依赖使用这个处理还是可以的 
        // 但是复杂的使用这个 就会很逻辑混乱
        private object getValueInjection(Type t, object name)
        {
            IInjectorBinding binding = binder.GetBinding(t,name);// key = t valuye = Dic{name,bindign} nae是二级索引默认是null
            if (binding.type == InjectionBindingType.VALUE)
            {
                if (!binding.toInject)
                {
                    return binding.value;
                }
                else
                {
                    // 注入
                    object retv = Inject(binding.value);
                    binding.ToInject(false);
                    return retv;
                }
            }
            else if (binding.type == InjectionBindingType.SINGLETON)
            {
                //暂时不处理
                return null;
            }
            else // defaoult 默认 也及时 需要实例化
            {
                return Instantiate(binding);
            }
        }
    }
}
