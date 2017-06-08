using System;
using System.Collections.Generic;


namespace FrameWork.IOCDI
{
    public class Binder : IBinder
    {
        // 一个callbakc 通过这个callback 可以通信binding
        // 
        public delegate void BindingResolver(IBinding binding);



        // 维护一个bindings 容器
        // 一个key可能绑定多个value 不同的vlue需要不同的标记
        protected Dictionary<object, Dictionary<object, IBinding>> bindings;

        public Binder()
        {
            bindings = new Dictionary<object, Dictionary<object, IBinding>>();
        }

        #region 绑定key操作
        virtual public IBinding Bind<T>()
        {
            return Bind(typeof(T));
        }

        virtual public IBinding Bind(object key)
        {
            //创建一个颗粒度 binding 然后存放到bindings容器中 原本是个依赖反正容器的 注册容器吧
            IBinding binding;
            // 这里不能直接new Binding 因为有派生类 也需要调用对应的派生类binding
            //binding = new Binding(resolver);
            binding = GetNewBinding();
            binding.Bind(key);
            return binding;

            // 为毛这里不讲binding add 到字典bindings中呢？
        }

        virtual public void Unbind<T>()
        {
            Unbind(typeof(T), null);
        }

        virtual public void Unbind(object key)
        {
            Unbind(key, null);
        }

        virtual public void Unbind(object key, object name)
        {
            if (bindings.ContainsKey(key))
            {
                Dictionary<object, IBinding> dict = bindings[key];
                object bindingName = (name == null) ? BindingConst.NULLOID : name;
                if (dict.ContainsKey(bindingName))
                {
                    dict.Remove(bindingName);
                }
            }
        }
        #endregion
        #region 获取颗粒度 Binding key/value/type等信息
        virtual public IBinding GetBinding<T>()
        {
            return GetBinding(typeof(T), null);
        }

        virtual public IBinding GetBinding(object key)
        {
            return GetBinding(key, null);
        }

        virtual public IBinding GetBinding(object key, object name)
        {

            //

            //
            if(bindings.ContainsKey(key))
            {
                Dictionary<object, IBinding> dict = bindings[key]; // 获取对应绑定信息
                name = (name == null) ? BindingConst.NULLOID : name;
                if (dict.ContainsKey(name))
                {
                    return dict[name];
                }
            }

            return null;
        }
        #endregion

        // 
        virtual public IBinding GetNewBinding()
        {
            return new Binding(resolver);
        }

        /// The default handler for resolving bindings during chained commands
        virtual protected void resolver(IBinding binding)
        {
            // 这里添加bindings add key value
            object key = binding.key;
            // 辨别 是否一对一还是一对多 还是多对一
            // 这里我们默认1：1
            ResolveBinding(binding, key);


        }

        // bindings add key value
        // key {name value}冲突处理
        virtual public void ResolveBinding(IBinding binding,object key)
        {

            object bindingName = (binding.name == null) ? BindingConst.NULLOID : binding.name;
            Dictionary<object, IBinding> dict;
            if(bindings.ContainsKey(key))
            {
                dict = bindings[key];
                if(dict.ContainsKey(bindingName))
                {

                }
                else
                {

                }
            }
            else
            {
                dict = new Dictionary<object, IBinding>();
                bindings.Add(key, dict);
            }

            if (!dict.ContainsKey(bindingName))
            {
                dict.Add(bindingName, binding);
            }

        }
    }
}
