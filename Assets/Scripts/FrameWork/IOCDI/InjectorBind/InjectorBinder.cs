using System;
using System.Collections.Generic;


namespace FrameWork.IOCDI
{
    // 依赖绑定 派生于Binder
    // 很多操作 最后都是 base 操作
    // 核心的绑定类 专门处理
    // interface to instacne/type  tosingleton

    // 基础的注入绑定操作 维护了一个injector
    /*
     主要提供提供GetInstance操作
     
     */
    public class InjectorBinder :Binder, IInjectorBinder
    {
        private IInjector injector;

        public InjectorBinder()
        {
            injector = new Injector();
            injector.binder = this;
            injector.reflector = new ReflectorBinder();
        }
        #region 重载的bind操作 这里只是强制转换到派生类接口
        new public IInjectorBinding Bind<T>()
        {
            IBinding binding = base.Bind<T>();
            //return base.Bind<T>() as IInjectorBinding;
            var injectorbinding = binding as IInjectorBinding;
            return injectorbinding;
        }

        public IInjectorBinding Bind(Type key)
        {
            return base.Bind(key) as IInjectorBinding;
        }

        /*new public void Unbind<T>()
        {
            base.Unbind<T>();
        }

        new public void Unbind(object key)
        {
            base.Unbind(key);
            
        }*/
        #endregion

        #region 获取实例操作
        public T GetInstance<T>()
        {
            object instance = GetInstance(typeof(T));
            T retv = (T)instance;
            return retv;
        }

        public T GetInstance<T>(object name)
        {
            object instance = GetInstance(typeof(T), name);
            T retv = (T)instance;
            return retv;
        }

        public object GetInstance(Type key)
        {
            return GetInstance(key, null);
        }

        public virtual object GetInstance(Type key, object name) //为毛还virtual 应该没哟派生了
        {
            // 1 通过key 获取对应的InjectorBinding 对象
            // 2 获取Injector 
            // 3 通过Injector 注入实例 获取实例
            IInjectorBinding binding = GetBinding(key, name) as IInjectorBinding;
            if(binding == null)
            {
                //报错******
                
            }
            // 开始实例化
            object instance = injector.Instantiate(binding);
            return instance;

        }
        #endregion


        #region getBinding 操作
        new public IInjectorBinding GetBinding<T>()
        {
            return base.GetBinding<T>() as IInjectorBinding;
        }

        new public IInjectorBinding GetBinding(object key)
        {
            return base.GetBinding(key) as IInjectorBinding;
        }

        new public IInjectorBinding GetBinding(object key, object name)
        {
            return base.GetBinding(key, name) as IInjectorBinding;
        }

        // 重载 不能new覆盖 返回
        override public IBinding GetNewBinding()
        {
            return new InjectorBinding(resolver);
        }
        #endregion
    }
}
