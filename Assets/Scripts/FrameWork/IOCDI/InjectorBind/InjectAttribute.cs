using System;


namespace FrameWork.IOCDI
{
    [AttributeUsage(AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class Inject : Attribute
    {
        public Inject() { }

        public Inject(object n)
        {
            name = n;
        }

        public object name { get; set; }
    }
}
