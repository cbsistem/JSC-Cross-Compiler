﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestVirtualPropertyMerge
{
    public class Vector<T>
    {
    }

    public class Class1
    {
        public event Action<int> Handler;

        public Vector<Action> MyProperty { get; set; }
        public virtual int MyVirtualProperty { get; set; }
    }


    public class Class2 : Class1
    {
        public override int MyVirtualProperty { get; set; }

        void ReferenceGenric(Vector<Class1> u)
        {

        }
    }
}
