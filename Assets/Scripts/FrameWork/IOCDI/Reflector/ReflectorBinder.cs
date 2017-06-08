using System;
using System.Collections.Generic;
using System.Reflection;

namespace FrameWork.IOCDI
{
    public class ReflectorBinder : Binder, IReflectorBinder
    {
        public ReflectorBinder()
        {
        }

        #region get 反射信息
        public ReflectedClass Get<T>()
        {
            return Get(typeof(T));
        }

        public ReflectedClass Get(Type type)
        {
            ReflectedClass retv;

            // 通过type ==》GetBinding获取对应binding
            IBinding binding = GetBinding(type);
            if(binding == null)
            {
                binding = GetNewBinding();
                ReflectedClass reflected = new ReflectedClass();
                // 获取构造信息
                mapPreferredConstructor(reflected, type);
                // 获取属性信息
                mapSetters(reflected,type);

                // 注册
                binding.Bind(type).To(reflected);
                retv = binding.value as ReflectedClass;
            }
            else
            {
                retv = binding.value as ReflectedClass;

            }

            return retv;
        }

        #endregion

        // 获取构造信息 有很多构造 我们需要选择一个 
        // 这是个问题 应该选择哪个构造呢 万一构造信息很多怎么把
        // 我们简单处理 
        // 参考 选择构造参数最少的
        // 这个应该auto 使用。。。但是多个带有参数的构造 确实反射很蛋疼
        private void mapPreferredConstructor(ReflectedClass reflected, Type type)
        {
            ConstructorInfo constructor = findPreferredConstructor(type);

            ParameterInfo[] parameters = constructor.GetParameters();

            Type[] paramList = new Type[parameters.Length];
            int i = 0;
            foreach (ParameterInfo param in parameters)
            {
                Type paramType = param.ParameterType;
                paramList[i] = paramType;
                i++;
            }

            reflected.Constructor = constructor;
            reflected.ConstructorParameters = paramList;
        }

        // 蛋疼。。
        //Look for a constructor in the order:
        //1. Only one (just return it, since it's our only option)
        //2. Tagged with [Construct] tag
        //3. The constructor with the fewest parameters
        // 我们这里特殊处理 值返回第一个。。。。
        private ConstructorInfo findPreferredConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.FlattenHierarchy |
                                                                        BindingFlags.Public |
                                                                        BindingFlags.Instance |
                                                                        BindingFlags.InvokeMethod);
            if (constructors.Length == 1)
            {
                return constructors[0];
            }

            return constructors[0];

            /*int len;
            int shortestLen = int.MaxValue;
            ConstructorInfo shortestConstructor = null;
            foreach (ConstructorInfo constructor in constructors)
            {
                object[] taggedConstructors = constructor.GetCustomAttributes(typeof(Construct), true);
                if (taggedConstructors.Length > 0)
                {
                    return constructor;
                }
                len = constructor.GetParameters().Length;
                if (len < shortestLen)
                {
                    shortestLen = len;
                    shortestConstructor = constructor;
                }
            }
            return shortestConstructor;
            */
        }



        private void mapSetters(ReflectedClass reflected, Type type)
        {
            // 属性注入 type 和 对应属性数据
            // 都是使用数组操作 速度效率高些
            KeyValuePair<Type, PropertyInfo>[] pairs = new KeyValuePair<Type, PropertyInfo>[0];
            // 属性名称
            object[] names = new object[0];

            // 
            MemberInfo[] members = type.FindMembers(MemberTypes.Property,
                                              BindingFlags.FlattenHierarchy |
                                              BindingFlags.SetProperty |
                                              BindingFlags.Public |
                                              BindingFlags.Instance,
                                              null, null);
            foreach(MemberInfo member in members)
            {
                object[] injections = member.GetCustomAttributes(typeof(Inject), true);
                if (injections.Length > 0)
                {
                    Inject attr = injections[0] as Inject;

                    PropertyInfo point = member as PropertyInfo;
                    Type pointType = point.PropertyType;

                    // type ==> ProperInfo
                    KeyValuePair<Type, PropertyInfo> pair = new KeyValuePair<Type, PropertyInfo>(pointType, point);
                    pairs = AddKV(pair, pairs);

                    object bindingName = attr.name;
                    names = Add(bindingName, names);
                }

            }

            reflected.Setters = pairs;
            reflected.SetterNames = names;
        }

        private KeyValuePair<Type, PropertyInfo>[] AddKV(KeyValuePair<Type, PropertyInfo> value, 
                                                         KeyValuePair<Type, PropertyInfo>[] list)
        {
            KeyValuePair<Type, PropertyInfo>[] tempList = list;
            int len = tempList.Length;
            list = new KeyValuePair<Type, PropertyInfo>[len + 1];
            tempList.CopyTo(list, 0);
            list[len] = value;
            return list;
        }

        private object[] Add(object value, object[] list)
        {
            object[] tempList = list;
            int len = tempList.Length;
            list = new object[len + 1];
            tempList.CopyTo(list, 0);
            list[len] = value;
            return list;
        }

    }
}
