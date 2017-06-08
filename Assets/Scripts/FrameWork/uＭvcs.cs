using System;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.IOCDI;
using FrameWork.Game;
namespace FrameWork.MVCS
{
    // 框架设置成单利 
    public class uＭvcs : MonoBehaviour
    {
        // 维护一个上下文Context
        private IInjectorBinder _injectorBinder;
        public IInjectorBinder injectorBinder
        {
            get
            {
                return _injectorBinder ?? (_injectorBinder = new InjectorBinder());
            }
            set
            {
                _injectorBinder = value;
            }
        }

        private IBox genrobox;
        void Start()
        {
            /* 模拟几个 object keyType  object valueType  => 其中keyType/valueType都是typeof(class)
             * 一般keyType是接口interface or singal/command
             * 
             * keyType ==》interface的情况
             * bind keyType to      valueType               普通注册 mapping
             * bind keyType to      valueType  toSingleton  做成单利
             * bind keyType toValue valueInstacen           绑定实例
             * 
             * 
             * keyType ==》singal/event的情况
             * bind keyType to      valueType
             * bind的过程 Add key操作 和 实例化keyType
             * To的过程   Add value操作 然后 delegate callbakc 触发
             * 一旦调用 触发 reslove的时候 将value 实例化
             * 
             * 也就是实例化instacne 要么是主动调用 比如inteface指定哪个实例就需要主动调用
             * 要么技术通过reslover的时候 调用 比如commnad就是reslover 触发调用
             * 
             * 
             */



            // for test 生成一个红色box
            // 这里只是注册
            injectorBinder.Bind<IBox>().To<redBoxFactory>();

            genrobox = injectorBinder.GetInstance<IBox>();
            genrobox.GetBox();
            // 如何将GenorateBox的信息注册额呢？
            //injectorBinder.Bind<GenorateBox>();

            //var componetn = this.gameObject.AddComponent<GenorateBox>();
            // start的时候 应该injecor一下
            // 添加一个Trigger 过程

            /*
             [Injector] 如何依赖注入
             
             */
            // 那么实例化的地方在哪



            //解绑
            injectorBinder.Unbind<IBox>();
            injectorBinder.Bind<IBox>().To<blueBoxFactory>();

            genrobox = injectorBinder.GetInstance<IBox>();
            genrobox.GetBox();
        }

    }
}
