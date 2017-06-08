using UnityEngine;
using System.Collections;

public class Bootstrapper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    // 启动框架 初始化框架
        //uMvcs 框架
        /*
         * View和Medatior： View负责显示  Medatior负责交互和逻辑处理
         * Controller 中央处理器 又command[]数组组成
         * Comannd 可以访问medatior singal/event 触发其他command
         * Model 通过databinding技术 实现model数据改变驱动view变换 【通过signal 触发 驱动command command再驱动medator
         * server 其实就是proxy 处理网络消息 可以直接修改model数据 or 通过singal or command修改mdoel数据  
         */
        // ioc + di 处理依赖问题
        // 

	}
}
