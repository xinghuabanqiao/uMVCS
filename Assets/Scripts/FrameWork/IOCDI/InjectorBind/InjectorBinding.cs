using System;

namespace FrameWork.IOCDI
{
    public enum InjectionBindingType
    {
        /// The binding provides a new instance every time
        DEFAULT,

        /// The binding always provides the same instance
        SINGLETON,

        /// The binding always provides the same instance based on a provided value
        VALUE,
    }

    public class InjectorBinding : Binding , IInjectorBinding
    {
        // 注入形式
        private InjectionBindingType _type = InjectionBindingType.DEFAULT;
        public InjectionBindingType type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }


        // 绑定binding是否已经注入过了
        private bool _toInject = true;
        public bool toInject
        {
            get
            {
                return _toInject;
            }
        }


        public InjectorBinding(Binder.BindingResolver resolver)
        {
            this.resolver = resolver;
        }

        // 注入标记设置
        public IInjectorBinding ToInject(bool value)
        {
            _toInject = value;
            return this;
        }

        #region bind to操作 重载 调用base基类方法 
        new public IInjectorBinding Bind<T>()
        {
            return base.Bind<T>() as IInjectorBinding;
        }

        new public IInjectorBinding Bind(object key)
        {
            return base.Bind(key) as IInjectorBinding;
        }

        new public IInjectorBinding To<T>()
        {
            return base.To<T>() as IInjectorBinding;
        }

        new public IInjectorBinding To(object o)
        {
            return base.To(o) as IInjectorBinding;
        }
        #endregion



    }
}
