using System;
using System.Collections.Generic;
using System.Text;

namespace MaxV.Helper.Entities
{
    public class GlobalVariable<T>
    {
        private static T _object;
        public GlobalVariable(T Object)
        {
            _object = Object;
        }

        public static T Object
        {
            get => _object;
        }
    }
}
