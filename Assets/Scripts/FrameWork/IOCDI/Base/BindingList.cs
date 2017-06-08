using System;
using System.Collections.Generic;

namespace FrameWork.IOCDI
{
    public enum BindingConst
    {
        /// Null is an acceptable binding, but dictionaries choke on it, so we map null to this instead.
        NULLOID
    }

    public enum BindingConstraintType
    {
        /// 一个key 对应一个value / 一个vlaue对应一个key
        ONE,
        /// 一个key对应多个value  / 一个value对应多个key
        MANY,
        /// 关于pool 暂时不处理
        //POOL,
    }

    // key or vlaue的单位 就是一个list容器存放某个type的key or value 
    public class BindingList
    {
        // 使用数组效率更高 
        // 避免使用list 虽然方便
        protected object[] objectValue;

        public Enum constraint { get; set; }


        // 这里一般value for 独一无二one
        virtual public object value
        {
            get
            {
                if (constraint.Equals(BindingConstraintType.ONE))
                {
                    return (objectValue == null) ? null : objectValue[0];
                }
                return objectValue;
            }
        }

        // 所有value都是独一无二的 
        //public bool uniqueValues { get; set; }

        public BindingList()
        {
            constraint = BindingConstraintType.ONE;
            //uniqueValues = true;
        }

        //
        public void Add(object obj)
        {
            //
            if (objectValue == null || (BindingConstraintType)constraint == BindingConstraintType.ONE)
            {
                objectValue = new object[1];
            }
            else
            {
                // 多个value 
                // auto选择 覆盖操作 和 add new操作
                //if(uniqueValues) 默认每个value独一无二
                {
                    int aa = objectValue.Length;
                    for (int a = 0; a < aa; a++)
                    {
                        object val = objectValue[a];
                        if (val.Equals(obj))
                        {
                            //如果存在这个value 那么直接返回 不添加
                            return ;
                        }
                    }
                }

                // add 
                object[] tempList = objectValue;
                int len = tempList.Length;
                objectValue = new object[len + 1];
                tempList.CopyTo(objectValue, 0);
            }

            objectValue[objectValue.Length - 1] = obj;
            return;
        }

        // 这个少用
        public void Add(object[] list)
        {
            foreach (object item in list)
                Add(item);

            return ;
        }

        public void Remove(object obj)
        {
            if (obj.Equals(objectValue) || objectValue == null)
            {
                objectValue = null;
                return ;
            }

            int aa = objectValue.Length;
            for (int a = 0; a < aa; a++)
            {
                object currVal = objectValue[a];
                if (obj.Equals(currVal))
                {
                    //数组操作
                    spliceValueAt(a);
                    return ;
                }
            }
            return ;
        }

        protected void spliceValueAt(int splicePos)
        {
            object[] newList = new object[objectValue.Length - 1];
            int mod = 0;
            int aa = objectValue.Length;
            for (int a = 0; a < aa; a++)
            {
                if (a == splicePos)
                {
                    mod = -1;
                    continue;
                }
                newList[a + mod] = objectValue[a];
            }
            objectValue = (newList.Length == 0) ? null : newList;
        }
    }
}
